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
      efpProducts.MaxTextItemCount = 5;

      efpWallets = new EFPMultiDocComboBox(FormProvider, cbWallets, ProgramDBUI.TheUI.DocTypes["Wallets"]);
      efpWallets.SelectionMode = DocSelectionMode.MultiCheckBoxes;
      efpWallets.CanBeEmpty = true;
      efpWallets.EmptyText = "[ Все кошельки ]";
      efpWallets.MaxTextItemCount = 3;
    }

    #endregion

    #region Поля

    public EFPDateRangeBox efpPeriod;

    public EFPMultiDocComboBox efpProducts;

    public EFPMultiDocComboBox efpWallets;

    #endregion
  }

  internal class PurchaseReportParams : EFPReportExtParams
  {
    #region Поля

    public DateTime? FirstDate;

    public DateTime? LastDate;

    public Int32[] ProductIds;

    public Int32[] WalletIds;

    #endregion

    #region Переопределенные методы

    protected override void OnInitTitle()
    {
      base.Title = "Покупки за " + DateRangeFormatter.Default.ToString(FirstDate, LastDate, true);

      base.FilterInfo.Add("Товары", String.Join(", ", ProgramDBUI.TheUI.DocProvider.GetTextValues("Products", ProductIds)));

      if (WalletIds != null && WalletIds.Length > 0)
        base.FilterInfo.Add("Кошельки", String.Join(", ", ProgramDBUI.TheUI.DocProvider.GetTextValues("Wallets", WalletIds)));
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
    }

    public override void ReadFormValues(EFPReportExtParamsForm Form, EFPReportExtParamsPart Part)
    {
      PurchaseReportParamForm Form2 = (PurchaseReportParamForm)Form;
      FirstDate = Form2.efpPeriod.First.NValue;
      LastDate = Form2.efpPeriod.Last.NValue;
      WalletIds = Form2.efpWallets.DocIds;
      ProductIds = Form2.efpProducts.DocIds;
    }

    public override void WriteConfig(FreeLibSet.Config.CfgPart Config, EFPReportExtParamsPart Part)
    {
      Config.SetNullableDate("FirstDate", FirstDate);
      Config.SetNullableDate("LastDate", LastDate);
      Config.SetIntCommaString("Products", ProductIds);
      Config.SetIntCommaString("Wallets", WalletIds);
    }

    public override void ReadConfig(FreeLibSet.Config.CfgPart Config, EFPReportExtParamsPart Part)
    {
      FirstDate = Config.GetNullableDate("FirstDate");
      LastDate = Config.GetNullableDate("LastDate");
      ProductIds = Config.GetIntCommaString("Products");
      WalletIds = Config.GetIntCommaString("Wallets");
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

      MainPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      MainPage.InitGrid += new EventHandler(MainPage_InitGrid);
      MainPage.ShowToolBar = true;
      MainPage.GridProducer = CreateMainPageGridProducer();
      Pages.Add(MainPage);
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

      IdList productIds2 = new IdList();
      for (int i = 0; i < Params.ProductIds.Length; i++)
        AddProductIds(productIds2, Params.ProductIds[i]); // рекурсивная процедура

      #endregion

      #region Загрузка данных

      DBxSelectInfo si = new DBxSelectInfo();
      si.TableName = "OperationProducts";
      si.Expressions.Add("Id"); // нужно только для EFPDBxGridView
      si.Expressions.Add("DocId,RecordOrder,Product,Product.Name,Description,Quantity1,MU1,MU1.Name,Quantity2,MU2,MU2.Name,Quantity3,MU3,MU3.Name,Formula,RecordSum,Comment");
      si.Expressions.Add("DocId.Date,DocId.OpOrder,DocId.WalletCredit,DocId.WalletCredit.Name,DocId.Shop,DocId.Shop.Name,DocId.Comment");

      List<DBxFilter> filters = new List<DBxFilter>();
      filters.Add(new IdsFilter("Product", productIds2));
      filters.Add(new DateRangeFilter("DocId.Date", Params.FirstDate, Params.LastDate));
      if (Params.WalletIds != null && Params.WalletIds.Length > 0)
        filters.Add(new IdsFilter("DocId.WalletCredit", Params.WalletIds));
      filters.Add(DBSSubDocType.DeletedFalseFilter);
      filters.Add(DBSSubDocType.DocIdDeletedFalseFilter);
      filters.Add(new ValueFilter("DocId.OpType", (int)OperationType.Expense)); // чтобы мусор не пролез
      si.Where = AndFilter.FromList(filters);

      DataTable table1 = ProgramDBUI.TheUI.DocProvider.FillSelect(si);

      #endregion

      #region Таблица для отображения

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
      }

      DataRow totalRow = table2.NewRow();
      totalRow["TotalRow"] = true;
      totalRow["Product.Name"] = "Итого";
      DataTools.SumDecimal(totalRow, "RecordSum");
      table2.Rows.Add(totalRow);

      table2.DefaultView.Sort = "TotalRow,DocId.Date,DocId.OpOrder,RecordOrder";

      MainPage.DataSource = table2.DefaultView;

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

    #region Страница отчета

    EFPReportDBxGridPage MainPage;

    private EFPGridProducer CreateMainPageGridProducer()
    {
      EFPDBxGridProducer gridProducer = new EFPDBxGridProducer(ProgramDBUI.TheUI);
      gridProducer.Columns.AddDate("DocId.Date", "Дата");
      gridProducer.Columns.AddText("DocId.WalletCredit.Name", "Кошелек", 15, 10);
      gridProducer.Columns.AddText("DocId.Shop.Name", "Магазин", 25, 10);

      gridProducer.Columns.AddText("Product.Name", "Товар, услуга", 20, 5);
      gridProducer.Columns.AddText("Description", "Описание", 20, 5);

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
      gridProducer.DefaultConfig.Columns.AddFill("Product.Name", 50);
      gridProducer.DefaultConfig.Columns.AddFill("Description", 50);
      gridProducer.DefaultConfig.Columns.Add("Quantity1");
      gridProducer.DefaultConfig.Columns.Add("MU1.Name");
      gridProducer.DefaultConfig.Columns.Add("Quantity2");
      gridProducer.DefaultConfig.Columns.Add("MU2.Name");
      gridProducer.DefaultConfig.Columns.Add("Quantity3");
      gridProducer.DefaultConfig.Columns.Add("MU3.Name");
      gridProducer.DefaultConfig.Columns.Add("RecordSum");

      return gridProducer;
    }

    void MainPage_InitGrid(object Sender, EventArgs Args)
    {
      MainPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(MainPage_GetRowAttributes);
      MainPage.ControlProvider.UseRowImages = true;
      MainPage.ControlProvider.ShowRowCountInTopLeftCellToolTipText = true;

      MainPage.ControlProvider.ReadOnly = false;
      MainPage.ControlProvider.Control.ReadOnly = true;
      MainPage.ControlProvider.CanInsert = false;
      MainPage.ControlProvider.CanDelete = false;
      MainPage.ControlProvider.EditData += new EventHandler(MainPage_EditData);

      MainPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(MainPage_GetDocSel);
    }

    void MainPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
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
          args.AddRowError("Описание не должно заполняться");
        }

        #endregion

        #region Количество и единица измерения

        float Q1 = DataTools.GetSingle(args.DataRow, "Quantity1");
        float Q2 = DataTools.GetSingle(args.DataRow, "Quantity2");
        float Q3 = DataTools.GetSingle(args.DataRow, "Quantity3");
        Int32 muId1 = DataTools.GetInt(args.DataRow, "MU1");
        Int32 muId2 = DataTools.GetInt(args.DataRow, "MU2");
        Int32 muId3 = DataTools.GetInt(args.DataRow, "MU3");

        if (Q1 == 0f && Q2 != 0f)
          args.AddRowError("Заполнена вторая единица измерения без первой", "Quantity2");
        if (Q2 == 0f && Q3 != 0f)
          args.AddRowError("Заполнена третья единица измерения без второй", "Quantity3");
        CheckQuantityAndMUPair(args, Q1, muId1, "1");
        CheckQuantityAndMUPair(args, Q2, muId2, "2");
        CheckQuantityAndMUPair(args, Q3, muId3, "3");
        CheckMUPair(args, muId1, muId2, "1", "2");
        CheckMUPair(args, muId2, muId3, "2", "3");
        CheckMUPair(args, muId1, muId3, "1", "3");

        bool hasQ = (Q1 != 0f) || (Q2 != 0f) || (Q3 != 0f);
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
              args.AddRowInformation("Количество обычно должно быть заполнено", "Quantity1,MU1.Name");
              break;
          }
        }

        #endregion
      }
    }

    private static void CheckQuantityAndMUPair(EFPDataGridViewRowAttributesEventArgs args, float Q, int muId, string suffix)
    {
      if ((Q == 0f) != (muId == 0))
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

    void MainPage_EditData(object Sender, EventArgs Args)
    {
      Int32[] DocIds = DataTools.GetIdsFromColumn(MainPage.ControlProvider.SelectedDataRows, "DocId");
      if (DocIds.Length == 0)
        EFPApp.ShowTempMessage("Нет выбранных операций");
      ProgramDBUI.TheUI.DocTypes["Operations"].PerformEditing(DocIds, MainPage.ControlProvider.State, false);
    }

    void MainPage_GetDocSel(object Sender, EFPDBxGridViewDocSelEventArgs Args)
    {
      Args.AddFromColumn("Operations", "DocId");
      Args.AddFromColumn("Products", "Product");
      Args.AddFromColumn("Shops", "DocId.Shop");
      Args.AddFromColumn("Wallets", "DocId.WalletCredit");
    }

    #endregion
  }
}