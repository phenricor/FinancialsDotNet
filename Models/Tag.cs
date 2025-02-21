namespace Financials.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Tag()
    {
        Name ??= "";
    }
}