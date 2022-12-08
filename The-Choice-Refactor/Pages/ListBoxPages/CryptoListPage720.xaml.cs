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
    /// Interaction logic for CryptoListPage720.xaml
    /// </summary>
    public partial class CryptoListPage720 : Page
    {
        public CryptoListPage720()
        {
            InitializeComponent();
        }
        private void Crypto_LstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void favorite_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            CryptoModel? newFavorite = ((CheckBox)sender).DataContext as CryptoModel;
            if (newFavorite != null)
            {
                StreamWriter writer = new StreamWriter(@"UserData\Favorites\FavoriteCryptoes.txt", true);
                writer.WriteLine(newFavorite.asset_id + ";");
                writer.Close();
            }
        }

        private void favorite_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            CryptoModel? removedFavorite = ((CheckBox)sender).DataContext as CryptoModel;
            if (removedFavorite != null)
            {
                string temp = File.ReadAllText(@"UserData\Favorites\FavoriteCryptoes.txt");
                StreamWriter writer = new StreamWriter(@"UserData\Favorites\FavoriteCryptoes.txt");
                writer.Write(temp.Replace(removedFavorite.asset_id + ";\r\n", ""));
                writer.Close();
            }
        }
    }
}
