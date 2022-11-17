using System;
using System.Collections.Generic;
using System.IO;
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

namespace The_Choice_Refactor.Pages.ListBoxPages
{
    /// <summary>
    /// Interaction logic for CurrencyListPage.xaml
    /// </summary>
    public partial class CurrencyListPage : Page
    {
        public CurrencyListPage()
        {
            InitializeComponent();
        }

        private void Currency_LstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void favorite_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            CurrencyModel? newFavorite = ((CheckBox)sender).DataContext as CurrencyModel;
            if (newFavorite != null)
            {
                StreamWriter writer = new StreamWriter(@"..\..\..\UserData\Favorites\FavoriteCurrencies.txt", true);
                writer.WriteLine(newFavorite.name + ";");
                writer.Close();
            }
        }

        private void favorite_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            CurrencyModel? removedFavorite = ((CheckBox)sender).DataContext as CurrencyModel;
            if (removedFavorite != null)
            {
                string temp = File.ReadAllText(@"..\..\..\UserData\Favorites\FavoriteCurrencies.txt");
                StreamWriter writer = new StreamWriter(@"..\..\..\UserData\Favorites\FavoriteCurrencies.txt");
                writer.Write(temp.Replace(removedFavorite.name + ";\r\n", ""));
                writer.Close();
            }
        }
    }
}
