using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        private WebClient webClient = new ();
        private WebClient webClient1 = new ();
        private WebClient webClient2 = new ();
        private WebClient webClient3 = new ();
        private bool isStopLoading = false;
       
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Image1.Source = null;
            Image2.Source = null;
            Image3.Source = null;
        }
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        private void Button_Start1_OnClick(object sender, RoutedEventArgs e)
        {
            webClient1 = new();
            if (Input_URL1.Text=="")
            {
                MessageBox.Show("Поле ввода адреса 1 не введено", "Error", MessageBoxButton.OK);
                Input_URL1.Focus();
            }
            if (Image1.Source != null)
            {
                Image1.Source = null;
            }
            try
            {
                webClient1.DownloadProgressChanged += Client_DownloadProgressChanged;
                webClient1.DownloadDataCompleted += (s, ea) =>
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new System.IO.MemoryStream(ea.Result);
                    image.EndInit();
                    Image1.Source = image;
                };
                webClient1.DownloadDataAsync(new Uri(Input_URL1.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex);
            }
        }

        private void Button_Start2_OnClick(object sender, RoutedEventArgs e)
        {
            webClient2 = new();
            if (Input_URL2.Text == "")
            {
                MessageBox.Show("Поле ввода адреса 2 не введено", "Error", MessageBoxButton.OK);
                Input_URL2.Focus();
            }
            if (Image2.Source != null)
            {
                Image2.Source = null;
            }
            try
            {
                webClient2.DownloadProgressChanged += Client_DownloadProgressChanged;
                webClient2.DownloadDataCompleted += (s, ea) =>
                {
                    BitmapImage image2 = new BitmapImage();
                    image2.BeginInit();
                    image2.StreamSource = new System.IO.MemoryStream(ea.Result);
                    image2.EndInit();
                    Image2.Source = image2;
                };
                webClient2.DownloadDataAsync(new Uri(Input_URL2.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex);
            }
        }

        private void Button_Start3_OnClick(object sender, RoutedEventArgs e)
        {
            webClient3 = new();
            if (Input_URL3.Text == "")
            {
                MessageBox.Show("Поле ввода адреса 2 не введено", "Error", MessageBoxButton.OK);
                Input_URL3.Focus();
            }
            if (Image3.Source != null)
            {
                Image3.Source = null;
            }
            try
            {
                webClient3.DownloadProgressChanged += Client_DownloadProgressChanged;
                webClient3.DownloadDataCompleted += (s, ea) =>
                {
                    BitmapImage image3 = new BitmapImage();
                    image3.BeginInit();
                    image3.StreamSource = new System.IO.MemoryStream(ea.Result);
                    image3.EndInit();
                    Image3.Source = image3;
                };
                webClient3.DownloadDataAsync(new Uri(Input_URL3.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex);
            }
        }

        private void Button_Stop1_OnClick(object sender, RoutedEventArgs e)
        {
            isStopLoading = true;
            webClient1.Dispose();
            webClient.Dispose();
        }

        private void Button_Stop2_OnClick(object sender, RoutedEventArgs e)
        {            
            isStopLoading = true;
            webClient2.Dispose();
            webClient.Dispose();
        }

        private void Button_Stop3_OnClick(object sender, RoutedEventArgs e)
        {
            isStopLoading = true;
            webClient3.Dispose();
            webClient.Dispose();
        }

        private async void Button_DownloadAll_OnClick(object sender, RoutedEventArgs e)
        {
            ProgressBar.Value = 0;
            isStopLoading = false;
            List <Image> images = new List<Image>();
            images.Add(Image1);
            images.Add(Image2);
            images.Add(Image3);
            List <string> urls = new List<string>();
            urls.Add(Input_URL1.Text);
            urls.Add(Input_URL2.Text);
            urls.Add(Input_URL3.Text);
            webClient.DownloadProgressChanged += Client_DownloadProgressChanged;
            for (int i = 0; i < images.Count; i++)
            {
                await LoadImageFromUrl(urls[i], images[i]);
                LabelBar.Content = "Загружено: " + (i + 1) + "/" + images.Count;
                Thread.Sleep(1000);
            }
            
        }

        private async Task LoadImageFromUrl(string imageUrl, Image image)
        {
            try
            {
                await webClient.DownloadDataTaskAsync(new Uri(imageUrl));
                    if (!isStopLoading)
                    {
                        BitmapImage bitmap = new BitmapImage(new Uri(imageUrl));
                        image.Source = bitmap;
                    }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load image.");
            }
        }
    }
}