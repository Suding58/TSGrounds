using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;

namespace TSGrounds.Models
{
    public class Ground
    {
        public int ImgWidth { get; set; }
        public int ImgHeight { get; set; }
        public List<GroundImage> PicMapData { get; set; } = new();
        public byte[,] Grid { get; set; }

        public List<GroundWave> Waves { get; set; } = new();
        public List<GroungObject> Objects { get; set; } = new();

        public byte Unk1 { get; set; }

        public byte FieldDefault { get; set; }
        public List<GroundField> Fields { get; set; } = new();

        public string GroundName { get; set; } = string.Empty;

        public uint GmX { get; set; }
        public uint GmY { get; set; }
        public byte Unk2 { get; set; }
        public byte Unk3 { get; set; }
        public byte Unk4 { get; set; }
        public byte Unk5 { get; set; }
        public byte Unk6 { get; set; }

        public string GroundSound { get; set; } = string.Empty;

        // Not include in ground structure
        public ushort Id { get; set; }
        public List<GroundBMP> Images { get; set; } = new();
        public Image? FinalImage { get; set; } = null;
        private bool IsTS1 = false;

        public Ground() { }

        public Ground(ushort groundID, byte[] raw, bool isTs1 = false)
        {
            using (BinaryReader br = new BinaryReader(new MemoryStream(raw)))
            {
                Id = groundID;
                IsTS1 = isTs1;

                ImgWidth = br.ReadInt32();
                ImgHeight = br.ReadInt32();
                var numOfPicMap = br.ReadByte();

                for (int i = 0; i < numOfPicMap; i++)
                {
                    PicMapData.Add(new GroundImage()
                    {
                        Id = br.ReadUInt16(),
                        X = br.ReadByte(),
                        W = br.ReadByte(),
                        Y = br.ReadByte(),
                        H = br.ReadByte(),
                    });
                }

                PicMapData = PicMapData.OrderBy(img => img.H).ThenBy(img => img.W).ToList();

                ushort gridRow = br.ReadUInt16();
                ushort gridCol = br.ReadUInt16();

                byte[] data = br.ReadBytes(gridRow * gridCol);

                Grid = new byte[gridRow, gridCol];
                int index = 0;
                for (int i = 0; i < gridRow; i++)
                {
                    for (int j = 0; j < gridCol; j++)
                    {
                        Grid[i, j] = data[index++];
                    }
                }


                ushort countWave = br.ReadUInt16();
                for (int i = 0; i < countWave; i++)
                    Waves.Add(new GroundWave()
                    {
                        PosX = br.ReadUInt16(),
                        PosY = br.ReadUInt16(),
                        No = br.ReadByte(),
                        Dist = br.ReadByte(),
                    });

                ushort countObject = br.ReadUInt16();
                for (int i = 0; i < countObject; i++)
                {
                    Objects.Add(new GroungObject()
                    {
                        ImageName = br.ReadUInt32(),
                        X = br.ReadUInt16(),
                        Y = br.ReadUInt16(),
                    });
                }

                Unk1 = br.ReadByte();
                FieldDefault = br.ReadByte();

                byte fieldCount = br.ReadByte();
                for (int j = 0; j < fieldCount; j++)
                {
                    Fields.Add(new GroundField() {
                        FieldId = br.ReadByte(),
                        PosX1 = br.ReadUInt16(),
                        PosY1 = br.ReadUInt16(),
                        PosX2 = br.ReadUInt16(),
                        PosY2 = br.ReadUInt16()
                    });
                }

                int leghtNameMax = !isTs1 ? 20 : 9;
                byte leghtName = br.ReadByte();
                GroundName = Encoding.ASCII.GetString(br.ReadBytes(leghtNameMax)).Replace("\0", "");

                GmX = br.ReadUInt32();
                GmY = br.ReadUInt32();

                Unk2 = br.ReadByte();
                Unk3 = br.ReadByte();
                Unk4 = br.ReadByte();

                if(!isTs1)
                {
                    Unk5 = br.ReadByte();
                    Unk6 = br.ReadByte();

                    byte leghtSound = br.ReadByte();
                    GroundSound = Encoding.ASCII.GetString(br.ReadBytes(leghtSound));
                }
            }

        }

        public async Task CombineImagesFitCanvas(bool showText)
        {
            try
            {
                if (FinalImage != null || Images.Count == 0)
                    return;

                int count = Images.Count;

                if (Images[0].Image == null)
                    Images[0].SET_IMG();

                int cols = ImgWidth / Images[0].Image.Width;
                int rows = ImgHeight / Images[0].Image.Height;

                if (cols == 0) cols = 1;
                if (rows == 0) rows = 1;

                int cellW = ImgWidth / cols;
                int cellH = ImgHeight / rows;

                FinalImage = new Bitmap(ImgWidth, ImgHeight);

                List<GroundRectangle> _groundRectangle = new();

                using (Graphics g = Graphics.FromImage(FinalImage))
                {
                    g.Clear(Color.Black);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    int position = 0;

                    for (int i = 0; i < count; i++)
                    {
                        var item = Images[i];

                        if (item.Image == null)
                            item.SET_IMG();

                        Image img = item.Image;
                        var picData = PicMapData[i];

                        // scale factor ≤ 1.0
                        double scaleW = (double)cellW / img.Width;
                        double scaleH = (double)cellH / img.Height;
                        double scale = Math.Min(Math.Min(scaleW, scaleH), 1.0);

                        int newW = (int)(img.Width * scale);
                        int newH = (int)(img.Height * scale);

                        // คำนวณตำแหน่งปกติ
                        int col = position % cols;
                        int row = position / cols;
                        int offX = col * cellW + (cellW - newW) / 2;
                        int offY = row * cellH + (cellH - newH) / 2;

                        var match = _groundRectangle.FirstOrDefault(gr => gr.Default == new Rectangle(picData.X, picData.Y, picData.W, picData.H));
                        if (match != null)
                        {
                            g.DrawImage(img, match.Current);
                        }
                        else
                        {
                            // วางตาม grid
                            g.DrawImage(img, new Rectangle(offX, offY, newW, newH));
                            _groundRectangle.Add(new GroundRectangle()
                            {
                                Default = new Rectangle(picData.X, picData.Y, picData.W, picData.H),
                                Current = new Rectangle(offX, offY, newW, newH)
                            });
                            position++;
                        }

                        if(showText)
                        {
                            // --------------------------
                            // ★ วาดข้อความชื่อรูป
                            // --------------------------
                            string text = $"{item.FileName} X:{picData.X} W:{picData.W} Y:{picData.Y} H:{picData.H}";
                            if (!string.IsNullOrEmpty(text))
                            {
                                using (Font font = new Font("Segoe UI", !IsTS1 ? 40 : 18, FontStyle.Bold))
                                using (Brush brush = new SolidBrush(Color.White))
                                using (Brush outline = new SolidBrush(Color.Black))
                                {
                                    float textX = (match != null ? match.Current.X : offX) + 10;
                                    float textY = (match != null ? match.Current.Y : offY) + 10;

                                    SizeF size = g.MeasureString(text, font);

                                    g.DrawString(text, font, outline, textX + 2, textY + 2);
                                    g.DrawString(text, font, brush, textX, textY);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ID: {Id} Error in CombineImagesFitCanvas: {ex.Message}");
            }
        }
    }

}
