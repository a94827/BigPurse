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
    }

    private SubDocumentEditor _Editor;

    EFPDocComboBox efpProduct;
    EFPTextComboBox efpDescription, efpUnit;
    EFPIntEditBox efpQuantity;
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

      efpDescription = new EFPTextComboBox(page.BaseProvider, cbDescription);
      efpDescription.CanBeEmpty = true;
      args.AddText(efpDescription, "Description", false);

      #endregion

      #region Количество и единица измерения

      efpQuantity = new EFPIntEditBox(page.BaseProvider, edQuantity);
      efpQuantity.CanBeEmpty = true;
      args.AddInt(efpQuantity, "Quantity", false);

      efpUnit = new EFPTextComboBox(page.BaseProvider, cbUnit);
      efpUnit.CanBeEmpty = true;
      args.AddText(efpUnit, "Unit", false);

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

    private class DocValueFormula : DocValueTextBox
    {
      public DocValueFormula(DBxDocValue docValue, IEFPTextBox controlProvider)
        : base (docValue, controlProvider, false)
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