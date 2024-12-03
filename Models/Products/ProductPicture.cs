namespace LTKGMaster.Models.Products
{
    public class ProductPicture
    {
        public string FileName { get; set; }

        public byte[] ImageDataByteArray { get; set; }

        public string ImageDataBase64String { get; set; }

        public int ProductId { get; set; }

        public ProductPicture(string fileName, byte[] byteData, string stringData) : this(fileName, byteData)
        {
            ImageDataBase64String = stringData;
        }

        public ProductPicture(string fileName, byte[] byteData)
        {
            FileName = fileName;
            ImageDataByteArray = byteData;
        }

        public ProductPicture(int productId, string fileName, byte[] byteData, string stringData) :this (productId, fileName, byteData)
        { 
            ImageDataBase64String = stringData;
        }

        public ProductPicture(int productId, string fileName, byte[] byteData)
        {
            ProductId = productId;
            FileName = fileName;
            ImageDataByteArray = byteData;
        }
    }
}
