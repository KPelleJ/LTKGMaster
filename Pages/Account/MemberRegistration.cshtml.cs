using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Pages.Account
{
    //Author Kasper
    public class MemberRegistrationModel : PageModel
    {
        private readonly UserRegistration _userRegistration;

        [BindProperty]
        public RegularUser User { get; set; }

        public MemberRegistrationModel(UserRegistration userRegistration)
        {
            _userRegistration = userRegistration;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _userRegistration.CreateUser(User);
            return RedirectToPage("/Index");
        }
    }
}
