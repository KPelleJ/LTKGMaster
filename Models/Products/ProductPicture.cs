namespace LTKGMaster.Models.Products
{
    /// <summary>
    /// Represents and hold data for pictures that are related to the Product class. It contains a Filename and a ProductId as a reference to a product.
    /// It can contain the image data in the format of a byte array or Base64String.
    /// </summary>
    public class ProductPicture
    {
        public string FileName { get; set; }

        public byte[] ImageDataByteArray { get; set; }

        public string ImageDataBase64String { get; set; }

        public int ProductId { get; set; }

        public ProductPicture(string fileName, byte[] byteData)
        {
            FileName = fileName;
            ImageDataByteArray = byteData;
        }

        public ProductPicture(int productId, string fileName, byte[] byteData)
        {
            ProductId = productId;
            FileName = fileName;
            ImageDataByteArray = byteData;
        }
    }
}
