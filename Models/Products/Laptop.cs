namespace LTKGMaster.Models.Products
{
    public class Laptop: Product
    {
        public int ScreenSize { get; set; }
        public string Storage {  get; set; }
        public string OperateSystem { get; set; }
        public string GPU { get; set; }
        public string RAM { get; set; }
        public string CPU { get; set; }
        public Laptop(string model, int year, string brand, double price, string description) : 
            base(model, year, brand, price, description)
        {
            Type = ProductType.Laptop;
        }

    }
}
