using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models
{
    /// <summary>
    /// Denne klasse har alle vores ProductTyper i en enum så vi har et bedre overblik over hvilke produkttyper vi har.
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
