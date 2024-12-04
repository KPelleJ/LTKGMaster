using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models.Products
{
    public class Laptop: Product
    {
        public Laptop(string model, int year, string brand, decimal price, string description) : 
            base(model, year, brand, price, description)
        {
            Type = ProductType.Laptop;
        }

        public Laptop()
        {
            Type = ProductType.Laptop;
        }

    }
}
