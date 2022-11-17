using System.IO;
using System.Windows;
using System.Windows.Controls;
using The_Choice_Refactor.Classes;

namespace The_Choice_Refactor.Pages.ListBoxPages
{
    /// <summary>
    /// Interaction logic for ShareListPage.xaml
    /// </summary>
    public partial class ShareListPage : Page
    {
        public ShareListPage()
        {
            InitializeComponent();
        }

        private void Share_LstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void favorite_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            ShareModel? newFavorite = ((CheckBox)sender).DataContext as ShareModel;
            if (newFavorite != null)
            {
                StreamWriter writer = new StreamWriter(@"..\..\..\UserData\Favorites\FavoriteShares.txt", true);
                writer.WriteLine(newFavorite.symbol + ";");
                writer.Close();
            }
        }

        private void favorite_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            ShareModel? removedFavorite = ((CheckBox)sender).DataContext as ShareModel;
            if (removedFavorite != null)
            {
                string temp = File.ReadAllText(@"..\..\..\UserData\Favorites\FavoriteShares.txt");
                StreamWriter writer = new StreamWriter(@"..\..\..\UserData\Favorites\FavoriteShares.txt");
                writer.Write(temp.Replace(removedFavorite.symbol + ";\r\n", ""));
                writer.Close();
            }
        }
    }
}
