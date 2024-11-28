using LTKGMaster.Models.Users;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Pages.Account
{
    public class MyPageModel : PageModel
    {
        [BindProperty]
        public IUser user { get; set; }
        public MyPageModel()
        {
            
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            
            return RedirectToPage("Account/MyPage");
        }
    }
}
