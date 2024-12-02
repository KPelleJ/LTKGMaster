using LTKGMaster.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Models.Users
{
    //Author Kasper
    public class LoginWithAuthentication:ILogin
    {
        private readonly IAccountRepository _accountRepository;

        public LoginWithAuthentication(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public bool UserLogin(Credential credential)
        {
            //User objekter til at holde information fra database
            var user = _accountRepository.Get(credential.Email);

            if (user.CredMail != null && credential.Email == user.CredMail && BCrypt.Net.BCrypt.EnhancedVerify(credential.Password, user.Credential.PasswordHash))
            {
                return true;
            }

            return false;

        }
    }
}
