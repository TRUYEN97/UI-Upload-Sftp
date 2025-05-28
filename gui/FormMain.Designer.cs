using System.Threading.Tasks;

namespace Upload
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btSetting = new System.Windows.Forms.Button();
            this.btDeleteStation = new System.Windows.Forms.Button();
            this.btCreateStation = new System.Windows.Forms.Button();
            this.btDeleteProduct = new System.Windows.Forms.Button();
            this.btCreateProduct = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbStation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbProduct = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbProgram = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbAutoOpen = new System.Windows.Forms.CheckBox();
            this.btUpdate = new System.Windows.Forms.Button();
            this.treeFolder = new System.Windows.Forms.TreeView();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWindowName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMainFile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCloseCmd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOpenCmd = new System.Windows.Forms.TextBox();
            this.btDeleteProgram = new System.Windows.Forms.Button();
            this.btCreateProgram = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtMassage = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btSetting);
            this.panel1.Controls.Add(this.btDeleteStation);
            this.panel1.Controls.Add(this.btCreateStation);
            this.panel1.Controls.Add(this.btDeleteProduct);
            this.panel1.Controls.Add(this.btCreateProduct);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbbStation);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbbProduct);
            this.panel1.Location = new System.Drawing.Point(466, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(326, 132);
            this.panel1.TabIndex = 0;
            // 
            // btSetting
            // 
            this.btSetting.Image = ((System.Drawing.Image)(resources.GetObject("btSetting.Image")));
            this.btSetting.Location = new System.Drawing.Point(278, 3);
            this.btSetting.Name = "btSetting";
            this.btSetting.Size = new System.Drawing.Size(30, 31);
            this.btSetting.TabIndex = 10;
            this.btSetting.UseVisualStyleBackColor = true;
            this.btSetting.Click += new System.EventHandler(this.btSetting_Click);
            // 
            // btDeleteStation
            // 
            this.btDeleteStation.Location = new System.Drawing.Point(211, 80);
            this.btDeleteStation.Name = "btDeleteStation";
            this.btDeleteStation.Size = new System.Drawing.Size(48, 23);
            this.btDeleteStation.TabIndex = 12;
            this.btDeleteStation.Text = "Delete";
            this.btDeleteStation.UseVisualStyleBackColor = true;
            // 
            // btCreateStation
            // 
            this.btCreateStation.Location = new System.Drawing.Point(157, 80);
            this.btCreateStation.Name = "btCreateStation";
            this.btCreateStation.Size = new System.Drawing.Size(48, 23);
            this.btCreateStation.TabIndex = 11;
            this.btCreateStation.Text = "Add";
            this.btCreateStation.UseVisualStyleBackColor = true;
            // 
            // btDeleteProduct
            // 
            this.btDeleteProduct.Location = new System.Drawing.Point(211, 38);
            this.btDeleteProduct.Name = "btDeleteProduct";
            this.btDeleteProduct.Size = new System.Drawing.Size(48, 23);
            this.btDeleteProduct.TabIndex = 10;
            this.btDeleteProduct.Text = "Delete";
            this.btDeleteProduct.UseVisualStyleBackColor = true;
            // 
            // btCreateProduct
            // 
            this.btCreateProduct.Location = new System.Drawing.Point(157, 38);
            this.btCreateProduct.Name = "btCreateProduct";
            this.btCreateProduct.Size = new System.Drawing.Size(48, 23);
            this.btCreateProduct.TabIndex = 5;
            this.btCreateProduct.Text = "Add";
            this.btCreateProduct.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(30, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Station";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbStation
            // 
            this.cbbStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbStation.FormattingEnabled = true;
            this.cbbStation.Location = new System.Drawing.Point(30, 82);
            this.cbbStation.Name = "cbbStation";
            this.cbbStation.Size = new System.Drawing.Size(121, 21);
            this.cbbStation.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(30, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Product";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbProduct
            // 
            this.cbbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProduct.FormattingEnabled = true;
            this.cbbProduct.Location = new System.Drawing.Point(30, 38);
            this.cbbProduct.Name = "cbbProduct";
            this.cbbProduct.Size = new System.Drawing.Size(121, 21);
            this.cbbProduct.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(133, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(508, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Program";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbProgram
            // 
            this.cbbProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProgram.FormattingEnabled = true;
            this.cbbProgram.Location = new System.Drawing.Point(12, 25);
            this.cbbProgram.Name = "cbbProgram";
            this.cbbProgram.Size = new System.Drawing.Size(629, 21);
            this.cbbProgram.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.btDeleteProgram);
            this.panel2.Controls.Add(this.btCreateProgram);
            this.panel2.Controls.Add(this.cbbProgram);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(3, 138);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(789, 570);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel3.Controls.Add(this.cbAutoOpen);
            this.panel3.Controls.Add(this.btUpdate);
            this.panel3.Controls.Add(this.treeFolder);
            this.panel3.Controls.Add(this.cbEnabled);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Location = new System.Drawing.Point(12, 54);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(765, 507);
            this.panel3.TabIndex = 15;
            // 
            // cbAutoOpen
            // 
            this.cbAutoOpen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAutoOpen.AutoSize = true;
            this.cbAutoOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoOpen.Location = new System.Drawing.Point(86, 17);
            this.cbAutoOpen.Name = "cbAutoOpen";
            this.cbAutoOpen.Size = new System.Drawing.Size(84, 17);
            this.cbAutoOpen.TabIndex = 18;
            this.cbAutoOpen.Text = "Auto open";
            this.cbAutoOpen.UseVisualStyleBackColor = true;
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUpdate.Location = new System.Drawing.Point(125, 471);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(531, 24);
            this.btUpdate.TabIndex = 17;
            this.btUpdate.Text = "Update";
            this.btUpdate.UseVisualStyleBackColor = true;
            // 
            // treeFolder
            // 
            this.treeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeFolder.Location = new System.Drawing.Point(3, 223);
            this.treeFolder.Name = "treeFolder";
            this.treeFolder.Size = new System.Drawing.Size(759, 242);
            this.treeFolder.TabIndex = 12;
            // 
            // cbEnabled
            // 
            this.cbEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEnabled.AutoSize = true;
            this.cbEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEnabled.Location = new System.Drawing.Point(8, 17);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(72, 17);
            this.cbEnabled.TabIndex = 10;
            this.cbEnabled.Text = "Enabled";
            this.cbEnabled.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.txtWindowName);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.txtMainFile);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.txtCloseCmd);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.txtOpenCmd);
            this.panel5.Location = new System.Drawing.Point(3, 40);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(759, 177);
            this.panel5.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(743, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Window name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtWindowName
            // 
            this.txtWindowName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWindowName.Location = new System.Drawing.Point(3, 150);
            this.txtWindowName.Name = "txtWindowName";
            this.txtWindowName.Size = new System.Drawing.Size(744, 20);
            this.txtWindowName.TabIndex = 6;
            this.txtWindowName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(743, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Main file";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMainFile
            // 
            this.txtMainFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMainFile.Location = new System.Drawing.Point(3, 108);
            this.txtMainFile.Name = "txtMainFile";
            this.txtMainFile.Size = new System.Drawing.Size(744, 20);
            this.txtMainFile.TabIndex = 4;
            this.txtMainFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(743, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Close command";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCloseCmd
            // 
            this.txtCloseCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCloseCmd.Location = new System.Drawing.Point(3, 67);
            this.txtCloseCmd.Name = "txtCloseCmd";
            this.txtCloseCmd.Size = new System.Drawing.Size(744, 20);
            this.txtCloseCmd.TabIndex = 2;
            this.txtCloseCmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(743, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Open command";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtOpenCmd
            // 
            this.txtOpenCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOpenCmd.Location = new System.Drawing.Point(3, 27);
            this.txtOpenCmd.Name = "txtOpenCmd";
            this.txtOpenCmd.Size = new System.Drawing.Size(744, 20);
            this.txtOpenCmd.TabIndex = 0;
            this.txtOpenCmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btDeleteProgram
            // 
            this.btDeleteProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeleteProgram.Location = new System.Drawing.Point(705, 23);
            this.btDeleteProgram.Name = "btDeleteProgram";
            this.btDeleteProgram.Size = new System.Drawing.Size(48, 23);
            this.btDeleteProgram.TabIndex = 14;
            this.btDeleteProgram.Text = "Delete";
            this.btDeleteProgram.UseVisualStyleBackColor = true;
            // 
            // btCreateProgram
            // 
            this.btCreateProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreateProgram.Location = new System.Drawing.Point(651, 23);
            this.btCreateProgram.Name = "btCreateProgram";
            this.btCreateProgram.Size = new System.Drawing.Size(48, 23);
            this.btCreateProgram.TabIndex = 13;
            this.btCreateProgram.Text = "Add";
            this.btCreateProgram.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel4.Controls.Add(this.txtMassage);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(460, 132);
            this.panel4.TabIndex = 2;
            // 
            // txtMassage
            // 
            this.txtMassage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMassage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtMassage.Location = new System.Drawing.Point(3, 4);
            this.txtMassage.Multiline = true;
            this.txtMassage.Name = "txtMassage";
            this.txtMassage.ReadOnly = true;
            this.txtMassage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMassage.Size = new System.Drawing.Size(454, 125);
            this.txtMassage.TabIndex = 2;
            this.txtMassage.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(793, 712);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbStation;
        public System.Windows.Forms.ComboBox CbbStation {  get { return cbbStation; } }
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbProduct;
        public System.Windows.Forms.ComboBox CbbProduct {  get { return cbbProduct; } }
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbProgram;
        public System.Windows.Forms.ComboBox CbbProgram {  get { return cbbProgram; } }
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btCreateProduct;
        public System.Windows.Forms.Button BtCreateProduct { get {  return btCreateProduct; } }

        private System.Windows.Forms.Button btDeleteProduct;
        public System.Windows.Forms.Button BtDeleteProduct { get {  return btDeleteProduct; } }
        private System.Windows.Forms.Button btDeleteStation;
        public System.Windows.Forms.Button BtDeleteStation { get {  return btDeleteStation; } }
        private System.Windows.Forms.Button btCreateStation;
        public System.Windows.Forms.Button BtCreateStation { get { return btCreateStation; } }
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btSetting;
        private System.Windows.Forms.TextBox txtMassage;
        private System.Windows.Forms.Button btDeleteProgram;
        public System.Windows.Forms.Button BtDeleteProgram { get {  return btDeleteProgram; } }
        private System.Windows.Forms.Button btCreateProgram;
        public System.Windows.Forms.Button BtCreateVersion { get {  return btCreateProgram; } }
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btUpdate;
        public System.Windows.Forms.Button BtUpdate {  get { return btUpdate; } }
        private System.Windows.Forms.TreeView treeFolder;
        public System.Windows.Forms.TreeView TreeVersion {  get { return treeFolder; } }
        private System.Windows.Forms.CheckBox cbEnabled;
        public System.Windows.Forms.CheckBox CbEnabled {  get { return cbEnabled; } }
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWindowName;
        public System.Windows.Forms.TextBox TxtWindowName {  get { return txtWindowName; } }
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMainFile;
        public System.Windows.Forms.TextBox TxtMainFile {  get { return txtMainFile; } }
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCloseCmd;
        public System.Windows.Forms.TextBox TxtCloseCmd {  get { return txtCloseCmd; } }
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOpenCmd;
        private System.Windows.Forms.CheckBox cbAutoOpen;

        public System.Windows.Forms.CheckBox CbAutoOpen {  get { return cbAutoOpen; } }

        public System.Windows.Forms.TextBox TxtOpenCmd {  get { return txtOpenCmd; } }
    }
}

