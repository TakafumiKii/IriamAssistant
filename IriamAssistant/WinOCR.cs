using System;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;

namespace IriamAssistant
0{
    public class WinOCR
    {
        public static async Task AnalizeTask()
        {
            var ocrEngine = OcrEngine.TryCreateFromUserProfileLanguages();

            // ローカルファイルの場合
            var storageFile = await StorageFile.GetFileFromPathAsync("D:\\Project\\Assistant\\IriamAssistant\\bin\\Debug\\net7.0-windows10.0.22621.0\\画像.bmp");
            using var iRandomAccessStream = await RandomAccessStreamReference.CreateFromFile(storageFile).OpenReadAsync();

            // URLの場合
            // var uri = new Uri("https://i.imgur.com/LkJ8ZEJ.png");
            // using var iRandomAccessStream = await RandomAccessStreamReference.CreateFromUri(uri).OpenReadAsync();

            var bitmapDecoder = await BitmapDecoder.CreateAsync(iRandomAccessStream);
            using var softwareBitmap = await bitmapDecoder.GetSoftwareBitmapAsync();

            var result = await ocrEngine.RecognizeAsync(softwareBitmap);
            Console.WriteLine(result.Text);
        }
    } 
}
