using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FreeLibSet.Forms;
using FreeLibSet.Logging;
using FreeLibSet.IO;
using FreeLibSet.Forms.Docs;
using FreeLibSet.Core;

namespace App
{
  static class Program
  {
    #region Main()

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      EFPApp.InitApp();
      try
      {
        using (ProgramDB db = new ProgramDB())
        {
          using (Splash spl = new Splash(new string[] { 
          "Инициализация базы данных", 
          "Аутентификация",
          "Инициализация главного окна"}))
          {
            #region Инициализация каталогов

            LogoutTools.LogBaseDirectory = FileTools.ApplicationBaseDir + "Log";
            if (!CreateDir(LogoutTools.LogBaseDirectory))
              return;

            TempDirectory.RootDir = FileTools.ApplicationBaseDir + "Temp";
            if (!CreateDir(TempDirectory.RootDir))
              return;

            AbsPath dbDir = FileTools.ApplicationBaseDir + "DB";
            if (!CreateDir(dbDir))
              return;

            #endregion

            db.InitDB(dbDir, spl);

            ProgramDBUI.Settings = new UserSettings();
            ProgramDBUI.Settings.ReadConfig();

            BalanceCalc.MainEntry = db.MainEntry;

            spl.Complete();

            ProgramDBUI.TheUI = new ProgramDBUI(db.CreateDocProvider().CreateProxy());
            // Картинки
            EFPApp.MainImages.Add(MainImagesResource.ResourceManager);

            //ProgramDBUI.TheUI.DebugShowIds = true; // показывать идентификаторы для отладки
            ProgramDBUI.ConfigSections = new ClientConfigSections(db);
            EFPApp.ConfigManager = ProgramDBUI.ConfigSections; // должно быть до показа форм


            if (!LoginForm.ProcessLogin())
              return;
            spl.Complete();

            #region Главное окно

            MainMenu.Init();
            EFPApp.Interface = new EFPAppInterfaceSDI();
            EFPApp.MainWindowTitle = "Кошелек";
            EFPApp.LoadMainWindowLayout();
            EFPApp.FormCreators.Add(ProgramDBUI.TheUI);
            EFPApp.LoadComposition();

            // 02.07.2023, убрано 07.10.2024
            //if (EFPApp.Interface.ChildFormCount == 0)
            //  EFPApp.Interface.ShowChildForm(new DocTableViewForm(ProgramDBUI.TheUI.DocTypes["Operations"], DocTableViewMode.Browse));
            ProgramDBUI.TheUI.DocTypes["Operations"].ShowOrOpen(null);

            EFPApp.BeforeClosing += new System.ComponentModel.CancelEventHandler(EFPApp_BeforeClosing);

            #endregion

            spl.Complete();
          }

          Application.Run();

          if (ProgramDBUI.Settings.BackupMode != UserSettings.BackupModes.None)
          {
            using (Splash spl = new Splash("Создание резервной копии"))
            {
              switch (ProgramDBUI.Settings.BackupMode)
              {
                case UserSettings.BackupModes.AfterEveryRun:
                  db.CreateBackup(spl);
                  break;
                case UserSettings.BackupModes.EveryDay:
                  if (!HasDailyBackup())
                    db.CreateBackup(spl);
                  break;
              }
              db.RemoveOldBackups(spl);
            }
          }

          ProgramDBUI.ConfigSections = null;
        }
      }
      catch (Exception e)
      {
        EFPApp.ShowException(e, "Ошибка запуска программы");
      }
    }
    static void EFPApp_BeforeClosing(object sender, System.ComponentModel.CancelEventArgs args)
    {
      EFPApp.SaveComposition();
      EFPApp.SaveMainWindowLayout();
    }

    private static bool CreateDir(AbsPath dir)
    {
      try
      {
        FileTools.ForceDirs(dir);
        return true;
      }
      catch (System.IO.IOException e)
      {
        EFPApp.ErrorMessageBox("Невозможно создать каталог \"" + dir.Path + "\". " + e.Message);
        return false;
      }
    }

    /// <summary>
    /// Возвращает true, если сегодня уже была создана резервная копия
    /// </summary>
    /// <returns></returns>
    private static bool HasDailyBackup()
    {
      string s1 = DateTime.Today.ToString(@"yyyyMMdd", StdConvert.DateTimeFormat);
      string[] aFiles = System.IO.Directory.GetFiles(ProgramDB.BackupDir.Path, s1 + "-??????.7z", System.IO.SearchOption.TopDirectoryOnly);
      return aFiles.Length > 0;
    }

    #endregion
  }
}