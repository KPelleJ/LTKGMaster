namespace LTKGMaster.Models.Users
{
    public class Account : IAccount
    {
        public IUser User { get; set; }
        public ICredential Credential { get; set; }

        public Account(IUser user, ICredential credential)
        {
            User = user;
            Credential = credential;
        }
    }
}
