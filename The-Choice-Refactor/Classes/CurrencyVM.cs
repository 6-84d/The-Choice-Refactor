using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.IO;

namespace The_Choice_Refactor.Classes
{
    public class CurrencyVM : INotifyPropertyChanged
    {
        public ObservableCollection<CurrencyModel> assets { get; set; }   // all currencies collection
        public ObservableCollection<string> names { get; set; }
        private CurrencyModel? selected;                                      // selected currency
        public CurrencyModel? Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
        public CurrencyVM()
        {
            assets = new ObservableCollection<CurrencyModel>();
            names = new ObservableCollection<string>();
            Load();
        }
        public async void Load()
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            result = await CurrencyGet.Load();                                                                                  // get info from api

            string[] favoritesIDs = File.ReadAllText(@"..\..\..\UserData\Favorites\FavoriteCurrencies.txt").Split(";\r\n");     // load favorites list

            int i = 0;
            foreach (var res in result)
            {
                CurrencyModel currency = new CurrencyModel();
                currency.number = i + 1;

                if (favoritesIDs.Contains(res.Key))
                    currency.isFavorite = true;

                currency.name = res.Key;
                currency.price = res.Value;

                names.Add(currency.name);

                assets.Add(currency);

                i++;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
