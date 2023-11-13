using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniX_Shared.DomainModels;

namespace AniX_Shared.Extensions
{
    public class AnimeWithPopularity : Anime
    {
        public int WatchlistCount { get; set; }
        public double? AverageRating { get; set; }
    }
}
