namespace Anix_Shared.DomainModels
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        private string _password;
        private string _salt;
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Banned { get; set; }
        public bool IsAdmin { get; set; }
        public string ProfileImagePath { get; set; }

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
