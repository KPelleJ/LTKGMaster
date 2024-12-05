using LTKGMaster.Models.Products;

namespace LTKGMaster.Models.Repositories
{
    public interface IProductRepository
    {
        Product Add(Product product);
        void Delete(int id);
        void Update(Product product);
        Product GetById(int id, ProductType type);
    }
}
