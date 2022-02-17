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

namespace App
{
  public partial class PurchaseReportParamForm : EFPReportExtParamsForm
  {
    #region Конструктор

    public PurchaseReportParamForm()
    {
      InitializeComponent();

      efpPeriod = new EFPDateRangeBox(FormProvider, edPeriod);

      efpProducts = new EFPMultiDocComboBox(FormProvider, cbProducts, ProgramDBUI.TheUI.DocTypes["Products"]);
      efpProducts.SelectionMode = DocSelectionMode.MultiCheckBoxes;
      efpProducts.CanBeEmpty = true;
      efpProducts.EmptyText = "[ Все расходы ]";
      efpProducts.MaxTextItemCount = 5;

      efpWallets = new EFPMultiDocComboBox(FormProvider, cbWallets, ProgramDBUI.TheUI.DocTypes["Wallets"]);
      efpWallets.SelectionMode = DocSelectionMode.MultiCheckBoxes;
      efpWallets.CanBeEmpty = true;
      efpWallets.EmptyText = "[ Все кошельки ]";
      efpWallets.MaxTextItemCount = 3;

      efpShops = new EFPMultiDocComboBox(FormProvider, cbShops, ProgramDBUI.TheUI.DocTypes["Shops"]);
      efpShops.SelectionMode = DocSelectionMode.MultiCheckBoxes;
      efpShops.CanBeEmpty = true;
      efpShops.EmptyText = "[ Все магазины ]";

      efpPurposes = new EFPMultiDocComboBox(FormProvider, cbPurposes, ProgramDBUI.TheUI.DocTypes["Purposes"]);
      efpPurposes.SelectionMode = DocSelectionMode.MultiCheckBoxes;
      efpPurposes.CanBeEmpty = true;
      efpPurposes.EmptyText = "[ Любое назначение ]";
      efpPurposes.MaxTextItemCount = 5;

      efpProductDet = new EFPListComboBox(FormProvider, cbProductDet);
    }

    #endregion

    #region Поля

    public EFPDateRangeBox efpPeriod;

    public EFPMultiDocComboBox efpProducts;

    public EFPMultiDocComboBox efpWallets;

    public EFPMultiDocComboBox efpShops;

    public EFPMultiDocComboBox efpPurposes;

    public EFPListComboBox efpProductDet;

    #endregion
  }

  internal class PurchaseReportParams : EFPReportExtParams
  {
    public enum ProducDetLevel { Product, Description, Quantity }

    #region Поля

    public DateTime? FirstDate;

    public DateTime? LastDate;

    public Int32[] ProductIds;

    public Int32[] WalletIds;

    public Int32[] ShopIds;

    public Int32[] PurposeIds;

    public ProducDetLevel ProductDet;

    #endregion

    #region Переопределенные методы

    protected override void OnInitTitle()
    {
      base.Title = "Покупки за " + DateRangeFormatter.Default.ToString(FirstDate, LastDate, true);

      if (ProductIds != null && ProductIds.Length > 0)
        base.FilterInfo.Add("Товары", String.Join(", ", ProgramDBUI.TheUI.DocProvider.GetTextValues("Products", ProductIds)));
      if (WalletIds != null && WalletIds.Length > 0)
        base.FilterInfo.Add("Кошельки", String.Join(", ", ProgramDBUI.TheUI.DocProvider.GetTextValues("Wallets", WalletIds)));
      if (ShopIds != null && ShopIds.Length > 0)
        base.FilterInfo.Add("Магазины", String.Join(", ", ProgramDBUI.TheUI.DocProvider.GetTextValues("Shops", ShopIds)));
      if (PurposeIds != null && PurposeIds.Length > 0)
        base.FilterInfo.Add("Назначение", String.Join(", ", ProgramDBUI.TheUI.DocProvider.GetTextValues("Purposes", PurposeIds)));
    }

