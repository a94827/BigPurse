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
    #region ����������� �����

    public EditOperationProduct()
    {
      InitializeComponent();
    }

    #endregion

    #region ��������

    public static void InitSubDocEditForm(object sender, InitSubDocEditFormEventArgs args)
    {
      EditOperationProduct form = new EditOperationProduct();
      form._Editor = args.Editor;
      form.AddPage(args);
      args.Editor.AfterWrite += new SubDocEditEventHandler(form.Editor_AfterWrite);
    }

    private SubDocumentEditor _Editor;

    EFPDocComboBox efpProduct;
    EFPTextComboBox efpDescription, efpMU1, efpUnit2;
    DocValueTextBox dvDescription, dvUnit1, dvUnit2;
    EFPSingleEditBox efpQuantity1, efpQuantity2;
    DocValueSingleEditBox dvQuantity1, dvQuantity2;
    EFPTextBox efpFormula;
    EFPDecimalEditBox efpSum;
    DocValueDecimalEditBox dvSum;

    private void AddPage(InitSubDocEditFormEventArgs args)
    {
      DocEditPage page = args.AddPage("�����", MainPanel);

      #region ������� � ��������

      efpProduct = new EFPDocComboBox(page.BaseProvider, cbProduct, ProgramDBUI.TheUI.DocTypes["Products"]);
      efpProduct.CanBeEmpty = false;
      args.AddRef(efpProduct, "Product", false);
      efpProduct.DocIdEx.ValueChanged += new EventHandler(efpProduct_ValueChanged);

      cbDescription.Enter += new EventHandler(cbDescription_Enter);
      cbDescription.AutoCompleteMode = AutoCompleteMode.Suggest;
      cbDescription.AutoCompleteSource = AutoCompleteSource.CustomSource;
      efpDescription = new EFPTextComboBox(page.BaseProvider, cbDescription);
      efpDescription.CanBeEmpty = true;
      dvDescription = args.AddText(efpDescription, "Description", false);

      #endregion

      #region ���������� � ������� ���������

      efpQuantity1 = new EFPSingleEditBox(page.BaseProvider, edQuantity1);
      efpQuantity1.CanBeEmpty = true;
      dvQuantity1 = args.AddSingle(efpQuantity1, "Quantity1", false);

      cbMU1.Enter += new EventHandler(cbUnit1_Enter);
      cbMU1.AutoCompleteMode = AutoCompleteMode.Suggest;
      cbMU1.AutoCompleteSource = AutoCompleteSource.CustomSource;
      efpMU1 = new EFPTextComboBox(page.BaseProvider, cbMU1);
      efpMU1.CanBeEmpty = true;
      efpMU1.DisplayName = "��. ���. 1";
      dvUnit1 = args.AddText(efpMU1, "Unit1", false);
      SetQuantityAndUnitValidation(efpQuantity1, efpMU1);


      efpQuantity2 = new EFPSingleEditBox(page.BaseProvider, edQuantity2);
      efpQuantity2.CanBeEmpty = true;
      dvQuantity2 = args.AddSingle(efpQuantity2, "Quantity2", false);

      cbMU2.Enter += new EventHandler(cbUnit2_Enter);
      cbMU2.AutoCompleteMode = AutoCompleteMode.Suggest;
      cbMU2.AutoCompleteSource = AutoCompleteSource.CustomSource;
      efpUnit2 = new EFPTextComboBox(page.BaseProvider, cbMU2);
      efpUnit2.CanBeEmpty = true;
      efpUnit2.DisplayName = "��. ���. 2";
      dvUnit2 = args.AddText(efpUnit2, "Unit2", false);
      SetQuantityAndUnitValidation(efpQuantity2, efpUnit2);

      efpQuantity2.Validators.AddError(new DepEqual<float>(efpQuantity2.ValueEx, 0f),
        "������ �������� ������ ���������� ��� �������",
        new DepEqual<float>(efpQuantity1.ValueEx, 0f));

      #endregion

      #region ������� � �����

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

      efpSum.Validators.AddWarning(new DepNot(new DepEqual<decimal>(efpSum.ValueEx, 0m)), "����� ������ ���� ������");

      //if (!args.Editor.IsReadOnly)
      //{ 
      //  efpFormula.TextEx.ValueChanged+=new EventHandler(efpFormula_ValueChanged);
      //}

      #endregion

      #region �����������

      EFPTextBox efpComment = new EFPTextBox(page.BaseProvider, edComment);
      efpComment.CanBeEmpty = true;
      args.AddText(efpComment, "Comment", true);

      #endregion
    }

    private void SetQuantityAndUnitValidation(EFPSingleEditBox efpQuantity, EFPTextComboBox efpUnit)
    {
      efpUnit.Validating += new FreeLibSet.UICore.UIValidatingEventHandler(efpUnit_Validating); // � ������ �������

      DepValue<bool> isNZ = new DepNot(new DepEqual<float>(efpQuantity.ValueEx, 0f));
      efpQuantity.Validators.AddError(isNZ,
        "���������� ������ ���� ������, ���� ������ ������� ���������",
        efpUnit.IsNotEmptyEx);
      efpUnit.Validators.AddError(efpUnit.IsNotEmptyEx, "������ ���� ������ ������� ���������", isNZ);
    }

    void efpUnit_Validating(object sender, UIValidatingEventArgs args)
    {
      if (args.ValidateState == UIValidateState.Error)
        return;

      EFPTextComboBox efpUnit = (EFPTextComboBox)sender;

      ProductBuffer.ValidateProductValue(efpProduct.DocId, Object.ReferenceEquals(efpUnit, efpMU1) ? "Unit1" : "Unit2", efpUnit.Text, args);
    }

    #region ������ ��� ������ �������� ��������� �����

    /// <summary>
    /// ������ ������� ����������� ������� ������ ����� � ��������� ����
    /// </summary>
    private bool lvDescription, lvUnit1, lvUnit2;

    void efpProduct_ValueChanged(object sender, EventArgs args)
    {
      lvDescription = false;
      lvUnit1 = false;
      lvUnit2 = false;

      dvDescription.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Description");
      dvQuantity1.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Quantity1");
      dvUnit1.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Unit1");
      dvQuantity2.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Quantity2");
      dvUnit2.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Unit2");

      efpDescription.Validate();
      efpMU1.Validate();
      efpUnit2.Validate();
    }

    void cbDescription_Enter(object sender, EventArgs args)
    {
      Do_CB_Enter(cbDescription, "Description", ref lvDescription);
    }

    void cbUnit1_Enter(object sender, EventArgs args)
    {
      Do_CB_Enter(efpMU1, "Unit1", ref lvUnit1);
    }

    void cbUnit2_Enter(object sender, EventArgs args)
    {
      Do_CB_Enter(efpMU2, "Unit2", ref lvUnit2);
    }

    private static bool CB_Enter_ErrorLogged = false;

    private void Do_CB_Enter(EFPDocComboBox efpMU, string columnName, ref bool lvFlag)
    {
      Int32 productId = 0;
      try
      {
        if (lvFlag)
          return; // ��� ���������

        productId = efpProduct.DocId;
        string[] a = ProductBuffer.GetOpProductValues(productId, columnName);
        efpMU.Items.Clear();
        efpMU.Items.AddRange(a);
        efpMU.AutoCompleteCustomSource.AddRange(a);
        lvFlag = true;
      }
      catch (Exception e)
      {
        if (!CB_Enter_ErrorLogged)
        {
          LogoutTools.LogoutException(e, "������ �������� ������ �������� ��� ���� \"" + columnName + "\", ProductId= " + productId.ToString() + ". ��������� ������ �� ��������������");
          CB_Enter_ErrorLogged = true;
        }
        EFPApp.ShowTempMessage("�� ������� �������� ������ ��������");
      }
    }

    void Editor_AfterWrite(object sender, SubDocEditEventArgs args)
    {
      ProductBuffer.AddOpProductValues(efpProduct.DocId, "Description", efpDescription.Text);
      ProductBuffer.AddOpProductValues(efpProduct.DocId, "Unit1", efpMU1.Text);
      ProductBuffer.AddOpProductValues(efpProduct.DocId, "Unit2", efpUnit2.Text);
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

    #region ����������� ��� �������

    private static ParserList FormulaPL = CreateFormulaPL();

    private static ParserList CreateFormulaPL()
    {
      ParserList pl = new ParserList();

      MathOpParser opp = new MathOpParser(true); // TODO: ������ ������ ��������
      pl.Add(opp);

      // ������ ���� ����� ��������
      NumConstParser ncp = new NumConstParser();
      ncp.NumberFormat = StdConvert.NumberFormat;
      pl.Add(ncp);

      return pl;
    }

    #endregion
  }
}