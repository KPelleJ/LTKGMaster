using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.SalesAds;
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
        public SalesAd salesAd { get; set; }
        public Product product { get; set; }
        private readonly SalesAdHandler _salesAdHandler;
        public List<SalesAd> _salesAdList {  get; set; }
        [BindProperty]
        public int Id { get; set; }
        public MySalesAdsModel(SalesAdHandler salesAdHandler)
        {
            _salesAdHandler = salesAdHandler;
        }
        public void OnGet()
        {
            int userId = int.Parse(User.FindFirst("Id").Value);
            _salesAdList = _salesAdHandler.GetSalesAdsFromUser(userId);
        }
        public IActionResult Onpost()
        {
            Debug.WriteLine(Id);
            _salesAdHandler.DeleteSalesAd(Id);
            return RedirectToPage("/Account/MySalesAds");
        }
    }
}
