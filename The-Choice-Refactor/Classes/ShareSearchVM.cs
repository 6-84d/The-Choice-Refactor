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

namespace The_Choice_Refactor.Classes
{
    public class ShareSearchVM: INotifyPropertyChanged
    {
        public ObservableCollection<ShareModel> shares { get; set; }    // found shares collection
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
            shares = new ObservableCollection<ShareModel>();
            Load(searchRequest, inFavorites);
        }
        public async void Load(string searchRequest, bool? inFavorites)
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
            ShareModel[] result = await ShareGet.Load(request);                                                         // get info from api

            string[] favoritesIDs = File.ReadAllText(@"..\..\..\UserData\Favorites\FavoriteShares.txt").Split(";\r\n"); // load favorites list

            int i = 0;

            foreach (var share in result)
            {
                if (!share.identifier.ToLower().Contains(searchRequest.ToLower()) 
                    && !share.symbol.ToLower().Contains(searchRequest.ToLower())) 
                    continue;
                share.number = i + 1;
                share.isFavorite = favoritesIDs.Contains(share.symbol);
                i++;
                if (inFavorites == true && !share.isFavorite) continue;
                shares.Add(share);
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
