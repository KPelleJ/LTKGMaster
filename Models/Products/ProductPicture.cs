namespace LTKGMaster.Models.Products
{
    public class ProductPicture
    {
        public string FileName { get; set; }

        public byte[] ImageData { get; set; }

        public int ProductId { get; set; }

        public ProductPicture(string fileName, byte[] imageData) 
        {
            ProductId = 37;
            FileName = fileName;
            ImageData = imageData;
        }
        
        public ProductPicture(int productId, string fileName, byte[] imageData) 
        { 
            ProductId = productId;
            FileName = fileName;
            ImageData = imageData;
        }
    }
}
