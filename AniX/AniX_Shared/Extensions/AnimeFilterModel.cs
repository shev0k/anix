namespace AniX_Shared.Extensions;

public class AnimeFilterModel
{
    public List<int> GenreIds { get; set; } = new List<int>();
    public string SearchQuery { get; set; }
    public List<string> Countries { get; set; } = new List<string>();
    public List<string> Premiered { get; set; } = new List<string>();
    public List<int> Years { get; set; } = new List<int>();
    public List<string> Types { get; set; } = new List<string>();
    public List<string> Statuses { get; set; } = new List<string>();    
    public List<string> Languages { get; set; } = new List<string>();
    public List<string> Ratings { get; set; } = new List<string>();
    public SortCriteria SortBy { get; set; } = SortCriteria.None;

    public AnimeFilterModel()
    {
    }
}