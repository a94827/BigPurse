namespace App
{
  partial class TurnoverStatementParamForm
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
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.edPeriod = new FreeLibSet.Controls.DateRangeBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cbWallets = new FreeLibSet.Controls.UserSelComboBox();
      this.MainPanel.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainPanel
      // 
      this.MainPanel.Controls.Add(this.groupBox2);
      this.MainPanel.Size = new System.Drawing.Size(502, 127);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.cbWallets);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.edPeriod);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(502, 127);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Параметры отчета";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(12, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(84, 19);
      this.label1.TabIndex = 0;
      this.label1.Text = "Период";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edPeriod
      // 
      this.edPeriod.Location = new System.Drawing.Point(102, 30);
      this.edPeriod.Name = "edPeriod";
      this.edPeriod.Size = new System.Drawing.Size(385, 37);
      this.edPeriod.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(12, 86);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(84, 19);
      this.label2.TabIndex = 2;
      this.label2.Text = "Кошельки";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbWallets
      // 
      this.cbWallets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbWallets.Location = new System.Drawing.Point(106, 87);
      this.cbWallets.Name = "cbWallets";
      this.cbWallets.Size = new System.Drawing.Size(381, 20);
      this.cbWallets.TabIndex = 3;
      // 
      // TurnoverStatementReportParams
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(502, 175);
      this.Name = "TurnoverStatementReportParams";
      this.Text = "Оборотная ведомость";
      this.MainPanel.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox2;
    private FreeLibSet.Controls.UserSelComboBox cbWallets;
    private System.Windows.Forms.Label label2;
    private FreeLibSet.Controls.DateRangeBox edPeriod;
    private System.Windows.Forms.Label label1;
  }
}