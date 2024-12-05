using LTKGMaster.Models;
using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.SalesAd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace LTKGMaster.Pages.Categories
{
    public class ShowLaptopModel : PageModel
    {
        private readonly ISalesAdRepository _salesAdRepository;
        private readonly SalesAdHandler _salesAdHandler;

        public List<SalesAds> laptops { get; set; }
        public SalesAds SalesAds { get; set; }
        [BindProperty]
        public int CategoryId {  get; set; }

        public ShowLaptopModel(ISalesAdRepository salesAdRepository, SalesAdHandler salesAdHandler)
        {
            _salesAdRepository = salesAdRepository;
            _salesAdHandler = salesAdHandler;
        }
        //public void OnGet()
        //{
        //    laptops = _salesAdHandler.GetProductsOfType();
        //}
    }
}
