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
using FreeLibSet.Forms.Data;

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

    public static string GetQuantityText(float q1, string muName1,
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
      if (!args.Editor.IsReadOnly)
      {
        args.Editor.AfterWrite += new SubDocEditEventHandler(form.Editor_AfterWrite);
      }
    }

    private SubDocumentEditor _Editor;

    EFPDocComboBox efpProduct;
    EFPTextComboBox efpDescription;
    ExtValueTextBox dvDescription;
    EFPDocComboBox efpPurpose, efpAuxPurpose;
    ExtValueDocComboBox dvPurpose, dvAuxPurpose;
    EFPDocComboBox efpMU1, efpMU2, efpMU3;
    ExtValueDocComboBox dvMU1, dvMU2, dvMU3;
    EFPSingleEditBox efpQuantity1, efpQuantity2, efpQuantity3;
    ExtValueSingleEditBox dvQuantity1, dvQuantity2, dvQuantity3;
    EFPTextBox efpFormula;
    EFPDecimalEditBox efpSum;
    ExtValueDecimalEditBox dvSum;

    private void AddPage(InitSubDocEditFormEventArgs args)
    {
      ExtEditPage page = args.AddPage("Общие", MainPanel);

      #region Продукт, описание и назначение

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

      efpPurpose = new EFPDocComboBox(page.BaseProvider, cbPurpose, ProgramDBUI.TheUI.DocTypes["Purposes"]);
      efpPurpose.CanBeEmpty = true;
      efpPurpose.Validating += new UIValidatingEventHandler(efpPurpose_Validating);
      dvPurpose = args.AddRef(efpPurpose, "Purpose", false);

      efpAuxPurpose = new EFPDocComboBox(page.BaseProvider, cbAuxPurpose, ProgramDBUI.TheUI.DocTypes["AuxPurposes"]);
      efpAuxPurpose.CanBeEmpty = true;
      efpAuxPurpose.Validating += new UIValidatingEventHandler(efpAuxPurpose_Validating);
      dvAuxPurpose = args.AddRef(efpAuxPurpose, "AuxPurpose", false);

      EFPDateTimeBox efpDate = (EFPDateTimeBox)(args.MainEditor.Properties["EFPDate"]);
      DateRangeInclusionGridFilter filtPurposeDate = new DateRangeInclusionGridFilter("FirstDate", "LastDate");
      filtPurposeDate.DisplayName = "Период действия";
      efpPurpose.Filters.Add(filtPurposeDate);
      filtPurposeDate.Value = efpDate.NValue;

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
      args.AddEditItem(dvFormula);

      efpSum = new EFPDecimalEditBox(page.BaseProvider, edSum);
      efpSum.CanBeEmpty = false;
      efpSum.Control.Format = Tools.MoneyFormat;
      dvSum = args.AddDecimal(efpSum, "RecordSum", false);
      dvSum.UserDisabledMode = ExtValueUserDisabledMode.AlwaysReplace;
      dvSum.UserEnabledEx = new DepNot(efpFormula.IsNotEmptyEx);

      efpSum.Validators.AddWarning(new DepNot(new DepEqual<decimal>(efpSum.ValueEx, 0m)), "Сумма должна быть задана");

      //if (!args.Editor.IsReadOnly)
      //{ 
      //  efpFormula.TextEx.ValueChanged+=new EventHandler(efpFormula_ValueChanged);
      //}

      #endregion

      #region История

      _ShopId = ((EFPDocComboBox)(args.MainEditor.Properties["EFPShop"])).DocId;

      btnHistory.Image = EFPApp.MainImages.Images["Time"];
      btnHistory.ImageAlign = ContentAlignment.MiddleCenter;
      EFPButton efpHistory = new EFPButton(page.BaseProvider, btnHistory);
      efpHistory.DisplayName = "История";
      efpHistory.ToolTipText = "Выбор товара из истории для магазина";
      if (_Editor.State == UIDataState.Insert && _ShopId != 0)
        efpHistory.Click += new EventHandler(efpHistory_Click);
      else
        efpHistory.Visible = false;

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

    void efpPurpose_Validating(object sender, UIValidatingEventArgs args)
    {
      ProductBuffer.ValidateProductPurpose(efpProduct.DocId, efpPurpose.DocId, args);
    }

    void efpAuxPurpose_Validating(object sender, UIValidatingEventArgs args)
    {
    }

    void efpProduct_ValueChanged(object sender, EventArgs args)
    {
      if (_Editor.IsReadOnly)
        return;

      lvDescription = false;
      dvDescription.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Description");
      efpDescription.Validate();

      dvPurpose.UserEnabled = ProductBuffer.GetColumnEnabled(efpProduct.DocId, "Purpose");
      efpPurpose.Validate();

      dvAuxPurpose.UserEnabled = dvPurpose.UserEnabled;
      efpAuxPurpose.Validate();

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
        if (!_UpdateMUs_ErrorLogged)
        {
          LogoutTools.LogoutException(e, "Ошибка загрузки списка значений для поля \"Description\", ProductId= " + productId.ToString() + ". Повторные ошибки не регистрируются");
          _UpdateMUs_ErrorLogged = true;
        }
        EFPApp.ShowTempMessage("Не удалось получить список значений");
      }
    }

    private static bool _UpdateMUs_ErrorLogged = false;

    private bool _InsideUpdateMUs;

    private void UpdateMUs(object sender, EventArgs args)
    {
      if (_InsideUpdateMUs)
        return;

      _InsideUpdateMUs = true;
      try
      {
        try
        {
          DoUpdateMUs();
        }
        catch (Exception e)
        {
          if (!_UpdateMUs_ErrorLogged)
          {
            LogoutTools.LogoutException(e, "Ошибка загрузки списка значений для единиц измерения, ProductId= " + efpProduct.DocId.ToString() + ". Повторные ошибки не регистрируются");
            _UpdateMUs_ErrorLogged = true;
          }
          EFPApp.ShowTempMessage("Не удалось получить список значений");
        }
      }
      finally
      {
        _InsideUpdateMUs = false;
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
          dvMU1.UserEnabled = dvQuantity1.UserEnabled;
          dvMU2.UserEnabled = dvQuantity2.UserEnabled;
          dvMU3.UserEnabled = dvQuantity3.UserEnabled;
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

          dvMU1.UserEnabled = lst1.Count > 0 && dvQuantity1.UserEnabled;
          dvMU2.UserEnabled = lst2.Count > 0 && dvQuantity2.UserEnabled;
          dvMU3.UserEnabled = lst3.Count > 0 && dvQuantity3.UserEnabled;
        }

        // Это тоже не надо
        //if (!efpQuantity1.NValue.HasValue)
        //  efpMU1.DocId = 0;
        //if (!efpQuantity2.NValue.HasValue)
        //  efpMU2.DocId = 0;
        //if (!efpQuantity3.NValue.HasValue)
        //  efpMU3.DocId = 0;
      }
      else
      {
        dvMU1.UserEnabled = false;
        dvMU2.UserEnabled = false;
        dvMU3.UserEnabled = false;
      }


      efpMU1.Validate();
      efpMU2.Validate();
      efpMU3.Validate();
    }

    void Editor_AfterWrite(object sender, SubDocEditEventArgs args)
    {
      if (!dvDescription.UserEnabled)
        args.Editor.SubDocs.Values["Description"].SetNull();
      if (!dvPurpose.UserEnabled)
        args.Editor.SubDocs.Values["Purpose"].SetNull();
      if (!dvQuantity1.UserEnabled)
      {
        args.Editor.SubDocs.Values["Quantity1"].SetNull();
        args.Editor.SubDocs.Values["MU1"].SetNull();
      }
      if (!dvQuantity2.UserEnabled)
      {
        args.Editor.SubDocs.Values["Quantity2"].SetNull();
        args.Editor.SubDocs.Values["MU2"].SetNull();
      }
      if (!dvQuantity3.UserEnabled)
      {
        args.Editor.SubDocs.Values["Quantity3"].SetNull();
        args.Editor.SubDocs.Values["MU3"].SetNull();
      }

      ProductBuffer.AddOpProductValues(efpProduct.DocId, "Description", efpDescription.Text);
    }

    #endregion

    private class DocValueFormula : ExtValueTextBox
    {
      public DocValueFormula(DBxExtValue extValue, IEFPTextBox controlProvider)
        : base(extValue, controlProvider, false)
      {
      }

      protected override void ValueToControl()
      {
        CurrentValueEx.Value = ExtValue.AsString.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
      }

      protected override void ValueFromControl()
      {
        ExtValue.SetString(CurrentValueEx.Value.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, "."));
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
        _FormulaPL.Parse(pd);
        if (pd.FirstErrorToken != null)
          args.SetError(pd.FirstErrorToken.ErrorMessage.Value.Text);
        else
        {
          IExpression expr = _FormulaPL.CreateExpression(pd);
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

    private static ParserList _FormulaPL = CreateFormulaPL();

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

    #region История

    private Int32 _ShopId;

    private static readonly Dictionary<Int32, DataTable> _ShopHistoryTables = new Dictionary<Int32, DataTable>();

    void efpHistory_Click(object sender, EventArgs args)
    {
      EFPButton efpHistory = (EFPButton)sender;
      string shopName = _Editor.UI.DocTypes["Shops"].GetTextValue(_ShopId);

      DataTable table;
      if (!_ShopHistoryTables.TryGetValue(_ShopId, out table))
      {
        table = LoadShopHistoryTable();
        _ShopHistoryTables.Add(_ShopId, table);
      }

      OKCancelGridForm form = new OKCancelGridForm();
      form.Text = "История покупок в магазине \"" + shopName + "\" за год";
      form.Icon = EFPApp.MainImages.Icons["Time"];
      EFPDBxGridView efpGrid = new EFPDBxGridView(form, _Editor.UI);
      efpGrid.ConfigSectionName = form.FormProvider.ConfigSectionName = "ProductHistory";
      efpGrid.Control.AutoGenerateColumns = false;
      efpGrid.GridProducer = CreateHistGridProducer();
      efpGrid.AutoSort = true;
      efpGrid.ReadOnly = false;
      efpGrid.CanInsert = false;
      efpGrid.CanDelete = false;
      efpGrid.Control.ReadOnly = true;
      efpGrid.CanView = true;
      efpGrid.EditData += new EventHandler(efpHistGrid_EditData);
      efpGrid.GetDocSel += new EFPDBxGridViewDocSelEventHandler(efpHistGrid_GetDocSel);
      efpGrid.CommandItems.EnterAsOk = true;
      efpGrid.RefreshData += new EventHandler(efpHistGrid_RefreshData);
      efpGrid.SelectedRowsMode = EFPDataGridViewSelectedRowsMode.PrimaryKey; // по Id записи

      efpGrid.Control.DataSource = table.DefaultView;

      if (EFPApp.ShowDialog(form, false, new EFPDialogPosition(efpHistory.Control)) == DialogResult.OK)
      {
        if (efpGrid.CurrentDataRow == null)
        {
          EFPApp.ShowTempMessage("Нет выбранной строки истории");
          return;
        }

        efpProduct.DocId = DataTools.GetInt(efpGrid.CurrentDataRow, "Product");
        efpDescription.Text = DataTools.GetString(efpGrid.CurrentDataRow, "Description");
        efpPurpose.DocId = DataTools.GetInt(efpGrid.CurrentDataRow, "Purpose");
        efpAuxPurpose.DocId = DataTools.GetInt(efpGrid.CurrentDataRow, "AuxPurpose");
        efpQuantity1.Value = DataTools.GetSingle(efpGrid.CurrentDataRow, "Quantity1");
        efpMU1.DocId = DataTools.GetInt(efpGrid.CurrentDataRow, "MU1");
        efpQuantity2.Value = DataTools.GetSingle(efpGrid.CurrentDataRow, "Quantity2");
        efpMU2.DocId = DataTools.GetInt(efpGrid.CurrentDataRow, "MU2");
        efpQuantity3.Value = DataTools.GetSingle(efpGrid.CurrentDataRow, "Quantity3");
        efpMU3.DocId = DataTools.GetInt(efpGrid.CurrentDataRow, "MU3");
        efpFormula.Text = DataTools.GetString(efpGrid.CurrentDataRow, "Formula");
        efpSum.Value = DataTools.GetDecimal(efpGrid.CurrentDataRow, "RecordSum");
      }
    }

    private DataTable LoadShopHistoryTable()
    {
      string shopName = _Editor.UI.DocTypes["Shops"].GetTextValue(_ShopId);
      using (new Splash("Получение истории для магазина \"" + shopName + "\""))
      {

        DBxSelectInfo selInfo = new DBxSelectInfo();
        selInfo.TableName = "OperationProducts";
        /*
        selInfo.Expressions.Add("Product,Product.Name,Description,Quantity1,MU1,MU1.Name,Quantity2,MU2,MU2.Name,Quantity3,MU3,MU3.Name,Formula,RecordSum,Purpose,Purpose.Name,AuxPurpose,AuxPurpose.Name");
        selInfo.OrderBy = DBxOrder.FromDataViewSort("DocId.Date DESC,DocId.OpOrder DESC");

        List<DBxFilter> filters=new List<DBxFilter>();
        filters.Add(new ValueFilter("DocId.Date", DateTime.Today.AddYears(-1), CompareKind.GreaterOrEqualThan));
        filters.Add(new ValueFilter("DocId.OpType", (int)OperationType.Expense));
        filters.Add(new ValueFilter("DocId.Shop", _ShopId));
        filters.Add(DBSSubDocType.DeletedFalseFilter);
        filters.Add(DBSSubDocType.DocIdDeletedFalseFilter);
        selInfo.Where = AndFilter.FromList(filters);
        selInfo.MaxRecordCount = 50;
        selInfo.Unique = true;
         */

        //selInfo.Expressions.Add("Product,Description,Quantity1,MU1,Quantity2,MU2,Quantity3,MU3,Purpose,AuxPurpose");
        selInfo.Expressions.Add("Product,Description,Purpose");
        selInfo.InitGroupBy();
        //selInfo.Expressions.Add("Product.Name,MU1.Name,MU2.Name,MU3.Name,Formula,RecordSum,Purpose.Name,AuxPurpose.Name,DocId,DocId.Date,Id");
        selInfo.Expressions.Add("Product.Name,Quantity1,MU1,MU1.Name,Quantity2,MU2,MU2.Name,Quantity3,MU3,MU3.Name,Formula,RecordSum,Purpose.Name,AuxPurpose,AuxPurpose.Name,DocId,DocId.Date,Id");
        selInfo.OrderBy = DBxOrder.FromDataViewSort("DocId.Date DESC,DocId.OpOrder DESC,DocId"); // сортировка нужна, чтобы при наличии повторов показывались последние операции, а не какие попало

        List<DBxFilter> filters = new List<DBxFilter>();
        filters.Add(new ValueFilter("DocId.Date", DateTime.Today.AddYears(-1), CompareKind.GreaterOrEqualThan));
        filters.Add(new ValueFilter("DocId.OpType", (int)OperationType.Expense));
        filters.Add(new ValueFilter("DocId.Shop", _ShopId));
        filters.Add(DBSSubDocType.DeletedFalseFilter);
        filters.Add(DBSSubDocType.DocIdDeletedFalseFilter);
        selInfo.Where = AndFilter.FromList(filters);
        //  selInfo.MaxRecordCount = 50;

        DataTable table = _Editor.UI.DocProvider.FillSelect(selInfo);
        DataTools.SetPrimaryKey(table, "Id");
        return table;
      }
    }

    private EFPGridProducer CreateHistGridProducer()
    {
      EFPGridProducer producer = new EFPGridProducer();
      producer.Columns.AddText("Product.Name", "Товар", 30, 20);
      producer.Columns.AddText("Description", "Описание", 30, 20);
      producer.Columns.AddText("Purpose.Name", "Назначение", 10, 5);
      producer.Columns.AddText("AuxPurpose.Name", "Доп. назначение", 10, 5);
      producer.Columns.AddUserText("QuantityText", "Quantity1,MU1.Name,Quantity2,MU2.Name,Quantity3,MU3.Name",
        QuantityTextColumnValueNeeded, "Количество", 20, 10);
      producer.Columns.AddMoney("RecordSum", "Сумма");
      producer.Columns.AddDate("DocId.Date", "Дата операции");

      producer.FixedColumns.Add("Product.Name");
      producer.FixedColumns.Add("Description");
      producer.FixedColumns.Add("Purpose.Name");
      producer.FixedColumns.Add("DocId.Date");

      producer.Orders.Add("Product.Name,Description,Purpose.Name", "Товар");
      producer.Orders.Add("DocId.Date DESC,Product.Name,Description,Purpose.Name", "Дата (по убыванию)", new EFPDataGridViewSortInfo("DocId.Date", ListSortDirection.Descending));
      producer.Orders.Add("DocId.Date ASC,Product.Name,Description,Purpose.Name", "Дата (по возрастанию)", new EFPDataGridViewSortInfo("DocId.Date", ListSortDirection.Ascending));
      producer.Orders.Add("Purpose.Name,Product.Name,Description", "Назначение");

      producer.NewDefaultConfig(false);
      producer.DefaultConfig.Columns.AddFill("Product.Name", 40);
      producer.DefaultConfig.Columns.AddFill("Description", 30);
      producer.DefaultConfig.Columns.Add("Purpose.Name");
      producer.DefaultConfig.Columns.Add("AuxPurpose.Name");
      producer.DefaultConfig.Columns.AddFill("QuantityText", 30);
      producer.DefaultConfig.Columns.Add("RecordSum");
      producer.DefaultConfig.Columns.Add("DocId.Date");

      return producer;
    }

    void efpHistGrid_EditData(object sender, EventArgs args)
    {
      EFPDBxGridView efpGrid = (EFPDBxGridView)sender;
      Int32 docId = DataTools.GetInt(efpGrid.CurrentDataRow, "DocId");
      _Editor.UI.DocTypes["Operations"].PerformEditing(docId, efpGrid.State == UIDataState.View);
    }

    void efpHistGrid_GetDocSel(object sender, EFPDBxGridViewDocSelEventArgs args)
    {
      args.AddFromColumn("Operations", "DocId");
      args.AddFromColumn("Products", "Product");
      args.AddFromColumn("Purposes", "Purpose");
      args.AddFromColumn("AuxPurposes", "AuxPurpose");
      args.AddFromColumn("MUs", "MU1");
      args.AddFromColumn("MUs", "MU2");
      args.AddFromColumn("MUs", "MU3");
    }


    void efpHistGrid_RefreshData(object sender, EventArgs args)
    {
      EFPDBxGridView efpGrid = (EFPDBxGridView)sender;
      DataTable table = LoadShopHistoryTable();
      _ShopHistoryTables[_ShopId] = table;
      efpGrid.Control.DataSource = table.DefaultView;
    }

    #endregion
  }
}