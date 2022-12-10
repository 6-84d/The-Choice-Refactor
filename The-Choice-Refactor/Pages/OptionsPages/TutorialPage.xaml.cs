using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace The_Choice_Refactor.Pages.OptionsPages
{
    /// <summary>
    /// Interaction logic for TutorialPage.xaml
    /// </summary>
    public partial class TutorialPage : Page
    {
        List<String> TutorialText = new List<string>();
        List<Uri> Images = new List<Uri>();
        int index = 0;
        public TutorialPage()
        {
            InitializeComponent();
            TutorialText.Add(FindResource("Thelang").ToString());
            TutorialText.Add(FindResource("Thetheme").ToString());
            TutorialText.Add(FindResource("Thecurr").ToString());
            TutorialText.Add(FindResource("Thecurrconv").ToString());
            TutorialText.Add(FindResource("Thebutton").ToString());
            TutorialText.Add(FindResource("Thereare").ToString() + ". " + FindResource("Indiff").ToString());
            TutorialText.Add(FindResource("Inthe").ToString());
            if (The_Choice_Refactor.Properties.Settings.Default.Dark == false)
            {
                Images.Add(new Uri(@"pack://application:,,,/Resources/Pictures/lang_change.png"));
                Images.Add(new Uri(@"pack://application:,,,/Resources/Pictures/theme_change.png"));
                Images.Add(new Uri(@"pack://application:,,,/Resources/Pictures/curr_change.png"));
                Images.Add(new Uri(@"pack://application:,,,/Resources/Pictures/conv_change.png"));
            }
            else
            {
                Images.Add(new Uri(@"pack://application:,,,/Resources/Pictures/lang_change_dark.png"));
                Images.Add(new Uri(@"pack://application:,,,/Resources/Pictures/theme_change_dark.png"));
                Images.Add(new Uri(@"pack://application:,,,/Resources/Pictures/curr_change_dark.png"));
                Images.Add(new Uri(@"pack://application:,,,/Resources/Pictures/conv_change_dark.png"));

            }
            Images.Add(new Uri(@"pack://application:,,,/Resources/Pictures/letsgo.png"));
            Images.Add(new Uri(@"pack://application:,,,/Resources/Pictures/pages.png"));
            Images.Add(new Uri(@"pack://application:,,,/Resources/Pictures/about_us.png"));
            TutText.Text = TutorialText[index];
            tut_image.Source = new BitmapImage(Images[index]);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            index++;
            if (index > 6)
                index = 0;
            tut_image.Source = new BitmapImage(Images[index]);
            TutText.Text = TutorialText[index];
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            index--;
            if (index < 0)
                index = 6;
            tut_image.Source = new BitmapImage(Images[index]);
            TutText.Text = TutorialText[index];
        }
    }
}
