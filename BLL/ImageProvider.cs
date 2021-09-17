using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace WorkWithImage.BLL
{
    public class ImageProvider : IDisposable
    {
        private readonly Font _font;
        private readonly Bitmap _background;
        private readonly Bitmap _watermark;
        private readonly Bitmap _sourceImage;
        private readonly string _textImage;
        private readonly int _heightSourceImage;
        private readonly int _widthSourceImage;
        private readonly int _heightBackgroundImage;

        public ImageProvider(
            string textImage,
            Bitmap watermark,
            Bitmap sourceImage)
        {
            _watermark = watermark;
            _sourceImage = sourceImage;
            _heightSourceImage = _sourceImage.Height;
            _widthSourceImage = _sourceImage.Width;
            _heightBackgroundImage = _heightSourceImage + 100;

            _font = new Font("Consolas", 20, System.Drawing.FontStyle.Regular);

            _background = new Bitmap(_widthSourceImage, _heightBackgroundImage);
            _textImage = textImage;
        }

        public void Dispose()
        {
            _font?.Dispose();
            _background?.Dispose();
        }

        private void CreateBackground()
        {
            using (Graphics g = Graphics.FromImage(_background))
            {
                g.FillRectangle(
                    System.Drawing.Brushes.Black, 1, 1, this._widthSourceImage - 2, this._heightBackgroundImage - 2);
                g.DrawString(_textImage, _font, System.Drawing.Brushes.White, 10, 5);
            }
        }

        public void SaveBG(string fileName, ImageFormat imgFormat)
        {
            _background.Save(fileName, imgFormat);
        }

        private void AddWaterMark(int startLeft, int startTop)
        {
            ColorMatrix color_matrix = new ColorMatrix();
            color_matrix.Matrix33 = 0.5f;
            using(ImageAttributes image_attributes = new ImageAttributes())
            {
                image_attributes.SetColorMatrices(color_matrix, null);

                _watermark.MakeTransparent(_watermark.GetPixel(0, 0));

                using (Graphics gr = Graphics.FromImage(this._sourceImage))
                {
                    Rectangle rect = new Rectangle(startLeft, startTop,
                        _watermark.Width, _watermark.Height);
                    gr.DrawImage(_watermark, rect, 0, 0,
                        _watermark.Width, _watermark.Height,
                        GraphicsUnit.Pixel, image_attributes);
                }
            }
        }

        private void AddMModifiedImageToBackground()
        {

        }

        public void Work()
        {
            this.CreateBackground();
            this.AddWaterMark(this._heightBackgroundImage - 50, 150);
        }
    }
}
