namespace App
{
  partial class EditProductMUSet
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
      this.cbMU1 = new FreeLibSet.Controls.UserSelComboBox();
      this.cbMU2 = new FreeLibSet.Controls.UserSelComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cbMU3 = new FreeLibSet.Controls.UserSelComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.MainPanel.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainPanel
      // 
      this.MainPanel.Controls.Add(this.groupBox1);
      this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainPanel.Location = new System.Drawing.Point(0, 0);
      this.MainPanel.Name = "MainPanel";
      this.MainPanel.Size = new System.Drawing.Size(253, 146);
      this.MainPanel.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cbMU3);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.cbMU2);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.cbMU1);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(253, 139);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Единицы измерения";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 28);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(60, 20);
      this.label1.TabIndex = 0;
      this.label1.Text = "&1";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbMU1
      // 
      this.cbMU1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbMU1.Location = new System.Drawing.Point(84, 28);
      this.cbMU1.Name = "cbMU1";
      this.cbMU1.Size = new System.Drawing.Size(150, 20);
      this.cbMU1.TabIndex = 1;
      // 
      // cbMU2
      // 
      this.cbMU2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbMU2.Location = new System.Drawing.Point(84, 62);
      this.cbMU2.Name = "cbMU2";
      this.cbMU2.Size = new System.Drawing.Size(150, 20);
      this.cbMU2.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(8, 62);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(60, 20);
      this.label2.TabIndex = 2;
      this.label2.Text = "&2";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cbMU3
      // 
      this.cbMU3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbMU3.Location = new System.Drawing.Point(84, 97);
      this.cbMU3.Name = "cbMU3";
      this.cbMU3.Size = new System.Drawing.Size(150, 20);
      this.cbMU3.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(8, 97);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(60, 20);
      this.label3.TabIndex = 4;
      this.label3.Text = "&3";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // EditProductMUSet
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(253, 146);
      this.Controls.Add(this.MainPanel);
      this.Name = "EditProductMUSet";
      this.MainPanel.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel MainPanel;
    private System.Windows.Forms.GroupBox groupBox1;
    private FreeLibSet.Controls.UserSelComboBox cbMU3;
    private System.Windows.Forms.Label label3;
    private FreeLibSet.Controls.UserSelComboBox cbMU2;
    private System.Windows.Forms.Label label2;
    private FreeLibSet.Controls.UserSelComboBox cbMU1;
    private System.Windows.Forms.Label label1;
  }
}