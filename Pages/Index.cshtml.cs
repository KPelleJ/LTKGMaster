using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.SalesAd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Reflection;

namespace LTKGMaster.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISalesAdRepository _salesAdRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ISalesAdRepository salesAdRepository, IPictureRepository pictureRepository, ILogger<IndexModel> logger)
        {
            _salesAdRepository = salesAdRepository;
            _logger = logger;
            _pictureRepository = pictureRepository;
        }

        public List<SalesAds> SalesAds { get; set; }


        public void OnGet()
        {
           
            var salesAds = _salesAdRepository.GetAll();
            List<SalesAds> output = new List<SalesAds>();

            foreach (var ad in salesAds)
            {
                ad.ProductPictures = _pictureRepository.GetAll(ad.ProdId);
                ad.ProductPictures = ProductPictureConverter.ByteArrayToBase64(ad.ProductPictures);
                output.Add(ad);
            }

            Debug.WriteLine("Stop");

            SalesAds = output;
        }
    }
}
