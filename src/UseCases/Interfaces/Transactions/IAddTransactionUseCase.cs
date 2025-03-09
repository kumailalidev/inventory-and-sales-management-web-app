namespace UseCases.Interfaces.Transactions;

public interface IAddTransactionUseCase
{
    void Execute(string? cashierName, int productId, string productName, double price, int beforeQty, int soldQty);
}
