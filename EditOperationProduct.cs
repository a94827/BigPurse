using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FreeLibSet.Forms.Docs;
using FreeLibSet.Forms;

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
      form.AddPage(args);
    }

    EFPDocComboBox efpProduct;
    EFPTextComboBox efpDescription, efpUnit;
    EFPIntEditBox efpQuantity;
    EFPTextBox efpFormula;
    EFPDecimalEditBox efpSum;

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
      // TODO:

      efpSum = new EFPDecimalEditBox(page.BaseProvider, edSum);
      efpSum.CanBeEmpty = false;
      efpSum.Control.Format = Tools.MoneyFormat;
      args.AddDecimal(efpSum, "RecordSum", false);

      #endregion

      #region Комментарий

      EFPTextBox efpComment = new EFPTextBox(page.BaseProvider, edComment);
      efpComment.CanBeEmpty = true;
      args.AddText(efpComment, "Comment", true);

      #endregion
    }

    #endregion
  }
}