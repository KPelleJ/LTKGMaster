
using LTKGMaster.Models.Products;
using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.SalesAds
{
    /// <summary>
    /// This interface represents a sales ad, which includes the product and user details,
    /// as well as information about the ad's creation date and title.
    /// </summary>
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