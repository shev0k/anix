using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniX_Controllers;
using Anix_Shared.DomainModels;
using AniX_DAL;
using AniX_Shared.DomainModels;

namespace AniX_FormsLogic
{
    public class ApplicationModel
    {
        public User LoggedInUser { get; set; }
        public User UserToEdit { get; set; }
        public UserController UserController { get; set; }
        public UserDAL UserDal { get; set; }

        public Anime AnimeToEdit { get; set; }
        public AnimeController AnimeController { get; set; }
        public AnimeDAL AnimeDal { get; set; }

        public ApplicationModel(
            UserController userController, UserDAL userDal,
            AnimeController animeController, AnimeDAL animeDal)
        {
            UserController = userController;
            UserDal = userDal;
            AnimeController = animeController;
            AnimeDal = animeDal;
        }
    }
}
