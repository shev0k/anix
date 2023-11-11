using housing.CustomElements;
using System.Data;
using System.Xml.Linq;

namespace AniX_APP.Forms_Utility
{
    partial class User_Add_Edit
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
            panelButtons = new Panel();
            cboxIsBanned = new CustomElements.CustomCheckBox();
            cboxIsAdmin = new CustomElements.CustomCheckBox();
            label3 = new Label();
            lbFormTitle = new Label();
            btnClose = new Button();
            btnSave = new CustomElements.RoundButton();
            lbIsAdmin = new Label();
            lbPassword = new Label();
            tbxPassword = new RoundTextBox();
            lbEmail = new Label();
            tbxEmail = new RoundTextBox();
            lbUsername = new Label();
            tbxUsername = new RoundTextBox();
            panelBody = new Panel();
            panelButtons.SuspendLayout();
            panelBody.SuspendLayout();
            SuspendLayout();
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.FromArgb(11, 7, 17);
            panelButtons.Controls.Add(cboxIsBanned);
            panelButtons.Controls.Add(cboxIsAdmin);
            panelButtons.Controls.Add(label3);
            panelButtons.Controls.Add(lbFormTitle);
            panelButtons.Controls.Add(btnClose);
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(lbIsAdmin);
            panelButtons.Controls.Add(lbPassword);
            panelButtons.Controls.Add(tbxPassword);
            panelButtons.Controls.Add(lbEmail);
            panelButtons.Controls.Add(tbxEmail);
            panelButtons.Controls.Add(lbUsername);
            panelButtons.Controls.Add(tbxUsername);
            panelButtons.Location = new Point(3, 3);
            panelButtons.Margin = new Padding(4, 3, 4, 3);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(439, 274);
            panelButtons.TabIndex = 4;
            panelButtons.MouseDown += panelTitleBar_MouseDown;
            // 
            // cboxIsBanned
            // 
            cboxIsBanned.AutoSize = true;
            cboxIsBanned.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cboxIsBanned.Location = new Point(349, 177);
            cboxIsBanned.MinimumSize = new Size(45, 22);
            cboxIsBanned.Name = "cboxIsBanned";
            cboxIsBanned.OffBackColor = Color.FromArgb(231, 34, 83);
            cboxIsBanned.OffToggleColor = Color.FromArgb(231, 34, 83);
            cboxIsBanned.OnBackColor = Color.FromArgb(231, 34, 83);
            cboxIsBanned.OnToggleColor = Color.FromArgb(11, 7, 17);
            cboxIsBanned.Size = new Size(45, 22);
            cboxIsBanned.SolidStyle = false;
            cboxIsBanned.TabIndex = 18;
            cboxIsBanned.UseVisualStyleBackColor = true;
            cboxIsBanned.CheckedChanged += cboxIsBanned_CheckedChanged;
            // 
            // cboxIsAdmin
            // 
            cboxIsAdmin.AutoSize = true;
            cboxIsAdmin.BackColor = Color.FromArgb(231, 34, 83);
            cboxIsAdmin.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cboxIsAdmin.ForeColor = Color.FromArgb(231, 34, 83);
            cboxIsAdmin.Location = new Point(144, 176);
            cboxIsAdmin.MinimumSize = new Size(45, 22);
            cboxIsAdmin.Name = "cboxIsAdmin";
            cboxIsAdmin.OffBackColor = Color.FromArgb(231, 34, 83);
            cboxIsAdmin.OffToggleColor = Color.FromArgb(231, 34, 83);
            cboxIsAdmin.OnBackColor = Color.FromArgb(231, 34, 83);
            cboxIsAdmin.OnToggleColor = Color.FromArgb(11, 7, 17);
            cboxIsAdmin.Size = new Size(45, 22);
            cboxIsAdmin.SolidStyle = false;
            cboxIsAdmin.TabIndex = 17;
            cboxIsAdmin.UseVisualStyleBackColor = false;
            cboxIsAdmin.CheckedChanged += cboxIsAdmin_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(231, 34, 83);
            label3.Location = new Point(235, 177);
            label3.Name = "label3";
            label3.Size = new Size(73, 21);
            label3.TabIndex = 16;
            label3.Text = "Banned:";
            // 
            // lbFormTitle
            // 
            lbFormTitle.AutoSize = true;
            lbFormTitle.Font = new Font("Cascadia Code", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lbFormTitle.ForeColor = Color.FromArgb(231, 34, 83);
            lbFormTitle.Location = new Point(8, 12);
            lbFormTitle.Margin = new Padding(4, 0, 4, 0);
            lbFormTitle.Name = "lbFormTitle";
            lbFormTitle.Size = new Size(42, 16);
            lbFormTitle.TabIndex = 14;
            lbFormTitle.Text = "Title";
            // 
            // btnClose
            // 
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 34, 83);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(392, 0);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(47, 38);
            btnClose.TabIndex = 13;
            btnClose.Text = "X";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(231, 34, 83);
            btnSave.BackgroundColor = Color.FromArgb(231, 34, 83);
            btnSave.BorderColor = Color.FromArgb(231, 34, 83);
            btnSave.BorderRadius = 14;
            btnSave.BorderSize = 0;
            btnSave.ClickedColor = Color.FromArgb(231, 34, 83);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnSave.ForeColor = Color.FromArgb(11, 7, 17);
            btnSave.Location = new Point(38, 207);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(356, 43);
            btnSave.TabIndex = 12;
            btnSave.Text = "Save";
            btnSave.TextColor = Color.FromArgb(11, 7, 17);
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lbIsAdmin
            // 
            lbIsAdmin.AutoSize = true;
            lbIsAdmin.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbIsAdmin.ForeColor = Color.FromArgb(231, 34, 83);
            lbIsAdmin.Location = new Point(38, 177);
            lbIsAdmin.Name = "lbIsAdmin";
            lbIsAdmin.Size = new Size(64, 21);
            lbIsAdmin.TabIndex = 11;
            lbIsAdmin.Text = "Admin:";
            // 
            // lbPassword
            // 
            lbPassword.AutoSize = true;
            lbPassword.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbPassword.ForeColor = Color.FromArgb(231, 34, 83);
            lbPassword.Location = new Point(38, 138);
            lbPassword.Name = "lbPassword";
            lbPassword.Size = new Size(91, 21);
            lbPassword.TabIndex = 9;
            lbPassword.Text = "Password:";
            // 
            // tbxPassword
            // 
            tbxPassword.BackColor = Color.FromArgb(231, 34, 83);
            tbxPassword.BorderColor = Color.FromArgb(231, 34, 83);
            tbxPassword.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxPassword.BorderRadius = 14;
            tbxPassword.BorderSize = 2;
            tbxPassword.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxPassword.ForeColor = Color.FromArgb(11, 7, 17);
            tbxPassword.Location = new Point(144, 132);
            tbxPassword.Margin = new Padding(4);
            tbxPassword.Multiline = false;
            tbxPassword.Name = "tbxPassword";
            tbxPassword.Padding = new Padding(10, 7, 10, 7);
            tbxPassword.PasswordChar = false;
            tbxPassword.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxPassword.PlaceholderText = "";
            tbxPassword.Size = new Size(250, 33);
            tbxPassword.TabIndex = 8;
            tbxPassword.Texts = "";
            tbxPassword.UnderlinedStyle = false;
            // 
            // lbEmail
            // 
            lbEmail.AutoSize = true;
            lbEmail.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbEmail.ForeColor = Color.FromArgb(231, 34, 83);
            lbEmail.Location = new Point(38, 97);
            lbEmail.Name = "lbEmail";
            lbEmail.Size = new Size(64, 21);
            lbEmail.TabIndex = 7;
            lbEmail.Text = "Email:";
            // 
            // tbxEmail
            // 
            tbxEmail.BackColor = Color.FromArgb(231, 34, 83);
            tbxEmail.BorderColor = Color.FromArgb(231, 34, 83);
            tbxEmail.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxEmail.BorderRadius = 14;
            tbxEmail.BorderSize = 2;
            tbxEmail.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxEmail.ForeColor = Color.FromArgb(11, 7, 17);
            tbxEmail.Location = new Point(144, 91);
            tbxEmail.Margin = new Padding(4);
            tbxEmail.Multiline = false;
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Padding = new Padding(10, 7, 10, 7);
            tbxEmail.PasswordChar = false;
            tbxEmail.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxEmail.PlaceholderText = "";
            tbxEmail.Size = new Size(250, 33);
            tbxEmail.TabIndex = 6;
            tbxEmail.Texts = "";
            tbxEmail.UnderlinedStyle = false;
            // 
            // lbUsername
            // 
            lbUsername.AutoSize = true;
            lbUsername.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbUsername.ForeColor = Color.FromArgb(231, 34, 83);
            lbUsername.Location = new Point(38, 56);
            lbUsername.Name = "lbUsername";
            lbUsername.Size = new Size(91, 21);
            lbUsername.TabIndex = 5;
            lbUsername.Text = "Username:";
            // 
            // tbxUsername
            // 
            tbxUsername.BackColor = Color.FromArgb(231, 34, 83);
            tbxUsername.BorderColor = Color.FromArgb(231, 34, 83);
            tbxUsername.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxUsername.BorderRadius = 14;
            tbxUsername.BorderSize = 2;
            tbxUsername.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxUsername.ForeColor = Color.FromArgb(11, 7, 17);
            tbxUsername.Location = new Point(144, 50);
            tbxUsername.Margin = new Padding(4);
            tbxUsername.Multiline = false;
            tbxUsername.Name = "tbxUsername";
            tbxUsername.Padding = new Padding(10, 7, 10, 7);
            tbxUsername.PasswordChar = false;
            tbxUsername.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxUsername.PlaceholderText = "";
            tbxUsername.Size = new Size(250, 33);
            tbxUsername.TabIndex = 4;
            tbxUsername.Texts = "";
            tbxUsername.UnderlinedStyle = false;
            // 
            // panelBody
            // 
            panelBody.BackColor = Color.FromArgb(231, 34, 83);
            panelBody.Controls.Add(panelButtons);
            panelBody.Dock = DockStyle.Fill;
            panelBody.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            panelBody.Location = new Point(0, 0);
            panelBody.Margin = new Padding(4, 3, 4, 3);
            panelBody.Name = "panelBody";
            panelBody.Padding = new Padding(13, 14, 0, 0);
            panelBody.Size = new Size(445, 280);
            panelBody.TabIndex = 6;
            // 
            // User_Add_Edit
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(231, 34, 83);
            ClientSize = new Size(445, 280);
            Controls.Add(panelBody);
            Font = new Font("Cascadia Code", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.Black;
            Margin = new Padding(3, 4, 3, 4);
            Name = "User_Add_Edit";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "User_Add_Edit";
            Load += User_Add_Edit_Load;
            panelButtons.ResumeLayout(false);
            panelButtons.PerformLayout();
            panelBody.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelButtons;
        private Label lbFormTitle;
        private Button btnClose;
        private CustomElements.RoundButton btnSave;
        private Label lbIsAdmin;
        private Label lbPassword;
        private RoundTextBox tbxPassword;
        private Label lbEmail;
        private RoundTextBox tbxEmail;
        private Label lbUsername;
        private RoundTextBox tbxUsername;
        private Panel panelBody;
        private Label label3;
        private CustomElements.CustomCheckBox cboxIsAdmin;
        private CustomElements.CustomCheckBox cboxIsBanned;
    }
}