using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AniX_APP.CustomElements;
using AniX_APP.Forms_Utility;
using AniX_Controllers;
using AniX_FormsLogic;
using Anix_Shared.DomainModels;
using AniX_Utility;


namespace AniX_APP.Forms_Dashboard
{
    public partial class Users : Form
    {
        private ApplicationModel _appModel;
        private UsersFormLogic _usersFormLogic;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;


        public Users(ApplicationModel appModel, IExceptionHandlingService exceptionHandlingService, IErrorLoggingService errorLoggingService)
        {
            InitializeComponent();
            _appModel = appModel;
            _usersFormLogic = new UsersFormLogic(_appModel);
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
            btnUser.Text = $" {_appModel.LoggedInUser.Username}";
            InitializeDataGridViewStyles();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            User_Add_Edit form = new User_Add_Edit(
                User_Add_Edit.FormMode.Add,
                _appModel,
                _exceptionHandlingService,
                _errorLoggingService
            );
            form.FormClosed += async (s, args) =>
            {
                await RefreshUsersAsync();
                ResetFiltersAndCombobox();
            };
            form.ShowDialog();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                User selectedUser = GetSelectedUserFromDataGridView();
                if (selectedUser != null)
                {
                    _appModel.UserToEdit = selectedUser;
                    User_Add_Edit form = new User_Add_Edit(
                        User_Add_Edit.FormMode.Edit,
                        _appModel,
                        _exceptionHandlingService,
                        _errorLoggingService);
                    form.FormClosed += async (s, args) =>
                    {
                        await RefreshUsersAsync();
                        ResetFiltersAndCombobox();
                    };
                    form.ShowDialog();
                }
                else
                {
                    RJMessageBox.Show("No user selected. Please select a user to edit.", "Warning", MessageBoxButtons.OK);
                }
            }
            catch (Exception exception)
            {
                RJMessageBox.Show("No user selected. Please select a user to edit.", "Warning", MessageBoxButtons.OK);
            }
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                User selectedUser = GetSelectedUserFromDataGridView();
                if (selectedUser != null)
                {
                    DialogResult dialogResult = RJMessageBox.Show($"Are you sure you want to delete {selectedUser.Username}?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        OperationResult operationResult = await _usersFormLogic.DeleteUserAsync(selectedUser.Id);
                        if (operationResult.Success)
                        {
                            RJMessageBox.Show(operationResult.Message, "Success", MessageBoxButtons.OK);
                            await RefreshUsersAsync();
                        }
                        else
                        {
                            RJMessageBox.Show(operationResult.Message, "Error", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    RJMessageBox.Show("No user selected. Please select a user to delete.", "Warning", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }
                RJMessageBox.Show("An error occurred while deleting the user.", "Error", MessageBoxButtons.OK);
            }
        }

        private async void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                User selectedUser = GetSelectedUserFromDataGridView();
                if (selectedUser != null)
                {
                    string userDetails = _usersFormLogic.ShowUserDetails(selectedUser);
                    RJMessageBox.Show(userDetails);
                }
                else
                {
                    RJMessageBox.Show("No user selected.");
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }
                RJMessageBox.Show("An error occurred while performing the action.", "Error", MessageBoxButtons.OK);
            }
        }

