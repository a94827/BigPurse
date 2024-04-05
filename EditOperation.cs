using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FreeLibSet.Forms;
using FreeLibSet.Forms.Docs;
using FreeLibSet.DependedValues;
using FreeLibSet.Data;
using FreeLibSet.Data.Docs;
using FreeLibSet.Core;

namespace App
{
  internal partial class EditOperation : Form
  {
    #region Конструктор формы

    public EditOperation()
    {
      InitializeComponent();
    }

    #endregion

    #region Табличный просмотр

    public static void ImageValueNeeded(object sender, DBxImageValueNeededEventArgs args)
    {
      OperationType opType = args.GetEnum<OperationType>("OpType");
      args.ImageKey = GetImageKey(opType);
    }

    public static readonly string[] ImageKeys = new string[] { 
      "OperationBalance",
      "OperationIncome",
      "OperationExpense",
      "OperationMove",
      "OperationDebt",
      "OperationCredit"};

    public static string GetImageKey(OperationType opType)
    {
      if ((int)opType < 0 || (int)opType >= ImageKeys.Length)
        return "Error";
      else
        return ImageKeys[(int)opType];
    }

    //public static void Total_ValueNeeded(object sender, EFPGridProducerValueNeededEventArgs args)
    //{
    //  for (int i = 0; i < 3; i++)
    //  {
    //    decimal s = args.GetDecimal(i);
    //    if (s != 0m)
    //    {
    //      args.Value = s;
    //      break;
    //    }
    //  }
    //}

    public static void Wallet_Text_ValueNeeded(object sender, EFPGridProducerValueNeededEventArgs args)
    {
      string s1 = ProgramDBUI.TheUI.DocTypes["Wallets"].GetTextValue(args.GetInt(0));
      string s2 = ProgramDBUI.TheUI.DocTypes["Wallets"].GetTextValue(args.GetInt(1));

      args.Value = DataTools.JoinNotEmptyStrings(" <- ", new string[] { s1, s2 });
    }

    public static void Contra_Text_ValueNeeded(object sender, EFPGridProducerValueNeededEventArgs args)
    {
      OperationType opType = args.GetEnum<OperationType>(0);
      switch (opType)
      {
        case OperationType.Income:
          args.Value = ProgramDBUI.TheUI.DocTypes["IncomeSources"].GetTextValue(args.GetInt(1));
          break;
        case OperationType.Expense:
          args.Value = ProgramDBUI.TheUI.DocTypes["Shops"].GetTextValue(args.GetInt(2));
          break;
        case OperationType.Debt:
        case OperationType.Credit:
          args.Value = ProgramDBUI.TheUI.DocTypes["Debtors"].GetTextValue(args.GetInt(3));
          break;
      }
    }

    /* ???
    private class WalletGridFilter : RefDocGridFilter
    {
      #region Конструктор

      public WalletGridFilter()
        :base(ProgramDBUI.TheUI, "Wallets", 
      { 
      }

      #endregion
    }
    */

    public static void InitView(object sender, InitEFPDBxViewEventArgs args)
    {
      #region Фильтры

      DateRangeGridFilter filtDate = new DateRangeGridFilter("Date");
      filtDate.DisplayName = "Дата операции";
      args.ControlProvider.Filters.Add(filtDate);

      EnumGridFilter filtOpType = new EnumGridFilter("OpType", Tools.OperationTypeNames);
      filtOpType.DisplayName = "Тип операции";
      filtOpType.ImageKeys = ImageKeys;
      args.ControlProvider.Filters.Add(filtOpType);

      DecimalRangeGridFilter filtSum = new DecimalRangeGridFilter("Total");
      filtSum.DisplayName = "Сумма операции";
      args.ControlProvider.Filters.Add(filtSum);

      #endregion
    }

    #endregion

    #region Редактор

    #region BeforeEdit

