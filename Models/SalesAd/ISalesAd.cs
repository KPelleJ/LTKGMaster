
namespace LTKGMaster.Models.SalesAd
{
    public interface ISalesAd
    {
        DateTime DateOfCreation { get; set; }
        int ProductId { get; set; }
        string Title { get; set; }
        int UserId { get; set; }
    }
}