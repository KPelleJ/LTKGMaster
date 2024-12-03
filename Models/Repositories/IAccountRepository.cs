using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.Repositories
{
    //Author Kasper
    public interface IAccountRepository
    {
        void Add(IUser account);

        void Update(IUser account);

        IUser Get(string email);

        IUser GetById(int id);
    }
}