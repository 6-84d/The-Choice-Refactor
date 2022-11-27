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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private MainWindow parent;
        public MainPage(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            CryptoPanel.DataContext = new CryptoVM();
            CurrPanel.DataContext = new CurrencyVM();
            MatPanel.DataContext = new MetalVM();
            SharesPanel.DataContext = new ShareVM();
        }

        private void LetsGo_Btn_Click(object sender, RoutedEventArgs e)
        {
            parent.LetsGo_Btn_Click();
        }
    }
}
