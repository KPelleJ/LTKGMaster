using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models
{
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
