using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LTKGMaster.Pages
{
    public class ImageTestingModel : PageModel
    {
        private readonly PictureRepository _pictureRepository;


        public ProductPicture ProductPicture { get; set; }

        public List<ProductPicture> ProductPictureList { get; set; }

        [BindProperty]
        public List<IFormFile> Images { get; set; }

        public ImageTestingModel(PictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public void OnGet()
        {
            ProductPictureList = _pictureRepository.GetAll(38);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please select an image to upload.");
                return Page();
            }

            foreach (var picture in Images)
            {
                _pictureRepository.Add(ProductPictureConverter.ConvertToByteArray(picture));
            }

            return Page();
        }


    }
}
