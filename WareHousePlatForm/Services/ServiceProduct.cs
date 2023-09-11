using WareHousePlatForm.APIServices;

namespace WareHousePlatForm.Services
{
    public class ServiceProduct
    {
        ProductAPIService apiProduct = new ProductAPIService();
        public List<List<string>> tableProduct()
        {
            var table = new List<List<string>>();
            var product = apiProduct.getProdut();
            foreach (var item in product)
            {
                var data = new List<string>();
                data.Add(item.Name);
                data.Add(item.Code);
                data.Add(item.Category);
                data.Add(item.Unit.ToString());
                data.Add(item.UnitOfUsable.ToString());
                data.Add(item.broken.ToString());
                data.Add(item.Stock.ToString());
                table.Add(data);
            }
            return table;
        }

        public bool editBroken(string name , int unit)
        {
            var result = false;
            var product = apiProduct.productforEdit(name);
            if (product != null)
            {
                product.broken = product.broken + unit;
                product.UnitOfUsable = product.UnitOfUsable - unit;
                result = apiProduct.EditProduct(product);
            }
            return result;
        }
    }
}
