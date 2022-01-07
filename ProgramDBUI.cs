using System;
using System.Collections.Generic;
using System.Text;
using FreeLibSet.Forms.Docs;
using FreeLibSet.Data.Docs;
using FreeLibSet.Data;
using System.Windows.Forms;
using FreeLibSet.Forms;
using System.ComponentModel;
using FreeLibSet.Core;

namespace BigPurse
{
  internal class ProgramDBUI : DBUI
  {
    #region Конструктор

    public ProgramDBUI(DBxDocProviderProxy sourceDocProviderProxy)
      : base(sourceDocProviderProxy)
    {
      DocTypeUI dt;
      SubDocTypeUI sdt;

      #region Операции

      #region Основной документ

      dt = base.DocTypes["Operations"];

      dt.GridProducer.Columns.AddDate("Date");

      dt.GridProducer.Columns.AddText("DisplayName", "Содержание", 40, 30);
      dt.GridProducer.Columns.AddText("Comment", "Комментарий", 30, 10);

      dt.GridProducer.ToolTips.AddText("DisplayName", "Содержание");
      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "Комментарий (если задан)";


      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.Add("Date");
      dt.GridProducer.DefaultConfig.Columns.AddFill("DisplayName", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.AddImageHandler("Operation", new DBxColumns("OpType"), new DBxImageValueNeededEventHandler(EditOperation.ImageValueNeeded));
      dt.BeforeEdit += new BeforeDocEditEventHandler(EditOperation.BeforeEditDoc);
      dt.InitEditForm += new InitDocEditFormEventHandler(EditOperation.InitDocEditForm);
      dt.CanInsertCopy = true;
      dt.CanMultiEdit = true;

      #endregion

      #region Продукты

      sdt = dt.SubDocTypes["OperationProducts"];
      sdt.GridProducer.Columns.AddText("Product.Name", "Товар, услуга", 20, 5);
      sdt.GridProducer.Columns.AddMoney("RecordSum", "Сумма");
      sdt.GridProducer.Columns.AddText("Comment", "Комментарий", 30, 10);

      sdt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "Комментарий (если задан)";

      sdt.GridProducer.DefaultConfig = new EFPDataGridViewConfig();
      sdt.GridProducer.DefaultConfig.Columns.AddFill("Product.Name", 100);
      sdt.GridProducer.DefaultConfig.Columns.Add("RecordSum");
      sdt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      sdt.CanInsertCopy = true;
      //sdt.InitEditForm += new InitSubDocEditFormEventHandler(EditOperationProduct.InitEditForm);
      sdt.ImageKey = "Product";

      #endregion

      #endregion

      #region Кошельки

      dt = base.DocTypes["Wallets"];

      dt.GridProducer.Columns.AddText("Name", "Название", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "Комментарий", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "Комментарий (если задан)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.ImageKey = "Wallet";

      dt.InitEditForm += new InitDocEditFormEventHandler(EditWallet.InitDocEditForm);
      dt.CanInsertCopy = true;
      dt.CanMultiEdit = true; 
      dt.DataBuffering = true;
      dt.Columns["Name"].NewMode = ColumnNewMode.AlwaysDefaultValue;

      #endregion

      #region Источники дохода

      dt = base.DocTypes["IncomeSources"];

      dt.GridProducer.Columns.AddText("Name", "Название", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "Комментарий", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "Комментарий (если задан)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.ImageKey = "IncomeSource";

      dt.InitEditForm += new InitDocEditFormEventHandler(EditIncomeSource.InitDocEditForm);
      dt.CanInsertCopy = true;
      dt.CanMultiEdit = true;
      dt.DataBuffering = true;
      dt.Columns["Name"].NewMode = ColumnNewMode.AlwaysDefaultValue;

      #endregion

      #region Магазины

      dt = base.DocTypes["Shops"];

      dt.GridProducer.Columns.AddText("Name", "Название", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "Комментарий", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "Комментарий (если задан)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.ImageKey = "Shop";

      dt.InitEditForm += new InitDocEditFormEventHandler(EditShop.InitDocEditForm);
      dt.CanInsertCopy = true;
      dt.CanMultiEdit = true;
      dt.DataBuffering = true;
      dt.Columns["Name"].NewMode = ColumnNewMode.AlwaysDefaultValue;

      #endregion

      #region Товары и услуги

      dt = base.DocTypes["Products"];

      dt.GridProducer.Columns.AddText("Name", "Название", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "Комментарий", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "Комментарий (если задан)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.ImageKey = "Product";

      dt.InitEditForm += new InitDocEditFormEventHandler(EditProduct.InitDocEditForm);
      dt.CanInsertCopy = true;
      dt.CanMultiEdit = true;
      dt.DataBuffering = true;
      dt.Columns["Name"].NewMode = ColumnNewMode.AlwaysDefaultValue;

      #endregion

      #region Должники

      dt = base.DocTypes["Debtors"];

      dt.GridProducer.Columns.AddText("Name", "Название", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "Комментарий", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "Комментарий (если задан)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.ImageKey = "Debtor";

      dt.InitEditForm += new InitDocEditFormEventHandler(EditDebtor.InitDocEditForm);
      dt.CanInsertCopy = true;
      dt.CanMultiEdit = true;
      dt.DataBuffering = true;
      dt.Columns["Name"].NewMode = ColumnNewMode.AlwaysDefaultValue;

      #endregion

      InitDocTextValues.Init(base.TextHandlers);
      base.EndInit();
    }

    private static void DateColumn_DefaultValueNeeded(object Sender, EventArgs Args)
    {
      ColumnUI ColUI = (ColumnUI)Sender;
      ColUI.DefaultValue = DateTime.Today;
    }

    #endregion

    #region Статические свойства

    public static ProgramDBUI TheUI;

    public static ClientConfigSections ConfigSections;

    //public static CoProDevDocuments Docs;

    //public static ProgramCache Cache;

    /// <summary>
    /// Настройки
    /// </summary>
    public static UserSettings Settings;

    #endregion
  }
}
