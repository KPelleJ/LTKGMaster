using LTKGMaster.Models.Products;

namespace LTKGMaster.Models.Repositories
{
    public interface ILaptopRepository
    {
        Laptop Add(Laptop product);
        void Delete(Laptop product);
        void Update(Laptop product);
        Laptop GetById(int id);
    }
}