using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IriamAssistant
{
    public class EnumWindow
    {
        public interface IMenuItem
        {
            void OnClick(IntPtr hWnd);
        }

        public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc,
            IntPtr lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd,
            StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetClassName(IntPtr hWnd,
            StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public IntPtr SelectWindowHandle { get; private set; }
        public ToolStripMenuItem WindowToolStripMenuItem { get; private set; }

        private IMenuItem _iMenuItem;

        public EnumWindow(ToolStripMenuItem toolStripMenuItem, IMenuItem iMenuItem)
        {
            SelectWindowHandle = IntPtr.Zero;
            WindowToolStripMenuItem = toolStripMenuItem;
            _iMenuItem = iMenuItem;
        }

        public void Refresh()
        {
            WindowToolStripMenuItem.DropDownItems.Clear();
            //            windowHandles.Clear();
            EnumWindows(new EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);
        }

        private bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
        {
            //ウィンドウのタイトルの長さを取得する
            int textLen = GetWindowTextLength(hWnd);
            if (0 < textLen)
            {
                //ウィンドウのタイトルを取得する
                StringBuilder tsb = new StringBuilder(textLen + 1);
                GetWindowText(hWnd, tsb, tsb.Capacity);

                //ウィンドウのクラス名を取得する
                StringBuilder csb = new StringBuilder(256);
                GetClassName(hWnd, csb, csb.Capacity);

                //結果を表示する

                // ウィンドウのクラス名が"Progman"なら無視
                if (csb.ToString() == "Progman")
                    return true;

                // 可視ウインドウでないなら無視
                if (!IsWindowVisible(hWnd))
                    return true;

                // 最小化されているウインドウは無視
                if (IsIconic(hWnd))
                    return true;

                // ウインドウサイズがゼロなら無視
                RECT winRect = new RECT();
                GetWindowRect(hWnd, ref winRect);
                if (winRect.top == winRect.bottom || winRect.right == winRect.left)
                    return true;

                string windowTitle = tsb.ToString();

                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = windowTitle;
                item.Tag = hWnd;
                item.Click += Item_Click;
                WindowToolStripMenuItem.DropDownItems.Add(item);
            }
            return true;
        }

        private void Item_Click(object? sender, EventArgs e)
        {
            if(sender is ToolStripMenuItem)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                SelectWindowHandle = (IntPtr)item.Tag;
                _iMenuItem?.OnClick(SelectWindowHandle);
            }
        }

        //void WindowCapture(IntPtr hWnd)
        //{
        //    if (!System.IO.Directory.Exists(this.saveFolderPath))
        //        return;

        //    Bitmap bitmap = GetBitmapFromhWnd(hWnd);

        //    if (bitmap == null)
        //    {
        //        MessageBox.Show("Bitmapが取得できない");
        //        return;
        //    }

        //    if (comboBox1.SelectedIndex == 0)
        //        saveFilePath = String.Format("{0}\\{1}.bmp", saveFolderPath, i);
        //    if (comboBox1.SelectedIndex == 1)
        //        saveFilePath = String.Format("{0}\\{1}.jpg", saveFolderPath, i);
        //    if (comboBox1.SelectedIndex == 2)
        //        saveFilePath = String.Format("{0}\\{1}.png", saveFolderPath, i);
        //    if (comboBox1.SelectedIndex == 3)
        //        saveFilePath = String.Format("{0}\\{1}.gif", saveFolderPath, i);
        //    if (comboBox1.SelectedIndex == 4)
        //        saveFilePath = String.Format("{0}\\{1}.tiff", saveFolderPath, i);

        //    bitmap.Save(saveFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    bitmap.Dispose();

        //    i++;
        //    saveFilePath = String.Format("{0}\\{1}.jpg", saveFolderPath, i);
        //    label1.Text = saveFilePath;
        //}
    }
}
