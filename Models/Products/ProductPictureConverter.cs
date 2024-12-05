using static System.Net.Mime.MediaTypeNames;

namespace LTKGMaster.Models.Products
{
    public class ProductPictureConverter
    {
        
        public ProductPicture ConvertToByteArray(IFormFile image)
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

        public List<ProductPicture> ByteArrayToBase64(List<ProductPicture> images)
        {
            var outputImages = images;

            foreach (var image in outputImages)
            {
                string base64 = $"data:image/jpeg;base64,{Convert.ToBase64String(image.ImageDataByteArray)}";
                image.ImageDataBase64String = base64;
            }

            return outputImages;
        }
    }
}
