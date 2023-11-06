using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniX_Shared.DomainModels
{
    public class AnimeWithViewCount : Anime
    {
        public int ViewCount { get; set; }
    }
}