    public override EFPReportExtParamsForm CreateForm()
    {
      return new PurchaseReportParamForm();
    }

    public override void WriteFormValues(EFPReportExtParamsForm Form, EFPReportExtParamsPart Part)
    {
      PurchaseReportParamForm Form2 = (PurchaseReportParamForm)Form;
      Form2.efpPeriod.First.NValue = FirstDate;
      Form2.efpPeriod.Last.NValue = LastDate;
      Form2.efpProducts.DocIds = ProductIds;
      Form2.efpWallets.DocIds = WalletIds;
      Form2.efpShops.DocIds = ShopIds;
      Form2.efpPurposes.DocIds = PurposeIds;
      Form2.efpProductDet.SelectedIndex = (int)ProductDet;
    }

    public override void ReadFormValues(EFPReportExtParamsForm Form, EFPReportExtParamsPart Part)
    {
      PurchaseReportParamForm Form2 = (PurchaseReportParamForm)Form;
      FirstDate = Form2.efpPeriod.First.NValue;
      LastDate = Form2.efpPeriod.Last.NValue;
      ProductIds = Form2.efpProducts.DocIds;
      WalletIds = Form2.efpWallets.DocIds;
      ShopIds = Form2.efpShops.DocIds;
      PurposeIds = Form2.efpPurposes.DocIds;
      ProductDet = (ProducDetLevel)(Form2.efpProductDet.SelectedIndex);
    }

    public override void WriteConfig(FreeLibSet.Config.CfgPart Config, EFPReportExtParamsPart Part)
    {
      Config.SetNullableDate("FirstDate", FirstDate);
      Config.SetNullableDate("LastDate", LastDate);
      Config.SetIntCommaString("Products", ProductIds);
      Config.SetIntCommaString("Wallets", WalletIds);
      Config.SetIntCommaString("Shops", ShopIds);
      Config.SetIntCommaString("Purposes", PurposeIds);
      Config.SetEnum<ProducDetLevel>("ProductDet", ProductDet);
    }

