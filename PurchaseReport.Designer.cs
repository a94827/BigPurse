namespace App
{
  partial class PurchaseReportParamForm
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
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.cbProductDet = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.edPeriod = new FreeLibSet.Controls.DateRangeBox();
      this.label1 = new System.Windows.Forms.Label();
      this.MainTabPage.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainTabPage
      // 
      this.MainTabPage.Controls.Add(this.groupBox3);
      this.MainTabPage.Controls.Add(this.groupBox2);
      this.MainTabPage.Location = new System.Drawing.Point(4, 22);
      this.MainTabPage.Size = new System.Drawing.Size(616, 242);
      // 
      // MainPanel
      // 
      this.MainPanel.Size = new System.Drawing.Size(624, 268);
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.cbProductDet);
      this.groupBox3.Controls.Add(this.label5);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox3.Location = new System.Drawing.Point(3, 86);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(610, 153);
      this.groupBox3.TabIndex = 3;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Вкладка \"Товары\"";
      // 
      // cbProductDet
      // 
      this.cbProductDet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbProductDet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbProductDet.FormattingEnabled = true;
      this.cbProductDet.Items.AddRange(new object[] {
            "Товар, услуга в справочнике",
            "Описание",
            "Количество"});
      this.cbProductDet.Location = new System.Drawing.Point(138, 19);
      this.cbProductDet.Name = "cbProductDet";
      this.cbProductDet.Size = new System.Drawing.Size(466, 21);
      this.cbProductDet.TabIndex = 1;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(12, 19);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(120, 21);
      this.label5.TabIndex = 0;
      this.label5.Text = "Детализация";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.edPeriod);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(3, 3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(610, 83);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Параметры отчета";
      // 
      // edPeriod
      // 
      this.edPeriod.Location = new System.Drawing.Point(138, 30);
      this.edPeriod.Name = "edPeriod";
      this.edPeriod.Size = new System.Drawing.Size(385, 37);
      this.edPeriod.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(12, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(120, 19);
      this.label1.TabIndex = 0;
      this.label1.Text = "Период";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // PurchaseReportParamForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(624, 316);
      this.Name = "PurchaseReportParamForm";
      this.Text = "Покупки";
      this.MainTabPage.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.ComboBox cbProductDet;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.GroupBox groupBox2;
    private FreeLibSet.Controls.DateRangeBox edPeriod;
    private System.Windows.Forms.Label label1;
  }
}