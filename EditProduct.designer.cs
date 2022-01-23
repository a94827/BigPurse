namespace App
{
  partial class EditProduct
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.cbParent = new FreeLibSet.Controls.UserSelComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.edName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.MainPanel2 = new System.Windows.Forms.Panel();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.grMUSets = new System.Windows.Forms.DataGridView();
      this.panSpbMUSets = new System.Windows.Forms.Panel();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.cbDescriptionPresence = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.cbQuantityPresence = new System.Windows.Forms.ComboBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.MainPanel1.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.MainPanel2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grMUSets)).BeginInit();
      this.groupBox2.SuspendLayout();
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
      this.tabControl1.Size = new System.Drawing.Size(582, 316);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.MainPanel1);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(574, 290);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Общие";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // MainPanel1
      // 
      this.MainPanel1.Controls.Add(this.groupBox5);
      this.MainPanel1.Controls.Add(this.groupBox1);
      this.MainPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainPanel1.Location = new System.Drawing.Point(3, 3);
      this.MainPanel1.Name = "MainPanel1";
      this.MainPanel1.Size = new System.Drawing.Size(568, 284);
      this.MainPanel1.TabIndex = 0;
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.edComment);
      this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox5.Location = new System.Drawing.Point(0, 83);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(568, 201);
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
      this.edComment.Size = new System.Drawing.Size(556, 176);
      this.edComment.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cbParent);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.edName);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(568, 83);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Товар, услуга";
      // 
      // cbParent
      // 
      this.cbParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbParent.Location = new System.Drawing.Point(180, 45);
      this.cbParent.Name = "cbParent";
      this.cbParent.Size = new System.Drawing.Size(372, 20);
      this.cbParent.TabIndex = 7;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(6, 45);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(158, 20);
      this.label2.TabIndex = 6;
      this.label2.Text = "&Родительский элемент";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // edName
      // 
      this.edName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edName.Location = new System.Drawing.Point(180, 19);
      this.edName.Name = "edName";
      this.edName.Size = new System.Drawing.Size(372, 20);
      this.edName.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(6, 19);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(158, 20);
      this.label1.TabIndex = 0;
      this.label1.Text = "&Название";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.MainPanel2);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(574, 290);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "В операции";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // MainPanel2
      // 
      this.MainPanel2.Controls.Add(this.groupBox3);
      this.MainPanel2.Controls.Add(this.groupBox2);
      this.MainPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainPanel2.Location = new System.Drawing.Point(3, 3);
      this.MainPanel2.Name = "MainPanel2";
      this.MainPanel2.Size = new System.Drawing.Size(568, 284);
      this.MainPanel2.TabIndex = 0;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.grMUSets);
      this.groupBox3.Controls.Add(this.panSpbMUSets);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox3.Location = new System.Drawing.Point(0, 89);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(568, 195);
      this.groupBox3.TabIndex = 1;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Сочетания единиц измерения";
      // 
      // grMUSets
      // 
      this.grMUSets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grMUSets.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grMUSets.Location = new System.Drawing.Point(3, 53);
      this.grMUSets.Name = "grMUSets";
      this.grMUSets.Size = new System.Drawing.Size(562, 139);
      this.grMUSets.TabIndex = 1;
      // 
      // panSpbMUSets
      // 
      this.panSpbMUSets.Dock = System.Windows.Forms.DockStyle.Top;
      this.panSpbMUSets.Location = new System.Drawing.Point(3, 16);
      this.panSpbMUSets.Name = "panSpbMUSets";
      this.panSpbMUSets.Size = new System.Drawing.Size(562, 37);
      this.panSpbMUSets.TabIndex = 0;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.cbDescriptionPresence);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.cbQuantityPresence);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(568, 89);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Наличие данные";
      // 
      // cbDescriptionPresence
      // 
      this.cbDescriptionPresence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbDescriptionPresence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDescriptionPresence.FormattingEnabled = true;
      this.cbDescriptionPresence.Location = new System.Drawing.Point(151, 27);
      this.cbDescriptionPresence.Name = "cbDescriptionPresence";
      this.cbDescriptionPresence.Size = new System.Drawing.Size(400, 21);
      this.cbDescriptionPresence.TabIndex = 1;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(15, 27);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(130, 21);
      this.label3.TabIndex = 0;
      this.label3.Text = "Описание";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(15, 54);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(130, 21);
      this.label4.TabIndex = 2;
      this.label4.Text = "Количество";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbQuantityPresence
      // 
      this.cbQuantityPresence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbQuantityPresence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbQuantityPresence.FormattingEnabled = true;
      this.cbQuantityPresence.Location = new System.Drawing.Point(151, 54);
      this.cbQuantityPresence.Name = "cbQuantityPresence";
      this.cbQuantityPresence.Size = new System.Drawing.Size(400, 21);
      this.cbQuantityPresence.TabIndex = 3;
      // 
      // EditProduct
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(582, 316);
      this.Controls.Add(this.tabControl1);
      this.Name = "EditProduct";
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.MainPanel1.ResumeLayout(false);
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.MainPanel2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grMUSets)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.Panel MainPanel1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox edName;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.TextBox edComment;
    private FreeLibSet.Controls.UserSelComboBox cbParent;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Panel MainPanel2;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.ComboBox cbQuantityPresence;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cbDescriptionPresence;
    private System.Windows.Forms.DataGridView grMUSets;
    private System.Windows.Forms.Panel panSpbMUSets;
  }
}