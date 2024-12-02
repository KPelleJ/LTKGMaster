using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.SalesAd;
using LTKGMaster.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace LTKGMaster.Pages.Account
{
    [Authorize]
    public class MySalesAdsModel : PageModel
    {
        public IUser user { get; set; }
        public SalesAds salesAd { get; set; }
        public Product product { get; set; }
        private readonly ISalesAdRepository _salesAdRepository;
        public List<SalesAds> _salesAdList {  get; set; }
        [BindProperty]
        public int Id { get; set; }
        public MySalesAdsModel(ISalesAdRepository salesAdRepository)
        {
            _salesAdRepository = salesAdRepository;
        }
        public void OnGet()
        {
            int userId = int.Parse(User.FindFirst("Id").Value);
            _salesAdList = _salesAdRepository.GetAllFromUser(userId);
        }
        public IActionResult Onpost()
        {
            Debug.WriteLine(Id);
            _salesAdRepository.Delete(Id);
            return RedirectToPage("/Account/MySalesAds");
        }
    }
}