    public static void BeforeEditDoc(object sender, BeforeDocEditEventArgs args)
    {
      if (args.Editor.State == EFPDataGridViewState.Insert)
      {
        ListSelectDialog dlg = new ListSelectDialog();
        dlg.Title = "Создание операции";
        dlg.ImageKey = "Insert";
        dlg.Items = Tools.OperationTypeNames;
        dlg.ImageKeys = ImageKeys;
        try { dlg.SelectedIndex = args.Editor.MainValues["OpType"].AsInteger; }
        catch { }

        if (dlg.ShowDialog() != DialogResult.OK)
        {
          args.Cancel = true;
          return;
        }

        args.Editor.MainValues["OpType"].SetInteger(dlg.SelectedIndex);
        OperationType opType = (OperationType)(dlg.SelectedIndex);
        if (Tools.UseDebt(opType))
        {
          if (args.Editor.MainValues["WalletDebt"].IsNull)
            args.Editor.MainValues["WalletDebt"].SetValue(args.Editor.DocTypeUI.Columns["WalletCredit"].Value);
        }
        else
          args.Editor.MainValues["WalletDebt"].SetNull();
        if (Tools.UseCredit(opType))
        {
          if (args.Editor.MainValues["WalletCredit"].IsNull)
            args.Editor.MainValues["WalletCredit"].SetValue(args.Editor.DocTypeUI.Columns["WalletDebt"].Value);
        }
        else
          args.Editor.MainValues["WalletCredit"].SetNull();
      }
    }


    #endregion

    #region InitDocEditForm

    DocumentEditor _Editor;

    OperationType opType;

    const OperationType MixedOpType = (OperationType)(-1);

    public static void InitDocEditForm(object sender, InitDocEditFormEventArgs args)
    {
      EditOperation form = new EditOperation();
      form._Editor = args.Editor;

      if (args.Editor.MainValues["OpType"].Grayed)
        form.opType = MixedOpType;
      else
        form.opType = (OperationType)(args.Editor.MainValues["OpType"].AsInteger);

      form.AddPage1(args);
      if (form.opType == OperationType.Expense && (!args.Editor.MultiDocMode))
        form.AddPage2(args);
    }

    #endregion

    #region Страница 1 (Общие)

    EFPDateTimeBox efpDate;
    EFPIntEditBox efpOpOrder;
    EFPDocComboBox efpWalletDebt, efpWalletCredit, efpContra;
    EFPDecimalEditBox efpSumBefore, efpSumOp, efpSumAfter;

