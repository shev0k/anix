
using AniX_APP.CustomElements;
using AniX_APP.Forms_Utility;
using AniX_FormsLogic;
using AniX_Shared.DomainModels;
using AniX_Utility;

namespace AniX_APP.Forms_Dashboard
{
    public partial class AnimeForm : Form
    {
        private ApplicationModel _appModel;
        private AnimeFormLogic _animeFormLogic;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;
        private System.Threading.Timer dgvTimer;
        private System.Windows.Forms.Timer suggestionTimer;

        public AnimeForm(ApplicationModel appModel, IExceptionHandlingService exceptionHandlingService, IErrorLoggingService errorLoggingService)
        {
            InitializeComponent();
            _appModel = appModel;
            _animeFormLogic = new AnimeFormLogic(_appModel);
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
            btnUser.Text = $" {_appModel.LoggedInUser.Username}";
            InitializeDataGridViewStyles();

            txtSearch.Visible = false;
            cmbFilterValues.Visible = false;
            txtSearch.TextChanged += txtSearch__TextChanged;
            cmbFilterValues.OnSelectedIndexChanged += cmbFilterValues_OnSelectedIndexChanged;
            cmbFilterOptions.OnSelectedIndexChanged += cmbFilterOptions_OnSelectedIndexChanged;
            lbSuggestion.Visible = false;

        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            ShowAnimeAddEditForm(Anime_Add_Edit.FormMode.Add);
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Anime selectedAnime = GetSelectedAnimeFromDataGridView();
                if (selectedAnime != null)
                {
                    _appModel.AnimeToEdit = selectedAnime;
                    ShowAnimeAddEditForm(Anime_Add_Edit.FormMode.Edit);
                }
                else
                {
                    RJMessageBox.Show(MessageConstants.NoAnimeSelectedToEdit, MessageConstants.Warning, MessageBoxButtons.OK);
                }
            }
            catch (Exception exception)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(exception);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(exception, LogSeverity.Critical);
                }
                RJMessageBox.Show(MessageConstants.NoAnimeSelectedToEdit, MessageConstants.Warning, MessageBoxButtons.OK);
            }
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Anime selectedAnime = GetSelectedAnimeFromDataGridView();
                if (selectedAnime != null)
                {
                    DialogResult dialogResult = RJMessageBox.Show(string.Format(MessageConstants.DeleteConfirmation, selectedAnime.Name), MessageConstants.DeleteConfirmation, MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        OperationResult operationResult = await _animeFormLogic.DeleteAnimeAsync(selectedAnime.Id);
                        if (operationResult.Success)
                        {
                            RJMessageBox.Show(operationResult.Message, "Success", MessageBoxButtons.OK);
                            await RefreshAnimeAsync();
                        }
                        else
                        {
                            RJMessageBox.Show(operationResult.Message, "Error", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    RJMessageBox.Show(MessageConstants.NoAnimeSelectedToEdit, MessageConstants.Warning, MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }
                RJMessageBox.Show(MessageConstants.NoAnimeSelectedToEdit, MessageConstants.Error, MessageBoxButtons.OK);
            }
        }

        private async void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                Anime selectedAnime = GetSelectedAnimeFromDataGridView();
                if (selectedAnime != null)
                {
                    ShowAnimeDetails(selectedAnime);
                }
                else
                {
                    RJMessageBox.Show(MessageConstants.NoAnimeSelected);
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }
                RJMessageBox.Show(MessageConstants.NoAnimeSelected);
            }
        }

        private async void dgvAnime_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Anime selectedAnime = GetSelectedAnimeFromDataGridView("CurrentCell");
                if (selectedAnime != null)
                {
                    ShowAnimeDetails(selectedAnime);
                }
            }
            catch (Exception ex)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                }
                RJMessageBox.Show(MessageConstants.NoAnimeSelected);
            }
        }

        private async void cmbFilterOptions_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFilter = cmbFilterOptions.SelectedItem.ToString();
            await ConfigureFilterControl(selectedFilter);
            ClearSuggestions();
        }

        private void txtSearch__TextChanged(object sender, EventArgs e)
        {
            PerformSearch();

            suggestionTimer?.Stop();
            suggestionTimer = new System.Windows.Forms.Timer
            {
                Interval = 100
            };
            suggestionTimer.Tick += async (s, args) =>
            {
                suggestionTimer.Stop();
                await PerformSearchAndUpdateSuggestions();
            };
            suggestionTimer.Start();
        }

        private void cmbFilterValues_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        #region USEFUL

        private void ClearSuggestions()
        {
            lbSuggestion.Text = string.Empty;
            txtSearch.Texts = string.Empty;
            lbSuggestion.Visible = false;
        }

        private async Task FetchAndDisplayAllAnime()
        {
            List<Anime> animes = await _animeFormLogic.RefreshAnimesAsync();
            List<Tuple<Anime, object>> transformedAnimes = _animeFormLogic.TransformAnimesForDataGridView(animes);

            this.Invoke(new Action(() =>
            {
                dgvAnime.DataSource = transformedAnimes.Select(t => t.Item2).ToList();
                dgvAnime.Tag = transformedAnimes;
            }));
        }

        public void ShowAnimeDetails(Anime anime)
        {
            string animeDetails = _animeFormLogic.ShowAnimeDetails(anime);
            RJMessageBox.Show($"{animeDetails}", $"{anime.Name}", MessageBoxButtons.OK);
        }

        private Anime GetSelectedAnimeFromDataGridView(string selectionType)
        {
            int rowIndex = (selectionType == "SelectedRows") ? dgvAnime.SelectedRows[0].Index : dgvAnime.CurrentCell.RowIndex;
            List<Tuple<Anime, object>> originalAnime = (List<Tuple<Anime, object>>)dgvAnime.Tag;
            return originalAnime[rowIndex].Item1;
        }

        private Anime GetSelectedAnimeFromDataGridView()
        {
            int rowIndex = dgvAnime.CurrentCell.RowIndex;
            List<Tuple<Anime, object>> originalAnimes = (List<Tuple<Anime, object>>)dgvAnime.Tag;

            return _animeFormLogic.GetSelectedAnimeFromDataGridView(originalAnimes, rowIndex);
        }

        private async Task ConfigureFilterControl(string filterType)
        {
            txtSearch.Visible = false;
            cmbFilterValues.Visible = false;

            if (IsDropdownFilterType(filterType))
            {
                await PopulateFilterValues(filterType);
                cmbFilterValues.Visible = true;
            }
            else if (IsTextSearchFilterType(filterType))
            {
                txtSearch.Visible = true;
            }
            else if (filterType == "All Anime")
            {
                await FetchAndDisplayAllAnime();
            }
            else
            {
                throw new NotImplementedException($"No input control implemented for filter type: {filterType}");
            }
        }

        private string DetermineSearchType()
        {
            string selectedFilter = cmbFilterOptions.SelectedItem?.ToString() ?? string.Empty;

            switch (selectedFilter)
            {
                case "Name":
                    return "Name";
                case "Year":
                    return "Year";
                case "Studio":
                    return "Studio";
                default:
                    return string.Empty;
            }
        }

        private bool IsDropdownFilterType(string filterType)
        {
            return new List<string> { "Country", "Language", "Rating", "Type", "Status", "Premiered", "Genre" }.Contains(filterType);
        }

        private bool IsTextSearchFilterType(string filterType)
        {
            return new List<string> { "Name", "Year", "Studio" }.Contains(filterType);
        }

        private async Task PopulateFilterValues(string filterType)
        {
            cmbFilterValues.Items.Clear();

            var values = filterType switch
            {
                "Rating" => new string[] { "G", "PG", "PG-13", "R", "R-17+" },
                "Country" => new string[] { "China", "Japan" },
                "Language" => new string[] { "Sub", "Dub" },
                "Type" => new string[] { "Movie", "TV", "OVA", "ONA", "Special", "Music" },
                "Status" => new string[] { "Not Yet Aired", "Currently Airing", "Finished Airing" },
                "Premiered" => new string[] { "Winter", "Spring", "Summer", "Fall" },
                "Genre" => new string[]
                {
                    "Action", "Adventure", "Avant Grade", "Boys Love", "Comedy", "Demons",
                    "Drama", "Ecchi", "Fantasy", "Girls Love", "Gourmet", "Harem", "Horror",
                    "Isekai", "Iyashikei", "Josei", "Kids", "Magic", "Mahou Shoujo",
                    "Martial Arts", "Mecha", "Military", "Music", "Mystery", "Parody",
                    "Psychological", "Reverse Harem", "Romance", "School", "Sci-Fi",
                    "Seinen", "Shoujo", "Shounen", "Slice Of Life", "Space", "Sports",
                    "Super Power", "Supernatural", "Suspense", "Thriller", "Vampire"
                },
                _ => throw new NotImplementedException($"Filter type '{filterType}' is not implemented.")
            };

            cmbFilterValues.Items.AddRange(values);
            cmbFilterValues.SelectedIndex = 0;
        }

        private async Task RefreshAnimeAsync()
        {
            List<Anime> anime = await _animeFormLogic.RefreshAnimesAsync();
            List<Tuple<Anime, object>> transformedAnimes = _animeFormLogic.TransformAnimesForDataGridView(anime);

            dgvAnime.DataSource = transformedAnimes.Select(t => t.Item2).ToList();
            dgvAnime.Tag = transformedAnimes;

        }

        private void PerformSearch()
        {
            dgvTimer?.Dispose();
            dgvTimer = new System.Threading.Timer(async _ =>
            {
                string filterType = cmbFilterOptions.SelectedItem?.ToString() ?? "All Anime";
                string searchTerm = filterType switch
                {
                    "Name" or "Year" or "Studio" => txtSearch.Texts.Trim(),
                    _ => cmbFilterValues.SelectedItem?.ToString() ?? string.Empty,
                };

                if (filterType == "All Anime")
                {
                    filterType = string.Empty;
                }

                List<Anime> filteredAnimes = await _animeFormLogic.UpdateAnimeListAsync(filterType, searchTerm);
                this.Invoke(new Action(() =>
                {
                    List<Tuple<Anime, object>> transformedAnimes = _animeFormLogic.TransformAnimesForDataGridView(filteredAnimes);
                    dgvAnime.DataSource = transformedAnimes.Select(t => t.Item2).ToList();
                    dgvAnime.Tag = transformedAnimes;
                }));
            }, null, 100, Timeout.Infinite);
        }

        private async Task PerformSearchAndUpdateSuggestions()
        {
            string searchTerm = txtSearch.Texts;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                string searchType = DetermineSearchType();
                var suggestions = await _animeFormLogic.GetSearchSuggestionsAsync(searchType, searchTerm);

                lbSuggestion.Text = string.Join(Environment.NewLine, suggestions);
                lbSuggestion.Visible = suggestions.Any();
            }
            else
            {
                lbSuggestion.Visible = false;
            }
        }

        #endregion

        #region STYLE

        public static class MessageConstants
        {
            public const string NoAnimeSelectedToEdit = "No anime selected. Please select an anime to edit.";
            public const string NoAnimeSelectedToDelete = "No anime selected. Please select an anime to delete.";
            public const string DeleteConfirmation = "Confirmation";
            public const string Success = "Success";
            public const string Warning = "Warning";
            public const string Error = "Error";
            public const string NoAnimeSelected = "No anime selected.";
            public const string ErrorOccurredDeleting = "An error occurred while deleting the anime.";
            public const string ErrorOccurredPerformingAction = "An error occurred while performing the action.";
        }

        private async void Anime_Load(object sender, EventArgs e)
        {
            dgvAnime.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(DgvAnime_DataBindingComplete);
            await RefreshAnimeAsync();
        }

        private void InitializeDataGridViewStyles()
        {
            #region COLORS DATAGRID
            dgvAnime.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 34, 83);
            dgvAnime.DefaultCellStyle.SelectionForeColor = dgvAnime.DefaultCellStyle.ForeColor;
            dgvAnime.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 34, 83);
            dgvAnime.RowHeadersDefaultCellStyle.SelectionForeColor = dgvAnime.RowHeadersDefaultCellStyle.ForeColor;
            dgvAnime.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvAnime.AdvancedRowHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
            dgvAnime.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvAnime.AdvancedColumnHeadersBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.Single;
            dgvAnime.BackgroundColor = Color.FromArgb(231, 34, 83);
            dgvAnime.GridColor = Color.FromArgb(11, 7, 17);
            dgvAnime.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(231, 34, 83);
            dgvAnime.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(11, 7, 17);
            dgvAnime.DefaultCellStyle.ForeColor = Color.White;
            dgvAnime.DefaultCellStyle.BackColor = Color.FromArgb(11, 7, 17);
            dgvAnime.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvAnime.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAnime.EnableHeadersVisualStyles = false;
            dgvAnime.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAnime.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvAnime.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvAnime.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvAnime.AllowUserToResizeRows = false;

            foreach (DataGridViewColumn column in dgvAnime.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dgvAnime.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }

            foreach (DataGridViewRow row in dgvAnime.Rows)
            {
                row.Resizable = DataGridViewTriState.False;
            }
            #endregion
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            dgvTimer?.Dispose();
            suggestionTimer?.Dispose();
        }

        private void ShowAnimeAddEditForm(Anime_Add_Edit.FormMode mode)
        {
            Anime_Add_Edit form = new Anime_Add_Edit(
                mode,
                _appModel,
                _exceptionHandlingService,
                _errorLoggingService);

            form.FormClosed += async (s, args) =>
            {
                await RefreshAnimeAsync();
                ResetFiltersAndCombobox();
            };
            form.ShowDialog();
        }

        private void ResetFiltersAndCombobox()
        {
            if (cmbFilterOptions.Items.Count > 0)
            {
                cmbFilterOptions.SelectedIndex = 0;
            }

            txtSearch.Text = "";
            if (cmbFilterValues.Items.Count > 0)
            {
                cmbFilterValues.SelectedIndex = 0;
            }
        }

        private void DgvAnime_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var columnsToHide = new[] { "Id", "Country", "Language", "Year", "Studio", "Rating", "Type", "Status", "Premiered" };
            foreach (var columnName in columnsToHide)
            {
                if (dgvAnime.Columns.Contains(columnName))
                {
                    dgvAnime.Columns[columnName].Visible = false;
                }
            }
        }

        #endregion
    }
}
