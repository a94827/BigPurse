using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FreeLibSet.Forms;
using FreeLibSet.Forms.Docs;
using FreeLibSet.Calendar;
using FreeLibSet.Data;
using FreeLibSet.Data.Docs;
using FreeLibSet.Core;
using FreeLibSet.UICore;
using FreeLibSet.Config;
using FreeLibSet.Forms.Data;

namespace App
{
  public partial class PurchaseReportParamForm : EFPReportExtParamsTwoPageForm
  {
    #region Конструктор

    public PurchaseReportParamForm()
    {
      InitializeComponent();

      efpPeriod = new EFPDateRangeBox(FormProvider, edPeriod);


      efpProductDet = new EFPListComboBox(FormProvider, cbProductDet);
    }

    #endregion

    #region Поля

    public EFPDateRangeBox efpPeriod;

    public EFPListComboBox efpProductDet;

    #endregion
  }

  internal class PurchaseReportParams : EFPReportExtParams
  {
    public enum ProducDetLevel { Product, Description, Quantity }

    #region Конструктор

    public PurchaseReportParams()
    {
      Filters = new EFPDBxGridFilters();

      FilterProducts = new RefDocGridFilter(ProgramDBUI.TheUI.DocTypes["Products"], "Product");
      FilterProducts.DisplayName = "Товар";
      FilterProducts.Nullable = false;
      Filters.Add(FilterProducts);

      FilterWallets = new RefDocGridFilter(ProgramDBUI.TheUI.DocTypes["Wallets"], "DocId.WalletCredit");
      FilterWallets.DisplayName = "Кошелек";
      FilterWallets.Nullable = false;
      Filters.Add(FilterWallets);
      BoolValueGridFilter filtWalletsDeposit = new BoolValueGridFilter("Deposit");
      filtWalletsDeposit.DisplayName = "Вклад";
      filtWalletsDeposit.Value = false;
      filtWalletsDeposit.FilterTextFalse = "Нет";
      FilterWallets.DocFilters.Add(filtWalletsDeposit);

      FilterShops = new RefDocGridFilterSet(ProgramDBUI.TheUI.DocTypes["Shops"], "DocId.Shop");
      FilterShops.DisplayName = "Магазин";
      FilterShops.Nullable = false;
      Filters.Add(FilterShops);

      FilterPurposes = new RefDocGridFilterSet(ProgramDBUI.TheUI.DocTypes["Purposes"], "Purpose");
      FilterPurposes.DisplayName = "Назначение";
      FilterPurposes.Nullable = false;
      Filters.Add(FilterPurposes);

      FilterAuxPurposes = new RefDocGridFilter(ProgramDBUI.TheUI.DocTypes["AuxPurposes"], "AuxPurpose");
      FilterAuxPurposes.DisplayName = "Доп. назначение";
      FilterAuxPurposes.Nullable = false;
      Filters.Add(FilterAuxPurposes);
    }

    #endregion

    #region Поля

    #region Общие

    public DateTime? FirstDate;

    public DateTime? LastDate;

    public ProducDetLevel ProductDet;

    #endregion

    #region Фильтры

    public EFPDBxGridFilters Filters;

    public RefDocGridFilter FilterProducts;

    public RefDocGridFilter FilterWallets;

    public RefDocGridFilterSet FilterShops;

    public RefDocGridFilterSet FilterPurposes;

    public RefDocGridFilter FilterAuxPurposes;

    #endregion

    #endregion

    #region Переопределенные методы

    protected override void OnInitTitle()
    {
      base.Title = "Покупки за " + DateRangeFormatter.Default.ToString(FirstDate, LastDate, true);
      Filters.AddFilterInfo(FilterInfo);
    }

    public override EFPReportExtParamsForm CreateForm()
    {
      return new PurchaseReportParamForm();
    }

    public override void WriteFormValues(EFPReportExtParamsForm form, SettingsPart part)
    {
      PurchaseReportParamForm form2 = (PurchaseReportParamForm)form;
      form2.efpPeriod.First.NValue = FirstDate;
      form2.efpPeriod.Last.NValue = LastDate;
      form2.efpProductDet.SelectedIndex = (int)ProductDet;
      form2.FiltersControlProvider.Filters = Filters;
    }

    public override void ReadFormValues(EFPReportExtParamsForm form, SettingsPart part)
    {
      PurchaseReportParamForm form2 = (PurchaseReportParamForm)form;
      FirstDate = form2.efpPeriod.First.NValue;
      LastDate = form2.efpPeriod.Last.NValue;
      ProductDet = (ProducDetLevel)(form2.efpProductDet.SelectedIndex);
    }

    public override void WriteConfig(CfgPart cfg, SettingsPart part)
    {
      cfg.SetNullableDate("FirstDate", FirstDate);
      cfg.SetNullableDate("LastDate", LastDate);
      cfg.SetEnum<ProducDetLevel>("ProductDet", ProductDet);
      Filters.WriteConfig(cfg);
    }

