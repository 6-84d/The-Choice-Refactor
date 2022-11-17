using System.Windows;
using System.Windows.Controls;
using The_Choice_Refactor.Classes;
using The_Choice_Refactor.Pages.ListBoxPages;

namespace The_Choice_Refactor.Pages.MainPages
{
    /// <summary>
    /// Interaction logic for MetalPage.xaml
    /// </summary>
    public partial class MetalPage : Page
    {
        private MetalListPage _list;
        public MetalPage()
        {
            InitializeComponent();
            _list = new MetalListPage();
            _list.DataContext = new MetalVM();
            ListBoxFrame_Frm.Navigate(_list);
        }
        private void favoriteMode_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
                _list.DataContext = new MetalFavoriteVM();
            else
                _list.DataContext = new MetalSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }

        private void favoriteMode_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
                _list.DataContext = new MetalVM();
            else
                _list.DataContext = new MetalSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }

        private void search_TxtBlck_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
            {
                if (favoriteMode_ChBx.IsChecked == true)
                    _list.DataContext = new MetalFavoriteVM();
                else
                    _list.DataContext = new MetalVM();
            }
            else
                _list.DataContext = new MetalSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }
    }
}
