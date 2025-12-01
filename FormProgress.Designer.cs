namespace TSGrounds
{
    partial class FormProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgress));
            pb = new ProgressBar();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // pb
            // 
            pb.Location = new Point(12, 44);
            pb.Name = "pb";
            pb.Size = new Size(265, 17);
            pb.TabIndex = 0;
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(12, 17);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(265, 23);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "label1";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormProgress
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(290, 85);
            ControlBox = false;
            Controls.Add(lblStatus);
            Controls.Add(pb);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormProgress";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormProgress";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar pb;
        private Label lblStatus;
    }
}