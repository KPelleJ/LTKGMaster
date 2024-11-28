namespace LTKGMaster.Models.Users
{
    public interface IAccount
    {
        ICredential Credential { get; set; }
        IUser User { get; set; }
    }
}