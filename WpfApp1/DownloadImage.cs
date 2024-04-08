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
        //public WebClient? client;
        public BitmapImage? image;
        public DownloadImage(string Url)
        {
            this.Url = Url;
            //this.client = client;
        }

        public void DownloadStart()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    
                    Task task = new(() =>
                    {
                        byte[] imageData = client.DownloadData(Url);
                       
                        image = new();
                        image.BeginInit();
                        image.StreamSource = new MemoryStream(imageData);
                        image.EndInit();
                        
                    });
                    task.Start();
                    task.Wait();

                    // return image;
                }
                catch (Exception ex)
                {
                    error = ("Ошибка при загрузке изображения: " + ex.Message);
                    //return null;
                }
            }
        }

        public void DownloadStop()
        {            
                //client?.CancelAsync();
            
            
        }


    }
}
