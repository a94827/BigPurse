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

namespace App
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

      dt.GridProducer.Columns.AddDate("Date", "Дата");
      dt.GridProducer.Columns.AddInt("OpOrder", "П.", 3);
      dt.GridProducer.Columns.LastAdded.DisplayName = "Порядок операции в пределах даты";
      dt.GridProducer.Columns.AddInt("OpOrder2", "OpOrder2", 3); // !!!

      dt.GridProducer.Columns.AddText("DisplayName", "Содержание", 40, 30);

      dt.GridProducer.Columns.AddUserMoney("Total", "TotalDebt,TotalCredit,InlineSum",
        new EFPGridProducerValueNeededEventHandler(EditOperation.Total_ValueNeeded),
        "Сумма");
      dt.GridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;
      dt.GridProducer.Columns.AddMoney("TotalDebt", "Сумма дебет");
      dt.GridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;
      dt.GridProducer.Columns.AddMoney("TotalCredit", "Сумма кредит");
      dt.GridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;

      dt.GridProducer.Columns.AddUserText("Wallet_Text", "WalletDebt,WalletCredit",
        new EFPGridProducerValueNeededEventHandler(EditOperation.Wallet_Text_ValueNeeded),
        "Кошелек", 15, 10);
      dt.GridProducer.Columns.LastAdded.SizeGroup = "WalletText";
      dt.GridProducer.Columns.AddRefDocText("WalletDebt", this.DocTypes["Wallets"], "Кошелек дебет", 15, 10);
      dt.GridProducer.Columns.LastAdded.SizeGroup = "WalletText";
      dt.GridProducer.Columns.AddRefDocText("WalletCredit", this.DocTypes["Wallets"], "Кошелек кредит", 15, 10);
      dt.GridProducer.Columns.LastAdded.SizeGroup = "WalletText";

      dt.GridProducer.Columns.AddText("Comment", "Комментарий", 30, 10);

      dt.GridProducer.ToolTips.AddText("DisplayName", "Содержание");
      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "Комментарий (если задан)";

      dt.GridProducer.Orders.Add("Date,OpOrder,OpOrder2,Id", "Основной порядок");
      dt.GridProducer.FixedColumns.Add("Date");
      dt.GridProducer.FixedColumns.Add("OpOrder");
      dt.GridProducer.FixedColumns.Add("OpOrder2");
      dt.GridProducer.FixedColumns.Add("Id");

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.Add("Date");
      dt.GridProducer.DefaultConfig.Columns.AddFill("DisplayName", 100);
      dt.GridProducer.DefaultConfig.Columns.Add("Total");
      dt.GridProducer.DefaultConfig.Columns.Add("Wallet_Text");
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.AddImageHandler("Operation", new DBxColumns("OpType"), new DBxImageValueNeededEventHandler(EditOperation.ImageValueNeeded));
      dt.InitView += new InitEFPDBxViewEventHandler(EditOperation.InitView);

      dt.BeforeEdit += new BeforeDocEditEventHandler(EditOperation.BeforeEditDoc);
      dt.InitEditForm += new InitDocEditFormEventHandler(EditOperation.InitDocEditForm);
      dt.CanInsertCopy = true;
      dt.CanMultiEdit = true;

      dt.Columns["Date"].DefaultValue = DateTime.Today;
      dt.Columns["Date"].NewMode = ColumnNewMode.SavedIfChangedElseDefault;
      dt.Columns["OpOrder"].NewMode = ColumnNewMode.SavedIfChangedElseDefault;
      dt.Columns["OpType"].NewMode = ColumnNewMode.Saved;
      dt.Columns["WalletDebt"].NewMode = ColumnNewMode.Saved;
      dt.Columns["WalletCredit"].NewMode = ColumnNewMode.Saved;

      #endregion

      #region Продукты

      sdt = dt.SubDocTypes["OperationProducts"];
      sdt.GridProducer.Columns.AddText("Product.Name", "Товар, услуга", 20, 5);
      sdt.GridProducer.Columns.AddText("Description", "Описание", 20, 5);
      sdt.GridProducer.Columns.AddText("Quantity1", "Кол-во 1", 5, 2);
      sdt.GridProducer.Columns.LastAdded.Format = "0.###";
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "Quantity";
      sdt.GridProducer.Columns.AddText("MU1.Name", "Ед. изм. 1", 5, 2);
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "MUName";
      sdt.GridProducer.Columns.AddText("Quantity2", "Кол-во 2", 5, 2);
      sdt.GridProducer.Columns.LastAdded.Format = "0.###";
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "Quantity";
      sdt.GridProducer.Columns.AddText("MU2.Name", "Ед. изм. 2", 5, 2);
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "MUName";
      sdt.GridProducer.Columns.AddText("Formula", "Формула", 15, 5);
      sdt.GridProducer.Columns.AddMoney("RecordSum", "Сумма");
      sdt.GridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;
      sdt.GridProducer.Columns.AddText("Comment", "Комментарий", 30, 10);

      sdt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "Комментарий (если задан)";

      sdt.GridProducer.DefaultConfig = new EFPDataGridViewConfig();
      sdt.GridProducer.DefaultConfig.Columns.AddFill("Product.Name", 50);
      sdt.GridProducer.DefaultConfig.Columns.AddFill("Description", 50);
      sdt.GridProducer.DefaultConfig.Columns.Add("RecordSum");
      sdt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      sdt.ImageKey = "Product";
      sdt.CanInsertCopy = true;
      sdt.InitEditForm += new InitSubDocEditFormEventHandler(EditOperationProduct.InitSubDocEditForm);

      sdt.Columns["Product"].NewMode = ColumnNewMode.Saved;

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

      #region Основной документ

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

      #region Списки единиц измерения

      sdt = dt.SubDocTypes["ProductMUs1"];
      sdt.GridProducer.Columns.AddText("Name", "Название", 20, 5);
      sdt.GridProducer.NewDefaultConfig(false);
      sdt.GridProducer.DefaultConfig.Columns.AddFill("Name");
      sdt.ImageKey = "MU";
      sdt.CanMultiEdit = false;
      sdt.CanInsertCopy = false;
      sdt.BeforeEdit+=new BeforeSubDocEditEventHandler(EditProduct.BeforeEditMU);

      #endregion

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

      #region Единицы измерения

      dt = base.DocTypes["MUs"];

      dt.GridProducer.Columns.AddText("Name", "Название", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "Комментарий", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "Комментарий (если задан)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.ImageKey = "MU";

      dt.InitEditForm += new InitDocEditFormEventHandler(EditMU.InitDocEditForm);
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
