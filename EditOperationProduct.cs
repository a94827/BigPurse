using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FreeLibSet.Forms.Docs;
using FreeLibSet.Forms;
using FreeLibSet.Parsing;
using FreeLibSet.Core;
using FreeLibSet.DependedValues;
using System.Globalization;
using FreeLibSet.Data.Docs;
using FreeLibSet.Logging;
using FreeLibSet.UICore;

namespace App
{
  internal partial class EditOperationProduct : Form
  {
    #region Конструктор формы

    public EditOperationProduct()
    {
      InitializeComponent();
    }

    #endregion

    #region Редактор

    public static void InitSubDocEditForm(object sender, InitSubDocEditFormEventArgs args)
    {
      EditOperationProduct form = new EditOperationProduct();
      form._Editor = args.Editor;
      form.AddPage(args);
      args.Editor.AfterWrite += new SubDocEditEventHandler(form.Editor_AfterWrite);
    }

    private SubDocumentEditor _Editor;

    EFPDocComboBox efpProduct;
    EFPTextComboBox efpDescription, efpUnit1, efpUnit2;
    EFPSingleEditBox efpQuantity1, efpQuantity2;
    EFPTextBox efpFormula;
    EFPDecimalEditBox efpSum;
    DocValueDecimalEditBox dvSum;

    private void AddPage(InitSubDocEditFormEventArgs args)
    {
      DocEditPage page = args.AddPage("Общие", MainPanel);

      #region Продукт и описание

      efpProduct = new EFPDocComboBox(page.BaseProvider, cbProduct, ProgramDBUI.TheUI.DocTypes["Products"]);
      efpProduct.CanBeEmpty = false;
      args.AddRef(efpProduct, "Product", false);
      efpProduct.DocIdEx.ValueChanged += new EventHandler(efpProduct_ValueChanged);

      cbDescription.Enter += new EventHandler(cbDescription_Enter);
      cbDescription.AutoCompleteMode = AutoCompleteMode.Suggest;
      cbDescription.AutoCompleteSource = AutoCompleteSource.CustomSource;
      efpDescription = new EFPTextComboBox(page.BaseProvider, cbDescription);
      efpDescription.CanBeEmpty = true;
      args.AddText(efpDescription, "Description", false);

      #endregion

      #region Количество и единица измерения

      efpQuantity1 = new EFPSingleEditBox(page.BaseProvider, edQuantity1);
      efpQuantity1.CanBeEmpty = true;
      args.AddSingle(efpQuantity1, "Quantity1", false);

      cbUnit1.Enter += new EventHandler(cbUnit1_Enter);
      cbUnit1.AutoCompleteMode = AutoCompleteMode.Suggest;
      cbUnit1.AutoCompleteSource = AutoCompleteSource.CustomSource;
      efpUnit1 = new EFPTextComboBox(page.BaseProvider, cbUnit1);
      efpUnit1.CanBeEmpty = true;
      args.AddText(efpUnit1, "Unit1", false);
      SetQuantityAndUnitValidation(efpQuantity1, efpUnit1);


      cbUnit2.Enter += new EventHandler(cbUnit2_Enter);
      cbUnit2.AutoCompleteMode = AutoCompleteMode.Suggest;
      cbUnit2.AutoCompleteSource = AutoCompleteSource.CustomSource;
      efpQuantity2 = new EFPSingleEditBox(page.BaseProvider, edQuantity2);
      efpQuantity2.CanBeEmpty = true;
      args.AddSingle(efpQuantity2, "Quantity2", false);

      efpUnit2 = new EFPTextComboBox(page.BaseProvider, cbUnit2);
      efpUnit2.CanBeEmpty = true;
      args.AddText(efpUnit2, "Unit2", false);
      SetQuantityAndUnitValidation(efpQuantity2, efpUnit2);

      efpQuantity2.Validators.AddError(new DepEqual<float>(efpQuantity2.ValueEx, 0f),
        "Нельзя задавать второе количество без первого",
        new DepEqual<float>(efpQuantity1.ValueEx, 0f));

      #endregion

      #region Формула и сумма

      efpFormula = new EFPTextBox(page.BaseProvider, edFormula);
      efpFormula.CanBeEmpty = true;
      efpFormula.Validating += new FreeLibSet.UICore.UIValidatingEventHandler(efpFormula_Validating);
      DocValueFormula dvFormula = new DocValueFormula(args.Values["Formula"], efpFormula);
      args.AddDocEditItem(dvFormula);

      efpSum = new EFPDecimalEditBox(page.BaseProvider, edSum);
      efpSum.CanBeEmpty = false;
      efpSum.Control.Format = Tools.MoneyFormat;
      dvSum = args.AddDecimal(efpSum, "RecordSum", false);
      dvSum.UserDisabledMode = DocValueUserDisabledMode.AlwaysReplace;
      dvSum.UserEnabledEx = new DepNot(efpFormula.IsNotEmptyEx);

      efpSum.Validators.AddWarning(new DepNot(new DepEqual<decimal>(efpSum.ValueEx, 0m)), "Сумма должна быть задана");

      //if (!args.Editor.IsReadOnly)
      //{ 
      //  efpFormula.TextEx.ValueChanged+=new EventHandler(efpFormula_ValueChanged);
      //}

      #endregion

      #region Комментарий

      EFPTextBox efpComment = new EFPTextBox(page.BaseProvider, edComment);
      efpComment.CanBeEmpty = true;
      args.AddText(efpComment, "Comment", true);

      #endregion
    }

