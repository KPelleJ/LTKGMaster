using LTKGMaster.Models.Products;
using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.SalesAd
{
    public class SalesAds 
    {
        public int ProdId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public Product _product { get; set; }
        public IUser _user { get; set; }
        public DateTime DateOfCreation { get; set; }
        public List<ProductPicture> ProductPictures { get; set; }

        public SalesAds()
        {
        }
    }
}
