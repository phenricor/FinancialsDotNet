using Financials.Data;
using Financials.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Financials.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _db;

    public TransactionRepository(AppDbContext db)
    {
        _db = db;
    }
    public IEnumerable<Transaction> GetTransactions()
    {
        string sql = "SELECT * FROM Transactions";
        IEnumerable<Transaction> transactions = _db.LoadData<Transaction>(sql);
        return transactions;
    }

    public bool Add(Transaction transaction)
    {
        string sql = "INSERT INTO Transactions VALUES (@Description, @Value, @Date, @TagId, @BucketId, @Status)";
        var param = new {
            Description = transaction.Description,
            Value = transaction.Value,
            Date = transaction.Date,
            TagId = transaction.TagId,
            BucketId = transaction.BucketId,
            Status = transaction.Status
        };
        return _db.ExecuteSql(sql, param);
    }

    public bool Delete(int id)
    {
        string sql = "DELETE FROM Transactions WHERE Id = @Id";
        var param = new { Id = id };
        return _db.ExecuteSql(sql, param);
    }

    public Transaction Find(int? id)
    {
        string sql = "SELECT * FROM Transactions WHERE Id = @Id";
        var param = new { Id = id };
        return _db.LoadDataSingle<Transaction>(sql, param);
    }
    public bool Update(Transaction obj)
    {
        string sql = @"
        UPDATE Transactions
        SET Description = @Description, Value = @Value, Date = @Date, TagId = @TagId, BucketId = @BucketId, Status = @Status
        WHERE Id = @Id";
        var param = new {
            Description = obj.Description,
            Value = obj.Value,
            Date = obj.Date, 
            TagId = obj.TagId,
            BucketId = obj.BucketId,
            Status = obj.Status,
            Id = obj.Id
        };
        return _db.ExecuteSql(sql, param);
    }
}