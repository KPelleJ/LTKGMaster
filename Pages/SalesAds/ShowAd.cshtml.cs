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

        public IActionResult OnGet(int id)
        {
            try
            {
                AdForShow = _salesAdHandler.Get(id);
                return Page();
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, "Kunne ikke finde salgsannoncen, prøv venligst en anden");
                return Page();
            }
        }
    }
}
