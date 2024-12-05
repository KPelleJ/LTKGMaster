using LTKGMaster.Models.Products;
using LTKGMaster.Models.SalesAd;

namespace LTKGMaster.Models.Repositories
{
    public interface ISalesAdRepository
    {
        void Add(SalesAds salesAd);
        void Delete(int id);
        List<SalesAds> GetAll();
        void Update(SalesAds salesAd);
        List<SalesAds> GetAllFromUser(int id);
        List<SalesAds> GetAllLaptops();
        List<SalesAds> GetAllProductsOfType(ProductType prodtype);
        SalesAds GetById(int id);
    }
}