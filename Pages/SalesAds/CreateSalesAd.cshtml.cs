using LTKGMaster.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LTKGMaster.Models.SalesAds;
using LTKGMaster.Models.Products;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using LTKGMaster.Models;
using System.Diagnostics;

namespace LTKGMaster.Pages.SalesAds
{
    [Authorize]
    public class CreateSalesAdModel : PageModel
    {
        private readonly SalesAdHandler _salesAdHandler;
        //private readonly ProductFactory _productFactory;

        [BindProperty]
        public ProductType Type { get; set; }

        [BindProperty]
        public StandardProduct ProductToAdd { get; set; } 

        [BindProperty]
        public SalesAd NewSalesAd { get; set; }

        [BindProperty]
        [Length(1, 9)]
        public List<IFormFile> ProductImages { get; set; }

        public CreateSalesAdModel(SalesAdHandler salesAdHandler) //ProductFactory productFactory)
        {
            _salesAdHandler = salesAdHandler;
            //_productFactory = productFactory;
        }
        
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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _salesAdHandler.Add(ProductToAdd, NewSalesAd, ProductImages);
            }
            catch(InvalidOperationException e)
            {
                ModelState.AddModelError(e.Message, "Der skete en fejl ved oprettelse af salgs annonce.");
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
