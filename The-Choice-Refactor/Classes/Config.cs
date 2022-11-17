using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace The_Choice_Refactor.Classes
{
    public class Config: INotifyPropertyChanged
    {
        private bool darkTheme;
        public bool DarkTheme
        {
            get { return darkTheme; }
            set
            {
                darkTheme = value;
                OnPropertyChanged("DarkTheme");
            }
        }
        public string? Currency { get; set; }
        public string? Language { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
