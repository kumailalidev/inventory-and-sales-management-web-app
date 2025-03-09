using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UseCases.Interfaces.Transactions;

using WebApp.ViewModels;

namespace WebApp.Controllers;

[Authorize(Policy = "AdminOrManager")]
public class TransactionsController : Controller
{
    // Transactions
    private readonly ISearchTransactionsUseCase _searchTransactionsUseCase;

    public TransactionsController(ISearchTransactionsUseCase searchTransactionsUseCase)
    {
        _searchTransactionsUseCase = searchTransactionsUseCase;
    }

    public IActionResult Index()
    {
        return View(viewName: "Index", model: new TransactionsViewModel());
    }

    public IActionResult Search(TransactionsViewModel transactionViewModel)
    {
        // Get the transactions
        var transactions = _searchTransactionsUseCase.Execute(
            cashierName: transactionViewModel.CashierName ?? string.Empty,
            startDate: transactionViewModel.StartDate,
            endDate: transactionViewModel.EndDate);

        transactionViewModel.Transactions = transactions;

        return View(viewName: "Index", model: transactionViewModel);
    }
}
