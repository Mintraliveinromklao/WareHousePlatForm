using WareHousePlatForm.Data;
using WareHousePlatForm.Models;

namespace WareHousePlatForm.APIServices
{
    public class ProductAPIService
    {
        public bool EditProduct(ProductInformation Edit)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.PostAsJsonAsync("api/ProductInformations/Edit", Edit).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }

            return true;
        }


        public bool removeProduct(string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.DeleteAsync($"api/ProductInformations/{name}").Result;

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }

            return true;
        }

        public ProductInformation productforEdit( string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.GetAsync($"api/ProductInformations/getForEdit/{name}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<ProductInformation>().Result;
                    return result;
                }
            }
            return null;
        }

        public List<ProductInformation> getProdut()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.GetAsync($"api/ProductInformations/getProdut").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<List<ProductInformation>>().Result;
                    return result;
                }
            }
            return null;
        }

        public List<ProductRemovePage> productInRemovepage()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.GetAsync($"api/ProductInformations/productInRemovepage").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<List<ProductRemovePage>>().Result;
                    return result;
                }
            }
            return null;
        }

        public List<selectProductBoundPage> productBoundPage()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSetting.Config.APIBaseAddress);
                var response = client.GetAsync("api/ProductInformations/productBoundPage").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<List<selectProductBoundPage>>().Result;
                    return result;
                }
            }
            return null;
        }
    }
}
