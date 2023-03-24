//  参考：https://impsbl.hatenablog.jp/entry/ScreenshotOrWindowCaptureWithCSharp
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IriamAssistant
{
    internal class Capture
    {
        [StructLayout(LayoutKind.Sequential)]

        private struct WindowCoordinate
        {
            public int upper_left_x;
            public int upper_left_y;
            public int bottom_right_x;
            public int bottom_right_y;
        }

        [DllImport("user32.Dll")]
        static extern int GetWindowRect(IntPtr hWnd, out WindowCoordinate rect);

        [DllImport("user32.dll")]
        extern static IntPtr GetForegroundWindow();

        [DllImport("dwmapi.dll")]
        extern static int DwmGetWindowAttribute(IntPtr hWnd, int dwAttribute, out WindowCoordinate rect, int cbAttribute);

        String filename(String val)
        {
            //String my_dir = "d:\\user temp\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            //String my_file = DateTime.Now.ToString("hhmmss") + DateTime.Now.Millisecond.ToString() + ".jpg";
            //return (my_dir + val + my_file);
            return "capture.jpg";
        }

        public void snap(Rectangle my_rectangle)
        {
            Bitmap my_bmp = new Bitmap(my_rectangle.Width, my_rectangle.Height);
            Graphics my_graphics = Graphics.FromImage(my_bmp);

            my_graphics.CopyFromScreen(my_rectangle.X, my_rectangle.Y, 0, 0, my_rectangle.Size);
            my_bmp.Save(filename(""), System.Drawing.Imaging.ImageFormat.Jpeg);
        }


        public void snapActiveWindow()
        {
            WindowCoordinate wc;
            IntPtr window_handle = GetForegroundWindow();
            GetWindowRect(window_handle, out wc);

            var rectangle_width = wc.bottom_right_x - wc.upper_left_x;
            var rectangle_height = wc.bottom_right_y - wc.upper_left_y;
            Rectangle my_rectangle = new Rectangle(wc.upper_left_x, wc.upper_left_y, rectangle_width, rectangle_height);

            snap(my_rectangle);
        }

        public void snapActiveDWMWindow()
        {
            int DWMWA_EXTENDED_FRAME_BOUNDS = 9;

            WindowCoordinate wc;
            IntPtr window_handle = GetForegroundWindow();
            DwmGetWindowAttribute(window_handle, DWMWA_EXTENDED_FRAME_BOUNDS, out wc, Marshal.SizeOf(typeof(WindowCoordinate)));

            var rectangle_width = wc.bottom_right_x - wc.upper_left_x;
            var rectangle_height = wc.bottom_right_y - wc.upper_left_y;
            Rectangle my_rectangle = new Rectangle(wc.upper_left_x, wc.upper_left_y, rectangle_width, rectangle_height);

            snap(my_rectangle);
        }

        public void snapScreen()
        {
            System.Diagnostics.Debug.Assert(Screen.PrimaryScreen is not null);
            snap(Screen.PrimaryScreen.Bounds);
        }

        public void snapRectangle()
        {
            System.Diagnostics.Debug.Assert(Screen.PrimaryScreen is not null);

            var x_width = 500;
            var y_height = 500;

            WindowCoordinate wc;

            wc.upper_left_x = Screen.PrimaryScreen.Bounds.Width - x_width;
            wc.upper_left_y = 0;
            wc.bottom_right_x = Screen.PrimaryScreen.Bounds.Width;
            wc.bottom_right_y = Screen.PrimaryScreen.Bounds.Height - y_height;

            Rectangle my_rectangle = new Rectangle(wc.upper_left_x, wc.upper_left_y, x_width, y_height);

            snap(my_rectangle);
        }
    }
}