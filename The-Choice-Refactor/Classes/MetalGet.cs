using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace The_Choice_Refactor.Classes
{
    public class MetalGet
    {
        public static async Task<Dictionary<string, double>> LoadAllMetals()
        {
            Dictionary<string, double> spot = new Dictionary<string, double>();
            Dictionary<string, double> commodities = new Dictionary<string, double>();
            spot = await LoadSpot();
            commodities = await LoadCommodities();
            Dictionary<string, double> metals = new Dictionary<string, double>();

            foreach(var metal in spot)
                metals.Add(metal.Key, metal.Value);

            foreach(var metal in commodities)
                metals.Add(metal.Key, metal.Value);

            return metals;
        }
        private static async Task<Dictionary<string, double>> LoadSpot()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://api.metals.live/v1/spot"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Dictionary<string, double> metals = new Dictionary<string, double>();
                    string content = await response.Content.ReadAsStringAsync();
                    content = Regex.Replace(content, "[\\[\\]{}\\\"]", "");
                    string[] pairs = content.Split(",");
                    foreach (string pair in pairs)
                    {
                        string[] temp = pair.Split(":");
                        if (temp[0] == "timestamp") continue;
                        metals.Add(temp[0], Convert.ToDouble(temp[1].Replace(".", ",")));
                    }
                    return metals;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        private static async Task<Dictionary<string, double>> LoadCommodities()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://api.metals.live/v1/spot/commodities"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Dictionary<string, double> metals = new Dictionary<string, double>();
                    string content = await response.Content.ReadAsStringAsync();
                    content = Regex.Replace(content, "[\\[\\]{}\\\"]", "");
                    string[] pairs = content.Split(",");
                    foreach (string pair in pairs)
                    {
                        string[] temp = pair.Split(":");
                        if (temp[0] == "timestamp") continue;
                        metals.Add(temp[0], Convert.ToDouble(temp[1].Replace(".", ",")));
                    }
                    return metals;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
