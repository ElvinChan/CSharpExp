namespace DataOperation
{
    partial class Form1
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
            this.cbxCity1 = new System.Windows.Forms.ComboBox();
            this.cbxCity2 = new System.Windows.Forms.ComboBox();
            this.btnExportData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxCity1
            // 
            this.cbxCity1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCity1.FormattingEnabled = true;
            this.cbxCity1.Location = new System.Drawing.Point(59, 75);
            this.cbxCity1.Name = "cbxCity1";
            this.cbxCity1.Size = new System.Drawing.Size(121, 21);
            this.cbxCity1.TabIndex = 0;
            this.cbxCity1.SelectedIndexChanged += new System.EventHandler(this.cbxCity1_SelectedIndexChanged);
            // 
            // cbxCity2
            // 
            this.cbxCity2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCity2.FormattingEnabled = true;
            this.cbxCity2.Location = new System.Drawing.Point(202, 75);
            this.cbxCity2.Name = "cbxCity2";
            this.cbxCity2.Size = new System.Drawing.Size(121, 21);
            this.cbxCity2.TabIndex = 1;
            // 
            // btnExportData
            // 
            this.btnExportData.Location = new System.Drawing.Point(59, 134);
            this.btnExportData.Name = "btnExportData";
            this.btnExportData.Size = new System.Drawing.Size(121, 23);
            this.btnExportData.TabIndex = 3;
            this.btnExportData.Text = "导出数据";
            this.btnExportData.UseVisualStyleBackColor = true;
            this.btnExportData.Click += new System.EventHandler(this.btnExportData_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 262);
            this.Controls.Add(this.btnExportData);
            this.Controls.Add(this.cbxCity2);
            this.Controls.Add(this.cbxCity1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxCity1;
        private System.Windows.Forms.ComboBox cbxCity2;
        private System.Windows.Forms.Button btnExportData;
    }
}

