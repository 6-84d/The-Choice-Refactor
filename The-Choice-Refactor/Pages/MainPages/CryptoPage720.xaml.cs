using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using The_Choice_Refactor.Pages.ListBoxPages;
using The_Choice_Refactor.Classes;
using System;
using The_Choice_Refactor.Interfaces;


namespace The_Choice_Refactor.Pages.MainPages
{
    /// <summary>
    /// Interaction logic for CryptoPage720.xaml
    /// </summary>
    public partial class CryptoPage720 : Page
    {
        private Page _list;
        private DispatcherTimer timer;
        private int Delay = 0;
        private bool isUpdating = false;
        public CryptoPage720()
        {
            InitializeComponent();
            timer = new DispatcherTimer();

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            LoadList(new CryptoVM());
        }
        private async void LoadList(ICryptoVM viewModel)
        {
            try
            {
                bool isSucces = await viewModel.Load();
                _list = new CryptoListPage720();
                _list.DataContext = viewModel;
            }
            catch (Exception ex)
            {
                _list = new MistakePage();
            }
            ListBoxFrame_Frm.Navigate(_list);
        }
        private void favoriteMode_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
                LoadList(new CryptoFavoriteVM());
            else
                LoadList(new CryptoSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked));
        }

        private void favoriteMode_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
                LoadList(new CryptoVM());
            else
                LoadList(new CryptoSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked));
        }

        private void search_TxtBlck_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
            {
                if (favoriteMode_ChBx.IsChecked == true)
                    LoadList(new CryptoFavoriteVM());
                else
                    LoadList(new CryptoVM());
            }
            else
                LoadList(new CryptoSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked));
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isUpdating)
            {
                if (favoriteMode_ChBx.IsChecked == true)
                {
                    if (search_TxtBlck.Text.Length == 0)
                        LoadList(new CryptoFavoriteVM());
                    else
                        LoadList(new CryptoSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked));
                    LoadList(new CryptoVM());
                }
                else
                {
                    if (search_TxtBlck.Text.Length == 0)
                        LoadList(new CryptoVM());
                    else
                        LoadList(new CryptoSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked));
                }
                isUpdating = true;
                timer.Start();
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Delay++;
            UpdateButton.Opacity = 0.1;
            if (Delay == 15)
            {
                Delay = 0;
                UpdateButton.Opacity = 1;
                isUpdating = false;
                timer.Stop();
            }
        }
    }
}
