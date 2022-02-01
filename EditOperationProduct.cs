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

    #region Табличный просмотр

    public static void QuantityTextColumnValueNeeded(object sender, EFPGridProducerValueNeededEventArgs args)
    {
      args.Value = GetQuantityText(args.GetSingle(0), args.GetString(1),
        args.GetSingle(2), args.GetString(3), args.GetSingle(4), args.GetString(5));
    }

    private static string GetQuantityText(float q1, string muName1,
      float q2, string muName2,
      float q3, string muName3)
    {
      string[] a = new string[3];
      a[0] = GetQuantityText(q1, muName1);
      a[1] = GetQuantityText(q2, muName2);
      a[2] = GetQuantityText(q3, muName3);

      return DataTools.JoinNotEmptyStrings(", ", a);
    }

    private static string GetQuantityText(float q, string muName)
    {
      if (q == 0f && String.IsNullOrEmpty(muName))
        return String.Empty;

      return q.ToString("0.###") + " " + muName;
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
      efpDescription.Validating += new UIValidatingEventHandler(efpDescription_Validating);
      dvDescription = args.AddText(efpDescription, "Description", false);

      #endregion

      #region Количество и единица измерения

      efpQuantity1 = new EFPSingleEditBox(page.BaseProvider, edQuantity1);
      efpQuantity1.CanBeEmpty = true;
      dvQuantity1 = args.AddSingle(efpQuantity1, "Quantity1", false);
      efpQuantity1.IsNotEmptyEx.ValueChanged += new EventHandler(UpdateMUs);
      efpQuantity1.Validating += new UIValidatingEventHandler(efpQuantity1_Validating);

      efpMU1 = new EFPDocComboBox(page.BaseProvider, cbMU1, ProgramDBUI.TheUI.DocTypes["MUs"]);
      efpMU1.CanBeEmpty = true;
      efpMU1.DisplayName = "Ед. изм. 1";
      efpMU1.DocIdEx.ValueChanged += new EventHandler(UpdateMUs);
      dvMU1 = args.AddRef(efpMU1, "MU1", false);
      SetQuantityAndUnitValidation(efpQuantity1, efpMU1);


      efpQuantity2 = new EFPSingleEditBox(page.BaseProvider, edQuantity2);
      efpQuantity2.CanBeEmpty = true;
      dvQuantity2 = args.AddSingle(efpQuantity2, "Quantity2", false);
      efpQuantity2.IsNotEmptyEx.ValueChanged += new EventHandler(UpdateMUs);

      efpMU2 = new EFPDocComboBox(page.BaseProvider, cbMU2, ProgramDBUI.TheUI.DocTypes["MUs"]);
      efpMU2.CanBeEmpty = true;
      efpMU2.DisplayName = "Ед. изм. 2";
      efpMU2.DocIdEx.ValueChanged += new EventHandler(UpdateMUs); 
      dvMU2 = args.AddRef(efpMU2, "MU2", false);
      SetQuantityAndUnitValidation(efpQuantity2, efpMU2);


      efpQuantity3 = new EFPSingleEditBox(page.BaseProvider, edQuantity3);
      efpQuantity3.CanBeEmpty = true;
      dvQuantity3 = args.AddSingle(efpQuantity3, "Quantity3", false);
      efpQuantity3.IsNotEmptyEx.ValueChanged += new EventHandler(UpdateMUs);

      efpMU3 = new EFPDocComboBox(page.BaseProvider, cbMU3, ProgramDBUI.TheUI.DocTypes["MUs"]);
      efpMU3.CanBeEmpty = true;
      efpMU3.DisplayName = "Ед. изм. 3";
      dvMU3 = args.AddRef(efpMU3, "MU3", false);
      SetQuantityAndUnitValidation(efpQuantity3, efpMU3);

      if (!_Editor.IsReadOnly)
      {
        efpQuantity2.Validators.AddError(new DepEqual<float>(efpQuantity2.ValueEx, 0f),
        "Нельзя задавать второе количество без первого",
        new DepEqual<float>(efpQuantity1.ValueEx, 0f));
        efpQuantity3.Validators.AddError(new DepEqual<float>(efpQuantity3.ValueEx, 0f),
          "Нельзя задавать третье количество без второго",
          new DepEqual<float>(efpQuantity2.ValueEx, 0f));

        EditProductMUSet.AddMUValidators(efpMU1, efpMU2, efpMU3);

        UpdateMUs(null, null);
      }

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
      if (_Editor.IsReadOnly)
        return;

      //efpMU.Validating += new FreeLibSet.UICore.UIValidatingEventHandler(efpUnit_Validating); // в первую очередь

      DepValue<bool> isNZ = new DepNot(new DepEqual<float>(efpQuantity.ValueEx, 0f));
      efpQuantity.Validators.AddError(isNZ,
        "Количество должно быть задано, если задана единица измерения",
        efpMU.IsNotEmptyEx);
      efpMU.Validators.AddError(efpMU.IsNotEmptyEx, "Должна быть задана единица измерения", isNZ);
    }

    void efpQuantity1_Validating(object sender, UIValidatingEventArgs args)
    {
      if (args.ValidateState == UIValidateState.Error)
        return;

      if (efpQuantity1.Value == 0f)
      {
        ProductBuffer.ProductData pd = ProductBuffer.GetProductData(efpProduct.DocId);

        switch (pd.QuantityPresence)
        {
          case PresenceType.Required:
            args.SetError("Количество должно быть заполнено");
            break;
          case PresenceType.WarningIfNone:
            args.SetWarning("Количество обычно должно быть заполнено");
            break;
        }
      }
    }


    #region Списки для выбора значений полей

    /// <summary>
    /// Флажки наличия загуженнных списков выбора строк в текстовые поля
    /// </summary>
    private bool lvDescription;

    void efpDescription_Validating(object sender, UIValidatingEventArgs args)
    {
      ProductBuffer.ValidateProductDescription(efpProduct.DocId, efpDescription.Text, args);
    }



    void efpProduct_ValueChanged(object sender, EventArgs args)
    {
      if (_Editor.IsReadOnly)
        return;

      lvDescription = false;
      dvDescription.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Description");
      efpDescription.Validate();

      efpQuantity1.Validate();
      efpQuantity2.Validate();
      efpQuantity3.Validate();

      UpdateMUs(null, null);
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
        if (!UpdateMUs_ErrorLogged)
        {
          LogoutTools.LogoutException(e, "Ошибка загрузки списка значений для поля \"Description\", ProductId= " + productId.ToString() + ". Повторные ошибки не регистрируются");
          UpdateMUs_ErrorLogged = true;
        }
        EFPApp.ShowTempMessage("Не удалось получить список значений");
      }
    }

    private static bool UpdateMUs_ErrorLogged = false;

    private bool InsideUpdateMUs;

    private void UpdateMUs(object sender, EventArgs args)
    {
      if (InsideUpdateMUs)
        return;

      InsideUpdateMUs = true;
      try
      {
        try
        {
          DoUpdateMUs();
        }
        catch (Exception e)
        {
          if (!UpdateMUs_ErrorLogged)
          {
            LogoutTools.LogoutException(e, "Ошибка загрузки списка значений для единиц измерения, ProductId= " + efpProduct.DocId.ToString() + ". Повторные ошибки не регистрируются");
            UpdateMUs_ErrorLogged = true;
          }
          EFPApp.ShowTempMessage("Не удалось получить список значений");
        }
      }
      finally
      {
        InsideUpdateMUs = false;
      }
    }

    private void DoUpdateMUs()
    {
      if (_Editor.IsReadOnly)
        return;

      ProductBuffer.ProductData pd = ProductBuffer.GetProductData(efpProduct.DocId);

      dvQuantity1.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Quantity1");
      dvQuantity2.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Quantity2");
      dvQuantity3.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Quantity3");

      if (!dvQuantity1.UserEnabled)
        efpQuantity1.NValue = null;
      if (!dvQuantity2.UserEnabled)
        efpQuantity2.NValue = null;
      if (!dvQuantity3.UserEnabled)
        efpQuantity3.NValue = null;

      // Так неудобно. Не видно, какие допустимы единицы измерения для товара
      //dvMU1.UserEnabled = efpQuantity1.NValue.HasValue;
      //dvMU2.UserEnabled = efpQuantity2.NValue.HasValue;
      //dvMU3.UserEnabled = efpQuantity3.NValue.HasValue;

      if (pd.MaxQuantityLevel > 0)
      {
        if (pd.MUSets.Length == 0)
        {
          efpMU1.FixedDocIds = null;
          efpMU2.FixedDocIds = null;
          efpMU3.FixedDocIds = null;
          dvMU1.UserEnabled = true;
          dvMU2.UserEnabled = true;
          dvMU3.UserEnabled = true;
        }
        else
        {
          IdList lst1 = new IdList();
          IdList lst2 = new IdList();
          IdList lst3 = new IdList();
          for (int i = 0; i < pd.MUSets.Length; i++)
          {
            lst1.Add(pd.MUSets[i].MUId1);

            if (pd.MUSets[i].MUId2 != 0)
            {
              if (efpMU1.DocId == 0 || efpMU1.DocId == pd.MUSets[i].MUId1)
              {
                lst2.Add(pd.MUSets[i].MUId2);
                if (pd.MUSets[i].MUId3 != 0)
                {
                  if (efpMU2.DocId == 0 || efpMU2.DocId == pd.MUSets[i].MUId2)
                    lst3.Add(pd.MUSets[i].MUId3);
                }
              }
            }
          }
          efpMU1.FixedDocIds = lst1;
          efpMU2.FixedDocIds = lst2;
          efpMU3.FixedDocIds = lst3;

          if (lst1.Count == 1 && efpQuantity1.NValue.HasValue && efpMU1.DocId == 0)
            efpMU1.DocId = lst1.SingleId;
          if (lst2.Count == 1 && efpQuantity2.NValue.HasValue && efpMU2.DocId == 0)
            efpMU2.DocId = lst2.SingleId;
          if (lst3.Count == 1 && efpQuantity3.NValue.HasValue && efpMU3.DocId == 0)
            efpMU3.DocId = lst3.SingleId;

          dvMU1.UserEnabled = lst1.Count > 0;
          dvMU2.UserEnabled = lst2.Count > 0;
          dvMU3.UserEnabled = lst3.Count > 0;
        }

        // Это тоже не надо
        //if (!efpQuantity1.NValue.HasValue)
        //  efpMU1.DocId = 0;
        //if (!efpQuantity2.NValue.HasValue)
        //  efpMU2.DocId = 0;
        //if (!efpQuantity3.NValue.HasValue)
        //  efpMU3.DocId = 0;
      }



      efpMU1.Validate();
      efpMU2.Validate();
      efpMU3.Validate();
    }


    void Editor_AfterWrite(object sender, SubDocEditEventArgs args)
    {
      ProductBuffer.AddOpProductValues(efpProduct.DocId, "Description", efpDescription.Text);
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