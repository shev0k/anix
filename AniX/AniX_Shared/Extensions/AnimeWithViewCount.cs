using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniX_Shared.DomainModels;

namespace AniX_Shared.Extensions
{
    public class AnimeWithViewCount : Anime
    {
        public int ViewCount { get; set; }
    }
}
