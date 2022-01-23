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

    EFPListComboBox efpDescriptionPresence, efpQuantityPresence;
    EFPSubDocGridView efpMUSets;

    private void AddPage2(InitDocEditFormEventArgs args)
    {
      DocEditPage page = args.AddPage("В операции", MainPanel2);
      page.ImageKey = "OperationExpense";


      cbDescriptionPresence.Items.AddRange(Tools.PresenceTypeNames);
      efpDescriptionPresence = new EFPListComboBox(page.BaseProvider, cbDescriptionPresence);
      args.AddInt(efpDescriptionPresence, "DescriptionPresence", true);


      cbQuantityPresence.Items.AddRange(Tools.PresenceTypeNames);
      efpQuantityPresence = new EFPListComboBox(page.BaseProvider, cbQuantityPresence);
      args.AddInt(efpQuantityPresence, "QuantityPresence", true);


      efpMUSets = new EFPSubDocGridView(page.BaseProvider, grMUSets, _Editor, _Editor.Documents[0].SubDocs["ProductMUSets"]);
      efpMUSets.ToolBarPanel = panSpbMUSets;

      efpParent.DocIdEx.ValueChanged += new EventHandler(efpParent_ValueChanged);
      efpParent_ValueChanged(null, null);
    }

    void efpParent_ValueChanged(object sender, EventArgs args)
    {
      Int32 parentId = efpParent.DocId;
      ProductBuffer.ProductData pd = ProductBuffer.GetProductData(parentId);

      cbDescriptionPresence.Items[0] = "Унаследовано - " + Tools.ToString(pd.DescriptionPresence);
      cbQuantityPresence.Items[0] = "Унаследовано - " + Tools.ToString(pd.QuantityPresence);
    }


    #endregion

    #endregion
  }
}