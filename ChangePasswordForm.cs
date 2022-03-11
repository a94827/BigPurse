using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FreeLibSet.UICore;
using FreeLibSet.Forms;
using FreeLibSet.DependedValues;
using FreeLibSet.Core;

namespace App
{
  public partial class ChangePasswordForm : Form
  {
    #region Конструктор формы

    public ChangePasswordForm()
    {
      InitializeComponent();
      Icon = EFPApp.MainImageIcon("Password");

      EFPFormProvider efpForm = new EFPFormProvider(this);
      efpForm.HelpContext = "UI/ChangePassword.html";

      efpOldPassword = new EFPTextBox(efpForm, edOldPassword);
      efpOldPassword.CanBeEmpty = false;

      efpPassword1 = new EFPTextBox(efpForm, edPassword1);
      efpPassword1.CanBeEmpty = true;

      efpPassword2 = new EFPTextBox(efpForm, edPassword2);
      efpPassword2.CanBeEmpty = true;
      efpPassword2.Validating += new UIValidatingEventHandler(efpPassword2_Validating);
      efpPassword1.TextEx.ValueChanged += new EventHandler(efpPassword2.Validate);

      efpShowSymbols = new EFPCheckBox(efpForm, cbShowSymbols);
      efpShowSymbols.ToolTipText = "Если включить флажок, то в поле ввода пароля будут отображены вводимые символы";
      efpShowSymbols.Control.Click += new EventHandler(efpShowSymbols_Click);
      efpShowSymbols.Checked = _PrevShowSymbols;
      efpShowSymbols_Click(null, null);
    }

    #endregion

    #region Поля

    private static bool _PrevShowSymbols = false;

    EFPTextBox efpOldPassword, efpPassword1, efpPassword2;

    EFPCheckBox efpShowSymbols;

    #endregion

    #region Обработчики

    void efpShowSymbols_Click(object sender, EventArgs args)
    {
      try
      {
        if (edOldPassword.Enabled)
          edOldPassword.UseSystemPasswordChar = !cbShowSymbols.Checked;
        edPassword1.UseSystemPasswordChar = !cbShowSymbols.Checked;
        edPassword2.UseSystemPasswordChar = !cbShowSymbols.Checked;
        // Свой пароль нужно подтверждать всегда.
        // При сбросе чужого пароля подтверждение не нужно.
        // efpPassword2.Enabled = !cbShowSymbols.Checked;
        _PrevShowSymbols = efpShowSymbols.Checked;
      }
      catch { }
    }

    void efpPassword2_Validating(object sender,  UIValidatingEventArgs args)
    {
      if (args.ValidateState == UIValidateState.Error)
        return;

      if (efpPassword2.Text != efpPassword1.Text)
        args.SetError("Подтверждение пароля не совпадает");
    }

    #endregion

    #region Статический метод запуска

    /// <summary>
    /// Команда смены собственного пароля
    /// </summary>
    /// <returns></returns>
    public static void ChangePassword(object sender, EventArgs args)
    {
      ConfigSection sect = ProgramDBUI.ConfigSections["Password", "Params"];
      string currMD5 = sect.GetString("PasswordMD5");

      using (ChangePasswordForm form = new ChangePasswordForm())
      {
        // Определяем наличие пароля
        if (String.IsNullOrEmpty(currMD5))
        {
          form.edOldPassword.Enabled = false;
          form.edOldPassword.UseSystemPasswordChar = false;
          form.edOldPassword.Text = "[ нет пароля ]";
        }

        while (EFPApp.ShowDialog(form, false) == DialogResult.OK)
        {
          if (!String.IsNullOrEmpty(currMD5))
          {
            string confirmedMD5 = DataTools.MD5SumFromString(form.efpOldPassword.Text);
            if (confirmedMD5 != currMD5)
            {
              EFPApp.ErrorMessageBox("Введен неверный существующий пароль");
              continue;
            }
          }

          if (form.efpPassword1.Text.Length > 0)
          {
            string newMD5 = DataTools.MD5SumFromString(form.efpPassword1.Text);
            sect.SetString("PasswordMD5", newMD5);
            EFPApp.MessageBox("Пароль установлен");
          }
          else
          {
            sect.SetString("PasswordMD5", String.Empty);
            EFPApp.MessageBox("Пароль удален");
          }
          sect.Save();
          break;
        }
      }
    }


    #endregion
  }
}