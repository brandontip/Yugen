namespace Yugen;


public class Order
{
    public string ticker { get; set; }
    public int quantity { get; set; }
    public DateTime targetEntryDate { get; set; }
    public bool isLong { get; set; }
    public double entryPrice { get; set; }
    public double stopLoss { get; set; }


    public Order(string ticker, double price, int quantity, double stopLoss)
    {
        this.ticker = ticker;
        this.quantity = quantity;
        this.stopLoss = stopLoss;
        entryPrice = price;
    }
        // "price": "",
        // "stopLossOnFill": {
        //     "trailingStopLossOnFill": "GTC",
        //     "distance": str(sl)
        // },
        // "timeInForce": "FOK",
        // "instrument": str(instrument),
        // "units": str(units),
        // "type": "MARKET",
        // "positionFill": "DEFAULT"
}