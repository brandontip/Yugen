namespace ConsoleApp1;
using Newtonsoft.Json;

//https://twelvedata.com/docs
//https://twelvedata.com/request-builder
//https://json2csharp.com/


// Limit 800 day 8 per minute
public class TweleveDataAPI
{
    public static async void t1()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://api.twelvedata.com/time_series?symbol=AAPL&interval=1min&apikey=demo&source=docs"),
            Headers =
            {
                { "X-RapidAPI-Key", "dce33204d0msheb94225c48ad0d0p19a65cjsn35382bad3901" },
                { "X-RapidAPI-Host", "twelve-data1.p.rapidapi.com" },
            },
        };
        using (var response = client.SendAsync(request))
        {
            response.Wait();
            var t = response.Result.Content;
            var rast = t.ReadAsStringAsync();
            var q= rast.Result;
 
           
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(q);

        }
    }

    public static async void t2()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://twelve-data1.p.rapidapi.com/symbol_search?outputsize=30&symbol=AA"),
            Headers =
            {
                { "X-RapidAPI-Key", "dce33204d0msheb94225c48ad0d0p19a65cjsn35382bad3901" },
                { "X-RapidAPI-Host", "twelve-data1.p.rapidapi.com" },
            },
        };
        using (var response = client.SendAsync(request))
        {
            response.Wait();
            var t = response.Result.Content;
            var rast = t.ReadAsStringAsync();
            var q= rast.Result;
 
           
           ExchangeRoot myDeserializedClass = JsonConvert.DeserializeObject<ExchangeRoot>(q);
        }
    }
}

public class Meta
{
    public string symbol { get; set; }
    public string interval { get; set; }
    public string currency { get; set; }
    public string exchange_timezone { get; set; }
    public string exchange { get; set; }
    public string mic_code { get; set; }
    public string type { get; set; }
}

public class Root
{
    public Meta meta { get; set; }
    public List<Value> values { get; set; }
    public string status { get; set; }
}

public class Value
{
    public string datetime { get; set; }
    public string open { get; set; }
    public string high { get; set; }
    public string low { get; set; }
    public string close { get; set; }
    public string volume { get; set; }
}

public class SymbolDatum
{
    public string symbol { get; set; }
    public string instrument_name { get; set; }
    public string exchange { get; set; }
    public string mic_code { get; set; }
    public string exchange_timezone { get; set; }
    public string instrument_type { get; set; }
    public string country { get; set; }
    public string currency { get; set; }
}

public class ExchangeRoot
{
    public List<SymbolDatum> data { get; set; }
    public string status { get; set; }
}
