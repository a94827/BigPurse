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
using FreeLibSet.Core;
using FreeLibSet.Data;
using FreeLibSet.Data.Docs;

namespace App
{
  internal partial class TurnoverStatementParamForm : EFPReportExtParamsForm
  {
    #region Конструктор

    public TurnoverStatementParamForm()
    {
      InitializeComponent();

      efpPeriod = new EFPDateRangeBox(FormProvider, edPeriod);

      efpWallets = new EFPMultiDocComboBox(FormProvider, cbWallets, ProgramDBUI.TheUI.DocTypes["Wallets"]);
    }

    #endregion

    #region Поля

    public EFPDateRangeBox efpPeriod;

    public EFPMultiDocComboBox efpWallets;

    #endregion
  }

  internal class TurnoverStatementParams : EFPReportExtParams
  {
    #region Поля

    public DateTime? FirstDate;

    public DateTime? LastDate;

    public Int32[] WalletIds;

    #endregion

    #region Переопределенные методы

    protected override void OnInitTitle()
    {
      base.Title = "Оборотная ведомость за " + DateRangeFormatter.Default.ToString(FirstDate, LastDate, true);
      string[] walletNames = new string[WalletIds.Length];
      for (int i = 0; i < WalletIds.Length; i++)
        walletNames[i] = ProgramDBUI.TheUI.DocProvider.GetTextValue("Wallets", WalletIds[i]);
      base.FilterInfo.Add("Кошельки", String.Join(", ", walletNames));
    }

    public override EFPReportExtParamsForm CreateForm()
    {
      return new TurnoverStatementParamForm();
    }

    public override void WriteFormValues(EFPReportExtParamsForm Form, EFPReportExtParamsPart Part)
    {
      TurnoverStatementParamForm Form2 = (TurnoverStatementParamForm)Form;
      Form2.efpPeriod.First.NValue = FirstDate;
      Form2.efpPeriod.Last.NValue = LastDate;
      Form2.efpWallets.DocIds = WalletIds;
    }

    public override void ReadFormValues(EFPReportExtParamsForm Form, EFPReportExtParamsPart Part)
    {
      TurnoverStatementParamForm Form2 = (TurnoverStatementParamForm)Form;
      FirstDate = Form2.efpPeriod.First.NValue;
      LastDate = Form2.efpPeriod.Last.NValue;
      WalletIds = Form2.efpWallets.DocIds;
    }

    public override void WriteConfig(FreeLibSet.Config.CfgPart Config, EFPReportExtParamsPart Part)
    {
      Config.SetNullableDate("FirstDate", FirstDate);
      Config.SetNullableDate("LastDate", LastDate);
      Config.SetIntCommaString("Wallets", WalletIds);
    }

    public override void ReadConfig(FreeLibSet.Config.CfgPart Config, EFPReportExtParamsPart Part)
    {
      FirstDate = Config.GetNullableDate("FirstDate");
      LastDate = Config.GetNullableDate("LastDate");
      WalletIds = Config.GetIntCommaString("Wallets");
    }

    #endregion
  }

  internal class TurnoverStatement : EFPReport
  {
    #region Конструктор

    public TurnoverStatement()
      :base("TurnoverStatement")
    {
      MainImageKey = "TurnoverStatement";

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
      return new TurnoverStatementParams();
    }

    public TurnoverStatementParams Params { get { return (TurnoverStatementParams)(base.ReportParams); } }

    #endregion

    #region Построение отчета

    protected override void BuildReport()
    {
      DataTable Table = new DataTable();
      Table.Columns.Add("Id", typeof(Int32));
      Table.Columns.Add("Date", typeof(DateTime));
      Table.Columns.Add("InitialBalance", typeof(decimal));
      Table.Columns.Add("DisplayName", typeof(string));
      Table.Columns.Add("Sum", typeof(decimal));
      Table.Columns.Add("FinalBalance", typeof(decimal));

      DataTable srcTable = ProgramDBUI.TheUI.DocProvider.FillSelect("Operations",
        new DBxColumns("Id,Date,DisplayName,OpType,TotalDebt,TotalCredit,WalletDebt,WalletCredit"),
        new AndFilter(new DateRangeFilter("Date", Params.FirstDate, Params.LastDate), DBSDocType.DeletedFalseFilter),
        DBxOrder.FromDataViewSort("Date,OpOrder,Id"));

      foreach (DataRow srcRow in srcTable.Rows)
      { 
        //!!

        DataRow resRow = Table.NewRow();
        DataTools.CopyRowValues(srcRow, resRow, true);
        Table.Rows.Add(resRow);
      }

      MainPage.DataSource = Table.DefaultView;
    }

    #endregion

    #region Страница отчета

    EFPReportDBxGridPage MainPage;


    private EFPGridProducer CreateMainPageGridProducer()
    {
      EFPDBxGridProducer gridProducer = new EFPDBxGridProducer(ProgramDBUI.TheUI);
      gridProducer.Columns.AddDate("Date", "Дата");
      gridProducer.Columns.AddMoney("InitialBalance", "Начальное сальдо");
      gridProducer.Columns.AddText("DisplayName", "Содержание операции", 20, 15);
      gridProducer.Columns.AddMoney("Sum", "Сумма операции");
      gridProducer.Columns.AddMoney("FinalBalance", "Конечное сальдо");

      gridProducer.NewDefaultConfig(false);
      gridProducer.DefaultConfig.Columns.Add("Date");
      gridProducer.DefaultConfig.Columns.Add("InitialBalance");
      gridProducer.DefaultConfig.Columns.AddFill("DisplayName");
      gridProducer.DefaultConfig.Columns.Add("Sum");
      gridProducer.DefaultConfig.Columns.Add("FinalBalance");

      return gridProducer;
    }

    void MainPage_InitGrid(object Sender, EventArgs Args)
    {
      MainPage.ControlProvider.ReadOnly = false;
      MainPage.ControlProvider.Control.ReadOnly = true;
      MainPage.ControlProvider.CanInsert = false;
      MainPage.ControlProvider.CanDelete = false;
      MainPage.ControlProvider.EditData += new EventHandler(MainPage_EditData);

      MainPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(MainPage_GetDocSel);
    }

    void MainPage_EditData(object Sender, EventArgs Args)
    {
      Int32[] DocIds = DataTools.GetIdsFromColumn(MainPage.ControlProvider.SelectedDataRows, "Id");
      if (DocIds.Length == 0)
        EFPApp.ShowTempMessage("Нет выбранных операций");
      ProgramDBUI.TheUI.DocTypes["Operations"].PerformEditing(DocIds, MainPage.ControlProvider.State, false);
    }

    void MainPage_GetDocSel(object Sender, EFPDBxGridViewDocSelEventArgs Args)
    {
      Args.AddFromColumn("Operations", "DocId");
    }

    #endregion
  }
}