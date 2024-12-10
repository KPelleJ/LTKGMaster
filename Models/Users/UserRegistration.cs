using LTKGMaster.Models.Repositories;

namespace LTKGMaster.Models.Users
{
    /// <summary>
    /// Handles actions related to the creation of user accounts
    /// </summary>
    public class UserRegistration
    {
        private readonly IAccountRepository _accountRepository;

        public UserRegistration(IAccountRepository accountRepository)
        {  
            _accountRepository = accountRepository; 
        }

        /// <summary>
        /// Creates a IUser object and then encrypts the password using BCrypt. The IUser object is then sent to the data layer.
        /// </summary>
        /// <param name="user"></param>
        public void CreateUser(IUser user)
        {
            IUser output = user;

            //The users password input in string format is salted and hashed by using BCrypt.
            output.Credential.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Credential.Password, 12);

            _accountRepository.Add(output);
        }
    }
}
