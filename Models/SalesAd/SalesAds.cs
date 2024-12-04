using LTKGMaster.Models.Products;
using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.SalesAd
{
    public class SalesAds 
    {
        public int ProdId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public Laptop _product { get; set; }
        public IUser _user { get; set; }
        public DateTime DateOfCreation { get; set; }
        public List<ProductPicture> ProductPictures { get; set; }

        public SalesAds(Laptop product, IUser user, string title, DateTime dateTimeOfCreation)
        {
            Title = title;
            _product = product;
            _user = user;
            DateOfCreation = dateTimeOfCreation;
        }

        public SalesAds()
        {
            _product = new Laptop();
            _user = new RegularUser();
        }
        public SalesAds(int prodId, int userId, string title, DateTime dateTimeOfCreation)
        {
            ProdId = prodId;
            UserId = userId;
            Title = title;
            DateOfCreation = dateTimeOfCreation;
        }
    }
}
