using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models
{
    /// <summary>
    /// This is where we store the enum that have all our different product types.
    /// </summary>
    public enum ProductType
    {
        [Display(Name = "Bærbar pc")]
        Laptop = 1,

        [Display(Name = "Fjernsyn")]
        Television = 2,

        [Display(Name = "Spillekonsol")]
        Console = 3
    }
}
