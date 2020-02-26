namespace Manipulation
{
    partial class FrmManipulation
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManipulation));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbHasPrimaryKey = new System.Windows.Forms.CheckBox();
            this.cbxIdentity = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbPrimaryKey = new System.Windows.Forms.ComboBox();
            this.txtNewTable = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTranfer = new System.Windows.Forms.Button();
            this.txtCurrentTable = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDesign = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.rbCommand = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMethods = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDataSource = new System.Windows.Forms.ComboBox();
            this.btnFreeAcess = new System.Windows.Forms.Button();
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox3.Controls.Add(this.cbHasPrimaryKey);
            this.groupBox3.Controls.Add(this.cbxIdentity);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cbPrimaryKey);
            this.groupBox3.Controls.Add(this.txtNewTable);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnTranfer);
            this.groupBox3.Controls.Add(this.txtCurrentTable);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(11, 366);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1062, 84);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tranfer Data:";
            // 
            // cbHasPrimaryKey
            // 
            this.cbHasPrimaryKey.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbHasPrimaryKey.AutoSize = true;
            this.cbHasPrimaryKey.Enabled = false;
            this.cbHasPrimaryKey.Location = new System.Drawing.Point(497, 49);
            this.cbHasPrimaryKey.Name = "cbHasPrimaryKey";
            this.cbHasPrimaryKey.Size = new System.Drawing.Size(133, 23);
            this.cbHasPrimaryKey.TabIndex = 8;
            this.cbHasPrimaryKey.Text = "Has Primary Key";
            this.cbHasPrimaryKey.UseVisualStyleBackColor = true;
            this.cbHasPrimaryKey.CheckedChanged += new System.EventHandler(this.cbHasPrimaryKey_CheckedChanged);
            // 
            // cbxIdentity
            // 
            this.cbxIdentity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxIdentity.AutoSize = true;
            this.cbxIdentity.Enabled = false;
            this.cbxIdentity.Location = new System.Drawing.Point(840, 51);
            this.cbxIdentity.Name = "cbxIdentity";
            this.cbxIdentity.Size = new System.Drawing.Size(73, 23);
            this.cbxIdentity.TabIndex = 10;
            this.cbxIdentity.Text = "Identity";
            this.cbxIdentity.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(632, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 19);
            this.label7.TabIndex = 8;
            this.label7.Text = "Primary Key";
            // 
            // cbPrimaryKey
            // 
            this.cbPrimaryKey.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbPrimaryKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrimaryKey.Enabled = false;
            this.cbPrimaryKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPrimaryKey.FormattingEnabled = true;
            this.cbPrimaryKey.Location = new System.Drawing.Point(636, 49);
            this.cbPrimaryKey.Name = "cbPrimaryKey";
            this.cbPrimaryKey.Size = new System.Drawing.Size(198, 27);
            this.cbPrimaryKey.TabIndex = 9;
            this.cbPrimaryKey.SelectedIndexChanged += new System.EventHandler(this.cbPrimaryKey_SelectedIndexChanged);
            this.cbPrimaryKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbPrimaryKey_KeyDown);
            // 
            // txtNewTable
            // 
            this.txtNewTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNewTable.Enabled = false;
            this.txtNewTable.Location = new System.Drawing.Point(312, 49);
            this.txtNewTable.Name = "txtNewTable";
            this.txtNewTable.Size = new System.Drawing.Size(179, 26);
            this.txtNewTable.TabIndex = 7;
            this.txtNewTable.TextChanged += new System.EventHandler(this.txtNewTable_TextChanged);
            this.txtNewTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTable2_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(308, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 19);
            this.label6.TabIndex = 4;
            this.label6.Text = "New Table";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 19);
            this.label5.TabIndex = 3;
            this.label5.Text = "transfer to ==>";
            // 
            // btnTranfer
            // 
            this.btnTranfer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTranfer.Enabled = false;
            this.btnTranfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTranfer.Location = new System.Drawing.Point(919, 41);
            this.btnTranfer.Name = "btnTranfer";
            this.btnTranfer.Size = new System.Drawing.Size(128, 37);
            this.btnTranfer.TabIndex = 11;
            this.btnTranfer.Text = "Transfer";
            this.btnTranfer.UseVisualStyleBackColor = true;
            this.btnTranfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // txtCurrentTable
            // 
            this.txtCurrentTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCurrentTable.Enabled = false;
            this.txtCurrentTable.Location = new System.Drawing.Point(20, 47);
            this.txtCurrentTable.Name = "txtCurrentTable";
            this.txtCurrentTable.Size = new System.Drawing.Size(179, 26);
            this.txtCurrentTable.TabIndex = 6;
            this.txtCurrentTable.TextChanged += new System.EventHandler(this.txtCurrentTable_TextChanged);
            this.txtCurrentTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTable1_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Current Table";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnDesign);
            this.groupBox2.Controls.Add(this.btnExecute);
            this.groupBox2.Controls.Add(this.rbCommand);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbMethods);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1061, 232);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Command:";
            // 
            // btnDesign
            // 
            this.btnDesign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesign.Location = new System.Drawing.Point(266, 42);
            this.btnDesign.Name = "btnDesign";
            this.btnDesign.Size = new System.Drawing.Size(138, 37);
            this.btnDesign.TabIndex = 3;
            this.btnDesign.Text = "Design";
            this.btnDesign.UseVisualStyleBackColor = true;
            this.btnDesign.Visible = false;
            this.btnDesign.Click += new System.EventHandler(this.btnDesign_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.Enabled = false;
            this.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecute.Location = new System.Drawing.Point(879, 185);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(166, 37);
            this.btnExecute.TabIndex = 5;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // rbCommand
            // 
            this.rbCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbCommand.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rbCommand.Enabled = false;
            this.rbCommand.Location = new System.Drawing.Point(28, 103);
            this.rbCommand.Name = "rbCommand";
            this.rbCommand.Size = new System.Drawing.Size(845, 119);
            this.rbCommand.TabIndex = 4;
            this.rbCommand.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Command DataBase";
            // 
            // cbMethods
            // 
            this.cbMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMethods.Enabled = false;
            this.cbMethods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMethods.FormattingEnabled = true;
            this.cbMethods.Items.AddRange(new object[] {
            "ALTER COLUMN",
            "ALTER DATABASE",
            "ALTER TABLE",
            "CREATE DATABASE",
            "CREATE TABLE",
            "DELETE",
            "DROP DATABASE",
            "DROP TABLE",
            "INSERT",
            "OTHER",
            "SELECT",
            "UPDATE"});
            this.cbMethods.Location = new System.Drawing.Point(28, 48);
            this.cbMethods.Name = "cbMethods";
            this.cbMethods.Size = new System.Drawing.Size(232, 27);
            this.cbMethods.Sorted = true;
            this.cbMethods.TabIndex = 2;
            this.cbMethods.SelectedIndexChanged += new System.EventHandler(this.cbMethods_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Methods";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbDataSource);
            this.groupBox1.Controls.Add(this.btnFreeAcess);
            this.groupBox1.Location = new System.Drawing.Point(11, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1062, 108);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "Data Source";
            // 
            // cbDataSource
            // 
            this.cbDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDataSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDataSource.FormattingEnabled = true;
            this.cbDataSource.Location = new System.Drawing.Point(20, 52);
            this.cbDataSource.Name = "cbDataSource";
            this.cbDataSource.Size = new System.Drawing.Size(902, 27);
            this.cbDataSource.Sorted = true;
            this.cbDataSource.TabIndex = 0;
            this.cbDataSource.SelectedIndexChanged += new System.EventHandler(this.cbDataSource_SelectedIndexChanged);
            this.cbDataSource.TextChanged += new System.EventHandler(this.cbDataSource_TextChanged);
            this.cbDataSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbDataSource_KeyDown);
            // 
            // btnFreeAcess
            // 
            this.btnFreeAcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFreeAcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFreeAcess.Location = new System.Drawing.Point(928, 46);
            this.btnFreeAcess.Name = "btnFreeAcess";
            this.btnFreeAcess.Size = new System.Drawing.Size(122, 37);
            this.btnFreeAcess.TabIndex = 1;
            this.btnFreeAcess.Text = "Free acess";
            this.btnFreeAcess.UseVisualStyleBackColor = true;
            this.btnFreeAcess.Click += new System.EventHandler(this.btnFreeAcess_Click);
            // 
            // error
            // 
            this.error.ContainerControl = this;
            // 
            // FrmManipulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1086, 453);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1102, 492);
            this.Name = "FrmManipulation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manipulation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmManipulation_KeyDown);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtNewTable;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTranfer;
        private System.Windows.Forms.TextBox txtCurrentTable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.RichTextBox rbCommand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMethods;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ErrorProvider error;
        private System.Windows.Forms.Button btnFreeAcess;
        private System.Windows.Forms.ComboBox cbDataSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbxIdentity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbPrimaryKey;
        private System.Windows.Forms.CheckBox cbHasPrimaryKey;
        private System.Windows.Forms.Button btnDesign;
    }
}

