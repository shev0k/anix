using Microsoft.VisualBasic.Logging;
using System.Windows.Forms;

namespace AniX_APP.Forms_Dashboard
{
    partial class Dashboard
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
            panelSideMenu = new Panel();
            btnSettings = new Button();
            btnErrorLogs = new Button();
            btnAuditLogs = new Button();
            btnLogOut = new Button();
            panelInformation = new Panel();
            btnReviews = new Button();
            btnAnime = new Button();
            btnUsers = new Button();
            btnManagement = new Button();
            panelLogo = new Panel();
            logo = new PictureBox();
            panelChildForm = new Panel();
            panelBackground = new CustomElements.RoundPanel();
            pictureBox1 = new PictureBox();
            panelSideMenu.SuspendLayout();
            panelInformation.SuspendLayout();
            panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logo).BeginInit();
            panelChildForm.SuspendLayout();
            panelBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelSideMenu
            // 
            panelSideMenu.BackColor = Color.FromArgb(11, 7, 17);
            panelSideMenu.Controls.Add(btnSettings);
            panelSideMenu.Controls.Add(btnErrorLogs);
            panelSideMenu.Controls.Add(btnAuditLogs);
            panelSideMenu.Controls.Add(btnLogOut);
            panelSideMenu.Controls.Add(panelInformation);
            panelSideMenu.Controls.Add(btnManagement);
            panelSideMenu.Controls.Add(panelLogo);
            panelSideMenu.Dock = DockStyle.Left;
            panelSideMenu.Location = new Point(0, 0);
            panelSideMenu.Margin = new Padding(4);
            panelSideMenu.Name = "panelSideMenu";
            panelSideMenu.Size = new Size(225, 573);
            panelSideMenu.TabIndex = 1;
            // 
            // btnSettings
            // 
            btnSettings.Dock = DockStyle.Top;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatAppearance.MouseDownBackColor = Color.FromArgb(23, 21, 32);
            btnSettings.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 22, 34);
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.ForeColor = Color.Silver;
            btnSettings.Image = Properties.Resources.settings;
            btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettings.Location = new Point(0, 363);
            btnSettings.Name = "btnSettings";
            btnSettings.Padding = new Padding(5, 0, 0, 0);
            btnSettings.Size = new Size(225, 45);
            btnSettings.TabIndex = 18;
            btnSettings.Text = "  Settings";
            btnSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnErrorLogs
            // 
            btnErrorLogs.Dock = DockStyle.Top;
            btnErrorLogs.FlatAppearance.BorderSize = 0;
            btnErrorLogs.FlatAppearance.MouseDownBackColor = Color.FromArgb(23, 21, 32);
            btnErrorLogs.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 22, 34);
            btnErrorLogs.FlatStyle = FlatStyle.Flat;
            btnErrorLogs.ForeColor = Color.Silver;
            btnErrorLogs.Image = Properties.Resources.error;
            btnErrorLogs.ImageAlign = ContentAlignment.MiddleLeft;
            btnErrorLogs.Location = new Point(0, 318);
            btnErrorLogs.Name = "btnErrorLogs";
            btnErrorLogs.Padding = new Padding(5, 0, 0, 0);
            btnErrorLogs.Size = new Size(225, 45);
            btnErrorLogs.TabIndex = 17;
            btnErrorLogs.Text = "  Error Logs";
            btnErrorLogs.TextAlign = ContentAlignment.MiddleLeft;
            btnErrorLogs.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnErrorLogs.UseVisualStyleBackColor = true;
            btnErrorLogs.Click += btnErrorLogs_Click;
            // 
            // btnAuditLogs
            // 
            btnAuditLogs.Dock = DockStyle.Top;
            btnAuditLogs.FlatAppearance.BorderSize = 0;
            btnAuditLogs.FlatAppearance.MouseDownBackColor = Color.FromArgb(23, 21, 32);
            btnAuditLogs.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 22, 34);
            btnAuditLogs.FlatStyle = FlatStyle.Flat;
            btnAuditLogs.ForeColor = Color.Silver;
            btnAuditLogs.Image = Properties.Resources.audit;
            btnAuditLogs.ImageAlign = ContentAlignment.MiddleLeft;
            btnAuditLogs.Location = new Point(0, 273);
            btnAuditLogs.Name = "btnAuditLogs";
            btnAuditLogs.Padding = new Padding(5, 0, 0, 0);
            btnAuditLogs.Size = new Size(225, 45);
            btnAuditLogs.TabIndex = 16;
            btnAuditLogs.Text = "  Audit Logs";
            btnAuditLogs.TextAlign = ContentAlignment.MiddleLeft;
            btnAuditLogs.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAuditLogs.UseVisualStyleBackColor = true;
            btnAuditLogs.Click += btnAuditLogs_Click;
            // 
            // btnLogOut
            // 
            btnLogOut.Dock = DockStyle.Bottom;
            btnLogOut.FlatAppearance.BorderSize = 0;
            btnLogOut.FlatAppearance.MouseDownBackColor = Color.FromArgb(23, 21, 32);
            btnLogOut.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 22, 34);
            btnLogOut.FlatStyle = FlatStyle.Flat;
            btnLogOut.ForeColor = Color.Silver;
            btnLogOut.Image = Properties.Resources.logout;
            btnLogOut.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogOut.Location = new Point(0, 533);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Padding = new Padding(5, 0, 0, 0);
            btnLogOut.Size = new Size(225, 40);
            btnLogOut.TabIndex = 15;
            btnLogOut.Text = "  Log Out";
            btnLogOut.TextAlign = ContentAlignment.MiddleLeft;
            btnLogOut.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLogOut.UseVisualStyleBackColor = true;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // panelInformation
            // 
            panelInformation.BackColor = Color.FromArgb(35, 32, 39);
            panelInformation.Controls.Add(btnReviews);
            panelInformation.Controls.Add(btnAnime);
            panelInformation.Controls.Add(btnUsers);
            panelInformation.Dock = DockStyle.Top;
            panelInformation.Location = new Point(0, 137);
            panelInformation.Name = "panelInformation";
            panelInformation.Size = new Size(225, 136);
            panelInformation.TabIndex = 14;
            // 
            // btnReviews
            // 
            btnReviews.Dock = DockStyle.Top;
            btnReviews.FlatAppearance.BorderSize = 0;
            btnReviews.FlatAppearance.MouseDownBackColor = Color.FromArgb(42, 38, 46);
            btnReviews.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 38, 46);
            btnReviews.FlatStyle = FlatStyle.Flat;
            btnReviews.ForeColor = Color.Silver;
            btnReviews.Location = new Point(0, 90);
            btnReviews.Name = "btnReviews";
            btnReviews.Padding = new Padding(35, 0, 0, 0);
            btnReviews.Size = new Size(225, 45);
            btnReviews.TabIndex = 6;
            btnReviews.Text = "Reviews";
            btnReviews.TextAlign = ContentAlignment.MiddleLeft;
            btnReviews.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReviews.UseVisualStyleBackColor = true;
            btnReviews.Click += btnReviews_Click;
            // 
            // btnAnime
            // 
            btnAnime.Dock = DockStyle.Top;
            btnAnime.FlatAppearance.BorderSize = 0;
            btnAnime.FlatAppearance.MouseDownBackColor = Color.FromArgb(42, 38, 46);
            btnAnime.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 38, 46);
            btnAnime.FlatStyle = FlatStyle.Flat;
            btnAnime.ForeColor = Color.Silver;
            btnAnime.Location = new Point(0, 45);
            btnAnime.Name = "btnAnime";
            btnAnime.Padding = new Padding(35, 0, 0, 0);
            btnAnime.Size = new Size(225, 45);
            btnAnime.TabIndex = 5;
            btnAnime.Text = "Anime";
            btnAnime.TextAlign = ContentAlignment.MiddleLeft;
            btnAnime.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAnime.UseVisualStyleBackColor = true;
            btnAnime.Click += btnAnime_Click;
            // 
            // btnUsers
            // 
            btnUsers.Dock = DockStyle.Top;
            btnUsers.FlatAppearance.BorderSize = 0;
            btnUsers.FlatAppearance.MouseDownBackColor = Color.FromArgb(42, 38, 46);
            btnUsers.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 38, 46);
            btnUsers.FlatStyle = FlatStyle.Flat;
            btnUsers.ForeColor = Color.Silver;
            btnUsers.Location = new Point(0, 0);
            btnUsers.Name = "btnUsers";
            btnUsers.Padding = new Padding(35, 0, 0, 0);
            btnUsers.Size = new Size(225, 45);
            btnUsers.TabIndex = 4;
            btnUsers.Text = "Users";
            btnUsers.TextAlign = ContentAlignment.MiddleLeft;
            btnUsers.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUsers.UseVisualStyleBackColor = true;
            btnUsers.Click += btnUsers_Click;
            // 
            // btnManagement
            // 
            btnManagement.Dock = DockStyle.Top;
            btnManagement.FlatAppearance.BorderSize = 0;
            btnManagement.FlatAppearance.MouseDownBackColor = Color.FromArgb(23, 21, 32);
            btnManagement.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 22, 34);
            btnManagement.FlatStyle = FlatStyle.Flat;
            btnManagement.ForeColor = Color.Silver;
            btnManagement.Image = Properties.Resources.management;
            btnManagement.ImageAlign = ContentAlignment.MiddleLeft;
            btnManagement.Location = new Point(0, 92);
            btnManagement.Name = "btnManagement";
            btnManagement.Padding = new Padding(5, 0, 0, 0);
            btnManagement.Size = new Size(225, 45);
            btnManagement.TabIndex = 13;
            btnManagement.Text = "  Management";
            btnManagement.TextAlign = ContentAlignment.MiddleLeft;
            btnManagement.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnManagement.UseVisualStyleBackColor = true;
            btnManagement.Click += btnManagement_Click;
            // 
            // panelLogo
            // 
            panelLogo.Controls.Add(logo);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(225, 92);
            panelLogo.TabIndex = 0;
            // 
            // logo
            // 
            logo.Image = Properties.Resources.Anix___Logo;
            logo.Location = new Point(35, 28);
            logo.Name = "logo";
            logo.Size = new Size(151, 41);
            logo.SizeMode = PictureBoxSizeMode.Zoom;
            logo.TabIndex = 0;
            logo.TabStop = false;
            // 
            // panelChildForm
            // 
            panelChildForm.Controls.Add(panelBackground);
            panelChildForm.Dock = DockStyle.Fill;
            panelChildForm.Location = new Point(225, 0);
            panelChildForm.Margin = new Padding(4);
            panelChildForm.Name = "panelChildForm";
            panelChildForm.Size = new Size(1041, 573);
            panelChildForm.TabIndex = 2;
            // 
            // panelBackground
            // 
            panelBackground.BackColor = Color.FromArgb(32, 30, 45);
            panelBackground.BackgroundImage = Properties.Resources.background;
            panelBackground.BorderColor = Color.White;
            panelBackground.BorderWidth = 5;
            panelBackground.Controls.Add(pictureBox1);
            panelBackground.FillColor = Color.FromArgb(11, 7, 17);
            panelBackground.IsBorder = false;
            panelBackground.IsFill = true;
            panelBackground.Location = new Point(12, 15);
            panelBackground.Name = "panelBackground";
            panelBackground.Radius = 20;
            panelBackground.Size = new Size(1011, 543);
            panelBackground.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(11, 7, 17);
            pictureBox1.Image = Properties.Resources.ezgif_com_gif_maker;
            pictureBox1.Location = new Point(344, 101);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(323, 328);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(231, 34, 83);
            ClientSize = new Size(1266, 573);
            Controls.Add(panelChildForm);
            Controls.Add(panelSideMenu);
            Font = new Font("Cascadia Code", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Dashboard";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            panelSideMenu.ResumeLayout(false);
            panelInformation.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logo).EndInit();
            panelChildForm.ResumeLayout(false);
            panelBackground.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSideMenu;
        private Button btnLogOut;
        private Panel panelLogo;
        private PictureBox logo;
        private Panel panelChildForm;
        private CustomElements.RoundPanel panelBackground;
        private PictureBox pictureBox1;
        private Button btnSettings;
        private Button btnErrorLogs;
        private Button btnAuditLogs;
        private Panel panelInformation;
        private Button btnReviews;
        private Button btnAnime;
        private Button btnUsers;
        private Button btnManagement;
    }
}