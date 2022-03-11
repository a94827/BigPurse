namespace App
{
  partial class ChangePasswordForm
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

    #region Windows TheForm Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.cbShowSymbols = new System.Windows.Forms.CheckBox();
      this.edPassword2 = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.edPassword1 = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.edOldPassword = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(8, 40);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(88, 24);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Отмена";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOK
      // 
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(8, 8);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(88, 24);
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "О&К";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnOK);
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel1.Location = new System.Drawing.Point(394, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(104, 160);
      this.panel1.TabIndex = 1;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.groupBox1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(394, 160);
      this.panel2.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cbShowSymbols);
      this.groupBox1.Controls.Add(this.edPassword2);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.edPassword1);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.edOldPassword);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(394, 160);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      // 
      // cbShowSymbols
      // 
      this.cbShowSymbols.AutoSize = true;
      this.cbShowSymbols.Location = new System.Drawing.Point(204, 37);
      this.cbShowSymbols.Name = "cbShowSymbols";
      this.cbShowSymbols.Size = new System.Drawing.Size(179, 17);
      this.cbShowSymbols.TabIndex = 7;
      this.cbShowSymbols.Text = "Показать вводимые символы";
      this.cbShowSymbols.UseVisualStyleBackColor = true;
      // 
      // edPassword2
      // 
      this.edPassword2.Location = new System.Drawing.Point(13, 129);
      this.edPassword2.Name = "edPassword2";
      this.edPassword2.Size = new System.Drawing.Size(159, 20);
      this.edPassword2.TabIndex = 5;
      this.edPassword2.UseSystemPasswordChar = true;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(10, 113);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(147, 13);
      this.label4.TabIndex = 4;
      this.label4.Text = "&3. Повторите новый пароль";
      // 
      // edPassword1
      // 
      this.edPassword1.Location = new System.Drawing.Point(13, 80);
      this.edPassword1.Name = "edPassword1";
      this.edPassword1.Size = new System.Drawing.Size(159, 20);
      this.edPassword1.TabIndex = 3;
      this.edPassword1.UseSystemPasswordChar = true;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(10, 64);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(135, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "&2. Введите новый пароль";
      // 
      // edOldPassword
      // 
      this.edOldPassword.Location = new System.Drawing.Point(13, 35);
      this.edOldPassword.Name = "edOldPassword";
      this.edOldPassword.Size = new System.Drawing.Size(159, 20);
      this.edOldPassword.TabIndex = 1;
      this.edOldPassword.UseSystemPasswordChar = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(180, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "&1. Введите существующий пароль";
      // 
      // ChangePasswordForm
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(498, 160);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ChangePasswordForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Смена пароля";
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox cbShowSymbols;
    public System.Windows.Forms.TextBox edPassword2;
    private System.Windows.Forms.Label label4;
    public System.Windows.Forms.TextBox edPassword1;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.TextBox edOldPassword;
    private System.Windows.Forms.Label label1;
  }
}