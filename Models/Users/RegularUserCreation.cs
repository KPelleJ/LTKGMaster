using LTKGMaster.Models.Repositories;

namespace LTKGMaster.Models.Users
{
    public class RegularUserCreation : IUserCreater
    {
        private readonly IUserRepo<IUser> _userRepo;
        private readonly IUserRepo<ICredential> _credentialRepo;

        public void Create(ICredential credential, IUser user)
        {
            ICredential outputCreds = new Credential(credential.Email, BCrypt.Net.BCrypt.EnhancedHashPassword(credential.Password,12));
            IUser outputUser = new RegularUser(user.UserName, user.City, credential);

            _credentialRepo.Add(outputCreds);
            _userRepo.Add(outputUser);
        }
    }
}
