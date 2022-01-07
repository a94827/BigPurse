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
  internal partial class EditOperation : Form
  {
    #region Конструктор формы

    public EditOperation()
    {
      InitializeComponent();
    }

    #endregion

    #region Табличный просмотр

    public static void ImageValueNeeded(object sender, DBxImageValueNeededEventArgs args)
    {
      OperationType opType = args.GetEnum<OperationType>("OpType");
      args.ImageKey = GetImageKey(opType);
    }

    private static readonly string[] ImageKeys = new string[] { 
      "OperationBalance",
      "OperationIncome",
      "OperationExpense",
      "OperationMove",
      "OperationDebt",
      "OperationCredit"};

    public static string GetImageKey(OperationType opType)
    {
      if ((int)opType < 0 || (int)opType >= ImageKeys.Length)
        return "Error";
      else
        return ImageKeys[(int)opType];
    }

    #endregion

    #region Редактор

    #region BeforeEdit

    public static void BeforeEditDoc(object sender, BeforeDocEditEventArgs args)
    {
      if (args.Editor.State == EFPDataGridViewState.Insert)
      {
        ListSelectDialog dlg = new ListSelectDialog();
        dlg.Title = "Создание операции";
        dlg.ImageKey = "Insert";
        dlg.Items = ProgramConvert.OperationTypeNames;
        dlg.ImageKeys = ImageKeys;
        try { dlg.SelectedIndex = args.Editor.MainValues["OpType"].AsInteger; }
        catch { }

        if (dlg.ShowDialog() != DialogResult.OK)
        {
          args.Cancel = true;
          return;
        }

        args.Editor.MainValues["OpType"].SetInteger(dlg.SelectedIndex);
      }
    }


    #endregion

    #region InitDocEditForm

    DocumentEditor _Editor;

    public static void InitDocEditForm(object Sender, InitDocEditFormEventArgs Args)
    {
      EditOperation Form = new EditOperation();

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