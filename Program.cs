// See https://aka.ms/new-console-template for more information

using Yugen;

//  var nums = Enumerable.Range(0, 1500);
//  var r = new System.Random();
//  var xs = new List<float>();
//  var ys = new List<float>();
//  for (var i  = 0; i < 20;i++)
//  {
//      xs.Add((float)r.NextDouble());
//      ys.Add((float)r.NextDouble());
//  }
// Plotter.PlotScatter(xs, ys);

//TweleveDataAPI.t1();
//
// AlpacaAPI.ViewAccountTest();
// AlpacaAPI.PlaceBuyOrderTest();



TweleveDataApi.GetTimeSeries("AAPL",new DateTime(2023, 5, 26), new DateTime(2023, 5, 27), TimeSeries.Interval.Hr1);

