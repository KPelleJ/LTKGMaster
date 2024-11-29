using LTKGMaster.Models.SalesAd;

namespace LTKGMaster.Models.Repositories
{
    public interface ISalesAdRepository
    {
        void Add(ISalesAd salesAd);
        void Delete(ISalesAd salesAd);
        List<SalesAd.SalesAd> GetAll(ISalesAd salesAd);
        void Update(ISalesAd salesAd);
    }
}