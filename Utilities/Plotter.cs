namespace Yugen;


using Plotly.NET;
using Plotly.NET.LayoutObjects;
using Microsoft.FSharp.Collections;

public class Plotter
{
    public static void PlotScatter(IEnumerable<float> xs, IEnumerable<float> ys)
    {
        var title = Title.init (Text : "A Plotly Fire");

        var layout = Layout.init<IConvertible>(Title:title, PlotBGColor : Color.fromString("#e5ecf6"));

        var xAxis = LinearAxis.init<IConvertible, IConvertible, IConvertible, IConvertible, IConvertible, IConvertible>(
            Title:Title.init("xAxis"),
            ZeroLineColor:Color.fromString("#ffff"),
            GridColor:Color.fromString("#ffff"),
            ZeroLineWidth:2);

        var yAxis = LinearAxis.init<IConvertible, IConvertible, IConvertible, IConvertible, IConvertible, IConvertible>(
            Title:Title.init("yAxis"),
            ZeroLineColor:Color.fromString("#ffff"),
            GridColor:Color.fromString("#ffff"),
            ZeroLineWidth:2);

        var ret =Chart2D.Chart
            .Scatter<float, float, string>(x: xs, y: ys, mode: StyleParam.Mode.Markers,  Name : "Scatter")
            .WithLayout(layout)
            .WithXAxis(xAxis)
            .WithYAxis(yAxis);
        ret.Show();
    }

    public static void PlotLine(IEnumerable<float> xss)
    {
        var ys = Enumerable.Range(0, xss.Count()).Select(v=>(float)v).ToList();
        PlotScatter(ys, xss );
    }
    public static void PlotBar()
    {
        LinearAxis xAxis = new LinearAxis();
        xAxis.SetValue("title", "xAxis");
        xAxis.SetValue("zerolinecolor", "#ffff");
        xAxis.SetValue("gridcolor", "#ffff");
        xAxis.SetValue("showline", true);
        xAxis.SetValue("zerolinewidth",2);

        LinearAxis yAxis = new LinearAxis();
        yAxis.SetValue("title", "yAxis");
        yAxis.SetValue("zerolinecolor", "#ffff");
        yAxis.SetValue("gridcolor", "#ffff");
        yAxis.SetValue("showline", true);
        yAxis.SetValue("zerolinewidth",2);

        Layout layout = new Layout();
        layout.SetValue("xaxis", xAxis);
        layout.SetValue("yaxis", yAxis);
        layout.SetValue("title", "A Figure Specified by DynamicObj");
        layout.SetValue("plot_bgcolor", "#e5ecf6");
        layout.SetValue("showlegend", true);

        Trace trace = new Trace("bar");
        trace.SetValue("x", new []{1,2,3});
        trace.SetValue("y", new []{1,3,2});


        var fig = GenericChart.Figure.create(ListModule.OfSeq(new []{trace}),layout);
        var w= GenericChart.fromFigure(fig);
        w.Show();
        
        
        
    }
}