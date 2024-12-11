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
            try
            {
                IUser output = user;

                output.Credential.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Credential.Password, 12);

                _accountRepository.Add(output);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An error occurred while creating the user.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error in CreateUser method.", ex);
            }
        }
    }
}
