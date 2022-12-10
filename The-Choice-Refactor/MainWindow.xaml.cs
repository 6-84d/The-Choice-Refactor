using System.Windows;
using System.Windows.Controls;
using System.IO;
using The_Choice_Refactor.Classes;
using The_Choice_Refactor.Pages.MainPages;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using System.Threading;

namespace The_Choice_Refactor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {    // application configuration (theme, language, currency)
        private Page currentPage;   // page that showed in frame at the moment
        public bool isOpen = false;
        public MainWindow()
        {
            The_Choice_Refactor.Properties.Settings.Default.Dark = true;
            ApiHelper.InitializeClient();
            LoadingWIndow loadingWIndow = new LoadingWIndow();
            loadingWIndow.Show();
            Thread.Sleep(1000);
            loadingWIndow.Close();
            InitializeComponent();                                                                        // init http client to work with apis
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.ResizeMode = ResizeMode.CanMinimize;
            currentPage = new MainPage(this);                                                                               // create MainPage and set to current page
            PageFrame_Frm.Navigate(currentPage);   // navigate frame to current page
            CryptoPageButton.FontWeight = FontWeights.Light;
            CurrenciesPageButton.FontWeight = FontWeights.Light;
            MaterialsPageButton.FontWeight = FontWeights.Light;
            SharesPageButton.FontWeight = FontWeights.Light;
            SetConfig();
        }

        private void MainPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            currentPage = new MainPage(this);           // create MainPage and set to current page
            PageFrame_Frm.Navigate(currentPage);    // navigate frame to current page
            CryptoPageButton.FontWeight = FontWeights.Light;
            CurrenciesPageButton.FontWeight = FontWeights.Light;
            MaterialsPageButton.FontWeight = FontWeights.Light;
            SharesPageButton.FontWeight = FontWeights.Light;
            if (currentPage is MainPage)
                MainGrid_Grd.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Pictures/LinesBackgroundLight.png")));
            else
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void CryptoPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is CryptoPage))
            {
                currentPage = new CryptoPage();
                PageFrame_Frm.Navigate(currentPage);
                CryptoPageButton.FontWeight = FontWeights.Normal;
                CurrenciesPageButton.FontWeight = FontWeights.Light;
                MaterialsPageButton.FontWeight = FontWeights.Light;
                SharesPageButton.FontWeight = FontWeights.Light;
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void CurrencyPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is CurrencyPage))
            {
                currentPage = new CurrencyPage();
                PageFrame_Frm.Navigate(currentPage);
                CryptoPageButton.FontWeight = FontWeights.Light;
                CurrenciesPageButton.FontWeight = FontWeights.Normal;
                MaterialsPageButton.FontWeight = FontWeights.Light;
                SharesPageButton.FontWeight = FontWeights.Light;
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void MetalPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is MetalPage))
            {
                currentPage = new MetalPage();
                PageFrame_Frm.Navigate(currentPage);
                CryptoPageButton.FontWeight = FontWeights.Light;
                CurrenciesPageButton.FontWeight = FontWeights.Light;
                MaterialsPageButton.FontWeight = FontWeights.Normal;
                SharesPageButton.FontWeight = FontWeights.Light;
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void SharePage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is SharePage))
            {
                currentPage = new SharePage();
                PageFrame_Frm.Navigate(currentPage);
                CryptoPageButton.FontWeight = FontWeights.Light;
                CurrenciesPageButton.FontWeight = FontWeights.Light;
                MaterialsPageButton.FontWeight = FontWeights.Light;
                SharesPageButton.FontWeight = FontWeights.Normal;
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void Options_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(!isOpen)
            {
                OptionsWindow options = new OptionsWindow(this);
                options.ShowDialog();
            }
        }
        public void SetConfig()
        {
            if (The_Choice_Refactor.Properties.Settings.Default.Dark == true)
            {
                this.Cursor = new System.Windows.Input.Cursor(App.GetResourceStream(new Uri(@"pack://application:,,,/Resources/Pictures/Icons/WhiteCursor.cur")).Stream);
                Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Pictures/BackgroundDark.jpg")));
            }
            else if (The_Choice_Refactor.Properties.Settings.Default.Dark == false)
            {
                this.Cursor = new System.Windows.Input.Cursor(App.GetResourceStream(new Uri(@"pack://application:,,,/Resources/Pictures/Icons/DarkCursor.cur")).Stream);
                Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Pictures/BackgroundLight.png")));
            }
            if (currentPage is MainPage)
                MainGrid_Grd.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Pictures/LinesBackgroundLight.png")));
            else
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
        }
        public void LetsGo_Btn_Click()
        {
            Random r = new Random();
            switch (r.Next(0, 4))
            {
                case 0:
                    currentPage = new CryptoPage();
                    CryptoPageButton.FontWeight = FontWeights.Normal;
                    CurrenciesPageButton.FontWeight = FontWeights.Light;
                    MaterialsPageButton.FontWeight = FontWeights.Light;
                    SharesPageButton.FontWeight = FontWeights.Light;
                    break;
                case 1:
                    currentPage = new CurrencyPage();
                    CryptoPageButton.FontWeight = FontWeights.Light;
                    CurrenciesPageButton.FontWeight = FontWeights.Normal;
                    MaterialsPageButton.FontWeight = FontWeights.Light;
                    SharesPageButton.FontWeight = FontWeights.Light;
                    break;
                case 2:
                    currentPage = new MetalPage();
                    CryptoPageButton.FontWeight = FontWeights.Light;
                    CurrenciesPageButton.FontWeight = FontWeights.Light;
                    MaterialsPageButton.FontWeight = FontWeights.Normal;
                    SharesPageButton.FontWeight = FontWeights.Light;
                    break;
                case 3:
                    currentPage = new SharePage();
                    CryptoPageButton.FontWeight = FontWeights.Light;
                    CurrenciesPageButton.FontWeight = FontWeights.Light;
                    MaterialsPageButton.FontWeight = FontWeights.Light;
                    SharesPageButton.FontWeight = FontWeights.Normal;
                    break;
                default:
                    return;
            }
            if (currentPage is MainPage)
                MainGrid_Grd.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Pictures/LinesBackgroundLight.png")));
            else
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            PageFrame_Frm.Navigate(currentPage);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
