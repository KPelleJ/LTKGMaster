﻿using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models.Products
{
    /// <summary>
    /// This is the superclass for all out products, this is where all the attributes is for the different products.
    /// The class is abstract so we cant instatiate them in our code.
    /// </summary>
    public abstract class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Maks 255 tegn.")]
        [StringLength(255)]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ. -]+$")]
        public string Model { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
        [Display(Name ="Årstal")]
        public int Year { get; set; }

        [Required(ErrorMessage ="Maks 255 tegn.")]
        [StringLength(255)]
        [RegularExpression(@"^[0-9a-zA-ZæøåÆØÅ. -]+$")]
        [Display(Name ="Mærke")]
        public string Brand { get; set; }

        [Required(ErrorMessage ="Skal være mere end 0.")]
        [Range(0, 1000000)]
        [Display(Name ="Pris")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Må ikke være mere end 4000 tegn.")]
        [MaxLength(4000)]
        [Display(Name ="Beskrivelse")]
        public string Description { get; set; }

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
