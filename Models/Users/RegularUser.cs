using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models.Users
{
    /// <summary>
    /// Normal users of the service. The class implements the IUSer interface.
    /// </summary>
    public class RegularUser:IUser
    {
        /// <summary>
        /// The UserId of the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The users unique credential email
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ@._-]+$", ErrorMessage = "The Email field contains invalid characters.")]
        [Length(5, 125,ErrorMessage = "Your Email must be between 5 and 125 characters")]
        public string CredMail { get; set; }

        /// <summary>
        /// The users chosen username
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ]+$", ErrorMessage = "The Username field contains invalid characters.")]
        [Length(5, 50, ErrorMessage = "Your Username must be between 5 and 50 characters")]
        public string UserName { get; set; }

        /// <summary>
        /// The date where the user account is created
        /// </summary>
        public DateTime SignUpDate { get; set; }

        /// <summary>
        /// The users rating
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// The users city
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ -]+$", ErrorMessage = "The City field contains invalid characters.")]
        [Length(1,50, ErrorMessage = "City name must be between 1 and 50 characters")]
        public string City { get; set; }

        /// <summary>
        /// The users credential information
        /// </summary>
        [Required]
        public Credential Credential { get; set; }

        /// <summary>
        /// A user is always created with a credential object to hold login information and the user rating is set to 0 as standard.
        /// </summary>
        public RegularUser() 
        {
            Credential = new Credential();
            Rating = 0;
        }
    }
}
