using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Logging;
using WebBanHang.Migrations;

namespace WebBanHang.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()

        {  //var productList = _db.Products.Include(x => x.Category).ToList();
           //    return View(productList);

            var dsSanPham = _db.Products.Include(x => x.Category).ToList();
            var result = dsSanPham
            .GroupBy(x => x.Category.Name)
            .SelectMany(g => g.Take(8))
            .ToList();

            return View(dsSanPham.ToList());
        }
        public IActionResult IndexByCategoryAndCompany(int companyId)
        {
            var products = _db.Products.Where(p => p.CompanyId == companyId).ToList();

            var company = _db.Companies.FirstOrDefault(c => c.Id == companyId);
            if (company != null)
            {
                ViewBag.CompanyName = company.Name;
            }
            else
            {
                ViewBag.CompanyName = "Unknown";
            }

            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]

        public IActionResult ProductDetail(int id)
        {
            // Cần sửa đổi dòng này nếu bạn không sử dụng Entity Framework hoặc cấu trúc database khác
            var product = _db.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpGet]
        public JsonResult GetProductDetails(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);

            var category = _db.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
            var company = _db.Companies.FirstOrDefault(c => c.Id == product.CompanyId);

            // Trả về JSON
            return Json(new
            {
                CategoryName = category.Name,
                CompanyName = company.Name
            });
        }
        [HttpGet]
        public JsonResult GetRelatedProducts(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);

            var relatedProducts = _db.Products
              .Where(p => p.CategoryId == product.CategoryId && p.CompanyId == product.CompanyId && p.Id != product.Id)
              .Take(4)
              .Select(p => new {
                  Name = p.Name,
                  CategoryId = p.CategoryId,
                  CompanyId = p.CompanyId,
                  Price = p.Price,
                  ImageUrl = p.ImageUrl
              })
              .ToList();

            // return as JSON
            return Json(relatedProducts);
        }

    }
}
