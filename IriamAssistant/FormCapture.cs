using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IriamAssistant
{
    public partial class FormCapture : Form
    {
        public FormCapture()
        {
            InitializeComponent();
        }

        private void FormCapture_Load(object sender, EventArgs e)
        {
            // フォーム全体を透過する
            //            this.TransparencyKey = this.BackColor;
        }

        public Image CaptureImage { get => pictureBox_Capture.Image; set => pictureBox_Capture.Image = value; }
        public Image AnalyzeImage { get => pictureBox_Analize.Image; set => pictureBox_Analize.Image = value; }

        public Bitmap CaptureText(IntPtr hWnd)
        {
            Debug.Assert(hWnd != IntPtr.Zero);

            var capture = new CaptureWindow();
            var img = capture.TextCapture(hWnd);
            pictureBox_Capture.Image = img;
            return img;
        }
    }
}
