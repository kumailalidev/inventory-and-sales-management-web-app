
using CoreBusiness;

using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Transactions;

namespace UseCases.TransactionsUsesCases;

public class GetTransactionsByDayAndCashierUseCase : IGetTransactionsByDayAndCashierUseCase
{
    private readonly ITransactionsRepository _transactionsRepository;

    public GetTransactionsByDayAndCashierUseCase(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public IEnumerable<Transaction> Execute(string cashierName, DateTime date)
    {
        return _transactionsRepository.GetByDayAndCashier(cashierName: cashierName, date: date);
    }
}
