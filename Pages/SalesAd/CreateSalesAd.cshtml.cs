using LTKGMaster.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LTKGMaster.Models.SalesAd;
using LTKGMaster.Models.Products;

namespace LTKGMaster.Pages.SalesAd
{
    public class CreateSalesAdModel : PageModel
    {
        private readonly SalesAdRepository _salesAdRepository;

        public CreateSalesAdModel(SalesAdRepository salesAdRepository)
        {
            _salesAdRepository = salesAdRepository;
        }

        [BindProperty]
        public Laptop Laptop { get; set; }

        [BindProperty]
        public ISalesAd NewSalesAd{ get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); 
            } 
            _salesAdRepository.Add(NewSalesAd);

            return RedirectToPage("/SalesAd/Index");
        }
    }
}
