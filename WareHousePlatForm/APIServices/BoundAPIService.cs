using WareHousePlatForm.Data;
using WareHousePlatForm.Models;

namespace WareHousePlatForm.APIServices
{
    public class BoundAPIService
    {
        public List<BoundData> getBound()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.GetAsync("api/BoundDatas/getBound").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<List<BoundData>>().Result;
                    return result;
                }
            }
            return null;
        }

        public List<BoundList> getboundByName(string name)
        {
            using( var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.GetAsync($"api/BoundDatas/getboundByName/{name}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<List<BoundList>>().Result;
                    return result;
                }
            }
            return null;
        }

        public bool bound(BoundData bound)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.PostAsJsonAsync("api/BoundDatas/boud", bound).Result;

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Rebound(BoundData bound)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.PostAsJsonAsync("api/BoundDatas/Reboud", bound).Result;

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public BoundData editBoundByname(string user, string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.GetAsync($"api/BoundDatas/getrebound/{user}/{name}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<BoundData>().Result;
                    return result;
                }
            }
                return null;
        }
    }
}
