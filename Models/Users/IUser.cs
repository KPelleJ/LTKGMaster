namespace LTKGMaster.Models.Users
{
    public interface IUser
    {
        public int Id { get; set; }
        public string CredMail { get; set; }
        public string UserName { get; set; }
        public DateTime SignUpDate { get; set; }
        public int Rating { get; set; }
        public string City { get; set; }
        public ICredential Credential { get; set; }
    }
}
