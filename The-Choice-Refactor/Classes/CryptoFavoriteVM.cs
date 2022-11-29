using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.IO;
using System.Threading.Tasks;

namespace The_Choice_Refactor.Classes
{
    public class CryptoFavoriteVM : INotifyPropertyChanged, ICryptoVM
    {
        public ObservableCollection<CryptoModel> assets { get; set; } // favorite cryptoes collection
        private CryptoModel? selected;                                   // selected crypto
        public CryptoModel? Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
        public CryptoFavoriteVM()
        {
            assets = new ObservableCollection<CryptoModel>();
        }
        public async Task<bool> Load()
        {
            List<CryptoModel> result = new List<CryptoModel>();
            try
            {
                result = await CryptoGet.Load();                                                                            // get info from api
            }
            catch (Exception ex)
            {
                result = null;
                throw new Exception(ex.Message, ex);
            }                                                                                // get info from api

            string[] favoritesIDs = File.ReadAllText(@"UserData\Favorites\FavoriteCryptoes.txt").Split(";\r\n");   // load favorites list

            for (int i = 0; i < result.Count; i++)
            {
                result[i].number = i + 1;

                if (favoritesIDs.Contains(result[i].asset_id))
                    result[i].isFavorite = true;
                else continue;

                if (result[i].name == "")
                    result[i].name = result[i].asset_id;

                if (result[i].change_1h > 0)
                    result[i].color_change_1h = "green";
                else
                    result[i].color_change_1h = "red";

                if (result[i].change_24h > 0)
                    result[i].color_change_24h = "green";
                else
                    result[i].color_change_24h = "red";

                if (result[i].change_7d > 0)
                    result[i].color_change_7d = "green";
                else
                    result[i].color_change_7d = "red";

                assets.Add(result[i]);
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
