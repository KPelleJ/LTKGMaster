using LTKGMaster.Models.Products;
using LTKGMaster.Models.SalesAds;

namespace LTKGMaster.Models.Repositories
{
    public interface ISalesAdRepository
    {
        void Add(SalesAd salesAd);
        void Delete(int id);
        List<SalesAd> GetAll();
        void Update(SalesAd salesAd);
        List<SalesAd> GetAllFromUser(int id);
        List<SalesAd> GetAllProductsOfType(ProductType prodtype);
        SalesAd GetById(int id);
    }
}