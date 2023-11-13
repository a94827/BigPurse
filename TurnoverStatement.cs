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
using FreeLibSet.Config;

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

    public override void WriteFormValues(EFPReportExtParamsForm form, SettingsPart part)
    {
      TurnoverStatementParamForm form2 = (TurnoverStatementParamForm)form;
      form2.efpPeriod.First.NValue = FirstDate;
      form2.efpPeriod.Last.NValue = LastDate;
      form2.efpWallets.DocIds = WalletIds;
    }

    public override void ReadFormValues(EFPReportExtParamsForm form, SettingsPart part)
    {
      TurnoverStatementParamForm form2 = (TurnoverStatementParamForm)form;
      FirstDate = form2.efpPeriod.First.NValue;
      LastDate = form2.efpPeriod.Last.NValue;
      WalletIds = form2.efpWallets.DocIds;
    }

    public override void WriteConfig(CfgPart cfg, SettingsPart part)
    {
      cfg.SetNullableDate("FirstDate", FirstDate);
      cfg.SetNullableDate("LastDate", LastDate);
      cfg.SetIntCommaString("Wallets", WalletIds);
    }

    public override void ReadConfig(CfgPart cfg, SettingsPart part)
    {
      FirstDate = cfg.GetNullableDate("FirstDate");
      LastDate = cfg.GetNullableDate("LastDate");
      WalletIds = cfg.GetIntCommaString("Wallets");
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

      _MainPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      _MainPage.InitGrid += new EventHandler(MainPage_InitGrid);
      _MainPage.ShowToolBar = true;
      _MainPage.GridProducer = CreateMainPageGridProducer();
      Pages.Add(_MainPage);
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
      DataTable table = new DataTable();
      table.Columns.Add("Id", typeof(Int32));
      table.Columns.Add("Date", typeof(DateTime));
      table.Columns.Add("OpType", typeof(int));
      table.Columns.Add("InitialBalance", typeof(decimal));
      table.Columns.Add("DisplayName", typeof(string));
      table.Columns.Add("TotalDebt", typeof(decimal));
      table.Columns.Add("TotalCredit", typeof(decimal));
      table.Columns.Add("FinalBalance", typeof(decimal));
      table.Columns.Add("BalanceConfirmed", typeof(bool));
      table.Columns.Add("RecType", typeof(int)); // 0 - операция, 1 - итоги по кошелькам 2 - общий итог

      decimal[] walletInitBalance = new decimal[Params.WalletIds.Length];

      #region Начальное сальдо

      for (int i = 0; i < Params.WalletIds.Length; i++)
      {
        List<DBxFilter> filters = new List<DBxFilter>();
        DBxSelectInfo si1 = new DBxSelectInfo();
        si1.TableName = "Operations";
        si1.Expressions.Add(new DBxAggregateFunction(DBxAggregateFunctionKind.Sum, "TotalDebt"), "SumDebt");
        filters.Add(new ValueFilter("Date", Params.FirstDate.Value, CompareKind.LessThan));
        filters.Add(new ValueFilter("WalletDebt", Params.WalletIds[i]));
        filters.Add(DBSDocType.DeletedFalseFilter);
        si1.Where = AndFilter.FromList(filters);
        decimal SumDebt = DataTools.GetDecimal(ProgramDBUI.TheUI.DocProvider.FillSelect(si1).Rows[0][0]);

        DBxSelectInfo si2 = new DBxSelectInfo();
        si2.TableName = "Operations";
        si2.Expressions.Add(new DBxAggregateFunction(DBxAggregateFunctionKind.Sum, "TotalCredit"), "SumCredit");
        filters.Clear();
        filters.Add(new ValueFilter("Date", Params.FirstDate.Value, CompareKind.LessThan));
        filters.Add(new ValueFilter("WalletCredit", Params.WalletIds[i]));
        filters.Add(DBSDocType.DeletedFalseFilter);
        si2.Where = AndFilter.FromList(filters);
        decimal SumCredit = DataTools.GetDecimal(ProgramDBUI.TheUI.DocProvider.FillSelect(si2).Rows[0][0]);

        walletInitBalance[i] = SumDebt - SumCredit;
      }

      decimal[] walletCurrBalance = (decimal[])(walletInitBalance.Clone());
      decimal totalCurrBalance = DataTools.SumDecimal(walletCurrBalance);
      decimal[] walletDebt = new decimal[Params.WalletIds.Length];
      decimal[] walletCredit = new decimal[Params.WalletIds.Length];


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


        DataRow resRow = table.NewRow();

        DataTools.CopyRowValues(srcRow, resRow, true);
        if (pDebtWallet < 0)
          resRow["TotalDebt"] = DBNull.Value;
        if (pCreditWallet < 0)
          resRow["TotalCredit"] = DBNull.Value;

        resRow["InitialBalance"] = totalCurrBalance;
        if (pDebtWallet >= 0)
        {
          walletCurrBalance[pDebtWallet] += DataTools.GetDecimal(srcRow, "TotalDebt");
          walletDebt[pDebtWallet] += DataTools.GetDecimal(srcRow, "TotalDebt");
          totalCurrBalance += DataTools.GetDecimal(srcRow, "TotalDebt");
        }
        if (pCreditWallet >= 0)
        {
          walletCurrBalance[pCreditWallet] -= DataTools.GetDecimal(srcRow, "TotalCredit");
          walletCredit[pCreditWallet] += DataTools.GetDecimal(srcRow, "TotalCredit");
          totalCurrBalance -= DataTools.GetDecimal(srcRow, "TotalCredit");
        }
        resRow["FinalBalance"] = totalCurrBalance;

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
          if (s == walletCurrBalance[pDebtWallet])
          {
            resRow["BalanceConfirmed"] = true;
            sb.Append("Подтвержден");
          }
          else
          {
            resRow["BalanceConfirmed"] = false;
            sb.Append("Не подтвержден. Реальный остаток: ");
            sb.Append(walletCurrBalance[pDebtWallet].ToString(Tools.MoneyFormat));
          }
          resRow["DisplayName"] = sb.ToString();
        }

        table.Rows.Add(resRow);
      }

      #endregion

      #region Конечное сальдо по кошелькам

      if (Params.WalletIds.Length > 1)
      {
        for (int i = 0; i < Params.WalletIds.Length; i++)
        {
          DataRow walletRow = table.NewRow();
          walletRow["DisplayName"] = ProgramDBUI.TheUI.DocTypes["Wallets"].GetTextValue(Params.WalletIds[i]);
          walletRow["InitialBalance"] = walletInitBalance[i];
          walletRow["TotalDebt"] = walletDebt[i];
          walletRow["TotalCredit"] = walletCredit[i];
          walletRow["FinalBalance"] = walletCurrBalance[i];
          walletRow["RecType"] = 1;
          table.Rows.Add(walletRow);
        }
      }

      #endregion

      #region Итоговая строка

      DataRow totalRow = table.NewRow();
      totalRow["DisplayName"] = "Итого";
      totalRow["InitialBalance"] = DataTools.SumDecimal(walletInitBalance);
      totalRow["TotalDebt"] = DataTools.SumDecimal(walletDebt);
      totalRow["TotalCredit"] = DataTools.SumDecimal(walletCredit);
      totalRow["FinalBalance"] = DataTools.SumDecimal(walletCurrBalance);
      totalRow["RecType"] = 2;
      table.Rows.Add(totalRow);

      #endregion

      _MainPage.DataSource = table.DefaultView;
    }

    #endregion

    #region Страница отчета

    EFPReportDBxGridPage _MainPage;

    private EFPGridProducer CreateMainPageGridProducer()
    {
      EFPDBxGridProducer producer = new EFPDBxGridProducer(ProgramDBUI.TheUI);
      producer.Columns.AddDate("Date", "Дата");
      producer.Columns.AddMoney("InitialBalance", "Начальное сальдо");
      producer.Columns.LastAdded.Format = Tools.MoneyFormat;
      producer.Columns.AddRefDocImage("Id", ProgramDBUI.TheUI.DocTypes["Operations"]);
      producer.Columns.AddText("DisplayName", "Содержание операции", 20, 15);
      producer.Columns.AddMoney("TotalDebt", "Дебет");
      producer.Columns.LastAdded.Format = Tools.MoneyFormat;
      producer.Columns.AddMoney("TotalCredit", "Кредит");
      producer.Columns.LastAdded.Format = Tools.MoneyFormat;
      producer.Columns.AddMoney("FinalBalance", "Конечное сальдо");
      producer.Columns.LastAdded.Format = Tools.MoneyFormat;

      producer.NewDefaultConfig(false);
      producer.DefaultConfig.Columns.Add("Date");
      producer.DefaultConfig.Columns.Add("InitialBalance");
      producer.DefaultConfig.Columns.Add("Id_Image");
      producer.DefaultConfig.Columns.AddFill("DisplayName");
      producer.DefaultConfig.Columns.Add("TotalDebt");
      producer.DefaultConfig.Columns.Add("TotalCredit");
      producer.DefaultConfig.Columns.Add("FinalBalance");

      return producer;
    }

    void MainPage_InitGrid(object sender, EventArgs args)
    {
      _MainPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(MainPage_GetRowAttributes);
      _MainPage.ControlProvider.GetCellAttributes += new EFPDataGridViewCellAttributesEventHandler(MainPage_GetCellAttributes);

      _MainPage.ControlProvider.ReadOnly = false;
      _MainPage.ControlProvider.Control.ReadOnly = true;
      _MainPage.ControlProvider.CanInsert = false;
      _MainPage.ControlProvider.CanDelete = false;
      _MainPage.ControlProvider.EditData += new EventHandler(MainPage_EditData);

      _MainPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(MainPage_GetDocSel);
    }

    void MainPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
    {
      int recType = DataTools.GetInt(args.DataRow, "RecType");
      switch (recType)
      {
        case 1: args.ColorType = EFPDataGridViewColorType.Total1; break;
        case 2: args.ColorType = EFPDataGridViewColorType.TotalRow; break;
      }
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
          if (DataTools.GetInt(args.DataRow, "RecType") == 0 &&
            DataTools.GetEnum<OperationType>(args.DataRow, "OpType") == OperationType.Balance)
          {
            if (!DataTools.GetBool(args.DataRow, "BalanceConfirmed"))
              args.ColorType = EFPDataGridViewColorType.Error;
          }
          break;
        case "Id_Image":
          switch (DataTools.GetInt(args.DataRow, "RecType"))
          {
            case 0:
              if (DataTools.GetEnum<OperationType>(args.DataRow, "OpType") == OperationType.Balance)
              {
                if (DataTools.GetBool(args.DataRow, "BalanceConfirmed"))
                {
                  args.Value = EFPApp.MainImages.Images["Ok"];
                  args.ToolTipText = "Баланс совпадает";
                }
                else
                {
                  args.Value = EFPApp.MainImages.Images["Error"];
                  args.ToolTipText = "Баланс не совпадает";
                }
              }
              break;
            case 1:
              args.Value = EFPApp.MainImages.Images["Wallet"];
              break;
            case 2:
              args.Value = EFPApp.MainImages.Images["Sum"];
              break;
          }
          break;
      }
    }

    void MainPage_EditData(object sender, EventArgs args)
    {
      Int32[] docIds = DataTools.GetIdsFromColumn(_MainPage.ControlProvider.SelectedDataRows, "Id");
      if (docIds.Length == 0)
        EFPApp.ShowTempMessage("Нет выбранных операций");
      ProgramDBUI.TheUI.DocTypes["Operations"].PerformEditing(docIds, _MainPage.ControlProvider.State, false);
    }

    void MainPage_GetDocSel(object sender, EFPDBxGridViewDocSelEventArgs args)
    {
      args.AddFromColumn("Operations", "Id");
    }

    #endregion
  }
}