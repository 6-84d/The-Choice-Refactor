using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Controls;
using The_Choice_Refactor.Classes;
using System.IO;
using System.Windows.Media;

namespace The_Choice_Refactor.Pages.OptionsPages
{
    /// <summary>
    /// Interaction logic for MainSettingsPage.xaml
    /// </summary>
    public partial class MainOptionsPage : Page
    {
        OptionsWindow parent;
        public MainOptionsPage(Config config, OptionsWindow parent)
        {
            InitializeComponent();
            DataContext = config;
            this.parent = parent;
        }

        private void themeSwitch_ChBx_Checked(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(@"..\..\..\UserData\Configuration.json", JsonConvert.SerializeObject(DataContext as Config));
            parent.SetConfig();
        }

        private void themeSwitch_ChBx_Unchecked(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(@"..\..\..\UserData\Configuration.json", JsonConvert.SerializeObject(DataContext as Config));
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
    }
}
