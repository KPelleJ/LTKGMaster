using LTKGMaster.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Models.Users
{
    public class LoginWithAuthorization:ILogin
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserAuthorization _authorizer;

        public LoginWithAuthorization(IAccountRepository accountRepository, IHttpContextAccessor contextAccesor)
        {
            _accountRepository = accountRepository;
            _authorizer = new UserAuthorization(contextAccesor);
        }

        public async Task<IActionResult> UserLogin(Credential credential)
        {
            //User objekter til at holde information fra database
            var user = _accountRepository.Get(credential.Email);

            //Verificering af om indtastede data er korrekt
            if (user.CredMail != null && credential.Email == user.CredMail && BCrypt.Net.BCrypt.EnhancedVerify(credential.Password, user.Credential.PasswordHash))
            {
                //Authorizer Useren med værdier fra databasen
                await _authorizer.AuthorizeAsync(user);
            }

            return new PageResult();
        }
    }
}
