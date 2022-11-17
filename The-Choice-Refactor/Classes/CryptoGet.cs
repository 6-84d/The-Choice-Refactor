using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace The_Choice_Refactor.Classes
{
    public class CryptoGet
    {
        // class to store json deserializing result
        class CryptoResult
        {
            public List<CryptoModel> assets { get; set; } = new List<CryptoModel>();
        }
        public static async Task<List<CryptoModel>> Load()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("https://cryptingup.com/api/assets"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    CryptoResult result = JsonConvert.DeserializeObject<CryptoResult>(content);
                    return result.assets;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
