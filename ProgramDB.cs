using System;
using System.Collections.Generic;
using System.Text;
using FreeLibSet.IO;
using FreeLibSet.Data.Docs;
using FreeLibSet.Logging;
using FreeLibSet.Data;
using System.IO;
using System.Data;
using FreeLibSet.Core;
using FreeLibSet.Collections;

namespace App
{
  /// <summary>
  /// ����������� � ���� ������
  /// </summary>
  internal class ProgramDB : DisposableObject
  {
    #region ����������� � Dispose()

    /// <summary>
    /// ����������� ������ �� ������.
    /// ������������� ����������� � ������ InitDB()
    /// </summary>
    public ProgramDB()
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (_GlobalDocData != null)
        {
          _GlobalDocData.DisposeDBs();
          _GlobalDocData = null;
        }
      }
      if (_DocTypes != null)
        _DocTypes = null;

      _GlobalDocData = null;

      base.Dispose(disposing);
    }

    #endregion

    #region InitDB()

    public void InitDB(AbsPath dbDir, ISplash spl)
    {
      #region ���������� ����� ����������

      string oldPT = spl.PhaseText;
      spl.PhaseText = "���������� ����� ����������";

      InitDocTypes();

      spl.PhaseText = oldPT;

      #endregion

      // ������� ����� FileTools.ForceDirs(DBDir); // ����� �������

      #region ������������� DBConnectionHelper

      InitDBConnectionHelper(dbDir);

      #endregion

      #region �������� / ���������� ��� ������ ����������

      // DBxRealDocProviderGlobal.DefaultClearCacheBuffer = 30; // ���������� ����� ������������� �� ������ �� �������, �� � ��� ������� ��������
      _DBConnectionHelper.Splash = spl;
      _GlobalDocData = _DBConnectionHelper.CreateRealDocProviderGlobal();
      InitDocTextValues.Init(_GlobalDocData.TextHandlers);
      _DBConnectionHelper.Splash = null; // �� ����� ��������
      if (_DBConnectionHelper.Errors.Count > 0)
      {
        // 13.05.2017
        // ������������ ��������� � log-�����
        AbsPath logFilePath = LogoutTools.GetLogFileName("DBChange", String.Empty);
        using (StreamWriter wrt = new StreamWriter(logFilePath.Path, false, LogoutTools.LogEncoding))
        {
          wrt.WriteLine("��������� ��������� ��� ������ BigPurse");
          wrt.WriteLine("�����: " + DateTime.Now.ToString());
          // ��� �� ���������������� wrt.WriteLine("����� ������ �������: " + this.ServerSessionId.ToString());
          DBx[] AllDBx = _GlobalDocData.GetDBs();
          if (AllDBx.Length > 0) // ������ �����������
          {
            wrt.WriteLine("������ �������: " + AllDBx[0].ServerVersionText);
            wrt.WriteLine("��� ���� ������:");
            for (int i = 0; i < AllDBx.Length; i++)
            {
              wrt.WriteLine((i + 1).ToString() + ". " + AllDBx[i].DisplayName + ". " + AllDBx[i].UnpasswordedConnectionString);
            }
          }
          wrt.Write("----------");
          wrt.WriteLine();
          wrt.Write(_DBConnectionHelper.Errors.AllText);
        }
      }

      using (DBxCon con = new DBxCon(GlobalDocData.MainDBEntry))
      {
        _DataVersionHandler.InitTableRow(con);
      }

      #endregion

      #region ����� ������������

      _Source = new DBxRealDocProviderSource(GlobalDocData);

      UserPermissionCreators upcs = new UserPermissionCreators();

      UserPermissions ups = new UserPermissions(upcs);
      _Source.UserPermissions = ups;

      _Source.SetReadOnly();

      #endregion

      _DBDir = dbDir;
    }

    /// <summary>
    /// ������� ���� ������
    /// </summary>
    public static AbsPath DBDir { get { return _DBDir; } }
    private static AbsPath _DBDir;

    #endregion

    #region ���������� ����� ����������

    /// <summary>
    /// ���������� ����� ����������
    /// </summary>
    public DBxDocTypes DocTypes { get { return _DocTypes; } }
    private DBxDocTypes _DocTypes;

    private void InitDocTypes()
    {
      _DocTypes = new DBxDocTypes();

      _DocTypes.UseDeleted = true; // ��������� �������� ����������
      _DocTypes.UseVersions = true; // ���������� ������ ����������
      _DocTypes.UseTime = true; // ���������� �����
      _DocTypes.UsersTableName = null; // �� ������������

      DBxDocType dt;
      DBxSubDocType sdt;

      #region ��������

      #region �������� ��������

      dt = new DBxDocType("Operations");
      dt.PluralTitle = "��������";
      dt.SingularTitle = "��������";
      dt.Struct.Columns.AddDate("Date", false);
      dt.Struct.Columns.AddInt16("OpOrder", false);
      dt.Struct.Columns.AddInt("OpType", DataTools.GetEnumRange(typeof(OperationType))).Nullable = false;
      dt.Struct.Columns.AddReference("WalletDebt", "Wallets", true);
      dt.Struct.Columns.AddReference("WalletCredit", "Wallets", true);
      dt.Struct.Columns.AddReference("IncomeSource", "IncomeSources", true);
      dt.Struct.Columns.AddReference("Shop", "Shops", true);
      dt.Struct.Columns.AddReference("Debtor", "Debtors", true);
      dt.Struct.Columns.AddMoney("InlineSum");
      dt.Struct.Columns.AddMoney("TotalDebt");
      dt.Struct.Columns.AddMoney("TotalCredit");
      dt.Struct.Columns.AddMemo("Comment");

      #region ����������� ����

      dt.Struct.Columns.AddString("DisplayName", 100, false);
      dt.CalculatedColumns.Add("DisplayName");
      dt.Struct.Columns.AddInt("OpOrder2", 1, 2, false);

      #endregion

      dt.BeforeWrite += new ServerDocTypeBeforeWriteEventHandler(Operation_BeforeWrite);
      dt.DefaultOrder = DBxOrder.FromDataViewSort("Date,OpOrder,OpOrder2,Id");
      dt.Struct.Indexes.Add("Date,OpOrder,OpOrder2,Id");
      _DocTypes.Add(dt);

      #endregion

      #region ������ �������

      sdt = new DBxSubDocType("OperationProducts");
      sdt.SingularTitle = "������ � ��������� �������";
      sdt.PluralTitle = "����� � �������� �������";

      sdt.Struct.Columns.AddInt16("RecordOrder");
      sdt.Struct.Columns.AddReference("Product", "Products", false);
      sdt.Struct.Columns.AddString("Description", 100, true); 
      sdt.Struct.Columns.AddSingle("Quantity1", true);
      sdt.Struct.Columns.AddReference("MU1", "MUs", true);
      sdt.Struct.Columns.AddSingle("Quantity2", true);
      sdt.Struct.Columns.AddReference("MU2", "MUs", true);
      sdt.Struct.Columns.AddSingle("Quantity3", true);
      sdt.Struct.Columns.AddReference("MU3", "MUs", true);
      sdt.Struct.Columns.AddString("Formula", 100, true);
      sdt.Struct.Columns.AddMoney("RecordSum");
      sdt.Struct.Columns.AddReference("Purpose", "Purposes", true);
      sdt.Struct.Columns.AddMemo("Comment");
      sdt.DefaultOrder = new DBxOrder("RecordOrder");
      dt.SubDocs.Add(sdt);

      #endregion

      #endregion

      #region ��������

      dt = new DBxDocType("Wallets");
      dt.SingularTitle = "�������";
      dt.PluralTitle = "��������";
      dt.Struct.Columns.AddString("Name", 50, false);
      dt.Struct.Columns.AddMemo("Comment");
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region ��������� ������

      dt = new DBxDocType("IncomeSources");
      dt.SingularTitle = "�������� ������";
      dt.PluralTitle = "��������� ������";
      dt.Struct.Columns.AddString("Name", 100, false);
      dt.Struct.Columns.AddMemo("Comment");
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region ��������

      #region ������

      dt = new DBxDocType("ShopGroups");
      dt.SingularTitle = "������ ���������";
      dt.PluralTitle = "������ ���������";
      dt.Struct.Columns.AddString("Name", 40, false);
      dt.Struct.Columns.AddReference("ParentId", "ShopGroups", true); // ���������� ������ �����
      dt.TreeParentColumnName = "ParentId";
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region �������� ��������

      dt = new DBxDocType("Shops");
      dt.SingularTitle = "�������";
      dt.PluralTitle = "��������";
      dt.Struct.Columns.AddString("Name", 100, false);
      dt.Struct.Columns.AddMemo("Comment");

      dt.Struct.Columns.AddReference("GroupId", "ShopGroups", true);
      dt.GroupRefColumnName = "GroupId";

      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #endregion

      #region ��������/���������

      dt = new DBxDocType("Debtors");
      dt.SingularTitle = "�������/��������";
      dt.PluralTitle = "��������/���������";
      dt.Struct.Columns.AddString("Name", 100, false);
      dt.Struct.Columns.AddMemo("Comment");
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region ������� ���������

      dt = new DBxDocType("MUs");
      dt.SingularTitle = "������� ���������";
      dt.PluralTitle = "������� ���������";
      dt.Struct.Columns.AddString("Name", 20, false);
      dt.Struct.Columns.AddMemo("Comment");
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region ������

      #region �������� ��������

      dt = new DBxDocType("Products");
      dt.SingularTitle = "�����";
      dt.PluralTitle = "������";
      dt.Struct.Columns.AddString("Name", 200, false);
      dt.Struct.Columns.AddReference("ParentId", "Products", true);
      dt.TreeParentColumnName = "ParentId";

      dt.Struct.Columns.AddInt("DescriptionPresence", DataTools.GetEnumRange(typeof(PresenceType)));
      dt.Struct.Columns.LastAdded.Nullable = true;
      dt.Struct.Columns.AddInt("QuantityPresence", DataTools.GetEnumRange(typeof(PresenceType)));
      dt.Struct.Columns.LastAdded.Nullable = true;
      dt.Struct.Columns.AddInt("PurposePresence", DataTools.GetEnumRange(typeof(PresenceType)));
      dt.Struct.Columns.LastAdded.Nullable = true;
      dt.Struct.Columns.AddInt16("MUSetCount");
      dt.CalculatedColumns.Add("MUSetCount");

      dt.Struct.Columns.AddMemo("Comment");
      dt.BeforeWrite += new ServerDocTypeBeforeWriteEventHandler(Product_BeforeWrite);
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region ������ ������ ���������

      sdt = new DBxSubDocType("ProductMUSets");
      sdt.SingularTitle = "���������� ��������� ������ ��������� � ��� ������";

      sdt.Struct.Columns.AddReference("MU1", "MUs", false);
      sdt.Struct.Columns.AddReference("MU2", "MUs", true);
      sdt.Struct.Columns.AddReference("MU3", "MUs", true);
      dt.SubDocs.Add(sdt);

      #endregion

      #endregion

      #region ����������

      dt = new DBxDocType("Purposes");
      dt.SingularTitle = "����������";
      dt.PluralTitle = "����������";
      dt.Struct.Columns.AddString("Name", 50, false);
      dt.Struct.Columns.AddMemo("Comment");
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region �������������� �������������

      foreach (DBxDocType dt2 in DocTypes)
      {
        // ����-���� ������������ ������������ ������ � �������� ��������

        if (dt2.Struct.Columns.Contains("Comment"))
          dt2.IndividualCacheColumns["Comment"] = false;
      }

      #endregion
    }

    #endregion

    #region ����������� ���������� �� ������� �������

    #region Operations

    void Operation_BeforeWrite(object sender, ServerDocTypeBeforeWriteEventArgs args)
    {
      OperationType opType = (OperationType)(args.Doc.Values["OpType"].AsInteger);

      args.Doc.Values["OpOrder2"].SetInteger(Tools.GetOpOrder2(opType));

      string sName = String.Empty;
      decimal totalDebt = 0m;
      decimal totalCredit = 0m;
      decimal inlineSum = args.Doc.Values["InlineSum"].AsDecimal;
      switch (opType)
      {
        case OperationType.Balance:
          break;
        case OperationType.Income:
          totalDebt = inlineSum;
          Int32 incSrcId = args.Doc.Values["IncomeSource"].AsInteger;
          sName = args.Doc.DocProvider.GetTextValue("IncomeSources", incSrcId);
          break;
        case OperationType.Expense:
          DataTable table = args.Doc.SubDocs["OperationProducts"].CreateSubDocsData();
          totalCredit = DataTools.SumDecimal(table, "RecordSum") + inlineSum;
          if (table.Rows.Count > 0)
          {
            SingleScopeStringList lst = new SingleScopeStringList(false);
            foreach (DataRow row in table.Rows)
            {
              string s1 = DataTools.GetString(row, "Description");
              if (s1.Length > 0)
                lst.Add(s1);
              else
              {
                Int32 productId = DataTools.GetInt(row, "Product");
                lst.Add(args.Doc.DocProvider.GetTextValue("Products", productId));
              }
            }
            sName = String.Join(", ", lst.ToArray());
          }
          break;
        case OperationType.Move:
          totalDebt = inlineSum;
          totalCredit = inlineSum;
          break;
        case OperationType.Debt:
          totalDebt = inlineSum;
          break;
        case OperationType.Credit:
          totalCredit = inlineSum;
          break;
        default:
          sName = "����������� ��� ��������";
          break;
      }

      if (String.IsNullOrEmpty(sName))
        sName = Tools.ToString(opType);

      args.Doc.Values["DisplayName"].SetString(sName);
      args.Doc.Values["TotalDebt"].SetDecimal(totalDebt);
      args.Doc.Values["TotalCredit"].SetDecimal(totalCredit);
    }

    #endregion

    #region Operations

    void Product_BeforeWrite(object sender, ServerDocTypeBeforeWriteEventArgs args)
    {
      args.Doc.Values["MUSetCount"].SetInteger(args.Doc.SubDocs["ProductMUSets"].NonDeletedSubDocCount);

      ProductBuffer.ResetProductData();
    }

    #endregion

    #endregion

    #region DBConnectionHelper

    /// <summary>
    /// ��������� ��� ������
    /// </summary>
    private DBxDocDBConnectionHelper _DBConnectionHelper;

    private DBxDataVersionHandler _DataVersionHandler;

    /// <summary>
    /// ������������� ������� DBxDocDBConnectionHelper
    /// </summary>
    /// <param name="DBDir"></param>
    /// <returns></returns>
    public void InitDBConnectionHelper(AbsPath dbDir)
    {
      _DBConnectionHelper = new DBxDocDBConnectionHelper();
      _DBConnectionHelper.DBDir = dbDir;

      _DBConnectionHelper.ProviderName = "SQLite";
      AbsPath FileName = new AbsPath(dbDir, "db.db");
      _DBConnectionHelper.ConnectionString = "Data Source=" + FileName.Path;

      _DBConnectionHelper.CommandTimeout = 0; // ����������� ����� ���������� ������
      _DBConnectionHelper.DocTypes = _DocTypes;

      #region ��������� ������������� (������ ������������)

      DBxTableStruct ts = new DBxTableStruct("UserSettings");
      ts.Columns.AddId();
      ts.Columns.AddString("Name", ConfigSection.MaxSectionNameLength, false);
      ts.Columns.AddString("Category", ConfigSection.MaxCategoryLength, false);
      ts.Columns.AddString("ConfigName", ConfigSection.MaxConfigNameLength, true);
      ts.Columns.AddXmlConfig("Data");
      ts.Columns.AddDateTime("WriteTime", true); // ���� ��������� ������
      //ts.Indices.Add("������������");
      ts.Indexes.Add("Name,Category,ConfigName");

      _DBConnectionHelper.MainDBStruct.Tables.Add(ts);

      #endregion

      _DataVersionHandler = new DBxDataVersionHandler(new Guid("d16e16de-4b22-4170-b5d7-d480f668fa8a"), 1, 1);
      _DataVersionHandler.AddTableStruct(_DBConnectionHelper.MainDBStruct);
    }

    #endregion

    #region ����� �����������

    /// <summary>
    /// �������� ������ ������� ����������
    /// </summary>
    public DBxRealDocProviderGlobal GlobalDocData { get { return _GlobalDocData; } }
    private DBxRealDocProviderGlobal _GlobalDocData;

    public DBxRealDocProviderSource Source { get { return _Source; } }
    private DBxRealDocProviderSource _Source;

    /// <summary>
    /// �������� ����������� � ���� ������ � ������ ������� ���� (��� ��������, ����������� ����������)
    /// </summary>
    public DBxEntry MainEntry { get { return _GlobalDocData.MainDBEntry; } }

    #endregion

    public DBxDocProvider CreateDocProvider()
    {
      return new DBxRealDocProvider(Source, 0, false);
    }

    #region ��������� �����������

    public static AbsPath BackupDir
    {
      get
      {
        if (ProgramDBUI.Settings == null)
          return AbsPath.Empty;
        else
          return FileTools.ApplicationBaseDir + new RelPath(ProgramDBUI.Settings.BackupDir);
      }
    }

    internal void CreateBackup(ISplash spl)
    {
      FileTools.ForceDirs(BackupDir);
      FileCompressor fc = new FileCompressor();
      fc.ArchiveFileName = new AbsPath(BackupDir, DateTime.Now.ToString(@"yyyyMMdd\-HHmmss", StdConvert.DateTimeFormat) + ".7z");
      fc.FileDirectory = _DBConnectionHelper.DBDir;
      fc.FileTemplates.Add("db.db");
      fc.FileTemplates.Add("undo.db");
      fc.Compress();
    }

    internal void RemoveOldBackups(ISplash spl)
    {
      spl.PhaseText = "����� ������ �����";

      string[] aFiles = System.IO.Directory.GetFiles(BackupDir.Path, "????????-??????.7z", SearchOption.TopDirectoryOnly);
      for (int i = 0; i < aFiles.Length; i++)
      {
        DateTime dt = System.IO.File.GetLastWriteTime(aFiles[i]);
        TimeSpan ts = DateTime.Today - dt;
        if (ts.TotalDays > 7)
        {
          spl.PhaseText = "�������� " + aFiles[i];
          try
          {
            System.IO.File.Delete(aFiles[i]);
          }
          catch { }
        }
      }
    }

    #endregion
  }
}
