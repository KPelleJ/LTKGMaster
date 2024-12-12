using LTKGMaster.Models.Products;

namespace LTKGMaster.Models.Repositories
{
    /// <summary>
    /// This is the interface that handles the methods we use in our ProductRepository class.
    /// </summary>
    public interface IProductRepository
    {
        Product Add(Product product);
        void Delete(int id);
        void Update(Product product);
        Product GetById(int id);
    }
}
