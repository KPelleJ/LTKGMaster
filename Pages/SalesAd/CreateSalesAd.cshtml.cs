using LTKGMaster.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LTKGMaster.Models.SalesAd;
using LTKGMaster.Models.Products;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;

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

        [BindProperty]
        [Length(1,9)]
        public List<IFormFile> ProductImages { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (NewSalesAd is null)
            {
                return Page();
            }

            if (ProductImages == null || ProductImages.Count < 1)
            {
                ModelState.AddModelError("ProductImages", "You must upload at least one file.");
                return Page();
            }

            if (ProductImages.Count > 9)
            {
                ModelState.AddModelError("ProductImages", "You can only upload up to 9 files.");
                return Page();
            }

            _salesAdHandler.Add(Laptop, NewSalesAd, ProductImages);

            return RedirectToPage("/Index");
        }
    }
}
