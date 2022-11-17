using System.IO;
using System.Windows;
using System.Windows.Controls;
using The_Choice_Refactor.Classes;

namespace The_Choice_Refactor.Pages.ListBoxPages
{
    /// <summary>
    /// Interaction logic for MetalListPage.xaml
    /// </summary>
    public partial class MetalListPage : Page
    {
        public MetalListPage()
        {
            InitializeComponent();
        }

        private void Metal_LstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void favorite_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            MetalModel? newFavorite = ((CheckBox)sender).DataContext as MetalModel;
            if (newFavorite != null)
            {
                StreamWriter writer = new StreamWriter(@"..\..\..\UserData\Favorites\FavoriteMetals.txt", true);
                writer.WriteLine(newFavorite.name + ";");
                writer.Close();
            }
        }

        private void favorite_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            MetalModel? removedFavorite = ((CheckBox)sender).DataContext as MetalModel;
            if (removedFavorite != null)
            {
                string temp = File.ReadAllText(@"..\..\..\UserData\Favorites\FavoriteMetals.txt");
                StreamWriter writer = new StreamWriter(@"..\..\..\UserData\Favorites\FavoriteMetals.txt");
                writer.Write(temp.Replace(removedFavorite.name + ";\r\n", ""));
                writer.Close();
            }
        }
    }
}