    public override void ReadConfig(CfgPart cfg, SettingsPart part)
    {
      FirstDate = cfg.GetNullableDate("FirstDate");
      LastDate = cfg.GetNullableDate("LastDate");
      ProductDet = cfg.GetEnumDef<ProducDetLevel>("ProductDet", ProducDetLevel.Quantity);
      Filters.ReadConfig(cfg);
    }

    #endregion
  }

  internal class PurchaseReport : EFPReport
  {
    #region Конструктор

    public PurchaseReport()
      : base("PurchaseReport")
    {
      MainImageKey = "PurchaseReport";

      _OpPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      _OpPage.Title = "Операции";
      _OpPage.ImageKey = "Operation";
      _OpPage.InitGrid += new EventHandler(OpPage_InitGrid);
      _OpPage.ShowToolBar = true;
      _OpPage.GridProducer = OpPage_CreateGridProducer();
      Pages.Add(_OpPage);

      _ProdPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      _ProdPage.Title = "Товары";
      _ProdPage.ImageKey = "Product";
      _ProdPage.InitGrid += new EventHandler(ProdPage_InitGrid);
      _ProdPage.ShowToolBar = true;
      Pages.Add(_ProdPage);

      _ShopPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      _ShopPage.Title = "Магазины";
      _ShopPage.ImageKey = "Shop";
      _ShopPage.InitGrid += new EventHandler(ShopPage_InitGrid);
      _ShopPage.ShowToolBar = true;
      Pages.Add(_ShopPage);

      _WalletPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      _WalletPage.Title = "Кошельки";
      _WalletPage.ImageKey = "Wallet";
      _WalletPage.InitGrid += new EventHandler(WalletPage_InitGrid);
      _WalletPage.ShowToolBar = true;
      Pages.Add(_WalletPage);
    }

    #endregion

    #region Запрос параметров

    protected override EFPReportParams CreateParams()
    {
      return new PurchaseReportParams();
    }

    public PurchaseReportParams Params { get { return (PurchaseReportParams)(base.ReportParams); } }

    #endregion

    #region Построение отчета

