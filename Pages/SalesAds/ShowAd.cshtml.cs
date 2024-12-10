using LTKGMaster.Models;
using LTKGMaster.Models.SalesAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Pages.SalesAds
{

    //Work in progress. Der skal tilføjes ting i HTML'en til de enkelte produktiformationer og der skal laves en billede karousel
    public class ShowAdModel : PageModel
    {
        [BindProperty]
        public SalesAd AdForShow {  get; set; }

        private readonly SalesAdHandler _salesAdHandler;

        public ShowAdModel(SalesAdHandler salesAdHandler)
        {
            _salesAdHandler = salesAdHandler;
        }

        public IActionResult OnGet(int id, ProductType type)
        {
            AdForShow = _salesAdHandler.Get(id, type);
            return Page();
        }
    }
}
