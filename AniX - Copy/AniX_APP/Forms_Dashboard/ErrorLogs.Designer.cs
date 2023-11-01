using AniX_APP.CustomElements;

namespace AniX_APP.Forms_Dashboard
{
    partial class ErrorLogs
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
            panelTop = new Panel();
            btnUser = new Button();
            panelBackground = new RoundPanel();
            panelFix = new Panel();
            cmbFilter = new CustomComboBox();
            label1 = new Label();
            roundPanelListBox1 = new RoundPanelListBox();
            dgvErrors = new DataGridView();
            panelTop.SuspendLayout();
            panelBackground.SuspendLayout();
            panelFix.SuspendLayout();
            roundPanelListBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvErrors).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(11, 7, 17);
            panelTop.Controls.Add(btnUser);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1041, 53);
            panelTop.TabIndex = 7;
            // 
            // btnUser
            // 
            btnUser.FlatAppearance.BorderSize = 0;
            btnUser.FlatAppearance.MouseDownBackColor = Color.FromArgb(23, 21, 32);
            btnUser.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 22, 34);
            btnUser.FlatStyle = FlatStyle.Flat;
            btnUser.ForeColor = Color.Silver;
            btnUser.Image = Properties.Resources.attendance;
            btnUser.ImageAlign = ContentAlignment.MiddleLeft;
            btnUser.Location = new Point(933, 0);
            btnUser.Name = "btnUser";
            btnUser.Padding = new Padding(5, 0, 0, 0);
            btnUser.Size = new Size(108, 53);
            btnUser.TabIndex = 14;
            btnUser.Text = "  Admin";
            btnUser.TextAlign = ContentAlignment.MiddleLeft;
            btnUser.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUser.UseVisualStyleBackColor = true;
            // 
            // panelBackground
            // 
            panelBackground.BackgroundImage = Properties.Resources.background;
            panelBackground.BorderColor = Color.White;
            panelBackground.BorderWidth = 5;
            panelBackground.Controls.Add(panelFix);
            panelBackground.FillColor = Color.FromArgb(11, 7, 17);
            panelBackground.IsBorder = false;
            panelBackground.IsFill = true;
            panelBackground.Location = new Point(46, 93);
            panelBackground.Name = "panelBackground";
            panelBackground.Radius = 20;
            panelBackground.Size = new Size(946, 442);
            panelBackground.TabIndex = 8;
            // 
            // panelFix
            // 
            panelFix.BackColor = Color.FromArgb(11, 7, 17);
            panelFix.Controls.Add(cmbFilter);
            panelFix.Controls.Add(label1);
            panelFix.Controls.Add(roundPanelListBox1);
            panelFix.Location = new Point(13, 14);
            panelFix.Name = "panelFix";
            panelFix.Size = new Size(921, 412);
            panelFix.TabIndex = 0;
            // 
            // cmbFilter
            // 
            cmbFilter.BackColor = Color.FromArgb(11, 7, 17);
            cmbFilter.BorderColor = Color.FromArgb(231, 34, 83);
            cmbFilter.BorderSize = 2;
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.Font = new Font("Cascadia Code", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cmbFilter.ForeColor = Color.FromArgb(231, 34, 83);
            cmbFilter.IconColor = Color.FromArgb(231, 34, 83);
            cmbFilter.ListBackColor = Color.FromArgb(11, 7, 17);
            cmbFilter.ListTextColor = Color.FromArgb(231, 34, 83);
            cmbFilter.Location = new Point(38, 349);
            cmbFilter.MinimumSize = new Size(200, 30);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Padding = new Padding(2);
            cmbFilter.Size = new Size(247, 33);
            cmbFilter.TabIndex = 10;
            cmbFilter.Texts = "< filter >";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(231, 34, 83);
            label1.Location = new Point(33, 320);
            label1.Name = "label1";
            label1.Size = new Size(100, 21);
            label1.TabIndex = 9;
            label1.Text = "Filter by:";
            // 
            // roundPanelListBox1
            // 
            roundPanelListBox1.Controls.Add(dgvErrors);
            roundPanelListBox1.Location = new Point(38, 38);
            roundPanelListBox1.Name = "roundPanelListBox1";
            roundPanelListBox1.Size = new Size(843, 272);
            roundPanelListBox1.TabIndex = 3;
            // 
            // dgvErrors
            // 
            dgvErrors.AllowUserToAddRows = false;
            dgvErrors.AllowUserToDeleteRows = false;
            dgvErrors.BackgroundColor = Color.FromArgb(231, 34, 83);
            dgvErrors.BorderStyle = BorderStyle.None;
            dgvErrors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvErrors.GridColor = Color.FromArgb(11, 7, 17);
            dgvErrors.Location = new Point(1, 0);
            dgvErrors.Name = "dgvErrors";
            dgvErrors.ReadOnly = true;
            dgvErrors.RowHeadersWidth = 62;
            dgvErrors.RowTemplate.Height = 28;
            dgvErrors.Size = new Size(843, 272);
            dgvErrors.TabIndex = 2;
            // 
            // ErrorLogs
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(231, 34, 83);
            ClientSize = new Size(1041, 573);
            Controls.Add(panelTop);
            Controls.Add(panelBackground);
            Font = new Font("Cascadia Code", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ErrorLogs";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            panelTop.ResumeLayout(false);
            panelBackground.ResumeLayout(false);
            panelFix.ResumeLayout(false);
            panelFix.PerformLayout();
            roundPanelListBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvErrors).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Button btnUser;
        private RoundPanel panelBackground;
        private Panel panelFix;
        private CustomComboBox cmbFilter;
        private Label label1;
        private RoundPanelListBox roundPanelListBox1;
        private DataGridView dgvErrors;
    }
}