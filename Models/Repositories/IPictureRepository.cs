using LTKGMaster.Models.Products;

namespace LTKGMaster.Models.Repositories
{
    public interface IPictureRepository
    {
        void Add(ProductPicture image);
        void Delete(int id);
        List<ProductPicture> GetAll(int productId);
    }
}