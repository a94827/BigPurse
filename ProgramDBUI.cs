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
using FreeLibSet.Reporting;

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
      //dt.GridProducer.Columns.AddInt("OpOrder2", "OpOrder2", 3); 

      dt.GridProducer.Columns.AddText("DisplayName", "����������", 40, 30);

      //dt.GridProducer.Columns.AddUserMoney("Total", "TotalDebt,TotalCredit,InlineSum",
      //  new EFPGridProducerValueNeededEventHandler(EditOperation.Total_ValueNeeded),
      //  "�����");
      dt.GridProducer.Columns.AddMoney("Total", "�����"); // 19.11.2023. ������ �� �������� ����������� ��������
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

      dt.GridProducer.Columns.AddUserText("Contra_Text", "OpType,IncomeSource,Shop,Debtor",
        new EFPGridProducerValueNeededEventHandler(EditOperation.Contra_Text_ValueNeeded),
        "����������", 15, 5);
      dt.GridProducer.Columns.LastAdded.DisplayName = "���������� (�������� ������, �������, �������, ��������)";
      dt.GridProducer.Columns.AddText("IncomeSource.Name", "�������� ������", 15, 5);
      dt.GridProducer.Columns.AddText("Shop.Name", "�������", 15, 5);
      dt.GridProducer.Columns.AddText("Debtor.Name", "�������/��������", 15, 5);

      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddText("DisplayName", "����������");
      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

      dt.GridProducer.Orders.Add("Date,OpOrder,OpOrder2,Id", "�� ���� (�� �����������)", new EFPDataGridViewSortInfo("Date", ListSortDirection.Ascending));
      dt.GridProducer.Orders.Add("Date DESC,OpOrder DESC,OpOrder2 DESC,Id", "�� ���� (�� ��������)", new EFPDataGridViewSortInfo("Date", ListSortDirection.Descending)); // 19.11.2023
      dt.GridProducer.FixedColumns.Add("Date");
      dt.GridProducer.FixedColumns.Add("OpOrder");
      dt.GridProducer.FixedColumns.Add("OpOrder2");
      dt.GridProducer.FixedColumns.Add("Id");

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.Add("Date");
      dt.GridProducer.DefaultConfig.Columns.AddFill("DisplayName", 100);
      dt.GridProducer.DefaultConfig.Columns.Add("Total");
      dt.GridProducer.DefaultConfig.Columns.Add("Wallet_Text");
      dt.GridProducer.DefaultConfig.Columns.Add("Contra_Text");
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.AddImageHandler("Operation", new DBxColumns("OpType"), new DBxImageValueNeededEventHandler(EditOperation.ImageValueNeeded));
      dt.InitView += new InitEFPDBxViewEventHandler(EditOperation.InitView);

      dt.BeforeEdit += new BeforeDocEditEventHandler(EditOperation.BeforeEditDoc);
      dt.InitEditForm += new InitDocEditFormEventHandler(EditOperation.InitDocEditForm);
      dt.CanInsertCopy = true;
      dt.CanMultiEdit = true;

      dt.Columns["Date"].NewMode = ColumnNewMode.SavedIfChangedElseDefault;
      //dt.Columns["Date"].DefaultValue = DateTime.Today;
      dt.Columns["Date"].DefaultValue = null; // ��� �������, ����� ������� ������� ����.
      dt.Columns["OpOrder"].NewMode = ColumnNewMode.SavedIfChangedElseDefault;
      dt.Columns["OpOrder"].DefaultValue = 0;
      dt.Columns["OpType"].NewMode = ColumnNewMode.Saved;
      dt.Columns["WalletDebt"].NewMode = ColumnNewMode.Saved;
      dt.Columns["WalletCredit"].NewMode = ColumnNewMode.Saved;

      dt.GridProducer.OutItem.Default.PageSetup.SetOrientation(BROrientation.Landscape, true);

      #endregion

      #region ��������

      sdt = dt.SubDocTypes["OperationProducts"];
      sdt.GridProducer.Columns.AddText("Product.Name", "�����, ������", 20, 5);
      sdt.GridProducer.Columns.AddText("Description", "��������", 20, 5);
      sdt.GridProducer.Columns.AddText("Purpose.Name", "����������", 20, 5);
      sdt.GridProducer.Columns.AddText("AuxPurpose.Name", "���. ����������", 20, 5);

      sdt.GridProducer.Columns.AddUserText("QuantityText", "Quantity1,MU1.Name,Quantity2,MU2.Name,Quantity3,MU3.Name",
        new EFPGridProducerValueNeededEventHandler(EditOperationProduct.QuantityTextColumnValueNeeded),
        "����������", 20, 10);

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

      sdt.GridProducer.Columns.AddText("Quantity3", "���-�� 3", 5, 2);
      sdt.GridProducer.Columns.LastAdded.Format = "0.###";
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "Quantity";
      sdt.GridProducer.Columns.AddText("MU3.Name", "��. ���. 3", 5, 2);
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "MUName";

      sdt.GridProducer.Columns.AddText("Formula", "�������", 15, 5);
      sdt.GridProducer.Columns.AddMoney("RecordSum", "�����");
      sdt.GridProducer.Columns.LastAdded.Format = Tools.MoneyFormat;
      sdt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      sdt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� - ����� � ������ (���� �����)";

      sdt.GridProducer.DefaultConfig = new EFPDataGridViewConfig();
      sdt.GridProducer.DefaultConfig.Columns.AddFill("Product.Name", 35);
      sdt.GridProducer.DefaultConfig.Columns.AddFill("Description", 35);
      sdt.GridProducer.DefaultConfig.Columns.AddFill("QuantityText", 30);
      sdt.GridProducer.DefaultConfig.Columns.Add("RecordSum");
      sdt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      sdt.ImageKey = "Product";
      sdt.CanInsertCopy = true;
      sdt.InitEditForm += new InitSubDocEditFormEventHandler(EditOperationProduct.InitSubDocEditForm);

      sdt.Columns["Product"].NewMode = ColumnNewMode.Saved;
      sdt.Columns["Purpose"].NewMode = ColumnNewMode.Saved;

      #endregion

      #endregion

      #region ��������

      dt = base.DocTypes["Wallets"];

      dt.GridProducer.Columns.AddText("Name", "��������", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;
      dt.GridProducer.Columns.AddDate("FirstDate", "������ ��������");
      dt.GridProducer.Columns.AddDate("LastDate", "��������� ��������");
      dt.GridProducer.Columns.AddBool("Deposit", "�����");
      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddDateRange("FirstDate", "LastDate", "������ ��������");
      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.AddImageHandler("Wallet", new DBxColumns("FirstDate,LastDate"), EditWallet.ImageValueNeeded);

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
      dt.GridProducer.Columns.AddDate("FirstDate", "������ ��������");
      dt.GridProducer.Columns.AddDate("LastDate", "��������� ��������");
      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddDateRange("FirstDate", "LastDate", "������ ��������");
      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.AddImageHandler("IncomeSource", new DBxColumns("FirstDate,LastDate"), EditIncomeSource.ImageValueNeeded);

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
      dt.GridProducer.Columns.AddDate("FirstDate", "������ ��������");
      dt.GridProducer.Columns.AddDate("LastDate", "��������� ��������");
      dt.GridProducer.Columns.AddText("GroupId.Name", "������", 15, 5);
      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddDateRange("FirstDate", "LastDate", "������ ��������");
      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.AddImageHandler("Shop", new DBxColumns("FirstDate,LastDate"), EditShop.ImageValueNeeded);

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

      dt.GridProducer.Columns.AddText("ParentId.Name", "������������ ������", 40, 15);

      dt.GridProducer.Columns.AddEnumText("DescriptionPresence", Tools.PresenceTypeNames, "������� ��������", 15, 5);
      dt.GridProducer.Columns.LastAdded.SizeGroup = "PresenceEnum";

      dt.GridProducer.Columns.AddEnumText("QuantityPresence", Tools.PresenceTypeNames, "������� ����������", 15, 5);
      dt.GridProducer.Columns.LastAdded.SizeGroup = "PresenceEnum";

      dt.GridProducer.Columns.AddInt("MUSetCount", "������ ��. ���.", 3);

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

      sdt = dt.SubDocTypes["ProductMUSets"];
      sdt.GridProducer.Columns.AddText("MU1.Name", "��.��� 1", 10, 5);
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "MUName";
      sdt.GridProducer.Columns.AddText("MU2.Name", "��.��� 2", 10, 5);
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "MUName";
      sdt.GridProducer.Columns.AddText("MU3.Name", "��.��� 3", 10, 5);
      sdt.GridProducer.Columns.LastAdded.SizeGroup = "MUName";

      sdt.GridProducer.NewDefaultConfig(false);
      sdt.GridProducer.DefaultConfig.Columns.AddFill("MU1.Name");
      sdt.GridProducer.DefaultConfig.Columns.AddFill("MU2.Name");
      sdt.GridProducer.DefaultConfig.Columns.AddFill("MU3.Name");
      sdt.ImageKey = "MU";
      sdt.CanMultiEdit = false;
      sdt.CanInsertCopy = true;
      sdt.InitEditForm += new InitSubDocEditFormEventHandler(EditProductMUSet.InitSubDocEditForm);

      #endregion

      #endregion

      #region ��������

      dt = base.DocTypes["Debtors"];

      dt.GridProducer.Columns.AddText("Name", "��������", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;
      dt.GridProducer.Columns.AddDate("FirstDate", "������ ��������");
      dt.GridProducer.Columns.AddDate("LastDate", "��������� ��������");
      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddDateRange("FirstDate", "LastDate", "������ ��������");
      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.AddImageHandler("Debtor", new DBxColumns("FirstDate,LastDate"), EditDebtor.ImageValueNeeded);

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

      #region ����������

      dt = base.DocTypes["Purposes"];

      dt.GridProducer.Columns.AddText("Name", "��������", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;
      dt.GridProducer.Columns.AddText("GroupId.Name", "������", 15, 5);
      dt.GridProducer.Columns.AddDate("FirstDate", "������ ��������");
      dt.GridProducer.Columns.AddDate("LastDate", "��������� ��������");
      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddDateRange("FirstDate", "LastDate", "������ ��������");
      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.AddImageHandler("Purpose", new DBxColumns("FirstDate,LastDate"), EditPurpose.ImageValueNeeded);

      dt.InitEditForm += new InitDocEditFormEventHandler(EditPurpose.InitDocEditForm);
      dt.CanInsertCopy = true;
      dt.CanMultiEdit = true;
      dt.DataBuffering = true;
      dt.Columns["Name"].NewMode = ColumnNewMode.AlwaysDefaultValue;

      #endregion

      #region ���. ����������

      dt = base.DocTypes["AuxPurposes"];

      dt.GridProducer.Columns.AddText("Name", "��������", 40, 15);
      dt.GridProducer.Columns.LastAdded.CanIncSearch = true;
      dt.GridProducer.Columns.AddText("Comment", "�����������", 30, 10);

      dt.GridProducer.ToolTips.AddText("Comment", String.Empty).DisplayName = "����������� (���� �����)";

      dt.GridProducer.NewDefaultConfig(false);
      dt.GridProducer.DefaultConfig.Columns.AddFill("Name", 100);
      dt.GridProducer.DefaultConfig.ToolTips.Add("Comment");

      dt.ImageKey = "AuxPurpose";

      dt.InitEditForm += new InitDocEditFormEventHandler(EditAuxPurpose.InitDocEditForm);
      dt.CanInsertCopy = false;
      dt.CanMultiEdit = true;
      dt.DataBuffering = true;
      dt.Columns["Name"].NewMode = ColumnNewMode.AlwaysDefaultValue;

      #endregion

      InitDocTextValues.Init(base.TextHandlers);
      base.EndInit();
    }

    private static void DateColumn_DefaultValueNeeded(object Sender, EventArgs Args)
    {
      ColumnUI dolUI = (ColumnUI)Sender;
      dolUI.DefaultValue = DateTime.Today;
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
