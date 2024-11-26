using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using FreeLibSet.Calendar;
using FreeLibSet.Config;
using FreeLibSet.Core;
using FreeLibSet.Data;
using FreeLibSet.Data.Docs;
using FreeLibSet.Forms;
using FreeLibSet.Forms.Docs;
using FreeLibSet.UICore;

namespace App
{
  internal class DebtReportParams : EFPReportParams
  {
    #region Конструктор

    public DebtReportParams()
    {
      LastDate = DateTime.Today;
    }

    #endregion

    #region Поля

    public DateTime LastDate;

    #endregion

    #region Переопределенные методы

    public override void WriteConfig(CfgPart cfg)
    {
      cfg.SetDate("LastDate", LastDate);
    }

    public override void ReadConfig(CfgPart cfg)
    {
      LastDate = cfg.GetNullableDate("LastDate") ?? DateTime.Today;
    }

    protected override void OnInitTitle()
    {
      base.Title = "Долги на " + DateRangeFormatter.Default.ToString(LastDate, false);
    }

    #endregion
  }

  /// <summary>
  /// Отчет по дебиторам и кредиторам.
  /// Сначала показывается только одна итоговая вкладка со списком дебиторов/кредиторов.
  /// Можно тыкать по строке для открытия дополнительных вкладок по одному дебитору/кредитору.
  /// </summary>
  internal class DebtReport:EFPReport
  {
    #region Конструктор

    public DebtReport()
      :base("DebtReport")
    {
      MainImageKey = "DebtReport";

      _MainPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      _MainPage.Title = "Итого";
      _MainPage.ImageKey = "Sum";
      _MainPage.InitGrid += new EventHandler(MainPage_InitGrid);
      _MainPage.ShowToolBar = true;
      _MainPage.ConfigSectionName = this.ConfigSectionName;
      _MainPage.GridProducer = CreateMainPageGridProducer();
      Pages.Add(_MainPage);
    }

    #endregion

    #region Запрос параметров

    public DebtReportParams Params { get { return (DebtReportParams)(base.ReportParams); } }

    protected override EFPReportParams CreateParams()
    {
      return new DebtReportParams();
    }

    protected override bool QueryParams()
    {
      DateTimeInputDialog dlg = new DateTimeInputDialog();
      dlg.Title = "Параметры отчета";
      dlg.Prompt = "Текущая дата";
      dlg.Kind = FreeLibSet.Formatting.EditableDateTimeFormatterKind.Date;
      dlg.CanBeEmpty = false;
      dlg.Value = Params.LastDate;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        Params.LastDate = dlg.Value;
        return true;
      }
      else
        return false;
    }

    #endregion

    #region Построение отчета

    private DataTable _TableOps;

    protected override void BuildReport()
    {
      #region Заготовка таблицы

      DataTable mainTable = new DataTable();
      mainTable.Columns.Add("RecType", typeof(int)); // 0 - запись по одному контрагента, 1 - итого
      mainTable.Columns.Add("Id", typeof(Int32));
      mainTable.Columns.Add("Debtor.Name", typeof(string));
      mainTable.Columns.Add("TotalDebt", typeof(decimal));
      mainTable.Columns.Add("TotalCredit", typeof(decimal));
      mainTable.Columns.Add("FirstDate", typeof(DateTime));
      mainTable.Columns.Add("LastDate", typeof(DateTime));
      DataTools.SetPrimaryKey(mainTable, "Id");
      mainTable.DefaultView.Sort = "RecType,Debtor.Name";

      #endregion

      #region Загрузка списка

      // Загружаем полный список из справочника
      DataTable tableDebtors = ProgramDBUI.TheUI.DocProvider.FillSelect("Debtors", new DBxColumns("Id,Name"), DBSDocType.DeletedFalseFilter);
      foreach (DataRow row in tableDebtors.Rows)
        mainTable.Rows.Add(0, row["Id"], row["Name"]);

      #endregion

      #region Просмотр операций

      _TableOps = ProgramDBUI.TheUI.DocProvider.FillSelect("Operations",
        new DBxColumns("Id,Date,OpType,OpOrder,DisplayName,WalletDebt,WalletCredit,TotalDebt,TotalCredit,Debtor,Comment"),
        new AndFilter(new ValuesFilter("OpType", new int[] { (int)OperationType.Debt, (int)OperationType.Credit }, DBxColumnType.Int),
        new AndFilter(new ValueFilter("Date", Params.LastDate, CompareKind.LessOrEqualThan),
          DBSDocType.DeletedFalseFilter)),
        DBxOrder.FromDataViewSort("Date,OpOrder,Id"));

      foreach (DataRow row in _TableOps.Rows)
      {
        Int32 debtorId = DataTools.GetInt(row, "Debtor");
        if (debtorId == 0)
          throw new BugException("debtorId=0");

        DataRow mainRow = mainTable.Rows.Find(debtorId);
        OperationType opType = DataTools.GetEnum<OperationType>(row, "OpType");
        if (DataTools.GetDecimal(mainRow, "TotalDebt") == 0m && DataTools.GetDecimal(mainRow, "TotalCredit") == 0m)
          mainRow["FirstDate"] = row["Date"];
        decimal sumOp = DataTools.GetDecimal(row, "TotalDebt") - DataTools.GetDecimal(row, "TotalCredit");
        if (opType == OperationType.Debt)
          DataTools.IncDecimal(mainRow, "TotalDebt", sumOp);
        else
          DataTools.IncDecimal(mainRow, "TotalCredit", -sumOp);

        bool isReturn = opType == OperationType.Debt ? (sumOp < 0) : (sumOp > 0);
        if (isReturn)
          mainRow["LastDate"] = row["Date"];
      }

      #endregion

      #region Итоговая строка

      DataRow totalRow = mainTable.NewRow();
      totalRow["RecType"] = 1;
      totalRow["Debtor.Name"] = "Итого";
      totalRow["Id"] = 0;
      DataTools.SumDecimal(totalRow, "TotalDebt");
      DataTools.SumDecimal(totalRow, "TotalCredit");
      mainTable.Rows.Add(totalRow);

      #endregion;

      _MainPage.DataSource = mainTable.DefaultView;
    }