    public override void ReadConfig(FreeLibSet.Config.CfgPart Config, EFPReportExtParamsPart Part)
    {
      FirstDate = Config.GetNullableDate("FirstDate");
      LastDate = Config.GetNullableDate("LastDate");
      ProductIds = Config.GetIntCommaString("Products");
      WalletIds = Config.GetIntCommaString("Wallets");
      ShopIds = Config.GetIntCommaString("Shops");
      PurposeIds = Config.GetIntCommaString("Purposes");
      ProductDet = Config.GetEnumDef<ProducDetLevel>("ProductDet", ProducDetLevel.Quantity);
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

      OpPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      OpPage.Title = "Операции";
      OpPage.ImageKey = "Operation";
      OpPage.InitGrid += new EventHandler(OpPage_InitGrid);
      OpPage.ShowToolBar = true;
      OpPage.GridProducer = OpPage_CreateGridProducer();
      Pages.Add(OpPage);

      ProdPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      ProdPage.Title = "Товары";
      ProdPage.ImageKey = "Product";
      ProdPage.InitGrid += new EventHandler(ProdPage_InitGrid);
      ProdPage.ShowToolBar = true;
      Pages.Add(ProdPage);

      ShopPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      ShopPage.Title = "Магазины";
      ShopPage.ImageKey = "Shop";
      ShopPage.InitGrid += new EventHandler(ShopPage_InitGrid);
      ShopPage.ShowToolBar = true;
      Pages.Add(ShopPage);

      WalletPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      WalletPage.Title = "Кошельки";
      WalletPage.ImageKey = "Wallet";
      WalletPage.InitGrid += new EventHandler(WalletPage_InitGrid);
      WalletPage.ShowToolBar = true;
      Pages.Add(WalletPage);
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
      #region Развернутый список идентификаторов видов товаров

      IdList productIds2 = null;
      if (Params.ProductIds != null && Params.ProductIds.Length > 0)
      {
        productIds2 = new IdList();
        for (int i = 0; i < Params.ProductIds.Length; i++)
          AddProductIds(productIds2, Params.ProductIds[i]); // рекурсивная процедура
      }

      #endregion

      #region Заготовка таблицы продуктов

      DataTable prodTable = new DataTable();
      prodTable.Columns.Add("Product", typeof(Int32));
      prodTable.Columns.Add("Product.Name", typeof(string));
      prodTable.Columns.Add("Description", typeof(string));
      prodTable.Columns.Add("Purpose", typeof(Int32));
      prodTable.Columns.Add("Purpose.Name", typeof(string));
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

      ProdTableOps = new Dictionary<DataRow, IdList>();

      #endregion

      #region Заготовка таблицы магазинов

      DataTable shopTable = new DataTable();
      shopTable.Columns.Add("Id", typeof(Int32));
      shopTable.Columns.Add("Shop.Name", typeof(string));
      shopTable.Columns.Add("RecordSum", typeof(decimal));
      shopTable.Columns.Add("RecType", typeof(int)); // 0-обычная запись, 1-итог
      shopTable.DefaultView.Sort = "Id";

      ShopTableOps = new Dictionary<DataRow, IdList>();

      #endregion

      #region Заготовка таблицы кошельков

      DataTable walletTable = new DataTable();
      walletTable.Columns.Add("Id", typeof(Int32));
      walletTable.Columns.Add("Wallet.Name", typeof(string));
      walletTable.Columns.Add("RecordSum", typeof(decimal));
      walletTable.Columns.Add("RecType", typeof(int)); // 0-обычная запись, 1-итог
      walletTable.DefaultView.Sort = "Id";

      WalletTableOps = new Dictionary<DataRow, IdList>();

      #endregion

      #region Загрузка данных

      DBxSelectInfo si = new DBxSelectInfo();
      si.TableName = "OperationProducts";
      si.Expressions.Add("Id"); // нужно только для EFPDBxGridView
      si.Expressions.Add("DocId,RecordOrder,Product,Product.Name,Description,Purpose,Purpose.Name,Quantity1,MU1,MU1.Name,Quantity2,MU2,MU2.Name,Quantity3,MU3,MU3.Name,Formula,RecordSum,Comment");
      si.Expressions.Add("DocId.Date,DocId.OpOrder,DocId.WalletCredit,DocId.WalletCredit.Name,DocId.Shop,DocId.Shop.Name,DocId.Comment");

      List<DBxFilter> filters = new List<DBxFilter>();
      if (productIds2 != null)
        filters.Add(new IdsFilter("Product", productIds2));
      filters.Add(new DateRangeFilter("DocId.Date", Params.FirstDate, Params.LastDate));
      if (Params.WalletIds != null && Params.WalletIds.Length > 0)
        filters.Add(new IdsFilter("DocId.WalletCredit", Params.WalletIds));
      if (Params.ShopIds != null && Params.ShopIds.Length > 0)
        filters.Add(new IdsFilter("DocId.Shop", Params.ShopIds));
      if (Params.PurposeIds != null && Params.PurposeIds.Length > 0)
        filters.Add(new IdsFilter("Purpose", Params.PurposeIds));
      filters.Add(DBSSubDocType.DeletedFalseFilter);
      filters.Add(DBSSubDocType.DocIdDeletedFalseFilter);
      filters.Add(new ValueFilter("DocId.OpType", (int)OperationType.Expense)); // чтобы мусор не пролез
      si.Where = AndFilter.FromList(filters);

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
          ProdTableOps.Add(prodRow, new IdList());
        }
        if (Params.ProductDet >= PurchaseReportParams.ProducDetLevel.Quantity)
          DataTools.IncSingle(row1, prodRow, "Quantity1");
        DataTools.IncDecimal(row1, prodRow, "RecordSum");
        ProdTableOps[prodRow].Add(DataTools.GetInt(row1, "DocId"));

