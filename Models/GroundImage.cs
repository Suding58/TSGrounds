using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSGrounds.Models
{
    public class GroundImage
    {
        public ushort Id { get; set; }
        public byte X { get; set; } //offset แนวนอน (เริ่มที่ 0)
        public byte W { get; set; } //offset ความกว้าง (เพิ่มทีละ tileWidth เช่น 4, 8,...)
        public byte Y { get; set; } //offset แนวตั้ง (เริ่มที่ 0)
        public byte H { get; set; } //offset ความสูง (เพิ่มทีละ tileHeight เช่น 3, 6,...)

        public GroundImage()
        {

        }
    }
}
