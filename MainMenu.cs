using System;
using System.Collections.Generic;
using System.Text;
using FreeLibSet.Forms;
using FreeLibSet.IO;
using System.Windows.Forms;
using System.Reflection;

namespace App
{
  /// <summary>
  /// ������� ���� ���������
  /// </summary>
  internal static class MainMenu
  {
    #region �������������

    public static void Init()
    {
      EFPAppToolBarCommandItems speedPanelStandard = EFPApp.ToolBars.Add("Standard", "�����������");

      EFPCommandItem ci;

      EFPAppCommandItemHelpers helpers = new EFPAppCommandItemHelpers();

      #region ����

      EFPCommandItem menuFile = EFPApp.CommandItems.Add(EFPAppStdCommandItems.MenuFile, null);

      helpers.AddExit(menuFile);
      helpers.Exit.GroupBegin = true;

      #endregion

      #region ������

      EFPCommandItem menuEdit = EFPApp.CommandItems.Add(EFPAppStdCommandItems.MenuEdit, null);

      ci = EFPApp.CommandItems.Add(EFPAppStdCommandItems.Cut, menuEdit);
      ci.Enabled = false;
      ci.GroupBegin = true;
      //SpeedPanelStandard.Add(Cut);

      ci = EFPApp.CommandItems.Add(EFPAppStdCommandItems.Copy, menuEdit);
      ci.Enabled = false;
      //SpeedPanelStandard.Add(Copy);

      ci = EFPApp.CommandItems.Add(EFPAppStdCommandItems.Paste, menuEdit);
      ci.Enabled = false;
      //SpeedPanelStandard.Add(Paste);

      ci = EFPApp.CommandItems.Add(EFPAppStdCommandItems.PasteSpecial, menuEdit);
      ci.Enabled = false;
      ci.GroupEnd = true;

      ci = EFPApp.CommandItems.Add(EFPAppStdCommandItems.Find, menuEdit);
      ci.Enabled = false;
      ci.GroupBegin = true;
      //SpeedPanelStandard.Add(Find);

      ci = EFPApp.CommandItems.Add(EFPAppStdCommandItems.IncSearch, menuEdit);
      ci.Enabled = false;
      //SpeedPanelStandard.Add(IncSearch);

      ci = EFPApp.CommandItems.Add(EFPAppStdCommandItems.FindNext, menuEdit);
      ci.Enabled = false;
      //SpeedPanelStandard.Add(FindNext);

      //ci = EFPApp.CommandItems.Add(EFPAppStdCommandItems.Replace, MenuEdit);
      //ci.Enabled = false;
      //SpeedPanelStandard.Add(Replace);

      //ci = EFPApp.CommandItems.Add(EFPAppStdCommandItems.Goto, MenuEdit);
      //ci.Enabled = false;
      //ci.GroupEnd = true;

      ci = EFPApp.CommandItems.Add(EFPAppStdCommandItems.SelectAll, menuEdit);
      ci.Enabled = false;
      ci.GroupBegin = true;
      ci.GroupEnd = true;

      #endregion

      #region ���

      EFPCommandItem menuView = EFPApp.CommandItems.Add(EFPAppStdCommandItems.MenuView, null);

      helpers.AddViewMenuCommands(menuView);

      #endregion

      #region �������

      EFPCommandItem menuRegs = new EFPCommandItem(null, "�������");
      menuRegs.MenuText = "&�������";
      EFPApp.CommandItems.Add(menuRegs);

      ci = ProgramDBUI.TheUI.DocTypes["Operations"].CreateMainMenuItem(menuRegs);
      speedPanelStandard.Add(ci);

      #endregion

      #region �����������

      EFPCommandItem menuRB = new EFPCommandItem(null, "�����������");
      menuRB.MenuText = "��&���������";
      EFPApp.CommandItems.Add(menuRB);

      ci = ProgramDBUI.TheUI.DocTypes["Wallets"].CreateMainMenuItem(menuRB);
      speedPanelStandard.Add(ci);

      ci = ProgramDBUI.TheUI.DocTypes["IncomeSources"].CreateMainMenuItem(menuRB);
      speedPanelStandard.Add(ci);

      ci = ProgramDBUI.TheUI.DocTypes["Shops"].CreateMainMenuItem(menuRB);
      speedPanelStandard.Add(ci);

      ci = ProgramDBUI.TheUI.DocTypes["Products"].CreateMainMenuItem(menuRB);
      speedPanelStandard.Add(ci);

      ci = ProgramDBUI.TheUI.DocTypes["MUs"].CreateMainMenuItem(menuRB);
      speedPanelStandard.Add(ci);

      ci = ProgramDBUI.TheUI.DocTypes["Debtors"].CreateMainMenuItem(menuRB);
      speedPanelStandard.Add(ci);

      ci = ProgramDBUI.TheUI.DocTypes["Purposes"].CreateMainMenuItem(menuRB);
      speedPanelStandard.Add(ci);

      #endregion

      #region ������

      EFPCommandItem menuReports = new EFPCommandItem(null, "Reports");
      menuReports.MenuText = "������";
      EFPApp.CommandItems.Add(menuReports);

      ci = new EFPCommandItem("Reports", "TurnoverStatement");
      ci.Parent = menuReports;
      ci.MenuText = "��������� ���������";
      ci.ImageKey = "TurnoverStatement";
      ci.Click += new EventHandler(ciTurnoverStatement_Click);
      ci.GroupBegin = true;
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);