    protected override void BuildReport()
    {
      #region Заготовка таблицы продуктов

      DataTable prodTable = new DataTable();
      prodTable.Columns.Add("Product", typeof(Int32));
      prodTable.Columns.Add("Product.Name", typeof(string));
      prodTable.Columns.Add("Description", typeof(string));
      prodTable.Columns.Add("Purpose", typeof(Int32));
      prodTable.Columns.Add("Purpose.Name", typeof(string));
      prodTable.Columns.Add("AuxPurpose", typeof(Int32));
      prodTable.Columns.Add("AuxPurpose.Name", typeof(string));
      prodTable.Columns.Add("Quantity1", typeof(float));
      prodTable.Columns.Add("MU1", typeof(Int32));
      prodTable.Columns.Add("MU1.Name", typeof(string));
      prodTable.Columns.Add("Quantity2", typeof(float));
      prodTable.Columns.Add("MU2", typeof(Int32));
      prodTable.Columns.Add("MU2.Name", typeof(string));
      prodTable.Columns.Add("Quantity3", typeof(float));
      prodTable.Columns.Add("MU3", typeof(Int32));
      prodTable.Columns.Add("MU3.Name", typeof(string));
      prodTable.Columns.Add("QuantityText", typeof(string));
      prodTable.Columns.Add("RecordSum", typeof(decimal));
      prodTable.Columns.Add("RecType", typeof(int)); // 0-обычная запись, 1-итог

      object[] prodKeys;
      switch (Params.ProductDet)
      {
        case PurchaseReportParams.ProducDetLevel.Product:
          prodTable.DefaultView.Sort = "Product";
          prodKeys = new object[1];
          break;
        case PurchaseReportParams.ProducDetLevel.Description:
          prodTable.DefaultView.Sort = "Product,Description";
          prodKeys = new object[2];
          break;
        case PurchaseReportParams.ProducDetLevel.Quantity:
          //                              0         1       2   3     4       5      6
          prodTable.DefaultView.Sort = "Product,Description,MU1,MU2,Quantity2,MU3,Quantity3"; // Quantity1 - суммируемое поле
          prodKeys = new object[7]; // для поиска
          break;
        default:
          throw new BugException("ProductDet=" + Params.ProductDet.ToString());
      }

      _ProdTableOps = new Dictionary<DataRow, IdList>();

      #endregion

      #region Заготовка таблицы магазинов

      DataTable shopTable = new DataTable();
      shopTable.Columns.Add("Id", typeof(Int32));
      shopTable.Columns.Add("Shop.Name", typeof(string));
      shopTable.Columns.Add("RecordSum", typeof(decimal));
      shopTable.Columns.Add("RecType", typeof(int)); // 0-обычная запись, 1-итог
      shopTable.DefaultView.Sort = "Id";

      _ShopTableOps = new Dictionary<DataRow, IdList>();

      #endregion

      #region Заготовка таблицы кошельков

      DataTable walletTable = new DataTable();
      walletTable.Columns.Add("Id", typeof(Int32));
      walletTable.Columns.Add("Wallet.Name", typeof(string));
      walletTable.Columns.Add("RecordSum", typeof(decimal));
      walletTable.Columns.Add("RecType", typeof(int)); // 0-обычная запись, 1-итог
      walletTable.DefaultView.Sort = "Id";

      _WalletTableOps = new Dictionary<DataRow, IdList>();

      #endregion

      #region Загрузка данных

      DBxSelectInfo si = new DBxSelectInfo();
      si.TableName = "OperationProducts";
      si.Expressions.Add("Id"); // нужно только для EFPDBxGridView
      si.Expressions.Add("DocId,RecordOrder,Product,Product.Name,Description,Purpose,Purpose.Name,AuxPurpose,AuxPurpose.Name,Quantity1,MU1,MU1.Name,Quantity2,MU2,MU2.Name,Quantity3,MU3,MU3.Name,Formula,RecordSum,Comment");
      si.Expressions.Add("DocId.Date,DocId.OpOrder,DocId.WalletCredit,DocId.WalletCredit.Name,DocId.Shop,DocId.Shop.Name,DocId.Comment");

      List<DBxFilter> sqlFilters = new List<DBxFilter>();
      foreach (DBxCommonFilter filter in Params.Filters)
      {
        if (!filter.IsEmpty)
          sqlFilters.Add(filter.GetSqlFilter());
      }
      sqlFilters.Add(new DateRangeFilter("DocId.Date", Params.FirstDate, Params.LastDate));
      sqlFilters.Add(DBSSubDocType.DeletedFalseFilter);
      sqlFilters.Add(DBSSubDocType.DocIdDeletedFalseFilter);
      sqlFilters.Add(new ValueFilter("DocId.OpType", (int)OperationType.Expense)); // чтобы мусор не пролез
      si.Where = AndFilter.FromList(sqlFilters);

      DataTable table1 = ProgramDBUI.TheUI.DocProvider.FillSelect(si);

      #endregion

      #region Таблица операций для отображения

      DataTable table2 = table1.Clone();
      table2.Columns["Id"].AllowDBNull = true;
      table2.Columns.Add("TotalRow", typeof(bool));
      table2.Columns.Remove("DocId.Comment"); // объединим комментарии

      foreach (DataRow row1 in table1.Rows)
      {
        DataRow row2 = table2.NewRow();
        DataTools.CopyRowValues(row1, row2, true);

        //if (row2["Description"].ToString().Length == 0)
        //  row2["Description"] = row1["Product.Name"]; // чтобы не оставалось пустое поле

        string comment = DataTools.JoinNotEmptyStrings(". ", new string[2]{
          row1["DocId.Comment"].ToString(), row1["Comment"].ToString()});
        DataTools.SetString(row2, "Comment", comment);

        row2["TotalRow"] = false;

        table2.Rows.Add(row2);

        prodKeys[0] = row1["Product"];
        if (Params.ProductDet >= PurchaseReportParams.ProducDetLevel.Description)
          prodKeys[1] = DataTools.GetString(row1, "Description");
        if (Params.ProductDet >= PurchaseReportParams.ProducDetLevel.Quantity)
        {
          prodKeys[2] = row1["MU1"];
          prodKeys[3] = row1["MU2"];
          prodKeys[4] = row1["Quantity2"];
          prodKeys[5] = row1["MU3"];
          prodKeys[6] = row1["Quantity3"];
        }
        DataRow prodRow;
        if (DataTools.FindOrAddDataRow(prodTable.DefaultView, prodKeys, out prodRow))
        {
          prodRow["Product.Name"] = row1["Product.Name"];
          if (Params.ProductDet >= PurchaseReportParams.ProducDetLevel.Quantity)
          {
            prodRow["MU1.Name"] = row1["MU1.Name"];
            prodRow["MU2.Name"] = row1["MU2.Name"];
            prodRow["MU3.Name"] = row1["MU3.Name"];
          }
          _ProdTableOps.Add(prodRow, new IdList());
        }
        if (Params.ProductDet >= PurchaseReportParams.ProducDetLevel.Quantity)
          DataTools.IncSingle(row1, prodRow, "Quantity1");
        DataTools.IncDecimal(row1, prodRow, "RecordSum");
        _ProdTableOps[prodRow].Add(DataTools.GetInt(row1, "DocId"));

        DataRow shopRow;
        if (DataTools.FindOrAddDataRow(shopTable.DefaultView, row1["DocId.Shop"], out shopRow))
        {
          if (DataTools.GetInt(row1, "DocId.Shop") == 0)
            shopRow["Shop.Name"] = "[ Магазин не задан ]";
          else
            shopRow["Shop.Name"] = row1["DocId.Shop.Name"];
          _ShopTableOps.Add(shopRow, new IdList());
        }
        DataTools.IncDecimal(row1, shopRow, "RecordSum");
        _ShopTableOps[shopRow].Add(DataTools.GetInt(row1, "DocId"));

        DataRow walletRow;
        if (DataTools.FindOrAddDataRow(walletTable.DefaultView, row1["DocId.WalletCredit"], out walletRow))
        {
          walletRow["Wallet.Name"] = row1["DocId.WalletCredit.Name"];
          _WalletTableOps.Add(walletRow, new IdList());
        }
        DataTools.IncDecimal(row1, walletRow, "RecordSum");
        _WalletTableOps[walletRow].Add(DataTools.GetInt(row1, "DocId"));
      }

      DataRow opTotalRow = table2.NewRow();
      opTotalRow["TotalRow"] = true;
      opTotalRow["Product.Name"] = "Итого";
      DataTools.SumDecimal(opTotalRow, "RecordSum");
      table2.Rows.Add(opTotalRow);

      table2.DefaultView.Sort = "TotalRow,DocId.Date,DocId.OpOrder,RecordOrder";

      _OpPage.DataSource = table2.DefaultView;

      #endregion

      #region Таблица продуктов

      if (Params.ProductDet >= PurchaseReportParams.ProducDetLevel.Quantity)
      {
        foreach (DataRow row in prodTable.Rows)
        {
          row["QuantityText"] = EditOperationProduct.GetQuantityText(
            DataTools.GetSingle(row, "Quantity1"), DataTools.GetString(row, "MU1.Name"),
            DataTools.GetSingle(row, "Quantity2"), DataTools.GetString(row, "MU2.Name"),
            DataTools.GetSingle(row, "Quantity3"), DataTools.GetString(row, "MU3.Name"));
        }
      }

      DataRow prodTotalRow = prodTable.NewRow();
      prodTotalRow["Product.Name"] = "Итого";
      prodTotalRow["RecType"] = 1;
      DataTools.SumDecimal(prodTotalRow, "RecordSum");
      prodTable.Rows.Add(prodTotalRow);

      switch (Params.ProductDet)
      {
        case PurchaseReportParams.ProducDetLevel.Product:
          prodTable.DefaultView.Sort = "RecType,Product.Name,Product";
          break;
        case PurchaseReportParams.ProducDetLevel.Description:
          prodTable.DefaultView.Sort = "RecType,Product.Name,Product,Description";
          break;
        case PurchaseReportParams.ProducDetLevel.Quantity:
          prodTable.DefaultView.Sort = "RecType,Product.Name,Product,Description,QuantityText";
          break;
      }
      _ProdPage.DataSource = prodTable.DefaultView;

      #endregion

      #region Таблица магазинов

      DataRow shopTotalRow = shopTable.NewRow();
      shopTotalRow["Shop.Name"] = "Итого";
      shopTotalRow["RecType"] = 1;
      DataTools.SumDecimal(shopTotalRow, "RecordSum");
      shopTable.Rows.Add(shopTotalRow);
      shopTable.DefaultView.Sort = "RecType,Shop.Name,Id";
      _ShopPage.DataSource = shopTable.DefaultView;

      #endregion

      #region Таблица кошельков

      DataRow walletTotalRow = walletTable.NewRow();
      walletTotalRow["Wallet.Name"] = "Итого";
      walletTotalRow["RecType"] = 1;
      DataTools.SumDecimal(walletTotalRow, "RecordSum");
      walletTable.Rows.Add(walletTotalRow);
      walletTable.DefaultView.Sort = "RecType,Wallet.Name,Id";
      _WalletPage.DataSource = walletTable.DefaultView;

      #endregion
    }

