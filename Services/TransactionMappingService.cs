using Financials.Data;
using Financials.Models;
using Financials.Repositories;
namespace Financials.Services;

public class TransactionMappingService
{
    private  readonly AppDbContext _db;
    private readonly ITransactionRepository _repository;
    
    public TransactionMappingService(AppDbContext db, ITransactionRepository repository)
    {
        _db = db;
        _repository = repository;
    }

    public int MapBucketsOnTransactions(IEnumerable<Transaction> objs)
    {
        string sql = "SELECT Description, TagId FROM DescriptionTagMap";
        var descriptionTagMaps = _db.LoadData<DescriptionTagMap>(sql).ToDictionary(x => x.Description, x=>x.TagId);
        int rowsAffected = 0;
        foreach (var obj in objs)
        {
            if (obj.TagId == null)
            {
                if (descriptionTagMaps.ContainsKey(obj.Description))
                {
                    _repository.Update(new()
                    {
                        Description = obj.Description,
                        Value = obj.Value,
                        Date = obj.Date, 
                        BucketId = obj.BucketId,
                        Status = obj.Status,
                        Id = obj.Id,
                        TagId = descriptionTagMaps[obj.Description]
                    });
                    rowsAffected++;
                }
            }
        }
        return rowsAffected;
    }
}