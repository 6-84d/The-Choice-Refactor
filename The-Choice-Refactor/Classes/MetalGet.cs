using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace The_Choice_Refactor.Classes
{
    public class MetalGet
    {
        public static async Task<Dictionary<string, double>> LoadSpot()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://api.metals.live/v1/spot"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Dictionary<string, double> metals = new Dictionary<string, double>();
                    string content = await response.Content.ReadAsStringAsync();
                    content = content.Replace("{", "");
                    content = content.Replace("}", "");
                    content = content.Replace("[", "");
                    content = content.Replace("]", "");
                    content = content.Replace("\"", "");
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
        public static async Task<Dictionary<string, double>> LoadCommodities()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://api.metals.live/v1/spot/commodities"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Dictionary<string, double> metals = new Dictionary<string, double>();
                    string content = await response.Content.ReadAsStringAsync();
                    content = content.Replace("{", "");
                    content = content.Replace("}", "");
                    content = content.Replace("[", "");
                    content = content.Replace("]", "");
                    content = content.Replace("\"", "");
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
