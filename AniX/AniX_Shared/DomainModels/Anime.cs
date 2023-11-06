using Anix_Shared.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniX_Shared.Enumerations;

namespace AniX_Shared.DomainModels
{
    public class Anime
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string TrailerLink { get; set; }
        public string Country { get; set; }
        public string Season { get; set; }
        public int? Episodes { get; set; } 
        public string Studio { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Premiered { get; set; }
        public DateTime? Aired { get; set; }
        public string CoverImage { get; set; }
        public string Thumbnail { get; set; }
        public string Language { get; set; }
        public string Rating { get; set; }
        public int? Year { get; set; }

        public List<Review> Reviews { get; set; }
        public List<Genre> Genres { get; set; }
        public List<User> WatchedByUsers { get; set; }
    }
}
