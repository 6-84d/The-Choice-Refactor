using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using The_Choice_Refactor.Classes;
using The_Choice_Refactor.Pages.MainPages;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using System.Threading;
using System.Security.Policy;
using System.Windows.Input;
using System.Windows.Resources;

namespace The_Choice_Refactor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Config? config;     // application configuration (theme, language, currency)
        private Page currentPage;   // page that showed in frame at the moment
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();                                                                            // init http client to work with apis
            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(@"..\..\..\UserData\Configuration.json"));  // load configuration from json-file
            SetConfig();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            currentPage = new MainPage(this);                                                                               // create MainPage and set to current page
            PageFrame_Frm.Navigate(currentPage);   // navigate frame to current page
        }

        private void MainPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            currentPage = new MainPage(this);           // create MainPage and set to current page
            PageFrame_Frm.Navigate(currentPage);    // navigate frame to current page
            SetConfig();
        }

        private void CryptoPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            currentPage = new CryptoPage();
            PageFrame_Frm.Navigate(currentPage);
            MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void CurrencyPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            currentPage = new CurrencyPage();
            PageFrame_Frm.Navigate(currentPage);
            MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void MetalPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            currentPage = new MetalPage();
            PageFrame_Frm.Navigate(currentPage);
            MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void SharePage_Btn_Click(object sender, RoutedEventArgs e)
        {
            currentPage = new SharePage();
            PageFrame_Frm.Navigate(currentPage);
            MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void Options_Btn_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow options = new OptionsWindow(config, this);
            options.ShowDialog();
            MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
        }
        public void SetConfig()
        {
            if(config.DarkTheme)
            {
                this.Cursor = new System.Windows.Input.Cursor(@"..\..\..\Resources\Pictures\Icons\WhiteCursor.cur");
                Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\..\Resources\Pictures\BackgroundDark.jpg", UriKind.Relative))){
                };
                MainGrid_Grd.Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\..\Resources\Pictures\LinesBackgroundDark.png", UriKind.Relative)));
            }
            else
            {
                this.Cursor = new System.Windows.Input.Cursor(@"..\..\..\Resources\Pictures\Icons\DarkCursor.cur");
                Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\..\Resources\Pictures\BackgroundLight.png", UriKind.Relative)));
                MainGrid_Grd.Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\..\Resources\Pictures\LinesBackgroundLight.png", UriKind.Relative)));
            }
        }
        public void LetsGo_Btn_Click()
        {
            Random r = new Random();
            switch (r.Next(0, 4))
            {
                case 0:
                    currentPage = new CryptoPage();
                    break;
                case 1:
                    currentPage = new CurrencyPage();
                    break;
                case 2:
                    currentPage = new MetalPage();
                    break;
                case 3:
                    currentPage = new SharePage();
                    break;
                default:
                    return;
            }
            PageFrame_Frm.Navigate(currentPage);
        }
    }
}
