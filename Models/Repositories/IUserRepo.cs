using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.Repositories
{
    public interface IUserRepo<T>
    {
        void Add(T output);
    }
}