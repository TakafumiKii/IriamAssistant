using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IriamAssistant
{
    internal class CaptureWindow
    {
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private extern static bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        private const uint PW_CLIENTONLY = 0x1;

        public Bitmap FrameCapture(IntPtr handle)
        {
            //ウィンドウサイズ取得
            RECT rect;
            bool flag = GetWindowRect(handle, out rect);

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            //ウィンドウをキャプチャする
            Bitmap img = new Bitmap(width, height);
            Graphics memg = Graphics.FromImage(img);
            IntPtr dc = memg.GetHdc();

            PrintWindow(handle, dc, PW_CLIENTONLY);

            memg.ReleaseHdc(dc);
            memg.Dispose();

            //img.Save("画像.bmp");
            return img;
        }


        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll")]
        private static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hdc);

        private const int SRCCOPY = 13369376;

        const int width = 966;     //  全体 1002
        const int height = 448;    //  全体 1842 
        const int src_x = 20;
        const int src_y = 1220;
        public Bitmap TextCapture(IntPtr windowHandle)
        {
            var winDC = GetWindowDC(windowHandle);

            //ウィンドウの大きさを取得
            GetWindowRect(windowHandle, out var winRect);

            //Bitmapの作成
//            Bitmap bmp = new Bitmap(winRect.right - winRect.left, winRect.bottom - winRect.top);
            Bitmap bmp = new Bitmap(width, height);

            //Graphicsの作成
            var g = Graphics.FromImage(bmp);

            //Graphicsのデバイスコンテキストを取得
            var hDC = g.GetHdc();

            //Bitmapに画像をコピーする
            BitBlt(hDC, 0, 0, width, height, winDC, src_x, src_y, SRCCOPY);

            g.ReleaseHdc(hDC);
            g.Dispose();
            ReleaseDC(windowHandle, winDC);

            return bmp;
        }
    }
}

/*
 
*/