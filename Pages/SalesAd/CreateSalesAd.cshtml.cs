using LTKGMaster.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LTKGMaster.Models.SalesAd;
using LTKGMaster.Models.Products;
using Microsoft.AspNetCore.Authorization;

namespace LTKGMaster.Pages.SalesAd
{
    [Authorize]
    public class CreateSalesAdModel : PageModel
    {
        private readonly SalesAdHandler _salesAdHandler;
        public CreateSalesAdModel(SalesAdHandler salesAdHandler)
        {
            _salesAdHandler = salesAdHandler;
        }
        
        
        [BindProperty]
        public Laptop Laptop { get; set; }

        [BindProperty]
        public SalesAds NewSalesAd { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (NewSalesAd._user.City == null && NewSalesAd._user.Rating == null)
            {
                return Page();
            }

            _salesAdHandler.Add(Laptop, NewSalesAd);

            return RedirectToPage("/Index");
        }
    }
}
