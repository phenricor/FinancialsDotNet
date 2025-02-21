using CsvHelper.Configuration.Attributes;
using Financials.Dtos;

namespace Financials.ViewModels;

public class TransactionViewModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }
    public string Date { get; set; }
    public string? Tag { get; set; }
    public string? Bucket { get; set; }
    public string Status { get; set; }

    public TransactionViewModel()
    {
        Status = Status == "0" ? "Pending" : "Done";
        Tag ??= "";
        Bucket ??= "";
    }
}