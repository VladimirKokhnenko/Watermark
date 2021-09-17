using System;
using System.Drawing;
using System.IO;

using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using WorkWithImage.BLL;
using System.Net;

namespace WorkWithImage
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToWatermark = @"D:\MyWorkFolder\it\develop\avto.pro\Watermark\bin\Debug\site_logo.png";
            string pathToSourceImage = @"https://abptest.blob.core.windows.net/images/c/Stellox_000006B-SX_1.jpg";

            using(Bitmap watermark = new Bitmap(pathToWatermark))
            {
                using (WebClient client = new WebClient())
                {
                    using (Stream stream = client.OpenRead(pathToSourceImage))
                    {
                        using (Bitmap sourceImage = new Bitmap(stream))
                        {
                            using (ImageProvider imgProvider = new ImageProvider("FEBI 11138", watermark, sourceImage))
                            {
                                imgProvider.Work();
                                imgProvider.SaveBG(@".\superTest.png", ImageFormat.Png);
                            }
                        }
                    }
                }
            }
        }
    }
}
