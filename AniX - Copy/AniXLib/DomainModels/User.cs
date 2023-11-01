using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniX_ClassLib.DomainModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Banned { get; set; }
        public bool IsAdmin { get; set; }
    }
}
