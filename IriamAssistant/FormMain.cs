using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using static IriamAssistant.EnumWindow;
using IriamAssistant.–¢Žg—p;

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

        Collections.CircularBuffer<string> _logBuffer = new Collections.CircularBuffer<string>(100);
        string checkcode = "";

        void IMenuItem.OnClick(IntPtr hWnd)
        {
            FrameCapture(hWnd);
            checkBox_speak.Checked = true;
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

            //              MessageBox.Show("•Û‘¶‚µ‚Ü‚µ‚½");
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

        private async void button_analize_Click(object sender, EventArgs e)
        {
            if (_enumWindow.SelectWindowHandle == IntPtr.Zero) return;

            var img = _formCapture.CaptureText(_enumWindow.SelectWindowHandle);
            await WinOCR.AnalizeTask(img);
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
            var lines = await WinOCR.AnalizeTask(img);

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
            }
            checkcode = lines.Last().Text;
            _readSpeaker.Speak(str.ToString());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
            var text = textbox.Text;
            var ms = int.Parse(text) * 1000;
            _intervalTask.IntervalMs = ms;
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