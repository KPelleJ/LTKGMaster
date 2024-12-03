using LTKGMaster.Models.SalesAd;

namespace LTKGMaster.Models.Repositories
{
    public interface ISalesAdRepository
    {
        void Add(SalesAds salesAd);
        void Delete(int id);
        List<SalesAds> GetAll(SalesAds salesAd);
        void Update(SalesAds salesAd);
        List<SalesAds> GetAllFromUser(int id);

        SalesAds GetById(int id);
    }
}