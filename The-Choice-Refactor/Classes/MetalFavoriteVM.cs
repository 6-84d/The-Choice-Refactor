using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.IO;
using The_Choice_Refactor.Interfaces;
using System.Threading.Tasks;

namespace The_Choice_Refactor.Classes
{
    public class MetalFavoriteVM: INotifyPropertyChanged, IMetalVM
    {
        public ObservableCollection<MetalModel> assets { get; set; }    // favorites metals collection
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
        public MetalFavoriteVM()
        {
            assets = new ObservableCollection<MetalModel>();
        }
        public async Task<bool> Load()
        {
            Dictionary<string, double> result = new Dictionary<string, double>();

            try
            {
                result = await MetalGet.LoadAllMetals();                                                                            // get info from api
            }
            catch (Exception ex)
            {
                result = null;
                throw new Exception(ex.Message, ex);
            }

            string[] favoritesIDs = File.ReadAllText(@"UserData\Favorites\FavoriteMetals.txt").Split(";\r\n");   // load favorites list

            int i = 0;

            foreach (var res in result)
            {
                i++;
                if (!favoritesIDs.Contains(res.Key)) continue;
                MetalModel metal = new MetalModel();
                metal.number = i;
                metal.name = res.Key;
                metal.price = res.Value;
                metal.isFavorite = favoritesIDs.Contains(res.Key);
                assets.Add(metal);
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
