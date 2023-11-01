using AniX_APP.CustomElements;

namespace AniX_APP.Forms_Dashboard
{
    partial class Users
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
            cmbFilter = new CustomComboBox();
            label1 = new Label();
            btnEdit = new RoundButton();
            btnRemove = new RoundButton();
            btnAdd = new RoundButton();
            tbxUser = new housing.CustomElements.RoundTextBox();
            lbUser = new Label();
            roundPanelListBox1 = new RoundPanelListBox();
            dgvUsers = new DataGridView();
            panelTop.SuspendLayout();
            panelBackground.SuspendLayout();
            panelFix.SuspendLayout();
            roundPanelListBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
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
            panelTop.TabIndex = 1;
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
            panelBackground.TabIndex = 2;
            // 
            // panelFix
            // 
            panelFix.BackColor = Color.FromArgb(11, 7, 17);
            panelFix.Controls.Add(btnDetails);
            panelFix.Controls.Add(cmbFilter);
            panelFix.Controls.Add(label1);
            panelFix.Controls.Add(btnEdit);
            panelFix.Controls.Add(btnRemove);
            panelFix.Controls.Add(btnAdd);
            panelFix.Controls.Add(tbxUser);
            panelFix.Controls.Add(lbUser);
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
            btnDetails.Click += btnDetails_Click;
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
            cmbFilter.Items.AddRange(new object[] { "Admin", "User", "Banned", "Not Banned", "All Users" });
            cmbFilter.ListBackColor = Color.FromArgb(11, 7, 17);
            cmbFilter.ListTextColor = Color.FromArgb(231, 34, 83);
            cmbFilter.Location = new Point(647, 269);
            cmbFilter.MinimumSize = new Size(200, 30);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Padding = new Padding(2);
            cmbFilter.Size = new Size(247, 33);
            cmbFilter.TabIndex = 10;
            cmbFilter.Texts = "All Users";
            cmbFilter.OnSelectedIndexChanged += cmbFilter_OnSelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(231, 34, 83);
            label1.Location = new Point(642, 240);
            label1.Name = "label1";
            label1.Size = new Size(100, 21);
            label1.TabIndex = 9;
            label1.Text = "Filter by:";
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
            btnEdit.Click += btnEdit_Click;
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
            btnRemove.Click += btnRemove_Click;
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
            btnAdd.Click += btnAdd_Click;
            // 
            // tbxUser
            // 
            tbxUser.BackColor = Color.FromArgb(231, 34, 83);
            tbxUser.BorderColor = Color.FromArgb(231, 34, 83);
            tbxUser.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxUser.BorderRadius = 14;
            tbxUser.BorderSize = 2;
            tbxUser.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxUser.ForeColor = Color.FromArgb(11, 7, 17);
            tbxUser.Location = new Point(642, 92);
            tbxUser.Margin = new Padding(4);
            tbxUser.Multiline = false;
            tbxUser.Name = "tbxUser";
            tbxUser.Padding = new Padding(10, 7, 10, 7);
            tbxUser.PasswordChar = false;
            tbxUser.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxUser.PlaceholderText = "< user >";
            tbxUser.Size = new Size(250, 33);
            tbxUser.TabIndex = 2;
            tbxUser.Texts = "";
            tbxUser.UnderlinedStyle = false;
            tbxUser._TextChanged += tbxUser__TextChanged;
            // 
            // lbUser
            // 
            lbUser.AutoSize = true;
            lbUser.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbUser.ForeColor = Color.FromArgb(231, 34, 83);
            lbUser.Location = new Point(642, 65);
            lbUser.Name = "lbUser";
            lbUser.Size = new Size(118, 21);
            lbUser.TabIndex = 4;
            lbUser.Text = "Search User:";
            // 
            // roundPanelListBox1
            // 
            roundPanelListBox1.Controls.Add(dgvUsers);
            roundPanelListBox1.Location = new Point(38, 50);
            roundPanelListBox1.Name = "roundPanelListBox1";
            roundPanelListBox1.Size = new Size(565, 272);
            roundPanelListBox1.TabIndex = 3;
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AllowUserToResizeColumns = false;
            dgvUsers.AllowUserToResizeRows = false;
            dgvUsers.BackgroundColor = Color.FromArgb(231, 34, 83);
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.GridColor = Color.FromArgb(11, 7, 17);
            dgvUsers.Location = new Point(0, 0);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersWidth = 62;
            dgvUsers.RowTemplate.Height = 28;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(565, 272);
            dgvUsers.TabIndex = 2;
            dgvUsers.CellDoubleClick += dgvUsers_CellDoubleClick;
            // 
            // Users
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(231, 34, 83);
            ClientSize = new Size(1041, 573);
            Controls.Add(panelBackground);
            Controls.Add(panelTop);
            Font = new Font("Cascadia Code", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Users";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Load += Users_Load;
            panelTop.ResumeLayout(false);
            panelBackground.ResumeLayout(false);
            panelFix.ResumeLayout(false);
            panelFix.PerformLayout();
            roundPanelListBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Button btnUser;
        private RoundPanel panelBackground;
        private Panel panelFix;
        private DataGridView dgvUsers;
        private RoundPanelListBox roundPanelListBox1;
        private housing.CustomElements.RoundTextBox tbxUser;
        private Label lbUser;
        private RoundButton btnEdit;
        private RoundButton btnRemove;
        private RoundButton btnAdd;
        private CustomComboBox cmbFilter;
        private Label label1;
        private RoundButton btnDetails;
    }
}