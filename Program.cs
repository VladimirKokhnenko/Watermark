using System;
using System.Drawing;
using System.IO;

using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WorkWithImage
{
    class Program
    {
        static void Main(string[] args)
        {

            // параметры -->
            //var imagePath = @"D:\input.jpg";
            
            string imagePath = "https://abptest.blob.core.windows.net/images/b/Stellox_000002BSX_1.jpg";
            string text = "Это норма!";

            var background = System.Windows.Media.Brushes.Black;
            var textColor = System.Windows.Media.Brushes.White;

            int gap = 20;
            int fontSize = 70;

            int dpi = 96;

            var font =
                new Typeface(
                    new System.Windows.Media.FontFamily("Segoe UI"), FontStyles.Normal,
                    FontWeights.Bold, FontStretches.SemiExpanded);
            // <--

            var image = BitmapFrame.Create(new Uri(imagePath));
            double imageWidth = (double)image.PixelWidth;
            double imageHeight = (double)image.PixelHeight;

            var formattedText =
                new FormattedText(
                    text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    font, fontSize, textColor, dpi)
                {
                    MaxTextWidth = imageWidth,
                    TextAlignment = TextAlignment.Center
                };

            var textWidth = formattedText.Width;
            var textHeight = formattedText.Height;

            var totalWidth = (int)Math.Ceiling(imageWidth + 2 * gap);
            var totalHeight = (int)Math.Ceiling(imageHeight + 3 * gap + textHeight);

            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(
                    background, null,
                    new Rect(0, 0, totalWidth, totalHeight));

                drawingContext.DrawImage(
                    image,
                    new Rect(gap, gap, imageWidth, imageHeight));
                drawingContext.DrawText(
                    formattedText,
                    new System.Windows.Point(gap, imageHeight + 2 * gap));
            }

            var bmp =
                new RenderTargetBitmap(
                    totalWidth, totalHeight, dpi, dpi,
                    PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            var resultPath = @"D:\output.jpg";
            using (var stream = File.Create(resultPath))
                encoder.Save(stream);

            Console.ReadKey();
        }
    }
}
