using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using The_Choice_Refactor.Classes;
using The_Choice_Refactor.Pages.ListBoxPages;
using System.Windows.Threading;

namespace The_Choice_Refactor.Pages.MainPages
{
    /// <summary>
    /// Interaction logic for SharePage.xaml
    /// </summary>
    public partial class SharePage : Page
    {
        private ShareListPage _list;
        private DispatcherTimer timer;
        private int Delay = 14;
        private bool isUpdating = false;
        public SharePage()
        {
            InitializeComponent();
            _list = new ShareListPage();
            _list.DataContext = new ShareVM();
            ListBoxFrame_Frm.Navigate(_list);
        }

        private void favoriteMode_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
                _list.DataContext = new ShareFavoriteVM();
            else
                _list.DataContext = new ShareSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }

        private void favoriteMode_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
                _list.DataContext = new ShareVM();
            else
                _list.DataContext = new ShareSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }

        private void search_TxtBlck_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
            {
                if (favoriteMode_ChBx.IsChecked == true)
                    _list.DataContext = new ShareFavoriteVM();
                else
                    _list.DataContext = new ShareVM();
            }
            else
                _list.DataContext = new ShareSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked);
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if(!isUpdating)
            { 
                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = new TimeSpan(0, 0, 1);
                isUpdating = true;
                timer.Start();
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Delay++;
            if (Delay == 15)
            {
                Delay = 0;
                _list.DataContext = new CryptoVM();
                ListBoxFrame_Frm.Navigate(_list);
                isUpdating = false;
                timer.Stop();
            }
        }
    }
}
