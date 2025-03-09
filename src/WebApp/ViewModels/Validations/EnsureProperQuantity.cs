using System.ComponentModel.DataAnnotations;

using UseCases.Interfaces.Products;

namespace WebApp.ViewModels.Validations;

public class EnsureProperQuantity : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        // Converting into SalesViewModel
        var salesViewModel = validationContext.ObjectInstance as SalesViewModel;

        if (salesViewModel != null)
        {
            if (salesViewModel.QuantityToSell <= 0)
                return new ValidationResult("The Quantity to sell has to be greater than zero.");
            else
            {
                // Old
                // var product = ProductsRepository.GetProductById(productId: salesViewModel.SelectedProductId);

                // Getting GetProductByIdUseCase service, GetService returns object therefore we need to convert it into interface
                IViewSelectedProductUseCase? getProductByIdUseCase = validationContext.GetService(typeof(IViewSelectedProductUseCase)) as IViewSelectedProductUseCase;

                if (getProductByIdUseCase != null)
                {
                    var product = getProductByIdUseCase.Execute(id: salesViewModel.SelectedProductId);
                    if (product != null)
                    {
                        if (product.Quantity < salesViewModel.QuantityToSell)
                            return new ValidationResult($"{product.Name} only has {product.Quantity} left.");
                    }
                }
                else
                {
                    return new ValidationResult("The selected product doesn't exist.");
                }
            }
        }
        return ValidationResult.Success;
    }
}