    #endregion

    #region Основная страница отчета

    EFPReportDBxGridPage _MainPage;

    private EFPGridProducer CreateMainPageGridProducer()
    {
      EFPGridProducer producer = new EFPGridProducer();
      producer.Columns.AddText("Debtor.Name", "Дебитор / кредитор", 20, 10);
      producer.Columns.LastAdded.CanIncSearch = true;
      producer.Columns.AddMoney("TotalDebt", "Дебет");
      producer.Columns.LastAdded.Format = Tools.MoneyFormat;
      producer.Columns.AddMoney("TotalCredit", "Кредит");
      producer.Columns.LastAdded.Format = Tools.MoneyFormat;
      producer.Columns.AddDate("FirstDate", "Дата образования задолженности");
      producer.Columns.AddDate("LastDate", "Дата возврата средств");
      return producer;
    }

    private void MainPage_InitGrid(object sender, EventArgs args)
    {
      _MainPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(MainPage_GetRowAttributes);
      //_MainPage.ControlProvider.GetCellAttributes += new EFPDataGridViewCellAttributesEventHandler(MainPage_GetCellAttributes);

      _MainPage.ControlProvider.ReadOnly = false;
      _MainPage.ControlProvider.Control.ReadOnly = true;
      _MainPage.ControlProvider.CanInsert = false;
      _MainPage.ControlProvider.CanDelete = false;
      _MainPage.ControlProvider.CanView = true;
      _MainPage.ControlProvider.Control.MultiSelect = true;
      _MainPage.ControlProvider.CanMultiEdit = false;
      _MainPage.ControlProvider.EditData += new EventHandler(MainPage_EditData);

      _MainPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(MainPage_GetDocSel);
    }

    private void MainPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
    {
      if (DataTools.GetInt(args.DataRow, "RecType") == 1)
        args.ColorType = UIDataViewColorType.TotalRow;
      else if (DataTools.GetDecimal(args.DataRow, "TotalDebt") == 0m && DataTools.GetDecimal(args.DataRow, "TotalCredit") == 0m)
        args.Grayed = true;
    }

    private void MainPage_EditData(object sender, EventArgs args)
    {
      if (!_MainPage.ControlProvider.CheckSingleRow())
        return;
      Int32 debtorId = DataTools.GetInt(_MainPage.ControlProvider.CurrentDataRow, "Id");
      if (debtorId == 0)
      {
        EFPApp.ShowTempMessage("Должна быть выбрана строка с дебетором или кредитором");
        return;
      }

      string pageKey = "Debtor" + StdConvert.ToString(debtorId);
      if (base.Pages.FindAndActivate(pageKey))
        return;

      // Создаем новую вкладку
      EFPReportDBxGridPage debtorPage = new EFPReportDBxGridPage(ProgramDBUI.TheUI);
      debtorPage.ExtraPageKey = pageKey;
      debtorPage.Title = ProgramDBUI.TheUI.DocTypes["Debtors"].GetTextValue(debtorId);
      debtorPage.ImageKey= ProgramDBUI.TheUI.DocTypes["Debtors"].GetImageKey(debtorId);
      debtorPage.ConfigSectionName = "DebtReport_DebtorPage";
      debtorPage.GridProducer = CreateDebtorPageGridProducer();
      debtorPage.InitGrid += DebtorPage_InitGrid;

      DataTable table = CreateDebtorPageTable(debtorId);
      debtorPage.DataSource = table.DefaultView;

      Pages.Add(debtorPage);
    }

    private void MainPage_GetDocSel(object sender, EFPDBxGridViewDocSelEventArgs args)
    {
      args.AddFromColumn("Debtors", "Id");
    }

