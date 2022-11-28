using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using The_Choice_Refactor.Pages.ListBoxPages;
using The_Choice_Refactor.Classes;
using System;
using System.Threading;
using System.Windows.Media;

namespace The_Choice_Refactor.Pages.MainPages
{
    /// <summary>
    /// Interaction logic for CryptoPage.xaml
    /// </summary>
    public partial class CryptoPage : Page
    {
        private CryptoListPage _list;
        private DispatcherTimer timer;
        private int Delay = 14;
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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Delay++;
            if(Delay == 15)
            {
                Delay = 0;
                _list.DataContext = new CryptoVM();
                ListBoxFrame_Frm.Navigate(_list);
                timer.Stop();
            }
        }
    }
}
