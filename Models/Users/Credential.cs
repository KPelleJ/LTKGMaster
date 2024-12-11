using System.ComponentModel.DataAnnotations;
using System.Net;

namespace LTKGMaster.Models.Users
{
    /// <summary>
    /// Contains information used for user creation and login operations.
    /// </summary>
    public class Credential
    {
        /// <summary>
        /// A unique email address to be used by the user
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ@._-]+$", ErrorMessage = "The Email field contains invalid characters.")]
        [Length(5, 125, ErrorMessage = "Your Email must be between 5 and 125 characters")]
        public string Email { get; set; }

        /// <summary>
        /// The password input before hashing
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Length(5,40, ErrorMessage = "Your Password must be between 5 and 125 characters")]
        public string Password { get; set; }

        /// <summary>
        /// The second password input to which is checked against the first
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// The hashed password to be used for validation
        /// </summary>
        public string ?PasswordHash { get; set; }

        public Credential() 
        { 
        
        }
    }
}
