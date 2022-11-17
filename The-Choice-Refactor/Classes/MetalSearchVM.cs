using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.IO;

namespace The_Choice_Refactor.Classes
{
    public class MetalSearchVM: INotifyPropertyChanged
    {
        public ObservableCollection<MetalModel> metals { get; set; }    // found metals collection
        private MetalModel? selected;                                  // selected metal
        public MetalModel? Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }
        public MetalSearchVM(string searchRequest, bool? inFavorites)
        {
            metals = new ObservableCollection<MetalModel>();
            Load(searchRequest, inFavorites);
        }
        public async void Load(string searchRequest, bool? inFavorites)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            result = await MetalGet.LoadSpot();                                                                            // get info from api

            string[] favoritesIDs = File.ReadAllText(@"..\..\..\UserData\Favorites\FavoriteMetals.txt").Split(";\r\n");   // load favorites list

            int i = 0;

            foreach (var res in result)
            {
                if (!res.Key.ToLower().Contains(searchRequest.ToLower())) continue;
                MetalModel metal = new MetalModel();
                metal.number = i + 1;
                metal.name = res.Key;
                metal.price = res.Value;
                metal.isFavorite = favoritesIDs.Contains(res.Key);
                i++;
                if (inFavorites == true && !metal.isFavorite) continue;
                metals.Add(metal);
            }

            result = await MetalGet.LoadCommodities();

            foreach (var res in result)
            {
                if (!res.Key.ToLower().Contains(searchRequest.ToLower())) continue;
                MetalModel metal = new MetalModel();
                metal.number = i + 1;
                metal.name = res.Key;
                metal.price = res.Value;
                metal.isFavorite = favoritesIDs.Contains(res.Key);
                i++;
                if (inFavorites == true && !metal.isFavorite) continue;
                metals.Add(metal);
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
