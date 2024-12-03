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
        public List<SalesAds> laptops { get; set; }
        public SalesAds SalesAds { get; set; }
        [BindProperty]
        public int CategoryId {  get; set; }
        public ShowLaptopModel(ISalesAdRepository salesAdRepository)
        {
            _salesAdRepository = salesAdRepository;
        }
        public void OnGet()
        {
            laptops = _salesAdRepository.GetAllLaptops();
        }
    }
}
