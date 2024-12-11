using LTKGMaster.Models.Exceptions;
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
                ModelState.AddModelError(string.Empty, "Der skete en fejl under oprettelsen af den nye bruger, prøv venligst igen.");
                return Page();
            }
            catch (EmailAlreadyInUseException ex) 
            { 
                ModelState.AddModelError(string.Empty, $"Denne email: {User.Credential.Email} er allerede i brug");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "En uventet fejl opstod. Prøv venligst igen.");
                return Page();
            }
        }
    }
}