    private void AddPage1(InitDocEditFormEventArgs args)
    {
      DocEditPage page = args.AddPage("Общие", MainPanel1);
      if (opType == MixedOpType)
      {
        page.ImageKey = args.Editor.DocTypeUI.ImageKey;
      }
      else
      {
        page.ImageKey = GetImageKey(opType);
        grpOp.Text += " (" + Tools.ToString(opType) + ")";
      }

      #region Дата и порядок операции

      efpDate = new EFPDateTimeBox(page.BaseProvider, edDate);
      efpDate.CanBeEmpty = false;
      args.AddDate(efpDate, "Date", true);

      efpOpOrder = new EFPIntEditBox(page.BaseProvider, edOpOrder);
      efpOpOrder.CanBeEmpty = false;
      efpOpOrder.Minimum = 0;
      efpOpOrder.Maximum = Int16.MaxValue;
      args.AddInt(efpOpOrder, "OpOrder", true);

      #endregion

      #region Кошельки

      if (Tools.UseDebt(opType))
      {
        efpWalletDebt = new EFPDocComboBox(page.BaseProvider, cbWalletDebt, ProgramDBUI.TheUI.DocTypes["Wallets"]);
        efpWalletDebt.Label = lblWalletDebt;
        efpWalletDebt.DisplayName = "Кошелек - дебет";
        efpWalletDebt.CanBeEmpty = false;
        args.AddRef(efpWalletDebt, "WalletDebt", true);
      }
      else
      {
        cbWalletDebt.Visible = false;
        lblWalletDebt.Visible = false;
        if (opType != MixedOpType && (!_Editor.IsReadOnly))
          _Editor.MainValues["WalletDebt"].SetNull();
      }

      if (Tools.UseCredit(opType))
      {
        efpWalletCredit = new EFPDocComboBox(page.BaseProvider, cbWalletCredit, ProgramDBUI.TheUI.DocTypes["Wallets"]);
        efpWalletCredit.Label = lblWalletCredit;
        efpWalletCredit.DisplayName = "Кошелек - кредит";
        efpWalletCredit.CanBeEmpty = false;
        args.AddRef(efpWalletCredit, "WalletCredit", true);
      }
      else
      {
        cbWalletCredit.Visible = false;
        lblWalletCredit.Visible = false;
        if (opType != MixedOpType && (!_Editor.IsReadOnly))
          _Editor.MainValues["WalletCredit"].SetNull();
      }

      if (opType == OperationType.Move && (!args.Editor.IsReadOnly))
      {
        efpWalletDebt.Validating += new FreeLibSet.UICore.UIValidatingEventHandler(efpWallet_MoveValidating);
        efpWalletCredit.Validating += new FreeLibSet.UICore.UIValidatingEventHandler(efpWallet_MoveValidating);
        efpWalletDebt.DocIdEx.ValueChanged += efpWalletCredit.Validate;
        efpWalletCredit.DocIdEx.ValueChanged += efpWalletDebt.Validate;
      }

      #endregion

      #region Контрагент

      // Поле "Контрагент" используется для разных целей
      if (opType != MixedOpType)
      {
        if (opType == OperationType.Income)
        {
          lblContra.Text = "Источник";
          efpContra = new EFPDocComboBox(page.BaseProvider, cbContra, ProgramDBUI.TheUI.DocTypes["IncomeSources"]);
          efpContra.CanBeEmpty = false;
          args.AddRef(efpContra, "IncomeSource", true);
        }
        else if (!_Editor.IsReadOnly)
          _Editor.MainValues["IncomeSource"].SetNull();

        if (opType == OperationType.Expense)
        {
          lblContra.Text = "Магазин";
          efpContra = new EFPDocComboBox(page.BaseProvider, cbContra, ProgramDBUI.TheUI.DocTypes["Shops"]);
          efpContra.CanBeEmpty = true;
          args.AddRef(efpContra, "Shop", true);
        }
        else if (!_Editor.IsReadOnly)
          _Editor.MainValues["Shop"].SetNull();

        if (opType == OperationType.Debt || opType == OperationType.Credit)
        {
          lblContra.Text = opType == OperationType.Debt ? "Кредитор" : "Дебитор";
          efpContra = new EFPDocComboBox(page.BaseProvider, cbContra, ProgramDBUI.TheUI.DocTypes["Debtors"]);
          efpContra.CanBeEmpty = false;
          args.AddRef(efpContra, "Debtor", true);
        }
        else
        {
          if (!_Editor.IsReadOnly)
            _Editor.MainValues["Debtor"].SetNull();
        }
      }

      if (efpContra == null)
      {
        cbContra.Visible = false;
        lblContra.Visible = false;
      }

      #endregion

      #region Суммы

      if (opType == MixedOpType || _Editor.MultiDocMode)
        grpSum.Visible = false;
      else
      {
        efpSumBefore = new EFPDecimalEditBox(page.BaseProvider, edSumBefore);
        efpSumBefore.Control.Format = Tools.MoneyFormat;
        efpSumBefore.CanBeEmpty = true;
        efpSumBefore.ReadOnly = true;

        efpSumOp = new EFPDecimalEditBox(page.BaseProvider, edSumOp);
        efpSumOp.Control.Format = Tools.MoneyFormat;
        efpSumOp.CanBeEmpty = false;

        efpSumAfter = new EFPDecimalEditBox(page.BaseProvider, edSumAfter);
        efpSumAfter.Control.Format = Tools.MoneyFormat;
        efpSumAfter.CanBeEmpty = true;
        efpSumAfter.ReadOnly = true;

        if (opType == OperationType.Expense)
        {
          efpSumOp.ReadOnly = true;
          page.PageShow += new DocEditPageEventHandler(Page1_PageShow_CalcExpenseSum);
          if (!args.Editor.IsReadOnly)
            efpSumOp.ValueEx.ValueChanged += efpSumOp_ValueChanged_Expense;
        }
        else
        {
          args.AddDecimal(efpSumOp, "InlineSum", false);
        }
        efpSumOp.Validating += new FreeLibSet.UICore.UIValidatingEventHandler(efpSumOp_Validating);
        if (opType == OperationType.Balance)
        {
          efpSumOp.Label.Text = "Реальный остаток";
          efpSumAfter.Label.Text = "Расхождение";
        }

        efpSumOp.ValueEx.ValueChanged += new EventHandler(efpSumOp_ValueChanged);
      }

      #endregion

      #region Комментарий

      EFPTextBox efpComment = new EFPTextBox(page.BaseProvider, edComment);
      efpComment.CanBeEmpty = true;
      args.AddText(efpComment, "Comment", true);

      #endregion

      #region Расчет баланса

      if (efpSumBefore != null)
      {
        if (efpWalletDebt != null)
          efpWalletDebt.DocIdEx.ValueChanged += new EventHandler(StartCalcBalance);
        if (efpWalletCredit != null)
          efpWalletCredit.DocIdEx.ValueChanged += new EventHandler(StartCalcBalance);
        efpDate.NValueEx.ValueChanged += new EventHandler(StartCalcBalance);
        efpOpOrder.ValueEx.ValueChanged += new EventHandler(StartCalcBalance);
        _Editor.AfterWrite += StartCalcBalance;

        efpSumBefore.UseIdle = true;
        efpSumBefore.Idle += new EventHandler(efpSumBefore_Idle);
        efpSumBefore.Attached += new EventHandler(StartCalcBalance);
      }


      #endregion
    }