    #endregion

    #region Страница "Операции"

    EFPReportDBxGridPage _OpPage;

    private EFPGridProducer OpPage_CreateGridProducer()
    {
      EFPDBxGridProducer producer = new EFPDBxGridProducer(ProgramDBUI.TheUI);
      producer.Columns.AddDate("DocId.Date", "Дата");
      producer.Columns.AddText("DocId.WalletCredit.Name", "Кошелек", 15, 10);
      producer.Columns.AddText("DocId.Shop.Name", "Магазин", 25, 10);

      producer.Columns.AddText("Product.Name", "Товар, услуга", 20, 5);
      producer.Columns.AddText("Description", "Описание", 20, 5);
      producer.Columns.AddText("Purpose.Name", "Назначение", 20, 5);
      producer.Columns.AddText("AuxPurpose.Name", "Доп. назначение", 20, 5);

      producer.Columns.AddUserText("QuantityText", "Quantity1,MU1.Name,Quantity2,MU2.Name,Quantity3,MU3.Name",
        new EFPGridProducerValueNeededEventHandler(EditOperationProduct.QuantityTextColumnValueNeeded),
        "Количество", 20, 10);

      producer.Columns.AddText("Quantity1", "Кол-во 1", 5, 2);
      producer.Columns.LastAdded.Format = "0.###";
      producer.Columns.LastAdded.SizeGroup = "Quantity";
      producer.Columns.AddText("MU1.Name", "Ед. изм. 1", 5, 2);
      producer.Columns.LastAdded.SizeGroup = "MUName";

      producer.Columns.AddText("Quantity2", "Кол-во 2", 5, 2);
      producer.Columns.LastAdded.Format = "0.###";
      producer.Columns.LastAdded.SizeGroup = "Quantity";
      producer.Columns.AddText("MU2.Name", "Ед. изм. 2", 5, 2);
      producer.Columns.LastAdded.SizeGroup = "MUName";

      producer.Columns.AddText("Quantity3", "Кол-во 3", 5, 2);
      producer.Columns.LastAdded.Format = "0.###";
      producer.Columns.LastAdded.SizeGroup = "Quantity";
      producer.Columns.AddText("MU3.Name", "Ед. изм. 3", 5, 2);
      producer.Columns.LastAdded.SizeGroup = "MUName";

      producer.Columns.AddText("Formula", "Формула", 15, 5);
      producer.Columns.AddMoney("RecordSum", "Сумма");
      producer.Columns.LastAdded.Format = Tools.MoneyFormat;


      producer.Columns.AddText("Comment", "Комментарий", 30, 10);

      producer.NewDefaultConfig(false);
      producer.DefaultConfig.Columns.Add("DocId.Date");
      producer.DefaultConfig.Columns.AddFill("Product.Name", 35);
      producer.DefaultConfig.Columns.AddFill("Description", 35);
      producer.DefaultConfig.Columns.AddFill("QuantityText", 30);
      //gridProducer.DefaultConfig.Columns.Add("Quantity1");
      //gridProducer.DefaultConfig.Columns.Add("MU1.Name");
      //gridProducer.DefaultConfig.Columns.Add("Quantity2");
      //gridProducer.DefaultConfig.Columns.Add("MU2.Name");
      //gridProducer.DefaultConfig.Columns.Add("Quantity3");
      //gridProducer.DefaultConfig.Columns.Add("MU3.Name");
      producer.DefaultConfig.Columns.Add("RecordSum");

      return producer;
    }

