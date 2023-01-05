namespace TestingModel.Models
{
    public class PerformanceMeasureHeader
    {
        AutoResetEvent resetEvent = new AutoResetEvent(true);
        public List<PerformanceMeasureNode> Nodes { get; set; } =
            new List<PerformanceMeasureNode>();
        public Guid Guid { get; set; } = Guid.NewGuid();
        public DateTime CreateAt { get; set; }= DateTime.Now;
        public string HeaderTitle { get; set; } = "";
        public int RequestEstimated { get; set; } = 0;
        public int MaxLatency { get; set; } = 0;
        public int NodeEstimated { get; set; } = 0;
        public string LatencyDetail { get; set; } = "";
        public string NodeEstimatedDetail { get; set; } = "";

        public PerformanceMeasureNode? BeginMeasure(string title,
            bool performanceMeasureAction = false)
        {
            if (performanceMeasureAction == false) return null;
            PerformanceMeasureNode node = new PerformanceMeasureNode()
            {
                Title = title,
                Begin = DateTime.Now,
                End = default(DateTime)
            };
            resetEvent.WaitOne();
            Nodes.Add(node);
            resetEvent.Set();
            return node;
        }

        public void EndMeasure(PerformanceMeasureNode node,
            bool performanceMeasureAction = false)
        {
            if (performanceMeasureAction == false) return;
            node.End = DateTime.Now;
        }
    }
}
