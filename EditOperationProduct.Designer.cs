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
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.edComment = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.cbMU3 = new FreeLibSet.Controls.UserSelComboBox();
      this.label9 = new System.Windows.Forms.Label();
      this.edQuantity3 = new FreeLibSet.Controls.SingleEditBox();
      this.label10 = new System.Windows.Forms.Label();
      this.cbMU2 = new FreeLibSet.Controls.UserSelComboBox();
      this.label7 = new System.Windows.Forms.Label();
      this.edQuantity2 = new FreeLibSet.Controls.SingleEditBox();
      this.label8 = new System.Windows.Forms.Label();
      this.edSum = new FreeLibSet.Controls.DecimalEditBox();
      this.label6 = new System.Windows.Forms.Label();
      this.edFormula = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.cbMU1 = new FreeLibSet.Controls.UserSelComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.edQuantity1 = new FreeLibSet.Controls.SingleEditBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cbDescription = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cbProduct = new FreeLibSet.Controls.UserSelComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.MainPanel.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainPanel
      // 
      this.MainPanel.Controls.Add(this.groupBox5);
      this.MainPanel.Controls.Add(this.groupBox1);
      this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainPanel.Location = new System.Drawing.Point(0, 0);
      this.MainPanel.Name = "MainPanel";
      this.MainPanel.Size = new System.Drawing.Size(494, 313);
      this.MainPanel.TabIndex = 0;
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.edComment);
      this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox5.Location = new System.Drawing.Point(0, 210);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(494, 103);
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
      this.edComment.Size = new System.Drawing.Size(482, 78);
      this.edComment.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cbMU3);
      this.groupBox1.Controls.Add(this.label9);
      this.groupBox1.Controls.Add(this.edQuantity3);
      this.groupBox1.Controls.Add(this.label10);
      this.groupBox1.Controls.Add(this.cbMU2);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Controls.Add(this.edQuantity2);
      this.groupBox1.Controls.Add(this.label8);
      this.groupBox1.Controls.Add(this.edSum);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Controls.Add(this.edFormula);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.cbMU1);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.edQuantity1);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.cbDescription);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.cbProduct);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(494, 210);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Запись";
      // 
      // cbMU3
      // 
      this.cbMU3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbMU3.Location = new System.Drawing.Point(283, 129);
      this.cbMU3.Name = "cbMU3";
      this.cbMU3.Size = new System.Drawing.Size(199, 20);
      this.cbMU3.TabIndex = 15;
      // 
      // label9
      // 
      this.label9.Location = new System.Drawing.Point(185, 129);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(92, 20);
      this.label9.TabIndex = 14;
      this.label9.Text = "Ед. измерения";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edQuantity3
      // 
      this.edQuantity3.Increment = 0F;
      this.edQuantity3.Location = new System.Drawing.Point(104, 129);
      this.edQuantity3.Name = "edQuantity3";
      this.edQuantity3.Size = new System.Drawing.Size(75, 20);
      this.edQuantity3.TabIndex = 13;
      // 
      // label10
      // 
      this.label10.Location = new System.Drawing.Point(6, 129);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(92, 20);
      this.label10.TabIndex = 12;
      this.label10.Text = "Количество &3";
      this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbMU2
      // 
      this.cbMU2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbMU2.Location = new System.Drawing.Point(283, 103);
      this.cbMU2.Name = "cbMU2";
      this.cbMU2.Size = new System.Drawing.Size(199, 20);
      this.cbMU2.TabIndex = 11;
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(185, 103);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(92, 20);
      this.label7.TabIndex = 10;
      this.label7.Text = "Ед. измерения";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edQuantity2
      // 
      this.edQuantity2.Increment = 0F;
      this.edQuantity2.Location = new System.Drawing.Point(104, 103);
      this.edQuantity2.Name = "edQuantity2";
      this.edQuantity2.Size = new System.Drawing.Size(75, 20);
      this.edQuantity2.TabIndex = 9;
      // 
      // label8
      // 
      this.label8.Location = new System.Drawing.Point(6, 103);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(92, 20);
      this.label8.TabIndex = 8;
      this.label8.Text = "Количество &2";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edSum
      // 
      this.edSum.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.edSum.Location = new System.Drawing.Point(104, 181);
      this.edSum.Name = "edSum";
      this.edSum.Size = new System.Drawing.Size(150, 20);
      this.edSum.TabIndex = 19;
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(6, 181);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(92, 20);
      this.label6.TabIndex = 18;
      this.label6.Text = "&Сумма";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edFormula
      // 
      this.edFormula.Location = new System.Drawing.Point(104, 155);
      this.edFormula.Name = "edFormula";
      this.edFormula.Size = new System.Drawing.Size(378, 20);
      this.edFormula.TabIndex = 17;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(6, 155);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(92, 20);
      this.label5.TabIndex = 16;
      this.label5.Text = "&Формула";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbMU1
      // 
      this.cbMU1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbMU1.Location = new System.Drawing.Point(283, 77);
      this.cbMU1.Name = "cbMU1";
      this.cbMU1.Size = new System.Drawing.Size(199, 20);
      this.cbMU1.TabIndex = 7;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(185, 77);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(92, 20);
      this.label4.TabIndex = 6;
      this.label4.Text = "Ед. измерения";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edQuantity1
      // 
      this.edQuantity1.Increment = 0F;
      this.edQuantity1.Location = new System.Drawing.Point(104, 77);
      this.edQuantity1.Name = "edQuantity1";
      this.edQuantity1.Size = new System.Drawing.Size(75, 20);
      this.edQuantity1.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(6, 77);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(92, 20);
      this.label3.TabIndex = 4;
      this.label3.Text = "Количество &1";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(6, 50);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(92, 21);
      this.label2.TabIndex = 2;
      this.label2.Text = "О&писание";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(6, 24);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(92, 20);
      this.label1.TabIndex = 0;
      this.label1.Text = "&Товар";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // EditOperationProduct
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(494, 313);
      this.Controls.Add(this.MainPanel);
      this.Name = "EditOperationProduct";
      this.MainPanel.ResumeLayout(false);
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
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
    private FreeLibSet.Controls.UserSelComboBox cbMU1;
    private System.Windows.Forms.Label label4;
    private FreeLibSet.Controls.SingleEditBox edQuantity1;
    private System.Windows.Forms.Label label3;
    private FreeLibSet.Controls.DecimalEditBox edSum;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.TextBox edComment;
    private FreeLibSet.Controls.UserSelComboBox cbMU2;
    private System.Windows.Forms.Label label7;
    private FreeLibSet.Controls.SingleEditBox edQuantity2;
    private System.Windows.Forms.Label label8;
    private FreeLibSet.Controls.UserSelComboBox cbMU3;
    private System.Windows.Forms.Label label9;
    private FreeLibSet.Controls.SingleEditBox edQuantity3;
    private System.Windows.Forms.Label label10;
  }
}