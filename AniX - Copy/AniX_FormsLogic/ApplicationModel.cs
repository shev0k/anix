using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniX_Controllers;
using Anix_Shared.DomainModels;
using AniX_DAL;

namespace AniX_FormsLogic
{
    public class ApplicationModel
    {
        public User LoggedInUser { get; set; }
        public User UserToEdit { get; set; }
        public UserController UserController { get; set; }
        public UserDAL UserDal { get; set; }
        public ApplicationModel(User loggedInUser, UserController userController, UserDAL userDal)
        {
            LoggedInUser = loggedInUser;
            UserController = userController;
            UserDal = userDal;
        }
    }
}
