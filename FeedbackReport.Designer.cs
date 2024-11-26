namespace App
{
  partial class FeedbackReportParamForm
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
      this.cbProducts = new FreeLibSet.Controls.UserSelComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.edPeriod = new FreeLibSet.Controls.DateRangeBox();
      this.label1 = new System.Windows.Forms.Label();
      this.MainPanel.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainPanel
      // 
      this.MainPanel.Controls.Add(this.groupBox2);
      this.MainPanel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
      this.MainPanel.Size = new System.Drawing.Size(772, 152);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.cbProducts);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.edPeriod);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.groupBox2.Size = new System.Drawing.Size(772, 223);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Параметры отчета";
      // 
      // cbProducts
      // 
      this.cbProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbProducts.Location = new System.Drawing.Point(136, 90);
      this.cbProducts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.cbProducts.Name = "cbProducts";
      this.cbProducts.Size = new System.Drawing.Size(628, 22);
      this.cbProducts.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(16, 90);
      this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(112, 23);
      this.label3.TabIndex = 2;
      this.label3.Text = "&Товары";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edPeriod
      // 
      this.edPeriod.Location = new System.Drawing.Point(136, 37);
      this.edPeriod.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
      this.edPeriod.Name = "edPeriod";
      this.edPeriod.Size = new System.Drawing.Size(513, 45);
      this.edPeriod.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(16, 37);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(112, 23);
      this.label1.TabIndex = 0;
      this.label1.Text = "Период";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // FeedbackReportParamForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(772, 211);
      this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
      this.Name = "FeedbackReportParamForm";
      this.Text = "Отзывы";
      this.MainPanel.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox2;
    private FreeLibSet.Controls.DateRangeBox edPeriod;
    private System.Windows.Forms.Label label1;
    private FreeLibSet.Controls.UserSelComboBox cbProducts;
    private System.Windows.Forms.Label label3;
  }
}