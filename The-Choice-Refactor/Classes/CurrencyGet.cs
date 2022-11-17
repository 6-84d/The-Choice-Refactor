using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace The_Choice_Refactor.Classes
{
    public class CurrencyGet
    {
        class CurrencyResult
        {
            public Dictionary<string, double> rates { get; set; }
        }
        public static async Task<Dictionary<string, double>> Load()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://openexchangerates.org/api/latest.json?app_id=b43edbb38ba14eea9766294e65657b5a"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    CurrencyResult result = JsonConvert.DeserializeObject<CurrencyResult>(content);
                    return result.rates;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