    void OpPage_InitGrid(object sender, EventArgs args)
    {
      _OpPage.ControlProvider.ConfigSectionName = "PurchaseReportOperations";
      _OpPage.ControlProvider.UseRowImages = true;
      _OpPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(OpPage_GetRowAttributes);
      _OpPage.ControlProvider.ShowRowCountInTopLeftCell = true;
      _OpPage.ControlProvider.ShowErrorCountInTopLeftCell = true;
      //OpPage.ControlProvider.DisableOrdering();

      _OpPage.ControlProvider.ReadOnly = false;
      _OpPage.ControlProvider.Control.ReadOnly = true;
      _OpPage.ControlProvider.CanInsert = false;
      _OpPage.ControlProvider.CanDelete = false;
      _OpPage.ControlProvider.EditData += new EventHandler(OpPage_EditData);

      _OpPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(MainPage_GetDocSel);
    }

    void OpPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
    {
      if (args.DataRow == null)
        return;

      if (DataTools.GetBool(args.DataRow, "TotalRow"))
        args.ColorType = UIDataViewColorType.TotalRow;
      else
      {
        Int32 productId = DataTools.GetInt(args.DataRow, "Product");
        ProductBuffer.ProductData pd = ProductBuffer.GetProductData(productId);

        if (!pd.IsLeaf)
          args.AddRowError("Задана группа товаров", "Product.Name");

        #region Описание

        string description = DataTools.GetString(args.DataRow, "Description");

        if (String.IsNullOrEmpty(description))
        {
          switch (pd.DescriptionPresence)
          {
            case PresenceType.Required:
              args.AddRowError("Описание должно быть заполнено", "Description");
              break;
            case PresenceType.WarningIfNone:
              args.AddRowInformation("Описание обычно должно быть заполнено", "Description");
              break;
          }
        }
        else if (pd.DescriptionPresence == PresenceType.Disabled)
        {
          args.AddRowError("Описание не должно заполняться", "Description");
        }

        #endregion

        #region Назначение

        Int32 purposeId = DataTools.GetInt(args.DataRow, "Purpose");

        if (purposeId == 0)
        {
          switch (pd.PurposePresence)
          {
            case PresenceType.Required:
              args.AddRowError("Назначение должно быть выбрано", "Purpose.Name");
              break;
            case PresenceType.WarningIfNone:
              args.AddRowInformation("Назначение обычно должно быть выбрано", "Purpose.Name");
              break;
          }
        }
        else if (pd.PurposePresence == PresenceType.Disabled)
        {
          args.AddRowError("Назначение не должно заполняться", "Purpose.Name");
        }

        #endregion

        #region Количество и единица измерения

        float q1 = DataTools.GetSingle(args.DataRow, "Quantity1");
        float q2 = DataTools.GetSingle(args.DataRow, "Quantity2");
        float q3 = DataTools.GetSingle(args.DataRow, "Quantity3");
        Int32 muId1 = DataTools.GetInt(args.DataRow, "MU1");
        Int32 muId2 = DataTools.GetInt(args.DataRow, "MU2");
        Int32 muId3 = DataTools.GetInt(args.DataRow, "MU3");

        if (q1 == 0f && q2 != 0f)
          args.AddRowError("Заполнена вторая единица измерения без первой", "Quantity2");
        if (q2 == 0f && q3 != 0f)
          args.AddRowError("Заполнена третья единица измерения без второй", "Quantity3");
        CheckQuantityAndMUPair(args, q1, muId1, "1");
        CheckQuantityAndMUPair(args, q2, muId2, "2");
        CheckQuantityAndMUPair(args, q3, muId3, "3");
        CheckMUPair(args, muId1, muId2, "1", "2");
        CheckMUPair(args, muId2, muId3, "2", "3");
        CheckMUPair(args, muId1, muId3, "1", "3");

        bool hasQ = (q1 != 0f) || (q2 != 0f) || (q3 != 0f);
        if (hasQ)
        {
          if (pd.QuantityPresence == PresenceType.Disabled)
            args.AddRowError("Количество не должно заполняться", "Quantity1,MU1.Name");
          else if (pd.MUSets.Length > 0)
          {
            // Проверяем попадание единиц измерения
            bool found = false;
            for (int i = 0; i < pd.MUSets.Length; i++)
            {
              if (pd.MUSets[i].MUId1 == muId1 && pd.MUSets[i].MUId2 == muId2 && pd.MUSets[i].MUId3 == muId3)
              {
                found = true;
                break;
              }
            }

            string sColNames = "MU1.Name";
            if (muId2 != 0)
              sColNames += ",MU2.Name";
            if (muId3 != 0)
              sColNames += ",MU3.Name";

            if (!found)
              args.AddRowWarning("Неизвестная комбинация единиц измерения", sColNames);
          }
        }
        else
        {
          switch (pd.QuantityPresence)
          {
            case PresenceType.Required:
              args.AddRowError("Количество должно быть заполнено", "Quantity1,MU1.Name");
              break;
            case PresenceType.WarningIfNone:
              args.AddRowWarning("Количество обычно должно быть заполнено", "Quantity1,MU1.Name");
              break;
          }
        }

        #endregion
      }
    }

