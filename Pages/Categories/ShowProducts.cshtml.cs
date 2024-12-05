using LTKGMaster.Models;
using LTKGMaster.Models.SalesAd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Pages.Categories
{
    public class ShowProductsModel : PageModel
    {
        private readonly SalesAdHandler _salesAdHandler;

        [BindProperty]
        public List<SalesAds> Products { get; set; }

        public ProductType[] ProductTypes = (ProductType[])Enum.GetValues(typeof(ProductType));

        public ShowProductsModel(SalesAdHandler salesAdHandler)
        {
            _salesAdHandler = salesAdHandler;
        }
        public IActionResult OnGet(ProductType type)
        {
            if (type == 0)
            {
                Products = new List<SalesAds>();
                return Page();
            }


            Products = _salesAdHandler.GetProductsOfType(type);
            return Page();
        }
    }
}
