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

namespace App
{
  public partial class FeedbackReportParamForm : EFPReportExtParamsForm
  {
    #region Конструктор

    public FeedbackReportParamForm()
    {
      InitializeComponent();

      efpPeriod = new EFPDateRangeBox(FormProvider, edPeriod);
      efpPeriod.First.CanBeEmpty = true;
      efpPeriod.Last.CanBeEmpty = true;

      efpProducts = new EFPMultiDocComboBox(FormProvider, cbProducts, ProgramDBUI.TheUI.DocTypes["Products"]);
      efpProducts.SelectionMode = DocSelectionMode.MultiCheckBoxes;
      efpProducts.CanBeEmpty = true;
      efpProducts.EmptyText = "[ Все расходы ]";
      efpProducts.MaxTextItemCount = 5;
    }

    #endregion

    #region Поля

    public EFPDateRangeBox efpPeriod;

    public EFPMultiDocComboBox efpProducts;

    #endregion
  }

  internal class FeedbackReportParams : EFPReportExtParams
  {
    #region Поля

    public DateTime? FirstDate;

    public DateTime? LastDate;

    public Int32[] ProductIds;

    #endregion

    #region Переопределенные методы

    protected override void OnInitTitle()
    {
      if (FirstDate.HasValue || LastDate.HasValue)
        base.Title = "Отзывы за период " + DateRangeFormatter.Default.ToString(FirstDate, LastDate, true);
      else
        base.Title = "Отзывы";

      if (ProductIds != null && ProductIds.Length > 0)
        base.FilterInfo.Add("Товары", String.Join(", ", ProgramDBUI.TheUI.DocProvider.GetTextValues("Products", ProductIds)));
    }

    public override EFPReportExtParamsForm CreateForm()
    {
      return new FeedbackReportParamForm();
    }

    public override void WriteFormValues(EFPReportExtParamsForm form, SettingsPart part)
    {
      FeedbackReportParamForm form2 = (FeedbackReportParamForm)form;
      form2.efpPeriod.First.NValue = FirstDate;
      form2.efpPeriod.Last.NValue = LastDate;
      form2.efpProducts.DocIds = ProductIds;
    }

    public override void ReadFormValues(EFPReportExtParamsForm form, SettingsPart part)
    {
      FeedbackReportParamForm form2 = (FeedbackReportParamForm)form;
      FirstDate = form2.efpPeriod.First.NValue;
      LastDate = form2.efpPeriod.Last.NValue;
      ProductIds = form2.efpProducts.DocIds;
    }

    public override void WriteConfig(CfgPart cfg, SettingsPart part)
    {
      cfg.SetNullableDate("FirstDate", FirstDate);
      cfg.SetNullableDate("LastDate", LastDate);
      cfg.SetIntCommaString("Products", ProductIds);
    }

    public override void ReadConfig(CfgPart cfg, SettingsPart part)
    {
      FirstDate = cfg.GetNullableDate("FirstDate");
      LastDate = cfg.GetNullableDate("LastDate");
      ProductIds = cfg.GetIntCommaString("Products");
    }

    #endregion
  }

  internal class FeedbackReport : EFPReport
  {
    #region Конструктор

    public FeedbackReport()
      : base("FeedbackReport")
    {
      MainImageKey = "FeedbackReport";

      _MainPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      _MainPage.Title = "Операции";
      _MainPage.ImageKey = "Operation";
      _MainPage.InitGrid += new EventHandler(MainPage_InitGrid);
      _MainPage.ShowToolBar = true;
      _MainPage.GridProducer = MainPage_CreateGridProducer();
      Pages.Add(_MainPage);
    }

    #endregion

    #region Запрос параметров

    protected override EFPReportParams CreateParams()
    {
      return new FeedbackReportParams();
    }

    public FeedbackReportParams Params { get { return (FeedbackReportParams)(base.ReportParams); } }

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
      filters.Add(new NotNullFilter("Comment", DBxColumnType.String));
      filters.Add(DBSSubDocType.DeletedFalseFilter);
      filters.Add(DBSSubDocType.DocIdDeletedFalseFilter);
      filters.Add(new ValueFilter("DocId.OpType", (int)OperationType.Expense)); // чтобы мусор не пролез
      si.Where = AndFilter.FromList(filters);

      DataTable prodTable = ProgramDBUI.TheUI.DocProvider.FillSelect(si);
      _MainPage.DataSource = prodTable.DefaultView;

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

    EFPReportDBxGridPage _MainPage;

    private EFPGridProducer MainPage_CreateGridProducer()
    {
      EFPDBxGridProducer producer = new EFPDBxGridProducer(ProgramDBUI.TheUI);
      producer.Columns.AddDate("DocId.Date", "Дата");

      producer.Columns.AddText("Product.Name", "Товар, услуга", 20, 5);
      producer.Columns.AddText("Comment", "Отзыв", 30, 10);
      producer.Columns.AddText("DocId.WalletCredit.Name", "Кошелек", 15, 10);
      producer.Columns.AddText("DocId.Shop.Name", "Магазин", 25, 10);
      producer.Columns.AddText("Description", "Описание", 20, 5);
      producer.Columns.AddText("Purpose.Name", "Назначение", 20, 5);


      producer.Columns.AddUserText("QuantityText", "Quantity1,MU1.Name,Quantity2,MU2.Name,Quantity3,MU3.Name",
        new EFPGridProducerValueNeededEventHandler(EditOperationProduct.QuantityTextColumnValueNeeded),
        "Количество", 20, 10);


      producer.NewDefaultConfig(false);
      producer.DefaultConfig.Columns.Add("DocId.Date");
      producer.DefaultConfig.Columns.AddFill("Product.Name", 30);
      producer.DefaultConfig.Columns.AddFill("Comment", 70);

      return producer;
    }

    void MainPage_InitGrid(object sender, EventArgs args)
    {
      _MainPage.ControlProvider.ConfigSectionName = base.ConfigSectionName;
      _MainPage.ControlProvider.UseRowImages = true;
      _MainPage.ControlProvider.ShowRowCountInTopLeftCell = true;
      _MainPage.ControlProvider.ShowErrorCountInTopLeftCell = true;
      //OpPage.ControlProvider.DisableOrdering();

      _MainPage.ControlProvider.ReadOnly = false;
      _MainPage.ControlProvider.Control.ReadOnly = true;
      _MainPage.ControlProvider.CanInsert = false;
      _MainPage.ControlProvider.CanDelete = false;
      _MainPage.ControlProvider.EditData += new EventHandler(MainPage_EditData);

      _MainPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(MainPage_GetDocSel);
    }

    void MainPage_EditData(object sender, EventArgs args)
    {
      Int32[] docIds = DataTools.GetIdsFromColumn(_MainPage.ControlProvider.SelectedDataRows, "DocId");
      if (docIds.Length == 0)
        EFPApp.ShowTempMessage("Нет выбранных операций");
      ProgramDBUI.TheUI.DocTypes["Operations"].PerformEditing(docIds, _MainPage.ControlProvider.State, false);
    }

    void MainPage_GetDocSel(object sender, EFPDBxGridViewDocSelEventArgs args)
    {
      args.AddFromColumn("Operations", "DocId");
      args.AddFromColumn("Products", "Product");
      args.AddFromColumn("Shops", "DocId.Shop");
      args.AddFromColumn("Wallets", "DocId.WalletCredit");
    }

    #endregion
  }
}