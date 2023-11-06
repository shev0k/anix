using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniX_Shared.DomainModels
{
    public class AnimeWithRatings : Anime
    {
        public int NumberOfReviews { get; set; }
        public float? AverageRating { get; set; }
    }
}
