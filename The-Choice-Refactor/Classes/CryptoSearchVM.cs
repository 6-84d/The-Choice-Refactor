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
    public class CryptoSearchVM : INotifyPropertyChanged, ICryptoVM
    {
        private string? searchRequest;
        private bool? inFavorites;
        public ObservableCollection<CryptoModel> assets { get; set; }
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
            assets = new ObservableCollection<CryptoModel>();
            this.searchRequest = searchRequest;
            this.inFavorites = inFavorites;
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
            }

            string[] favoritesIDs = File.ReadAllText(@"UserData\Favorites\FavoriteCryptoes.txt").Split(";\r\n");   // load favorites list

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
                            assets.Add(result[i]);
                    }
                }
                else
                {
                    if (result[i].asset_id.ToLower().Contains(searchRequest.ToLower()) ||
                            result[i].name.ToLower().Contains(searchRequest.ToLower()))
                        assets.Add(result[i]);
                }
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
