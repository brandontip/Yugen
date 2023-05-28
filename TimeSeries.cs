namespace ConsoleApp1;

public class TimeSeries
{
    DateTime startDate { get; set; }
    DateTime endDate { get; set; }
    string ticker { get; set; }
    DateTime interval { get; set; }
    List<double> open { get; set; }
    List<double> high { get; set; }
    List<double> low { get; set; }
    List<double> close { get; set; }
    List<double> volume { get; set; }
}