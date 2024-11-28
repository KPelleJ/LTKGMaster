using LTKGMaster.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LTKGMaster.Models.SalesAd;

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
            NewSalesAd.DateOfCreation = DateTime.Now;
            
            _salesAdRepository.Add(NewSalesAd);

            return RedirectToPage("/SalesAd/Index");
        }
    }
}
