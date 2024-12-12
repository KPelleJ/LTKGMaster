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
            try
            {
                int userId = int.Parse(User.FindFirst("Id").Value);
                _salesAdList = _salesAdHandler.GetSalesAdsFromUser(userId);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, "Der opstod en fejl under indlæsningen af dine annoncer, prøv venligst igen");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Der er opstået en uventet fejl");
            }
        }

        public IActionResult Onpost()
        {
            try
            {
                _salesAdHandler.DeleteSalesAd(Id);
                return RedirectToPage("/Account/MySalesAds");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, "Der opstod en fejl under sletningen af din annonce, prøv venligst igen");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Der er opstået en uventet fejl");
                return Page();
            }
        }
    }
}
