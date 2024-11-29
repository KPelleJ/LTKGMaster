namespace LTKGMaster.Models.SalesAd
{
    public class SalesAds 
    {
        public string Title { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }

        public SalesAds(string title, int productId, int userId, DateTime dateTimeOfCreation)
        {
            Title = title;
            ProductId = productId;
            UserId = userId;
            DateOfCreation = dateTimeOfCreation;
        }

        public SalesAds()
        {
        }
    }
}
