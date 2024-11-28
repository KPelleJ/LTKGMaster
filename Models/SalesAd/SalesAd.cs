namespace LTKGMaster.Models.SalesAd
{
    public class SalesAd : ISalesAd
    {
        public string Title { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfCreation { get; set; }

        public SalesAd(string title, int productId, int userId, DateTime dateTimeOfCreation)
        {
            Title = title;
            ProductId = productId;
            UserId = userId;
            DateOfCreation = dateTimeOfCreation;
        }

        public SalesAd()
        {
        }
    }
}
