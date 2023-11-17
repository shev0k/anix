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
using AniX_FormsLogic;
using Anix_Shared.DomainModels;
using AniX_Shared.DomainModels;
using AniX_Utility;

namespace AniX_APP.Forms_Dashboard
{
    public partial class Reviews : Form
    {
        private ApplicationModel _appModel;
        private ReviewsFormLogic _reviewsFormLogic;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;

        public Reviews(ApplicationModel appModel, IExceptionHandlingService exceptionHandlingService, IErrorLoggingService errorLoggingService)
        {
            InitializeComponent();
            _appModel = appModel;
            _reviewsFormLogic = new ReviewsFormLogic(_appModel);
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
            btnUser.Text = $" {_appModel.LoggedInUser.Username}";
            InitializeDataGridViewStyles();
        }

        private void InitializeDataGridViewStyles()
        {
            #region COLORS DATAGRID
            dgvReviews.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 34, 83);
            dgvReviews.DefaultCellStyle.SelectionForeColor = dgvReviews.DefaultCellStyle.ForeColor;
            dgvReviews.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 34, 83);
            dgvReviews.RowHeadersDefaultCellStyle.SelectionForeColor = dgvReviews.RowHeadersDefaultCellStyle.ForeColor;
            dgvReviews.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvReviews.AdvancedRowHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
            dgvReviews.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvReviews.AdvancedColumnHeadersBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.Single;
            dgvReviews.BackgroundColor = Color.FromArgb(231, 34, 83);
            dgvReviews.GridColor = Color.FromArgb(11, 7, 17);
            dgvReviews.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(231, 34, 83);
            dgvReviews.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(11, 7, 17);
            dgvReviews.DefaultCellStyle.ForeColor = Color.White;
            dgvReviews.DefaultCellStyle.BackColor = Color.FromArgb(11, 7, 17);
            dgvReviews.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvReviews.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReviews.EnableHeadersVisualStyles = false;
            dgvReviews.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReviews.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvReviews.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvReviews.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvReviews.AllowUserToResizeRows = false;

            foreach (DataGridViewColumn column in dgvReviews.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dgvReviews.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }

