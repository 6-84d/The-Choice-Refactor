using System;
using System.Windows;
using System.Windows.Controls;
using The_Choice_Refactor.Classes;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Markup;

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
        bool isFirstTimeCurr = true;
        bool isFirstTimeTheme = true;
        public MainOptionsPage(OptionsWindow parent)
        {
            InitializeComponent();

            // ban to paste text
            DataObject.AddPastingHandler(From_TxtBx, this.OnCancelCommand);

            converter = new AssetConverter();
            Result_TxtBx.DataContext = converter;

            this.parent = parent;
            App.LanguageChanged += LanguageChanged;

            CultureInfo currLang = App.Language;
            menuLanguage.Header = currLang.ToString();

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

            ComboBoxItem item = new ComboBoxItem();
            item.Content = "USD";
            CurrCombo.Items.Add(item);
            item = new ComboBoxItem();
            item.Content = "UAH";
            CurrCombo.Items.Add(item);
            item = new ComboBoxItem();
            item.Content = "Big";
            SizeCombo.Items.Add(item);
            item = new ComboBoxItem();
            item.Content = "Medium";
            SizeCombo.Items.Add(item);
            item = new ComboBoxItem();
            item.Content = "Small";
            SizeCombo.Items.Add(item);
            SizeCombo.SelectedIndex = The_Choice_Refactor.Properties.Settings.Default.Size;
            themeSwitch_ChBx.DataContext = The_Choice_Refactor.Properties.Settings.Default;
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

        private async void FromAssetType_CmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FromAsset_CmbBx.SelectedIndex = -1;
            FromAsset_CmbBx.DataContext = null;
            switch (FromAssetType_CmbBx.SelectedIndex)
            {
                case 0:
                    CryptoVM cryptoVM = new CryptoVM();
                    try
                    {
                        await cryptoVM.Load();
                        FromAsset_CmbBx.DataContext = cryptoVM;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FindResource("TextError") as string);
                        ToAssetType_CmbBx.SelectedIndex = -1;
                    }
                    break;
                case 1:
                    CurrencyVM currencyVM = new CurrencyVM();
                    try
                    {
                        await currencyVM.Load();
                        FromAsset_CmbBx.DataContext = currencyVM;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FindResource("TextError") as string);
                        ToAssetType_CmbBx.SelectedIndex = -1;
                    }
                    break;
                case 2:
                    MetalVM metalVM = new MetalVM();
                    try
                    {
                        await metalVM.Load();
                        FromAsset_CmbBx.DataContext = metalVM;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FindResource("TextError") as string);
                        ToAssetType_CmbBx.SelectedIndex = -1;
                    }
                    break;
                case 3:
                    ShareVM shareVM = new ShareVM();
                    try
                    {
                        await shareVM.Load();
                        FromAsset_CmbBx.DataContext = shareVM;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FindResource("TextError") as string);
                        ToAssetType_CmbBx.SelectedIndex = -1;
                    }
                    break;
                default:
                    return;
            }
        }

        private async void ToAssetType_CmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToAsset_CmbBx.SelectedIndex = -1;
            ToAsset_CmbBx.DataContext = null;
            switch (ToAssetType_CmbBx.SelectedIndex)
            {
                case 0:
                    CryptoVM cryptoVM = new CryptoVM();
                    try
                    {
                        await cryptoVM.Load();
                        ToAsset_CmbBx.DataContext = cryptoVM;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FindResource("TextError") as string);
                        ToAssetType_CmbBx.SelectedIndex = -1;
                    }
                    break;
                case 1:
                    CurrencyVM currencyVM = new CurrencyVM();
                    try
                    {
                        await currencyVM.Load();
                        ToAsset_CmbBx.DataContext = currencyVM;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FindResource("TextError") as string);
                        ToAssetType_CmbBx.SelectedIndex = -1;
                    }
                    break;
                case 2:
                    MetalVM metalVM = new MetalVM();
                    try
                    {
                        await metalVM.Load();
                        ToAsset_CmbBx.DataContext = metalVM;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FindResource("TextError") as string);
                        ToAssetType_CmbBx.SelectedIndex = -1;
                    }
                    break;
                case 3:
                    ShareVM shareVM = new ShareVM();
                    try
                    {
                        await shareVM.Load();
                        ToAsset_CmbBx.DataContext = shareVM;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FindResource("TextError") as string);
                        ToAssetType_CmbBx.SelectedIndex = -1;
                    }
                    break;
                default:
                    return;
            }
        }
        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;
            menuLanguage.Header = currLang.ToString();
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
            if (isFirstTimeCurr)
            {
                if (The_Choice_Refactor.Properties.Settings.Default.CurrView == 1)
                    CurrCombo.SelectedIndex = 0;
                else
                    CurrCombo.SelectedIndex = 1;

                isFirstTimeCurr = false;

                return;
            }
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
            else
                Result_TxtBx.Text = "";
        }

        private void From_TxtBx_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // [^0-9,]+
            // [0-9]+,\d\d
            // [0-9]+,\d
            // [0-9]

            if (Regex.IsMatch(e.Text, @"[\d\,]+"))
            {
                if (Regex.IsMatch(e.Text, @"\,"))
                {
                    if (Regex.IsMatch((sender as TextBox).Text + e.Text, @"^[0-9]+\,{1}$"))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
                else if (Regex.IsMatch(e.Text, "[0-9]"))
                {
                    if (Regex.IsMatch((sender as TextBox).Text + e.Text, @"^[0-9]+[,]{1}\d{0,2}$") ||
                        Regex.IsMatch((sender as TextBox).Text, @"^[0-9]*$")
                        )
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ToAsset_CmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ToAssetType_CmbBx.SelectedIndex)
            {
                case 0:
                    converter.ToPrice = (ToAsset_CmbBx.SelectedItem as CryptoModel) is null ? null : (ToAsset_CmbBx.SelectedItem as CryptoModel).price;
                    break;
                case 1:
                    converter.ToPrice = (ToAsset_CmbBx.SelectedItem as CurrencyModel) is null ? null : (ToAsset_CmbBx.SelectedItem as CurrencyModel).price;
                    break;
                case 2:
                    converter.ToPrice = (ToAsset_CmbBx.SelectedItem as MetalModel) is null ? null : (ToAsset_CmbBx.SelectedItem as MetalModel).price;
                    break;
                case 3:
                    converter.ToPrice = (ToAsset_CmbBx.SelectedItem as ShareModel) is null ? null : (ToAsset_CmbBx.SelectedItem as ShareModel).lastPrice;
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
                    converter.FromPrice = (FromAsset_CmbBx.SelectedItem as CryptoModel) is null ? null : (FromAsset_CmbBx.SelectedItem as CryptoModel).price;
                    break;
                case 1:
                    converter.FromPrice = (FromAsset_CmbBx.SelectedItem as CurrencyModel) is null ? null : (FromAsset_CmbBx.SelectedItem as CurrencyModel).price;
                    break;
                case 2:
                    converter.FromPrice = (FromAsset_CmbBx.SelectedItem as MetalModel) is null ? null : (FromAsset_CmbBx.SelectedItem as MetalModel).price;
                    break;
                case 3:
                    converter.FromPrice = (FromAsset_CmbBx.SelectedItem as ShareModel) is null ? null : (FromAsset_CmbBx.SelectedItem as ShareModel).lastPrice;
                    break;
                default:
                    return;
            }
            converter.Calculate();
        }

        private void From_TxtBx_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        private void OnCancelCommand(object sender, DataObjectEventArgs e)
        {
            e.CancelCommand();
        }

        private void SizeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SizeCombo.SelectedIndex == 0 && this.parent.parent1920 == null)
            {
                if (this.parent.parent1280 != null)
                {
                    this.parent.parent1280.Close();
                    this.parent.parent1280 = null;
                }
                else if (this.parent.parent1366 != null)
                {
                    this.parent.parent1366.Close();
                    this.parent.parent1366 = null;
                }
                this.parent.parent1920 = new MainWindow();
                this.parent.parent1920.isOpen = true;
                this.parent.parent1920.Show();
                The_Choice_Refactor.Properties.Settings.Default.Size = 0;
                this.parent.Close();
            }
            else if (SizeCombo.SelectedIndex == 1 && this.parent.parent1366 == null)
            {
                if (this.parent.parent1280 != null)
                {
                    this.parent.parent1280.Close();
                    this.parent.parent1280 = null;
                }
                else if (this.parent.parent1920 != null)
                {
                    this.parent.parent1920.Close();
                    this.parent.parent1920 = null;
                }
                this.parent.parent1366 = new MainWindow1366();
                this.parent.parent1366.isOpen = true;
                this.parent.parent1366.Show();
                The_Choice_Refactor.Properties.Settings.Default.Size = 1;
                this.parent.Close();
            }
            else if (SizeCombo.SelectedIndex == 2 && this.parent.parent1280 == null)
            {
                if (this.parent.parent1366 != null)
                {
                    this.parent.parent1366.Close();
                    this.parent.parent1366 = null;
                }
                else if (this.parent.parent1920 != null)
                {
                    this.parent.parent1920.Close();
                    this.parent.parent1920 = null;
                }
                this.parent.parent1280 = new MainWindow1280();
                this.parent.parent1280.isOpen = true;
                this.parent.parent1280.Show();
                The_Choice_Refactor.Properties.Settings.Default.Size = 2;
                this.parent.Close();
            }
        }
    }
}
