using System.Globalization;
using CsvHelper;
using Financials.Dtos;
using Financials.Models;
using Financials.Repositories;

namespace Financials.Services;

public class CsvImporterService
{
    private readonly ITransactionRepository _repository;
    public CsvImporterService(ITransactionRepository repository)
    {
        _repository = repository;
    }
    public int ImportTransactions(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<TransactionsToImport>();
        int recordsImported = 0;
        foreach (var record in records)
        {
            if (record.Date > _repository.GetLastDate())
            {
                var transaction = new Transaction
                {
                    Description = record.Description,
                    Date = record.Date,
                    Value = record.Value,
                    TagId = null,
                    BucketId = null 
                };
                _repository.Add(transaction);
                recordsImported++;
            }
        }
        return recordsImported;
    }
}