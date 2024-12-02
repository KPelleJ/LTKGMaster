using static System.Net.Mime.MediaTypeNames;

namespace LTKGMaster.Models.Products
{
    public class ProductPictureConverter
    {
        
        public static ProductPicture ConvertToByteArray(IFormFile image)
        {
            byte[] outputData;

            using (MemoryStream ms = new MemoryStream())
            { 
                image.CopyTo(ms);
                outputData = ms.ToArray();
            }    

            ProductPicture output = new ProductPicture(image.FileName, outputData);
            return output;
        }

        public static List<IFormFile> ByteArrayToImage(List<ProductPicture> images)
        {
            return null;
        }
    }
}
