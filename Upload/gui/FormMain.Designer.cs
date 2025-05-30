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
            this.btAccessUser = new System.Windows.Forms.Button();
            this.btSetting = new System.Windows.Forms.Button();
            this.btDeleteStation = new System.Windows.Forms.Button();
            this.btCreateStation = new System.Windows.Forms.Button();
            this.btDeleteProduct = new System.Windows.Forms.Button();
            this.btCreateProduct = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbStation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbProduct = new System.Windows.Forms.ComboBox();
            this.cbbProgram = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbAutoUpdate = new System.Windows.Forms.CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFTUVersion = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBOMVersion = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFCDVersion = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFWVersion = new System.Windows.Forms.TextBox();
            this.btDeleteProgram = new System.Windows.Forms.Button();
            this.cbAutoRemove = new System.Windows.Forms.CheckBox();
            this.btCreateProgram = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMainFile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCloseCmd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOpenCmd = new System.Windows.Forms.TextBox();
            this.pnAccessUser = new System.Windows.Forms.Panel();
            this.cbAutoOpen = new System.Windows.Forms.CheckBox();
            this.btUpdate = new System.Windows.Forms.Button();
            this.treeFolder = new System.Windows.Forms.TreeView();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtMassage = new System.Windows.Forms.TextBox();
            this.cbCloseAndClear = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btAccessUser);
            this.panel1.Controls.Add(this.btSetting);
            this.panel1.Controls.Add(this.btDeleteStation);
            this.panel1.Controls.Add(this.btCreateStation);
            this.panel1.Controls.Add(this.btDeleteProduct);
            this.panel1.Controls.Add(this.btCreateProduct);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbbStation);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbbProduct);
            this.panel1.Location = new System.Drawing.Point(681, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(326, 101);
            this.panel1.TabIndex = 0;
            // 
            // btAccessUser
            // 
            this.btAccessUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btAccessUser.Image = ((System.Drawing.Image)(resources.GetObject("btAccessUser.Image")));
            this.btAccessUser.Location = new System.Drawing.Point(273, 65);
            this.btAccessUser.Name = "btAccessUser";
            this.btAccessUser.Size = new System.Drawing.Size(33, 23);
            this.btAccessUser.TabIndex = 13;
            this.btAccessUser.UseVisualStyleBackColor = false;
            this.btAccessUser.Click += new System.EventHandler(this.btAccessUser_Click);
            // 
            // btSetting
            // 
            this.btSetting.Image = ((System.Drawing.Image)(resources.GetObject("btSetting.Image")));
            this.btSetting.Location = new System.Drawing.Point(273, 23);
            this.btSetting.Name = "btSetting";
            this.btSetting.Size = new System.Drawing.Size(33, 23);
            this.btSetting.TabIndex = 10;
            this.btSetting.UseVisualStyleBackColor = true;
            this.btSetting.Click += new System.EventHandler(this.btSetting_Click);
            // 
            // btDeleteStation
            // 
            this.btDeleteStation.Location = new System.Drawing.Point(202, 65);
            this.btDeleteStation.Name = "btDeleteStation";
            this.btDeleteStation.Size = new System.Drawing.Size(48, 23);
            this.btDeleteStation.TabIndex = 12;
            this.btDeleteStation.Text = "Delete";
            this.btDeleteStation.UseVisualStyleBackColor = true;
            // 
            // btCreateStation
            // 
            this.btCreateStation.Location = new System.Drawing.Point(148, 65);
            this.btCreateStation.Name = "btCreateStation";
            this.btCreateStation.Size = new System.Drawing.Size(48, 23);
            this.btCreateStation.TabIndex = 11;
            this.btCreateStation.Text = "Add";
            this.btCreateStation.UseVisualStyleBackColor = true;
            // 
            // btDeleteProduct
            // 
            this.btDeleteProduct.Location = new System.Drawing.Point(202, 23);
            this.btDeleteProduct.Name = "btDeleteProduct";
            this.btDeleteProduct.Size = new System.Drawing.Size(48, 23);
            this.btDeleteProduct.TabIndex = 10;
            this.btDeleteProduct.Text = "Delete";
            this.btDeleteProduct.UseVisualStyleBackColor = true;
            // 
            // btCreateProduct
            // 
            this.btCreateProduct.Location = new System.Drawing.Point(148, 23);
            this.btCreateProduct.Name = "btCreateProduct";
            this.btCreateProduct.Size = new System.Drawing.Size(48, 23);
            this.btCreateProduct.TabIndex = 5;
            this.btCreateProduct.Text = "Add";
            this.btCreateProduct.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(21, 51);
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
            this.cbbStation.Location = new System.Drawing.Point(21, 67);
            this.cbbStation.Name = "cbbStation";
            this.cbbStation.Size = new System.Drawing.Size(121, 21);
            this.cbbStation.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 7);
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
            this.cbbProduct.Location = new System.Drawing.Point(21, 23);
            this.cbbProduct.Name = "cbbProduct";
            this.cbbProduct.Size = new System.Drawing.Size(121, 21);
            this.cbbProduct.TabIndex = 0;
            // 
            // cbbProgram
            // 
            this.cbbProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProgram.FormattingEnabled = true;
            this.cbbProgram.Location = new System.Drawing.Point(531, 13);
            this.cbbProgram.Name = "cbbProgram";
            this.cbbProgram.Size = new System.Drawing.Size(343, 21);
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
            this.panel2.Location = new System.Drawing.Point(3, 107);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1004, 450);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel3.Controls.Add(this.cbCloseAndClear);
            this.panel3.Controls.Add(this.cbAutoUpdate);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.btDeleteProgram);
            this.panel3.Controls.Add(this.cbAutoRemove);
            this.panel3.Controls.Add(this.btCreateProgram);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.cbbProgram);
            this.panel3.Controls.Add(this.pnAccessUser);
            this.panel3.Controls.Add(this.cbAutoOpen);
            this.panel3.Controls.Add(this.btUpdate);
            this.panel3.Controls.Add(this.treeFolder);
            this.panel3.Controls.Add(this.cbEnabled);
            this.panel3.Location = new System.Drawing.Point(2, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(997, 442);
            this.panel3.TabIndex = 15;
            // 
            // cbAutoUpdate
            // 
            this.cbAutoUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAutoUpdate.AutoSize = true;
            this.cbAutoUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoUpdate.Location = new System.Drawing.Point(182, 14);
            this.cbAutoUpdate.Name = "cbAutoUpdate";
            this.cbAutoUpdate.Size = new System.Drawing.Size(95, 17);
            this.cbAutoUpdate.TabIndex = 22;
            this.cbAutoUpdate.Text = "Auto update";
            this.cbAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Silver;
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.txtFTUVersion);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.txtBOMVersion);
            this.panel7.Controls.Add(this.label10);
            this.panel7.Controls.Add(this.txtFCDVersion);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Controls.Add(this.txtFWVersion);
            this.panel7.Location = new System.Drawing.Point(3, 40);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(151, 180);
            this.panel7.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 19);
            this.label8.TabIndex = 7;
            this.label8.Text = "FTU version";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFTUVersion
            // 
            this.txtFTUVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFTUVersion.Location = new System.Drawing.Point(3, 150);
            this.txtFTUVersion.Name = "txtFTUVersion";
            this.txtFTUVersion.Size = new System.Drawing.Size(136, 20);
            this.txtFTUVersion.TabIndex = 6;
            this.txtFTUVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 19);
            this.label9.TabIndex = 5;
            this.label9.Text = "BOM version";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBOMVersion
            // 
            this.txtBOMVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBOMVersion.Location = new System.Drawing.Point(3, 108);
            this.txtBOMVersion.Name = "txtBOMVersion";
            this.txtBOMVersion.Size = new System.Drawing.Size(136, 20);
            this.txtBOMVersion.TabIndex = 4;
            this.txtBOMVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(135, 19);
            this.label10.TabIndex = 3;
            this.label10.Text = "FCD version";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFCDVersion
            // 
            this.txtFCDVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFCDVersion.Location = new System.Drawing.Point(3, 67);
            this.txtFCDVersion.Name = "txtFCDVersion";
            this.txtFCDVersion.Size = new System.Drawing.Size(136, 20);
            this.txtFCDVersion.TabIndex = 2;
            this.txtFCDVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(135, 19);
            this.label11.TabIndex = 1;
            this.label11.Text = "FW version";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFWVersion
            // 
            this.txtFWVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFWVersion.Location = new System.Drawing.Point(3, 27);
            this.txtFWVersion.Name = "txtFWVersion";
            this.txtFWVersion.Size = new System.Drawing.Size(136, 20);
            this.txtFWVersion.TabIndex = 0;
            this.txtFWVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btDeleteProgram
            // 
            this.btDeleteProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDeleteProgram.Location = new System.Drawing.Point(934, 11);
            this.btDeleteProgram.Name = "btDeleteProgram";
            this.btDeleteProgram.Size = new System.Drawing.Size(48, 23);
            this.btDeleteProgram.TabIndex = 14;
            this.btDeleteProgram.Text = "Delete";
            this.btDeleteProgram.UseVisualStyleBackColor = true;
            // 
            // cbAutoRemove
            // 
            this.cbAutoRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAutoRemove.AutoSize = true;
            this.cbAutoRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoRemove.Location = new System.Drawing.Point(288, 14);
            this.cbAutoRemove.Name = "cbAutoRemove";
            this.cbAutoRemove.Size = new System.Drawing.Size(97, 17);
            this.cbAutoRemove.TabIndex = 20;
            this.cbAutoRemove.Text = "Auto remove";
            this.cbAutoRemove.UseVisualStyleBackColor = true;
            // 
            // btCreateProgram
            // 
            this.btCreateProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCreateProgram.Location = new System.Drawing.Point(880, 11);
            this.btCreateProgram.Name = "btCreateProgram";
            this.btCreateProgram.Size = new System.Drawing.Size(48, 23);
            this.btCreateProgram.TabIndex = 13;
            this.btCreateProgram.Text = "Add";
            this.btCreateProgram.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.Silver;
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.txtVersion);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.txtMainFile);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.txtCloseCmd);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.txtOpenCmd);
            this.panel5.Location = new System.Drawing.Point(528, 40);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(466, 180);
            this.panel5.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(450, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "Version";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.Location = new System.Drawing.Point(3, 150);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(451, 20);
            this.txtVersion.TabIndex = 6;
            this.txtVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(450, 19);
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
            this.txtMainFile.Size = new System.Drawing.Size(451, 20);
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
            this.label6.Size = new System.Drawing.Size(450, 19);
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
            this.txtCloseCmd.Size = new System.Drawing.Size(451, 20);
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
            this.label4.Size = new System.Drawing.Size(450, 19);
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
            this.txtOpenCmd.Size = new System.Drawing.Size(451, 20);
            this.txtOpenCmd.TabIndex = 0;
            this.txtOpenCmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnAccessUser
            // 
            this.pnAccessUser.BackColor = System.Drawing.Color.Silver;
            this.pnAccessUser.Location = new System.Drawing.Point(157, 40);
            this.pnAccessUser.Name = "pnAccessUser";
            this.pnAccessUser.Size = new System.Drawing.Size(368, 180);
            this.pnAccessUser.TabIndex = 12;
            // 
            // cbAutoOpen
            // 
            this.cbAutoOpen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAutoOpen.AutoSize = true;
            this.cbAutoOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoOpen.Location = new System.Drawing.Point(88, 14);
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
            this.btUpdate.Location = new System.Drawing.Point(124, 415);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(763, 24);
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
            this.treeFolder.Size = new System.Drawing.Size(991, 186);
            this.treeFolder.TabIndex = 12;
            // 
            // cbEnabled
            // 
            this.cbEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEnabled.AutoSize = true;
            this.cbEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEnabled.Location = new System.Drawing.Point(8, 14);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(72, 17);
            this.cbEnabled.TabIndex = 10;
            this.cbEnabled.Text = "Enabled";
            this.cbEnabled.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel4.Controls.Add(this.txtMassage);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(675, 101);
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
            this.txtMassage.Size = new System.Drawing.Size(669, 94);
            this.txtMassage.TabIndex = 2;
            this.txtMassage.TabStop = false;
            // 
            // cbCloseAndClear
            // 
            this.cbCloseAndClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCloseAndClear.AutoSize = true;
            this.cbCloseAndClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCloseAndClear.Location = new System.Drawing.Point(394, 14);
            this.cbCloseAndClear.Name = "cbCloseAndClear";
            this.cbCloseAndClear.Size = new System.Drawing.Size(114, 17);
            this.cbCloseAndClear.TabIndex = 23;
            this.cbCloseAndClear.Text = "Close and clear";
            this.cbCloseAndClear.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbProduct;
        private System.Windows.Forms.ComboBox cbbProgram;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btCreateProduct;
        private System.Windows.Forms.Button btDeleteProduct;
        private System.Windows.Forms.Button btDeleteStation;
        private System.Windows.Forms.Button btCreateStation;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btSetting;
        private System.Windows.Forms.TextBox txtMassage;
        private System.Windows.Forms.Button btDeleteProgram;
        private System.Windows.Forms.Button btCreateProgram;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.TreeView treeFolder;
        private System.Windows.Forms.CheckBox cbEnabled;
        private System.Windows.Forms.CheckBox cbAutoOpen;
        private System.Windows.Forms.Panel pnAccessUser;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMainFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCloseCmd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOpenCmd;
        private System.Windows.Forms.CheckBox cbAutoRemove;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFTUVersion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBOMVersion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFCDVersion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFWVersion;
        private System.Windows.Forms.CheckBox cbAutoUpdate;
        private System.Windows.Forms.Button btAccessUser;
        private System.Windows.Forms.CheckBox cbCloseAndClear;

        public System.Windows.Forms.ComboBox CbbStation { get { return cbbStation; } }
        public System.Windows.Forms.ComboBox CbbProduct { get { return cbbProduct; } }
        public System.Windows.Forms.ComboBox CbbProgram { get { return cbbProgram; } }
        public System.Windows.Forms.Button BtCreateProduct { get { return btCreateProduct; } }
        public System.Windows.Forms.Button BtDeleteProduct { get { return btDeleteProduct; } }
        public System.Windows.Forms.Button BtDeleteStation { get { return btDeleteStation; } }
        public System.Windows.Forms.Button BtCreateStation { get { return btCreateStation; } }
        public System.Windows.Forms.Button BtDeleteProgram { get { return btDeleteProgram; } }
        public System.Windows.Forms.Button BtCreateVersion { get { return btCreateProgram; } }
        public System.Windows.Forms.Button BtUpdate { get { return btUpdate; } }
        public System.Windows.Forms.TreeView TreeVersion { get { return treeFolder; } }
        public System.Windows.Forms.CheckBox CbEnabled { get { return cbEnabled; } }
        public System.Windows.Forms.TextBox TxtWindowName { get { return txtVersion; } }
        public System.Windows.Forms.TextBox TxtMainFile { get { return txtMainFile; } }
        public System.Windows.Forms.TextBox TxtCloseCmd { get { return txtCloseCmd; } }
        public System.Windows.Forms.TextBox TxtBOMVersion { get { return txtBOMVersion; } }
        public System.Windows.Forms.TextBox TxtFCDVersion { get { return txtFCDVersion; } }
        public System.Windows.Forms.TextBox TxtFTUVersion { get { return txtFTUVersion; } }
        public System.Windows.Forms.TextBox TxtFWVersion { get { return txtFWVersion; } }
        public System.Windows.Forms.CheckBox CbAutoOpen {  get { return cbAutoOpen; } }
        public System.Windows.Forms.CheckBox CbAutoRemove {  get { return cbAutoRemove; } }
        public System.Windows.Forms.CheckBox CbAutoUpdate {  get { return cbAutoUpdate; } }
        public System.Windows.Forms.CheckBox CbCloseAndClear {  get { return cbCloseAndClear; } }
        public System.Windows.Forms.TextBox TxtOpenCmd {  get { return txtOpenCmd; } }
    }
}

