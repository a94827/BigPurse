using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FreeLibSet.Forms;
using FreeLibSet.Core;
using FreeLibSet.UICore;

namespace App
{
  internal partial class LoginForm : Form
  {
    #region Конструктор формы

    public LoginForm()
    {
      InitializeComponent();
      Icon = EFPApp.MainImageIcon("Password");

      EFPFormProvider efpForm = new EFPFormProvider(this);
      efpForm.OwnStatusBar = false; // иначе из-за поля "Пароль" будет статусная строка

      efpPassword = new EFPTextBox(efpForm, edPassword);
      efpPassword.CanBeEmpty = false; 

      efpShowSymbols = new EFPCheckBox(efpForm, cbShowSymbols);
      efpShowSymbols.ToolTipText = "Если включить флажок, то в поле ввода пароля будут отображены вводимые символы";
      efpShowSymbols.Control.Click += new EventHandler(efpShowSymbols_Click);

    }

    #endregion

    protected override void OnVisibleChanged(EventArgs args)
    {
      base.OnVisibleChanged(args);
      if (Visible)
        edPassword.SelectAll(); // 23.01.2017
    }

    void efpShowSymbols_Click(object sender, EventArgs args)
    {
      try
      {
        edPassword.UseSystemPasswordChar = !cbShowSymbols.Checked;
        efpPassword.CommandItems.InitEnabled(); // 24.01.2019
      }
      catch { }
    }

    #region Поля

    EFPTextBox efpPassword;

    EFPCheckBox efpShowSymbols;

    #endregion

    #region Статический метод запуска

    public static bool ProcessLogin()
    {
      ConfigSection sect = ProgramDBUI.ConfigSections["Password", "Params"];
      string currMD5 = sect.GetString("PasswordMD5");
      if (currMD5.Length == 0)
        return true;

      using (LoginForm form = new LoginForm())
      {
        int cnt = 0;
        while (EFPApp.ShowDialog(form, false) == DialogResult.OK)
        {
          cnt++;
          string password = form.efpPassword.Text;
          string md5 = DataTools.MD5SumFromString(password);
          if (md5 == currMD5)
            return true;

          EFPApp.ErrorMessageBox("Введен неверный пароль");
          if (cnt >= 3)
            break;
        }
        return false;
      }
    }


    #endregion
  }
}