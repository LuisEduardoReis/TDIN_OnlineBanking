namespace BankingDepartment
{
    partial class BankingDepartmentForm
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
            this.orderViewListBox = new System.Windows.Forms.ListBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // orderViewListBox
            // 
            this.orderViewListBox.ColumnWidth = 10;
            this.orderViewListBox.FormattingEnabled = true;
            this.orderViewListBox.Location = new System.Drawing.Point(12, 12);
            this.orderViewListBox.Name = "orderViewListBox";
            this.orderViewListBox.Size = new System.Drawing.Size(459, 238);
            this.orderViewListBox.TabIndex = 0;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(106, 266);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(172, 20);
            this.txtValue.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Stock Value";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(396, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Execute";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BankingDepartmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 360);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.orderViewListBox);
            this.Name = "BankingDepartmentForm";
            this.Text = "Banking Department";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox orderViewListBox;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

