﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using The_Choice_Refactor.Classes;
using The_Choice_Refactor.Pages.MainPages;

namespace The_Choice_Refactor
{
    /// <summary>
    /// Interaction logic for MainWindow1280.xaml
    /// </summary>
    public partial class MainWindow1280 : Window
    {
        private Page currentPage;   // page that showed in frame at the moment
        public MainWindow1280()
        {
            ApiHelper.InitializeClient();
            LoadingWIndow loadingWIndow = new LoadingWIndow();
            loadingWIndow.Show();
            Thread.Sleep(1000);
            loadingWIndow.Close();
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.ResizeMode = ResizeMode.CanMinimize;
           // currentPage = new MainPage(this);                                                                               // create MainPage and set to current page
            PageFrame_Frm.Navigate(currentPage);   // navigate frame to current page
            SetConfig();
        }
        private void MainPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            //currentPage = new MainPage(this);           // create MainPage and set to current page
            PageFrame_Frm.Navigate(currentPage);    // navigate frame to current page
            if (currentPage is MainPage)
                MainGrid_Grd.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Pictures/LinesBackgroundLight.png")));
            else
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void CryptoPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is CryptoPage))
            {
                currentPage = new CryptoPage();
                PageFrame_Frm.Navigate(currentPage);
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void CurrencyPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is CurrencyPage))
            {
                currentPage = new CurrencyPage();
                PageFrame_Frm.Navigate(currentPage);
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void MetalPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is MetalPage))
            {
                currentPage = new MetalPage();
                PageFrame_Frm.Navigate(currentPage);
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void SharePage_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentPage is SharePage))
            {
                currentPage = new SharePage();
                PageFrame_Frm.Navigate(currentPage);
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void Options_Btn_Click(object sender, RoutedEventArgs e)
        {
            //OptionsWindow options = new OptionsWindow(this);
            //options.ShowDialog();
        }
        public void SetConfig()
        {
            if (The_Choice_Refactor.Properties.Settings.Default.Dark == true)
            {
                this.Cursor = new System.Windows.Input.Cursor(App.GetResourceStream(new Uri(@"pack://application:,,,/Resources/Pictures/Icons/WhiteCursor.cur")).Stream);
                Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Pictures/BackgroundDark.jpg")));
            }
            else if (The_Choice_Refactor.Properties.Settings.Default.Dark == false)
            {
                this.Cursor = new System.Windows.Input.Cursor(App.GetResourceStream(new Uri(@"pack://application:,,,/Resources/Pictures/Icons/DarkCursor.cur")).Stream);
                Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Pictures/BackgroundLight.png")));
            }
            if (currentPage is MainPage)
                MainGrid_Grd.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Pictures/LinesBackgroundLight.png")));
            else
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
        }
        public void LetsGo_Btn_Click()
        {
            Random r = new Random();
            switch (r.Next(0, 4))
            {
                case 0:
                    currentPage = new CryptoPage();
                    break;
                case 1:
                    currentPage = new CurrencyPage();
                    break;
                case 2:
                    currentPage = new MetalPage();
                    break;
                case 3:
                    currentPage = new SharePage();
                    break;
                default:
                    return;
            }
            if (currentPage is MainPage)
                MainGrid_Grd.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Pictures/LinesBackgroundLight.png")));
            else
                MainGrid_Grd.Background = new SolidColorBrush(Colors.Transparent);
            PageFrame_Frm.Navigate(currentPage);
        }
    }
}