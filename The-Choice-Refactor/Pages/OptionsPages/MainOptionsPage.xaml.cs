using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Controls;
using The_Choice_Refactor.Classes;
using System.IO;
using System.Windows.Media;
using System.Globalization;

namespace The_Choice_Refactor.Pages.OptionsPages
{
    /// <summary>
    /// Interaction logic for MainSettingsPage.xaml
    /// </summary>
    public partial class MainOptionsPage : Page
    {
        OptionsWindow parent;
        public MainOptionsPage(OptionsWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            App.LanguageChanged += LanguageChanged;

            CultureInfo currLang = App.Language;

            //Заполняем меню смены языка:
            menuLanguage.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                menuLanguage.Items.Add(menuLang);
            }
        }

        private void themeSwitch_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            The_Choice_Refactor.Properties.Settings.Default.Dark = true;
            parent.SetConfig();
        }

        private void themeSwitch_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            The_Choice_Refactor.Properties.Settings.Default.Dark = false;
            parent.SetConfig();

        }

        private void FromAssetType_CmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(FromAssetType_CmbBx.SelectedIndex)
            {
                case 0:
                    FromAsset_CmbBx.DataContext = new CryptoVM();
                    break;
                case 1:
                    FromAsset_CmbBx.DataContext = new CurrencyVM();
                    break;
                case 2:
                    FromAsset_CmbBx.DataContext = new MetalVM();
                    break;
                case 3:
                    FromAsset_CmbBx.DataContext = new ShareVM();
                    break;
                default:
                    return;
            }
    }

        private void ToAssetType_CmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (FromAssetType_CmbBx.SelectedIndex)
            {
                case 0:
                    ToAsset_CmbBx.DataContext = new CryptoVM();
                    break;
                case 1:
                    ToAsset_CmbBx.DataContext = new CurrencyVM();
                    break;
                case 2:
                    ToAsset_CmbBx.DataContext = new MetalVM();
                    break;
                case 3:
                    ToAsset_CmbBx.DataContext = new ShareVM();
                    break;
                default:
                    return;
            }
        }
        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in menuLanguage.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurrCombo.SelectedIndex == 0)
            {
                The_Choice_Refactor.Properties.Settings.Default.CurrView = 1;
            }
            else if(CurrCombo.SelectedIndex == 1)
            {
                The_Choice_Refactor.Properties.Settings.Default.CurrView = 37;
            }
        }
    }
}
