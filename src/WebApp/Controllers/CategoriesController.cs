using CoreBusiness;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UseCases.Interfaces.Categories;

namespace WebApp;

[Authorize(Policy = "Admin")] // Only authorize users can access the endpoints
public class CategoriesController : Controller
{
    // Dependency Injection
    private readonly IViewCategoriesUseCase viewCategoriesUseCase; // Violation _
    private readonly IViewSelectedCategoryUseCase _viewSelectedCategoryUseCase;
    private readonly IEditCategoryUseCase _editCategoryUseCase;
    private readonly IAddCategoryUseCase _addCategoryUseCase;
    private readonly IDeleteCategoryUseCase _deleteCategoryUseCase;

    public CategoriesController(
        IViewCategoriesUseCase viewCategoriesUseCase,
        IViewSelectedCategoryUseCase viewSelectedCategoryUseCase,
        IEditCategoryUseCase editCategoryUseCase,
        IAddCategoryUseCase addCategoryUseCase,
        IDeleteCategoryUseCase deleteCategoryUseCase)
    {
        this.viewCategoriesUseCase = viewCategoriesUseCase;
        _viewSelectedCategoryUseCase = viewSelectedCategoryUseCase;
        _editCategoryUseCase = editCategoryUseCase;
        _addCategoryUseCase = addCategoryUseCase;
        _deleteCategoryUseCase = deleteCategoryUseCase;
    }
    public IActionResult Index()
    {
        var categories = viewCategoriesUseCase.Execute();
        return View(model: categories);
    }

    // [FromRoute] id: id data is coming from route, therefore /Categories/Edit?Id=1 will
    // fail to do model binding from query.
    // BY default ASP.NET will locate data from all the 5 Routes.
    [HttpGet] // Default
    public IActionResult Edit(int? id) // nullable integer; default value is null not zero
    {
        // if (id.HasValue)
        //     return new ContentResult { Content = id.ToString() }; // returns value of id; Categories/Edit/{id?}
        // else
        //     return new ContentResult { Content = "NO CONTENT; id is null" };

        // Used by partial view to identify 'Edit' or 'Add'
        ViewBag.Action = "Edit"; // NOTE: Case sensitive

        var category = _viewSelectedCategoryUseCase.Execute(id: id ?? 0);

        return View(model: category); // viewName is optional
    }

    [HttpPost] // Handles POST request
    public IActionResult Edit(Category category)
    {
        // Checking state of model and its values (i.e Category)
        if (ModelState.IsValid)
        {
            _editCategoryUseCase.Execute(id: category.Id, category: category);
            return RedirectToAction(actionName: nameof(Index));
        }

        ViewBag.Action = "Edit"; // Required for partial to be rendered correctly

        // On invalid form submission, show user Edit with the
        // invalid data (entered inside fields)
        return View(model: category);
    }

    [HttpGet]
    public IActionResult Add()
    {
        // Used by partial view to identify 'Edit' or 'Add'
        ViewBag.Action = "Add"; // NOTE: Case sensitive

        return View(viewName: "Add"); // viewName is optional if it is same as controller method name
    }

    [HttpPost]
    public IActionResult Add([FromForm] Category category)
    {
        if (ModelState.IsValid)
        {
            _addCategoryUseCase.Execute(category: category);
            return RedirectToAction(actionName: nameof(Index));
        }

        ViewBag.Action = "Add"; // Required for partial to be rendered correctly

        return View(viewName: "Add", model: category);
    }

    public IActionResult Delete(int id)
    {
        _deleteCategoryUseCase.Execute(id: id);
        return RedirectToAction(actionName: nameof(Index));
    }
}
