using Microsoft.AspNetCore.Mvc;
using wareHouse.Services;
using WareHousePlatForm.APIServices;
using WareHousePlatForm.Models;
using WareHousePlatForm.Services;

namespace WareHousePlatForm.Controllers
{
    [SessionFilter]
    public class BoundController : Controller
    {
        ServiceBound servicecal = new ServiceBound();
        public IActionResult Borrow()
        { 
            ViewData["tool"] = (TempData["tool"] == null)? "-" : TempData["tool"];
            ViewData["code"] = (TempData["code"] == null) ? "-" : TempData["code"];
            ViewData["unit"] = (TempData["unit"] == null) ? 0: TempData["unit"];
            ViewData["checkborrow"] = TempData["checkborrow"];
            return View();
        }
        
        public IActionResult borrowItem(string name , int unit)
        {
            var result = false;
            if (name != "-")
            {
                var Username = HttpContext.Session.Get<UserAccount>("UserLogin").Name;
                result = servicecal.borrow(Username,name, unit);
            }
            TempData["checkborrow"] = (result==true)? "บันทึกการยืมสำเร็จ" : "บันทึกการยืมไม่สำเร็จ";
            return RedirectToAction("Borrow");
        }

        public JsonResult TableBorrow()
        {
            var table = servicecal.tableBorrow();
            return Json(table);
        }

        public IActionResult Return()
        {
            ViewData["checkReturn"] = TempData["checkReturn"];
            return View();
        }

        public IActionResult returnItem(string name,int Unit)
        {
            var user = HttpContext.Session.Get<UserAccount>("UserLogin").Name;
            var check = false;
            if (name != null && Unit != null)
            { 
                check = servicecal.returnItem(user,name, Unit);
            }
            TempData["checkReturn"] = (check == true)?"บันทึกการคืนสำเร็จ":"บันทึกการคืนไม่สำเร็จ";
            return RedirectToAction("Return");
        }

        public JsonResult getTableReturn( )
        {
            var name = HttpContext.Session.Get<UserAccount>("UserLogin").Name;
            var bound = servicecal.tableReturn(name);
            return Json(bound);
        }

    }
}
