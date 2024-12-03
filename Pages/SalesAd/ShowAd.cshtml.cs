using LTKGMaster.Models.SalesAd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Pages.SalesAd
{

    //Work in progress. Der skal tilføjes ting i HTML'en til de enkelte produktiformationer og der skal laves en billede karousel
    public class ShowAdModel : PageModel
    {
        [BindProperty]
        public SalesAds AdForShow {  get; set; }

        private readonly SalesAdHandler _salesAdHandler;

        public ShowAdModel(SalesAdHandler salesAdHandler)
        {
            _salesAdHandler = salesAdHandler;
        }

        public IActionResult OnGet(int id)
        {
            AdForShow = _salesAdHandler.Get(id);
            return Page();
        }
    }
}
