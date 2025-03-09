using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces.Transactions;

namespace UseCases.TransactionsUsesCases;

public class AddTransactionUseCase : IAddTransactionUseCase
{
    private readonly ITransactionsRepository _transactionsRepository;

    public AddTransactionUseCase(ITransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    public void Execute(string? cashierName, int productId, string productName, double price, int beforeQty, int soldQty)
    {
        _transactionsRepository.Add(
            cashierName: cashierName,
            productId: productId,
            productName: productName,
            price: price,
            beforeQty: beforeQty,
            soldQty: soldQty);
    }
}