    private static void CheckQuantityAndMUPair(EFPDataGridViewRowAttributesEventArgs args, float q, int muId, string suffix)
    {
      if ((q == 0f) != (muId == 0))
      {
        string msg;
        if (muId == 0)
          msg = "Задано количество без единицы измерения";
        else
          msg = "Задана единица измерения без количества";
        args.AddRowError("Количество №" + suffix + ". " + msg, "Quantity" + suffix + ",MU" + suffix + ".Name");
      }
    }

    private static void CheckMUPair(EFPDataGridViewRowAttributesEventArgs args, Int32 muId1, Int32 muId2, string suffix1, string suffix2)
    {
      if (muId1 == 0 || muId2 == 0)
        return;
      if (muId1 != muId2)
        return;
      string sColNames = "MU" + suffix1 + ".Name,MU" + suffix2 + ".Name";
      args.AddRowError("Две одинаковые единицы измерения №" + suffix1 + " и №" + suffix2, sColNames);
    }

    void OpPage_EditData(object sender, EventArgs args)
    {
      Int32[] docIds = DataTools.GetIdsFromColumn(_OpPage.ControlProvider.SelectedDataRows, "DocId");
      if (docIds.Length == 0)
        EFPApp.ShowTempMessage("Нет выбранных операций");
      ProgramDBUI.TheUI.DocTypes["Operations"].PerformEditing(docIds, _OpPage.ControlProvider.State, false);
    }

    void MainPage_GetDocSel(object sender, EFPDBxGridViewDocSelEventArgs args)
    {
      args.AddFromColumn("Operations", "DocId");
      args.AddFromColumn("Products", "Product");
      args.AddFromColumn("Shops", "DocId.Shop");
      args.AddFromColumn("Wallets", "DocId.WalletCredit");
    }

    #endregion

    #region Страница "Продукты"

    EFPReportDBxGridPage _ProdPage;

    /// <summary>
    /// Ключ - строка таблицы операций
    /// Значение - идентификаторы операций
    /// </summary>
    Dictionary<DataRow, IdList> _ProdTableOps;