        private async void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                User selectedUser = GetSelectedUserFromDataGridView("CurrentCell");
                if (selectedUser != null)
                {
                    ShowUserDetails(selectedUser);
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }
                RJMessageBox.Show("An error occurred while performing the action.", "Error", MessageBoxButtons.OK);
            }
        }

        private async void tbxUser__TextChanged(object sender, EventArgs e)
        {
            await UpdateUserListAsync();
        }

        private async void cmbFilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            await UpdateUserListAsync();
        }

        #region USEFUL
        private void InitializeDataGridViewStyles()
        {
            #region COLORS DATAGRID
            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 34, 83);
            dgvUsers.DefaultCellStyle.SelectionForeColor = dgvUsers.DefaultCellStyle.ForeColor;
            dgvUsers.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 34, 83);
            dgvUsers.RowHeadersDefaultCellStyle.SelectionForeColor = dgvUsers.RowHeadersDefaultCellStyle.ForeColor;
            dgvUsers.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvUsers.AdvancedRowHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
            dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvUsers.AdvancedColumnHeadersBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.Single;
            dgvUsers.BackgroundColor = Color.FromArgb(231, 34, 83);
            dgvUsers.GridColor = Color.FromArgb(11, 7, 17);
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(231, 34, 83);
            dgvUsers.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(11, 7, 17);
            dgvUsers.DefaultCellStyle.ForeColor = Color.White;
            dgvUsers.DefaultCellStyle.BackColor = Color.FromArgb(11, 7, 17);
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvUsers.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvUsers.AllowUserToResizeRows = false;

            foreach (DataGridViewColumn column in dgvUsers.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dgvUsers.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }

            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                row.Resizable = DataGridViewTriState.False;
            }
            #endregion
        }
        private async void Users_Load(object sender, EventArgs e)
        {
            dgvUsers.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(DgvUsers_DataBindingComplete);
            await RefreshUsersAsync();
        }
        private void ResetFiltersAndCombobox()
        {
            if (cmbFilter.Items.Count > 0)
            {
                cmbFilter.SelectedIndex = 0;
            }
        }
        private void ShowUserDetails(User user)
        {
            string userDetails = $"Username: {user.Username}\nEmail: {user.Email}\nBanned: {(user.Banned ? "Yes" : "No")}\nAdmin: {(user.IsAdmin ? "Yes" : "No")}";
            RJMessageBox.Show($"{userDetails}", "View User", MessageBoxButtons.OK);
        }
        private User GetSelectedUserFromDataGridView(string selectionType)
        {
            int rowIndex = (selectionType == "SelectedRows") ? dgvUsers.SelectedRows[0].Index : dgvUsers.CurrentCell.RowIndex;
            List<Tuple<User, object>> originalUsers = (List<Tuple<User, object>>)dgvUsers.Tag;
            return originalUsers[rowIndex].Item1;
        }
        private void DgvUsers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvUsers.Columns.Contains("Id"))
            {
                dgvUsers.Columns["Id"].Visible = false;
            }
        }
        private async Task RefreshUsersAsync()
        {
            List<User> users = await _usersFormLogic.RefreshUsersAsync();
            List<Tuple<User, object>> transformedUsers = _usersFormLogic.TransformUsersForDataGridView(users);

            dgvUsers.DataSource = transformedUsers.Select(t => t.Item2).ToList();
            dgvUsers.Tag = transformedUsers;

            if (dgvUsers.Columns["RegistrationDate"] != null)
                dgvUsers.Columns["RegistrationDate"].HeaderText = "Made on";
            if (dgvUsers.Columns["IsAdmin"] != null)
                dgvUsers.Columns["IsAdmin"].HeaderText = "Admin";
        }
        private async Task UpdateUserListAsync()
        {
            string selectedFilter = cmbFilter.SelectedItem?.ToString() ?? "All Users";
            string searchTerm = tbxUser.Texts;

            List<User> users = await _usersFormLogic.UpdateUserListAsync(selectedFilter, searchTerm);
            List<Tuple<User, object>> transformedUsers = _usersFormLogic.TransformUsersForDataGridView(users);

            dgvUsers.DataSource = transformedUsers.Select(t => t.Item2).ToList();
            dgvUsers.Tag = transformedUsers;
        }
        private User GetSelectedUserFromDataGridView()
        {
            int rowIndex = dgvUsers.CurrentCell.RowIndex;
            List<Tuple<User, object>> originalUsers = (List<Tuple<User, object>>)dgvUsers.Tag;

            return _usersFormLogic.GetSelectedUserFromDataGridView(originalUsers, rowIndex);
        }
        #endregion
    }
}