        DataRow shopRow;
        if (DataTools.FindOrAddDataRow(shopTable.DefaultView, row1["DocId.Shop"], out shopRow))
        {
          if (DataTools.GetInt(row1, "DocId.Shop") == 0)
            shopRow["Shop.Name"] = "[ Магазин не задан ]";
          else
            shopRow["Shop.Name"] = row1["DocId.Shop.Name"];
          ShopTableOps.Add(shopRow, new IdList());
        }
        DataTools.IncDecimal(row1, shopRow, "RecordSum");
        ShopTableOps[shopRow].Add(DataTools.GetInt(row1, "DocId"));

        DataRow walletRow;
        if (DataTools.FindOrAddDataRow(walletTable.DefaultView, row1["DocId.WalletCredit"], out walletRow))
        {
          walletRow["Wallet.Name"] = row1["DocId.WalletCredit.Name"];
          WalletTableOps.Add(walletRow, new IdList());
        }
        DataTools.IncDecimal(row1, walletRow, "RecordSum");
        WalletTableOps[walletRow].Add(DataTools.GetInt(row1, "DocId"));
      }

      DataRow opTotalRow = table2.NewRow();
      opTotalRow["TotalRow"] = true;
      opTotalRow["Product.Name"] = "Итого";
      DataTools.SumDecimal(opTotalRow, "RecordSum");
      table2.Rows.Add(opTotalRow);

      table2.DefaultView.Sort = "TotalRow,DocId.Date,DocId.OpOrder,RecordOrder";

      OpPage.DataSource = table2.DefaultView;

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
      ProdPage.DataSource = prodTable.DefaultView;

      #endregion

      #region Таблица магазинов

      DataRow shopTotalRow = shopTable.NewRow();
      shopTotalRow["Shop.Name"] = "Итого";
      shopTotalRow["RecType"] = 1;
      DataTools.SumDecimal(shopTotalRow, "RecordSum");
      shopTable.Rows.Add(shopTotalRow);
      shopTable.DefaultView.Sort = "RecType,Shop.Name,Id";
      ShopPage.DataSource = shopTable.DefaultView;

      #endregion

      #region Таблица кошельков

      DataRow walletTotalRow = walletTable.NewRow();
      walletTotalRow["Wallet.Name"] = "Итого";
      walletTotalRow["RecType"] = 1;
      DataTools.SumDecimal(walletTotalRow, "RecordSum");
      walletTable.Rows.Add(walletTotalRow);
      walletTable.DefaultView.Sort = "RecType,Wallet.Name,Id";
      WalletPage.DataSource = walletTable.DefaultView;

