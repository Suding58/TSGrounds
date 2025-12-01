using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSGrounds.Models
{
    public class GroundField
    {
        public byte FieldId { get; set; }
        public ushort PosX1 { get; set; }
        public ushort PosY1 { get; set; }
        public ushort PosX2 { get; set; }
        public ushort PosY2 { get; set; }
    }
}
