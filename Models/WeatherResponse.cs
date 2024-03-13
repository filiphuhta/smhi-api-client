public class WeatherResponse
{
    public DateTime approvedTime { get; set; }
    public DateTime referenceTime { get; set; }
    public Geometry? geometry { get; set; }
    public List<TimeSeries>? timeSeries { get; set; }
}


