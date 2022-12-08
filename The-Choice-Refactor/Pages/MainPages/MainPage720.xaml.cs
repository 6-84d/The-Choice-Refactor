using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using The_Choice_Refactor.Classes;

namespace The_Choice_Refactor.Pages.MainPages
{
    /// <summary>
    /// Interaction logic for MainPage720.xaml
    /// </summary>
    /// 

    //private MainWindow parent;

    public partial class MainPage720 : Page
    {
        private MainWindow1280 parent;

        public MainPage720(MainWindow1280 parent)
        {
            InitializeComponent(); 
            this.parent = parent;
            LoadDataContext(new CryptoVM(), new CurrencyVM(), new MetalVM(), new ShareVM());
        }
        private async void LoadDataContext(CryptoVM crypto, CurrencyVM currency, MetalVM metal, ShareVM share)
        {
            try
            {
                bool isSucces = await crypto.Load();
                CryptoPanel.Visibility = Visibility.Visible;
                CryptoPanel.DataContext = crypto;
            }
            catch (Exception ex)
            {
                CryptoPanel.Visibility = Visibility.Hidden;
                CryptoTop_Btn.Content = FindResource("TextError");
            }

            try
            {
                bool isSucces = await currency.Load();
                CurrPanel.Visibility = Visibility.Visible;
                CurrPanel.DataContext = currency;
            }
            catch (Exception ex)
            {
                CurrPanel.Visibility = Visibility.Hidden;
                CurrTop_Btn.Content = FindResource("TextError");
            }

            try
            {
                bool isSucces = await metal.Load();
                MatPanel.Visibility = Visibility.Visible;
                MatPanel.DataContext = metal;
            }
            catch (Exception ex)
            {
                MatPanel.Visibility = Visibility.Hidden;
                MetalTop_Btn.Content = FindResource("TextError");
            }

            try
            {
                bool isSucces = await share.Load();
                SharesPanel.Visibility = Visibility.Visible;
                SharesPanel.DataContext = share;
            }
            catch (Exception ex)
            {
                SharesPanel.Visibility = Visibility.Hidden;
                ShareTop_Btn.Content = FindResource("TextError");
            }
        }
        private void LetsGo_Btn_Click(object sender, RoutedEventArgs e)
        {
            parent.LetsGo_Btn_Click();
        }
    }
}
