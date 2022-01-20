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

namespace App
{
  internal partial class EditMU : Form
  {
    #region Конструктор формы

    public EditMU()
    {
      InitializeComponent();
    }

    #endregion

    #region Редактор

    #region InitDocEditForm

    DocumentEditor _Editor;

    public static void InitDocEditForm(object sender, InitDocEditFormEventArgs args)
    {
      EditMU form = new EditMU();
      form._Editor = args.Editor;
      form.AddPage1(args);
    }

    #endregion

    #region Страница 1 (общие)

    private EFPTextBox efpName;

    private void AddPage1(InitDocEditFormEventArgs args)
    {
      DocEditPage page = args.AddPage("Общие", MainPanel1);

      efpName = new EFPTextBox(page.BaseProvider, edName);
      efpName.CanBeEmpty = false;
      args.AddText(efpName, "Name", false);

      #region Комментарий

      EFPTextBox efpComment = new EFPTextBox(page.BaseProvider, edComment);
      efpComment.CanBeEmpty = true;
      args.AddText(efpComment, "Comment", true);

      #endregion
    }

    #endregion

    #endregion
  }
}