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
  internal partial class EditProduct : Form
  {
    #region Конструктор формы

    public EditProduct()
    {
      InitializeComponent();
    }

    #endregion

    #region Редактор документа

    #region InitDocEditForm

    DocumentEditor _Editor;

    public static void InitDocEditForm(object sender, InitDocEditFormEventArgs args)
    {
      EditProduct form = new EditProduct();
      form._Editor = args.Editor;
      form.AddPage1(args);
      form.AddPage2(args);
    }

    #endregion

    #region Страница 1 (Общие)

    private EFPTextBox efpName;
    private EFPDocComboBox efpParent;

    private void AddPage1(InitDocEditFormEventArgs args)
    {
      DocEditPage page = args.AddPage("Общие", MainPanel1);

      efpName = new EFPTextBox(page.BaseProvider, edName);
      efpName.CanBeEmpty = false;
      args.AddText(efpName, "Name", false);

      efpParent = new EFPDocComboBox(page.BaseProvider, cbParent, args.Editor.DocTypeUI);
      efpParent.CanBeEmpty = true;
      args.AddRef(efpParent, "ParentId", true);

      #region Комментарий

      EFPTextBox efpComment = new EFPTextBox(page.BaseProvider, edComment);
      efpComment.CanBeEmpty = true;
      args.AddText(efpComment, "Comment", true);

      #endregion
    }

    #endregion

    #region Страница 2 (В операции)

    EFPListComboBox efpDescriptionPresence, efpUnit1Presence, efpUnit2Presence;
    EFPAllSubDocComboBox efpMU1, efpMU2;

    private void AddPage2(InitDocEditFormEventArgs args)
    {
      DocEditPage page = args.AddPage("В операции", MainPanel2);
      page.ImageKey = "OperationExpense";


      cbDescriptionPresence.Items.AddRange(Tools.PresenceTypeNames);
      efpDescriptionPresence = new EFPListComboBox(page.BaseProvider, cbDescriptionPresence);
      args.AddInt(efpDescriptionPresence, "DescriptionPresence", true);


      cbUnit1Presence.Items.AddRange(Tools.PresenceTypeNames);
      efpUnit1Presence = new EFPListComboBox(page.BaseProvider, cbUnit1Presence);
      args.AddInt(efpUnit1Presence, "Unit1Presence", true);

      efpMU1 = new EFPAllSubDocComboBox(page.BaseProvider, cbMU1, _Editor, _Editor.Documents[0].SubDocs["ProductMUs1"]);
      efpMU1.EmptyText = "[ Наследуется ]";
      efpMU1.MaxTextItemCount = 10;

      cbUnit2Presence.Items.AddRange(Tools.PresenceTypeNames);
      efpUnit2Presence = new EFPListComboBox(page.BaseProvider, cbUnit2Presence);
      args.AddInt(efpUnit2Presence, "Unit2Presence", true);

      efpMU2 = new EFPAllSubDocComboBox(page.BaseProvider, cbMU2, _Editor, _Editor.Documents[0].SubDocs["ProductMUs2"]);
      efpMU2.EmptyText = "[ Наследуется ]";
      efpMU2.MaxTextItemCount = 10;

      efpParent.DocIdEx.ValueChanged += new EventHandler(efpParent_ValueChanged);
      efpParent_ValueChanged(null, null);
    }

    void efpParent_ValueChanged(object sender, EventArgs args)
    {
      Int32 parentId = efpParent.DocId;
      ProductBuffer.ProductData pd = ProductBuffer.GetProductData(parentId);

      cbDescriptionPresence.Items[0] = "Унаследовано - " + Tools.ToString(pd.DescriptionPresence);
      cbUnit1Presence.Items[0] = "Унаследовано - " + Tools.ToString(pd.Unit1Presence);
      cbUnit2Presence.Items[0] = "Унаследовано - " + Tools.ToString(pd.Unit2Presence);
    }


    #endregion

    #endregion

    #region Редактор единицы измерения

    public static void BeforeEditMU(object sender, BeforeSubDocEditEventArgs args)
    {
      args.ShowEditor = false;

      DocSelectDialog dlg;
      switch (args.Editor.State)
      { 
        case EFPDataGridViewState.Insert:
          dlg=new DocSelectDialog(ProgramDBUI.TheUI.DocTypes["MUs"]);
          dlg.Title="Добавление единицы измерения";
          if (dlg.ShowDialog() == DialogResult.OK)
            args.Editor.SubDocs.Values["MU"].SetInteger(dlg.DocId);
          else
            args.Cancel = true;
          break;

        case EFPDataGridViewState.Edit:
          dlg = new DocSelectDialog(ProgramDBUI.TheUI.DocTypes["MUs"]);
          dlg.Title = "Замена единицы измерения";
          dlg.DocId = args.Editor.SubDocs.Values["MU"].AsInteger;
          if (dlg.ShowDialog() == DialogResult.OK)
            args.Editor.SubDocs.Values["MU"].SetInteger(dlg.DocId);
          else
            args.Cancel = true;
          break;

        case EFPDataGridViewState.View:
          ProgramDBUI.TheUI.DocTypes["MUs"].PerformEditing(args.Editor.SubDocs.Values["MU"].AsInteger, true);
          break;

        case EFPDataGridViewState.Delete:
          break;
      }
    }

    #endregion
  }
}