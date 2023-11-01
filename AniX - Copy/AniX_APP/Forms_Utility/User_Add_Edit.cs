using AniX_Controllers;
using AniX_DAL;
using Anix_Shared.DomainModels;
using AniX_Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AniX_APP.CustomElements;
using AniX_FormsLogic;

namespace AniX_APP.Forms_Utility
{
    public partial class User_Add_Edit : Form
    {
        public enum FormMode { Add, Edit }
        private readonly FormMode _currentMode;
        private readonly ApplicationModel _appModel;
        private readonly UserAddEditFormLogic _userAddEditFormLogic;


        public User_Add_Edit(FormMode mode, ApplicationModel appModel)
        {
            InitializeComponent();
            _currentMode = mode;
            _appModel = appModel;
            _userAddEditFormLogic = new UserAddEditFormLogic(_appModel);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void User_Add_Edit_Load(object sender, EventArgs e)
        {
            if (_currentMode == FormMode.Edit && _appModel.UserToEdit != null)
            {
                tbxUsername.Texts = _appModel.UserToEdit.Username;
                tbxEmail.Texts = _appModel.UserToEdit.Email;
                cboxIsAdmin.Checked = _appModel.UserToEdit.IsAdmin;
                cboxIsBanned.Checked = _appModel.UserToEdit.Banned;
                lbFormTitle.Text = "Edit User";
            }
            else
            {
                lbFormTitle.Text = "Add User";
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var (IsValid, Message) = await _userAddEditFormLogic.ValidateFormAsync(
                tbxUsername.Texts,
                tbxEmail.Texts,
                tbxPassword.Texts,
                _currentMode == FormMode.Edit
            );

            if (IsValid)
            {
                try
                {
                    User user = CreateUserFromForm();

                    if (_currentMode == FormMode.Add)
                    {
                        bool created = await _userAddEditFormLogic.AddNewUserAsync(user, tbxPassword.Texts);
                        RJMessageBox.Show(created ? "User created successfully." : "An error occurred while creating the user.");
                    }
                    else
                    {
                        bool updated = await _userAddEditFormLogic.UpdateExistingUserAsync(user, tbxPassword.Texts);
                        RJMessageBox.Show(updated ? "User updated successfully." : "An error occurred while updating the user.");
                    }
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    await ExceptionHandlingService.HandleExceptionAsync(ex);
                    RJMessageBox.Show("An error occurred. Please try again.");
                }
                this.Close();
            }
            else
            {
                RJMessageBox.Show(Message);
            }
        }


        private User CreateUserFromForm()
        {
            return new User
            {
                Username = tbxUsername.Texts,
                Email = tbxEmail.Texts,
                IsAdmin = cboxIsAdmin.Checked,
                Banned = cboxIsBanned.Checked
            };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region FORM CUSTOM STYLE

        private void SetRadioButtonStyles(CustomCheckBox cbox, bool isChecked)
        {
            if (isChecked)
            {
                cbox.SolidStyle = true;
                cbox.OnBackColor = Color.FromArgb(231, 34, 83);
                cbox.OnToggleColor = Color.FromArgb(11, 7, 17);
            }
            else
            {
                cbox.SolidStyle = false;
                cbox.OffBackColor = Color.FromArgb(231, 34, 83);
                cbox.OffToggleColor = Color.FromArgb(231, 34, 83);
            }
        }

        private void cboxIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtonStyles(cboxIsAdmin, cboxIsAdmin.Checked);
        }

        private void cboxIsBanned_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtonStyles(cboxIsBanned, cboxIsBanned.Checked);
        }

        //FORM DRAG NO BORDER
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion
    }
}

