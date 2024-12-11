using LTKGMaster.Models;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Authentication;
using System.Security.Claims;

namespace LTKGMaster.Pages.Account
{
    //Author Kasper
    public class LoginModel : PageModel
    {
        private readonly ILogin _loginservice;
        private readonly IAccountRepository _accountRepository;

        [BindProperty]
        public Credential Credential { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }

        public LoginModel(ILogin loginservice, IAccountRepository accountRepository) 
        { 
            _loginservice = loginservice;
            _accountRepository = accountRepository;
        }

        public async Task<IActionResult> OnPost()
        {
            if (Credential.Email is null || Credential.Password is null)
            {
                ModelState.AddModelError(string.Empty, "Email og password felterne skal udfyldes");
                return Page();
            }

            try
            {
                if (_loginservice.UserLogin(Credential))
                {
                    return await CookieAuthorizationAsync(Credential);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email eller kodeord var forkert, prøv venligst igen");
                    return Page();
                }
            }
            catch (InvalidCredentialException ex)
            {
                ModelState.AddModelError(string.Empty, "Der skete en fejl under valideringen af dine oplysninger, prøv venligst igen");
                return Page();
            }
        }

        private async Task<IActionResult> CookieAuthorizationAsync(Credential credential)
        {
            try
            {
                IUser user = _accountRepository.Get(credential.Email);

                //Creating security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email,user.CredMail),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("SignUpDate",user.SignUpDate.ToString()),
                    new Claim("Rating",user.Rating.ToString()),
                    new Claim("City", user.City)
                };

                var identity = new ClaimsIdentity(claims, CookieConstants.CookieName);
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = RememberMe
                };

                await HttpContext.SignInAsync(CookieConstants.CookieName, principal, authProperties);

                return RedirectToPage("/Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError (string.Empty, "Der skete en fejl under login, prøv venligst igen");
                return Page();
            }
        }


    }
}
