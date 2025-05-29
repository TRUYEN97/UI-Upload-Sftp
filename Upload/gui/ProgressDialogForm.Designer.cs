namespace Upload.gui
{
    partial class ProgressDialogForm
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
            this.btCancel = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lbMess = new System.Windows.Forms.Label();
            this.lbCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(244, 97);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 0;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 60);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(317, 18);
            this.progressBar.TabIndex = 1;
            // 
            // lbMess
            // 
            this.lbMess.AutoEllipsis = true;
            this.lbMess.Location = new System.Drawing.Point(12, 9);
            this.lbMess.Name = "lbMess";
            this.lbMess.Size = new System.Drawing.Size(317, 48);
            this.lbMess.TabIndex = 2;
            this.lbMess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbMess.UseCompatibleTextRendering = true;
            // 
            // lbCount
            // 
            this.lbCount.Location = new System.Drawing.Point(12, 81);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(317, 13);
            this.lbCount.TabIndex = 3;
            this.lbCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbCount.UseCompatibleTextRendering = true;
            // 
            // ProgressDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 128);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.lbMess);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressDialogForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progress";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressDialogForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lbMess;
        private System.Windows.Forms.Label lbCount;
    }
}