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
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.cbPurposes = new FreeLibSet.Controls.UserSelComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.cbShops = new FreeLibSet.Controls.UserSelComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.cbProducts = new FreeLibSet.Controls.UserSelComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cbWallets = new FreeLibSet.Controls.UserSelComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.edPeriod = new FreeLibSet.Controls.DateRangeBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.cbProductDet = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.MainPanel.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainPanel
      // 
      this.MainPanel.Controls.Add(this.groupBox3);
      this.MainPanel.Controls.Add(this.groupBox2);
      this.MainPanel.Size = new System.Drawing.Size(579, 234);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.cbPurposes);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Controls.Add(this.cbShops);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.cbProducts);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.cbWallets);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.edPeriod);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(579, 181);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Параметры отчета";
      // 
      // cbPurposes
      // 
      this.cbPurposes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbPurposes.Location = new System.Drawing.Point(102, 151);
      this.cbPurposes.Name = "cbPurposes";
      this.cbPurposes.Size = new System.Drawing.Size(471, 20);
      this.cbPurposes.TabIndex = 9;
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(12, 151);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(84, 19);
      this.label6.TabIndex = 8;
      this.label6.Text = "Назначение";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbShops
      // 
      this.cbShops.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbShops.Location = new System.Drawing.Point(102, 125);
      this.cbShops.Name = "cbShops";
      this.cbShops.Size = new System.Drawing.Size(471, 20);
      this.cbShops.TabIndex = 7;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(12, 125);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(84, 19);
      this.label4.TabIndex = 6;
      this.label4.Text = "Магазины";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbProducts
      // 
      this.cbProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbProducts.Location = new System.Drawing.Point(102, 73);
      this.cbProducts.Name = "cbProducts";
      this.cbProducts.Size = new System.Drawing.Size(471, 20);
      this.cbProducts.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(12, 73);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(84, 19);
      this.label3.TabIndex = 2;
      this.label3.Text = "&Товары";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbWallets
      // 
      this.cbWallets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbWallets.Location = new System.Drawing.Point(102, 99);
      this.cbWallets.Name = "cbWallets";
      this.cbWallets.Size = new System.Drawing.Size(471, 20);
      this.cbWallets.TabIndex = 5;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(12, 99);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(84, 19);
      this.label2.TabIndex = 4;
      this.label2.Text = "Кошельки";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edPeriod
      // 
      this.edPeriod.Location = new System.Drawing.Point(102, 30);
      this.edPeriod.Name = "edPeriod";
      this.edPeriod.Size = new System.Drawing.Size(385, 37);
      this.edPeriod.TabIndex = 1;
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
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.cbProductDet);
      this.groupBox3.Controls.Add(this.label5);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox3.Location = new System.Drawing.Point(0, 181);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(579, 53);
      this.groupBox3.TabIndex = 1;
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
      this.cbProductDet.Location = new System.Drawing.Point(102, 19);
      this.cbProductDet.Name = "cbProductDet";
      this.cbProductDet.Size = new System.Drawing.Size(471, 21);
      this.cbProductDet.TabIndex = 1;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(12, 19);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(84, 21);
      this.label5.TabIndex = 0;
      this.label5.Text = "Детализация";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // PurchaseReportParamForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(579, 282);
      this.Name = "PurchaseReportParamForm";
      this.Text = "Покупки";
      this.MainPanel.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox2;
    private FreeLibSet.Controls.UserSelComboBox cbWallets;
    private System.Windows.Forms.Label label2;
    private FreeLibSet.Controls.DateRangeBox edPeriod;
    private System.Windows.Forms.Label label1;
    private FreeLibSet.Controls.UserSelComboBox cbProducts;
    private System.Windows.Forms.Label label3;
    private FreeLibSet.Controls.UserSelComboBox cbShops;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.ComboBox cbProductDet;
    private System.Windows.Forms.Label label5;
    private FreeLibSet.Controls.UserSelComboBox cbPurposes;
    private System.Windows.Forms.Label label6;
  }
}