﻿using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using The_Choice_Refactor.Classes;
using The_Choice_Refactor.Pages.MainPages;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using System.Threading;
using System.Security.Policy;
using System.Windows.Input;
using System.Windows.Resources;

namespace The_Choice_Refactor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {    // application configuration (theme, language, currency)
        private Page currentPage;   // page that showed in frame at the moment
        private CryptoVM CryptoVM;
        private CurrencyVM CurrencyVM;
        private MetalVM MetalVM;
        private ShareVM ShareVM;
        public MainWindow()
        {
            ApiHelper.InitializeClient();

            InitializeComponent();                                                                        // init http client to work with apis

            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            currentPage = new MainPage(this);                                                                               // create MainPage and set to current page
            PageFrame_Frm.Navigate(currentPage);   // navigate frame to current page
            SetConfig();
        }

        private void MainPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            currentPage = new MainPage(this);           // create MainPage and set to current page
            PageFrame_Frm.Navigate(currentPage);    // navigate frame to current page
            SetConfig();
        }

        private void CryptoPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is CryptoPage))
            {
                currentPage = new CryptoPage(CryptoVM);
                PageFrame_Frm.Navigate(currentPage);
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void CurrencyPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is CurrencyPage))
            {
                currentPage = new CurrencyPage(CurrencyVM);
                PageFrame_Frm.Navigate(currentPage);
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void MetalPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is MetalPage))
            {
                currentPage = new MetalPage(MetalVM);
                PageFrame_Frm.Navigate(currentPage);
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void SharePage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is SharePage))
            {
                currentPage = new SharePage(ShareVM);
                PageFrame_Frm.Navigate(currentPage);
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void Options_Btn_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow options = new OptionsWindow(this);
            options.ShowDialog();
        }
        public void SetConfig()
        {
            if (The_Choice_Refactor.Properties.Settings.Default.Dark == true)
            {
                this.Cursor = new System.Windows.Input.Cursor(@"..\..\..\Resources\Pictures\Icons\WhiteCursor.cur");
                Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\..\Resources\Pictures\BackgroundDark.jpg", UriKind.Relative)));
            }
            else if (The_Choice_Refactor.Properties.Settings.Default.Dark == false)
            {
                this.Cursor = new System.Windows.Input.Cursor(@"..\..\..\Resources\Pictures\Icons\DarkCursor.cur");
                Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\..\Resources\Pictures\BackgroundLight.png", UriKind.Relative)));
            }
            if (currentPage.Title != "MainPage")
            {
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                MainGrid_Grd.Background = new ImageBrush(new BitmapImage(new Uri(@"..\..\..\Resources\Pictures\LinesBackgroundLight.png", UriKind.Relative)));
            }
        }
        public void LetsGo_Btn_Click()
        {
            Random r = new Random();
            switch (r.Next(0, 4))
            {
                case 0:
                    currentPage = new CryptoPage(CryptoVM);
                    break;
                case 1:
                    currentPage = new CurrencyPage(CurrencyVM);
                    break;
                case 2:
                    currentPage = new MetalPage(MetalVM);
                    break;
                case 3:
                    currentPage = new SharePage(ShareVM);
                    break;
                default:
                    return;
            }
            PageFrame_Frm.Navigate(currentPage);
        }
    }
}
