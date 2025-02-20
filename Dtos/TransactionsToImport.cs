using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace Financials.Dtos;

public class TransactionsToImport
{
        [Index(3)]
        public string Description { get; set; }
        [Index(1)]
        public decimal Value { get; set; }
        [Format("dd/MM/yyyy")]
        [Index(0)]
        public DateTime Date { get; set; }

        public TransactionsToImport()
        {
            Description ??= "";
        }
}