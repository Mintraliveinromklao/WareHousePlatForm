using Microsoft.AspNetCore.Mvc;
using wareHouse.Services;
using WareHousePlatForm.APIServices;
using WareHousePlatForm.Services;

namespace WareHousePlatForm.Controllers
{
    [SessionFilter]
    public class ProductController : Controller
    {
        ServiceHistory history = new ServiceHistory();
        ServiceProduct servicecal = new ServiceProduct();
        
        public IActionResult CheckStock()
        {
            ViewData["editStock"] = TempData["editStock"];
            return View();
        }

        public IActionResult edit(string name, int unit)
        {
            var edit = false;
            if (name != "กรุณาเลือกอุปกรณ์ก่อน")
            {
                edit = servicecal.editBroken(name, unit);
            }
            TempData["editStock"] = (name == "กรุณาเลือกอุปกรณ์ก่อน")?"กรุณาเลือกอุปกรณ์ในตาราง":(edit == true)? "บันทึกการแจ้งชำรุดสำเร็จ":"บันทึกไม่สำเร็จ";
            return RedirectToAction("CheckStock");
        }

        public JsonResult tableStock()
        {
            var data = servicecal.tableProduct();
            return Json(data);
        }

        public IActionResult History()
        {
            var item = history.historyProducts();
            ViewData["history"] = item;
             return View();
        }
    }
}
