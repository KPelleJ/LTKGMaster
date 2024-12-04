using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.SalesAd
{
    public class SalesAdHandler
    {
        private ILaptopRepository _laptopRepository;
        private ISalesAdRepository _salesAdRepository;
        private IAccountRepository _accountRepository;
        private readonly PictureRepository _pictureRepository;

        public SalesAdHandler(ILaptopRepository laptopRepository, ISalesAdRepository salesAdRepository, PictureRepository pictureRepository, IAccountRepository accountRepository)
        {
            _laptopRepository = laptopRepository;
            _salesAdRepository = salesAdRepository;
            _pictureRepository = pictureRepository;
            _accountRepository = accountRepository;
        }

        public void Add(Laptop laptop, SalesAds salesAd, List<IFormFile> productImages)
        {
            salesAd._product.Id = _laptopRepository.Add(laptop).Id;

            foreach (var image in productImages)
            {
                ProductPicture output = ProductPictureConverter.ConvertToByteArray(image);
                output.ProductId = salesAd._product.Id;
                _pictureRepository.Add(output);
            }

            _salesAdRepository.Add(salesAd);
        }

        //!!!!!!!!!!!!!!!!!!!!!!!ATTENZIONE!!!!!!!!!!!!!!!!!!
        //Den her metode kan laves om til en giga lang query hvis det er bedre, vi skal lige høre Camilla ad
        //!!!!!!!!!!!!!!!!!!!!!!!ATTENZIONE!!!!!!!!!!!!!!!!!!
        public SalesAds Get(int id)
        { 
            SalesAds output = _salesAdRepository.GetById(id);
            output._user = _accountRepository.GetById(output.UserId);
            output._product = _laptopRepository.GetById(id);
            output._product.Pictures = ProductPictureConverter.ByteArrayToBase64(_pictureRepository.GetAll(id));
            return output;
        }

        public List<SalesAds> GetAllSalesToUser(int userId)
        {
            return _salesAdRepository.GetAllFromUser(userId);
        }

        public void DeleteSalesAd(int id)
        {
            _salesAdRepository.Delete(id);
        }
    }
}