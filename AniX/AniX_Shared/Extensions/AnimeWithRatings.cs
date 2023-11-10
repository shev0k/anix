using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniX_Shared.DomainModels;

namespace AniX_Shared.Extensions
{
    public class AnimeWithRatings : Anime
    {
        public int NumberOfReviews { get; set; }
        public float? AverageRating { get; set; }
    }
}
