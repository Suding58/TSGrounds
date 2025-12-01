namespace TSGrounds
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
        }

        public void UpdateProgress(int value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => pb.Value = value));
            }
            else
            {
                pb.Value = value;
            }
        }

        public void UpdateText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => lblStatus.Text = text));
            }
            else
            {
                lblStatus.Text = text;
            }
        }
    }
}
