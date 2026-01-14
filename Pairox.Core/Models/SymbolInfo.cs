namespace Pairox.Core.Models
{
    public class SymbolInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public double volumeMin { get; set; }
        public double VolumeMax { get; set; }
        public double VolumeStep { get; set; }
        public double Point { get; set; }
        public double Ask{ get; set; }
        public double Bid { get; set; }
        public double Spread { get; set; }
        public double Digits { get; set; }
        public int FillingMode { get; set; }
        public int Time { get; set; }
        public bool Select { get; set; }
    }
}
