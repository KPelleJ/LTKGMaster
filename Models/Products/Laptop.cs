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
        public Laptop(int catId, string model, int year, string brand, double price, string description,
            int screenSize, string storage, string operateSystem, string gpu, string ram, string cpu
            ) : base(catId, model, year, brand, price, description)
        {
            Storage = storage;
            ScreenSize = screenSize;
            OperateSystem = operateSystem;
            GPU = gpu;
            RAM = ram;
            CPU = cpu;
        }
    }
}
