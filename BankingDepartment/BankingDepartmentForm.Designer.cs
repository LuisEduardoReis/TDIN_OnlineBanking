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
            this.SuspendLayout();
            // 
            // orderViewListBox
            // 
            this.orderViewListBox.FormattingEnabled = true;
            this.orderViewListBox.Location = new System.Drawing.Point(12, 12);
            this.orderViewListBox.Name = "orderViewListBox";
            this.orderViewListBox.Size = new System.Drawing.Size(459, 238);
            this.orderViewListBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 360);
            this.Controls.Add(this.orderViewListBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox orderViewListBox;
    }
}

