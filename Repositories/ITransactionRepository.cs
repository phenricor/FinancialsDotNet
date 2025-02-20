using Financials.Models;

namespace Financials.Repositories;

public interface ITransactionRepository
{
    public IEnumerable<Transaction> GetTransactions();
    public bool Add(Transaction transaction);
    public bool Delete(int id);
    public Transaction Find(int? id);
    public bool Update(Transaction obj);
    public DateTime GetLastDate();
    public string? GetTagName(Transaction obj);
    public Bucket GetBucket(Transaction obj);
}