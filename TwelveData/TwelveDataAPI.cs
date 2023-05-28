namespace Yugen;
using Newtonsoft.Json;

//https://twelvedata.com/docs
//https://twelvedata.com/request-builder
//https://json2csharp.com/


// Limit 800 day
// Limit 8 per minute
public class TwelveDataApi
{
    public static TimeSeries GetTimeSeries(string ticker, DateTime startDate, DateTime endDate, TimeSeries.Interval interval)
    {
        var isValidCall = IsValidTimeConstraints(startDate, endDate, interval);
        if(!isValidCall) throw new Exception("Invalid time constraints sent to TwelveDataApi.GetTimeSeries");
        var client = new HttpClient();
        var url = StringBuildSeriesUrl(ticker, startDate, endDate, interval);
        var request = GetHttpRequest(url);
        using var response = client.SendAsync(request);
        response.Wait();
        var resultAsString = response.Result.Content.ReadAsStringAsync().Result;
        var timeseriesJsonStrings = JsonConvert.DeserializeObject<JSONStringClasses.TimeseriesJSONStrings>(resultAsString);
        if(timeseriesJsonStrings.status != "ok") throw new Exception("TwelveDataApi.GetTimeSeries returned status not ok");
        return new TimeSeries(timeseriesJsonStrings, startDate, endDate, interval);
    }

    private static bool IsValidTimeConstraints( DateTime startDate, DateTime endDate, TimeSeries.Interval interval)
    {
        //todo timezone conversion?return 9startDate-endDate)
        //todo assert market open
        return startDate < endDate;
    }

    private static string StringBuildSeriesUrl(string ticker, DateTime startDate, DateTime endDate, TimeSeries.Interval interval)
    {
        var intervalString = TimeSeries.IntervalToString(interval);
        var startString = startDate.ToString("yyyy-MM-dd HH:mm:ss");
        var endString = endDate.ToString("yyyy-MM-dd HH:mm:ss");
        //&symbol=ETH/BTC&timezone=Europe/Zurich&start_date=2019-08-09 15:50:00&end_date=2019-08-09 15:55:00&...

        var url = $"https://api.twelvedata.com/time_series?"
                  + $"symbol={ticker}"
                  + $"&interval={intervalString}"
                  + $"&apikey=demo&source=docs"
                  + $"&start_date={startString}"
                  + $"&end_date={endString}";
        return url;
    }

    private static HttpRequestMessage GetHttpRequest(string url)
    {
        return new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url),
            Headers =
            {
                { "X-RapidAPI-Key", "dce33204d0msheb94225c48ad0d0p19a65cjsn35382bad3901" },
                { "X-RapidAPI-Host", "twelve-data1.p.rapidapi.com" },
            },
        };
    }
}