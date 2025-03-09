using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UseCases.Interfaces.Categories;
using UseCases.Interfaces.Products;
using UseCases.Interfaces.Transactions;

namespace WebApp.Controllers;

[Authorize(Policy = "AdminOrSeller")]
public class SalesController : Controller
{
    // Categories
    private readonly IViewCategoriesUseCase _viewCategoriesUseCase;
    // Products
    private readonly IViewSelectedProductUseCase _viewSelectedProductUseCase;
    private readonly IViewProductsByCategoryIdUseCase _viewProductsByCategoryUseCase;
    private readonly IEditProductUseCase _editProductUseCase;
    // Transactions
    private readonly IAddTransactionUseCase _addTransactionUseCase;

    public SalesController(
        IViewCategoriesUseCase viewCategoriesUseCase,
        IViewSelectedProductUseCase viewSelectedProductUseCase,
        IEditProductUseCase editProductUseCase,
        IAddTransactionUseCase addTransactionUseCase,
        IViewProductsByCategoryIdUseCase viewProductsByCategoryUseCase)
    {
        _viewCategoriesUseCase = viewCategoriesUseCase;
        _viewSelectedProductUseCase = viewSelectedProductUseCase;
        _editProductUseCase = editProductUseCase;
        _addTransactionUseCase = addTransactionUseCase;
        _viewProductsByCategoryUseCase = viewProductsByCategoryUseCase;
    }

    public IActionResult Index()
    {
        var salesViewModel = new SalesViewModel
        {
            Categories = _viewCategoriesUseCase.Execute()
        };

        return View(viewName: "Index", model: salesViewModel);
    }

    public IActionResult Sell(SalesViewModel salesViewModel)
    {
        if (ModelState.IsValid)
        {
            var prod = _viewSelectedProductUseCase.Execute(id: salesViewModel.SelectedProductId);
            if (prod != null)
            {
                _addTransactionUseCase.Execute(
                    User.Identity?.Name,
                    salesViewModel.SelectedCategoryId,
                    prod.Name,
                    prod.Price.HasValue ? prod.Price.Value : 0,
                    prod.Quantity.HasValue ? prod.Quantity.Value : 0,
                    salesViewModel.QuantityToSell
                );

                // Decrease the product quantity
                prod.Quantity -= salesViewModel.QuantityToSell;

                // Update the product
                // ProductsRepository.UpdateProduct(salesViewModel.SelectedProductId, product: prod); // OLD
                _editProductUseCase.Execute(id: salesViewModel.SelectedProductId, product: prod);
            }
        }

        // In case validation fails

        // Get the product
        var product = _viewSelectedProductUseCase.Execute(id: salesViewModel.SelectedProductId);

        // Get the category of the product
        salesViewModel.SelectedCategoryId = (product?.CategoryId == null) ? 0 : product.CategoryId.Value;
        salesViewModel.Categories = _viewCategoriesUseCase.Execute();

        return View(viewName: "Index", model: salesViewModel);
    }

    // Returns partial view (Contains product detail)
    public IActionResult SellProductPartial(int id)
    {
        var product = _viewSelectedProductUseCase.Execute(id: id);
        return PartialView(viewName: "_SellProduct", model: product);
    }

    // Returns partial view (Contains list of products based on category)
    public IActionResult ProductsByCategoryPartial(int categoryId)
    {
        var products = _viewProductsByCategoryUseCase.Execute(id: categoryId);
        return PartialView(viewName: "_Products", model: products);
    }
}
