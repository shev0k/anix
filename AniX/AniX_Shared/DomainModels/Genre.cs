using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniX_Shared.DomainModels
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Anime> Animes { get; set; }

    }
}
