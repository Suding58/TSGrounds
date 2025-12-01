namespace TSGrounds.Models
{
    public class GroundBMP
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public Image? Image { get; set; }

        public GroundBMP()
        {
            FileName = string.Empty;
            Data = Array.Empty<byte>();
            Image = null;
        }

        public Image SET_IMG()
        {
            using (var ms = new MemoryStream(Data))
            {
                Image = Image.FromStream(ms);
            }
            return Image;
        }
    }
}

