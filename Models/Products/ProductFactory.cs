namespace LTKGMaster.Models.Products
{
    public class ProductFactory
    {
        public Product Create(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.Laptop:
                    return new Laptop();
                case ProductType.Television: 
                    return new Television();
                case ProductType.Console:
                    return new Console();
                default:
                    return null;
            }
        }
    }
}
