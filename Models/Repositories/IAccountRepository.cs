using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.Repositories
{
    /// <summary>
    /// Defines methods to be used for handling data related to the User accounts.
    /// </summary>
    public interface IAccountRepository
    {
        void Add(IUser account);

        void Update(IUser account);

        IUser Get(string email);

        IUser GetById(int id);
    }
}