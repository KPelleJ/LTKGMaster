using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;

namespace LTKGMaster.Models.SalesAd
{
    public class SalesAdHandler
    {
        private ILaptopRepository _laptopRepository;
        private ISalesAdRepository _salesAdRepository;

        public SalesAdHandler(ILaptopRepository laptopRepository, ISalesAdRepository salesAdRepository)
        {
            _laptopRepository = laptopRepository;
            _salesAdRepository = salesAdRepository;
        }
    }
}