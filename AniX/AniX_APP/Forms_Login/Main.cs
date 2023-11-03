using AniX_APP.CustomElements;
using AniX_APP.Forms;
using AniX_APP.Forms_Dashboard;
using AniX_BusinessLogic;
using AniX_DAL;
using AniX_Utility;
using Anix_Shared.DomainModels;
using System;
using System.Windows.Forms;
using AniX_Controllers;
using AniX_FormsLogic;
using static AniX_Controllers.UserController;

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
        private ApplicationModel _appModel;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;
        public Main(
            ApplicationModel appModel,
            IExceptionHandlingService exceptionHandlingService,
            IErrorLoggingService errorLoggingService)
        {
            InitializeComponent();
            SetButtonStyles();
            SetButtonImages();
            _appModel = appModel;
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
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

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                User authenticatedUser = await _appModel.UserController.LoginAsync(tbxUsername.Texts, tbxPassword.Texts);
                _appModel.LoggedInUser = authenticatedUser;
                NavigateToDashboard();
            }
            catch (ValidationException ex)
            {
                RJMessageBox.Show(ex.Message, "", MessageBoxButtons.OK);
            }
            catch (AuthenticationException ex)
            {
                RJMessageBox.Show(ex.Message, "", MessageBoxButtons.OK);
            }
            catch (AccountBannedException ex)
            {
                RJMessageBox.Show(ex.Message, "", MessageBoxButtons.OK);
            }
            catch (AuthorizationException ex)
            {
                RJMessageBox.Show(ex.Message, "", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    RJMessageBox.Show("An unknown error occurred. Please try again later.", "", MessageBoxButtons.OK);
                }
            }
        }


        private void NavigateToDashboard()
        {
            Dashboard windowOpen = new Dashboard(
                _appModel,
                _exceptionHandlingService,
                _errorLoggingService
            );
            RJMessageBox.Show($"Welcome back, < {_appModel.LoggedInUser.Username} >", "", MessageBoxButtons.OK);
            this.Hide();
            windowOpen.ShowDialog();
            this.Close();
        }

    }
}