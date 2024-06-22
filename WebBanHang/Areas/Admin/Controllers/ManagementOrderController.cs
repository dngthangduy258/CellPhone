using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;
using WebBanHang.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ManagementOrderController : Controller
    {

        private readonly ApplicationDbContext _db;
        public ManagementOrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SearchPhoneAPI(string Phone)
        {
            var orders = _db.Orders.Where(x => x.Phone == Phone).ToList();
            if (orders != null && orders.Count > 0)
            {
                return Json(new { msg = "Product added to cart", orders = orders });
            }
            return Json(new { msg = "error" });
        }
    }
}
