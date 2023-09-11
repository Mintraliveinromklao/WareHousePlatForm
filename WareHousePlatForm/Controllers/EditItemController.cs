using Microsoft.AspNetCore.Mvc;
using wareHouse.Services;
using WareHousePlatForm.Data;
using WareHousePlatForm.Models;
using Microsoft.Data.SqlClient;
using WareHousePlatForm.APIServices;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;
using WareHousePlatForm.Services;

namespace WareHousePlatForm.Controllers
{
    [SessionFilter]
    public class EditItemController : Controller
    {
        ProductAPIService productAPI = new ProductAPIService();
        ImportItemAPIService apiAddItem = new ImportItemAPIService();
        ServiceEdit servicecal = new ServiceEdit();
        public IActionResult AddItem()
        { 
            ViewData["TagComment"] = "-";
            var data = TempData["alert"] as string;
            if (!string.IsNullOrEmpty(data))
            {
                ViewData["TagComment"] = data ;
            }

            return View();
        }

        public IActionResult AddItemToStock(ImportItem dataItem , IFormFile files)
        {
            dataItem.DateTime = DateTime.Now;
            dataItem.UserName = HttpContext?.Session.Get<UserAccount>("UserLogin").Name;

            var check = apiAddItem.AddItem(dataItem);
            var path = createPath(files);
            if (check == false)
            {
                TempData["alert"] = "บันทึกไม่สำเร็จ";
            }
            if (check == true)
            {
                var editproduct = servicecal.saveProduct(dataItem);
                TempData["alert"] = (editproduct == true)?"บันทึกสำเร็จ":"แก้ไขข้อมูลอุปกรณ์ไม่สำเร็จ";
            }
            return RedirectToAction("AddItem");
        }

        public string createPath(IFormFile file) //uploadfile
        {
            var src = "";
            var path = (file == null)? null:Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "IMGTool", file.FileName);
            if (path != null)
            {
                using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                src = $"~/imgtool/{file.FileName}";
            }
             
            return src;
        }

        public IActionResult removeItem()
        {
            var product = productAPI.productInRemovepage();
            ViewData["product"] = product;
            ViewData["check"] = TempData["remove"];
            return View();
        }

        public IActionResult RemoveHistory(ExportHistory history)
        {
            var delete = servicecal.removeProduct(history);
            TempData["remove"] = (delete == true)?"บันทึกการนำออกสำเร็จ":"บันทึกไม่สำเร็จ";
            return RedirectToAction("removeItem");
        }
    }
}
