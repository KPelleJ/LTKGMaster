namespace LTKGMaster.Models.Products
{
    /// <summary>
    /// This class is a subclass to the Product superclass it inherits from the superclass,
    /// it only tells that when we create a Console object it is a Console.
    /// </summary>
    public class Console:Product
    {
        public Console() 
        { 
            Type = ProductType.Console;
        }
    }
}
