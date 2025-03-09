using System.ComponentModel.DataAnnotations;

using CoreBusiness;

using WebApp.ViewModels.Validations;

public class SalesViewModel
{
    public int SelectedCategoryId { get; set; }
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    public int SelectedProductId { get; set; }
    [Range(1, int.MaxValue)]
    [EnsureProperQuantity]
    [Display(Name = "Quantity")]
    public int QuantityToSell { get; set; }
}
