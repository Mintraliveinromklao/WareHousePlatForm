using NuGet.Versioning;
using WareHousePlatForm.APIServices;
using WareHousePlatForm.Data;
using WareHousePlatForm.Models;

namespace WareHousePlatForm.Services
{
    public class ServiceHistory
    {
        ProductAPIService apiProduct = new ProductAPIService();
        BoundAPIService apiBound = new BoundAPIService();
        ImportItemAPIService apiImport = new ImportItemAPIService();
        ExportAPIService apiExpoert = new ExportAPIService();
        public List<HistoryProduct> historyProducts()
        {
            var ProductHistory = new List<HistoryProduct>();
            var product = apiProduct.getProdut();
            var bound = apiBound.getBound();
            var import = apiImport.historyImport();
            var export = apiExpoert.exportHistory();
            foreach (var item in product)
            {
                var productHistory = new HistoryProduct();
                var boundHistory = bound.Where(x => x.Name == item.Name).ToList();
                var importHistory = import.Where(x => x.Name == item.Name).ToList();
                var exportHistory = export.Where(x => x.Name == item.Name).ToList();
                productHistory.Name = item.Name;
                productHistory.Code = item.Code;
                productHistory.Category = item.Category;
                productHistory.Bound = boundHistory;
                productHistory.importItems = importHistory;
                productHistory.exports = exportHistory;
                ProductHistory.Add(productHistory);
            }
            return ProductHistory;
        }
    }
}
