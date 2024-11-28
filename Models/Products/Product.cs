namespace LTKGMaster.Models.Products
{
    public class Product
    {
        public int CatId { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public enum ProductCategory { }
        public Product(int catId, string model, int year, string brand, double price, string description)
        {
            CatId = catId;
            Model = model;
            Year = year;
            Brand = brand;
            Price = price;
            Description = description;
        }
    }
}
