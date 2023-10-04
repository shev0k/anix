using AniX_APP.CustomElements;

namespace AniX_APP.Forms_Dashboard
{
    partial class Admin
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
            btnDetails = new RoundButton();
            btnEdit = new RoundButton();
            btnRemove = new RoundButton();
            btnAdd = new RoundButton();
            tbxAdmin = new housing.CustomElements.RoundTextBox();
            lbAdmin = new Label();
            roundPanelListBox1 = new RoundPanelListBox();
            dgvAdmins = new DataGridView();
            panelTop.SuspendLayout();
            panelBackground.SuspendLayout();
            panelFix.SuspendLayout();
            roundPanelListBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAdmins).BeginInit();
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
            panelTop.TabIndex = 3;
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
            panelBackground.TabIndex = 4;
            // 
            // panelFix
            // 
            panelFix.BackColor = Color.FromArgb(11, 7, 17);
            panelFix.Controls.Add(btnDetails);
            panelFix.Controls.Add(btnEdit);
            panelFix.Controls.Add(btnRemove);
            panelFix.Controls.Add(btnAdd);
            panelFix.Controls.Add(tbxAdmin);
            panelFix.Controls.Add(lbAdmin);
            panelFix.Controls.Add(roundPanelListBox1);
            panelFix.Location = new Point(13, 14);
            panelFix.Name = "panelFix";
            panelFix.Size = new Size(921, 412);
            panelFix.TabIndex = 0;
            // 
            // btnDetails
            // 
            btnDetails.BackColor = Color.FromArgb(11, 7, 17);
            btnDetails.BackgroundColor = Color.FromArgb(11, 7, 17);
            btnDetails.BorderColor = Color.FromArgb(231, 34, 83);
            btnDetails.BorderRadius = 14;
            btnDetails.BorderSize = 2;
            btnDetails.ClickedColor = Color.FromArgb(231, 34, 83);
            btnDetails.FlatAppearance.BorderSize = 0;
            btnDetails.FlatStyle = FlatStyle.Flat;
            btnDetails.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnDetails.ForeColor = Color.FromArgb(231, 34, 83);
            btnDetails.Location = new Point(465, 332);
            btnDetails.Name = "btnDetails";
            btnDetails.Size = new Size(138, 43);
            btnDetails.TabIndex = 11;
            btnDetails.Text = "View Details";
            btnDetails.TextColor = Color.FromArgb(231, 34, 83);
            btnDetails.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(231, 34, 83);
            btnEdit.BackgroundColor = Color.FromArgb(231, 34, 83);
            btnEdit.BorderColor = Color.FromArgb(231, 34, 83);
            btnEdit.BorderRadius = 14;
            btnEdit.BorderSize = 0;
            btnEdit.ClickedColor = Color.FromArgb(231, 34, 83);
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Cascadia Code", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnEdit.ForeColor = Color.FromArgb(11, 7, 17);
            btnEdit.Location = new Point(642, 189);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(250, 43);
            btnEdit.TabIndex = 8;
            btnEdit.Text = "Edit";
            btnEdit.TextColor = Color.FromArgb(11, 7, 17);
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.FromArgb(11, 7, 17);
            btnRemove.BackgroundColor = Color.FromArgb(11, 7, 17);
            btnRemove.BorderColor = Color.FromArgb(231, 34, 83);
            btnRemove.BorderRadius = 14;
            btnRemove.BorderSize = 2;
            btnRemove.ClickedColor = Color.FromArgb(231, 34, 83);
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnRemove.ForeColor = Color.FromArgb(231, 34, 83);
            btnRemove.Location = new Point(771, 140);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(121, 43);
            btnRemove.TabIndex = 5;
            btnRemove.Text = "Remove";
            btnRemove.TextColor = Color.FromArgb(231, 34, 83);
            btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(231, 34, 83);
            btnAdd.BackgroundColor = Color.FromArgb(231, 34, 83);
            btnAdd.BorderColor = Color.FromArgb(231, 34, 83);
            btnAdd.BorderRadius = 14;
            btnAdd.BorderSize = 0;
            btnAdd.ClickedColor = Color.FromArgb(231, 34, 83);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Cascadia Code", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnAdd.ForeColor = Color.FromArgb(11, 7, 17);
            btnAdd.Location = new Point(642, 140);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(121, 43);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Add";
            btnAdd.TextColor = Color.FromArgb(11, 7, 17);
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // tbxAdmin
            // 
            tbxAdmin.BackColor = Color.FromArgb(231, 34, 83);
            tbxAdmin.BorderColor = Color.FromArgb(231, 34, 83);
            tbxAdmin.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxAdmin.BorderRadius = 14;
            tbxAdmin.BorderSize = 2;
            tbxAdmin.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxAdmin.ForeColor = Color.FromArgb(11, 7, 17);
            tbxAdmin.Location = new Point(642, 92);
            tbxAdmin.Margin = new Padding(4);
            tbxAdmin.Multiline = false;
            tbxAdmin.Name = "tbxAdmin";
            tbxAdmin.Padding = new Padding(10, 7, 10, 7);
            tbxAdmin.PasswordChar = false;
            tbxAdmin.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxAdmin.PlaceholderText = "< admin >";
            tbxAdmin.Size = new Size(250, 33);
            tbxAdmin.TabIndex = 2;
            tbxAdmin.Texts = "";
            tbxAdmin.UnderlinedStyle = false;
            // 
            // lbAdmin
            // 
            lbAdmin.AutoSize = true;
            lbAdmin.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbAdmin.ForeColor = Color.FromArgb(231, 34, 83);
            lbAdmin.Location = new Point(642, 65);
            lbAdmin.Name = "lbAdmin";
            lbAdmin.Size = new Size(127, 21);
            lbAdmin.TabIndex = 4;
            lbAdmin.Text = "Search Admin:";
            // 
            // roundPanelListBox1
            // 
            roundPanelListBox1.Controls.Add(dgvAdmins);
            roundPanelListBox1.Location = new Point(38, 50);
            roundPanelListBox1.Name = "roundPanelListBox1";
            roundPanelListBox1.Size = new Size(565, 272);
            roundPanelListBox1.TabIndex = 3;
            // 
            // dgvAdmins
            // 
            dgvAdmins.AllowUserToAddRows = false;
            dgvAdmins.AllowUserToDeleteRows = false;
            dgvAdmins.BackgroundColor = Color.FromArgb(231, 34, 83);
            dgvAdmins.BorderStyle = BorderStyle.None;
            dgvAdmins.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAdmins.GridColor = Color.FromArgb(11, 7, 17);
            dgvAdmins.Location = new Point(0, 0);
            dgvAdmins.Name = "dgvAdmins";
            dgvAdmins.ReadOnly = true;
            dgvAdmins.RowHeadersWidth = 62;
            dgvAdmins.RowTemplate.Height = 28;
            dgvAdmins.Size = new Size(565, 272);
            dgvAdmins.TabIndex = 2;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(231, 34, 83);
            ClientSize = new Size(1041, 573);
            Controls.Add(panelTop);
            Controls.Add(panelBackground);
            Font = new Font("Cascadia Code", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Admin";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            panelTop.ResumeLayout(false);
            panelBackground.ResumeLayout(false);
            panelFix.ResumeLayout(false);
            panelFix.PerformLayout();
            roundPanelListBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAdmins).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Button btnUser;
        private RoundPanel panelBackground;
        private Panel panelFix;
        private RoundButton btnDetails;
        private RoundButton btnEdit;
        private RoundButton btnRemove;
        private RoundButton btnAdd;
        private housing.CustomElements.RoundTextBox tbxAdmin;
        private Label lbAdmin;
        private RoundPanelListBox roundPanelListBox1;
        private DataGridView dgvAdmins;
    }
}