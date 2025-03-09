using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoreBusiness;

using Microsoft.EntityFrameworkCore;

using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class TransactionSQLRepository : ITransactionsRepository
    {
        private readonly MarketContext db;

        public TransactionSQLRepository(MarketContext db)
        {
            this.db = db;
        }

        public void Add(string? cashierName, int productId, string productName, double price, int beforeQty, int soldQty)
        {
            var transaction = new Transaction
            {
                ProductId = productId,
                ProductName = productName,
                TimeStamp = DateTime.Now,
                Price = price,
                BeforeQty = beforeQty,
                SoldQty = soldQty,
                CashierName = cashierName ?? string.Empty
            };

            db.Transactions.Add(transaction);
            db.SaveChanges();
        }

        public IEnumerable<Transaction> GetByDayAndCashier(string cashierName, DateTime date)
        {
            if(string.IsNullOrWhiteSpace(cashierName))
            {
                return db.Transactions.Where(t => t.TimeStamp.Date == date.Date);
            }
            else
            {
                return db.Transactions.Where(t =>
                    EF.Functions.Like(t.CashierName, $"%{cashierName}%") &&
                    t.TimeStamp.Date == date.Date);
            }
        }

        public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
            {
                return db.Transactions.Where(t =>
                    t.TimeStamp.Date >= startDate.Date &&
                    t.TimeStamp.Date <= endDate.Date);
            }
            else
            {
                return db.Transactions.Where(t =>
                    EF.Functions.Like(t.CashierName, $"%{cashierName}%") &&
                    t.TimeStamp.Date >= startDate.Date &&
                    t.TimeStamp.Date <= endDate.Date);
            }
        }
    }
}
