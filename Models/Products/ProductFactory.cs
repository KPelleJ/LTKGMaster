namespace LTKGMaster.Models.Products
{
    public class ProductFactory
    {
        public ProductFactory() 
        { 
        
        }

        public Product Create(ProductType productType)
        {
            switch (productType)
            {
                case ProductType.Laptop:
                    return new Laptop();
                case ProductType.Television: 
                    return new Television();
                default:
                    return null;
            }
        }
    }
}
