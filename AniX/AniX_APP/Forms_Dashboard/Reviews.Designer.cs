using AniX_APP.CustomElements;

namespace AniX_APP.Forms_Dashboard
{
    partial class Reviews
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
            btnApprove = new RoundButton();
            btnDetails = new RoundButton();
            cmbFilter = new CustomComboBox();
            label1 = new Label();
            btnRemove = new RoundButton();
            roundPanelListBox1 = new RoundPanelListBox();
            dgvReviews = new DataGridView();
            panelTop.SuspendLayout();
            panelBackground.SuspendLayout();
            panelFix.SuspendLayout();
            roundPanelListBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReviews).BeginInit();
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
            panelFix.Controls.Add(btnApprove);
            panelFix.Controls.Add(btnDetails);
            panelFix.Controls.Add(cmbFilter);
            panelFix.Controls.Add(label1);
            panelFix.Controls.Add(btnRemove);
            panelFix.Controls.Add(roundPanelListBox1);
            panelFix.Location = new Point(13, 14);
            panelFix.Name = "panelFix";
            panelFix.Size = new Size(921, 412);
            panelFix.TabIndex = 0;
            // 
            // btnApprove
            // 
            btnApprove.BackColor = Color.FromArgb(231, 34, 83);
            btnApprove.BackgroundColor = Color.FromArgb(231, 34, 83);
            btnApprove.BorderColor = Color.FromArgb(231, 34, 83);
            btnApprove.BorderRadius = 14;
            btnApprove.BorderSize = 2;
            btnApprove.ClickedColor = Color.FromArgb(231, 34, 83);
            btnApprove.FlatAppearance.BorderSize = 0;
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnApprove.ForeColor = Color.FromArgb(11, 7, 17);
            btnApprove.Location = new Point(640, 156);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(121, 43);
            btnApprove.TabIndex = 12;
            btnApprove.Text = "Approve";
            btnApprove.TextColor = Color.FromArgb(11, 7, 17);
            btnApprove.UseVisualStyleBackColor = false;
            btnApprove.Click += btnApprove_Click;
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
            cmbFilter.ListBackColor = Color.FromArgb(11, 7, 17);
            cmbFilter.ListTextColor = Color.FromArgb(231, 34, 83);
            cmbFilter.Location = new Point(643, 106);
            cmbFilter.MinimumSize = new Size(200, 30);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Padding = new Padding(2);
            cmbFilter.Size = new Size(247, 33);
            cmbFilter.TabIndex = 10;
            cmbFilter.Texts = "";
            cmbFilter.OnSelectedIndexChanged += cmbFilter_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(231, 34, 83);
            label1.Location = new Point(638, 77);
            label1.Name = "label1";
            label1.Size = new Size(100, 21);
            label1.TabIndex = 9;
            label1.Text = "Filter by:";
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
            btnRemove.Location = new Point(769, 156);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(121, 43);
            btnRemove.TabIndex = 5;
            btnRemove.Text = "Remove";
            btnRemove.TextColor = Color.FromArgb(231, 34, 83);
            btnRemove.UseVisualStyleBackColor = false;
            btnRemove.Click += btnRemove_Click;
            // 
            // roundPanelListBox1
            // 
            roundPanelListBox1.Controls.Add(dgvReviews);
            roundPanelListBox1.Location = new Point(38, 50);
            roundPanelListBox1.Name = "roundPanelListBox1";
            roundPanelListBox1.Size = new Size(565, 272);
            roundPanelListBox1.TabIndex = 3;
            // 
            // dgvReviews
            // 
            dgvReviews.AllowUserToAddRows = false;
            dgvReviews.AllowUserToDeleteRows = false;
            dgvReviews.AllowUserToResizeColumns = false;
            dgvReviews.AllowUserToResizeRows = false;
            dgvReviews.BackgroundColor = Color.FromArgb(231, 34, 83);
            dgvReviews.BorderStyle = BorderStyle.None;
            dgvReviews.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReviews.GridColor = Color.FromArgb(11, 7, 17);
            dgvReviews.Location = new Point(0, 0);
            dgvReviews.Name = "dgvReviews";
            dgvReviews.ReadOnly = true;
            dgvReviews.RowHeadersWidth = 62;
            dgvReviews.RowTemplate.Height = 28;
            dgvReviews.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReviews.Size = new Size(565, 272);
            dgvReviews.TabIndex = 2;
            dgvReviews.CellDoubleClick += dgvReviews_CellDoubleClick;
            // 
            // Reviews
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(231, 34, 83);
            ClientSize = new Size(1041, 573);
            Controls.Add(panelTop);
            Controls.Add(panelBackground);
            Font = new Font("Cascadia Code", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Reviews";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Load += Reviews_Load;
            panelTop.ResumeLayout(false);
            panelBackground.ResumeLayout(false);
            panelFix.ResumeLayout(false);
            panelFix.PerformLayout();
            roundPanelListBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReviews).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Button btnUser;
        private RoundPanel panelBackground;
        private Panel panelFix;
        private RoundButton btnDetails;
        private CustomComboBox cmbFilter;
        private Label label1;
        private RoundButton btnRemove;
        private RoundPanelListBox roundPanelListBox1;
        private DataGridView dgvReviews;
        private RoundButton btnApprove;
    }
}