namespace Upload.gui
{
    partial class ConfirmOverrideForm
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
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.btOk = new System.Windows.Forms.Button();
            this.txtMess = new System.Windows.Forms.TextBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbAll
            // 
            this.cbAll.AutoSize = true;
            this.cbAll.Location = new System.Drawing.Point(53, 104);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(75, 17);
            this.cbAll.TabIndex = 0;
            this.cbAll.Text = "Cho tất cả";
            this.cbAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbAll.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(194, 98);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(63, 23);
            this.btOk.TabIndex = 2;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // txtMess
            // 
            this.txtMess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMess.Location = new System.Drawing.Point(12, 27);
            this.txtMess.Multiline = true;
            this.txtMess.Name = "txtMess";
            this.txtMess.ReadOnly = true;
            this.txtMess.Size = new System.Drawing.Size(258, 55);
            this.txtMess.TabIndex = 3;
            this.txtMess.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(269, 98);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(63, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // ConfirmOverrideFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 127);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.txtMess);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.cbAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfirmOverrideFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông báo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbAll;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.TextBox txtMess;
        private System.Windows.Forms.Button btCancel;
    }
}