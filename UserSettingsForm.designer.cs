namespace BigPurse
{
  partial class UserSettingsForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.TheTabControl = new System.Windows.Forms.TabControl();
      this.tpBackup = new System.Windows.Forms.TabPage();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.btnExploreBackupDir = new System.Windows.Forms.Button();
      this.btnBrowseBackupDir = new System.Windows.Forms.Button();
      this.edBackupDir = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cbBackupMode = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.TheTabControl.SuspendLayout();
      this.tpBackup.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Controls.Add(this.btnOk);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 222);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(624, 40);
      this.panel1.TabIndex = 1;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(102, 8);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(88, 24);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Отмена";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(8, 8);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(88, 24);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "О&К";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // TheTabControl
      // 
      this.TheTabControl.Controls.Add(this.tpBackup);
      this.TheTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TheTabControl.Location = new System.Drawing.Point(0, 0);
      this.TheTabControl.Name = "TheTabControl";
      this.TheTabControl.SelectedIndex = 0;
      this.TheTabControl.Size = new System.Drawing.Size(624, 222);
      this.TheTabControl.TabIndex = 0;
      // 
      // tpBackup
      // 
      this.tpBackup.Controls.Add(this.groupBox2);
      this.tpBackup.Location = new System.Drawing.Point(4, 22);
      this.tpBackup.Name = "tpBackup";
      this.tpBackup.Padding = new System.Windows.Forms.Padding(3);
      this.tpBackup.Size = new System.Drawing.Size(616, 196);
      this.tpBackup.TabIndex = 1;
      this.tpBackup.Text = "Резервное копирование";
      this.tpBackup.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.btnExploreBackupDir);
      this.groupBox2.Controls.Add(this.btnBrowseBackupDir);
      this.groupBox2.Controls.Add(this.edBackupDir);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.cbBackupMode);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(3, 3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(610, 122);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Параметры резервного копирования";
      // 
      // btnExploreBackupDir
      // 
      this.btnExploreBackupDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnExploreBackupDir.Location = new System.Drawing.Point(462, 91);
      this.btnExploreBackupDir.Name = "btnExploreBackupDir";
      this.btnExploreBackupDir.Size = new System.Drawing.Size(132, 24);
      this.btnExploreBackupDir.TabIndex = 5;
      this.btnExploreBackupDir.Text = "Проводник";
      this.btnExploreBackupDir.UseVisualStyleBackColor = true;
      // 
      // btnBrowseBackupDir
      // 
      this.btnBrowseBackupDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBrowseBackupDir.Location = new System.Drawing.Point(506, 63);
      this.btnBrowseBackupDir.Name = "btnBrowseBackupDir";
      this.btnBrowseBackupDir.Size = new System.Drawing.Size(88, 24);
      this.btnBrowseBackupDir.TabIndex = 4;
      this.btnBrowseBackupDir.Text = "Обзор";
      this.btnBrowseBackupDir.UseVisualStyleBackColor = true;
      // 
      // edBackupDir
      // 
      this.edBackupDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edBackupDir.Location = new System.Drawing.Point(93, 65);
      this.edBackupDir.Name = "edBackupDir";
      this.edBackupDir.Size = new System.Drawing.Size(407, 20);
      this.edBackupDir.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(6, 65);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(81, 21);
      this.label2.TabIndex = 2;
      this.label2.Text = "Каталог";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbBackupMode
      // 
      this.cbBackupMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbBackupMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbBackupMode.FormattingEnabled = true;
      this.cbBackupMode.Items.AddRange(new object[] {
            "Не делать",
            "При каждом завершении работы программы",
            "Один раз в день"});
      this.cbBackupMode.Location = new System.Drawing.Point(93, 28);
      this.cbBackupMode.Name = "cbBackupMode";
      this.cbBackupMode.Size = new System.Drawing.Size(501, 21);
      this.cbBackupMode.TabIndex = 1;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(6, 28);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(81, 21);
      this.label3.TabIndex = 0;
      this.label3.Text = "Ре&жим";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // UserSettingsForm
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(624, 262);
      this.Controls.Add(this.TheTabControl);
      this.Controls.Add(this.panel1);
      this.Name = "UserSettingsForm";
      this.Text = "Настройки";
      this.panel1.ResumeLayout(false);
      this.TheTabControl.ResumeLayout(false);
      this.tpBackup.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.TabControl TheTabControl;
    private System.Windows.Forms.TabPage tpBackup;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button btnBrowseBackupDir;
    private System.Windows.Forms.TextBox edBackupDir;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbBackupMode;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnExploreBackupDir;
  }
}