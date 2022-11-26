using System;
using System.Windows;
using System.Windows.Controls;
using The_Choice_Refactor.Classes;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace The_Choice_Refactor.Pages.OptionsPages
{
    /// <summary>
    /// Interaction logic for MainSettingsPage.xaml
    /// </summary>
    public partial class MainOptionsPage : Page
    {
        class AssetConverter : INotifyPropertyChanged
        {
            public double? Count { get; set; }
            public double? FromPrice { get; set; }
            public double? ToPrice { get; set; }
            private double? _result;
            public double? Result
            {
                get
                {
                    return _result;
                }
                set
                {
                    _result = value;
                    OnPropertyChanged("Result");
                }
            }
            public AssetConverter()
            {
                Count = null;
                FromPrice = null;
                ToPrice = null;
            }
            public void Calculate()
            {
                if (Count != null && FromPrice != null && ToPrice != null)
                    Result = (Count * FromPrice) / ToPrice;
            }
            public event PropertyChangedEventHandler? PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        OptionsWindow parent;
        AssetConverter converter;
        public MainOptionsPage(OptionsWindow parent)
        {
            InitializeComponent();

            converter = new AssetConverter();
            Result_TxtBx.DataContext = converter;

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
            switch (FromAssetType_CmbBx.SelectedIndex)
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
            switch (ToAssetType_CmbBx.SelectedIndex)
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
            else if (CurrCombo.SelectedIndex == 1)
            {
                The_Choice_Refactor.Properties.Settings.Default.CurrView = 37;
            }
        }

        private void From_TxtBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (From_TxtBx.Text.Length > 0)
            {
                converter.Count = Convert.ToDouble(From_TxtBx.Text);
                converter.Calculate();
            }
        }

        private void From_TxtBx_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ToAsset_CmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ToAssetType_CmbBx.SelectedIndex)
            {
                case 0:
                    converter.ToPrice = (ToAsset_CmbBx.SelectedItem as CryptoModel).price;
                    break;
                case 1:
                    converter.ToPrice = (ToAsset_CmbBx.SelectedItem as CurrencyModel).price;
                    break;
                case 2:
                    converter.ToPrice = (ToAsset_CmbBx.SelectedItem as MetalModel).price;
                    break;
                case 3:
                    converter.ToPrice = (ToAsset_CmbBx.SelectedItem as ShareModel).lastPrice;
                    break;
                default:
                    return;
            }
            converter.Calculate();
        }

        private void FromAsset_CmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (FromAssetType_CmbBx.SelectedIndex)
            {
                case 0:
                    converter.FromPrice = (FromAsset_CmbBx.SelectedItem as CryptoModel).price;
                    break;
                case 1:
                    converter.FromPrice = (FromAsset_CmbBx.SelectedItem as CurrencyModel).price;
                    break;
                case 2:
                    converter.FromPrice = (FromAsset_CmbBx.SelectedItem as MetalModel).price;
                    break;
                case 3:
                    converter.FromPrice = (FromAsset_CmbBx.SelectedItem as ShareModel).lastPrice;
                    break;
                default:
                    return;
            }
            converter.Calculate();
        }
    }
}
