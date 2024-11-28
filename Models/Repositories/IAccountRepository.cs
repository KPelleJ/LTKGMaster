using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.Repositories
{
    public interface IAccountRepository
    {
        void Add(IAccount account);
    }
}