using System.Net;

namespace LTKGMaster.Models.Users
{
    public class Credential
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ?PasswordHash { get; set; }
        public bool RememberMe { get; set; }

        public Credential(string email, string passwordHash) :this()
        { 
            Email = email;
            PasswordHash = passwordHash;
        }

        public Credential() 
        { 
        
        }
    }
}
