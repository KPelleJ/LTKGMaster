using LTKGMaster.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Models.Users
{
    /// <summary>
    /// Used for user login actions with authentication
    /// </summary>
    public class LoginWithAuthentication:ILogin
    {
        private readonly IAccountRepository _accountRepository;

        public LoginWithAuthentication(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Takes a user input and authenticates the user if the input data matches that in the database.
        /// The plain text password is checked using BCrypt to decrypt the data from the sql database and
        /// checking whether it matches the input
        /// </summary>
        /// <param name="credential">The users input containing an email and a password in plain text</param>
        /// <returns>True if the Credential data is correct</returns>
        public bool UserLogin(Credential credential)
        {
            var user = _accountRepository.Get(credential.Email);

            if (user.CredMail != null && credential.Email == user.CredMail && BCrypt.Net.BCrypt.EnhancedVerify(credential.Password, user.Credential.PasswordHash))
            {
                return true;
            }

            return false;

        }
    }
}
