namespace Financials.Models;

public class DescriptionTagMap
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int TagId { get; set; }

    public DescriptionTagMap()
    {
        Description ??= "";
    }
}