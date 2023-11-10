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
using AniX_Shared.DomainModels;
using AniX_Utility;

namespace AniX_APP.Forms_Utility
{
    public partial class Anime_Add_Edit : Form
    {
        public enum FormMode { Add, Edit }
        private readonly FormMode _currentMode;
        private readonly ApplicationModel _appModel;
        private readonly AnimeAddEditFormLogic _animeAddEditFormLogic;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;

        public Anime_Add_Edit(
            FormMode mode,
            ApplicationModel appModel,
            IExceptionHandlingService exceptionHandlingService,
            IErrorLoggingService errorLoggingService)
        {
            InitializeComponent();
            _currentMode = mode;
            _appModel = appModel;
            _animeAddEditFormLogic = new AnimeAddEditFormLogic(_appModel);
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void Anime_Add_Edit_Load(object sender, EventArgs e)
        {
            SetupForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            Anime anime = CreateAnimeFromForm();
            int genreCount = genreList.CheckedItems.Count;

            var validationOutcome = await _animeAddEditFormLogic.ValidateAnimeFormAsync(
                anime.Name,
                anime.Country,
                anime.Language,
                anime.Season,
                anime.Episodes.HasValue ? anime.Episodes.Value.ToString() : string.Empty,
                anime.Studio,
                cbxRating.SelectedItem,
                cbxType.SelectedItem,
                cbxStatus.SelectedItem,
                cbxPremiered.SelectedItem,
                anime.TrailerLink,
                anime.CoverImage,
                anime.Thumbnail,
                anime.Description,
                genreCount,
                _currentMode == FormMode.Edit
            );

            if (validationOutcome.IsValid)
            {
                try
                {
                    List<int> genreIds = CollectGenreIds();
                    OperationResult operationResult;

                    if (_currentMode == FormMode.Add)
                    {
                        operationResult = await _animeAddEditFormLogic.AddNewAnimeAsync(anime, genreIds);
                    }
                    else
                    {
                        operationResult = await _animeAddEditFormLogic.UpdateExistingAnimeAsync(anime, genreIds);
                    }

                    RJMessageBox.Show(operationResult.Message);
                    if (operationResult.Success)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    bool handled = await _exceptionHandlingService.HandleExceptionAsync(ex);
                    if (!handled)
                    {
                        await _errorLoggingService.LogErrorAsync(ex, LogSeverity.Critical);
                    }
                    RJMessageBox.Show("An error occurred. Please try again.");
                }
                finally
                {
                    this.Close();
                }
            }
            else
            {
                RJMessageBox.Show(validationOutcome.Message);
            }
        }

        #region DRAG FORM
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

        #region EVENTS

        private async void SetupForm()
        {
            PopulateRatings();
            PopulateCountries();
            PopulateLanguage();
            PopulateTypes();
            PopulateStatus();
            PopulatePremiered();
            await PopulateGenres();

            if (_currentMode == FormMode.Edit && _appModel.AnimeToEdit != null)
            {
                tbxName.Texts = _appModel.AnimeToEdit.Name;
                cbxCountry.SelectedItem = _appModel.AnimeToEdit.Country;
                cbxLanguage.SelectedItem = _appModel.AnimeToEdit.Language;
                tbxSeason.Texts = _appModel.AnimeToEdit.Season;
                tbxEpisodes.Texts = _appModel.AnimeToEdit.Episodes.ToString();
                tbxStudio.Texts = _appModel.AnimeToEdit.Studio;
                dpYear.Value = (_appModel.AnimeToEdit.Year.HasValue && _appModel.AnimeToEdit.Year.Value > DateTime.MinValue.Year) ? new DateTime(_appModel.AnimeToEdit.Year.Value, 1, 1) : DateTime.Now;
                cbxRating.SelectedItem = _appModel.AnimeToEdit.Rating;
                cbxType.SelectedItem = _appModel.AnimeToEdit.Type;
                cbxStatus.SelectedItem = _appModel.AnimeToEdit.Status;
                cbxPremiered.SelectedItem = _appModel.AnimeToEdit.Premiered;
                dpAired.Value = _appModel.AnimeToEdit.Aired ?? DateTime.Now;
                dpRelease.Value = _appModel.AnimeToEdit.ReleaseDate ?? DateTime.Now;
                tbxTrailer.Texts = _appModel.AnimeToEdit.TrailerLink;
                tbxBanner.Texts = _appModel.AnimeToEdit.CoverImage;
                tbxThumbnail.Texts = _appModel.AnimeToEdit.Thumbnail;
                tbxDescription.Texts = _appModel.AnimeToEdit.Description;
                foreach (var genre in _appModel.AnimeToEdit.Genres)
                {
                    for (int i = 0; i < genreList.Items.Count; i++)
                    {
                        if (((Genre)genreList.Items[i]).Id == genre.Id)
                        {
                            genreList.SetItemChecked(i, true);
                        }
                    }
                }

                lbAnime.Text = "Edit Anime";
            }
            else
            {
                lbAnime.Text = "Create Anime";
            }
        }

        private Anime CreateAnimeFromForm()
        {
            var anime = new Anime
            {
                Name = tbxName.Texts,
                Country = cbxCountry.SelectedItem.ToString(),
                Language = cbxLanguage.SelectedItem.ToString(),
                Season = tbxSeason.Texts,
                Episodes = int.TryParse(tbxEpisodes.Texts, out int episodes) ? episodes : (int?)null,
                Studio = tbxStudio.Texts,
                Year = dpYear.Value.Year,
                Rating = cbxRating.SelectedItem.ToString(),
                Type = cbxType.SelectedItem.ToString(),
                Status = cbxStatus.SelectedItem.ToString(),
                Premiered = cbxPremiered.SelectedItem.ToString(),
                Aired = dpAired.Value,
                ReleaseDate = dpRelease.Value,
                TrailerLink = tbxTrailer.Texts,
                CoverImage = tbxBanner.Texts,
                Thumbnail = tbxThumbnail.Texts,
                Description = tbxDescription.Texts,
                Genres = genreList.CheckedItems.Cast<Genre>().ToList()
            };

            if (_currentMode == FormMode.Edit && _appModel.AnimeToEdit != null)
            {
                anime.Id = _appModel.AnimeToEdit.Id;
            }

            return anime;
        }

        private List<int> CollectGenreIds()
        {
            var selectedGenreIds = new List<int>();

            foreach (object item in genreList.CheckedItems)
            {
                var genre = (Genre)item;
                selectedGenreIds.Add(genre.Id);
            }

            return selectedGenreIds;
        }

        private void PopulateRatings()
        {
            cbxRating.Items.AddRange(new string[] { "G", "PG", "PG-13", "R", "R-17+" });
            cbxRating.SelectedIndex = 0;
        }

        private void PopulateCountries()
        {
            cbxCountry.Items.AddRange(new string[] { "China", "Japan" });
            cbxCountry.SelectedIndex = 0;
        }

        private void PopulateLanguage()
        {
            cbxLanguage.Items.AddRange(new string[] { "Sub", "Dub" });
            cbxLanguage.SelectedIndex = 0;
        }

        private void PopulateTypes()
        {
            cbxType.Items.AddRange(new string[] { "Movie", "TV", "OVA", "ONA", "Special", "Music" });
            cbxType.SelectedIndex = 0;
        }

        private void PopulateStatus()
        {
            cbxStatus.Items.AddRange(new string[] { "Not Yet Aired", "Currently Airing", "Finished Airing" });
            cbxStatus.SelectedIndex = 0;
        }

        private void PopulatePremiered()
        {
            cbxPremiered.Items.AddRange(new string[] { "Winter", "Spring", "Summer", "Fall" });
            cbxPremiered.SelectedIndex = 0;
        }

        private async Task PopulateGenres()
        {
            try
            {
                var genres = await _appModel.AnimeController.GetAllGenresAsync();
                foreach (var genre in genres)
                {
                    genreList.Items.Add(genre);
                }
            }
            catch (Exception ex)
            {
                await _errorLoggingService.LogErrorAsync(ex);
                RJMessageBox.Show("Failed to load genres. " + ex.Message);
            }
        }

        #endregion

    }
}
