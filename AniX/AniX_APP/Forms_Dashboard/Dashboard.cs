using AniX_APP.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anix_Shared.DomainModels;
using AniX_Controllers;
using AniX_APP.CustomElements;
using AniX_FormsLogic;
using AniX_Utility;

namespace AniX_APP.Forms_Dashboard
{
    public partial class Dashboard : Form
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
            SetButtonStyle(btnManagement);
            SetButtonStyle(btnUsers);
            SetButtonStyle(btnAnime);
            SetButtonStyle(btnReviews);
            SetButtonStyle(btnAuditLogs);
            SetButtonStyle(btnErrorLogs);
            SetButtonStyle(btnSettings);
            SetButtonStyle(btnLogOut);
        }

        private void SetButtonImages()
        {
            SetImageButtonStyle(btnManagement, btnManagement.Image, AniX_APP.Properties.Resources.management_invert);
            SetImageButtonStyle(btnAuditLogs, btnAuditLogs.Image, AniX_APP.Properties.Resources.audit_invert);
            SetImageButtonStyle(btnErrorLogs, btnErrorLogs.Image, AniX_APP.Properties.Resources.error_invert);
            SetImageButtonStyle(btnSettings, btnSettings.Image, AniX_APP.Properties.Resources.settings_invert);
            SetImageButtonStyle(btnLogOut, btnLogOut.Image, AniX_APP.Properties.Resources.exit_invert);
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
            button.FlatAppearance.BorderSize = 0;
            button.MouseEnter += Button_MouseEnter;
            button.MouseMove += Button_MouseMove;
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

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                Button_MouseEnter(button, EventArgs.Empty);
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

        private void SetActiveButton(Button button)
        {
            if (activeButton != null)
            {
                ResetButtonStyle();
            }

            activeButton = button;

            HighlightButton();
        }

        private void ResetButtonStyle()
        {
            if (activeButton != null)
            {
                activeButton.BackColor = GetOriginalButtonColor(activeButton);
                activeButton.ForeColor = Color.Silver;
                if (buttonImages.ContainsKey(activeButton))
                {
                    activeButton.Image = buttonImages[activeButton].Original;
                }
            }
        }

        private Color GetOriginalButtonColor(Button button)
        {
            if (button == btnManagement)
            {
                return Color.FromArgb(11, 7, 17);
            }
            else if (button == btnAuditLogs)
            {
                return Color.FromArgb(11, 7, 17);
            }
            else if (button == btnErrorLogs)
            {
                return Color.FromArgb(11, 7, 17);
            }
            else if (button == btnSettings)
            {
                return Color.FromArgb(11, 7, 17);
            }
            else if (button == btnLogOut)
            {
                return Color.FromArgb(11, 7, 17);
            }

            return Color.FromArgb(35, 32, 39);
        }

        #endregion

        #region CHILD FORMS

        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
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

        private ApplicationModel _dashboardModel;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;

        public Dashboard(
            ApplicationModel dashboardModel,
            IExceptionHandlingService exceptionHandlingService,
            IErrorLoggingService errorLoggingService)
        {
            InitializeComponent();
            SetButtonStyles();
            SetButtonImages();
            hideSubMenu();
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
            openChildForm(new Users(dashboardModel, exceptionHandlingService, errorLoggingService));
            SetActiveButton(btnManagement);
            _dashboardModel = dashboardModel;
        }

        private void btnManagement_Click(object sender, EventArgs e)
        {
            showSubMenu(panelInformation);
            SetActiveButton(btnUsers);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            HandleLogout();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            openChildForm(new Users(_dashboardModel, _exceptionHandlingService, _errorLoggingService));
            SetActiveButton((Button)sender);
        }

        private void btnAnime_Click(object sender, EventArgs e)
        {
            openChildForm(new Anime(_dashboardModel.LoggedInUser));
            SetActiveButton((Button)sender);
        }

        private void btnReviews_Click(object sender, EventArgs e)
        {
            openChildForm(new Reviews(_dashboardModel.LoggedInUser));
            SetActiveButton((Button)sender);
        }

        private void btnAuditLogs_Click(object sender, EventArgs e)
        {
            openChildForm(new AuditLogs(_dashboardModel.LoggedInUser));
            SetActiveButton((Button)sender);
        }

        private void btnErrorLogs_Click(object sender, EventArgs e)
        {
            openChildForm(new ErrorLogs(_dashboardModel.LoggedInUser));
            SetActiveButton((Button)sender);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            openChildForm(new Settings(_dashboardModel.LoggedInUser));
            SetActiveButton((Button)sender);
        }

        private void HandleLogout()
        {
            _dashboardModel.LoggedInUser = null;
            Main windowOpen = new Main(
                _dashboardModel,
                _exceptionHandlingService,
                _errorLoggingService);
            this.Hide();
            windowOpen.ShowDialog();
            this.Close();
        }
    }
}
