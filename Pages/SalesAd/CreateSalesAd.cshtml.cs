using LTKGMaster.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LTKGMaster.Models.SalesAd;
using LTKGMaster.Models.Products;

namespace LTKGMaster.Pages.SalesAd
{
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
            if (!ModelState.IsValid)
            {
                return Page(); 
            }
            NewSalesAd.UserId = Int32.Parse(User.FindFirst("Id").ValueType);
            _salesAdHandler.Add(Laptop, NewSalesAd);

            return RedirectToPage("/Index");
        }
    }
}
