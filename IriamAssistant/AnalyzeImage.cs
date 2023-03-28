using OpenCvSharp;
using System;
using System.Collections.Generic;
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

            Cv2.Canny(src, dst, 50, 200);
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