    void ProdPage_InitGrid(object sender, EventArgs args)
    {
      // Нельзя использовать GridProducer для таблицы, так как столбцы товара, описания и количества являются обязательными,
      // без них итоги будут бессмысленными

      _ProdPage.ControlProvider.Control.AutoGenerateColumns = false;
      _ProdPage.ControlProvider.Columns.AddTextFill("Product.Name", true, "Товар, услуга", 35, 5);
      if (Params.ProductDet >= PurchaseReportParams.ProducDetLevel.Description)
        _ProdPage.ControlProvider.Columns.AddTextFill("Description", true, "Описание", 35, 5);

      if (Params.ProductDet >= PurchaseReportParams.ProducDetLevel.Quantity)
        _ProdPage.ControlProvider.Columns.AddTextFill("QuantityText", true, "Количество", 30, 10);
      _ProdPage.ControlProvider.Columns.AddText("RecordSum", true, "Сумма");
      _ProdPage.ControlProvider.Columns.LastAdded.TextAlign = HorizontalAlignment.Right;
      _ProdPage.ControlProvider.Columns.LastAdded.GridColumn.DefaultCellStyle.Format = Tools.MoneyFormat;
      _ProdPage.ControlProvider.DisableOrdering();
      _ProdPage.ControlProvider.UseRowImages = true;
      _ProdPage.ControlProvider.ShowErrorCountInTopLeftCell = true;
      _ProdPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(ProdPage_GetRowAttributes);

      _ProdPage.ControlProvider.Control.ReadOnly = true;
      _ProdPage.ControlProvider.ReadOnly = false;
      _ProdPage.ControlProvider.CanInsert = false;
      _ProdPage.ControlProvider.CanDelete = false;
      _ProdPage.ControlProvider.EditData += new EventHandler(ProdPage_EditData);
      _ProdPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(ProdPage_GetDocSel);
    }

    void ProdPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
    {
      if (DataTools.GetInt(args.DataRow, "RecType") == 1)
        args.ColorType = UIDataViewColorType.TotalRow;
      else
      {
        Int32 productId = DataTools.GetInt(args.DataRow, "Product");
        ProductBuffer.ProductData pd = ProductBuffer.GetProductData(productId);

        if (!pd.IsLeaf)
          args.AddRowError("Задана группа товаров", "Product.Name");
      }
    }

    void ProdPage_EditData(object sender, EventArgs args)
    {
      DataRow[] rows = _ProdPage.ControlProvider.SelectedDataRows;
      IdList allOps = new IdList();
      for (int i = 0; i < rows.Length; i++)
      {
        IdList lst;
        if (_ProdTableOps.TryGetValue(rows[i], out lst))
          allOps.Add(lst);
      }

      switch (allOps.Count)
      {
        case 0:
          EFPApp.ShowTempMessage("Нет выбранных записей. Итоговую строку выбирать нельзя");
          break;
        case 1:
          ProgramDBUI.TheUI.DocTypes["Operations"].PerformEditing(allOps.SingleId, false);
          break;
        default:
          DBxDocSelection docSel = new DBxDocSelection(ProgramDBUI.TheUI.DocProvider.DBIdentity, "Operations", allOps.ToArray());
          ProgramDBUI.TheUI.ShowDocSel(docSel);
          break;
      }
    }

    void ProdPage_GetDocSel(object sender, EFPDBxGridViewDocSelEventArgs args)
    {
      IdList allOps = new IdList();
      for (int i = 0; i < args.DataRows.Length; i++)
      {
        IdList lst;
        if (_ProdTableOps.TryGetValue(args.DataRows[i], out lst))
          allOps.Add(lst);
      }
      args.DocSel.Add("Operations", allOps);
      args.AddFromColumn("Products", "Product");
    }

    #endregion

    #region Страница "Магазины"

    EFPReportDBxGridPage _ShopPage;

    /// <summary>
    /// Ключ - строка таблицы операций
    /// Значение - идентификаторы операций
    /// </summary>
    Dictionary<DataRow, IdList> _ShopTableOps;

    void ShopPage_InitGrid(object sender, EventArgs args)
    {
      _ShopPage.ControlProvider.Control.AutoGenerateColumns = false;
      _ShopPage.ControlProvider.Columns.AddTextFill("Shop.Name", true, "Магазин", 100, 20);
      _ShopPage.ControlProvider.Columns.AddText("RecordSum", true, "Сумма");
      _ShopPage.ControlProvider.Columns.LastAdded.TextAlign = HorizontalAlignment.Right;
      _ShopPage.ControlProvider.Columns.LastAdded.GridColumn.DefaultCellStyle.Format = Tools.MoneyFormat;
      _ShopPage.ControlProvider.DisableOrdering();
      _ShopPage.ControlProvider.UseRowImages = true;
      _ShopPage.ControlProvider.ShowErrorCountInTopLeftCell = true;
      _ShopPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(ShopPage_GetRowAttributes);

      _ShopPage.ControlProvider.Control.ReadOnly = true;
      _ShopPage.ControlProvider.ReadOnly = false;
      _ShopPage.ControlProvider.CanInsert = false;
      _ShopPage.ControlProvider.CanDelete = false;
      _ShopPage.ControlProvider.EditData += new EventHandler(ShopPage_EditData);
      _ShopPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(ShopPage_GetDocSel);
    }

