using static System.Net.Mime.MediaTypeNames;

namespace LTKGMaster.Models.Products
{
    /// <summary>
    /// Handles conversion of the product picture type between IFormFile, Byte array and base64String.
    /// </summary>
    public class ProductPictureConverter
    {
        public ProductPictureConverter() { }

        /// <summary>
        /// Converts a picture in the format of IFormFile to a byte array.
        /// </summary>
        /// <param name="image">The image file input</param>
        /// <returns>Product picture object with a filename and byte array of the image file</returns>
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

        /// <summary>
        /// Converts the byte arrays from a list of ProductPicture objects into Base64Strings.
        /// </summary>
        /// <param name="images">A list of one or more ProductPictures connected to a SalesAd</param>
        /// <returns>A list of ProductPictures where each ProductPicture contains a FileName and the imagefile in both byte array and Base64String formats.</returns>
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
