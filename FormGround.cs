using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TSGrounds.Models;

namespace TSGrounds
{
    public partial class FormGround : Form
    {
        private byte _currentBrushType = 0;
        private string _filePath { get; set; } = string.Empty;
        private Dictionary<ushort, Ground> _grounds { get; set; } = new();
        private Ground? _currentGround = null;
        private List<GroundBMP> _loadedImagesInRam = new();
        private bool _isDragging = false;
        private bool _isTS1 = false;
        public FormGround()
        {
            InitializeComponent();
        }

        private async Task LoadGroundImg(Ground? g)
        {
            if (g == null)
                return;

            // เลือกรูปที่ตรงกับ PicMapData
            g.Images = g.PicMapData
                .Select(pic =>
                    _loadedImagesInRam.FirstOrDefault(img =>
                        Path.GetFileNameWithoutExtension(img.FileName) == pic.Id.ToString()
                    )
                )
                .Where(img => img != null)
                .ToList()!;

            if (g.FinalImage == null)
            {
                foreach (var i in g.Images)
                    i.Image = null;
                await g.CombineImagesFitCanvas(cbTEXT_GROUND.Checked);
            }
        }

        private async Task refreshPictureGround()
        {
            if (_currentGround != null)
            {
                await LoadGroundImg(_currentGround);
                picBox_GROUND.Image = _currentGround.FinalImage;
                picBox_GROUND.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void PaintAtMousePosition(Point mouseLocation)
        {
            if (picBox_GROUND.Image == null || _currentGround == null || !groupBoxBRUSH.Enabled) return;

            Size containerSize = picBox_GROUND.ClientSize;
            Size imageSize = picBox_GROUND.Image.Size;

            float ratioX = (float)containerSize.Width / imageSize.Width;
            float ratioY = (float)containerSize.Height / imageSize.Height;
            float scale = Math.Min(ratioX, ratioY);

            float displayWidth = imageSize.Width * scale;
            float displayHeight = imageSize.Height * scale;

            float offsetX = (containerSize.Width - displayWidth) / 2;
            float offsetY = (containerSize.Height - displayHeight) / 2;

            // --- 2. แปลงพิกัด ---
            float realX = (mouseLocation.X - offsetX) / scale;
            float realY = (mouseLocation.Y - offsetY) / scale;

            if (realX < 0 || realX >= imageSize.Width ||
                realY < 0 || realY >= imageSize.Height) return;

            // --- 3. คำนวณ Grid Index ---
            float rowLimit = _currentGround.Grid.GetLength(0);
            float colLimit = _currentGround.Grid.GetLength(1);
            float cellSize = (float)(_currentGround.ImgWidth / rowLimit);

            int gridX = (int)(realX / cellSize);
            int gridY = (int)(realY / cellSize);

            // --- 4. ตรวจสอบขอบเขตและอัปเดตค่า ---
            if (gridX >= 0 && gridX < rowLimit &&
                gridY >= 0 && gridY < colLimit)
            {
                // *** OPTIMIZATION: เช็คก่อนว่าค่าเปลี่ยนจริงไหม เพื่อลดการกระพริบ ***
                if (_currentGround.Grid[gridX, gridY] != _currentBrushType)
                {
                    _currentGround.Grid[gridX, gridY] = _currentBrushType;

                    // สั่งวาดใหม่เฉพาะเมื่อมีการเปลี่ยนแปลง
                    picBox_GROUND.Invalidate();
                }
            }
        }

        private async void menuItem_LOAD_Click(object sender, EventArgs e)
        {
            var progressForm = new FormProgress();
            progressForm.Show();

            var progress = new Progress<int>(value => progressForm.UpdateProgress(value));

            var progressText = new Progress<string>(msg => progressForm.UpdateText(msg));

            var progressTitle = new Progress<string>(title => this.Text = title);
            var progressGroundCount = new Progress<int>(count => lbGROUND_NUM.Text = $"GROUNDS: {count}");

            await HandleMmgFile(progress, progressText, progressTitle, progressGroundCount);

            progressForm.Close();
        }

        public bool READ(string filePath, IProgress<int> progress, IProgress<string> progressText, IProgress<string> progressTitle, IProgress<int> progressGroundCount)
        {
            try
            {
                if (filePath == string.Empty)
                {
                    return false;
                }

                _filePath = filePath;
                var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                progressTitle?.Report($"TSGrounds - Look - v{version.Major}.{version.Minor} - {Path.GetFileName(_filePath)}");

                using (FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, data.Length);
                    fs.Close();
                    fs.Dispose();

                    using (MemoryStream stream = new MemoryStream(data))
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        br.BaseStream.Position = br.BaseStream.Length - 2;

                        short NumOfGroundFiles = br.ReadInt16();
                        br.BaseStream.Position = br.BaseStream.Length - 2 - NumOfGroundFiles * 0x1D;

                        int nameLength = !_isTS1 ? 20 : 16;
                        for (int _round = 0; _round < NumOfGroundFiles; _round++)
                        {
                            string name = Encoding.ASCII.GetString(br.ReadBytes(br.ReadByte()));
                            br.ReadBytes(nameLength - name.Length);

                            if (_isTS1)
                                br.ReadUInt32(); // Unk1 for ts1

                            int offset = br.ReadInt32();
                            int length = br.ReadInt32();
                            long temp = br.BaseStream.Position;
                            br.BaseStream.Position = offset;
                            byte[] rawGround = br.ReadBytes(length);
                            br.BaseStream.Position = temp;

                            var match = Regex.Match(name, @"^(\d+)");
                            ushort groundID = 0;
                            if (match.Success)
                                ushort.TryParse(match.Groups[1].Value, out groundID);


                            var ground = new Ground(groundID, rawGround, _isTS1);
                            _grounds.TryAdd(groundID, ground);

                            progress?.Report((_round + 1) * 100 / NumOfGroundFiles);
                            progressText?.Report($"Reading {_round + 1}/{NumOfGroundFiles}");
                        }
                        progressGroundCount?.Report(_grounds.Count);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการอ่านไฟล์: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async Task HandleMmgFile(IProgress<int> progress, IProgress<string> progressText, IProgress<string> progressTitle, IProgress<int> progressGroundCount)
        {
            try
            {
                string dataFolder = Path.Combine(Application.StartupPath, "data");

                if (!Directory.Exists(dataFolder))
                    Directory.CreateDirectory(dataFolder);

                string filePath = Path.Combine(dataFolder, "ground.mmg");

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("ไม่พบไฟล์ ground.mmg ในโฟลเดอร์ /data/",
                                    "ไม่พบไฟล์", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool result = await Task.Run(() => READ(
                    filePath,
                    progress,
                    progressText,
                    progressTitle,
                    progressGroundCount
                ));

                if (!result)
                {
                    MessageBox.Show("ไม่สามารถอ่านไฟล์ ground.mmg ได้", "Error");
                    return;
                }

                await Task.Delay(500);
                refreshDvg();
                LoadPathImages();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void refreshDvg()
        {
            dvgGROUND.DataSource = null;
            if (_grounds != null)
            {
                dvgGROUND.DataSource = _grounds.Values.ToList();
                DvgUtility.HideUnwantedColumns(dvgGROUND);
            }
        }

        public void LoadPathImages()
        {
            // path โฟลเดอร์ /pic/ ข้าง exe
            string path = Path.Combine(Application.StartupPath, "pic");

            // ถ้าโฟลเดอร์ไม่มี ให้สร้าง
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            foreach (var img in _loadedImagesInRam) img.Image?.Dispose();
            _loadedImagesInRam.Clear();

            // หาไฟล์ .JMG และ .BZP
            var allowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jmg", ".bzp" };

            var archiveFiles = Directory.GetFiles(path)
                .Where(file => allowedExtensions.Contains(Path.GetExtension(file)))
                .ToList();

            if (archiveFiles.Count == 0)
            {
                MessageBox.Show("ไม่พบไฟล์ .JMG หรือ .BZP ในโฟลเดอร์ /pic/",
                                "ไม่พบไฟล์", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- วนลูปแตกไฟล์ JMG/BZP ---
            foreach (string file in archiveFiles)
            {
                try
                {
                    using (BinaryReader b = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        int start = 2;
                        int qtyfile = 0;
                        int amount = 0;

                        b.BaseStream.Seek(0, SeekOrigin.Begin);
                        qtyfile = BitConverter.ToInt16(b.ReadBytes(2), 0);

                        while (true)
                        {
                            if (start >= b.BaseStream.Length || amount >= qtyfile)
                                break;

                            b.BaseStream.Seek(start, SeekOrigin.Begin);
                            int nle = b.ReadByte();
                            if (nle <= 0) break;

                            byte[] name = b.ReadBytes(nle);

                            b.BaseStream.Seek(start + 24, SeekOrigin.Begin);
                            int offset = BitConverter.ToInt32(b.ReadBytes(4));

                            b.BaseStream.Seek(start + 28, SeekOrigin.Begin);
                            int dataSize = BitConverter.ToInt32(b.ReadBytes(4));

                            b.BaseStream.Seek(offset, SeekOrigin.Begin);
                            byte[] data = b.ReadBytes(dataSize);

                            string namefile = Encoding.ASCII.GetString(name).Trim('\0');

                            string ext = Path.GetExtension(namefile).ToLower();

                            if (ext == ".png" || ext == ".jpg" || ext == ".bmp" || ext == ".gif")
                            {
                                _loadedImagesInRam.Add(new GroundBMP()
                                {
                                    FileName = namefile,
                                    Data = data
                                });
                            }

                            start += 32;
                            amount++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error unpacking {file}: {ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (_loadedImagesInRam.Count > 0)
            {
                MessageBox.Show($"โหลดเสร็จสิ้น! พบรูปภาพทั้งหมด {_loadedImagesInRam.Count} รูป",
                                "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                picBox_GROUND.Invalidate();
            }
            else
            {
                MessageBox.Show("แตกไฟล์สำเร็จ แต่ไม่พบไฟล์รูปภาพข้างใน (หรือนามสกุลไม่รองรับ)",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picBox_GROUND_Paint(object sender, PaintEventArgs e)
        {
            // 1. ตรวจสอบความถูกต้องของข้อมูล
            if (picBox_GROUND.Image == null || _currentGround == null) return;

            Graphics g = e.Graphics;

            // --- ส่วนสำคัญ: คำนวณ Scale และ Offset สำหรับ Mode Zoom ---
            Size containerSize = picBox_GROUND.ClientSize;
            Size imageSize = picBox_GROUND.Image.Size;

            // หาอัตราส่วนว่าด้านไหนชนขอบก่อน (Width หรือ Height)
            float ratioX = (float)containerSize.Width / imageSize.Width;
            float ratioY = (float)containerSize.Height / imageSize.Height;
            float scale = Math.Min(ratioX, ratioY); // ใช้ค่าน้อยสุดเพื่อให้รูปไม่เกินขอบ

            // คำนวณขนาดรูปจริงที่แสดงผล
            float displayWidth = imageSize.Width * scale;
            float displayHeight = imageSize.Height * scale;

            // คำนวณจุดเริ่มต้น (Offset) เพื่อให้รูปอยู่กึ่งกลาง (Center)
            float offsetX = (containerSize.Width - displayWidth) / 2;
            float offsetY = (containerSize.Height - displayHeight) / 2;

            // สั่งให้ Graphics เริ่มวาดที่จุด Offset และขยายขนาดตาม Scale
            g.TranslateTransform(offsetX, offsetY);
            g.ScaleTransform(scale, scale);

            // *หมายเหตุ: ไม่ต้องสั่ง g.DrawImage ซ้ำ เพราะ PictureBox วาดพื้นหลังให้อยู่แล้ว*
            // --------------------------------------------------------

            float rowLimit = _currentGround.Grid.GetLength(0); // ค่า Limit แกน X (ตามโค้ดเดิมของคุณ)
            float colLimit = _currentGround.Grid.GetLength(1); //  ค่า Limit แกน Y (ตามโค้ดเดิมของคุณ)
            float cellSize = (float)(_currentGround.ImgWidth / rowLimit);//20.00f;

            // เพื่อความสวยงามเมื่อ Zoom: ทำให้ขอบคมชัดขึ้น (Optional)
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            // 2. วาด Cell Highlight
            // (โค้ดส่วนนี้ใช้ Logic เดิม แต่พิกัดจะถูกปรับอัตโนมัติตาม Scale ด้านบน)
            if (cbLAYER.Checked)
            {
                if (rbLAYER_COLLISION.Checked)
                {
                    for (int x = 0; x < rowLimit; x++)
                    {
                        for (int y = 0; y < colLimit; y++)
                        {
                            int type = _currentGround.Grid[x, y];
                            Color fill = Color.Transparent;

                            if (type == 0) fill = Color.FromArgb(60, Color.Green);
                            else if (type == 1) fill = Color.FromArgb(60, Color.Red);
                            else if (type == 2) fill = Color.FromArgb(60, Color.Blue);

                            if (fill != Color.Transparent)
                            {
                                using (SolidBrush brush = new SolidBrush(fill))
                                {
                                    g.FillRectangle(brush, x * cellSize, y * cellSize, cellSize, cellSize);
                                }
                            }
                        }
                    }
                }
            }


            if (cbGRID.Checked)
            {
                // 3. วาดเส้น Grid
                // ปรับปรุง Logic การวาดเส้นให้ครอบคลุมตามขนาด Loop ที่ถูกต้อง
                float totalWidth = rowLimit * cellSize;
                float totalHeight = colLimit * cellSize;

                using (Pen pen = new Pen(Color.FromArgb(60, Color.Black), 1))
                {
                    // ป้องกันเส้นเบลอเมื่อ Zoom ด้วยการปรับความหนาเส้นกลับ (Optional: ถ้าอยากให้เส้นเท่าเดิมตลอด)
                    pen.Width = 1 / scale;

                    // วาดเส้นแนวตั้ง (Vertical Lines)
                    for (int x = 0; x <= rowLimit; x++)
                    {
                        g.DrawLine(pen, x * cellSize, 0, x * cellSize, totalHeight);
                    }

                    // วาดเส้นแนวนอน (Horizontal Lines)
                    for (int y = 0; y <= colLimit; y++)
                    {
                        g.DrawLine(pen, 0, y * cellSize, totalWidth, y * cellSize);
                    }
                }
            }


            // ลบการตั้งค่า SizeMode ภายใน Paint event ออก เพื่อป้องกันการกระพริบหรือ Loop
            picBox_GROUND.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void picBox_GROUND_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // เช็คว่าเป็นคลิกซ้าย
            {
                _isDragging = true;
                PaintAtMousePosition(e.Location);
            }
        }

        private void picBox_GROUND_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void picBox_GROUND_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                PaintAtMousePosition(e.Location);
            }
        }

        private void btnSEARCH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbSEARCH.Text))
            {
                MessageBox.Show("กรุณากรอก GROUND ID ที่ต้องการค้นหา", "ข้อมูลไม่ครบถ้วน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbSEARCH.Focus();
                return;
            }

            if (!ushort.TryParse(txtbSEARCH.Text, out ushort searchId))
            {
                MessageBox.Show("GROUND ID ต้องเป็นตัวเลขเท่านั้น", "ข้อมูลไม่ถูกต้อง", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtbSEARCH.Focus();
                return;
            }

            var found = _grounds.Values.FirstOrDefault(g => g.Id == searchId);
            if (found == null)
            {
                MessageBox.Show($"ไม่พบ GROUND ที่มี MAP ID '{searchId}'", "ไม่พบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dvgGROUND.ClearSelection();
                foreach (DataGridViewRow row in dvgGROUND.Rows)
                {
                    var gInRow = row.DataBoundItem as Ground;
                    if (gInRow != null && gInRow.Id == found.Id)
                    {
                        row.Selected = true;
                        dvgGROUND.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
        }

        private void rdBRUSH_NONE_CheckedChanged(object sender, EventArgs e)
        {
            _currentBrushType = 1;
        }

        private void rdBRUSH_OVER_CheckedChanged(object sender, EventArgs e)
        {
            _currentBrushType = 0;
        }

        private void rdBRUSH_WATER_CheckedChanged(object sender, EventArgs e)
        {
            _currentBrushType = 2;
        }

        private void cbGRID_CheckedChanged(object sender, EventArgs e)
        {
            picBox_GROUND.Invalidate();
            groupBoxBRUSH.Enabled = cbGRID.Checked;
        }

        private void cbLAYER_CheckedChanged(object sender, EventArgs e)
        {
            picBox_GROUND.Invalidate();
        }

        private async void cbTEXT_GROUND_CheckedChanged(object sender, EventArgs e)
        {
            if (_currentGround == null)
                return;

            foreach (var g in _grounds.Values.Where(x => x.FinalImage != null))
            {
                g.FinalImage = null;
                g.Images.Clear();
            }

            await LoadGroundImg(_currentGround);
            picBox_GROUND.Invalidate();
        }

        private void dvgGROUND_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dvgGROUND.SelectedRows.Count > 0)
                {
                    _currentGround = dvgGROUND.SelectedRows[0].DataBoundItem as Ground;
                }
                refreshPictureGround();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuItem_EXPORT_IMG_Click(object sender, EventArgs e)
        {
            if (_currentGround == null || _currentGround.FinalImage == null)
            {
                MessageBox.Show("กรุณาเลือก GROUND ที่ต้องการส่งออก", "ไม่มีข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "บันทึกไฟล์ GROUND IMG";
                sfd.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp";
                sfd.FileName = $"{_currentGround.Id}.png";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // เลือก format ตามนามสกุลไฟล์
                        System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
                        string ext = Path.GetExtension(sfd.FileName).ToLower();
                        if (ext == ".jpg" || ext == ".jpeg") format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        else if (ext == ".bmp") format = System.Drawing.Imaging.ImageFormat.Bmp;

                        _currentGround.FinalImage.Save(sfd.FileName, format);

                        MessageBox.Show("ส่งออกไฟล์ GROUND IMG สำเร็จ!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"เกิดข้อผิดพลาดในการส่งออกไฟล์: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void FormGround_Load(object sender, EventArgs e)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Text = $"TSGrounds - Look - v{version.Major}.{version.Minor}";
        }

        private void checkBoxVersion_CheckedChanged(object sender, EventArgs e)
        {
            _isTS1 = checkBoxVersion.Checked;
        }

        private void linkLabelDEV_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabelDEV.LinkVisited = true;
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/Suding58",
                UseShellExecute = true
            });
        }
    }
}
