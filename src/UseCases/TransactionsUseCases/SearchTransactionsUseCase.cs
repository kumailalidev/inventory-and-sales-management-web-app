
using CoreBusiness;

using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Transactions;

namespace UseCases.TransactionsUsesCases;

public class SearchTransactionsUseCase : ISearchTransactionsUseCase
{
    private readonly ITransactionsRepository _transactionsRepository;

    public SearchTransactionsUseCase(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public IEnumerable<Transaction> Execute(
        string cashierName,
        DateTime startDate,
        DateTime endDate)
    {
        return _transactionsRepository.Search(
            cashierName: cashierName,
            startDate: startDate,
            endDate: endDate);
    }
}
