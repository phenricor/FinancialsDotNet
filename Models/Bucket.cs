namespace Financials.Models;

public class Bucket 
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Bucket()
    {
        Name ??= "";
    }
}