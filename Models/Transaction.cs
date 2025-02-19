using System.ComponentModel.DataAnnotations;

namespace Financials.Models;

public partial class Transaction
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
    public int? TagId { get; set; }
    public int? BucketId { get; set; }
    public int Status { get; set; }

    public Transaction()
    {
        Description ??= "";
        Status = 0;
    }
}