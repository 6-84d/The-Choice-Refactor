using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace The_Choice_Refactor.Classes
{
    public class CryptoSearchVM: INotifyPropertyChanged
    {
        public ObservableCollection<CryptoModel> cryptoes { get; set; }
        private CryptoModel? selected;
        public CryptoModel? Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
        public CryptoSearchVM(string searchRequest, bool? inFavorites)
        {
            cryptoes = new ObservableCollection<CryptoModel>();
            Load(searchRequest, inFavorites);
        }
        public async void Load(string searchRequest, bool? inFavorites)
        {
            List<CryptoModel> result = new List<CryptoModel>();
            result = await CryptoGet.Load();                                                                            // get info from api

            string[] favoritesIDs = File.ReadAllText(@"..\..\..\UserData\Favorites\FavoriteCryptoes.txt").Split(";\r\n");   // load favorites list

            for (int i = 0; i < result.Count; i++)
            {
                result[i].number = i + 1;

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

                if (favoritesIDs.Contains(result[i].asset_id))
                    result[i].isFavorite = true;

                if (inFavorites == true)
                {
                    if (result[i].isFavorite)
                    {
                        if (result[i].asset_id.ToLower().Contains(searchRequest.ToLower()) ||
                            result[i].name.ToLower().Contains(searchRequest.ToLower()))
                            cryptoes.Add(result[i]);
                    }
                }
                else
                {
                    if (result[i].asset_id.ToLower().Contains(searchRequest.ToLower()) ||
                            result[i].name.ToLower().Contains(searchRequest.ToLower()))
                        cryptoes.Add(result[i]);
                }
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
