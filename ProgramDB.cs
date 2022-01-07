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

namespace BigPurse
{
  /// <summary>
  /// Подключение к базе данных
  /// </summary>
  internal class ProgramDB : DisposableObject
  {
    #region Конструктор и Dispose

    /// <summary>
    /// Конструктор ничего не делает.
    /// Инициализация выполняется в методе InitDB()
    /// </summary>
    public ProgramDB()
    {
    }

    protected override void Dispose(bool Disposing)
    {
      if (Disposing)
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

      base.Dispose(Disposing);
    }

    #endregion

    #region InitDB()

    public void InitDB(AbsPath DBDir, ISplash Splash)
    {
      #region Объявление видов документов

      string OldPT = Splash.PhaseText;
      Splash.PhaseText = "Объявление видов документов";

      InitDocTypes();

      Splash.PhaseText = OldPT;

      #endregion

      // вызвано ранее FileTools.ForceDirs(DBDir); // сразу создаем

      #region Инициализация DBConnectionHelper

      InitDBConnectionHelper(DBDir);

      #endregion

      #region Создание / обновление баз данных документов

      // DBxRealDocProviderGlobal.DefaultClearCacheBuffer = 30; // обновления могут переключаться не только по таймеру, но и при запуске процедур
      _DBConnectionHelper.Splash = Splash;
      _GlobalDocData = _DBConnectionHelper.CreateRealDocProviderGlobal();
      _DBConnectionHelper.Splash = null; // он скоро исчезнет
      if (_DBConnectionHelper.Errors.Count > 0)
      {
        // 13.05.2017
        // Регистрируем сообщения в log-файле
        AbsPath LogFilePath = LogoutTools.GetLogFileName("DBChange", String.Empty);
        using (StreamWriter wrt = new StreamWriter(LogFilePath.Path, false, LogoutTools.LogEncoding))
        {
          wrt.WriteLine("Изменение структуры баз данных BigPurse");
          wrt.WriteLine("Время: " + DateTime.Now.ToString());
          // еще не инициализировано wrt.WriteLine("Сеанс работы сервера: " + this.ServerSessionId.ToString());
          DBx[] AllDBx = _GlobalDocData.GetDBs();
          if (AllDBx.Length > 0) // всегда выполняется
          {
            wrt.WriteLine("Версия сервера: " + AllDBx[0].ServerVersionText);
            wrt.WriteLine("Все базы данных:");
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

      using (DBxCon Con = new DBxCon(GlobalDocData.MainDBEntry))
      {
        _DataVersionHandler.InitTableRow(Con);
      }

      #endregion

      #region Права пользователя

      _Source = new DBxRealDocProviderSource(GlobalDocData);

      UserPermissionCreators upcs = new UserPermissionCreators();

      UserPermissions ups = new UserPermissions(upcs);
      _Source.UserPermissions = ups;

      _Source.SetReadOnly();

      #endregion

      _DBDir = DBDir;
    }

    /// <summary>
    /// Каталог базы данных
    /// </summary>
    public static AbsPath DBDir { get { return _DBDir; } }
    private static AbsPath _DBDir;

    #endregion

    #region Объявление видов документов

    /// <summary>
    /// Объявления видов документов
    /// </summary>
    public DBxDocTypes DocTypes { get { return _DocTypes; } }
    private DBxDocTypes _DocTypes;

    private void InitDocTypes()
    {
      _DocTypes = new DBxDocTypes();

      _DocTypes.UseDeleted = true; // обратимое удаление документов
      _DocTypes.UseVersions = true; // используем версии документов
      _DocTypes.UseTime = true; // запоминаем время
      _DocTypes.UsersTableName = null; // не авторизуемся

      DBxDocType dt;
      DBxSubDocType sdt;

      #region Операции

      #region Основной документ

      dt = new DBxDocType("Operations");
      dt.PluralTitle = "Операции";
      dt.SingularTitle = "Операция";
      dt.Struct.Columns.AddDate("Date", false);
      dt.Struct.Columns.AddInt16("OpOrder");
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

      #region Вычисляемые поля

      dt.Struct.Columns.AddString("DisplayName", 100, true);
      dt.CalculatedColumns.Add("DisplayName");

      #endregion

      dt.BeforeWrite += new ServerDocTypeBeforeWriteEventHandler(Operation_BeforeWrite);
      dt.DefaultOrder = DBxOrder.FromDataViewSort("Date,OpOrder,Id");
      dt.Struct.Indexes.Add("Date,OpOrder,Id");
      _DocTypes.Add(dt);

      #endregion

      #region Записи расхода

      sdt = new DBxSubDocType("OperationProducts");
      sdt.SingularTitle = "Товары в операциях расхода";
      sdt.PluralTitle = "Товар в операции расхода";

      sdt.Struct.Columns.AddInt16("RecordOrder");
      sdt.Struct.Columns.AddReference("Product", "Products", false);
      sdt.Struct.Columns.AddString("Description", 100, false);
      sdt.Struct.Columns.AddInt("Quantity", true);
      sdt.Struct.Columns.AddString("Unit", 50, true);
      sdt.Struct.Columns.AddString("Formula", 100, true);
      sdt.Struct.Columns.AddMoney("RecordSum");
      sdt.Struct.Columns.AddMemo("Comment");
      sdt.DefaultOrder = new DBxOrder("RecordOrder");
      dt.SubDocs.Add(sdt);

      #endregion

      #endregion

      #region Кошельки

      dt = new DBxDocType("Wallets");
      dt.SingularTitle = "Кошелек";
      dt.PluralTitle = "Кошельки";
      dt.Struct.Columns.AddString("Name", 50, false);
      dt.Struct.Columns.AddMemo("Comment");
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region Источники дохода

      dt = new DBxDocType("IncomeSources");
      dt.SingularTitle = "Источник дохода";
      dt.PluralTitle = "Источники дохода";
      dt.Struct.Columns.AddString("Name", 100, false);
      dt.Struct.Columns.AddMemo("Comment");
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region Магазины

      dt = new DBxDocType("Shops");
      dt.SingularTitle = "Магазин";
      dt.PluralTitle = "Магазины";
      dt.Struct.Columns.AddString("Name", 100, false);
      dt.Struct.Columns.AddMemo("Comment");
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region Дебиторы/Кредиторы

      dt = new DBxDocType("Debtors");
      dt.SingularTitle = "Дебитор/кредитор";
      dt.PluralTitle = "Дебиторы/кредиторы";
      dt.Struct.Columns.AddString("Name", 100, false);
      dt.Struct.Columns.AddMemo("Comment");
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region Товары

      dt = new DBxDocType("Products");
      dt.SingularTitle = "Товар";
      dt.PluralTitle = "Товары";
      dt.Struct.Columns.AddString("Name", 200, false);
      dt.Struct.Columns.AddReference("ParentId", "Products", true);
      dt.TreeParentColumnName = "ParentId";
      dt.Struct.Columns.AddMemo("Comment");
      dt.DefaultOrder = new DBxOrder("Name");
      _DocTypes.Add(dt);

      #endregion

      #region Дополнительная инициализация

      foreach (DBxDocType dt2 in DocTypes)
      {
        // МЕМО-поля комментариев буферизуются вместе с основной таблицей

        if (dt2.Struct.Columns.Contains("Comment"))
          dt2.IndividualCacheColumns["Comment"] = false;
      }

      #endregion
    }

    #endregion

    #region Обработчики документов на стороне сервера

    #region Operations

    void Operation_BeforeWrite(object sender, ServerDocTypeBeforeWriteEventArgs args)
    {
      OperationType opType = (OperationType)(args.Doc.Values["OpType"].AsInteger);
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
          //sName=DataTools.GetStringsFromColumn
          break;
        case OperationType.Debt:
          totalDebt = inlineSum;
          break;
        case OperationType.Credit:
          totalCredit = inlineSum;
          break;
        default:
          sName = "Неизвестный тип операции";
          break;
      }

      if (String.IsNullOrEmpty(sName))
        sName = ProgramConvert.ToString(opType);

      args.Doc.Values["DisplayName"].SetString(sName);
    }

    #endregion

    #endregion

    #region DBConnectionHelper

    /// <summary>
    /// Создатель баз данных
    /// </summary>
    private DBxDocDBConnectionHelper _DBConnectionHelper;

    private DBxDataVersionHandler _DataVersionHandler;

    /// <summary>
    /// Инициализация объекта DBxDocDBConnectionHelper
    /// </summary>
    /// <param name="DBDir"></param>
    /// <returns></returns>
    public void InitDBConnectionHelper(AbsPath DBDir)
    {
      _DBConnectionHelper = new DBxDocDBConnectionHelper();
      _DBConnectionHelper.DBDir = DBDir;

      _DBConnectionHelper.ProviderName = "SQLite";
      AbsPath FileName = new AbsPath(DBDir, "db.db");
      _DBConnectionHelper.ConnectionString = "Data Source=" + FileName.Path;

      _DBConnectionHelper.CommandTimeout = 0; // бесконечное время выполнение команд
      _DBConnectionHelper.DocTypes = _DocTypes;

      #region Настройки пользователей (секции конфигурации)

      DBxTableStruct ts = new DBxTableStruct("UserSettings");
      ts.Columns.AddId();
      ts.Columns.AddString("Name", ConfigSection.MaxSectionNameLength, false);
      ts.Columns.AddString("Category", ConfigSection.MaxCategoryLength, false);
      ts.Columns.AddString("ConfigName", ConfigSection.MaxConfigNameLength, true);
      ts.Columns.AddXmlConfig("Data");
      ts.Columns.AddDateTime("WriteTime", true); // Дата последней записи
      //ts.Indices.Add("Пользователь");
      ts.Indexes.Add("Name,Category,ConfigName");

      _DBConnectionHelper.MainDBStruct.Tables.Add(ts);

      #endregion

      _DataVersionHandler = new DBxDataVersionHandler(new Guid("d16e16de-4b22-4170-b5d7-d480f668fa8a"), 1, 1);
      _DataVersionHandler.AddTableStruct(_DBConnectionHelper.MainDBStruct);
    }

    #endregion

    #region Точки подключения

    /// <summary>
    /// Корневой объект системы документов
    /// </summary>
    public DBxRealDocProviderGlobal GlobalDocData { get { return _GlobalDocData; } }
    private DBxRealDocProviderGlobal _GlobalDocData;

    public DBxRealDocProviderSource Source { get { return _Source; } }
    private DBxRealDocProviderSource _Source;

    /// <summary>
    /// Основное подключение к базе данных с полным набором прав (для действий, выполняемых программой)
    /// </summary>
    public DBxEntry MainEntry { get { return _GlobalDocData.MainDBEntry; } }

    #endregion

    public DBxDocProvider CreateDocProvider()
    {
      return new DBxRealDocProvider(Source, 0, false);
    }

    #region Резервное копирование

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
      spl.PhaseText = "Поиск старых копий";

      string[] aFiles = System.IO.Directory.GetFiles(BackupDir.Path, "????????-??????.7z", SearchOption.TopDirectoryOnly);
      for (int i = 0; i < aFiles.Length; i++)
      {
        DateTime dt = System.IO.File.GetLastWriteTime(aFiles[i]);
        TimeSpan ts = DateTime.Today - dt;
        if (ts.TotalDays > 7)
        {
          spl.PhaseText = "Удаление " + aFiles[i];
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
