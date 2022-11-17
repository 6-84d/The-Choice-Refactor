using System.Windows;
using System.Windows.Controls;
using The_Choice_Refactor.Pages.ListBoxPages;
using The_Choice_Refactor.Classes;

namespace The_Choice_Refactor.Pages.MainPages
{
    /// <summary>
    /// Interaction logic for CryptoPage.xaml
    /// </summary>
    public partial class CryptoPage : Page
    {
        private CryptoListPage _list;
        public CryptoPage()
        {
            InitializeComponent();
            _list = new CryptoListPage();
            _list.DataContext = new CryptoVM();
            ListBoxFrame_Frm.Navigate(_list);
        }

        private void favoriteMode_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
                _list.DataContext = new CryptoFavoriteVM();
            else
                _list.DataContext = new CryptoSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }

        private void favoriteMode_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
                _list.DataContext = new CryptoVM();
            else
                _list.DataContext = new CryptoSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }

        private void search_TxtBlck_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
            {
                if(favoriteMode_ChBx.IsChecked == true)
                    _list.DataContext = new CryptoFavoriteVM();
                else
                    _list.DataContext = new CryptoVM();
            }
            else
                _list.DataContext = new CryptoSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }
    }
}
