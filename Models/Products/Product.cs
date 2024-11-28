namespace LTKGMaster.Models.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; protected set; }
        public Product(string model, int year, string brand, double price, string description)
        {
            Model = model;
            Year = year;
            Brand = brand;
            Price = price;
            Description = description;
        }
    }
}
