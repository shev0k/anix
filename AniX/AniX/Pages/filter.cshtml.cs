using AniX_Shared.DomainModels;
using AniX_Shared.Extensions;
using AniX_Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AniX_WEB.Pages
{
    public class FilterListModel : PageModel
    {
        private readonly IAnimeManagement _animeManagement;

        public List<AnimeWithRatings> Animes { get; set; }
        public List<string> AvailableCountries { get; } = new List<string> { "Japan", "China"};
        
        [BindProperty]
        public bool IsGridView { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 18;
        public int TotalCount { get; private set; }
        public List<Genre> AvailableGenres { get; set; }

        [BindProperty]
		public AnimeFilterModel Filter { get; set; } = new AnimeFilterModel();


        public IEnumerable<int> GetPagination()
        {
            int totalPages = (int)Math.Ceiling((double)TotalCount / PageSize);
            return Enumerable.Range(1, totalPages);
        }

        public FilterListModel(IAnimeManagement animeManagement)
        {
            _animeManagement = animeManagement;
        }
        public async Task OnGetAsync(
            bool gridView = false,
            int currentPage = 1,
            int[] genreIds = null,
            string[] countries = null,
            string[] premiered = null,
            int[] years = null,
            string[] types = null,
            string[] statuses = null,
            string[] languages = null,
            string[] ratings = null,
            SortCriteria sortBy = SortCriteria.None,
            string searchQuery = null)


        {
            CurrentPage = currentPage;
            IsGridView = gridView;
            Filter = new AnimeFilterModel
            {
                GenreIds = genreIds?.ToList() ?? new List<int>(),
                Countries = countries?.ToList() ?? new List<string>(),
                Premiered = premiered?.ToList() ?? new List<string>(),
                Years = years?.ToList() ?? new List<int>(),
                Types = types?.ToList() ?? new List<string>(),
                Statuses = statuses?.ToList() ?? new List<string>(),
                Languages = languages?.ToList() ?? new List<string>(),
                Ratings = ratings?.ToList() ?? new List<string>(),
                SortBy = sortBy,
                SearchQuery = searchQuery
            };

            AvailableGenres = await _animeManagement.GetAllGenresAsync();
            var result = await _animeManagement.GetFilteredAnimesAsync(Filter, CurrentPage, PageSize);
            Animes = result.Animes;
            TotalCount = result.TotalCount;
        }

        public async Task<IActionResult> OnPostApplyFiltersAsync()
        {
            AvailableGenres = await _animeManagement.GetAllGenresAsync();

            var result = await _animeManagement.GetFilteredAnimesAsync(Filter, CurrentPage, PageSize);
            Animes = result.Animes;
            TotalCount = result.TotalCount;

            Console.WriteLine($"Filter values: {JsonConvert.SerializeObject(Filter)}");

            return RedirectToPage(new
            {
                gridView = IsGridView,
                currentPage = CurrentPage,
                genreIds = Filter.GenreIds,
                countries = Filter.Countries,
                premiered = Filter.Premiered,
                years = Filter.Years,
                types = Filter.Types,
                statuses = Filter.Statuses,
                languages = Filter.Languages,
                ratings = Filter.Ratings,
                sortBy = Filter.SortBy,
                searchQuery = Filter.SearchQuery
            });
        }

        public string GenerateQueryStringWithFilters(int targetPage)
        {
            var queryParams = new List<string>
    {
        $"currentPage={targetPage}",
        $"gridView={IsGridView}"
    };

            if (Filter.GenreIds != null && Filter.GenreIds.Count > 0)
            {
                queryParams.Add($"genreIds={string.Join(",", Filter.GenreIds)}");
            }
            AddFilterParamIfNotEmpty(queryParams, "countries", Filter.Countries);
            AddFilterParamIfNotEmpty(queryParams, "premiered", Filter.Premiered);
            if (Filter.Years != null && Filter.Years.Count > 0)
            {
                queryParams.Add($"years={string.Join(",", Filter.Years)}");
            }
            AddFilterParamIfNotEmpty(queryParams, "types", Filter.Types);
            AddFilterParamIfNotEmpty(queryParams, "statuses", Filter.Statuses);
            AddFilterParamIfNotEmpty(queryParams, "languages", Filter.Languages);
            AddFilterParamIfNotEmpty(queryParams, "ratings", Filter.Ratings);

            if (Filter.SortBy != null)
            {
                queryParams.Add($"sortBy={(int)Filter.SortBy}");
            }

            if (!string.IsNullOrEmpty(Filter.SearchQuery))
            {
                queryParams.Add($"searchQuery={Uri.EscapeDataString(Filter.SearchQuery)}");
            }

            return string.Join("&", queryParams);
        }

        private void AddFilterParamIfNotEmpty(List<string> queryParams, string paramName, IEnumerable<string> filterValues)
        {
            if (filterValues != null && filterValues.Any())
            {
                queryParams.Add($"{paramName}={string.Join(",", filterValues)}");
            }
        }
    }
}
