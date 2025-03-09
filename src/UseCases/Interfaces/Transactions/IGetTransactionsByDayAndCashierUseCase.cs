using CoreBusiness;

namespace UseCases.Interfaces.Transactions;

public interface IGetTransactionsByDayAndCashierUseCase
{
    IEnumerable<Transaction> Execute(string cashierName, DateTime date);
}