    void ShopPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
    {
      if (DataTools.GetInt(args.DataRow, "RecType") == 1)
        args.ColorType = UIDataViewColorType.TotalRow;
    }

    void ShopPage_EditData(object sender, EventArgs args)
    {
      DataRow[] rows = _ShopPage.ControlProvider.SelectedDataRows;
      IdList allOps = new IdList();
      for (int i = 0; i < rows.Length; i++)
      {
        IdList lst;
        if (_ShopTableOps.TryGetValue(rows[i], out lst))
          allOps.Add(lst);
      }

      switch (allOps.Count)
      {
        case 0:
          EFPApp.ShowTempMessage("Нет выбранных записей. Итоговую строку выбирать нельзя");
          break;
        case 1:
          ProgramDBUI.TheUI.DocTypes["Operations"].PerformEditing(allOps.SingleId, false);
          break;
        default:
          DBxDocSelection docSel = new DBxDocSelection(ProgramDBUI.TheUI.DocProvider.DBIdentity, "Operations", allOps.ToArray());
          ProgramDBUI.TheUI.ShowDocSel(docSel);
          break;
      }
    }

    void ShopPage_GetDocSel(object sender, EFPDBxGridViewDocSelEventArgs args)
    {
      IdList allOps = new IdList();
      for (int i = 0; i < args.DataRows.Length; i++)
      {
        IdList lst;
        if (_ShopTableOps.TryGetValue(args.DataRows[i], out lst))
          allOps.Add(lst);
      }
      args.DocSel.Add("Operations", allOps);
      args.AddFromColumn("Shops", "Id");
    }

    #endregion

    #region Страница "Кошельки"

    EFPReportDBxGridPage _WalletPage;

    /// <summary>
    /// Ключ - строка таблицы операций
    /// Значение - идентификаторы операций
    /// </summary>
    Dictionary<DataRow, IdList> _WalletTableOps;

    void WalletPage_InitGrid(object sender, EventArgs args)
    {
      _WalletPage.ControlProvider.Control.AutoGenerateColumns = false;
      _WalletPage.ControlProvider.Columns.AddTextFill("Wallet.Name", true, "Кошелек", 100, 20);
      _WalletPage.ControlProvider.Columns.AddText("RecordSum", true, "Сумма");
      _WalletPage.ControlProvider.Columns.LastAdded.TextAlign = HorizontalAlignment.Right;
      _WalletPage.ControlProvider.Columns.LastAdded.GridColumn.DefaultCellStyle.Format = Tools.MoneyFormat;
      _WalletPage.ControlProvider.DisableOrdering();
      _WalletPage.ControlProvider.UseRowImages = true;
      _WalletPage.ControlProvider.ShowErrorCountInTopLeftCell = true;
      _WalletPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(WalletPage_GetRowAttributes);

      _WalletPage.ControlProvider.Control.ReadOnly = true;
      _WalletPage.ControlProvider.ReadOnly = false;
      _WalletPage.ControlProvider.CanInsert = false;
      _WalletPage.ControlProvider.CanDelete = false;
      _WalletPage.ControlProvider.EditData += new EventHandler(WalletPage_EditData);
      _WalletPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(WalletPage_GetDocSel);
    }

    void WalletPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
    {
      if (DataTools.GetInt(args.DataRow, "RecType") == 1)
        args.ColorType = UIDataViewColorType.TotalRow;
    }

    void WalletPage_EditData(object sender, EventArgs args)
    {
      DataRow[] rows = _WalletPage.ControlProvider.SelectedDataRows;
      IdList allOps = new IdList();
      for (int i = 0; i < rows.Length; i++)
      {
        IdList lst;
        if (_WalletTableOps.TryGetValue(rows[i], out lst))
          allOps.Add(lst);
      }

      switch (allOps.Count)
      {
        case 0:
          EFPApp.ShowTempMessage("Нет выбранных записей. Итоговую строку выбирать нельзя");
          break;
        case 1:
          ProgramDBUI.TheUI.DocTypes["Operations"].PerformEditing(allOps.SingleId, false);
          break;
        default:
          DBxDocSelection docSel = new DBxDocSelection(ProgramDBUI.TheUI.DocProvider.DBIdentity, "Operations", allOps.ToArray());
          ProgramDBUI.TheUI.ShowDocSel(docSel);
          break;
      }
    }

    void WalletPage_GetDocSel(object sender, EFPDBxGridViewDocSelEventArgs args)
    {
      IdList allOps = new IdList();
      for (int i = 0; i < args.DataRows.Length; i++)
      {
        IdList lst;
        if (_WalletTableOps.TryGetValue(args.DataRows[i], out lst))
          allOps.Add(lst);
      }
      args.DocSel.Add("Operations", allOps);
      args.AddFromColumn("Wallets", "Id");
    }

    #endregion
  }
}