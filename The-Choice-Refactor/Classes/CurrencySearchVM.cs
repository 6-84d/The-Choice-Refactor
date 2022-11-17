using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.IO;

namespace The_Choice_Refactor.Classes
{
    public class CurrencySearchVM : INotifyPropertyChanged
    {
        public ObservableCollection<CurrencyModel> currencies { get; set; }   // found currencies collection
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
        public CurrencySearchVM(string searchRequest, bool? inFavorites)
        {
            currencies = new ObservableCollection<CurrencyModel>();
            Load(searchRequest, inFavorites);
        }
        public async void Load(string searchRequest, bool? inFavorites)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            result = await CurrencyGet.Load();                                                                                  // get info from api

            string[] favoritesIDs = File.ReadAllText(@"..\..\..\UserData\Favorites\FavoriteCurrencies.txt").Split(";\r\n");     // load favorites list

            int i = 0;
            foreach (var res in result)
            {
                i++;

                if (!res.Key.ToLower().Contains(searchRequest.ToLower())) continue;

                CurrencyModel currency = new CurrencyModel();
                currency.number = i;

                if (favoritesIDs.Contains(res.Key))
                    currency.isFavorite = true;
                else
                {
                    if (inFavorites == true)
                        continue;
                }
                currency.name = res.Key;
                currency.price = res.Value;

                currencies.Add(currency);
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
