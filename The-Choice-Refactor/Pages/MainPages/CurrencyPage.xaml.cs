using System.Windows;
using System.Windows.Controls;
using The_Choice_Refactor.Classes;
using The_Choice_Refactor.Pages.ListBoxPages;

namespace The_Choice_Refactor.Pages.MainPages
{
    /// <summary>
    /// Interaction logic for CurrencyPage.xaml
    /// </summary>
    public partial class CurrencyPage : Page
    {
        private CurrencyListPage _list;
        public CurrencyPage()
        {
            InitializeComponent();
            _list = new CurrencyListPage();
            _list.DataContext = new CurrencyVM();
            ListBoxFrame_Frm.Navigate(_list);
        }
        private void favoriteMode_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
                _list.DataContext = new CurrencyFavoriteVM();
            else
                _list.DataContext = new CurrencySearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }
        private void favoriteMode_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
                _list.DataContext = new CurrencyVM();
            else
                _list.DataContext = new CurrencySearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }

        private void search_TxtBlck_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
            {
                if (favoriteMode_ChBx.IsChecked == true)
                    _list.DataContext = new CurrencyFavoriteVM();
                else
                    _list.DataContext = new CurrencyVM();
            }
            else
                _list.DataContext = new CurrencySearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }
    }
}
