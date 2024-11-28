using System.Net;

namespace LTKGMaster.Models.Users
{
    public class Credential : ICredential
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public bool RememberMe { get; set; }

        public Credential(string email, string passwordHash) 
        { 
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
