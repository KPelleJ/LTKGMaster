using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.SalesAd
{
    public class SalesAdHandler
    {
        private IProductRepository _productRepository;
        private ISalesAdRepository _salesAdRepository;
        private IAccountRepository _accountRepository;
        private readonly IPictureRepository _pictureRepository;

        public SalesAdHandler(IProductRepository productRepository, ISalesAdRepository salesAdRepository, IPictureRepository pictureRepository, IAccountRepository accountRepository)
        {
            _productRepository = productRepository;
            _salesAdRepository = salesAdRepository;
            _pictureRepository = pictureRepository;
            _accountRepository = accountRepository;
        }

        public void Add(Product product, SalesAds salesAd, List<IFormFile> productImages)
        {
            salesAd.ProdId = _productRepository.Add(product).Id;

            foreach (var image in productImages)
            {
                ProductPicture output = ProductPictureConverter.ConvertToByteArray(image);
                output.ProductId = salesAd.ProdId;
                _pictureRepository.Add(output);
            }

            _salesAdRepository.Add(salesAd);
        }

        //!!!!!!!!!!!!!!!!!!!!!!!ATTENZIONE!!!!!!!!!!!!!!!!!!
        //Den her metode kan laves om til en giga lang query hvis det er bedre, vi skal lige høre Camilla ad
        //!!!!!!!!!!!!!!!!!!!!!!!ATTENZIONE!!!!!!!!!!!!!!!!!!
        public SalesAds Get(int id, ProductType type)
        { 
            SalesAds output = _salesAdRepository.GetById(id);
            output._user = _accountRepository.GetById(output.UserId);
            output._product = _productRepository.GetById(id, type);
            output._product.Pictures = ProductPictureConverter.ByteArrayToBase64(_pictureRepository.GetAll(id));
            return output;
        }

        public List<SalesAds> GetAllSalesToUser(int userId)
        {
            return _salesAdRepository.GetAllFromUser(userId);
        }

        public void DeleteSalesAd(int id)
        {
            _pictureRepository.Delete(id);
            _salesAdRepository.Delete(id);
            _productRepository.Delete(id);
        }
    }
}