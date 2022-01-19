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
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.edUnit2List = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.cbUnit2Presence = new System.Windows.Forms.ComboBox();
      this.label7 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.edUnit1List = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.cbUnit1Presence = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.cbDescriptionPresence = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.MainPanel1.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.MainPanel2.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.groupBox3.SuspendLayout();
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
      this.tabPage1.Size = new System.Drawing.Size(574, 283);
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
      this.MainPanel1.Size = new System.Drawing.Size(568, 277);
      this.MainPanel1.TabIndex = 0;
      // 
      // groupBox5
      // 
      this.groupBox5.Controls.Add(this.edComment);
      this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox5.Location = new System.Drawing.Point(0, 83);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(568, 194);
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
      this.edComment.Size = new System.Drawing.Size(556, 169);
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
      this.MainPanel2.Controls.Add(this.groupBox4);
      this.MainPanel2.Controls.Add(this.groupBox3);
      this.MainPanel2.Controls.Add(this.groupBox2);
      this.MainPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainPanel2.Location = new System.Drawing.Point(3, 3);
      this.MainPanel2.Name = "MainPanel2";
      this.MainPanel2.Size = new System.Drawing.Size(568, 284);
      this.MainPanel2.TabIndex = 0;
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.edUnit2List);
      this.groupBox4.Controls.Add(this.label6);
      this.groupBox4.Controls.Add(this.cbUnit2Presence);
      this.groupBox4.Controls.Add(this.label7);
      this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox4.Location = new System.Drawing.Point(0, 169);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(568, 109);
      this.groupBox4.TabIndex = 3;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Поле \"Количество 2\"";
      // 
      // edUnit2List
      // 
      this.edUnit2List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edUnit2List.Location = new System.Drawing.Point(23, 79);
      this.edUnit2List.Name = "edUnit2List";
      this.edUnit2List.Size = new System.Drawing.Size(528, 20);
      this.edUnit2List.TabIndex = 3;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(15, 58);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(229, 13);
      this.label6.TabIndex = 2;
      this.label6.Text = "Фиксированный список единиц измерения";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbUnit2Presence
      // 
      this.cbUnit2Presence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbUnit2Presence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbUnit2Presence.FormattingEnabled = true;
      this.cbUnit2Presence.Location = new System.Drawing.Point(151, 27);
      this.cbUnit2Presence.Name = "cbUnit2Presence";
      this.cbUnit2Presence.Size = new System.Drawing.Size(400, 21);
      this.cbUnit2Presence.TabIndex = 1;
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(15, 27);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(130, 21);
      this.label7.TabIndex = 0;
      this.label7.Text = "Наличие";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.edUnit1List);
      this.groupBox3.Controls.Add(this.label5);
      this.groupBox3.Controls.Add(this.cbUnit1Presence);
      this.groupBox3.Controls.Add(this.label4);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox3.Location = new System.Drawing.Point(0, 60);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(568, 109);
      this.groupBox3.TabIndex = 2;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Поле \"Количество 1\"";
      // 
      // edUnit1List
      // 
      this.edUnit1List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edUnit1List.Location = new System.Drawing.Point(23, 79);
      this.edUnit1List.Name = "edUnit1List";
      this.edUnit1List.Size = new System.Drawing.Size(528, 20);
      this.edUnit1List.TabIndex = 3;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(15, 58);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(229, 13);
      this.label5.TabIndex = 2;
      this.label5.Text = "Фиксированный список единиц измерения";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbUnit1Presence
      // 
      this.cbUnit1Presence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbUnit1Presence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbUnit1Presence.FormattingEnabled = true;
      this.cbUnit1Presence.Location = new System.Drawing.Point(151, 27);
      this.cbUnit1Presence.Name = "cbUnit1Presence";
      this.cbUnit1Presence.Size = new System.Drawing.Size(400, 21);
      this.cbUnit1Presence.TabIndex = 1;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(15, 27);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(130, 21);
      this.label4.TabIndex = 0;
      this.label4.Text = "Наличие";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.cbDescriptionPresence);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(568, 60);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Поле \"Описание\"";
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
      this.label3.Text = "Наличие";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
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
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.TextBox edUnit2List;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ComboBox cbUnit2Presence;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.TextBox edUnit1List;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox cbUnit1Presence;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cbDescriptionPresence;
  }
}