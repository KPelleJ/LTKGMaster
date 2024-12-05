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
        private readonly ProductPictureConverter _pictureConverter;

        public SalesAdHandler(IProductRepository productRepository, ISalesAdRepository salesAdRepository, IPictureRepository pictureRepository, IAccountRepository accountRepository, ProductPictureConverter pictureConverter)
        {
            _productRepository = productRepository;
            _salesAdRepository = salesAdRepository;
            _pictureRepository = pictureRepository;
            _accountRepository = accountRepository;
            _pictureConverter = pictureConverter;
        }

        public void Add(Product product, SalesAds salesAd, List<IFormFile> productImages)
        {
            salesAd.ProdId = _productRepository.Add(product).Id;

            foreach (var image in productImages)
            {
                ProductPicture output = _pictureConverter.ConvertToByteArray(image);
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
            output._product.Pictures = _pictureConverter.ByteArrayToBase64(_pictureRepository.GetAll(id));
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

        public List<SalesAds> GetProductsOfType(ProductType type)
        {
            List<SalesAds> outputList = new List<SalesAds>();

            foreach (SalesAds output in _salesAdRepository.GetAllProductsOfType(type))
            {
                output._product = _productRepository.GetById(output.ProdId, type);
                output._user = _accountRepository.GetById(output.UserId);
                output.ProductPictures = _pictureRepository.GetAll(output.ProdId);
                outputList.Add(output);
            }

            return outputList;
        }
    }
}