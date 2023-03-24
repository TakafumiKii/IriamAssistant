using System;
using System.Runtime.InteropServices;
using static IriamAssistant.EnumWindow;

public delegate bool CallBack(int hwnd, int lParam);

namespace IriamAssistant
{
    public partial class FormMain : Form, IMenuItem
    {
        [DllImport("user32")]
        public static extern int EnumWindows(CallBack x, int y);

        FormCapture _formCapture;
        EnumWindow _enumWindow;

        void IMenuItem.OnClick(IntPtr hWnd)
        {
            StartCapture(hWnd);
        }

        void StartCapture(IntPtr hWnd)
        {
            if (hWnd != 0)
            {
                var capture = new CaptureWindow();
                pictureBox_Capture.Image = capture.Capture(hWnd);
            }
        }

        public FormMain()
        {
            InitializeComponent();
            _formCapture = new FormCapture();
            //_formCapture.FrameBorderSize = 10; //線の太さ

            //_formCapture.FrameColor = Color.Blue; //線の色

            //_formCapture.AllowedTransform = true; //サイズ変更の可否
            _formCapture.Show();

            _enumWindow = new EnumWindow(WindowToolStripMenuItem, this);

            var img = new Bitmap("画像.bmp");
            pictureBox_Capture.Image = img;
//              MessageBox.Show("保存しました");
      }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        private void SelectWindowMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            _enumWindow.Refresh();
        }

        private async void button_analize_Click(object sender, EventArgs e)
        {
            await WinOCR.AnalizeTask();
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            StartCapture(_enumWindow.SelectWindowHandle);
        }
    }
}