      #endregion
    }

    private void AddProductIds(IdList productIds2, Int32 id)
    {
      if (id == 0)
        throw new ArgumentException("id=0");

      if (productIds2.Contains(id))
        return; // предотвращение зацикливания в дереве продуктов
      productIds2.Add(id);

      DBxFilter[] filters = new DBxFilter[2];
      filters[0] = new ValueFilter("ParentId", id);
      filters[1] = DBSDocType.DeletedFalseFilter;
      IdList children = ProgramDBUI.TheUI.DocProvider.GetIds("Products", AndFilter.FromArray(filters));
      foreach (Int32 child in children)
        AddProductIds(productIds2, child);
    }

    #endregion

    #region Страница "Операции"

    EFPReportDBxGridPage OpPage;

    private EFPGridProducer OpPage_CreateGridProducer()
    {
      EFPDBxGridProducer gridProducer = new EFPDBxGridProducer(ProgramDBUI.TheUI);
      gridProducer.Columns.AddDate("DocId.Date", "Дата");
      gridProducer.Columns.AddText("DocId.WalletCredit.Name", "Кошелек", 15, 10);
      gridProducer.Columns.AddText("DocId.Shop.Name", "Магазин", 25, 10);

      gridProducer.Columns.AddText("Product.Name", "Товар, услуга", 20, 5);
      gridProducer.Columns.AddText("Description", "Описание", 20, 5);
      gridProducer.Columns.AddText("Purpose.Name", "Назначение", 20, 5);

      gridProducer.Columns.AddUserText("QuantityText", "Quantity1,MU1.Name,Quantity2,MU2.Name,Quantity3,MU3.Name",
        new EFPGridProducerValueNeededEventHandler(EditOperationProduct.QuantityTextColumnValueNeeded),
        "Количество", 20, 10);

      gridProducer.Columns.AddText("Quantity1", "Кол-во 1", 5, 2);
      gridProducer.Columns.LastAdded.Format = "0.###";
      gridProducer.Columns.LastAdded.SizeGroup = "Quantity";
      gridProducer.Columns.AddText("MU1.Name", "Ед. изм. 1", 5, 2);
      gridProducer.Columns.LastAdded.SizeGroup = "MUName";

      gridProducer.Columns.AddText("Quantity2", "Кол-во 2", 5, 2);
      gridProducer.Columns.LastAdded.Format = "0.###";
      gridProducer.Columns.LastAdded.SizeGroup = "Quantity";
      gridProducer.Columns.AddText("MU2.Name", "Ед. изм. 2", 5, 2);
      gridProducer.Columns.LastAdded.SizeGroup = "MUName";

      gridProducer.Columns.AddText("Quantity3", "Кол-во 3", 5, 2);
      gridProducer.Columns.LastAdded.Format = "0.###";
      gridProducer.Columns.LastAdded.SizeGroup = "Quantity";
      gridProducer.Columns.AddText("MU3.Name", "Ед. изм. 3", 5, 2);
      gridProducer.Columns.LastAdded.SizeGroup = "MUName";

      gridProducer.Columns.AddText("Formula", "Формула", 15, 5);
      gridProducer.Columns.AddMoney("RecordSum", "Сумма");
      gridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;


      gridProducer.Columns.AddText("Comment", "Комментарий", 30, 10);

      gridProducer.NewDefaultConfig(false);
      gridProducer.DefaultConfig.Columns.Add("DocId.Date");
      gridProducer.DefaultConfig.Columns.AddFill("Product.Name", 35);
      gridProducer.DefaultConfig.Columns.AddFill("Description", 35);
      gridProducer.DefaultConfig.Columns.AddFill("QuantityText", 30);
      //gridProducer.DefaultConfig.Columns.Add("Quantity1");
      //gridProducer.DefaultConfig.Columns.Add("MU1.Name");
      //gridProducer.DefaultConfig.Columns.Add("Quantity2");
      //gridProducer.DefaultConfig.Columns.Add("MU2.Name");
      //gridProducer.DefaultConfig.Columns.Add("Quantity3");
      //gridProducer.DefaultConfig.Columns.Add("MU3.Name");
      gridProducer.DefaultConfig.Columns.Add("RecordSum");

      return gridProducer;
    }

    void OpPage_InitGrid(object sender, EventArgs args)
    {
      OpPage.ControlProvider.ConfigSectionName = "PurchaseReportOperations";
      OpPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(OpPage_GetRowAttributes);
      OpPage.ControlProvider.UseRowImages = true;
      OpPage.ControlProvider.ShowRowCountInTopLeftCellToolTipText = true;
      //OpPage.ControlProvider.DisableOrdering();

      OpPage.ControlProvider.ReadOnly = false;
      OpPage.ControlProvider.Control.ReadOnly = true;
      OpPage.ControlProvider.CanInsert = false;
      OpPage.ControlProvider.CanDelete = false;
      OpPage.ControlProvider.EditData += new EventHandler(OpPage_EditData);

      OpPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(MainPage_GetDocSel);
    }

    void OpPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
    {
      if (args.DataRow == null)
        return;

      if (DataTools.GetBool(args.DataRow, "TotalRow"))
        args.ColorType = EFPDataGridViewColorType.TotalRow;
      else
      {
        Int32 productId = DataTools.GetInt(args.DataRow, "Product");
        ProductBuffer.ProductData pd = ProductBuffer.GetProductData(productId);

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
      Int32[] docIds = DataTools.GetIdsFromColumn(OpPage.ControlProvider.SelectedDataRows, "DocId");
      if (docIds.Length == 0)
        EFPApp.ShowTempMessage("Нет выбранных операций");
      ProgramDBUI.TheUI.DocTypes["Operations"].PerformEditing(docIds, OpPage.ControlProvider.State, false);
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

    EFPReportDBxGridPage ProdPage;

    /// <summary>
    /// Ключ - строка таблицы операций
    /// Значение - идентификаторы операций
    /// </summary>
    Dictionary<DataRow, IdList> ProdTableOps;

    void ProdPage_InitGrid(object sender, EventArgs args)
    {
      // Нельзя использовать GridProducer для таблицы, так как столбцы товара, описания и количества являются обязательными,
      // без них итоги будут бессмысленными

      ProdPage.ControlProvider.Control.AutoGenerateColumns = false;
      ProdPage.ControlProvider.Columns.AddTextFill("Product.Name", true, "Товар, услуга", 35, 5);
      if (Params.ProductDet >= PurchaseReportParams.ProducDetLevel.Description)
        ProdPage.ControlProvider.Columns.AddTextFill("Description", true, "Описание", 35, 5);

      if (Params.ProductDet >= PurchaseReportParams.ProducDetLevel.Quantity)
        ProdPage.ControlProvider.Columns.AddTextFill("QuantityText", true, "Количество", 30, 10);
      ProdPage.ControlProvider.Columns.AddText("RecordSum", true, "Сумма");
      ProdPage.ControlProvider.Columns.LastAdded.TextAlign = HorizontalAlignment.Right;
      ProdPage.ControlProvider.Columns.LastAdded.GridColumn.DefaultCellStyle.Format = Tools.MoneyFormat;
      ProdPage.ControlProvider.DisableOrdering();
      ProdPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(ProdPage_GetRowAttributes);

      ProdPage.ControlProvider.Control.ReadOnly = true;
      ProdPage.ControlProvider.ReadOnly = false;
      ProdPage.ControlProvider.CanInsert = false;
      ProdPage.ControlProvider.CanDelete = false;
      ProdPage.ControlProvider.EditData += new EventHandler(ProdPage_EditData);
      ProdPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(ProdPage_GetDocSel);
    }

    void ProdPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
    {
      if (DataTools.GetInt(args.DataRow, "RecType") == 1)
        args.ColorType = EFPDataGridViewColorType.TotalRow;
    }

    void ProdPage_EditData(object sender, EventArgs args)
    {
      DataRow[] rows = ProdPage.ControlProvider.SelectedDataRows;
      IdList allOps = new IdList();
      for (int i = 0; i < rows.Length; i++)
      {
        IdList lst;
        if (ProdTableOps.TryGetValue(rows[i], out lst))
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
        if (ProdTableOps.TryGetValue(args.DataRows[i], out lst))
          allOps.Add(lst);
      }
      args.DocSel.Add("Operations", allOps);
      args.AddFromColumn("Products", "Product");
    }

    #endregion

    #region Страница "Магазины"

    EFPReportDBxGridPage ShopPage;

    /// <summary>
    /// Ключ - строка таблицы операций
    /// Значение - идентификаторы операций
    /// </summary>
    Dictionary<DataRow, IdList> ShopTableOps;

    void ShopPage_InitGrid(object sender, EventArgs args)
    {
      ShopPage.ControlProvider.Control.AutoGenerateColumns = false;
      ShopPage.ControlProvider.Columns.AddTextFill("Shop.Name", true, "Магазин", 100, 20);
      ShopPage.ControlProvider.Columns.AddText("RecordSum", true, "Сумма");
      ShopPage.ControlProvider.Columns.LastAdded.TextAlign = HorizontalAlignment.Right;
      ShopPage.ControlProvider.Columns.LastAdded.GridColumn.DefaultCellStyle.Format = Tools.MoneyFormat;
      ShopPage.ControlProvider.DisableOrdering();
      ShopPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(ShopPage_GetRowAttributes);

      ShopPage.ControlProvider.Control.ReadOnly = true;
      ShopPage.ControlProvider.ReadOnly = false;
      ShopPage.ControlProvider.CanInsert = false;
      ShopPage.ControlProvider.CanDelete = false;
      ShopPage.ControlProvider.EditData += new EventHandler(ShopPage_EditData);
      ShopPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(ShopPage_GetDocSel);
    }

    void ShopPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
    {
      if (DataTools.GetInt(args.DataRow, "RecType") == 1)
        args.ColorType = EFPDataGridViewColorType.TotalRow;
    }

    void ShopPage_EditData(object sender, EventArgs args)
    {
      DataRow[] rows = ShopPage.ControlProvider.SelectedDataRows;
      IdList allOps = new IdList();
      for (int i = 0; i < rows.Length; i++)
      {
        IdList lst;
        if (ShopTableOps.TryGetValue(rows[i], out lst))
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
        if (ShopTableOps.TryGetValue(args.DataRows[i], out lst))
          allOps.Add(lst);
      }
      args.DocSel.Add("Operations", allOps);
      args.AddFromColumn("Shops", "Id");
    }

    #endregion

    #region Страница "Кошельки"

    EFPReportDBxGridPage WalletPage;

    /// <summary>
    /// Ключ - строка таблицы операций
    /// Значение - идентификаторы операций
    /// </summary>
    Dictionary<DataRow, IdList> WalletTableOps;

    void WalletPage_InitGrid(object sender, EventArgs args)
    {
      WalletPage.ControlProvider.Control.AutoGenerateColumns = false;
      WalletPage.ControlProvider.Columns.AddTextFill("Wallet.Name", true, "Кошелек", 100, 20);
      WalletPage.ControlProvider.Columns.AddText("RecordSum", true, "Сумма");
      WalletPage.ControlProvider.Columns.LastAdded.TextAlign = HorizontalAlignment.Right;
      WalletPage.ControlProvider.Columns.LastAdded.GridColumn.DefaultCellStyle.Format = Tools.MoneyFormat;
      WalletPage.ControlProvider.DisableOrdering();
      WalletPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(WalletPage_GetRowAttributes);

      WalletPage.ControlProvider.Control.ReadOnly = true;
      WalletPage.ControlProvider.ReadOnly = false;
      WalletPage.ControlProvider.CanInsert = false;
      WalletPage.ControlProvider.CanDelete = false;
      WalletPage.ControlProvider.EditData += new EventHandler(WalletPage_EditData);
      WalletPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(WalletPage_GetDocSel);
    }

    void WalletPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
    {
      if (DataTools.GetInt(args.DataRow, "RecType") == 1)
        args.ColorType = EFPDataGridViewColorType.TotalRow;
    }

    void WalletPage_EditData(object sender, EventArgs args)
    {
      DataRow[] rows = WalletPage.ControlProvider.SelectedDataRows;
      IdList allOps = new IdList();
      for (int i = 0; i < rows.Length; i++)
      {
        IdList lst;
        if (WalletTableOps.TryGetValue(rows[i], out lst))
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
        if (WalletTableOps.TryGetValue(args.DataRows[i], out lst))
          allOps.Add(lst);
      }
      args.DocSel.Add("Operations", allOps);
      args.AddFromColumn("Wallets", "Id");
    }

    #endregion
  }
}