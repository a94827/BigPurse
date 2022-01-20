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
    #region �����������

    public ProgramDBUI(DBxDocProviderProxy sourceDocProviderProxy)
      : base(sourceDocProviderProxy)
    {
      DocTypeUI dt;
      SubDocTypeUI sdt;

      #region ��������

      #region �������� ��������

      dt = base.DocTypes["Operations"];

      dt.GridProducer.Columns.AddDate("Date", "����");
      dt.GridProducer.Columns.AddInt("OpOrder", "�.", 3);
      dt.GridProducer.Columns.LastAdded.DisplayName = "������� �������� � �������� ����";
      dt.GridProducer.Columns.AddInt("OpOrder2", "OpOrder2", 3); // !!!

      dt.GridProducer.Columns.AddText("DisplayName", "����������", 40, 30);

      dt.GridProducer.Columns.AddUserMoney("Total", "TotalDebt,TotalCredit,InlineSum",
        new EFPGridProducerValueNeededEventHandler(EditOperation.Total_ValueNeeded),
        "�����");
      dt.GridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;
      dt.GridProducer.Columns.AddMoney("TotalDebt", "����� �����");
      dt.GridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;
      dt.GridProducer.Columns.AddMoney("TotalCredit", "����� ������");
      dt.GridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;

      dt.GridProducer.Columns.AddUserText("Wallet_Text", "WalletDebt,WalletCredit",
        new EFPGridProducerValueNeededEventHandler(EditOperation.Wallet_Text_ValueNeeded),
        "�������", 15, 10);
      dt.GridProducer.Columns.LastAdded.SizeGroup = "WalletText";
      dt.GridProducer.Columns.AddRefDocText("WalletDebt", this.DocTypes["Wallets"], "������� �����", 15, 10);
      dt.GridProducer.Columns.LastAdded.SizeGroup = "WalletText";
      dt.GridProducer.Columns.AddRefDocText("WalletCredit", this.DocTypes["Wallets"], "������� ������", 15, 10);
      dt.GridProducer.Columns.LastAdded.SizeGroup = "WalletText";

      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddText("DisplayName", "����������");
      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

      dt.GridProducer.Orders.Add("Date,OpOrder,OpOrder2,Id", "�������� �������");
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

      #region ��������

      sdt = dt.SubDocTypes["OperationProducts"];
      sdt.GridProducer.Columns.AddText("Product.Name", "�����, ������", 20, 5);
      sdt.GridProducer.Columns.AddText("Description", "��������", 20, 5);
      sdt.GridProducer.Columns.AddText("Quantity1", "���-�� 1", 5, 2);
      sdt.GridProducer.Columns.LastAdded.Format = "0.###";
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "Quantity";
      sdt.GridProducer.Columns.AddText("MU1.Name", "��. ���. 1", 5, 2);
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "MUName";
      sdt.GridProducer.Columns.AddText("Quantity2", "���-�� 2", 5, 2);
      sdt.GridProducer.Columns.LastAdded.Format = "0.###";
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "Quantity";
      sdt.GridProducer.Columns.AddText("MU2.Name", "��. ���. 2", 5, 2);
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "MUName";
      sdt.GridProducer.Columns.AddText("Formula", "�������", 15, 5);
      sdt.GridProducer.Columns.AddMoney("RecordSum", "�����");
      sdt.GridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;
      sdt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      sdt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

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

      #region ��������

      dt = base.DocTypes["Wallets"];

      dt.GridProducer.Columns.AddText("Name", "��������", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

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

      #region ��������� ������

      dt = base.DocTypes["IncomeSources"];

      dt.GridProducer.Columns.AddText("Name", "��������", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

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

      #region ��������

      dt = base.DocTypes["Shops"];

      dt.GridProducer.Columns.AddText("Name", "��������", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

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

      #region ������ � ������

      #region �������� ��������

      dt = base.DocTypes["Products"];

      dt.GridProducer.Columns.AddText("Name", "��������", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

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

      #region ������ ������ ���������

      sdt = dt.SubDocTypes["ProductMUs1"];
      sdt.GridProducer.Columns.AddText("Name", "��������", 20, 5);
      sdt.GridProducer.NewDefaultConfig(false);
      sdt.GridProducer.DefaultConfig.Columns.AddFill("Name");
      sdt.ImageKey = "MU";
      sdt.CanMultiEdit = false;
      sdt.CanInsertCopy = false;
      sdt.BeforeEdit+=new BeforeSubDocEditEventHandler(EditProduct.BeforeEditMU);

      #endregion

      #endregion

      #region ��������

      dt = base.DocTypes["Debtors"];

      dt.GridProducer.Columns.AddText("Name", "��������", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

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

      #region ������� ���������

      dt = base.DocTypes["MUs"];

      dt.GridProducer.Columns.AddText("Name", "��������", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;

      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

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

    #region ����������� ��������

    public static ProgramDBUI TheUI;

    public static ClientConfigSections ConfigSections;

    //public static CoProDevDocuments Docs;

    //public static ProgramCache Cache;

    /// <summary>
    /// ���������
    /// </summary>
    public static UserSettings Settings;

    #endregion
  }
}
