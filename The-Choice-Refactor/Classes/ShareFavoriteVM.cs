using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.IO;
using The_Choice_Refactor.Interfaces;
using System.Threading.Tasks;

namespace The_Choice_Refactor.Classes
{
    internal class ShareFavoriteVM: INotifyPropertyChanged, IShareVM
    {
        public ObservableCollection<ShareModel> assets { get; set; }    // favorite shares collection
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
        public ShareFavoriteVM()
        {
            assets = new ObservableCollection<ShareModel>();
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            string[] favoritesIDs = File.ReadAllText(@"UserData\Favorites\FavoriteShares.txt").Split(";\r\n"); // load favorites list

            int i = 0;

            foreach (var share in result)
            {
                i++;
                share.number = i;
                if (!favoritesIDs.Contains(share.symbol)) continue;
                share.isFavorite = favoritesIDs.Contains(share.symbol);
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
