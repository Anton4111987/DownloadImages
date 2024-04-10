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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
    public partial class MainWindow : Window
    {
        private int totalProgress;
        private WebClient webClient = new WebClient();
        private WebClient webClient1 = new WebClient();
        private WebClient webClient2 = new WebClient();
        private WebClient webClient3 = new WebClient();
        private bool isStopLoading = false;
        public event PropertyChangedEventHandler PropertyChanged;
        private List<string> imageUrls = new List<string>();
        public int TotalProgress
        {
            get { return totalProgress; }
            set
            {
                totalProgress = value;
                //OnPropertyChanged("TotalProgress");
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            /* DataContext = this;            
             webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
             webClient.DownloadDataCompleted += WebClient_DownloadDataCompleted;*/
        }
        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            TotalProgress = e.ProgressPercentage;
            ProgressBar.Value = e.ProgressPercentage;
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
            if(Input_URL1.Text=="")
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
                //WebClient webClient1 = new WebClient();
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
                    //Image1.Source = downloadImage.image;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Ошибка "+ex);
                }
        }

        
    

        private void Button_Start2_OnClick(object sender, RoutedEventArgs e)
        {
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
                //WebClient webClient2 = new WebClient();
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
                //Image1.Source = downloadImage.image;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex);
            }
            /*DownloadImage downloadImage = new(Input_URL2.Text, webClient);
            try
            {
                await downloadImage.DownloadStartAsync();
                Image2.Source = downloadImage.image;
            }
            catch
            {
                MessageBox.Show(downloadImage.error);
            }*/

        }

        private async void Button_Start3_OnClick(object sender, RoutedEventArgs e)
        {
            /*if (Input_URL3.Text == "")
            {
                MessageBox.Show("Поле ввода адреса 3 не введено", "Error", MessageBoxButton.OK);
                Input_URL3.Focus();
            }
            if (Image3.Source != null)
            {
                Image3.Source = null;
            }
            DownloadImage downloadImage = new(Input_URL3.Text, webClient);
            try
            {
                await downloadImage.DownloadStartAsync();
                Image3.Source = downloadImage.image;
            }
            catch
            {
                MessageBox.Show(downloadImage.error);
            }*/
            try
            {
                webClient.DownloadProgressChanged += Client_DownloadProgressChanged;
                webClient.DownloadDataCompleted += (s, ea) =>
                {
                    BitmapImage image3 = new BitmapImage();
                    image3.BeginInit();
                    image3.StreamSource = new System.IO.MemoryStream(ea.Result);
                    image3.EndInit();
                    Image3.Source = image3;
                };
                webClient.DownloadDataAsync(new Uri(Input_URL3.Text));
                //Image1.Source = downloadImage.image;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex);
            }
        }

        private void Button_Stop1_OnClick(object sender, RoutedEventArgs e)
        {
            isStopLoading = true;
            webClient1.CancelAsync();
            TotalProgress = 0;
        }

        private void Button_Stop2_OnClick(object sender, RoutedEventArgs e)
        {
            webClient2.CancelAsync();
            TotalProgress = 0;
        }

        private void Button_Stop3_OnClick(object sender, RoutedEventArgs e)
        {
            webClient3.CancelAsync();
            TotalProgress = 0;
        }

        private async void Button_DownloadAll_OnClick(object sender, RoutedEventArgs e)
        {
           
            isStopLoading = false;
           
            List <Image> images = new List<Image>();
            images.Add(Image1);
            images.Add(Image2);
            images.Add(Image3);
            List <string> urls = new List<string>();
            urls.Add(Input_URL1.Text);
            urls.Add(Input_URL2.Text);
            urls.Add(Input_URL3.Text);
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = images.Count;

   
           /* webClient.DownloadProgressChanged += (s, e) =>
            {
                ProgressBar.Value = e.ProgressPercentage;
            };*/

            /*await LoadImageFromUrl(Input_URL1.Text, Image1);
            if (isStopLoading) return;

            await LoadImageFromUrl(Input_URL2.Text, Image2);
            if (isStopLoading) return;

            await LoadImageFromUrl(Input_URL3.Text, Image3);*/

            for (int i = 0; i < 3; i++)
                {
                await LoadImageFromUrl(urls[i], images[i]);
                    if (isStopLoading) return;
                    
                Thread.Sleep(1000);
                ProgressBar.Value ++;
                LabelBar.Content = "Загружено: " + (i+1)+"/"+images.Count;
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
                    Thread.Sleep(1000);
                    }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load image.");
            }

        }

  

















       /* public partial class MainWindow : Window, INotifyPropertyChanged
        {
            private WebClient webClient;
            private int totalProgress;

            public int TotalProgress
            {
                get { return totalProgress; }
                set
                {
                    totalProgress = value;
                    OnPropertyChanged("TotalProgress");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public MainWindow()
            {
                InitializeComponent();
                //DataContext = this;
                webClient = new WebClient();
                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                webClient.DownloadDataCompleted += WebClient_DownloadDataCompleted;
            }

            private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
            {
                TotalProgress = e.ProgressPercentage;
            }

            private void WebClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new System.IO.MemoryStream(e.Result);
                image.EndInit();
                Image1.Source = image;

            }

            private void DownloadImage(string url)
            {
               // WebClient web = new WebClient();
                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                webClient.DownloadFileCompleted += OnDownloadFileCompleted;
                webClient.DownloadDataTaskAsync(url);
            }

            private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
            {
                *//*var projectName = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                var imagePath = projectName + "\\output.jpg";
                img.Source = new BitmapImage(new Uri(imagePath));
                MessageBox.Show("Загрузка файла завершена!");*//*

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new MemoryStream(webClient.DownloadDataTaskAsync(url));
                image.EndInit();
                Image1.Source = image;
            }

            *//*private void StartButton_Click(object sender, RoutedEventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(Input_URL1.Text))
                {
                    webClient.DownloadDataAsync(new Uri(UrlInput.Text));
                }
            }*//*

            private void StopButton_Click(object sender, RoutedEventArgs e)
            {
                webClient.CancelAsync();
                TotalProgress = 0;
            }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
*/
    }
}