using Microsoft.AspNetCore.Mvc;

using UseCases.Interfaces.Transactions;

namespace WebApp.ViewComponents;

[ViewComponent]
// NOTE: Class name ends with ViewComponent so it is not necessary to add attribute to decorate
// class
public class TransactionsViewComponent : ViewComponent
{
    private readonly IGetTransactionsByDayAndCashierUseCase _getTransactionsByDayAndCashierUseCase;

    public TransactionsViewComponent(IGetTransactionsByDayAndCashierUseCase getTransactionsByDayAndCashierUseCase)
    {
        _getTransactionsByDayAndCashierUseCase = getTransactionsByDayAndCashierUseCase;
    }

    public IViewComponentResult Invoke(string userName)
    {
        var transactions = _getTransactionsByDayAndCashierUseCase.Execute(cashierName: userName, date: DateTime.Now);

        return View(transactions);
    }
}
