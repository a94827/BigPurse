using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FreeLibSet.Forms.Docs;

namespace App
{
  internal partial class EditProductMUSet : Form
  {
    #region Конструктор формы

    public EditProductMUSet()
    {
      InitializeComponent();
    }

    #endregion

    #region Редактор

    public static void InitSubDocEditForm(object sender, InitSubDocEditFormEventArgs args)
    {
      EditProductMUSet form = new EditProductMUSet();
      form._Editor = args.Editor;
      form.AddPage(args);
    }

    private SubDocumentEditor _Editor;

    EFPDocComboBox efpMU1, efpMU2, efpMU3;

    private void AddPage(InitSubDocEditFormEventArgs args)
    {
      DocEditPage page = args.AddPage("Общие", MainPanel);

      efpMU1 = new EFPDocComboBox(page.BaseProvider, cbMU1, ProgramDBUI.TheUI.DocTypes["MUs"]);
      efpMU1.CanBeEmpty = false;
      args.AddRef(efpMU1, "MU1", false);

      efpMU2 = new EFPDocComboBox(page.BaseProvider, cbMU2, ProgramDBUI.TheUI.DocTypes["MUs"]);
      efpMU2.CanBeEmpty = true;
      args.AddRef(efpMU2, "MU2", false);

      efpMU3 = new EFPDocComboBox(page.BaseProvider, cbMU3, ProgramDBUI.TheUI.DocTypes["MUs"]);
      efpMU3.CanBeEmpty = true;
      args.AddRef(efpMU3, "MU3", false);
    }

    #endregion
  }
}