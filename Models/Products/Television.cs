namespace LTKGMaster.Models.Products
{
    /// <summary>
    /// The Television class inherits from the Product class
    /// </summary>
    public class Television: Product
    {
        // Constructor that initializes the Television object with specific parameters
        // This constructor is called when creating a new instance of Television with details
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

