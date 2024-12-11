using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Pages.Account
{
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

            try
            {
                _userRegistration.CreateUser(User);
                return RedirectToPage("/Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, "An error occured while creating the user");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                return Page();
            }
        }
    }
}
