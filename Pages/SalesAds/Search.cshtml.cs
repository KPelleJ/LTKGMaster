using LTKGMaster.Models.SalesAds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Pages.SalesAds
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        public List<SalesAd> SearchResults { get; set; }

        [BindProperty]
        public string SearchQuery { get; set; }

        private readonly SalesAdHandler _salesAdHandler;

        public SearchModel(SalesAdHandler salesAdHandler)
        {
            _salesAdHandler = salesAdHandler;
        }

        public IActionResult OnGet(string searchQuery)
        {
            SearchQuery = searchQuery;
            SearchResults = _salesAdHandler.GetSearchResults(SearchQuery);
            return Page();
        }
    }
}