            foreach (DataGridViewRow row in dgvReviews.Rows)
            {
                row.Resizable = DataGridViewTriState.False;
            }
            #endregion
        }

        private async void Reviews_Load(object sender, EventArgs e)
        {
            dgvReviews.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(DgvReviews_DataBindingComplete);
            await RefreshReviewsAsync();
            PopulateFilterComboBox();
        }

        private async void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                Review selectedReview = GetSelectedReviewFromDataGridView();
                if (selectedReview != null && !selectedReview.IsApproved)
                {
                    OperationResult operationResult = await _reviewsFormLogic.ApproveReviewAsync(selectedReview.Id);
                    if (operationResult.Success)
                    {
                        RJMessageBox.Show(operationResult.Message, "Success", MessageBoxButtons.OK);
                        await RefreshReviewsAsync();
                    }
                    else
                    {
                        RJMessageBox.Show(operationResult.Message, "Error", MessageBoxButtons.OK);
                    }
                }
                else if (selectedReview.IsApproved)
                {
                    RJMessageBox.Show("This review is already approved.", "Information", MessageBoxButtons.OK);
                }
                else
                {
                    RJMessageBox.Show("No review selected. Please select a review to approve.", "Warning", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                RJMessageBox.Show("An error occurred while approving the review.", "Error", MessageBoxButtons.OK);
            }
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Review selectedReview = GetSelectedReviewFromDataGridView();
                if (selectedReview != null)
                {
                    DialogResult dialogResult = RJMessageBox.Show($"Are you sure you want to delete this review?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        OperationResult operationResult = await _reviewsFormLogic.DeleteReviewAsync(selectedReview.Id);
                        if (operationResult.Success)
                        {
                            RJMessageBox.Show(operationResult.Message, "Success", MessageBoxButtons.OK);
                            await RefreshReviewsAsync();
                        }
                        else
                        {
                            RJMessageBox.Show(operationResult.Message, "Error", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    RJMessageBox.Show("No review selected. Please select a review to delete.", "Warning", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }
                RJMessageBox.Show("An error occurred while deleting the review.", "Error", MessageBoxButtons.OK);
            }
        }

        private async void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                Review selectedReview = GetSelectedReviewFromDataGridView();
                if (selectedReview != null)
                {
                    string reviewDetails = _reviewsFormLogic.ShowReviewDetails(selectedReview);
                    RJMessageBox.Show(reviewDetails, "Review Details", MessageBoxButtons.OK);
                }
                else
                {
                    RJMessageBox.Show("No review selected. Please select a review.", "Warning", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }
                RJMessageBox.Show("An error occurred while displaying review details.", "Error", MessageBoxButtons.OK);
            }
        }

        private async void dgvReviews_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Review selectedReview = GetSelectedReviewFromDataGridView("CurrentCell");
                if (selectedReview != null)
                {
                    ShowReviewDetails(selectedReview);
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

        private async void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await UpdateReviewListAsync();
        }

        #region USEFUL
        private void PopulateFilterComboBox()
        {
            cmbFilter.Items.AddRange(new string[] { "All", "Approved", "Pending" });
            cmbFilter.SelectedIndex = 0;
        }
        private void ShowReviewDetails(Review review)
        {
            string reviewDetails = _reviewsFormLogic.ShowReviewDetails(review);
            RJMessageBox.Show(reviewDetails, "Review Details", MessageBoxButtons.OK);
        }
        private Review GetSelectedReviewFromDataGridView(string selectionType)
        {
            int rowIndex = (selectionType == "SelectedRows") ? dgvReviews.SelectedRows[0].Index : dgvReviews.CurrentCell.RowIndex;
            List<Tuple<Review, object>> originalReview = (List<Tuple<Review, object>>)dgvReviews.Tag;
            return originalReview[rowIndex].Item1;
        }
        private void DgvReviews_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvReviews.Columns.Contains("Id"))
            {
                dgvReviews.Columns["Id"].Visible = false;
            }
            if (dgvReviews.Columns.Contains("Rating"))
            {
                dgvReviews.Columns["Rating"].Visible = false;
            }
        }
        private async Task RefreshReviewsAsync()
        {
            try
            {
                string selectedFilter = cmbFilter.SelectedItem?.ToString() ?? "All";
                List<Review> reviews = await _reviewsFormLogic.UpdateReviewListAsync(selectedFilter);
                List<Tuple<Review, object>> transformedReviews = _reviewsFormLogic.TransformReviewsForDataGridView(reviews);

                dgvReviews.DataSource = transformedReviews.Select(t => t.Item2).ToList();
                dgvReviews.Tag = transformedReviews;
            }
            catch (Exception ex)
            {
                await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
            }
        }
        private async Task UpdateReviewListAsync()
        {
            string selectedFilter = cmbFilter.SelectedItem?.ToString() ?? "All Reviews";
            List<Review> reviews = await _reviewsFormLogic.UpdateReviewListAsync(selectedFilter);
            List<Tuple<Review, object>> transformedReviews = _reviewsFormLogic.TransformReviewsForDataGridView(reviews);

            dgvReviews.DataSource = transformedReviews.Select(t => t.Item2).ToList();
            dgvReviews.Tag = transformedReviews;
        }
        private Review GetSelectedReviewFromDataGridView()
        {
            int rowIndex = dgvReviews.CurrentCell.RowIndex;
            List<Tuple<Review, object>> originalReviews = (List<Tuple<Review, object>>)dgvReviews.Tag;

            return _reviewsFormLogic.GetSelectedReviewFromDataGridView(originalReviews, rowIndex);
        }
        #endregion
    }
}
