namespace TestingModel.Models
{
    public class PerformanceMeasureNode
    {
        public string Title { get; set; } = "";
        public DateTime Begin { get; set; } = default(DateTime);
        public DateTime End { get; set; } = default(DateTime);
        public TimeSpan EstimatedTime
        {
            get { return End - Begin; }
        }
        public int Latency { get; set; } = 0;
        public string LatencyDetail { get; set; } = "";
        public List<PerformanceMeasureHeader> SubHeader { get; set; } = new List<PerformanceMeasureHeader>();
    }
}
