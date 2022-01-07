namespace App
{
  partial class EditOperationProduct
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
      this.MainPanel = new System.Windows.Forms.Panel();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbProduct = new FreeLibSet.Controls.UserSelComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cbDescription = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.edQuantity = new FreeLibSet.Controls.IntEditBox();
      this.label4 = new System.Windows.Forms.Label();
      this.cbUnit = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.edFormula = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.edSum = new FreeLibSet.Controls.DecimalEditBox();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.edComment = new System.Windows.Forms.TextBox();
      this.MainPanel.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainPanel
      // 
      this.MainPanel.Controls.Add(this.groupBox5);
      this.MainPanel.Controls.Add(this.groupBox1);
      this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainPanel.Location = new System.Drawing.Point(0, 0);
      this.MainPanel.Name = "MainPanel";
      this.MainPanel.Size = new System.Drawing.Size(494, 246);
      this.MainPanel.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.edSum);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Controls.Add(this.edFormula);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.cbUnit);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.edQuantity);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.cbDescription);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.cbProduct);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(494, 162);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Запись";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(6, 24);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(92, 20);
      this.label1.TabIndex = 0;
      this.label1.Text = "&Товар";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbProduct
      // 
      this.cbProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbProduct.Location = new System.Drawing.Point(104, 24);
      this.cbProduct.Name = "cbProduct";
      this.cbProduct.Size = new System.Drawing.Size(378, 20);
      this.cbProduct.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(6, 50);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(92, 21);
      this.label2.TabIndex = 2;
      this.label2.Text = "О&писание";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbDescription
      // 
      this.cbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbDescription.FormattingEnabled = true;
      this.cbDescription.Location = new System.Drawing.Point(104, 50);
      this.cbDescription.Name = "cbDescription";
      this.cbDescription.Size = new System.Drawing.Size(378, 21);
      this.cbDescription.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(6, 77);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(92, 20);
      this.label3.TabIndex = 4;
      this.label3.Text = "Коли&чество";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edQuantity
      // 
      this.edQuantity.Increment = 1;
      this.edQuantity.Location = new System.Drawing.Point(104, 77);
      this.edQuantity.Minimum = 1;
      this.edQuantity.Name = "edQuantity";
      this.edQuantity.Size = new System.Drawing.Size(75, 20);
      this.edQuantity.TabIndex = 5;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(185, 77);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(92, 20);
      this.label4.TabIndex = 6;
      this.label4.Text = "&Ед. измерения";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbUnit
      // 
      this.cbUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbUnit.FormattingEnabled = true;
      this.cbUnit.Location = new System.Drawing.Point(283, 77);
      this.cbUnit.Name = "cbUnit";
      this.cbUnit.Size = new System.Drawing.Size(199, 21);
      this.cbUnit.TabIndex = 7;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(6, 103);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(92, 20);
      this.label5.TabIndex = 8;
      this.label5.Text = "&Формула";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edFormula
      // 
      this.edFormula.Location = new System.Drawing.Point(104, 103);
      this.edFormula.Name = "edFormula";
      this.edFormula.Size = new System.Drawing.Size(378, 20);
      this.edFormula.TabIndex = 9;
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(6, 129);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(92, 20);
      this.label6.TabIndex = 10;
      this.label6.Text = "&Сумма";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edSum
      // 
      this.edSum.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.edSum.Location = new System.Drawing.Point(104, 129);
      this.edSum.Name = "edSum";
      this.edSum.Size = new System.Drawing.Size(150, 20);
      this.edSum.TabIndex = 11;
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.edComment);
      this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox5.Location = new System.Drawing.Point(0, 162);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(494, 84);
      this.groupBox5.TabIndex = 3;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Комментари&й";
      // 
      // edComment
      // 
      this.edComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edComment.Location = new System.Drawing.Point(6, 19);
      this.edComment.Multiline = true;
      this.edComment.Name = "edComment";
      this.edComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.edComment.Size = new System.Drawing.Size(482, 59);
      this.edComment.TabIndex = 0;
      // 
      // EditOperationProduct
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(494, 246);
      this.Controls.Add(this.MainPanel);
      this.Name = "EditOperationProduct";
      this.MainPanel.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel MainPanel;
    private System.Windows.Forms.GroupBox groupBox1;
    private FreeLibSet.Controls.UserSelComboBox cbProduct;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbDescription;
    private System.Windows.Forms.TextBox edFormula;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cbUnit;
    private System.Windows.Forms.Label label4;
    private FreeLibSet.Controls.IntEditBox edQuantity;
    private System.Windows.Forms.Label label3;
    private FreeLibSet.Controls.DecimalEditBox edSum;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.TextBox edComment;
  }
}