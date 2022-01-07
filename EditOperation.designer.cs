namespace App
{
  partial class EditOperation
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
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.MainPanel1 = new System.Windows.Forms.Panel();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.edComment = new System.Windows.Forms.TextBox();
      this.grpSum = new System.Windows.Forms.GroupBox();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.label5 = new System.Windows.Forms.Label();
      this.edSumBefore = new FreeLibSet.Controls.DecimalEditBox();
      this.label7 = new System.Windows.Forms.Label();
      this.edSumOp = new FreeLibSet.Controls.DecimalEditBox();
      this.label6 = new System.Windows.Forms.Label();
      this.edSumAfter = new FreeLibSet.Controls.DecimalEditBox();
      this.grpOp = new System.Windows.Forms.GroupBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.lblDate = new System.Windows.Forms.Label();
      this.edDate = new FreeLibSet.Controls.DateTimeBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.edOpOrder = new FreeLibSet.Controls.IntEditBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lblWalletDebt = new System.Windows.Forms.Label();
      this.lblWalletCredit = new System.Windows.Forms.Label();
      this.cbWalletDebt = new FreeLibSet.Controls.UserSelComboBox();
      this.cbWalletCredit = new FreeLibSet.Controls.UserSelComboBox();
      this.lblContra = new System.Windows.Forms.Label();
      this.cbContra = new FreeLibSet.Controls.UserSelComboBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.MainPanel2 = new System.Windows.Forms.Panel();
      this.panel3 = new System.Windows.Forms.Panel();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.MainPanel1.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.grpSum.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.grpOp.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.MainPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(592, 370);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.MainPanel1);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(584, 344);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Общие";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // MainPanel1
      // 
      this.MainPanel1.Controls.Add(this.groupBox5);
      this.MainPanel1.Controls.Add(this.grpSum);
      this.MainPanel1.Controls.Add(this.grpOp);
      this.MainPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainPanel1.Location = new System.Drawing.Point(3, 3);
      this.MainPanel1.Name = "MainPanel1";
      this.MainPanel1.Size = new System.Drawing.Size(578, 338);
      this.MainPanel1.TabIndex = 0;
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.edComment);
      this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox5.Location = new System.Drawing.Point(0, 182);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(578, 156);
      this.groupBox5.TabIndex = 2;
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
      this.edComment.Size = new System.Drawing.Size(566, 131);
      this.edComment.TabIndex = 0;
      // 
      // grpSum
      // 
      this.grpSum.AutoSize = true;
      this.grpSum.Controls.Add(this.tableLayoutPanel2);
      this.grpSum.Dock = System.Windows.Forms.DockStyle.Top;
      this.grpSum.Location = new System.Drawing.Point(0, 117);
      this.grpSum.Name = "grpSum";
      this.grpSum.Size = new System.Drawing.Size(578, 65);
      this.grpSum.TabIndex = 1;
      this.grpSum.TabStop = false;
      this.grpSum.Text = "Сумма";
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.AutoSize = true;
      this.tableLayoutPanel2.ColumnCount = 3;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.edSumBefore, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.label7, 1, 0);
      this.tableLayoutPanel2.Controls.Add(this.edSumOp, 1, 1);
      this.tableLayoutPanel2.Controls.Add(this.label6, 2, 0);
      this.tableLayoutPanel2.Controls.Add(this.edSumAfter, 2, 1);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 2;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel2.Size = new System.Drawing.Size(572, 46);
      this.tableLayoutPanel2.TabIndex = 0;
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label5.Location = new System.Drawing.Point(3, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(184, 20);
      this.label5.TabIndex = 0;
      this.label5.Text = "До операции";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // edSumBefore
      // 
      this.edSumBefore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edSumBefore.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.edSumBefore.Location = new System.Drawing.Point(3, 23);
      this.edSumBefore.Name = "edSumBefore";
      this.edSumBefore.Size = new System.Drawing.Size(184, 20);
      this.edSumBefore.TabIndex = 1;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label7.Location = new System.Drawing.Point(193, 0);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(184, 20);
      this.label7.TabIndex = 4;
      this.label7.Text = "В операции";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // edSumOp
      // 
      this.edSumOp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edSumOp.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.edSumOp.Location = new System.Drawing.Point(193, 23);
      this.edSumOp.Name = "edSumOp";
      this.edSumOp.Size = new System.Drawing.Size(184, 20);
      this.edSumOp.TabIndex = 5;
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label6.Location = new System.Drawing.Point(383, 0);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(186, 20);
      this.label6.TabIndex = 6;
      this.label6.Text = "После операции";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // edSumAfter
      // 
      this.edSumAfter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edSumAfter.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.edSumAfter.Location = new System.Drawing.Point(383, 23);
      this.edSumAfter.Name = "edSumAfter";
      this.edSumAfter.Size = new System.Drawing.Size(186, 20);
      this.edSumAfter.TabIndex = 7;
      // 
      // grpOp
      // 
      this.grpOp.AutoSize = true;
      this.grpOp.Controls.Add(this.tableLayoutPanel1);
      this.grpOp.Dock = System.Windows.Forms.DockStyle.Top;
      this.grpOp.Location = new System.Drawing.Point(0, 0);
      this.grpOp.Name = "grpOp";
      this.grpOp.Size = new System.Drawing.Size(578, 117);
      this.grpOp.TabIndex = 0;
      this.grpOp.TabStop = false;
      this.grpOp.Text = "Операция";
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.AutoSize = true;
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.lblDate, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.edDate, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.lblWalletDebt, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.lblWalletCredit, 2, 1);
      this.tableLayoutPanel1.Controls.Add(this.cbWalletDebt, 1, 2);
      this.tableLayoutPanel1.Controls.Add(this.cbWalletCredit, 2, 2);
      this.tableLayoutPanel1.Controls.Add(this.lblContra, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.cbContra, 1, 3);
      this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.Size = new System.Drawing.Size(572, 98);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // lblDate
      // 
      this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblDate.Location = new System.Drawing.Point(3, 0);
      this.lblDate.Name = "lblDate";
      this.lblDate.Size = new System.Drawing.Size(94, 26);
      this.lblDate.TabIndex = 0;
      this.lblDate.Text = "Дата";
      this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edDate
      // 
      this.edDate.Location = new System.Drawing.Point(103, 3);
      this.edDate.Name = "edDate";
      this.edDate.Size = new System.Drawing.Size(120, 20);
      this.edDate.TabIndex = 1;
      // 
      // panel1
      // 
      this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panel1.Controls.Add(this.edOpOrder);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Location = new System.Drawing.Point(339, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(230, 20);
      this.panel1.TabIndex = 5;
      // 
      // edOpOrder
      // 
      this.edOpOrder.Dock = System.Windows.Forms.DockStyle.Left;
      this.edOpOrder.Increment = 1;
      this.edOpOrder.Location = new System.Drawing.Point(80, 0);
      this.edOpOrder.Name = "edOpOrder";
      this.edOpOrder.Size = new System.Drawing.Size(70, 20);
      this.edOpOrder.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Left;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(80, 20);
      this.label1.TabIndex = 0;
      this.label1.Text = "Порядок";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.Location = new System.Drawing.Point(3, 46);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(94, 26);
      this.label2.TabIndex = 3;
      this.label2.Text = "Кошелек";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblWalletDebt
      // 
      this.lblWalletDebt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblWalletDebt.Location = new System.Drawing.Point(103, 26);
      this.lblWalletDebt.Name = "lblWalletDebt";
      this.lblWalletDebt.Size = new System.Drawing.Size(230, 20);
      this.lblWalletDebt.TabIndex = 4;
      this.lblWalletDebt.Text = "Дебет";
      this.lblWalletDebt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblWalletCredit
      // 
      this.lblWalletCredit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblWalletCredit.Location = new System.Drawing.Point(339, 26);
      this.lblWalletCredit.Name = "lblWalletCredit";
      this.lblWalletCredit.Size = new System.Drawing.Size(230, 20);
      this.lblWalletCredit.TabIndex = 6;
      this.lblWalletCredit.Text = "Кредит";
      this.lblWalletCredit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // cbWalletDebt
      // 
      this.cbWalletDebt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbWalletDebt.Location = new System.Drawing.Point(103, 49);
      this.cbWalletDebt.Name = "cbWalletDebt";
      this.cbWalletDebt.Size = new System.Drawing.Size(230, 20);
      this.cbWalletDebt.TabIndex = 5;
      // 
      // cbWalletCredit
      // 
      this.cbWalletCredit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbWalletCredit.Location = new System.Drawing.Point(339, 49);
      this.cbWalletCredit.Name = "cbWalletCredit";
      this.cbWalletCredit.Size = new System.Drawing.Size(230, 20);
      this.cbWalletCredit.TabIndex = 7;
      // 
      // lblContra
      // 
      this.lblContra.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblContra.Location = new System.Drawing.Point(3, 72);
      this.lblContra.Name = "lblContra";
      this.lblContra.Size = new System.Drawing.Size(94, 26);
      this.lblContra.TabIndex = 8;
      this.lblContra.Text = "Контрагент";
      this.lblContra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbContra
      // 
      this.cbContra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutPanel1.SetColumnSpan(this.cbContra, 2);
      this.cbContra.Location = new System.Drawing.Point(103, 75);
      this.cbContra.Name = "cbContra";
      this.cbContra.Size = new System.Drawing.Size(466, 20);
      this.cbContra.TabIndex = 9;
      // 
      // panel2
      // 
      this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panel2.Location = new System.Drawing.Point(3, 29);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(94, 14);
      this.panel2.TabIndex = 2;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.MainPanel2);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(584, 344);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Товары";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // MainPanel2
      // 
      this.MainPanel2.Controls.Add(this.panel3);
      this.MainPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainPanel2.Location = new System.Drawing.Point(3, 3);
      this.MainPanel2.Name = "MainPanel2";
      this.MainPanel2.Size = new System.Drawing.Size(578, 338);
      this.MainPanel2.TabIndex = 0;
      // 
      // panel3
      // 
      this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel3.Location = new System.Drawing.Point(0, 306);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(578, 32);
      this.panel3.TabIndex = 0;
      // 
      // EditOperation
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(592, 370);
      this.Controls.Add(this.tabControl1);
      this.Name = "EditOperation";
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.MainPanel1.ResumeLayout(false);
      this.MainPanel1.PerformLayout();
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.grpSum.ResumeLayout(false);
      this.grpSum.PerformLayout();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.grpOp.ResumeLayout(false);
      this.grpOp.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.MainPanel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.Panel MainPanel1;
    private System.Windows.Forms.GroupBox grpOp;
    private System.Windows.Forms.Label label1;
    private FreeLibSet.Controls.DateTimeBox edDate;
    private System.Windows.Forms.Label lblDate;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel panel1;
    private FreeLibSet.Controls.IntEditBox edOpOrder;
    private System.Windows.Forms.Label lblWalletDebt;
    private System.Windows.Forms.Label lblWalletCredit;
    private FreeLibSet.Controls.UserSelComboBox cbWalletDebt;
    private FreeLibSet.Controls.UserSelComboBox cbWalletCredit;
    private System.Windows.Forms.Label lblContra;
    private FreeLibSet.Controls.UserSelComboBox cbContra;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.TextBox edComment;
    private System.Windows.Forms.GroupBox grpSum;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private FreeLibSet.Controls.DecimalEditBox edSumBefore;
    private FreeLibSet.Controls.DecimalEditBox edSumOp;
    private FreeLibSet.Controls.DecimalEditBox edSumAfter;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Panel MainPanel2;
    private System.Windows.Forms.Panel panel3;
  }
}