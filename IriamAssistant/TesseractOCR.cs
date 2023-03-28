using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace IriamAssistant
{
    internal class TesseractOCR
    {
        TesseractEngine _engine;
        public TesseractOCR()
        {
            _engine = new TesseractEngine("tessdata", "jpn", EngineMode.LstmOnly);
        }

        public string Analyze(Bitmap image)
        {
            return AnalyzeFromGray(ConvertGray(image));
        }
        public string AnalyzeFromGray(Pix pix)
        {           
            using (var page = _engine.Process(pix))
            {
                var text = page.GetText();

                // 結果表示
                return text;
            }
        }

        static int sx = 200, sy = 60;
        static int smx = 0, smy = 0;

        static int whsize = 7;
        static bool addborder = false;
        static float factor = 0;

        static int nx = 10, ny = 10;

        public Pix ConvertGray(Bitmap image)
        {
            var pix = PixConverter.ToPix(image);
            var gray = pix.ConvertRGBToGray();
            //var thold = gray.BinarizeOtsuAdaptiveThreshold(sx, sy, smx, smy, factor);
            //var sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            //sw.Stop();
            //Debug.WriteLine("Sauvola:" + sw.Elapsed.ToString());
            var thold = gray.BinarizeSauvola(whsize, factor, addborder);

            return thold;
        }

        public static Bitmap ToBitmap(Pix pix)
        {
            return PixConverter.ToBitmap(pix);
        }
    }
}
