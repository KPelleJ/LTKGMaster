using LTKGMaster.Models;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Implementering af ny bool metode
            if (_loginservice.UserLogin(Credential))
            {
                return await CookieAuthorizationAsync(Credential);
            }

            return RedirectToPage("/Index");
        }

        private async Task<IActionResult> CookieAuthorizationAsync(Credential credential)
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


    }
}
