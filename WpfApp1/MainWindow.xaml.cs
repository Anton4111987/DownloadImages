using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
    public partial class MainWindow : Window
    {
        private int totalProgress;
        private WebClient webClient1;
        private WebClient webClient2;
        private WebClient webClient3;
        public event PropertyChangedEventHandler PropertyChanged;
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
            webClient = new WebClient();
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            webClient.DownloadDataCompleted += WebClient_DownloadDataCompleted;*/
        }
        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            TotalProgress = e.ProgressPercentage;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Image1.Source = null;
            Image2.Source = null;
            Image3.Source = null;
           
        }

        private async void Button_Start1_OnClick(object sender, RoutedEventArgs e)
        {
            if(Input_URL1.Text=="")
            {
                MessageBox.Show("Поле ввода адреса 1 не введено", "Error", MessageBoxButton.OK);
                Input_URL1.Focus();
            }

            using (WebClient webClient1 = new())
            {
                if (Image1.Source != null)
                {
                    Image1.Source = null;
                }
                try
                {
                    byte[] imageData1 = await webClient1.DownloadDataTaskAsync(Input_URL1.Text);
                    Scroll.Minimum = 0;
                    Scroll.Maximum = imageData1.Length;
                    BitmapImage image1 = new BitmapImage();
                    image1.BeginInit();
                    image1.StreamSource = new MemoryStream(imageData1);
                    image1.EndInit();

                    Image1.Source = image1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                }


            }
        }

        private async void Button_Start2_OnClick(object sender, RoutedEventArgs e)
        {
            if (Input_URL2.Text == "")
            {
                MessageBox.Show("Поле ввода адреса 2 не введено", "Error", MessageBoxButton.OK);
                Input_URL2.Focus();
            }
            else
            {

                /*DownloadImage downloadImage = new(Input_URL2.Text);
                    try
                    {
                        downloadImage.DownloadStart();
                   
                        Image2.Source = downloadImage.image;


                    }
                    catch
                    {
                        MessageBox.Show(downloadImage.error);
                    }*/


                /*using (WebClient client = new WebClient())
                {
                    
                    if (Image2.Source != null)
                    {
                        Image2.Source = null;
                    }
                    try
                    {
                        byte[] imageData2 = await client.DownloadDataTaskAsync(Input_URL2.Text);
                        BitmapImage image2 = new BitmapImage();
                        image2.BeginInit();
                        image2.StreamSource = new MemoryStream(imageData2);
                        image2.EndInit();
                   //  https://gas-kvas.com/grafic/uploads/posts/2023-10/1696502271_gas-kvas-com-p-kartinki-lyubie-5.jpg
                    // https://klike.net/uploads/posts/2023-02/1675842971_3-353.jpg

                        Image2.Source = image2;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                    }


                }*/

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(Input_URL2.Text);

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                        BitmapImage image2 = new BitmapImage();
                        image2.BeginInit();
                        image2.StreamSource = new MemoryStream(imageBytes);
                        /*string imageName = Path.GetFileName(Input_URL2.Text);
                        string imagePath = Path.Combine(Environment.CurrentDirectory, imageName);*/
                        image2.EndInit();
                        Image2.Source = image2;


                    }

                }
            }
        }

        private async void Button_Start3_OnClick(object sender, RoutedEventArgs e)
        {
            if (Input_URL3.Text == "")
            {
                MessageBox.Show("Поле ввода адреса 3 не введено", "Error", MessageBoxButton.OK);
                Input_URL3.Focus();
            }

            using (WebClient webClient3 = new())
            {
                if (Image3.Source != null)
                {
                    Image3.Source = null;
                }
                try
                {
                    byte[] imageData3 = await webClient3.DownloadDataTaskAsync(Input_URL3.Text);
                    BitmapImage image3 = new BitmapImage();
                    image3.BeginInit();
                    image3.StreamSource = new MemoryStream(imageData3);
                    image3.EndInit();

                    Image3.Source = image3;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                }


            }
        }

        private void Button_Stop1_OnClick(object sender, RoutedEventArgs e)
        {
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

        private void Button_DownloadAll_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Scroll_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }



      

        

        private void Input_URL1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Input_URL1.Text == "Введите адрес картинки №1")
                Input_URL1.Text = "";
            Input_URL3.Text = "Введите адрес картинки №3";
            Input_URL2.Text = "Введите адрес картинки №2";

        }

        private void Input_URL2_MouseMove(object sender, MouseEventArgs e)
        {
            if (Input_URL2.Text == "Введите адрес картинки №2")
                Input_URL2.Text = "";
            Input_URL3.Text = "Введите адрес картинки №3";
            Input_URL1.Text = "Введите адрес картинки №1";

        }

        private void Input_URL3_MouseMove(object sender, MouseEventArgs e)
        {
            if (Input_URL3.Text == "Введите адрес картинки №3")
                Input_URL3.Text = "";
            Input_URL1.Text = "Введите адрес картинки №3";
            Input_URL2.Text = "Введите адрес картинки №2";
        }

















      /*  public partial class MainWindow : Window, INotifyPropertyChanged
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
                DataContext = this;
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
                LoadedImage.Source = image;
            }

            private void StartButton_Click(object sender, RoutedEventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(UrlInput.Text))
                {
                    webClient.DownloadDataAsync(new Uri(UrlInput.Text));
                }
            }

            private void StopButton_Click(object sender, RoutedEventArgs e)
            {
                webClient.CancelAsync();
                TotalProgress = 0;
            }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }*/
















    }
}