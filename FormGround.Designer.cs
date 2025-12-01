namespace TSGrounds
{
    partial class FormGround
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGround));
            menuStripGROUND = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            menuItem_LOAD = new ToolStripMenuItem();
            menuItem_EXPORT = new ToolStripMenuItem();
            menuItem_EXPORT_IMG = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            btnSEARCH = new Button();
            txtbSEARCH = new TextBox();
            panel2 = new Panel();
            dvgGROUND = new DataGridView();
            panel3 = new Panel();
            groupBoxBRUSH = new GroupBox();
            rdBRUSH_WATER = new RadioButton();
            rdBRUSH_OVER = new RadioButton();
            rdBRUSH_NONE = new RadioButton();
            groupBoxLAYER = new GroupBox();
            rbLAYER_OBJECT = new RadioButton();
            rbLAYER_COLLISION = new RadioButton();
            groupBox1 = new GroupBox();
            cbTEXT_GROUND = new CheckBox();
            cbLAYER = new CheckBox();
            cbGRID = new CheckBox();
            panel4 = new Panel();
            lbGROUND_NUM = new Label();
            panel5 = new Panel();
            panel6 = new Panel();
            picBox_GROUND = new PictureBox();
            panel7 = new Panel();
            checkBoxVersion = new CheckBox();
            panel8 = new Panel();
            linkLabelDEV = new LinkLabel();
            menuStripGROUND.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dvgGROUND).BeginInit();
            panel3.SuspendLayout();
            groupBoxBRUSH.SuspendLayout();
            groupBoxLAYER.SuspendLayout();
            groupBox1.SuspendLayout();
            panel4.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBox_GROUND).BeginInit();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripGROUND
            // 
            menuStripGROUND.GripStyle = ToolStripGripStyle.Visible;
            menuStripGROUND.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStripGROUND.Location = new Point(0, 0);
            menuStripGROUND.Name = "menuStripGROUND";
            menuStripGROUND.Size = new Size(800, 24);
            menuStripGROUND.TabIndex = 0;
            menuStripGROUND.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuItem_LOAD, menuItem_EXPORT });
            fileToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // menuItem_LOAD
            // 
            menuItem_LOAD.Name = "menuItem_LOAD";
            menuItem_LOAD.Size = new Size(107, 22);
            menuItem_LOAD.Text = "&Load";
            menuItem_LOAD.Click += menuItem_LOAD_Click;
            // 
            // menuItem_EXPORT
            // 
            menuItem_EXPORT.DropDownItems.AddRange(new ToolStripItem[] { menuItem_EXPORT_IMG });
            menuItem_EXPORT.Name = "menuItem_EXPORT";
            menuItem_EXPORT.Size = new Size(107, 22);
            menuItem_EXPORT.Text = "&Export";
            // 
            // menuItem_EXPORT_IMG
            // 
            menuItem_EXPORT_IMG.Name = "menuItem_EXPORT_IMG";
            menuItem_EXPORT_IMG.Size = new Size(107, 22);
            menuItem_EXPORT_IMG.Text = "&Image";
            menuItem_EXPORT_IMG.Click += menuItem_EXPORT_IMG_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 117F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 1);
            tableLayoutPanel1.Controls.Add(panel3, 2, 1);
            tableLayoutPanel1.Controls.Add(panel4, 0, 2);
            tableLayoutPanel1.Controls.Add(panel5, 1, 2);
            tableLayoutPanel1.Controls.Add(panel6, 1, 1);
            tableLayoutPanel1.Controls.Add(panel7, 1, 0);
            tableLayoutPanel1.Controls.Add(panel8, 2, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 24);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tableLayoutPanel1.Size = new Size(800, 509);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSEARCH);
            panel1.Controls.Add(txtbSEARCH);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(144, 34);
            panel1.TabIndex = 0;
            // 
            // btnSEARCH
            // 
            btnSEARCH.BackgroundImage = Properties.Resources.icons_search;
            btnSEARCH.BackgroundImageLayout = ImageLayout.Zoom;
            btnSEARCH.Location = new Point(109, 5);
            btnSEARCH.Name = "btnSEARCH";
            btnSEARCH.Size = new Size(26, 23);
            btnSEARCH.TabIndex = 1;
            btnSEARCH.UseVisualStyleBackColor = true;
            btnSEARCH.Click += btnSEARCH_Click;
            // 
            // txtbSEARCH
            // 
            txtbSEARCH.Location = new Point(9, 5);
            txtbSEARCH.MaxLength = 5;
            txtbSEARCH.Name = "txtbSEARCH";
            txtbSEARCH.Size = new Size(94, 23);
            txtbSEARCH.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(dvgGROUND);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(4, 45);
            panel2.Name = "panel2";
            panel2.Size = new Size(144, 422);
            panel2.TabIndex = 1;
            // 
            // dvgGROUND
            // 
            dvgGROUND.AllowUserToAddRows = false;
            dvgGROUND.AllowUserToDeleteRows = false;
            dvgGROUND.AllowUserToResizeColumns = false;
            dvgGROUND.AllowUserToResizeRows = false;
            dvgGROUND.BackgroundColor = Color.WhiteSmoke;
            dvgGROUND.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dvgGROUND.Dock = DockStyle.Fill;
            dvgGROUND.Location = new Point(0, 0);
            dvgGROUND.MultiSelect = false;
            dvgGROUND.Name = "dvgGROUND";
            dvgGROUND.RowHeadersWidth = 25;
            dvgGROUND.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dvgGROUND.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgGROUND.Size = new Size(144, 422);
            dvgGROUND.TabIndex = 0;
            dvgGROUND.SelectionChanged += dvgGROUND_SelectionChanged;
            // 
            // panel3
            // 
            panel3.Controls.Add(groupBoxBRUSH);
            panel3.Controls.Add(groupBoxLAYER);
            panel3.Controls.Add(groupBox1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(685, 45);
            panel3.Name = "panel3";
            panel3.Size = new Size(111, 422);
            panel3.TabIndex = 3;
            // 
            // groupBoxBRUSH
            // 
            groupBoxBRUSH.Controls.Add(rdBRUSH_WATER);
            groupBoxBRUSH.Controls.Add(rdBRUSH_OVER);
            groupBoxBRUSH.Controls.Add(rdBRUSH_NONE);
            groupBoxBRUSH.Dock = DockStyle.Top;
            groupBoxBRUSH.Location = new Point(0, 175);
            groupBoxBRUSH.Name = "groupBoxBRUSH";
            groupBoxBRUSH.Size = new Size(111, 103);
            groupBoxBRUSH.TabIndex = 4;
            groupBoxBRUSH.TabStop = false;
            groupBoxBRUSH.Text = "BRUSH";
            // 
            // rdBRUSH_WATER
            // 
            rdBRUSH_WATER.AutoSize = true;
            rdBRUSH_WATER.ForeColor = Color.Blue;
            rdBRUSH_WATER.Location = new Point(6, 72);
            rdBRUSH_WATER.Name = "rdBRUSH_WATER";
            rdBRUSH_WATER.Size = new Size(63, 19);
            rdBRUSH_WATER.TabIndex = 2;
            rdBRUSH_WATER.Text = "WATER";
            rdBRUSH_WATER.UseVisualStyleBackColor = true;
            rdBRUSH_WATER.CheckedChanged += rdBRUSH_WATER_CheckedChanged;
            // 
            // rdBRUSH_OVER
            // 
            rdBRUSH_OVER.AutoSize = true;
            rdBRUSH_OVER.ForeColor = Color.Green;
            rdBRUSH_OVER.Location = new Point(6, 47);
            rdBRUSH_OVER.Name = "rdBRUSH_OVER";
            rdBRUSH_OVER.Size = new Size(54, 19);
            rdBRUSH_OVER.TabIndex = 1;
            rdBRUSH_OVER.Text = "OVER";
            rdBRUSH_OVER.UseVisualStyleBackColor = true;
            rdBRUSH_OVER.CheckedChanged += rdBRUSH_OVER_CheckedChanged;
            // 
            // rdBRUSH_NONE
            // 
            rdBRUSH_NONE.AutoSize = true;
            rdBRUSH_NONE.Checked = true;
            rdBRUSH_NONE.ForeColor = Color.Red;
            rdBRUSH_NONE.Location = new Point(6, 22);
            rdBRUSH_NONE.Name = "rdBRUSH_NONE";
            rdBRUSH_NONE.Size = new Size(58, 19);
            rdBRUSH_NONE.TabIndex = 0;
            rdBRUSH_NONE.TabStop = true;
            rdBRUSH_NONE.Text = "NONE";
            rdBRUSH_NONE.UseVisualStyleBackColor = true;
            rdBRUSH_NONE.CheckedChanged += rdBRUSH_NONE_CheckedChanged;
            // 
            // groupBoxLAYER
            // 
            groupBoxLAYER.Controls.Add(rbLAYER_OBJECT);
            groupBoxLAYER.Controls.Add(rbLAYER_COLLISION);
            groupBoxLAYER.Dock = DockStyle.Top;
            groupBoxLAYER.Location = new Point(0, 100);
            groupBoxLAYER.Name = "groupBoxLAYER";
            groupBoxLAYER.Size = new Size(111, 75);
            groupBoxLAYER.TabIndex = 3;
            groupBoxLAYER.TabStop = false;
            groupBoxLAYER.Text = "LAYER";
            // 
            // rbLAYER_OBJECT
            // 
            rbLAYER_OBJECT.AutoSize = true;
            rbLAYER_OBJECT.Enabled = false;
            rbLAYER_OBJECT.Location = new Point(6, 47);
            rbLAYER_OBJECT.Name = "rbLAYER_OBJECT";
            rbLAYER_OBJECT.Size = new Size(66, 19);
            rbLAYER_OBJECT.TabIndex = 1;
            rbLAYER_OBJECT.Text = "OBJECT";
            rbLAYER_OBJECT.UseVisualStyleBackColor = true;
            // 
            // rbLAYER_COLLISION
            // 
            rbLAYER_COLLISION.AutoSize = true;
            rbLAYER_COLLISION.Checked = true;
            rbLAYER_COLLISION.Location = new Point(6, 22);
            rbLAYER_COLLISION.Name = "rbLAYER_COLLISION";
            rbLAYER_COLLISION.Size = new Size(84, 19);
            rbLAYER_COLLISION.TabIndex = 0;
            rbLAYER_COLLISION.TabStop = true;
            rbLAYER_COLLISION.Text = "COLLISION";
            rbLAYER_COLLISION.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cbTEXT_GROUND);
            groupBox1.Controls.Add(cbLAYER);
            groupBox1.Controls.Add(cbGRID);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(111, 100);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "SHOW";
            // 
            // cbTEXT_GROUND
            // 
            cbTEXT_GROUND.AutoSize = true;
            cbTEXT_GROUND.Location = new Point(6, 72);
            cbTEXT_GROUND.Name = "cbTEXT_GROUND";
            cbTEXT_GROUND.Size = new Size(53, 19);
            cbTEXT_GROUND.TabIndex = 2;
            cbTEXT_GROUND.Text = "TEXT";
            cbTEXT_GROUND.UseVisualStyleBackColor = true;
            cbTEXT_GROUND.CheckedChanged += cbTEXT_GROUND_CheckedChanged;
            // 
            // cbLAYER
            // 
            cbLAYER.AutoSize = true;
            cbLAYER.Location = new Point(6, 47);
            cbLAYER.Name = "cbLAYER";
            cbLAYER.Size = new Size(59, 19);
            cbLAYER.TabIndex = 1;
            cbLAYER.Text = "LAYER";
            cbLAYER.UseVisualStyleBackColor = true;
            cbLAYER.CheckedChanged += cbLAYER_CheckedChanged;
            // 
            // cbGRID
            // 
            cbGRID.AutoSize = true;
            cbGRID.Location = new Point(6, 22);
            cbGRID.Name = "cbGRID";
            cbGRID.Size = new Size(52, 19);
            cbGRID.TabIndex = 0;
            cbGRID.Text = "GRID";
            cbGRID.UseVisualStyleBackColor = true;
            cbGRID.CheckedChanged += cbGRID_CheckedChanged;
            // 
            // panel4
            // 
            panel4.Controls.Add(lbGROUND_NUM);
            panel4.Location = new Point(4, 474);
            panel4.Name = "panel4";
            panel4.Size = new Size(144, 31);
            panel4.TabIndex = 4;
            // 
            // lbGROUND_NUM
            // 
            lbGROUND_NUM.Location = new Point(8, 9);
            lbGROUND_NUM.Name = "lbGROUND_NUM";
            lbGROUND_NUM.Size = new Size(124, 15);
            lbGROUND_NUM.TabIndex = 0;
            lbGROUND_NUM.Text = "GROUNDS: 0";
            lbGROUND_NUM.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(155, 474);
            panel5.Name = "panel5";
            panel5.Size = new Size(523, 31);
            panel5.TabIndex = 5;
            // 
            // panel6
            // 
            panel6.Controls.Add(picBox_GROUND);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(155, 45);
            panel6.Name = "panel6";
            panel6.Size = new Size(523, 422);
            panel6.TabIndex = 6;
            // 
            // picBox_GROUND
            // 
            picBox_GROUND.BackColor = SystemColors.ActiveCaptionText;
            picBox_GROUND.Dock = DockStyle.Fill;
            picBox_GROUND.Location = new Point(0, 0);
            picBox_GROUND.Name = "picBox_GROUND";
            picBox_GROUND.Size = new Size(523, 422);
            picBox_GROUND.SizeMode = PictureBoxSizeMode.Zoom;
            picBox_GROUND.TabIndex = 0;
            picBox_GROUND.TabStop = false;
            picBox_GROUND.Paint += picBox_GROUND_Paint;
            picBox_GROUND.MouseDown += picBox_GROUND_MouseDown;
            picBox_GROUND.MouseMove += picBox_GROUND_MouseMove;
            picBox_GROUND.MouseUp += picBox_GROUND_MouseUp;
            // 
            // panel7
            // 
            panel7.Controls.Add(checkBoxVersion);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(155, 4);
            panel7.Name = "panel7";
            panel7.Size = new Size(523, 34);
            panel7.TabIndex = 7;
            // 
            // checkBoxVersion
            // 
            checkBoxVersion.AutoSize = true;
            checkBoxVersion.Location = new Point(12, 8);
            checkBoxVersion.Name = "checkBoxVersion";
            checkBoxVersion.Size = new Size(45, 19);
            checkBoxVersion.TabIndex = 0;
            checkBoxVersion.Text = "TS1";
            checkBoxVersion.UseVisualStyleBackColor = true;
            checkBoxVersion.CheckedChanged += checkBoxVersion_CheckedChanged;
            // 
            // panel8
            // 
            panel8.Controls.Add(linkLabelDEV);
            panel8.Location = new Point(685, 474);
            panel8.Name = "panel8";
            panel8.Size = new Size(111, 31);
            panel8.TabIndex = 8;
            // 
            // linkLabelDEV
            // 
            linkLabelDEV.AutoSize = true;
            linkLabelDEV.Location = new Point(24, 8);
            linkLabelDEV.Name = "linkLabelDEV";
            linkLabelDEV.Size = new Size(57, 15);
            linkLabelDEV.TabIndex = 5;
            linkLabelDEV.TabStop = true;
            linkLabelDEV.Text = "DEVX TS2";
            linkLabelDEV.LinkClicked += linkLabelDEV_LinkClicked;
            // 
            // FormGround
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 533);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStripGROUND);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripGROUND;
            Name = "FormGround";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TSGrounds - Look";
            Load += FormGround_Load;
            menuStripGROUND.ResumeLayout(false);
            menuStripGROUND.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dvgGROUND).EndInit();
            panel3.ResumeLayout(false);
            groupBoxBRUSH.ResumeLayout(false);
            groupBoxBRUSH.PerformLayout();
            groupBoxLAYER.ResumeLayout(false);
            groupBoxLAYER.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel4.ResumeLayout(false);
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBox_GROUND).EndInit();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStripGROUND;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem menuItem_LOAD;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button btnSEARCH;
        private TextBox txtbSEARCH;
        private Panel panel2;
        private DataGridView dvgGROUND;
        private Panel panel3;
        private GroupBox groupBoxBRUSH;
        private GroupBox groupBoxLAYER;
        private GroupBox groupBox1;
        private Panel panel4;
        private Panel panel5;
        private RadioButton rdBRUSH_WATER;
        private RadioButton rdBRUSH_OVER;
        private RadioButton rdBRUSH_NONE;
        private RadioButton rbLAYER_OBJECT;
        private RadioButton rbLAYER_COLLISION;
        private CheckBox cbLAYER;
        private CheckBox cbGRID;
        private Panel panel6;
        private PictureBox picBox_GROUND;
        private CheckBox cbTEXT_GROUND;
        private Label lbGROUND_NUM;
        private ToolStripMenuItem menuItem_EXPORT;
        private ToolStripMenuItem menuItem_EXPORT_IMG;
        private Panel panel7;
        private CheckBox checkBoxVersion;
        private Panel panel8;
        private LinkLabel linkLabelDEV;
    }
}
