namespace Pairox.Core.Models
{
    public class Settings
    {
        public string[] Filters { get; set; } = [];
        public string[] Groups { get; set; } = [];
        public int StopLossPercent { get; set; } = 3;
        public int TakeProfitPercent { get; set; } = 3;
        public int RiskPerTrade { get; set; } = 1;
        public int LookbackPeriod { get; set; } = 1000;
        public int RollingWindow { get; set; } = 120;
        public string Timeframe { get; set; } = "H1";
        public int MagicNumber { get; set; } = 13666;
        public string Comment { get; set; } = "Pairox";
    }
}
