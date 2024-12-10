using LTKGMaster.Models.Products;

namespace LTKGMaster.Models.Repositories
{
    /// <summary>
    /// Defines methods to be used for actions relating to ProductPicture data
    /// </summary>
    public interface IPictureRepository
    {
        void Add(ProductPicture image);
        void Delete(int id);
        List<ProductPicture> GetAll(int productId);
    }
}