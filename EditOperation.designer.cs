namespace BigPurse
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditOperation));
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.MainPanel1 = new System.Windows.Forms.Panel();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.edComment = new System.Windows.Forms.TextBox();
      this.grpOp = new System.Windows.Forms.GroupBox();
      this.edDate = new System.Windows.Forms.Label();
      this.dateTimeBox1 = new FreeLibSet.Controls.DateTimeBox();
      this.label1 = new System.Windows.Forms.Label();
      this.edOpOrder = new FreeLibSet.Controls.IntEditBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.MainPanel1.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.grpOp.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
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
      this.groupBox5.Location = new System.Drawing.Point(0, 183);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(578, 155);
      this.groupBox5.TabIndex = 1;
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
      this.edComment.Size = new System.Drawing.Size(566, 130);
      this.edComment.TabIndex = 0;
      // 
      // grpOp
      // 
      this.grpOp.Controls.Add(this.edOpOrder);
      this.grpOp.Controls.Add(this.label1);
      this.grpOp.Controls.Add(this.dateTimeBox1);
      this.grpOp.Controls.Add(this.edDate);
      this.grpOp.Dock = System.Windows.Forms.DockStyle.Top;
      this.grpOp.Location = new System.Drawing.Point(0, 0);
      this.grpOp.Name = "grpOp";
      this.grpOp.Size = new System.Drawing.Size(578, 183);
      this.grpOp.TabIndex = 0;
      this.grpOp.TabStop = false;
      this.grpOp.Text = "Операция";
      // 
      // edDate
      // 
      this.edDate.Location = new System.Drawing.Point(6, 22);
      this.edDate.Name = "edDate";
      this.edDate.Size = new System.Drawing.Size(96, 20);
      this.edDate.TabIndex = 0;
      this.edDate.Text = "Дата";
      this.edDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // dateTimeBox1
      // 
      this.dateTimeBox1.Location = new System.Drawing.Point(119, 22);
      this.dateTimeBox1.Name = "dateTimeBox1";
      this.dateTimeBox1.Size = new System.Drawing.Size(120, 20);
      this.dateTimeBox1.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(255, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(80, 20);
      this.label1.TabIndex = 2;
      this.label1.Text = "Порядок";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // OpOrder
      // 
      this.edOpOrder.BackColor = System.Drawing.SystemColors.Window;
      this.edOpOrder.ForeColor = System.Drawing.SystemColors.WindowText;
      this.edOpOrder.FormatProvider = new System.Globalization.CultureInfo("ru-RU");
      this.edOpOrder.Increment = 1;
      this.edOpOrder.Location = new System.Drawing.Point(341, 23);
      this.edOpOrder.Name = "OpOrder";
      this.edOpOrder.Size = new System.Drawing.Size(56, 20);
      this.edOpOrder.TabIndex = 3;
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
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.grpOp.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.Panel MainPanel1;
    private System.Windows.Forms.GroupBox grpOp;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.TextBox edComment;
    private System.Windows.Forms.Label label1;
    private FreeLibSet.Controls.DateTimeBox dateTimeBox1;
    private System.Windows.Forms.Label edDate;
    private FreeLibSet.Controls.IntEditBox edOpOrder;
  }
}