using System.Drawing;
using System.Drawing.Imaging;

namespace WorkWithImage
{
    class Watermark
    {
        public void DrawWatermark(Bitmap watermark_bm, Bitmap result_bm, int x, int y)
        {
            // Make a ColorMatrix that multiplies
            // the alpha component by 0.5.
            ColorMatrix color_matrix = new ColorMatrix();
            color_matrix.Matrix33 = 0.5f;

            // Make an ImageAttributes that uses the ColorMatrix.
            ImageAttributes image_attributes = new ImageAttributes();
            image_attributes.SetColorMatrices(color_matrix, null);

            // Make pixels that are the same color as the
            // one in the upper left transparent.
            watermark_bm.MakeTransparent(watermark_bm.GetPixel(0, 0));

            // Draw the image using the ColorMatrix.
            //using (Graphics gr = Graphics.FromImage(result_bm))
            //{
            //    Rectangle rect = new Rectangle(x, y,
            //        watermark_bm.Width, watermark_bm.Height);
            //    gr.DrawImage(watermark_bm, rect, 0, 0,
            //        watermark_bm.Width, watermark_bm.Height,
            //        GraphicsUnit.Pixel, image_attributes);
            //}
        }
    }
}
