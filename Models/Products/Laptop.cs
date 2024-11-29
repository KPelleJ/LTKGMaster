using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models.Products
{
    public class Laptop: Product
    {
        [Range(0, 1000)]
        public int ScreenSize { get; set; }
        [Length(0, 150)]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ.-]+$")]
        public string Storage {  get; set; }
        [Length(0, 150)]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ.-]+$")]
        public string OperateSystem { get; set; }
        [Length(0, 100)]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ.-]+$")]
        public string GPU { get; set; }
        [Length(0, 100)]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ.-]+$")]
        public string RAM { get; set; }
        [Length(0, 100)]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ.-]+$")]
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
