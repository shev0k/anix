using Anix_Shared.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniX_Shared.DomainModels
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AnimeId { get; set; }
        public Anime Anime { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
