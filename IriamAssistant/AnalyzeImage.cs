using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IriamAssistant
{
    public class AnalyzeImage
    {
        public void OpenCvTest(Bitmap bitmap)
        {
            using var src = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap);//new Mat("lenna.png", ImreadModes.Grayscale);
            using var dst = new Mat();
            var sw = new Stopwatch();
            sw.Start();
            for(int i = 0; i < 100;i++)
            {
                Cv2.Canny(src, dst, 50, 200);
            }
            sw.Stop();
            Debug.WriteLine(sw.ElapsedMilliseconds);

            using (new Window("src image", src))
            {
                using (new Window("dst image", dst))
                {
                    Cv2.WaitKey();
                }
            }
        }
    }
}
