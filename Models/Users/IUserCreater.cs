namespace LTKGMaster.Models.Users
{
    public interface IUserCreater
    {
        void Create(ICredential credential, IUser user);
    }
}
