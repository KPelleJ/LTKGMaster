namespace LTKGMaster.Models.Products
{
    /// <summary>
    /// // The ProductFactory class is responsible for creating instances of different product types
    /// </summary>
    public class ProductFactory
    { 
        public Product Create(ProductType productType)
        {
            // Switch statement to check the value of productType and create the corresponding product
            switch (productType)
            {
                // If the product type is Laptop, return a new instance of Laptop and so on
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
