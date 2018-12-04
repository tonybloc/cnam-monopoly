using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Monopoly.Service
{
    public static class Base64Converter
    {
        /// <summary>
        /// Convert string base64 to image
        /// </summary>
        /// <param name="base64String">base64</param>
        /// <returns></returns>
        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            return Image.FromStream(ms, true);
        }

        /// <summary>
        /// Convert Image to base64
        /// </summary>
        /// <param name="image">image </param>
        /// <param name="format">format</param>
        /// <returns></returns>
        public static string ImageToBase64(Image image, ImageFormat format)
        {
            // Convert Image to byte[]
            MemoryStream ms = new MemoryStream();
            image.Save(ms, format);
            byte[] imageBytes = ms.ToArray();

            // Convert byte[] to Base64 String
            return Convert.ToBase64String(imageBytes);
        }

        public static ImageSource base64ToImageSource(string imageData)
        {
            byte[] imgStr = Convert.FromBase64String(imageData);

            
            BitmapImage bitmapImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imgStr);
            bitmapImg.BeginInit();
            bitmapImg.StreamSource = ms;
            bitmapImg.EndInit();

            return (ImageSource)bitmapImg;

        }
        
    }
}
