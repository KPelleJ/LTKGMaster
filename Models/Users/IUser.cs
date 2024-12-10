namespace LTKGMaster.Models.Users
{
    /// <summary>
    /// Defines properties related to IUser 
    /// </summary>
    public interface IUser
    {
        public int Id { get; set; }
        public string CredMail { get; set; }
        public string UserName { get; set; }
        public DateTime SignUpDate { get; set; }
        public int Rating { get; set; }
        public string City { get; set; }
        public Credential Credential { get; set; }
    }
}
