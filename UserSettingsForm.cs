using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FreeLibSet.Forms;
using FreeLibSet.IO;
using FreeLibSet.Config;
using FreeLibSet.DependedValues;

namespace App
{
  /// <summary>
  /// ������ "���������"
  /// </summary>
  internal partial class UserSettingsForm : Form
  {
    #region ����������� �����

    public UserSettingsForm()
    {
      InitializeComponent();
      Icon = EFPApp.MainImages.Icons["Settings"];

      EFPFormProvider efpForm = new EFPFormProvider(this);

      #region ��������� �����������

      efpBackupMode = new EFPListComboBox(efpForm, cbBackupMode);

      efpBackupDir = new EFPTextBox(efpForm, edBackupDir);
      efpBackupDir.CanBeEmpty = true;
      efpBackupDir.ToolTipText = "����� ��� ���������� ��������� �����." + Environment.NewLine + "���� �� ������, ����� ����������� � ����� ��������� (" + FileTools.ApplicationBaseDir.Path + ")" + Environment.NewLine +
        "����� ������ ���������� ���� � ������� ������ \"�����\" ��� ������������� ���� �� ����� ���������";
      efpBackupDir.EnabledEx = new DepNot(new DepEqual<int>(efpBackupMode.SelectedIndexEx, (int)UserSettings.BackupModes.None));

      EFPFolderBrowserButton efpBrowseBackupDir = new EFPFolderBrowserButton(efpBackupDir, btnBrowseBackupDir);
      efpBrowseBackupDir.ShowNewFolderButton = true;
      efpBrowseBackupDir.Description = "����� ��� ���������� ��������� �����";

      EFPWindowsExplorerButton efpExploreBackupDir = new EFPWindowsExplorerButton(efpBackupDir, btnExploreBackupDir);

      #endregion
    }

    #endregion

    #region ����

    #region ��������� �����������

    EFPListComboBox efpBackupMode;
    EFPTextBox efpBackupDir;

    #endregion

    #endregion

    #region ������ � ������ �����

    public void ValueToForm(UserSettings Settings)
    {
      efpBackupMode.SelectedIndex = (int)(Settings.BackupMode);
      efpBackupDir.Text = Settings.BackupDir;
    }

    public void ValueFromForm(UserSettings Settings)
    {
      Settings.BackupMode = (UserSettings.BackupModes)(efpBackupMode.SelectedIndex);
      Settings.BackupDir = efpBackupDir.Text;
    }

    #endregion

    #region ��������� �������

    /// <summary>
    /// ���������� ��������� ������� ����� �������� � �������� ������ ������
    /// </summary>
    private static int _LastSelectedPageIndex = 0;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      TheTabControl.SelectedIndex = _LastSelectedPageIndex;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      _LastSelectedPageIndex = TheTabControl.SelectedIndex;
      base.OnClosing(e);
    }

    #endregion
  }

  /// <summary>
  /// ��������� ������������
  /// </summary>
  public class UserSettings
  {
    #region �����������

    public UserSettings()
    {
      BackupMode = BackupModes.AfterEveryRun;
      BackupDir = "." + System.IO.Path.DirectorySeparatorChar + "Backup" + System.IO.Path.DirectorySeparatorChar;
    }

    #endregion

    #region ����

    #region ��������� �����������

    public enum BackupModes { None, AfterEveryRun, EveryDay }

    public BackupModes BackupMode;

    /// <summary>
    /// ������� ��� ���������� �����������.
    /// �� ���������� AbsPath, �.�. ����� ���� ������������� ����
    /// </summary>
    public string BackupDir;

    #endregion

    #endregion

    #region ������ / ������

    /// <summary>
    /// ���� � ����� "LocalConfig.xml"
    /// </summary>
    public static AbsPath LocalConfigFilePath
    {
      get { return new AbsPath(ProgramDB.DBDir.ParentDir, "LocalConfig.xml"); }
    }

    public void WriteConfig()
    {
      XmlCfgFile cfg = new XmlCfgFile(LocalConfigFilePath);

      cfg.SetEnum<BackupModes>("BackupMode", BackupMode);
      cfg.SetString("BackupDir", BackupDir);

      cfg.Save();
    }

    public void ReadConfig()
    {
      XmlCfgFile cfg = new XmlCfgFile(LocalConfigFilePath);

      cfg.GetEnum<BackupModes>("BackupMode", ref BackupMode);
      BackupDir = cfg.GetString("BackupDir");
    }

    #endregion
  }

}