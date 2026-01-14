namespace Pairox.Core.Models
{
    public class Pair
    {
        public string Ticker1 { get; set; }
        public string Ticker2 { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Path { get; set; }
        public double HedgeRatio{ get; set; }
        public double SpreadMean { get; set; }
        public double SpreadStd { get; set; }
        public double PValue { get; set; }
        public double Corr { get; set; }
        public double ZScore { get; set; }
        public double HalfLife { get; set; }
        public PairStatusEnum Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Comment { get; set; }
        public double Pnl { get; set; }
        public bool HasPosition { get; set; }
        public int Ticket1 { get; set; }
        public int Ticket2 { get; set; }
        public DateTime EntryTime { get; set; }
        public double EntryZScore { get; set; }
        public double Swap { get; set; }
    }
}
