using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models.Products
{
    public class Laptop: Product
    {
        public Laptop()
        {
            Type = ProductType.Laptop;
        }

    }
}
