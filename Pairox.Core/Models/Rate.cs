using System;
using System.Collections.Generic;
using System.Text;

namespace Pairox.Core.Models
{
    public class Rate
    {
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int Volume { get; set; }
        public DateTime Time { get; set; }
    }
}
