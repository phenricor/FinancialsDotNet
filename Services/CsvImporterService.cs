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
    public void ImportTransactions(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<TransactionsToImport>();
        foreach (var record in records)
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
        }
    }
}