using LTKGMaster.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILogin _loginservice;

        [BindProperty]
        public Credential Credential { get; set; }

        public LoginModel(ILogin loginservice) 
        { 
            _loginservice = loginservice;
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

            return await _loginservice.UserLogin(Credential);
        }
    }
}
