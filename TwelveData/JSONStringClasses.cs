namespace Yugen;

public class JSONStringClasses
{
    
    public class SymbolMeta
    {
        public string symbol { get; set; }
        public string interval { get; set; }
        public string currency { get; set; }
        public string exchange_timezone { get; set; }
        public string exchange { get; set; }
        public string mic_code { get; set; }
        public string type { get; set; }
    }

    public class TimeseriesJSONStrings
    {
        public SymbolMeta meta { get; set; }
        public List<TickerMarketSnapshot> values { get; set; }
        public string status { get; set; }
    }

    public class TickerMarketSnapshot
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
        public string Symbol { get; set; }
        public string InstrumentName { get; set; }
        public string Exchange { get; set; }
        public string MicCode { get; set; }
        public string ExchangeTimezone { get; set; }
        public string InstrumentType { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
    }

    public class ExchangeRoot
    {
        public List<SymbolDatum> Data { get; set; }
        public string Status { get; set; }
    }
    
}