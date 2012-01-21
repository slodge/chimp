using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NinetyNinePercent.Helpers
{
    public static class FileIO
    {
        private const int JpegQuality = 95;

        public static void SaveToFile(this BitmapImage bitmapImage, string tempFileName)
        {
            var wb = new WriteableBitmap(bitmapImage);
            SaveToFile(wb, tempFileName, (stream) => { });
        }

        public static void SaveToFile(this WriteableBitmap wb, string tempFileName, Action<Stream> extra)
        {
            using (var iso = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (iso.FileExists(tempFileName))
                {
                    iso.DeleteFile(tempFileName);
                }

                using (var fileStream = iso.CreateFile(tempFileName))
                {
                    // Encode WriteableBitmap object to a JPEG stream.
                    wb.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, 0, JpegQuality);
                    extra(fileStream);
                }
            }
        }

        public static BitmapImage LoadFromFile(string fileName)
        {
            var toReturn = new BitmapImage();
            LoadFromFile(fileName, toReturn.SetSource);
            return toReturn;
        }

        public static void LoadFromFile(string fileName, Action<Stream> loadAction)
        {
            using (var iso = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var fileStream = iso.OpenFile(fileName, FileMode.Open))
                {
                    loadAction(fileStream);
                }
            }
        }
    }
}
