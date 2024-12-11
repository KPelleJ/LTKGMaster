using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.SalesAds
{
    public class SalesAdHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly ISalesAdRepository _salesAdRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly ProductPictureConverter _pictureConverter;

        public SalesAdHandler(IProductRepository productRepository, ISalesAdRepository salesAdRepository, IPictureRepository pictureRepository, IAccountRepository accountRepository, ProductPictureConverter productPictureConverter)
        {
            _productRepository = productRepository;
            _salesAdRepository = salesAdRepository;
            _pictureRepository = pictureRepository;
            _accountRepository = accountRepository;
            _pictureConverter = productPictureConverter;
        }

        public void Add(Product product, SalesAd salesAd, List<IFormFile> productImages)
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

        public SalesAd Get(int id, ProductType type)
        { 
            SalesAd output = _salesAdRepository.GetById(id);
            output.User = _accountRepository.GetById(output.UserId);
            output.Product = _productRepository.GetByIdAndType(id, type);
            output.ProductPictures = _pictureConverter.ByteArrayToBase64(_pictureRepository.GetAll(id));
            return output;
        }

        public List<SalesAd> GetSalesAdsFromUser(int userId)
        {
            return _salesAdRepository.GetAllFromUser(userId);
        }

        public void DeleteSalesAd(int id)
        {
            _pictureRepository.Delete(id);
            _salesAdRepository.Delete(id);
            _productRepository.Delete(id);
        }

        public List<SalesAd> GetProductsOfType(ProductType type)
        {
            List<SalesAd> outputList = new List<SalesAd>();

            foreach (SalesAd output in _salesAdRepository.GetAllProductsOfType(type))
            {
                output.Product = _productRepository.GetByIdAndType(output.ProdId, type);
                output.User = _accountRepository.GetById(output.UserId);
                output.ProductPictures = _pictureConverter.ByteArrayToBase64(_pictureRepository.GetAll(output.ProdId));
                outputList.Add(output);
            }

            return outputList;
        }

        public List<SalesAd> GetSearchResults(string searchQuery)
        {
            List<SalesAd> listToSearch = new List<SalesAd>();
            List<SalesAd> outputList = new List<SalesAd>();

            foreach (SalesAd output in _salesAdRepository.GetAll())
            {
                output.Product = _productRepository.GetById(output.ProdId);
                listToSearch.Add(output);
            }

            foreach (SalesAd output in listToSearch.FindAll(x => 
            x.Title.Contains(searchQuery) || 
            x.Product.Type.ToString().Equals(searchQuery) ||
            x.Product.Brand.Contains(searchQuery) ||
            x.Product.Model.Contains(searchQuery)))
            {
                output.User = _accountRepository.GetById(output.UserId);
                output.ProductPictures = _pictureConverter.ByteArrayToBase64(_pictureRepository.GetAll(output.ProdId));
                outputList.Add(output);
            }

            return outputList;
        }
    }
}