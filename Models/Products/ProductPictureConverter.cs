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

        public static List<string> ByteArrayToBase64OldMethod(List<ProductPicture> images)
        {


            var base64Strings = new List<string>();

            foreach (var image in images)
            {
                string base64 = Convert.ToBase64String(image.ImageDataByteArray);
                base64Strings.Add($"data:image/jpeg;base64,{base64}"); // Change mime type if needed
            }

            return base64Strings;
        }

        public static List<ProductPicture> ByteArrayToBase64(List<ProductPicture> images)
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
