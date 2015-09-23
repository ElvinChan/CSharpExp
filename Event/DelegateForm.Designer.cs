namespace Event
{
    partial class DelegateForm
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
            this.myTripleClick2 = new System.Windows.Forms.Button();
            this.userControlEvent1 = new Event.UserControlEvent();
            this.myTripleClick1 = new Event.MyTripleClick();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // myTripleClick2
            // 
            this.myTripleClick2.Location = new System.Drawing.Point(235, 63);
            this.myTripleClick2.Name = "myTripleClick2";
            this.myTripleClick2.Size = new System.Drawing.Size(75, 21);
            this.myTripleClick2.TabIndex = 1;
            this.myTripleClick2.Text = "盗用三连击";
            this.myTripleClick2.UseVisualStyleBackColor = true;
            this.myTripleClick2.Click += new System.EventHandler(this.myTripleClick2_Click);
            // 
            // userControlEvent1
            // 
            this.userControlEvent1.Location = new System.Drawing.Point(3, 106);
            this.userControlEvent1.Name = "userControlEvent1";
            this.userControlEvent1.Size = new System.Drawing.Size(341, 90);
            this.userControlEvent1.TabIndex = 2;
            this.userControlEvent1.UcEvent += new Event.UcDelegate(this.userControlEvent1_UcEvent);
            // 
            // myTripleClick1
            // 
            this.myTripleClick1.Location = new System.Drawing.Point(235, 26);
            this.myTripleClick1.Name = "myTripleClick1";
            this.myTripleClick1.Size = new System.Drawing.Size(75, 21);
            this.myTripleClick1.TabIndex = 0;
            this.myTripleClick1.Text = "三连击";
            this.myTripleClick1.UseVisualStyleBackColor = true;
            this.myTripleClick1.Click += new System.EventHandler(this.myTripleClick1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "注意，这个按钮是从工具栏拖进来的";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "相当于MyTripleClick的一个实例对象";
            // 
            // DelegateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 242);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userControlEvent1);
            this.Controls.Add(this.myTripleClick2);
            this.Controls.Add(this.myTripleClick1);
            this.Name = "DelegateForm";
            this.Text = "DelegateForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyTripleClick myTripleClick1;
        private System.Windows.Forms.Button myTripleClick2;
        private UserControlEvent userControlEvent1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

