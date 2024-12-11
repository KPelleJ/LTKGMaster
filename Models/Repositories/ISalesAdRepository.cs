using LTKGMaster.Models.Products;
using LTKGMaster.Models.SalesAds;

namespace LTKGMaster.Models.Repositories
{
    /// <summary>
    /// This is the interface that handles the methods we use in our SalesAdRepository class.
    /// </summary>
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