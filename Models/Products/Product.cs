using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models.Products
{
    public abstract class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Maks 255 tegn.")]
        [StringLength(255)]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ.-]+$")]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required(ErrorMessage ="Maks 255 tegn.")]
        [StringLength(255)]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ.-]+$")]
        public string Brand { get; set; }

        [Required(ErrorMessage ="Skal være mere end 0.")]
        [Range(0, 1000000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Må ikke være mere end 4000 tegn.")]
        [MaxLength(4000)]
        public string Description { get; set; }

        public List<ProductPicture> Pictures { get; set; }

        public ProductType Type { get; set; }

        public Product(string model, int year, string brand, decimal price, string description)
        {
            Model = model;
            Year = year;
            Brand = brand;
            Price = price;
            Description = description;
        }

        public Product()
        {
            
        }
    }
}
