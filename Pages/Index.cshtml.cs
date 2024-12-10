using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.SalesAds;
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
        private readonly ProductPictureConverter _pictureConverter;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ISalesAdRepository salesAdRepository, IPictureRepository pictureRepository, ILogger<IndexModel> logger, ProductPictureConverter pictureConverter)
        {
            _salesAdRepository = salesAdRepository;
            _logger = logger;
            _pictureRepository = pictureRepository;
            _pictureConverter = pictureConverter;
        }

        public List<SalesAd> SalesAds { get; set; }


        public void OnGet()
        {
           
            //!!!!!!!!!!!!!!ATTENZIONE!!!!!!!!!!!!
            //Det her skal flyttes in i salesAdHandler
            var salesAds = _salesAdRepository.GetAll();
            List<SalesAd> output = new List<SalesAd>();

            foreach (var ad in salesAds)
            {
                ad.ProductPictures = _pictureRepository.GetAll(ad.ProdId);
                ad.ProductPictures = _pictureConverter.ByteArrayToBase64(ad.ProductPictures);
                output.Add(ad);
            }

            Debug.WriteLine("Stop");

            SalesAds = output;
        }
    }
}
