using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;

namespace IriamAssistant
{
    public class WinOCR
    {
        public static async Task<IReadOnlyList<OcrLine>> AnalizeTask(Bitmap image)
        {
            //// ローカルファイルの場合
            //var storageFile = await StorageFile.GetFileFromPathAsync("D:\\Project\\Assistant\\IriamAssistant\\bin\\Debug\\net7.0-windows10.0.22621.0\\画像.bmp");
            //using var iRandomAccessStream = await RandomAccessStreamReference.CreateFromFile(storageFile).OpenReadAsync();

            //// URLの場合
            //// var uri = new Uri("https://i.imgur.com/LkJ8ZEJ.png");
            //// using var iRandomAccessStream = await RandomAccessStreamReference.CreateFromUri(uri).OpenReadAsync();

            //var bitmapDecoder = await BitmapDecoder.CreateAsync(iRandomAccessStream);
            //using var softwareBitmap = await bitmapDecoder.GetSoftwareBitmapAsync();

            using (var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream())
            {
                image.Save(stream.AsStream(), ImageFormat.Bmp);//choose the specific image format by your own bitmap source
                Windows.Graphics.Imaging.BitmapDecoder decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);
                SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                var ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();
                var result = await ocrEngine.RecognizeAsync(softwareBitmap);
                Console.WriteLine(result.Text);
                return result.Lines;
            }
        }
        //static SoftwareBitmap MakeSoftwareBitmap(System.Drawing.Bitmap bmp)
        //{
        //    unsafe
        //    {
        //        var softwareBitmap = new SoftwareBitmap(BitmapPixelFormat.Bgra8, bmp.Width, bmp.Height, BitmapAlphaMode.Premultiplied);


        //        System.Drawing.Imaging.BitmapData bd = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        //        byte* pSrc = (byte*)bd.Scan0;

        //        using (BitmapBuffer buffer = softwareBitmap.LockBuffer(BitmapBufferAccessMode.Write))
        //        {
        //            using (var reference = buffer.CreateReference())
        //            {
        //                byte* pDest;
        //                uint capacity;
        //                ((IMemoryBufferByteAccess)reference).GetBuffer(out pDest, out capacity);

        //                // Fill-in the BGRA plane
        //                BitmapPlaneDescription bl = buffer.GetPlaneDescription(0);
        //                for (int y = 0; y < bl.Height; y++)
        //                {
        //                    int blOffset = bl.StartIndex + y * bl.Stride;
        //                    int yb = y * bd.Stride;
        //                    for (int x = 0; x < bl.Width; x++)
        //                    {
        //                        pDest[blOffset + 4 * x] = pSrc[yb + 4 * x]; // blue
        //                        pDest[blOffset + 4 * x + 1] = pSrc[yb + 4 * x + 1]; // green
        //                        pDest[blOffset + 4 * x + 2] = pSrc[yb + 4 * x + 2]; // red
        //                        pDest[blOffset + 4 * x + 3] = (byte)255; // alpha
        //                    }
        //                }
        //            }
        //        }

        //        bmp.UnlockBits(bd);

        //        return softwareBitmap;
        //    }
        //}
    } 
}