    void Page1_PageShow_CalcExpenseSum(object sender, DocEditPageEventArgs args)
    {
      efpSumOp.ReadOnly = true; // заранее отключаем чтобы не срабатывал обработчик ValueChanged
      efpSumOp.Value = DataTools.SumDecimal(sdgProducts.SourceAsDataView, "RecordSum");
      string productName;
      if ((!_Editor.IsReadOnly) && IsSingleProductSum(out productName))
      {
        efpSumOp.ReadOnly = false;
        efpSumOp.ToolTipText = "Сумма для товара \"" + productName + "\"";
      }
      else
        efpSumOp.ToolTipText = "Сумма по всем товарам";
    }

    /// <summary>
    /// Можно ли редактировать сумму на первой вкладке для одновременной записи.
    /// Требуется, чтобы в таблице "Товары" была ровно одна строка и не было формулы
    /// </summary>
    /// <param name="productName">Сюда помещается название товара, если редактирование возможно</param>
    /// <returns></returns>
    private bool IsSingleProductSum(out string productName)
    {
      productName = null;
      if (sdgProducts.SourceAsDataView.Count != 1)
        return false;
      DataRow row = sdgProducts.SourceAsDataView[0].Row;
      if (DataTools.GetString(row["Formula"]).Length > 0)
        return false;

      Int32 productId = DataTools.GetInt(row, "Product");
      productName = ProgramDBUI.TheUI.DocTypes["Products"].GetTextValue(productId);
      return true;
    }

    private void efpSumOp_ValueChanged_Expense(object sender, EventArgs args)
    {
      if (efpSumOp.ReadOnly)
        return;
      DataRow row = sdgProducts.SourceAsDataView[0].Row;
      if (DataTools.GetDecimal(row, "RecordSum") == efpSumOp.Value)
        return;
      DataTools.SetDecimal(row, "RecordSum", efpSumOp.Value);
      _Editor.SubDocsChangeInfo.Changed = true;
    }


