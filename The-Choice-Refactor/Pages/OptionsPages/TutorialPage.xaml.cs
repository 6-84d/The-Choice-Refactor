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
        int index = 0;
        public TutorialPage()
        {
            InitializeComponent();
            TutorialText.Add(FindResource("Thelang").ToString());
            TutorialText.Add(FindResource("Thetheme").ToString());
            TutorialText.Add(FindResource("Thecurr").ToString());
            TutorialText.Add(FindResource("Thecurrconv").ToString());
            TutorialText.Add(FindResource("Thebutton").ToString());
            TutorialText.Add(FindResource("Thereare").ToString());
            TutorialText.Add(FindResource("Indiff").ToString());
            TutorialText.Add(FindResource("Inthe").ToString());
            TutText.Text = TutorialText[index];
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            index++;
            if (index > 7)
                index = 0;
            TutText.Text = TutorialText[index];
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            index--;
            if (index < 0)
                index = 7;
            TutText.Text = TutorialText[index];
        }
    }
}
