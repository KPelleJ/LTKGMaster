using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models.Products
{
    public class Laptop: Product
    {
        [Range(0, 1000)]
        public int ScreenSize { get; set; }
        [Range(0, 150)]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ._-]+$")]
        public string Storage {  get; set; }
        [Range(0, 150)]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ@._-]+$")]
        public string OperateSystem { get; set; }
        [Range(0, 100)]
        [RegularExpression("@\"^[0-9a-zA-ZæøåÆØÅ@._-]+$")]
        public string GPU { get; set; }
        [Range(0, 100)]
        [RegularExpression("@\"^[0-9a-zA-ZæøåÆØÅ@._-]+$")]
        public string RAM { get; set; }
        [Range(0, 100)]
        [RegularExpression("%[^0-9a-zA-ZæøåÆØÅ -]%'")]
        public string CPU { get; set; }
        
        public Laptop(string model, int year, string brand, double price, string description) : 
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
