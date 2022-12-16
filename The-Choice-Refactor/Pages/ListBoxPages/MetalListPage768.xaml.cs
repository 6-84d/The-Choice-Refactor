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
    /// Interaction logic for MetalListPage768.xaml
    /// </summary>
    public partial class MetalListPage768 : Page
    {
        public MetalListPage768()
        {
            InitializeComponent();
        }
        private void Metal_LstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MetalModel? selected = (sender as ListBox).SelectedItem as MetalModel;
            if (selected != null)
                Clipboard.SetText(selected.ToString());
        }
        private void favorite_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            MetalModel? newFavorite = ((CheckBox)sender).DataContext as MetalModel;
            if (newFavorite != null)
            {
                StreamWriter writer = new StreamWriter(@"UserData\Favorites\FavoriteMetals.txt", true);
                writer.WriteLine(newFavorite.name + ";");
                writer.Close();
            }
        }

        private void favorite_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            MetalModel? removedFavorite = ((CheckBox)sender).DataContext as MetalModel;
            if (removedFavorite != null)
            {
                string temp = File.ReadAllText(@"UserData\Favorites\FavoriteMetals.txt");
                StreamWriter writer = new StreamWriter(@"UserData\Favorites\FavoriteMetals.txt");
                writer.Write(temp.Replace(removedFavorite.name + ";\r\n", ""));
                writer.Close();
            }
        }
    }
}
