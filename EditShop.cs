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

namespace BigPurse
{
  internal partial class EditShop : Form
  {
    #region Конструктор формы

    public EditShop()
    {
      InitializeComponent();
    }

    #endregion

    #region Редактор

    #region InitDocEditForm

    DocumentEditor _Editor;

    public static void InitDocEditForm(object Sender, InitDocEditFormEventArgs Args)
    {
      EditShop Form = new EditShop();

      Form._Editor = Args.Editor;

      Form.AddPage1(Args);
    }

    #endregion

    #region Страница 1 (общие)

    private EFPTextBox efpName;

    private void AddPage1(InitDocEditFormEventArgs args)
    {
      DocEditPage Page = args.AddPage("Общие", MainPanel1);
      Page.ImageKey = args.Editor.DocTypeUI.ImageKey;

      efpName = new EFPTextBox(Page.BaseProvider, edName);
      efpName.CanBeEmpty = false;
      args.AddText(efpName, "Name", false);

      #region Комментарий

      EFPTextBox efpComment = new EFPTextBox(Page.BaseProvider, edComment);
      efpComment.CanBeEmpty = true;
      args.AddText(efpComment, "Comment", true);

      #endregion
    }

    #endregion

    #endregion
  }
}