using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSGrounds.Models
{
    public class GroundWave
    {
        public ushort PosX { get; set; }
        public ushort PosY { get; set; }
        public byte No { get; set; }
        public byte Dist { get; set; }
    }
}
