
using LTKGMaster.Models.Products;
using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.SalesAds
{
    public interface ISalesAd
    {
        DateTime DateOfCreation { get; set; }
        int ProductId { get; set; }
        string Title { get; set; }
        int UserId { get; set; }
        Product _product { get; set; }
        IUser _user { get; set; }
    }
}