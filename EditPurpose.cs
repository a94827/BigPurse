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
using FreeLibSet.Core;

namespace App
{
  internal partial class EditPurpose : Form
  {
    #region Конструктор формы

    public EditPurpose()
    {
      InitializeComponent();
    }

    #endregion

    #region Табличный просмотр

    public static void ImageValueNeeded(object sender, DBxImageValueNeededEventArgs args)
    {
      DateTime? dt1 = args.GetNullableDateTime("FirstDate");
      DateTime? dt2 = args.GetNullableDateTime("LastDate");
      if (!DataTools.DateInRange(DateTime.Today, dt1, dt2))
        args.ImageKey = "No";
    }

    #endregion

    #region Редактор

    #region InitDocEditForm

    DocumentEditor _Editor;

    public static void InitDocEditForm(object sender, InitDocEditFormEventArgs args)
    {
      EditPurpose form = new EditPurpose();
      form._Editor = args.Editor;
      form.AddPage1(args);
    }

    #endregion

    #region Страница 1 (общие)

    private EFPTextBox efpName;

    private void AddPage1(InitDocEditFormEventArgs args)
    {
      ExtEditPage page = args.AddPage("Общие", MainPanel1);

      efpName = new EFPTextBox(page.BaseProvider, edName);
      efpName.CanBeEmpty = false;
      args.AddText(efpName, "Name", false);

      EFPDateRangeBox efpPeriod = new EFPDateRangeBox(page.BaseProvider, edPeriod);
      efpPeriod.First.CanBeEmpty = true;
      efpPeriod.Last.CanBeEmpty = true;
      args.AddDate(efpPeriod, "FirstDate", "LastDate", true);

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