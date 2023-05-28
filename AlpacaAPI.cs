using System.Runtime.CompilerServices;
using Alpaca.Markets;

namespace ConsoleApp1;

public class AlpacaAPI
{
    //cNydHZknmE57ahulDKpMIOr5XBnwUnYitJ4irWZK
    //PK8P0GBIQ931PWDFNDJD
    private const String KEY_ID = "PK8P0GBIQ931PWDFNDJD";
    private const String SECRET_KEY = "cNydHZknmE57ahulDKpMIOr5XBnwUnYitJ4irWZK";

    public static void ViewAccountTest()
    {
        var client = Environments.Paper
            .GetAlpacaTradingClient(new SecretKey(KEY_ID, SECRET_KEY));
        //var account = await client.GetAccountAsync();
        using (var account = client.GetAccountAsync())
        {
            account.Wait();
            Console.WriteLine(account.Result.BuyingPower);
        }
        Console.WriteLine(1);
    }
    
    public static void PlaceBuyOrderTest()
    {
        var client = Environments.Paper
            .GetAlpacaTradingClient(new SecretKey(KEY_ID, SECRET_KEY));
        //var account = await client.GetAccountAsync();
        using (var account = client.GetAccountAsync())
        {
            account.Wait();
            
            // Set order parameters
            String symbol = "AAPL";
            Int64 quantity = 1;

            // Placing buy order
            using (var buyOrder = client.PostOrderAsync(OrderSide.Buy.Market(symbol, quantity)))
            {
                buyOrder.Wait();
                var order = buyOrder.Result;
                Console.WriteLine(order.OrderId);
            }
        }
        Console.WriteLine(1);
    }
}