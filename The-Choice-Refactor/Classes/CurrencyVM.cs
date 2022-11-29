using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.IO;
using System.Threading.Tasks;
using The_Choice_Refactor.Interfaces;

namespace The_Choice_Refactor.Classes
{
    public class CurrencyVM : INotifyPropertyChanged, ICurrencyVM
    {
        public ObservableCollection<CurrencyModel> assets { get; set; }   // all currencies collection
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
        }
        public async Task<bool> Load()
        {
            Dictionary<string, double> result = new Dictionary<string, double>();

            try
            {
                result = await CurrencyGet.Load();                                                                                  // get info from api
            }
            catch(Exception ex)
            {
                result = null;
                throw new Exception(ex.Message, ex);
            }

            string[] favoritesIDs = File.ReadAllText(@"UserData\Favorites\FavoriteCurrencies.txt").Split(";\r\n");     // load favorites list

            int i = 0;
            foreach (var res in result)
            {
                i++;
                CurrencyModel currency = new CurrencyModel();
                currency.number = i;

                if (favoritesIDs.Contains(res.Key))
                    currency.isFavorite = true;

                currency.name = res.Key;
                currency.price = res.Value;
                currency.price *= The_Choice_Refactor.Properties.Settings.Default.CurrView;

                assets.Add(currency);
            }
            return true;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
