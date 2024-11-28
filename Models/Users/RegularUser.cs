namespace LTKGMaster.Models.Users
{
    public class RegularUser:IUser
    {
        public int Id { get; set; }
        public string CredMail { get; set; }
        public string UserName { get; set; }
        public DateTime SignUpDate { get; set; }
        public int Rating { get; set; }
        public string City { get; set; }
        public ICredential Credential { get; set; }

        public RegularUser(int id, string userName, DateTime signUpDate, string city, ICredential credential) : this(userName, city, credential)
        {
            Id = id;
            SignUpDate = signUpDate;
        }

        public RegularUser(string userName, string city, ICredential credential)
        {
            UserName = userName;
            City = city;
            Credential = credential;

            Rating = 0;
            CredMail = Credential.Email;
        }
    }
}
