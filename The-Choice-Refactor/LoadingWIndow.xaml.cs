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

namespace The_Choice_Refactor
{
    /// <summary>
    /// Interaction logic for LoadingWIndow.xaml
    /// </summary>
    public partial class LoadingWIndow : Window
    {
        public LoadingWIndow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Back.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Resources/Pictures/LoadingScreen.png")));
        }
    }
}
