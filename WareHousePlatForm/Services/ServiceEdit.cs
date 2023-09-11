using WareHousePlatForm.APIServices;
using WareHousePlatForm.Data;

namespace WareHousePlatForm.Services
{
    public class ServiceEdit
    {
        ProductAPIService productAPI = new ProductAPIService();
        public bool saveProduct(ImportItem dataItem)
        {
            var result = false;
            var Edit = productAPI.productforEdit(dataItem.Name); ;
            if (Edit != null)
            {
                Edit.Unit = Edit.Unit + dataItem.Unit;
                Edit.UnitOfUsable = Edit.UnitOfUsable + dataItem.Unit;
                Edit.Stock = Edit.Stock + dataItem.Unit;
                result = productAPI.EditProduct(Edit);
            }
            return result;
        }

        public bool removeProduct(ExportHistory history)
        {
            var result = false;
            var product = productAPI.productforEdit(history.Name);
            if (product != null)
            {
                product.Unit = product.Unit - history.Unit;
                product.UnitOfUsable = product.UnitOfUsable - history.Unit;
                product.Stock = product.Stock - history.Unit;
                result = (product.Stock == 0) ? productAPI.removeProduct(product.Name): productAPI.EditProduct(product);
            }

            return result;
        }
    }
}
