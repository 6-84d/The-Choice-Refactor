using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using The_Choice_Refactor.Pages.OptionsPages;

namespace The_Choice_Refactor
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private MainWindow parent;  // application configuration (theme, language, currency)
        private MainWindow1280 parent1280;  // application configuration (theme, language, currency)
        private MainWindow1366 parent1366;  // application configuration (theme, language, currency)
        private Page currentPage;   // page that showed in frame at the moment
        public OptionsWindow(MainWindow parent)
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;      // ban resizing 
            this.parent = parent;
            currentPage = new MainOptionsPage(this);
            OptionsFrame_Frm.Navigate(currentPage);
            this.SetConfig();
            // navigate frame to current page
        }
        public OptionsWindow(MainWindow1280 parent)
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;      // ban resizing 
            this.parent1280 = parent;
            currentPage = new MainOptionsPage(this);
            OptionsFrame_Frm.Navigate(currentPage);
            this.SetConfig();
            // navigate frame to current page
        }
        public OptionsWindow(MainWindow1366 parent)
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;      // ban resizing 
            this.parent1366 = parent;
            currentPage = new MainOptionsPage(this);
            OptionsFrame_Frm.Navigate(currentPage);
            this.SetConfig();
            // navigate frame to current page
        }
        private void MainOptions_Btn_Click(object sender, RoutedEventArgs e)
        {
            currentPage = new MainOptionsPage(this);        // create MainOptionsPage and set to current page
            OptionsFrame_Frm.Navigate(currentPage);     // navigate frame to current page
        }

        private void Tutorial_Btn_Click(object sender, RoutedEventArgs e)
        {
            currentPage = new TutorialPage();           // create TutorialPage and set to current page
            OptionsFrame_Frm.Navigate(currentPage);     // navigate frame to current page
        }

        private void AboutUs_Btn_Click(object sender, RoutedEventArgs e)
        {
            currentPage = new AboutUsPage();            // create AboutUsPage and set to current page
            OptionsFrame_Frm.Navigate(currentPage);     // navigate frame to current page   
        }
        public void SetConfig()
        {
            if (The_Choice_Refactor.Properties.Settings.Default.Dark == true)
            {
                this.Cursor = new System.Windows.Input.Cursor(App.GetResourceStream(new Uri(@"pack://application:,,,/Resources/Pictures/Icons/WhiteCursor.cur")).Stream);
                Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(14), Convert.ToByte(17), Convert.ToByte(34)));
            }
            else if (The_Choice_Refactor.Properties.Settings.Default.Dark == false)
            {
                this.Cursor = new System.Windows.Input.Cursor(App.GetResourceStream(new Uri(@"pack://application:,,,/Resources/Pictures/Icons/DarkCursor.cur")).Stream);
                Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(101), Convert.ToByte(84), Convert.ToByte(133)));
            }
            if (parent != null) parent.SetConfig();
            else if (parent1280 != null) parent1280.SetConfig();
            else parent1366.SetConfig();
        }
    }
}
