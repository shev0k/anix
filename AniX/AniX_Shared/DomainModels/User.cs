using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anix_Shared.DomainModels
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        private string _password;
        private string _salt;
        [Required, EmailAddress]
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Banned { get; set; }
        public bool IsAdmin { get; set; }

        public void UpdatePassword(string newPassword, string newSalt)
        {
            _password = newPassword;
            _salt = newSalt;
        }

        public (string Password, string Salt) RetrieveCredentials()
        {
            return (_password, _salt);
        }
    }
}