    private void SetQuantityAndUnitValidation(EFPSingleEditBox efpQuantity, EFPTextComboBox efpUnit)
    {
      efpUnit.Validating += new FreeLibSet.UICore.UIValidatingEventHandler(efpUnit_Validating); // в первую очередь

      DepValue<bool> isNZ = new DepNot(new DepEqual<float>(efpQuantity.ValueEx, 0f));
      efpQuantity.Validators.AddError(isNZ,
        "Количество должно быть задано, если задана единица измерения",
        efpUnit.IsNotEmptyEx);
      efpUnit.Validators.AddError(efpUnit.IsNotEmptyEx, "Должна быть задана единица измерения", isNZ);
    }

    void efpUnit_Validating(object sender, UIValidatingEventArgs args)
    {
      if (args.ValidateState == UIValidateState.Error)
        return;

      EFPTextComboBox efpUnit = (EFPTextComboBox)sender;

      string errorText;
      if (!Tools.IsValidUnit(efpUnit.Text, out errorText))
        args.SetError(errorText);
    }

    #region Списки для выбора значений текстовых полей

    /// <summary>
    /// Флажки наличия загуженнных списков выбора строк в текстовые поля
    /// </summary>
    private bool lvDescription, lvUnit1, lvUnit2;

    void efpProduct_ValueChanged(object sender, EventArgs args)
    {
      lvDescription = false;
      lvUnit1 = false;
      lvUnit2 = false;
    }

    void cbDescription_Enter(object sender, EventArgs args)
    {
      Do_CB_Enter(cbDescription, "Description", ref lvDescription);
    }

    void cbUnit1_Enter(object sender, EventArgs args)
    {
      Do_CB_Enter(cbUnit1, "Unit1", ref lvUnit1);
    }

    void cbUnit2_Enter(object sender, EventArgs args)
    {
      Do_CB_Enter(cbUnit2, "Unit2", ref lvUnit2);
    }

    private static bool CB_Enter_ErrorLogged = false;

    private void Do_CB_Enter(ComboBox control, string columnName, ref bool lvFlag)
    {
      Int32 productId = 0;
      try
      {
        if (lvFlag)
          return; // Уже загружено

        productId = efpProduct.DocId;
        string[] a = ProgramValueBuffer.GetOpProductValues(productId, columnName);
        control.Items.Clear();
        control.Items.AddRange(a);
        control.AutoCompleteCustomSource.AddRange(a);
        lvFlag = true;
      }
      catch (Exception e)
      {
        if (!CB_Enter_ErrorLogged)
        {
          LogoutTools.LogoutException(e, "Ошибка загрузки списка значений для поля \"" + columnName + "\", ProductId= " + productId.ToString() + ". Повторные ошибки не регистрируются");
          CB_Enter_ErrorLogged = true;
        }
        EFPApp.ShowTempMessage("Не удалось получить список значений");
      }
    }

    void Editor_AfterWrite(object sender, SubDocEditEventArgs args)
    {
      ProgramValueBuffer.AddOpProductValues(efpProduct.DocId, "Description", efpDescription.Text);
      ProgramValueBuffer.AddOpProductValues(efpProduct.DocId, "Unit1", efpUnit1.Text);
      ProgramValueBuffer.AddOpProductValues(efpProduct.DocId, "Unit2", efpUnit2.Text);
    }

    #endregion

    private class DocValueFormula : DocValueTextBox
    {
      public DocValueFormula(DBxDocValue docValue, IEFPTextBox controlProvider)
        : base(docValue, controlProvider, false)
      {
      }

      protected override void ValueToControl()
      {
        CurrentValueEx.Value = DocValue.AsString.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
      }

      protected override void ValueFromControl()
      {
        DocValue.SetString(CurrentValueEx.Value.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, "."));
      }
    }

    void efpFormula_Validating(object sender, FreeLibSet.UICore.UIValidatingEventArgs args)
    {
      if (args.ValidateState == FreeLibSet.UICore.UIValidateState.Error)
        return;

      if (efpFormula.Text.Length == 0)
        return;

      decimal? sRes = null;

      try
      {
        ParsingData pd = new ParsingData(efpFormula.Text.Replace(',', '.'));
        FormulaPL.Parse(pd);
        if (pd.FirstErrorToken != null)
          args.SetError(pd.FirstErrorToken.ErrorMessage.Value.Text);
        else
        {
          IExpression expr = FormulaPL.CreateExpression(pd);
          if (pd.FirstErrorToken != null)
            args.SetError(pd.FirstErrorToken.ErrorMessage.Value.Text);
          else
          {
            sRes = DataTools.GetDecimal(expr.Calc());
            sRes = Math.Round(sRes.Value, 2, MidpointRounding.AwayFromZero);
          }
        }
      }
      catch (Exception e)
      {
        args.SetError(e.Message);
      }
      efpSum.NValue = sRes;
      dvSum.UserDisabledValue = sRes;
    }


    #endregion

    #region Вычислитель для формулы

    private static ParserList FormulaPL = CreateFormulaPL();

    private static ParserList CreateFormulaPL()
    {
      ParserList pl = new ParserList();

      MathOpParser opp = new MathOpParser(true); // TODO: Убрать лишние операции
      pl.Add(opp);

      // должно быть после операций
      NumConstParser ncp = new NumConstParser();
      ncp.NumberFormat = StdConvert.NumberFormat;
      pl.Add(ncp);

      return pl;
    }

    #endregion
  }
}