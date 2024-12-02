using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models.Users
{
    //Author Kasper
    public class RegularUser:IUser
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ@._-]+$", ErrorMessage = "The Email field contains invalid characters.")]
        [Length(5, 125,ErrorMessage = "Your Email must be between 5 and 125 characters")]
        public string CredMail { get; set; }

        [Required]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ]+$", ErrorMessage = "The Username field contains invalid characters.")]
        [Length(5, 50, ErrorMessage = "Your Username must be between 5 and 50 characters")]
        public string UserName { get; set; }

        public DateTime SignUpDate { get; set; }

        public int Rating { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ -]+$", ErrorMessage = "The City field contains invalid characters.")]
        [Length(1,50, ErrorMessage = "City name must be between 1 and 50 characters")]
        public string City { get; set; }

        [Required]
        public Credential Credential { get; set; }

        public RegularUser(int id, string userName, DateTime signUpDate, string city, Credential credential) : this(userName, city, credential)
        {
            Id = id;
            SignUpDate = signUpDate;
        }

        public RegularUser(string userName, string city, Credential credential) : this()
        {
            UserName = userName;
            City = city;
            Credential = credential;

            CredMail = Credential.Email;
        }

        public RegularUser() 
        {
            Credential = new Credential();
            Rating = 0;
        }
    }
}
