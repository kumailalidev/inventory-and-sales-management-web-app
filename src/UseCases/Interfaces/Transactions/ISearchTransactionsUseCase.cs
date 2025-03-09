using CoreBusiness;

namespace UseCases.Interfaces.Transactions;

public interface ISearchTransactionsUseCase
{
    IEnumerable<Transaction> Execute(string cashierName, DateTime startDate, DateTime endDate);
}
