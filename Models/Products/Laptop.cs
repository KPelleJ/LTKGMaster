using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models.Products
{
    /// <summary>
    /// Denne klasse er en subklasse til Product superklassen den fortæller bare at en Laptop bliver født som en Laptop.
    /// </summary>
    public class Laptop: Product
    {
        public Laptop()
        {
            Type = ProductType.Laptop;
        }

    }
}
