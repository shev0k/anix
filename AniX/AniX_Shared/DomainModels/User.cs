using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anix_Shared.DomainModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Banned { get; set; }
        public bool IsAdmin { get; set; }
    }
}
