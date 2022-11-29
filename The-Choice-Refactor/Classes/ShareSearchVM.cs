using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using The_Choice_Refactor.Interfaces;

namespace The_Choice_Refactor.Classes
{
    public class ShareSearchVM: INotifyPropertyChanged, IShareVM
    {
        private string? searchRequest;
        private bool? inFavorites;
        public ObservableCollection<ShareModel> assets { get; set; }    // found shares collection
        private ShareModel? selected;                                   // selected share
        public ShareModel? Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
        public ShareSearchVM(string searchRequest, bool? inFavorites)
        {
            assets = new ObservableCollection<ShareModel>();
            this.searchRequest = searchRequest;
            this.inFavorites = inFavorites;
        }
        public async Task<bool> Load()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://latest-stock-price.p.rapidapi.com/price?Indices=NIFTY%2050"),
                Headers =
                {
                    { "X-RapidAPI-Key", "2c575eb23bmshe6c992138d16f61p159201jsn8d0f3485fdce" },
                    { "X-RapidAPI-Host", "latest-stock-price.p.rapidapi.com" }
                }
            };

            ShareModel[] result = null;

            try
            {
                result = await ShareGet.Load(request);                                                         // get info from api
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            string[] favoritesIDs = File.ReadAllText(@"UserData\Favorites\FavoriteShares.txt").Split(";\r\n"); // load favorites list

            int i = 0;

            foreach (var share in result)
            {
                i++;
                if (!share.identifier.ToLower().Contains(searchRequest.ToLower()) 
                    && !share.symbol.ToLower().Contains(searchRequest.ToLower())) 
                    continue;
                share.number = i;
                share.isFavorite = favoritesIDs.Contains(share.symbol);
                if (inFavorites == true && !share.isFavorite) continue;
                assets.Add(share);
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
