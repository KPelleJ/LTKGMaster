using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Pages.Account
{
    public class MemberRegistrationModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        [BindProperty]
        public RegularUser User { get; set; }

        public MemberRegistrationModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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

            _accountRepository.Add(User);
            return RedirectToPage("/Index");
        }
    }
}
