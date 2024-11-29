using System.ComponentModel.DataAnnotations;
using System.Net;

namespace LTKGMaster.Models.Users
{
    //Author Kasper
    public class Credential
    {
        [Required]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ@._-]+$", ErrorMessage = "The Email field contains invalid characters.")]
        [Length(5, 125, ErrorMessage = "Your Email must be between 5 and 125 characters")]
        public string Email { get; set; }

        [Required]
        [Length(5,40, ErrorMessage = "Your Password must be between 5 and 125 characters")]
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
