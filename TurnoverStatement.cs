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
      efpWallets.SelectionMode = DocSelectionMode.MultiCheckBoxes;
      efpWallets.MaxTextItemCount = 3;
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
      base.FilterInfo.Add("Кошельки", String.Join(", ", ProgramDBUI.TheUI.DocProvider.GetTextValues("Wallets", WalletIds)));
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
      : base("TurnoverStatement")
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
      Table.Columns.Add("OpType", typeof(int));
      Table.Columns.Add("InitialBalance", typeof(decimal));
      Table.Columns.Add("DisplayName", typeof(string));
      Table.Columns.Add("TotalDebt", typeof(decimal));
      Table.Columns.Add("TotalCredit", typeof(decimal));
      Table.Columns.Add("FinalBalance", typeof(decimal));
      Table.Columns.Add("BalanceConfirmed", typeof(bool));

      decimal currSaldo = 0m;
      decimal[] walletSaldo = new decimal[Params.WalletIds.Length];

      #region Начальное сальдо

      for (int i = 0; i < Params.WalletIds.Length; i++)
      {
        List<DBxFilter> filters = new List<DBxFilter>();
        DBxSelectInfo si1 = new DBxSelectInfo();
        si1.TableName = "Operations";
        si1.Expressions.Add(new DBxAgregateFunction(DBxAgregateFunctionKind.Sum, "TotalDebt"), "SumDebt");
        filters.Add(new ValueFilter("Date", Params.FirstDate.Value, CompareKind.LessThan));
        filters.Add(new ValueFilter("WalletDebt", Params.WalletIds[i]));
        filters.Add(DBSDocType.DeletedFalseFilter);
        si1.Where = AndFilter.FromList(filters);
        decimal SumDebt = DataTools.GetDecimal(ProgramDBUI.TheUI.DocProvider.FillSelect(si1).Rows[0][0]);

        DBxSelectInfo si2 = new DBxSelectInfo();
        si2.TableName = "Operations";
        si2.Expressions.Add(new DBxAgregateFunction(DBxAgregateFunctionKind.Sum, "TotalCredit"), "SumCredit");
        filters.Clear();
        filters.Add(new ValueFilter("Date", Params.FirstDate.Value, CompareKind.LessThan));
        filters.Add(new ValueFilter("WalletCredit", Params.WalletIds[i]));
        filters.Add(DBSDocType.DeletedFalseFilter);
        si2.Where = AndFilter.FromList(filters);
        decimal SumCredit = DataTools.GetDecimal(ProgramDBUI.TheUI.DocProvider.FillSelect(si2).Rows[0][0]);

        walletSaldo[i] = SumDebt - SumCredit;
        currSaldo += walletSaldo[i];
      }

      #endregion

      #region За период

      DataTable srcTable = ProgramDBUI.TheUI.DocProvider.FillSelect("Operations",
        new DBxColumns("Id,Date,DisplayName,OpType,TotalDebt,TotalCredit,InlineSum,WalletDebt,WalletCredit"),
        new AndFilter(new DateRangeFilter("Date", Params.FirstDate, Params.LastDate), DBSDocType.DeletedFalseFilter),
        DBxOrder.FromDataViewSort("Date,OpOrder,OpOrder2,Id"));

      foreach (DataRow srcRow in srcTable.Rows)
      {
        OperationType opType = DataTools.GetEnum<OperationType>(srcRow, "OpType");
        int pDebtWallet = -1;
        if (Tools.UseDebt(opType))
          pDebtWallet = Array.IndexOf<Int32>(Params.WalletIds, DataTools.GetInt(srcRow, "WalletDebt"));
        int pCreditWallet = -1;
        if (Tools.UseCredit(opType))
          pCreditWallet = Array.IndexOf<Int32>(Params.WalletIds, DataTools.GetInt(srcRow, "WalletCredit"));

        if (pDebtWallet < 0 && pCreditWallet < 0)
          continue;


        DataRow resRow = Table.NewRow();

        DataTools.CopyRowValues(srcRow, resRow, true);
        if (pDebtWallet < 0)
          resRow["TotalDebt"] = DBNull.Value;
        if (pCreditWallet < 0)
          resRow["TotalCredit"] = DBNull.Value;

        resRow["InitialBalance"] = currSaldo;
        if (pDebtWallet >= 0)
        {
          currSaldo += DataTools.GetDecimal(srcRow, "TotalDebt");
          walletSaldo[pDebtWallet] += DataTools.GetDecimal(srcRow, "TotalDebt");
        }
        if (pCreditWallet >= 0)
        {
          currSaldo -= DataTools.GetDecimal(srcRow, "TotalCredit");
          walletSaldo[pCreditWallet] -= DataTools.GetDecimal(srcRow, "TotalCredit");
        }
        resRow["FinalBalance"] = currSaldo;

        if (opType == OperationType.Balance)
        {
          if (pDebtWallet < 0)
            throw new BugException("Balance operation without wallet");

          StringBuilder sb = new StringBuilder();
          sb.Append("Баланс для кошелька \"");
          sb.Append(ProgramDBUI.TheUI.DocProvider.GetTextValue("Wallets", Params.WalletIds[pDebtWallet]));
          sb.Append("\": ");
          decimal s = DataTools.GetDecimal(srcRow, "InlineSum");
          sb.Append(s.ToString(Tools.MoneyFormat));
          sb.Append(" - ");
          if (s == walletSaldo[pDebtWallet])
          {
            resRow["BalanceConfirmed"] = true;
            sb.Append("Подтвержден");
          }
          else
          {
            resRow["BalanceConfirmed"] = false;
            sb.Append("Не подтвержден. Реальный остаток: ");
            sb.Append(walletSaldo[pDebtWallet].ToString(Tools.MoneyFormat));
          }
          resRow["DisplayName"] = sb.ToString();
        }

        Table.Rows.Add(resRow);
      }

      #endregion

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
      gridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;
      gridProducer.Columns.AddRefDocImage("Id", ProgramDBUI.TheUI.DocTypes["Operations"]);
      gridProducer.Columns.AddText("DisplayName", "Содержание операции", 20, 15);
      gridProducer.Columns.AddMoney("TotalDebt", "Дебет");
      gridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;
      gridProducer.Columns.AddMoney("TotalCredit", "Кредит");
      gridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;
      gridProducer.Columns.AddMoney("FinalBalance", "Конечное сальдо");
      gridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;

      gridProducer.NewDefaultConfig(false);
      gridProducer.DefaultConfig.Columns.Add("Date");
      gridProducer.DefaultConfig.Columns.Add("InitialBalance");
      gridProducer.DefaultConfig.Columns.Add("Id_Image");
      gridProducer.DefaultConfig.Columns.AddFill("DisplayName");
      gridProducer.DefaultConfig.Columns.Add("TotalDebt");
      gridProducer.DefaultConfig.Columns.Add("TotalCredit");
      gridProducer.DefaultConfig.Columns.Add("FinalBalance");

      return gridProducer;
    }

    void MainPage_InitGrid(object Sender, EventArgs Args)
    {
      MainPage.ControlProvider.GetCellAttributes += new EFPDataGridViewCellAttributesEventHandler(MainPage_GetCellAttributes);

      MainPage.ControlProvider.ReadOnly = false;
      MainPage.ControlProvider.Control.ReadOnly = true;
      MainPage.ControlProvider.CanInsert = false;
      MainPage.ControlProvider.CanDelete = false;
      MainPage.ControlProvider.EditData += new EventHandler(MainPage_EditData);

      MainPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(MainPage_GetDocSel);
    }

    void MainPage_GetCellAttributes(object sender, EFPDataGridViewCellAttributesEventArgs args)
    {
      switch (args.ColumnName)
      { 
        case "InitialBalance":
        case "FinalBalance":
          if (DataTools.GetDecimal(args.DataRow, args.ColumnName) < 0)
            args.ColorType = EFPDataGridViewColorType.Error;
          break;
        case "DisplayName":
          if (DataTools.GetEnum<OperationType>(args.DataRow, "OpType") == OperationType.Balance)
          {
            if (!DataTools.GetBool(args.DataRow, "BalanceConfirmed"))
              args.ColorType = EFPDataGridViewColorType.Error;
          }
          break;
      }
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
      Args.AddFromColumn("Operations", "Id");
    }

    #endregion
  }
}