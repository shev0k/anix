using AniX_APP.CustomElements;
using AniX_APP.Forms;
using AniX_APP.Forms_Dashboard;
using AniX_BusinessLogic;
using AniX_DAL;
using Anix_Shared.DomainModels;
using System;
using System.Windows.Forms;

namespace AniX_APP
{
    public partial class Main : Form
    {
        #region BUTTON STYLE

        private class ButtonImages
        {
            public Image Original { get; set; }
            public Image Inverted { get; set; }
        }

        private Button activeButton = null;
        private Dictionary<Button, ButtonImages> buttonImages = new Dictionary<Button, ButtonImages>();
        private Form activeForm = null;

        private void SetButtonStyles()
        {
            SetButtonStyle(btnInformation);
            SetButtonStyle(btnOverview);
            SetButtonStyle(btnExit);
        }

        private void SetButtonImages()
        {
            SetImageButtonStyle(btnInformation, btnInformation.Image, AniX_APP.Properties.Resources.question_invert);
            SetImageButtonStyle(btnExit, btnExit.Image, AniX_APP.Properties.Resources.exit_invert);
        }

        private void SetImageButtonStyle(Button button, Image originalImage, Image invertedImage)
        {
            SetButtonStyle(button);
            buttonImages[button] = new ButtonImages { Original = originalImage, Inverted = invertedImage };
        }

        private void SetButtonStyle(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.FromArgb(35, 32, 39);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(35, 32, 39);
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 34, 83);
            button.FlatAppearance.BorderSize = 0; // No border
            button.MouseEnter += Button_MouseEnter;
            button.MouseLeave += Button_MouseLeave;
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                button.ForeColor = Color.Black;
                if (buttonImages.ContainsKey(button))
                {
                    button.Image = buttonImages[button].Inverted;
                }
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (button != activeButton)
                {
                    button.ForeColor = Color.Silver;
                    if (buttonImages.ContainsKey(button))
                    {
                        button.Image = buttonImages[button].Original;
                    }
                }
                else
                {
                    button.ForeColor = Color.Black;
                    if (buttonImages.ContainsKey(button))
                    {
                        button.Image = buttonImages[button].Inverted;
                    }
                }
            }
        }

        private void HighlightButton()
        {
            activeButton.BackColor = Color.FromArgb(231, 34, 83);
            activeButton.ForeColor = Color.FromArgb(11, 7, 17);
            if (buttonImages.ContainsKey(activeButton))
            {
                activeButton.Image = buttonImages[activeButton].Inverted;
            }
        }

        private void ResetButtonStyle()
        {
            activeButton.BackColor = Color.FromArgb(35, 32, 39);
            activeButton.ForeColor = Color.Silver;
            if (buttonImages.ContainsKey(activeButton))
            {
                activeButton.Image = buttonImages[activeButton].Original;
            }
        }

        #endregion

        #region CHILD FORMS
        private void openChildForm(Form childForm, Button senderButton)
        {
            if (activeButton != null)
            {
                ResetButtonStyle();
            }

            activeButton = senderButton;

            HighlightButton();

            if (activeForm != null)
            {
                activeForm.Closed -= ChildForm_Closed;
                activeForm.Close();
            }

            activeForm = childForm;
            activeForm.Closed += ChildForm_Closed;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void ChildForm_Closed(object sender, EventArgs e)
        {
            if (activeButton != null)
            {
                ResetButtonStyle();
                activeButton = null;
            }
        }

        private void hideSubMenu()
        {
            panelInformation.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        #endregion

        #region FOCUS_APP

        private void panelBackground_Click(object sender, EventArgs e)
        {
            btnLogin.Focus();
        }

        private void tbxUsername_Leave(object sender, EventArgs e)
        {
            btnLogin.Focus();
        }

        private void tbxPassword_Leave(object sender, EventArgs e)
        {
            btnLogin.Focus();
        }

        private void panelSideMenu_Click(object sender, EventArgs e)
        {
            btnLogin.Focus();
        }

        private void panelChildForm_Click(object sender, EventArgs e)
        {
            btnLogin.Focus();
        }

        private void Main_Click(object sender, EventArgs e)
        {
            btnLogin.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            btnLogin.Focus();
        }
        #endregion

        public Main()
        {
            InitializeComponent();
            SetButtonStyles();
            SetButtonImages();
        }

        private void btnInformation_Click(object sender, EventArgs e)
        {
            showSubMenu(panelInformation);
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            openChildForm(new Overview(), (Button)sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbxUsername.Texts) || string.IsNullOrEmpty(tbxPassword.Texts))
                {
                    RJMessageBox.Show("Input correct credentials!", "", MessageBoxButtons.OK);
                    return;
                }

                AuthenticationService authService = new AuthenticationService();
                User authenticatedUser = authService.AuthenticateUser(tbxUsername.Texts, tbxPassword.Texts);

                if (authenticatedUser == null)
                {
                    RJMessageBox.Show("Failed to authenticate. Please check your username and password.", "", MessageBoxButtons.OK);
                    return;
                }
                if (authenticatedUser.Banned)
                {
                    RJMessageBox.Show("Your account has been banned. Contact the administrator for more details.", "", MessageBoxButtons.OK);
                    return;
                }
                if (!authenticatedUser.IsAdmin)
                {
                    RJMessageBox.Show("You are not authorized to access this application.", "", MessageBoxButtons.OK);
                    return;
                }

                Dashboard windowOpen = new Dashboard();
                RJMessageBox.Show($"Welcome back, < {authenticatedUser.Username} >", "", MessageBoxButtons.OK);
                this.Hide();
                windowOpen.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandlingService.HandleException(ex);
                RJMessageBox.Show("An error occurred. Please try again later.", "", MessageBoxButtons.OK);
            }
        }
    }
}