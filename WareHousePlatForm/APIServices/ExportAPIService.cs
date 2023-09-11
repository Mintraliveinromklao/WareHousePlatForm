using WareHousePlatForm.Data;
using WareHousePlatForm.Models;

namespace WareHousePlatForm.APIServices
{
    public class ExportAPIService
    {
        public List<ExportHistory> exportHistory()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.GetAsync("api/ExportHistories/getExport").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<List<ExportHistory>>().Result;
                    return result;
                }
                return null;
            }    
        }   
    }
}
