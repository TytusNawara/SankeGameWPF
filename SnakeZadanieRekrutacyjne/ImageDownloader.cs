using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

namespace SnakeZadanieRekrutacyjne
{
    public class ImageDownloader
    {
        private readonly string URL;

        public ImageDownloader(string url)
        {
            URL = url;
        }

        public BitmapImage DowloadImage()
        {
            WebClient client = new WebClient();
            Stream stream;
            try
            {
                stream = client.OpenRead(URL);

                using (Bitmap bitmap = new Bitmap(stream))
                {
                    stream.Flush();
                    stream.Close();

                    using (MemoryStream memory = new MemoryStream())
                    {
                        bitmap.Save(memory, ImageFormat.Png);
                        memory.Position = 0;

                        BitmapImage toReturn = new BitmapImage();
                        toReturn.BeginInit();
                        toReturn.StreamSource = memory;
                        toReturn.CacheOption = BitmapCacheOption.OnLoad;
                        toReturn.EndInit();
                        return toReturn;
                    }
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Response);
            }

            return null;
        }
    }
}