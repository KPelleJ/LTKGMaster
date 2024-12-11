namespace LTKGMaster.Models.Products
{
    public class Television: Product
    {
        public Television(string model, int year, string brand, decimal price, string description) :
            base(model, year, brand, price, description)
        {
            Type = ProductType.Television;
        }

        public Television()
        {
            Type = ProductType.Television;
        }
    }
}

