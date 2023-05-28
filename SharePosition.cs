namespace Yugen;

public class SharePosition
{
    string ticker { get; set; }
    int quantity { get; set; }
    DateTime entryDate { get; set; }
    bool isLong { get; set; }
}