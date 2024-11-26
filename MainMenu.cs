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
  /// Главное меню программы
  /// </summary>
  internal static class MainMenu
  {
    #region Инициализация

    public static void Init()
    {
      EFPAppToolBarCommandItems speedPanelStandard = EFPApp.ToolBars.Add("Standard", "Стандартная");

      EFPCommandItem ci;

      EFPAppCommandItemHelpers helpers = new EFPAppCommandItemHelpers();

      #region Файл

      EFPCommandItem menuFile = EFPApp.CommandItems.Add(EFPAppStdCommandItems.MenuFile, null);

      helpers.AddExit(menuFile);
      helpers.Exit.GroupBegin = true;

      #endregion

      #region Правка

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

      #region Вид

      EFPCommandItem menuView = EFPApp.CommandItems.Add(EFPAppStdCommandItems.MenuView, null);

      helpers.AddViewMenuCommands(menuView);

      #endregion

      #region Журналы

      EFPCommandItem menuRegs = new EFPCommandItem(null, "Журналы");
      menuRegs.MenuText = "&Журналы";
      EFPApp.CommandItems.Add(menuRegs);

      ci = ProgramDBUI.TheUI.DocTypes["Operations"].CreateMainMenuItem(menuRegs);
      speedPanelStandard.Add(ci);

      #endregion

      #region Справочники

      EFPCommandItem menuRB = new EFPCommandItem(null, "Справочники");
      menuRB.MenuText = "Сп&равочники";
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

      #region Отчеты

      EFPCommandItem menuReports = new EFPCommandItem(null, "Reports");
      menuReports.MenuText = "Отчеты";
      EFPApp.CommandItems.Add(menuReports);

      ci = new EFPCommandItem("Reports", "TurnoverStatement");
      ci.Parent = menuReports;
      ci.MenuText = "Оборотная ведомость";
      ci.ImageKey = "TurnoverStatement";
      ci.Click += new EventHandler(ciTurnoverStatement_Click);
      ci.GroupBegin = true;
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);

      ci = new EFPCommandItem("Reports", "PurchaseReport");
      ci.Parent = menuReports;
      ci.MenuText = "Покупки";
      ci.ImageKey = "PurchaseReport";
      ci.Click += new EventHandler(ciPurchaseReport_Click);
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);

      ci = new EFPCommandItem("Reports", "DebtReport");
      ci.Parent = menuReports;
      ci.MenuText = "Долги";
      ci.ImageKey = "DebtReport";
      ci.Click += new EventHandler(ciDebtReport_Click);
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);

      ci = new EFPCommandItem("Reports", "FeedbackReport");
      ci.Parent = menuReports;
      ci.MenuText = "Отзывы";
      ci.ImageKey = "FeedbackReport";
      ci.Click += new EventHandler(ciFeedbackReport_Click);
      ci.GroupEnd = true;
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);

      #endregion

      #region Сервис

      EFPCommandItem menuService = new EFPCommandItem(null, "Сервис");
      menuService.MenuText = "С&ервис";
      EFPApp.CommandItems.Add(menuService);

      ci = new EFPCommandItem("Сервис", "Настройки");
      ci.Parent = menuService;
      ci.MenuText = "Настройки";
      ci.ImageKey = "Settings";
      ci.Click += new EventHandler(ciSetting_Click);
      ci.GroupBegin = true;
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);


      ci = new EFPCommandItem("Сервис", "СменитьПароль");
      ci.Parent = menuService;
      ci.MenuText = "Сменить пароль";
      ci.ImageKey = "Password";
      ci.Click += new EventHandler(ChangePasswordForm.ChangePassword);
      ci.GroupEnd = true;
      EFPApp.CommandItems.Add(ci);
      speedPanelStandard.Add(ci);


      ci = new EFPCommandItem("Сервис", "ПросмотрДействийПользователя");
      ci.Parent = menuService;
      ci.MenuText = "Просмотр действий пользователя";
      ci.ImageKey = "UserActions";
      ci.Click += new EventHandler(ciUserActions_Click);
      EFPApp.CommandItems.Add(ci);
      //SpeedPanelStandard.Add(ci);

      #endregion

      #region Отладка

      EFPCommandItem menuDebug = new EFPCommandItem(null, "Отладка");
      menuDebug.MenuText = "Отладка";
      menuDebug.ImageKey = "Debug";
      menuDebug.Parent = menuService;
      menuDebug.GroupBegin = true;
      menuDebug.GroupEnd = true;
      EFPApp.CommandItems.Add(menuDebug);

      ci = new EFPCommandItem("Отладка", "ПоказыватьИдентификаторы");
      ci.Parent = menuDebug; 
      ci.MenuText = "Показывать идентификаторы";
      ci.ImageKey = "ПоказыватьИдентификаторы";
      ci.Click += new EventHandler(ShowIds_Click);
      EFPApp.CommandItems.Add(ci);

      #endregion

      #region Окно

      EFPCommandItem menuWindow = EFPApp.CommandItems.Add(EFPAppStdCommandItems.MenuWindow, null);
      helpers.AddWindowMenuCommands(menuWindow);

      #endregion

      #region Справка

      EFPCommandItem menuHelp = EFPApp.CommandItems.Add(EFPAppStdCommandItems.MenuHelp, null);

      ci = EFPApp.CommandItems.CreateContext(EFPAppStdCommandItems.About);
      ci.Parent = menuHelp;
      ci.Click += new EventHandler(AboutClick);
      EFPApp.CommandItems.Add(ci);

      #endregion

      // Делаем видимыми или невидимыми нужные команды
      EFPApp.CommandItems.InitMenuVisible();
    }

    #endregion

    #region Методы выполнения команд

    #region Вид

    static void RestoreBars_Click(object sender, EventArgs args)
    {
      foreach (EFPAppToolBar toolBar in EFPApp.AppToolBars)
      {
        toolBar.Reset();
        if (toolBar.Name == "Задачи")
          toolBar.Visible = false;
      }
      EFPApp.AppToolBars.Attach();

      // ClientStatusBar.StatusBar.Visible = true;
    }

    #endregion

    #region Меню "Отчеты"

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

    #region Меню "Сервис"

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
      // Также включаем другие отладочные флаги
      EFPFormProvider.DebugFormProvider = ProgramDBUI.TheUI.DebugShowIds;
      EFPPasteHandler.PasteSpecialDebugMode = ProgramDBUI.TheUI.DebugShowIds;

//#if DEBUG
//      ci.StatusBarText = Accoo2DBUI.TheUI.DebugShowIds ? "Вкл" : "Выкл";
//#endif
    }

    static void ciUserActions_Click(object sender, EventArgs args)
    {
      ProgramDBUI.TheUI.ShowUserActions();
    }

    #endregion

    #region Меню "Отладка"

    static private void ShowIds_Click(object sender, EventArgs args)
    {
      EFPCommandItem ci = (EFPCommandItem)sender;
      ProgramDBUI.TheUI.DebugShowIds = !ProgramDBUI.TheUI.DebugShowIds;
      ci.Checked = ProgramDBUI.TheUI.DebugShowIds;
      // Также включаем другие отладочные флаги
      EFPFormProvider.DebugFormProvider = ProgramDBUI.TheUI.DebugShowIds;
      EFPPasteHandler.PasteSpecialDebugMode = ProgramDBUI.TheUI.DebugShowIds;
    }

    #endregion

    #region Меню "Инфо"

    public static void AboutClick(object sender, EventArgs args)
    {
      AboutDialog dlg = new AboutDialog(Assembly.GetExecutingAssembly());
      dlg.ShowDialog();
    }

    #endregion

    #endregion
  }
}
