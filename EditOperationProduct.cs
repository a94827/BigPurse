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
using FreeLibSet.Data;

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
    EFPTextComboBox efpDescription;
    EFPDocComboBox efpMU1, efpMU2, efpMU3;
    DocValueTextBox dvDescription;
    DocValueDocComboBox dvMU1, dvMU2, dvMU3;
    EFPSingleEditBox efpQuantity1, efpQuantity2, efpQuantity3;
    DocValueSingleEditBox dvQuantity1, dvQuantity2, dvQuantity3;
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
      dvDescription = args.AddText(efpDescription, "Description", false);

      #endregion

      #region Количество и единица измерения

      efpQuantity1 = new EFPSingleEditBox(page.BaseProvider, edQuantity1);
      efpQuantity1.CanBeEmpty = true;
      dvQuantity1 = args.AddSingle(efpQuantity1, "Quantity1", false);

      cbMU1.Enter += new EventHandler(cbMU1_Enter);
      efpMU1 = new EFPDocComboBox(page.BaseProvider, cbMU1, ProgramDBUI.TheUI.DocTypes["MUs"]);
      efpMU1.CanBeEmpty = true;
      efpMU1.DisplayName = "Ед. изм. 1";
      dvMU1 = args.AddRef(efpMU1, "MU1", false);
      SetQuantityAndUnitValidation(efpQuantity1, efpMU1);


      efpQuantity2 = new EFPSingleEditBox(page.BaseProvider, edQuantity2);
      efpQuantity2.CanBeEmpty = true;
      dvQuantity2 = args.AddSingle(efpQuantity2, "Quantity2", false);

      cbMU2.Enter += new EventHandler(cbMU2_Enter);
      efpMU2 = new EFPDocComboBox(page.BaseProvider, cbMU2, ProgramDBUI.TheUI.DocTypes["MUs"]);
      efpMU2.CanBeEmpty = true;
      efpMU2.DisplayName = "Ед. изм. 2";
      dvMU2 = args.AddRef(efpMU2, "MU2", false);
      SetQuantityAndUnitValidation(efpQuantity2, efpMU2);


      efpQuantity3 = new EFPSingleEditBox(page.BaseProvider, edQuantity3);
      efpQuantity3.CanBeEmpty = true;
      dvQuantity3 = args.AddSingle(efpQuantity3, "Quantity3", false);

      cbMU3.Enter += new EventHandler(cbMU3_Enter);
      efpMU3 = new EFPDocComboBox(page.BaseProvider, cbMU3, ProgramDBUI.TheUI.DocTypes["MUs"]);
      efpMU3.CanBeEmpty = true;
      efpMU3.DisplayName = "Ед. изм. 3";
      dvMU3 = args.AddRef(efpMU3, "MU3", false);
      SetQuantityAndUnitValidation(efpQuantity3, efpMU3);

      efpQuantity3.Validators.AddError(new DepEqual<float>(efpQuantity3.ValueEx, 0f),
        "Нельзя задавать третье количество без второго",
        new DepEqual<float>(efpQuantity2.ValueEx, 0f));

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

    private void SetQuantityAndUnitValidation(EFPSingleEditBox efpQuantity, EFPDocComboBox efpMU)
    {
      //efpMU.Validating += new FreeLibSet.UICore.UIValidatingEventHandler(efpUnit_Validating); // в первую очередь

      DepValue<bool> isNZ = new DepNot(new DepEqual<float>(efpQuantity.ValueEx, 0f));
      efpQuantity.Validators.AddError(isNZ,
        "Количество должно быть задано, если задана единица измерения",
        efpMU.IsNotEmptyEx);
      efpMU.Validators.AddError(efpMU.IsNotEmptyEx, "Должна быть задана единица измерения", isNZ);
    }

    #region Списки для выбора значений текстовых полей

    /// <summary>
    /// Флажки наличия загуженнных списков выбора строк в текстовые поля
    /// </summary>
    private bool lvDescription;

    void efpProduct_ValueChanged(object sender, EventArgs args)
    {
      lvDescription = false;

      dvDescription.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Description");
      dvQuantity1.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Quantity1");
      dvMU1.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "MU1");
      dvQuantity2.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Quantity2");
      dvMU2.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "MU2");
      dvQuantity3.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Quantity3");
      dvMU3.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "MU3");

      efpDescription.Validate();
      efpMU1.Validate();
      efpMU2.Validate();
      efpMU3.Validate();
    }

    void cbDescription_Enter(object sender, EventArgs args)
    {
      Int32 productId = 0;
      try
      {
        if (lvDescription)
          return; // Уже загружено

        productId = efpProduct.DocId;
        string[] a = ProductBuffer.GetOpProductValues(productId, "Description");
        efpDescription.Control.Items.Clear();
        efpDescription.Control.Items.AddRange(a);
        efpDescription.Control.AutoCompleteCustomSource.AddRange(a);
        lvDescription = true;
      }
      catch (Exception e)
      {
        if (!CB_Enter_ErrorLogged)
        {
          LogoutTools.LogoutException(e, "Ошибка загрузки списка значений для поля \"Description\", ProductId= " + productId.ToString() + ". Повторные ошибки не регистрируются");
          CB_Enter_ErrorLogged = true;
        }
        EFPApp.ShowTempMessage("Не удалось получить список значений");
      }
    }

    void cbMU1_Enter(object sender, EventArgs args)
    {
      Do_CB_Enter(efpMU1, 1);
    }

    void cbMU2_Enter(object sender, EventArgs args)
    {
      Do_CB_Enter(efpMU2, 2);
    }

    void cbMU3_Enter(object sender, EventArgs args)
    {
      Do_CB_Enter(efpMU3, 3);
    }

    private static bool CB_Enter_ErrorLogged = false;

    private void Do_CB_Enter(EFPDocComboBox efpMU, int nMU)
    {
      Int32 productId = 0;
      try
      {
        productId = efpProduct.DocId;
        ProductBuffer.ProductData pd = ProductBuffer.GetProductData(productId);
        if (pd.MUSets.Length == 0)
          efpMU.FixedDocIds = null;
        else
        {
          // TODO: Надо для единиц 2 и 3 учитывать выбранные значения в верхних полях
          IdList lst = new IdList();
          for (int i = 0; i < pd.MUSets.Length; i++)
          {
            Int32 id;
            switch (nMU)
            {
              case 1: id = pd.MUSets[i].MUId1; break;
              case 2: id = pd.MUSets[i].MUId2; break;
              case 3: id = pd.MUSets[i].MUId3; break;
              default: throw new ArgumentException();
            }
            if (id != 0)
              lst.Add(id);
          }
          efpMU.FixedDocIds = lst;
        }
      }
      catch (Exception e)
      {
        if (!CB_Enter_ErrorLogged)
        {
          LogoutTools.LogoutException(e, "Ошибка загрузки списка значений для единицы измерения "+nMU.ToString()+", ProductId= " + productId.ToString() + ". Повторные ошибки не регистрируются");
          CB_Enter_ErrorLogged = true;
        }
        EFPApp.ShowTempMessage("Не удалось получить список значений");
      }
    }

    void Editor_AfterWrite(object sender, SubDocEditEventArgs args)
    {
      ProductBuffer.AddOpProductValues(efpProduct.DocId, "Description", efpDescription.Text);
      //ProductBuffer.AddOpProductValues(efpProduct.DocId, "Unit1", efpMU1.Text);
      //ProductBuffer.AddOpProductValues(efpProduct.DocId, "Unit2", efpMU2.Text);
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