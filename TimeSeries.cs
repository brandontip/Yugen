namespace Yugen;


public class TimeSeries
{
    DateTime startDate { get; set; }
    DateTime endDate { get; set; }
    string ticker { get; set; }
    Interval interval { get; set; }
    public List<MarketSnapshot > series { get; set; }


    public TimeSeries(JSONStringClasses.TimeseriesJSONStrings data, DateTime startDate, DateTime endDate, Interval interval)
    {
        this.startDate = startDate;
        this.endDate = endDate;
        this.interval = interval;
        ticker = data.meta.symbol;
        series = new List<MarketSnapshot>();
        foreach (var snapshot in data.values)
        {
            series.Add(new MarketSnapshot(snapshot, ticker));
        }
    }
    
    public struct MarketSnapshot
    {
        public string ticker { get; set; }
        public DateTime Datetime { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }

        public MarketSnapshot(JSONStringClasses.TickerMarketSnapshot allStrings, string ticker)
        {
            this.ticker = ticker;
            Datetime = DateTime.Parse(allStrings.datetime);
            Open = double.Parse(allStrings.open);
            High = double.Parse(allStrings.high);
            Low = double.Parse(allStrings.low);
            Close = double.Parse(allStrings.close);
            Volume = double.Parse(allStrings.volume);
        }
    }
    
    public int IndexForDate(DateTime date)
    {
        throw new NotImplementedException();
    }
    
    public enum Interval
    {
        Min1, 
        Min5,
        Min15,
        Min30, 
        Min45, 
        Hr1, 
        Hr2, 
        Hr4, 
        Day1, 
        Week1, 
        Month1  
    }
    
    public static string IntervalToString(Interval interval)
    {
        return interval switch
        {
            Interval.Min1 => "1min",
            Interval.Min5 => "5min",
            Interval.Min15 => "15min",
            Interval.Min30 => "30min",
            Interval.Min45 => "45min",
            Interval.Hr1 => "1h",
            Interval.Hr2 => "2h",
            Interval.Hr4 => "4h",
            Interval.Day1 => "1day",
            Interval.Week1 => "1week",
            Interval.Month1 => "1month",
            _ => throw new NotImplementedException()
        };
    }
}