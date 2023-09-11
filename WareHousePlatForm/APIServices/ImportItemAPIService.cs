using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Configuration;
using WareHousePlatForm.Data;
using WareHousePlatForm.Models;

namespace WareHousePlatForm.APIServices
{
    public class ImportItemAPIService
    {
        public bool AddItem(ImportItem item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.PostAsJsonAsync("api/ImportItems/Add",item).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }

            return true;
        }

        public List<ImportItem> historyImport()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.GetAsync("api/ImportItems/getImport").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<List<ImportItem>>().Result;
                    return result;
                }
            }
            return null;
        }

       
    }
}