    /// <summary>
    /// Проверка, что кошельки не совпадают для операции перемещения
    /// </summary>
    void efpWallet_MoveValidating(object sender, FreeLibSet.UICore.UIValidatingEventArgs args)
    {
      if (args.ValidateState == FreeLibSet.UICore.UIValidateState.Error)
        return;
      if (efpWalletDebt.DocId == efpWalletCredit.DocId)
        args.SetError("Для операции перемещения кошельки должны быть разными");
    }

    void efpSumOp_Validating(object sender, FreeLibSet.UICore.UIValidatingEventArgs args)
    {
      if (args.ValidateState != FreeLibSet.UICore.UIValidateState.Ok)
        return;
      if (efpSumOp.Value == 0m)
        args.SetWarning("Сумма должна быть задана");
    }

    #endregion

    #region Расчет баланса

    /// <summary>
    /// Текущий калькулятор баланса.
    /// </summary>
    BalanceCalc CurrCalc;

    private void StartCalcBalance(object sender, EventArgs args)
    {
      if (efpSumBefore.ProviderState != EFPControlProviderState.Attached)
        return;

      efpSumBefore.NValue = null;

      CurrCalc = new BalanceCalc();
      if (Tools.UseCredit(opType))
        CurrCalc.WalletId = efpWalletCredit.DocId;
      else
        CurrCalc.WalletId = efpWalletDebt.DocId;
      CurrCalc.Date = efpDate.NValue;
      CurrCalc.OpOrder = efpOpOrder.Value;
      CurrCalc.OpType = opType;
      CurrCalc.OperationId = _Editor.Documents[0][0].DocId;
      CurrCalc.Calculate();
    }

    void efpSumBefore_Idle(object sender, EventArgs args)
    {
      if (CurrCalc == null)
        return;
      if (CurrCalc.IsComplete)
      {
        efpSumBefore.NValue = CurrCalc.Balance;

        CurrCalc = null;

        efpSumOp_ValueChanged(null, null);
      }
    }


    void efpSumOp_ValueChanged(object sender, EventArgs e)
    {
      if (efpSumBefore.NValue.HasValue)
      {
        if (opType == OperationType.Balance)
          efpSumAfter.NValue = efpSumBefore.NValue.Value - efpSumOp.Value;
        else if (Tools.UseCredit(opType))
          efpSumAfter.NValue = efpSumBefore.NValue.Value - efpSumOp.Value;
        else
          efpSumAfter.NValue = efpSumBefore.NValue.Value + efpSumOp.Value;
      }
      else
        efpSumAfter.NValue = null;
    }

    #endregion

    #region Страница 2 (Товары)

    EFPSubDocGridView sdgProducts;

    /// <summary>
    /// Панель статусной строки "Всего"
    /// </summary>
    EFPCommandItem ciSBProductSum;

    private void AddPage2(InitDocEditFormEventArgs args)
    {
      bool oldCalcSums = EFPApp.ShowAutoCalcSums;
      try
      {
        EFPApp.ShowAutoCalcSums = false; // временно отключаем панели статусной строки
        DocEditPage page = args.AddSubDocsPage("OperationProducts", out sdgProducts);
        page.Title = "Товары";
        sdgProducts.ManualOrderColumn = "RecordOrder";

        ciSBProductSum = new EFPCommandItem("View", "Total");
        ciSBProductSum.Usage = EFPCommandItemUsage.StatusBar;
        ciSBProductSum.ImageKey = "Sum";
        ciSBProductSum.ToolTipText = "Общая сумма по чеку";
        ciSBProductSum.StatusBarText = "?";
        sdgProducts.CommandItems.Add(ciSBProductSum);

        sdgProducts.UseIdle = true;
        sdgProducts.Idle += new EventHandler(sdgProducts_Idle);
      }
      finally
      {
        EFPApp.ShowAutoCalcSums = oldCalcSums;
      }
    }

    void sdgProducts_Idle(object sender, EventArgs args)
    {
      decimal s = DataTools.SumDecimal(sdgProducts.SourceAsDataView, "RecordSum");
      ciSBProductSum.StatusBarText = s.ToString(Tools.MoneyFormat);
    }

    #endregion

    #endregion
  }
}