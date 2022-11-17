using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace The_Choice_Refactor.Classes
{
    public class ShareGet
    {
        public static async Task<ShareModel[]> Load(HttpRequestMessage request)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(request))
            {

                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                ShareModel[] result = JsonConvert.DeserializeObject<ShareModel[]>(content);
                return result;
            }
        }
    }
}
