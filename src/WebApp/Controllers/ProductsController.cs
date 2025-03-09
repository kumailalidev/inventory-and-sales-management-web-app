using CoreBusiness;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UseCases.Interfaces.Categories;
using UseCases.Interfaces.Products;

using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize(Policy = "Admin")]
public class ProductsController : Controller
{
    // Products
    private readonly IViewProductsUseCase _viewProductsInterface;
    private readonly IViewSelectedProductUseCase _viewSelectedProductUseCase;
    private readonly IAddProductUseCase _addProductUseCase;
    private readonly IEditProductUseCase _editProductUseCase;
    private readonly IDeleteProductUseCase _deleteProductUseCase;

    // Categories
    private readonly IViewCategoriesUseCase _viewCategoriesUseCase;


    public ProductsController(
        IViewProductsUseCase viewProductsInterface,
        IViewSelectedProductUseCase viewSelectedProductUseCase,
        IViewProductsByCategoryIdUseCase viewProductsByCategoryUseCase,
        IAddProductUseCase addProductUseCase,
        IEditProductUseCase editProductUseCase,
        IDeleteProductUseCase deleteProductUseCase,
        IViewCategoriesUseCase viewCategoriesUseCase)
    {
        _viewProductsInterface = viewProductsInterface;
        _viewSelectedProductUseCase = viewSelectedProductUseCase;
        _addProductUseCase = addProductUseCase;
        _editProductUseCase = editProductUseCase;
        _deleteProductUseCase = deleteProductUseCase;
        _viewCategoriesUseCase = viewCategoriesUseCase;
    }

    public IActionResult Index()
    {
        var products = _viewProductsInterface.Execute(loadCategory: true);
        return View(viewName: "Index", model: products);
    }

    public IActionResult Add()
    {
        ViewBag.Action = "Add";

        var productViewProduct = new ProductViewModel
        {
            Categories = _viewCategoriesUseCase.Execute()
        };
        return View(viewName: "Add", model: productViewProduct);
    }

    [HttpPost]
    public IActionResult Add(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            _addProductUseCase.Execute(product: productViewModel.Product);
            return RedirectToAction(actionName: nameof(Index));
        }

        ViewBag.Action = "Add";

        productViewModel.Categories = _viewCategoriesUseCase.Execute();
        return View(viewName: "Add", model: productViewModel);
    }

    public IActionResult Edit(int id)
    {
        ViewBag.Action = "Edit";

        var productViewModel = new ProductViewModel
        {
            Product = _viewSelectedProductUseCase.Execute(id: id) ?? new Product(), // if null create a new instance of Product
            Categories = _viewCategoriesUseCase.Execute()
        };

        return View(viewName: "Edit", model: productViewModel);
    }

    [HttpPost]
    public IActionResult Edit(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            _editProductUseCase.Execute(
                id: productViewModel.Product.Id,
                product: productViewModel.Product);
            return RedirectToAction(actionName: nameof(Index));
        }

        ViewBag.Action = "Edit";

        productViewModel.Categories = _viewCategoriesUseCase.Execute();
        return View(viewName: "Edit", model: productViewModel);
    }

    public IActionResult Delete(int id)
    {
        _deleteProductUseCase.Execute(id: id);
        return RedirectToAction(actionName: nameof(Index));
    }
}
