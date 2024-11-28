using LTKGMaster.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Pages.Categories
{
    public class ShowLaptopModel : PageModel
    {
        SalesAdRepository _salesAdRepository{  get; set; }
        public ShowLaptopModel(SalesAdRepository salesAdRepository)
        {
            _salesAdRepository = salesAdRepository;
        }
        public void OnGet()
        {
            
        }
    }
}
