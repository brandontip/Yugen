namespace ConsoleApp1;

public class Order
{
    public string ticker { get; set; }
    public int quantity { get; set; }
    public DateTime targetEntryDate { get; set; }
    public bool isLong { get; set; }
}