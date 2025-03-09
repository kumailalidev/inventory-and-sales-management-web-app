using CoreBusiness;

using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory;

public class TransactionsInMemoryRepository : ITransactionsRepository
{
    private static readonly List<Transaction> transactions = new List<Transaction>();

    public IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date)
    {
        if (string.IsNullOrWhiteSpace(cashierName))
            return transactions.Where(x => x.TimeStamp.Date == date.Date);
        else
            return transactions.Where(x =>
                x.CashierName.ToLower().Contains(cashierName.ToLower()) &&
                x.TimeStamp.Date == date.Date);
    }

    public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(cashierName))
            return transactions.Where(x =>
                x.TimeStamp >= startDate.Date &&
                x.TimeStamp <= endDate.AddDays(1).Date);
        else
            return transactions.Where(x =>
                x.CashierName.ToLower().Contains(cashierName.ToLower()) &&
                x.TimeStamp >= startDate.Date &&
                x.TimeStamp <= endDate.AddDays(1).Date);
    }

    public void Add(
        string? cashierName,
        int productId,
        string ProductName,
        double price,
        int beforeQty,
        int soldQty)
    {
        var transaction = new Transaction
        {
            ProductId = productId,
            ProductName = ProductName,
            TimeStamp = DateTime.Now,
            Price = price,
            BeforeQty = beforeQty,
            SoldQty = soldQty,
            CashierName = cashierName ?? string.Empty
        };

        if (transactions != null && transactions.Count > 0)
        {
            var maxId = transactions.Max(x => x.Id);
            transaction.Id = maxId + 1;
        }
        else
        {
            transaction.Id = 1;
        }

        transactions?.Add(transaction);
    }
}
