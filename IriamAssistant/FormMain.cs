using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using static IriamAssistant.EnumWindow;
using IriamAssistant.未使用;
using System.Numerics;
using Tesseract;
using OpenCvSharp;

public delegate bool CallBack(int hwnd, int lParam);

namespace IriamAssistant
{
    public partial class FormMain : Form, IMenuItem
    {
        [DllImport("user32")]
        public static extern int EnumWindows(CallBack x, int y);

        FormCapture _formCapture;
        EnumWindow _enumWindow;
        ReadSpeaker _readSpeaker = new ReadSpeaker();
        IntervalTask _intervalTask = new IntervalTask();
        TesseractOCR _tesseractOCR = new TesseractOCR();
        AnalyzeImage _analyzeImage = new AnalyzeImage();

        Collections.CircularBuffer<string> _logBuffer = new Collections.CircularBuffer<string>(50);
        string checkcode = "";

        void IMenuItem.OnClick(IntPtr hWnd)
        {
            FrameCapture(hWnd);
            checkBox_AutoRefresh.Checked = true;
        }

        void FrameCapture(IntPtr hWnd)
        {
            if (hWnd != 0)
            {
                var capture = new CaptureWindow();
                pictureBox_Capture.Image = capture.FrameCapture(hWnd);
            }
        }
        Bitmap CaptureText(IntPtr hWnd)
        {
            return _formCapture.CaptureText(hWnd);
        }

        public FormMain()
        {
            InitializeComponent();
            _formCapture = new FormCapture();

            _enumWindow = new EnumWindow(WindowToolStripMenuItem, this);

            //              MessageBox.Show("保存しました");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            _formCapture.Show();
            _formCapture.SetDesktopLocation(Right, Top);
        }

        private void SelectWindowMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            _enumWindow.Refresh();
        }

        static int whsize = 7;
        static float factor = 0.03f;
        static bool addborder = true;
        private async void button_analize_Click(object sender, EventArgs e)
        {
            if (_enumWindow.SelectWindowHandle == IntPtr.Zero) return;

            checkBox_AutoRefresh.Checked = false;


            var img = _formCapture.CaptureText(_enumWindow.SelectWindowHandle);
            //            img.Save("解析用画像.bmp");
            //var img = new Bitmap("解析用画像.bmp");
            _analyzeImage.OpenCvTest(img);
            return;

            var pix = PixConverter.ToPix(img);
            var gray = pix.ConvertRGBToGray();
            var thold = gray.BinarizeSauvola(whsize, factor, addborder);

            //            var gray = _tesseractOCR.ConvertGray(img);
            _formCapture.CaptureImage = PixConverter.ToBitmap(gray);
            _formCapture.AnalyzeImage = PixConverter.ToBitmap(thold);
            //            var text = await WinOCR.AnalizeTask(img);
            var text = _tesseractOCR.AnalyzeFromGray(thold);
            Debug.WriteLine(text);
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            FrameCapture(_enumWindow.SelectWindowHandle);
        }

        private async void button_speak_Click(object sender, EventArgs e)
        {
            await SpeakTask();
        }

        private async Task SpeakTask()
        {
            if (_enumWindow.SelectWindowHandle == IntPtr.Zero) return;
            var img = _formCapture.CaptureText(_enumWindow.SelectWindowHandle);

            var pix = PixConverter.ToPix(img);
            var gray = pix.ConvertRGBToGray();
            var thold = gray.BinarizeSauvola(whsize, factor, addborder);
            var data =             

            //            var gray = _tesseractOCR.ConvertGray(img);
            _formCapture.CaptureImage = PixConverter.ToBitmap(gray);
            var bitmap = PixConverter.ToBitmap(thold);
            _formCapture.AnalyzeImage = bitmap;

            //var gray = _tesseractOCR.ConvertGray(img);
            //var bitmap = TesseractOCR.ToBitmap(gray);

            //            var lines = await WinOCR.AnalizeTask(img);
            var lines = await WinOCR.AnalizeTask(bitmap);

            if (lines.Count == 0) return;
            if (lines.Last().Text == checkcode) return;

            var str = new StringBuilder();
            foreach (var line in lines)
            {
                var text = line.Text.Replace(" ", "");
                if (_logBuffer.Contains(text))
                {
                    continue;
                }
                _logBuffer.InsertLast(text);
                str.Append(text + "\r\n");
                Debug.WriteLine(text);
            }
            checkcode = lines.Last().Text;
            _readSpeaker.Speak(str.ToString());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
            var text = textbox.Text;
            if(int.TryParse(text, out int value))
            {
                _intervalTask.IntervalMs = value * 1000;
            }
            else
            {
                MessageBox.Show("入力可能な文字は半角数字のみです");
            }
        }

        private void checkBox_speak_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            if (checkbox.Checked)
            {
                _intervalTask.Run(SpeakTask);
            }
            else
            {
                _intervalTask.Cancel();
            }
        }
    }
}