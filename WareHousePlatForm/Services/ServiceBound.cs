using System.Xml.Linq;
using WareHousePlatForm.APIServices;
using WareHousePlatForm.Data;

namespace WareHousePlatForm.Services
{
    public class ServiceBound
    {
        ProductAPIService apiProduct = new ProductAPIService();
        BoundAPIService apiBound = new BoundAPIService();
        public List<List<string>> tableBorrow()
        {
            var table = new List<List<string>>();
            var borrow = apiProduct.productBoundPage();
            foreach (var item in borrow)
            {
                var data = new List<string>();
                data.Add(item.Name);
                data.Add(item.Code);
                data.Add(item.Category);
                data.Add(item.UnitOfUsable.ToString());
                table.Add(data);
            }
            return table;
        }

        public List<List<string>> tableReturn(string name)
        {
            var table = new List<List<string>>();
            var Return = apiBound.getboundByName(name);
            foreach (var item in Return)
            {
                var data = new List<string>();
                data.Add(item.Name);
                data.Add(item.Unit.ToString());
                data.Add(item.DateBorrow.ToString());
                table.Add(data);
            }
            return table;
        }

        public bool borrow(string user,string name ,int unit)
        {
            var result = false;
            var product = apiProduct.productforEdit(name);
            if (product != null)
            {
                var bound = new BoundData();
                bound.UserName = user;
                bound.Name = name;
                bound.Category = product.Category;
                bound.Unit = unit;
                bound.DateBorrow = DateTime.Now;
                bound.Status = "borrow";
                product.Unit = product.Unit - unit;
                product.UnitOfUsable = product.Unit - unit;
                var productresult = apiProduct.EditProduct(product);
                var borrowresult = apiBound.bound(bound);
                result = (productresult == false)? false:(borrowresult == false)? false: true;
            }
            return result;
        }

        public bool returnItem(string user, string name, int Unit)
        {
            var result = false;
            var bound = apiBound.editBoundByname(user,name);
            var product = apiProduct.productforEdit(name);
            if (bound != null && product != null)
            {
                bound.DateReturn = DateTime.Now;
                bound.Status = "return";
                product.UnitOfUsable = product.UnitOfUsable + Unit;
                product.Unit = product.UnitOfUsable + Unit;
                var editbound = apiBound.Rebound(bound);
                var editproduct = apiProduct.EditProduct(product);
                result = (editbound == false)? false:(editproduct == false)? false:true;
            }
            return result;
        }
    }
}
