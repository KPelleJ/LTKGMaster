using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models.Products
{
    /// <summary>
    /// This class is a subclass to the Product superclass it inherits from the superclass,
    /// it only tells that when we create a Laptop object it is a Laptop.
    /// </summary>
    public class Laptop: Product
    {
        public Laptop()
        {
            Type = ProductType.Laptop;
        }

    }
}
