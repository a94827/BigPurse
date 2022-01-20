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
      this.edUnit2List = new System.Windows.Forms.TextBox();
      this.cbUnit2Presence = new System.Windows.Forms.ComboBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.edUnit1List = new System.Windows.Forms.TextBox();
      this.cbUnit1Presence = new System.Windows.Forms.ComboBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.cbDescriptionPresence = new System.Windows.Forms.ComboBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.groupBox6 = new System.Windows.Forms.GroupBox();
      this.lblInfoMU1 = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.panSpbMU1 = new System.Windows.Forms.Panel();
      this.grMU1 = new System.Windows.Forms.DataGridView();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.grMU2 = new System.Windows.Forms.DataGridView();
      this.panSpbMU2 = new System.Windows.Forms.Panel();
      this.lblInfoMU2 = new System.Windows.Forms.Label();
      this.groupBox7 = new System.Windows.Forms.GroupBox();
      this.groupBox8 = new System.Windows.Forms.GroupBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.MainPanel1.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.MainPanel2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grMU1)).BeginInit();
      this.groupBox4.SuspendLayout();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grMU2)).BeginInit();
      this.groupBox7.SuspendLayout();
      this.groupBox8.SuspendLayout();
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
      this.tabControl1.Size = new System.Drawing.Size(582, 362);
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
      this.tabPage2.Size = new System.Drawing.Size(574, 336);
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
      this.MainPanel2.Size = new System.Drawing.Size(568, 330);
      this.MainPanel2.TabIndex = 0;
      // 
      // edUnit2List
      // 
      this.edUnit2List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edUnit2List.Location = new System.Drawing.Point(284, 252);
      this.edUnit2List.Name = "edUnit2List";
      this.edUnit2List.Size = new System.Drawing.Size(275, 20);
      this.edUnit2List.TabIndex = 3;
      // 
      // cbUnit2Presence
      // 
      this.cbUnit2Presence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbUnit2Presence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbUnit2Presence.FormattingEnabled = true;
      this.cbUnit2Presence.Location = new System.Drawing.Point(6, 19);
      this.cbUnit2Presence.Name = "cbUnit2Presence";
      this.cbUnit2Presence.Size = new System.Drawing.Size(263, 21);
      this.cbUnit2Presence.TabIndex = 1;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.tableLayoutPanel1);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox3.Location = new System.Drawing.Point(0, 42);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(568, 288);
      this.groupBox3.TabIndex = 1;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Поля \"Количество\" и \"Единица измерения\"";
      // 
      // edUnit1List
      // 
      this.edUnit1List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.edUnit1List.Location = new System.Drawing.Point(3, 252);
      this.edUnit1List.Name = "edUnit1List";
      this.edUnit1List.Size = new System.Drawing.Size(275, 20);
      this.edUnit1List.TabIndex = 3;
      // 
      // cbUnit1Presence
      // 
      this.cbUnit1Presence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbUnit1Presence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbUnit1Presence.FormattingEnabled = true;
      this.cbUnit1Presence.Location = new System.Drawing.Point(6, 19);
      this.cbUnit1Presence.Name = "cbUnit1Presence";
      this.cbUnit1Presence.Size = new System.Drawing.Size(263, 21);
      this.cbUnit1Presence.TabIndex = 1;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.cbDescriptionPresence);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(568, 42);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Наличие поля \"Описание\"";
      // 
      // cbDescriptionPresence
      // 
      this.cbDescriptionPresence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbDescriptionPresence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDescriptionPresence.FormattingEnabled = true;
      this.cbDescriptionPresence.Location = new System.Drawing.Point(12, 14);
      this.cbDescriptionPresence.Name = "cbDescriptionPresence";
      this.cbDescriptionPresence.Size = new System.Drawing.Size(550, 21);
      this.cbDescriptionPresence.TabIndex = 1;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.groupBox8, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.edUnit2List, 1, 2);
      this.tableLayoutPanel1.Controls.Add(this.edUnit1List, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.groupBox6, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.groupBox7, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(562, 269);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // groupBox6
      // 
      this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox6.Controls.Add(this.panel1);
      this.groupBox6.Controls.Add(this.lblInfoMU1);
      this.groupBox6.Location = new System.Drawing.Point(3, 58);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new System.Drawing.Size(275, 188);
      this.groupBox6.TabIndex = 2;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "Фиксированный список ед.изм. 1";
      // 
      // lblInfoMU1
      // 
      this.lblInfoMU1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblInfoMU1.Location = new System.Drawing.Point(3, 162);
      this.lblInfoMU1.Name = "lblInfoMU1";
      this.lblInfoMU1.Size = new System.Drawing.Size(269, 23);
      this.lblInfoMU1.TabIndex = 1;
      this.lblInfoMU1.Text = "???";
      this.lblInfoMU1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.grMU1);
      this.panel1.Controls.Add(this.panSpbMU1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 16);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(269, 146);
      this.panel1.TabIndex = 0;
      // 
      // panSpbMU1
      // 
      this.panSpbMU1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panSpbMU1.Location = new System.Drawing.Point(0, 0);
      this.panSpbMU1.Name = "panSpbMU1";
      this.panSpbMU1.Size = new System.Drawing.Size(269, 34);
      this.panSpbMU1.TabIndex = 0;
      // 
      // grMU1
      // 
      this.grMU1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grMU1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grMU1.Location = new System.Drawing.Point(0, 34);
      this.grMU1.Name = "grMU1";
      this.grMU1.Size = new System.Drawing.Size(269, 112);
      this.grMU1.TabIndex = 1;
      // 
      // groupBox4
      // 
      this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox4.Controls.Add(this.panel2);
      this.groupBox4.Controls.Add(this.lblInfoMU2);
      this.groupBox4.Location = new System.Drawing.Point(284, 58);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(275, 188);
      this.groupBox4.TabIndex = 5;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Фиксированный список ед.изм. 2";
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.grMU2);
      this.panel2.Controls.Add(this.panSpbMU2);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(3, 16);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(269, 146);
      this.panel2.TabIndex = 0;
      // 
      // grMU2
      // 
      this.grMU2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grMU2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grMU2.Location = new System.Drawing.Point(0, 34);
      this.grMU2.Name = "grMU2";
      this.grMU2.Size = new System.Drawing.Size(269, 112);
      this.grMU2.TabIndex = 1;
      // 
      // panSpbMU2
      // 
      this.panSpbMU2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panSpbMU2.Location = new System.Drawing.Point(0, 0);
      this.panSpbMU2.Name = "panSpbMU2";
      this.panSpbMU2.Size = new System.Drawing.Size(269, 34);
      this.panSpbMU2.TabIndex = 0;
      // 
      // lblInfoMU2
      // 
      this.lblInfoMU2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblInfoMU2.Location = new System.Drawing.Point(3, 162);
      this.lblInfoMU2.Name = "lblInfoMU2";
      this.lblInfoMU2.Size = new System.Drawing.Size(269, 23);
      this.lblInfoMU2.TabIndex = 1;
      this.lblInfoMU2.Text = "???";
      this.lblInfoMU2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // groupBox7
      // 
      this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox7.Controls.Add(this.cbUnit1Presence);
      this.groupBox7.Location = new System.Drawing.Point(3, 3);
      this.groupBox7.Name = "groupBox7";
      this.groupBox7.Size = new System.Drawing.Size(275, 49);
      this.groupBox7.TabIndex = 6;
      this.groupBox7.TabStop = false;
      this.groupBox7.Text = "Наличие количества &1";
      // 
      // groupBox8
      // 
      this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox8.Controls.Add(this.cbUnit2Presence);
      this.groupBox8.Location = new System.Drawing.Point(284, 3);
      this.groupBox8.Name = "groupBox8";
      this.groupBox8.Size = new System.Drawing.Size(275, 49);
      this.groupBox8.TabIndex = 7;
      this.groupBox8.TabStop = false;
      this.groupBox8.Text = "Наличие количества &2";
      // 
      // EditProduct
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(582, 362);
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
      this.groupBox2.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.groupBox6.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grMU1)).EndInit();
      this.groupBox4.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grMU2)).EndInit();
      this.groupBox7.ResumeLayout(false);
      this.groupBox8.ResumeLayout(false);
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
    private System.Windows.Forms.TextBox edUnit2List;
    private System.Windows.Forms.ComboBox cbUnit2Presence;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.TextBox edUnit1List;
    private System.Windows.Forms.ComboBox cbUnit1Presence;
    private System.Windows.Forms.ComboBox cbDescriptionPresence;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.GroupBox groupBox6;
    private System.Windows.Forms.Label lblInfoMU1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.DataGridView grMU2;
    private System.Windows.Forms.Panel panSpbMU2;
    private System.Windows.Forms.Label lblInfoMU2;
    private System.Windows.Forms.DataGridView grMU1;
    private System.Windows.Forms.Panel panSpbMU1;
    private System.Windows.Forms.GroupBox groupBox7;
    private System.Windows.Forms.GroupBox groupBox8;
  }
}