    #endregion

    #region Вкладка по дебитору / кредитору

    private DataTable CreateDebtorPageTable(Int32 debtorId)
    {
      DataTable table = _TableOps.Clone();
      table.Columns.Add("RecType", typeof(int)); // 0-строка операции, 1-итог
      table.Columns.Add("Wallet_Text", typeof(string));

      #region Перебор таблицы операций

      using (DataView dv = new DataView(_TableOps))
      {
        dv.RowFilter = new ValueFilter("Debtor", debtorId).ToString();
        foreach (DataRowView drv in dv)
        {
          DataRow resRow = table.Rows.Add(drv.Row.ItemArray);
          resRow["RecType"] = 0;
          Int32 walletId = DataTools.GetInt(drv.Row, "WalletDebt");
          if (walletId==0)
            walletId= DataTools.GetInt(drv.Row, "WalletCredit");
          resRow["Wallet_Text"] = ProgramDBUI.TheUI.DocTypes["Wallets"].GetTextValue(walletId);
        }
      }

      #endregion

      #region Итоговая строка

      DataRow totalRow = table.NewRow();
      totalRow["RecType"] = 1;
      totalRow["DisplayName"] = "Итого";
      totalRow["Id"] = 0;
      DataTools.SumDecimal(totalRow, "TotalDebt");
      DataTools.SumDecimal(totalRow, "TotalCredit");
      table.Rows.Add(totalRow);

      #endregion

      return table;
    }

    private EFPGridProducer CreateDebtorPageGridProducer()
    {
      EFPGridProducer producer = new EFPGridProducer();

      producer.Columns.AddDate("Date", "Дата");
      producer.Columns.AddInt("OpOrder", "П.", 3);
      producer.Columns.LastAdded.DisplayName = "Порядок операции в пределах даты";
      producer.Columns.AddText("DisplayName", "Содержание", 40, 30);
      producer.Columns.AddMoney("TotalDebt", "Сумма дебет");
      producer.Columns.LastAdded.Format = Tools.MoneyFormat;
      producer.Columns.AddMoney("TotalCredit", "Сумма кредит");
      producer.Columns.LastAdded.Format = Tools.MoneyFormat;
      producer.Columns.AddText("Wallet_Text", "Кошелек", 15, 10);
      producer.Columns.AddText("Comment", "Комментарий", 30, 10);

      producer.ToolTips.AddText("DisplayName", "Содержание");
      producer.ToolTips.AddText("Comment", String.Empty).DisplayName = "Комментарий (если задан)";

      producer.NewDefaultConfig(false);
      producer.DefaultConfig.Columns.Add("Date");
      producer.DefaultConfig.Columns.AddFill("DisplayName", 100);
      producer.DefaultConfig.Columns.Add("TotalDebt");
      producer.DefaultConfig.Columns.Add("TotalCredit");
      producer.DefaultConfig.ToolTips.Add("Comment");

      return producer;
    }

    private void DebtorPage_InitGrid(object sender, EventArgs args)
    {
      EFPReportDBxGridPage debtorPage = (EFPReportDBxGridPage)sender;

      debtorPage.ControlProvider.GetRowAttributes += new EFPDataGridViewRowAttributesEventHandler(DebtorPage_GetRowAttributes);
      //debtorPage.ControlProvider.GetCellAttributes += new EFPDataGridViewCellAttributesEventHandler(DebtorPage_GetCellAttributes);

      debtorPage.ControlProvider.ReadOnly = false;
      debtorPage.ControlProvider.Control.ReadOnly = true;
      debtorPage.ControlProvider.CanInsert = false;
      debtorPage.ControlProvider.CanDelete = false;
      debtorPage.ControlProvider.CanView = true;
      debtorPage.ControlProvider.Control.MultiSelect = true;
      debtorPage.ControlProvider.CanMultiEdit = ProgramDBUI.TheUI.DocTypes["Operations"].CanMultiEdit;
      debtorPage.ControlProvider.EditData += new EventHandler(DebtorPage_EditData);

      debtorPage.ControlProvider.GetDocSel += new EFPDBxGridViewDocSelEventHandler(DebtorPage_GetDocSel);
    }

    private void DebtorPage_GetRowAttributes(object sender, EFPDataGridViewRowAttributesEventArgs args)
    {
      if (DataTools.GetInt(args.DataRow, "RecType") == 1)
        args.ColorType = UIDataViewColorType.TotalRow;
    }

    private void DebtorPage_EditData(object sender, EventArgs args)
    {
      EFPDBxGridView controlProvider = (EFPDBxGridView)sender;
      ProgramDBUI.TheUI.DocTypes["Operations"].PerformEditing(controlProvider.SelectedIds, controlProvider.State, true);
    }

    private void DebtorPage_GetDocSel(object sender, EFPDBxGridViewDocSelEventArgs args)
    {
      args.AddFromColumn("Operations", "Id", true);
    }

    #endregion
  }
}