      ci = new EFPCommandItem("Reports", "PurchaseReport");
      ci.Parent = menuReports;
      ci.MenuText = "�������";
      ci.ImageKey = "PurchaseReport";
      ci.Click += new EventHandler(ciPurchaseReport_Click);
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);

      ci = new EFPCommandItem("Reports", "DebtReport");
      ci.Parent = menuReports;
      ci.MenuText = "�����";
      ci.ImageKey = "DebtReport";
      ci.Click += new EventHandler(ciDebtReport_Click);
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);

      ci = new EFPCommandItem("Reports", "FeedbackReport");
      ci.Parent = menuReports;
      ci.MenuText = "������";
      ci.ImageKey = "FeedbackReport";
      ci.Click += new EventHandler(ciFeedbackReport_Click);
      ci.GroupEnd = true;
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);

      #endregion

      #region ������

      EFPCommandItem menuService = new EFPCommandItem(null, "������");
      menuService.MenuText = "�&�����";
      EFPApp.CommandItems.Add(menuService);

      ci = new EFPCommandItem("������", "���������");
      ci.Parent = menuService;
      ci.MenuText = "���������";
      ci.ImageKey = "Settings";
      ci.Click += new EventHandler(ciSetting_Click);
      ci.GroupBegin = true;
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);


      ci = new EFPCommandItem("������", "�������������");
      ci.Parent = menuService;
      ci.MenuText = "������� ������";
      ci.ImageKey = "Password";
      ci.Click += new EventHandler(ChangePasswordForm.ChangePassword);
      ci.GroupEnd = true;
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);


      ci = new EFPCommandItem("������", "����������������������������");
      ci.Parent = menuService;
      ci.MenuText = "�������� �������� ������������";
      ci.ImageKey = "UserActions";
      ci.Click += new EventHandler(ciUserActions_Click);
      EFPApp.CommandItems.Add(ci);
      //SpeedPanelStandard.Add(ci);

      #endregion

      #region �������

      EFPCommandItem menuDebug = new EFPCommandItem(null, "�������");
      menuDebug.MenuText = "�������";
      menuDebug.ImageKey = "Debug";
      menuDebug.Parent = menuService;
      menuDebug.GroupBegin = true;
      menuDebug.GroupEnd = true;
      EFPApp.CommandItems.Add(menuDebug);

      ci = new EFPCommandItem("�������", "������������������������");
      ci.Parent = menuDebug; 
      ci.MenuText = "���������� ��������������";
      ci.ImageKey = "������������������������";
      ci.Click += new EventHandler(ShowIds_Click);
      EFPApp.CommandItems.Add(ci);

      #endregion

      #region ����

      EFPCommandItem menuWindow = EFPApp.CommandItems.Add(EFPAppStdCommandItems.MenuWindow, null);
      helpers.AddWindowMenuCommands(menuWindow);

      #endregion

      #region �������

      EFPCommandItem menuHelp = EFPApp.CommandItems.Add(EFPAppStdCommandItems.MenuHelp, null);

      ci = EFPApp.CommandItems.CreateContext(EFPAppStdCommandItems.About);
      ci.Parent = menuHelp;
      ci.Click += new EventHandler(AboutClick);
      EFPApp.CommandItems.Add(ci);

      #endregion

      // ������ �������� ��� ���������� ������ �������
      EFPApp.CommandItems.InitMenuVisible();
    }

    #endregion

    #region ������ ���������� ������

    #region ���

    static void RestoreBars_Click(object sender, EventArgs args)
    {
      foreach (EFPAppToolBar toolBar in EFPApp.AppToolBars)
      {
        toolBar.Reset();
        if (toolBar.Name == "������")
          toolBar.Visible = false;
      }
      EFPApp.AppToolBars.Attach();

      // ClientStatusBar.StatusBar.Visible = true;
    }

    #endregion

    #region ���� "������"

    private static void ciTurnoverStatement_Click(object sender, EventArgs args)
    {
      new TurnoverStatement().Run();
    }

    private static void ciPurchaseReport_Click(object sender, EventArgs args)
    {
      new PurchaseReport().Run();
    }

    private static void ciDebtReport_Click(object sender, EventArgs args)
    {
      new DebtReport().Run();
    }

    private static void ciFeedbackReport_Click(object sender, EventArgs args)
    {
      new FeedbackReport().Run();
    }

    #endregion

    #region ���� "������"

    static void ciSetting_Click(object sender, EventArgs args)
    {
      UserSettingsForm form = new UserSettingsForm();
      form.ValueToForm(ProgramDBUI.Settings);
      if (EFPApp.ShowDialog(form, true) != DialogResult.OK)
        return;
      form.ValueFromForm(ProgramDBUI.Settings);
      ProgramDBUI.Settings.WriteConfig();
    }

    static private void ShowIds_Cllick(object sender, EventArgs args)
    {
      EFPCommandItem ci = (EFPCommandItem)sender;
      ProgramDBUI.TheUI.DebugShowIds = !ProgramDBUI.TheUI.DebugShowIds;
      ci.Checked = ProgramDBUI.TheUI.DebugShowIds;
      // ����� �������� ������ ���������� �����
      EFPFormProvider.DebugFormProvider = ProgramDBUI.TheUI.DebugShowIds;
      EFPPasteHandler.PasteSpecialDebugMode = ProgramDBUI.TheUI.DebugShowIds;

//#if DEBUG
//      ci.StatusBarText = Accoo2DBUI.TheUI.DebugShowIds ? "���" : "����";
//#endif
    }

    static void ciUserActions_Click(object sender, EventArgs args)
    {
      ProgramDBUI.TheUI.ShowUserActions();
    }

    #endregion

    #region ���� "�������"

    static private void ShowIds_Click(object sender, EventArgs args)
    {
      EFPCommandItem ci = (EFPCommandItem)sender;
      ProgramDBUI.TheUI.DebugShowIds = !ProgramDBUI.TheUI.DebugShowIds;
      ci.Checked = ProgramDBUI.TheUI.DebugShowIds;
      // ����� �������� ������ ���������� �����
      EFPFormProvider.DebugFormProvider = ProgramDBUI.TheUI.DebugShowIds;
      EFPPasteHandler.PasteSpecialDebugMode = ProgramDBUI.TheUI.DebugShowIds;
    }

    #endregion

    #region ���� "����"

    public static void AboutClick(object sender, EventArgs args)
    {
      AboutDialog dlg = new AboutDialog(Assembly.GetExecutingAssembly());
      dlg.ShowDialog();
    }

    #endregion

    #endregion
  }
}
