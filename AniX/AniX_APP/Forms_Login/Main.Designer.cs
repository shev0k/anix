namespace AniX_APP
{
    partial class Main
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
            panelSideMenu = new Panel();
            btnExit = new Button();
            panelInformation = new Panel();
            btnOverview = new Button();
            btnInformation = new Button();
            panelLogo = new Panel();
            logo = new PictureBox();
            panelChildForm = new Panel();
            panelBackground = new CustomElements.RoundPanel();
            pictureBox1 = new PictureBox();
            lbWelcome = new Label();
            tbxPassword = new housing.CustomElements.RoundTextBox();
            tbxUsername = new housing.CustomElements.RoundTextBox();
            btnLogin = new CustomElements.RoundButton();
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
            panelSideMenu.Controls.Add(btnExit);
            panelSideMenu.Controls.Add(panelInformation);
            panelSideMenu.Controls.Add(btnInformation);
            panelSideMenu.Controls.Add(panelLogo);
            panelSideMenu.Dock = DockStyle.Left;
            panelSideMenu.Location = new Point(0, 0);
            panelSideMenu.Margin = new Padding(4);
            panelSideMenu.Name = "panelSideMenu";
            panelSideMenu.Size = new Size(225, 573);
            panelSideMenu.TabIndex = 0;
            panelSideMenu.Click += panelSideMenu_Click;
            // 
            // btnExit
            // 
            btnExit.Dock = DockStyle.Bottom;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseDownBackColor = Color.FromArgb(23, 21, 32);
            btnExit.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 22, 34);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.ForeColor = Color.Silver;
            btnExit.Image = Properties.Resources.logout;
            btnExit.ImageAlign = ContentAlignment.MiddleLeft;
            btnExit.Location = new Point(0, 533);
            btnExit.Name = "btnExit";
            btnExit.Padding = new Padding(5, 0, 0, 0);
            btnExit.Size = new Size(225, 40);
            btnExit.TabIndex = 15;
            btnExit.TabStop = false;
            btnExit.Text = "  Exit";
            btnExit.TextAlign = ContentAlignment.MiddleLeft;
            btnExit.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // panelInformation
            // 
            panelInformation.BackColor = Color.FromArgb(35, 32, 39);
            panelInformation.Controls.Add(btnOverview);
            panelInformation.Dock = DockStyle.Top;
            panelInformation.Location = new Point(0, 137);
            panelInformation.Name = "panelInformation";
            panelInformation.Size = new Size(225, 45);
            panelInformation.TabIndex = 14;
            // 
            // btnOverview
            // 
            btnOverview.Dock = DockStyle.Top;
            btnOverview.FlatAppearance.BorderSize = 0;
            btnOverview.FlatAppearance.MouseDownBackColor = Color.FromArgb(42, 38, 46);
            btnOverview.FlatAppearance.MouseOverBackColor = Color.FromArgb(42, 38, 46);
            btnOverview.FlatStyle = FlatStyle.Flat;
            btnOverview.ForeColor = Color.Silver;
            btnOverview.Location = new Point(0, 0);
            btnOverview.Name = "btnOverview";
            btnOverview.Padding = new Padding(35, 0, 0, 0);
            btnOverview.Size = new Size(225, 45);
            btnOverview.TabIndex = 3;
            btnOverview.TabStop = false;
            btnOverview.Text = "Overview";
            btnOverview.TextAlign = ContentAlignment.MiddleLeft;
            btnOverview.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnOverview.UseVisualStyleBackColor = true;
            btnOverview.Click += btnOverview_Click;
            // 
            // btnInformation
            // 
            btnInformation.Dock = DockStyle.Top;
            btnInformation.FlatAppearance.BorderSize = 0;
            btnInformation.FlatAppearance.MouseDownBackColor = Color.FromArgb(23, 21, 32);
            btnInformation.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 22, 34);
            btnInformation.FlatStyle = FlatStyle.Flat;
            btnInformation.ForeColor = Color.Silver;
            btnInformation.Image = Properties.Resources.question;
            btnInformation.ImageAlign = ContentAlignment.MiddleLeft;
            btnInformation.Location = new Point(0, 92);
            btnInformation.Name = "btnInformation";
            btnInformation.Padding = new Padding(5, 0, 0, 0);
            btnInformation.Size = new Size(225, 45);
            btnInformation.TabIndex = 13;
            btnInformation.TabStop = false;
            btnInformation.Text = "  Information";
            btnInformation.TextAlign = ContentAlignment.MiddleLeft;
            btnInformation.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnInformation.UseVisualStyleBackColor = true;
            btnInformation.Click += btnInformation_Click;
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
            panelChildForm.TabIndex = 1;
            panelChildForm.Click += panelChildForm_Click;
            // 
            // panelBackground
            // 
            panelBackground.BackColor = Color.FromArgb(32, 30, 45);
            panelBackground.BackgroundImage = Properties.Resources.background;
            panelBackground.BorderColor = Color.White;
            panelBackground.BorderWidth = 5;
            panelBackground.Controls.Add(pictureBox1);
            panelBackground.Controls.Add(lbWelcome);
            panelBackground.Controls.Add(tbxPassword);
            panelBackground.Controls.Add(tbxUsername);
            panelBackground.Controls.Add(btnLogin);
            panelBackground.FillColor = Color.FromArgb(11, 7, 17);
            panelBackground.IsBorder = false;
            panelBackground.IsFill = true;
            panelBackground.Location = new Point(12, 15);
            panelBackground.Name = "panelBackground";
            panelBackground.Radius = 20;
            panelBackground.Size = new Size(1011, 543);
            panelBackground.TabIndex = 0;
            panelBackground.Click += panelBackground_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(11, 7, 17);
            pictureBox1.Image = Properties.Resources.Anix___Logo1;
            pictureBox1.Location = new Point(409, 44);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(193, 191);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // lbWelcome
            // 
            lbWelcome.BackColor = Color.FromArgb(11, 7, 17);
            lbWelcome.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbWelcome.ForeColor = Color.FromArgb(231, 34, 83);
            lbWelcome.Location = new Point(206, 259);
            lbWelcome.Name = "lbWelcome";
            lbWelcome.Size = new Size(598, 21);
            lbWelcome.TabIndex = 6;
            lbWelcome.Text = "Welcome, Admin! To gain entry, please enter the provided details!";
            // 
            // tbxPassword
            // 
            tbxPassword.BackColor = Color.FromArgb(11, 7, 17);
            tbxPassword.BorderColor = Color.FromArgb(231, 34, 83);
            tbxPassword.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxPassword.BorderRadius = 14;
            tbxPassword.BorderSize = 2;
            tbxPassword.Font = new Font("Cascadia Code", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbxPassword.ForeColor = Color.FromArgb(231, 34, 83);
            tbxPassword.Location = new Point(380, 367);
            tbxPassword.Margin = new Padding(4);
            tbxPassword.Multiline = false;
            tbxPassword.Name = "tbxPassword";
            tbxPassword.Padding = new Padding(10, 7, 10, 7);
            tbxPassword.PasswordChar = true;
            tbxPassword.PlaceholderColor = Color.FromArgb(231, 34, 83);
            tbxPassword.PlaceholderText = "< password >";
            tbxPassword.Size = new Size(250, 32);
            tbxPassword.TabIndex = 5;
            tbxPassword.Texts = "";
            tbxPassword.UnderlinedStyle = false;
            tbxPassword.Leave += tbxPassword_Leave;
            // 
            // tbxUsername
            // 
            tbxUsername.BackColor = Color.FromArgb(11, 7, 17);
            tbxUsername.BorderColor = Color.FromArgb(231, 34, 83);
            tbxUsername.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxUsername.BorderRadius = 14;
            tbxUsername.BorderSize = 2;
            tbxUsername.Font = new Font("Cascadia Code", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbxUsername.ForeColor = Color.FromArgb(231, 34, 83);
            tbxUsername.Location = new Point(380, 318);
            tbxUsername.Margin = new Padding(4);
            tbxUsername.Multiline = false;
            tbxUsername.Name = "tbxUsername";
            tbxUsername.Padding = new Padding(10, 7, 10, 7);
            tbxUsername.PasswordChar = false;
            tbxUsername.PlaceholderColor = Color.FromArgb(231, 34, 83);
            tbxUsername.PlaceholderText = "< username >";
            tbxUsername.Size = new Size(250, 32);
            tbxUsername.TabIndex = 2;
            tbxUsername.Texts = "";
            tbxUsername.UnderlinedStyle = false;
            tbxUsername.Leave += tbxUsername_Leave;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(231, 34, 83);
            btnLogin.BackgroundColor = Color.FromArgb(231, 34, 83);
            btnLogin.BorderColor = Color.FromArgb(231, 34, 83);
            btnLogin.BorderRadius = 14;
            btnLogin.BorderSize = 0;
            btnLogin.ClickedColor = Color.FromArgb(231, 34, 83);
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Cascadia Code", 12.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnLogin.ForeColor = Color.FromArgb(11, 7, 17);
            btnLogin.Location = new Point(349, 435);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(312, 40);
            btnLogin.TabIndex = 3;
            btnLogin.TabStop = false;
            btnLogin.Text = "ENTER";
            btnLogin.TextColor = Color.FromArgb(11, 7, 17);
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(231, 34, 83);
            ClientSize = new Size(1266, 573);
            Controls.Add(panelChildForm);
            Controls.Add(panelSideMenu);
            Font = new Font("Cascadia Code", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Main";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Click += Main_Click;
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
        private Panel panelLogo;
        private Button btnInformation;
        private Panel panelInformation;
        private Button btnOverview;
        private PictureBox logo;
        private Button btnExit;
        private Panel panelChildForm;
        private CustomElements.RoundPanel panelBackground;
        private PictureBox pictureBox1;
        private Label lbWelcome;
        private housing.CustomElements.RoundTextBox tbxPassword;
        private housing.CustomElements.RoundTextBox tbxUsername;
        private CustomElements.RoundButton btnLogin;
    }
}