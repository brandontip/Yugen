namespace Yugen;

public class SMAStrategy
{
    public void Execute(Account account)
    {
        try
        {
            //todo
            // Get a list of all tradeable tickers
            // remove all tickers that are already in a position for this account
            var tickers = new List<string>();
            foreach (string ticker in tickers)
            {
                Console.WriteLine("Analyzing " + ticker);
                DataFrame data = Candles(ticker);
                DataFrame ohlc_df = Stochastic(data, 14, 3, 3);
                ohlc_df = SMA(ohlc_df, 100, 200);
                var signal = TradeSignal(ohlc_df, ticker);
                switch (signal)
                {
                    case "Buy":
                    {
                        Order newWorldOrder = new(ticker, pos_size,1, 3 * ATR(data, 120));
                        Console.WriteLine("New long position initiated for " + ticker);
                        break;
                    }
                    case "Sell":
                        break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    
    public string TradeSignal(DataFrame df, string curr)
    {
        // Function to generate signal
        Dictionary<string, bool> upward_sma_dir = new Dictionary<string, bool>();
        Dictionary<string, bool> dnward_sma_dir = new Dictionary<string, bool>();
        string signal = "";

        // 'sticky values'
        if (df["sma_fast"][-1] > df["sma_slow"][-1] && df["sma_fast"][-2] < df["sma_slow"][-2])
        {
            upward_sma_dir[curr] = true;
            dnward_sma_dir[curr] = false;
        }
        if (df["sma_fast"][-1] < df["sma_slow"][-1] && df["sma_fast"][-2] > df["sma_slow"][-2])
        {
            upward_sma_dir[curr] = false;
            dnward_sma_dir[curr] = true;
        }
        if (upward_sma_dir[curr] == true && Math.Min(df["K"][-1], df["D"][-1]) > 25 && Math.Max(df["K"][-2], df["D"][-2]) < 25)
        {
            signal = "Buy";
        }
        if (dnward_sma_dir[curr] == true && Math.Min(df["K"][-1], df["D"][-1]) > 75 && Math.Max(df["K"][-2], df["D"][-2]) < 75)
        {
            signal = "Sell";
        }

        return signal;
    }
    
    public DataFrame Stochastic(DataFrame df, int a, int b, int c)
    {
        // Function to calculate stochastic
        df["k"] = ((df["c"] - df["l"].Rolling(a).Min()) / (df["h"].Rolling(a).Max() - df["l"].Rolling(a).Min())) * 100;
        df["K"] = df["k"].Rolling(b).Mean();
        df["D"] = df["K"].Rolling(c).Mean();
        return df;
    }

    public DataFrame SMA(DataFrame df, int a, int b)
    {
        // Function to calculate SMA
        df["sma_fast"] = df["c"].Rolling(a).Mean();
        df["sma_slow"] = df["c"].Rolling(b).Mean();
        return df;
    }

    public class Candle
    {
        public double o { get; set; }
        public double h { get; set; }
        public double l { get; set; }
        public double c { get; set; }
        public int volume { get; set; }
    }

    public DataTable Candles(string instrument)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "count", 400 },
            { "granularity", "M5" }
        };

        // Make an API request to fetch instrument candles
        var candles = client.InstrumentsCandles(instrument, parameters);
        client.Request(candles);

        // Extract candle data from the response
        var ohlcDict = candles.Response["candles"];
        var ohlcList = ohlcDict.ToObject<List<Candle>>();

        // Create a DataTable to store the candle data
        DataTable ohlcDataTable = new DataTable();
        ohlcDataTable.Columns.Add("time", typeof(DateTime));
        ohlcDataTable.Columns.Add("open", typeof(double));
        ohlcDataTable.Columns.Add("high", typeof(double));
        ohlcDataTable.Columns.Add("low", typeof(double));
        ohlcDataTable.Columns.Add("close", typeof(double));
        ohlcDataTable.Columns.Add("volume", typeof(int));

        // Populate the DataTable with the candle data
        foreach (var candle in ohlcList)
        {
            DataRow row = ohlcDataTable.NewRow();
            row["time"] = candle.time;
            row["open"] = candle.o;
            row["high"] = candle.h;
            row["low"] = candle.l;
            row["close"] = candle.c;
            row["volume"] = candle.volume;
            ohlcDataTable.Rows.Add(row);
        }

        return ohlcDataTable;
    }
    
    public double ATR(DataFrame DF, int n)
    {
        // Function to calculate True Range and Average True Range
        DataFrame df = DF.Copy();
        df["H-L"] = df["h"] - df["l"];
        df["H-PC"] = df["h"] - df["c"].Shift(1);
        df["L-PC"] = df["l"] - df["c"].Shift(1);
        df["TR"] = df[["H-L", "H-PC", "L-PC"]].Max(axis: 1, skipna: false);
        df["ATR"] = df["TR"].Rolling(n).Mean();
        //df["ATR"] = df["TR"].Ewm(span: n, adjust: false, min_periods: n).Mean();
        DataFrame df2 = df.Drop(new List<string> { "H-L", "H-PC", "L-PC" });
        return Math.Round(df2["ATR"][-1], 2);
    }

}