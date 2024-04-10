using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace WpfApp1
{
    public class DownloadImage
    {
        public string? Url;
        public string? error;
        public WebClient? client;
        public BitmapImage? image;
        public DownloadImage(string Url, WebClient client)
        {
            this.Url = Url;
            this.client = client;
        }

        public async Task DownloadStartAsync()
        {
            try
            {
                byte[] imageData = await client.DownloadDataTaskAsync(Url);
                image = new();
                image.BeginInit();
                image.StreamSource = new MemoryStream(imageData);
                image.EndInit();
            }
            catch (Exception ex)
                {
                error = ("Ошибка при загрузке изображения: " + ex.Message);
            }
        }

        public void DownloadStop()
        {            
                client?.CancelAsync();
        }


    }
}
