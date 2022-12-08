﻿using System.Windows;
using System.Threading;
using System.Windows.Controls;
using The_Choice_Refactor.Classes;
using The_Choice_Refactor.Pages.ListBoxPages;
using System.Windows.Threading;
using System;
using The_Choice_Refactor.Interfaces;
using System.Timers;

namespace The_Choice_Refactor.Pages.MainPages
{
    /// <summary>
    /// Interaction logic for MetalPage768.xaml
    /// </summary>
    /// 
    public partial class MetalPage768 : Page
    {
        private Page _list;
        private DispatcherTimer timer;
        private int Delay = 0;
        private bool isUpdating = false;
        public MetalPage768()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);

            LoadList(new MetalVM());
        }
        private async void LoadList(IMetalVM viewModel)
        {
            try
            {
                bool isSucces = await viewModel.Load();
                _list = new MetalListPage();
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
                LoadList(new MetalFavoriteVM());
            else
                LoadList(new MetalSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked));
        }

        private void favoriteMode_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
                LoadList(new MetalVM());
            else
                LoadList(new MetalSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked));
        }

        private void search_TxtBlck_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search_TxtBlck.Text.Length == 0)
            {
                if (favoriteMode_ChBx.IsChecked == true)
                    LoadList(new MetalFavoriteVM());
                else
                    LoadList(new MetalVM());
            }
            else
                LoadList(new MetalSearchVM(search_TxtBlck.Text, favoriteMode_ChBx.IsChecked));
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isUpdating)
            {
                LoadList(new MetalVM());
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
