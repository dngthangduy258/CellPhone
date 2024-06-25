using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoreLinq;
using Newtonsoft.Json;

namespace WebBanHang.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
  
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        //Hiển thị danh sách chủng loại
        public IActionResult Index()
        {
            var listCategory = _db.Categories.ToList();
            return View(listCategory);
        }
        //Hiển thị form thêm mới chủng loại
        public IActionResult Add()
        {
            ViewBag.CompanyList = _db.Companies.DistinctBy(x => x.Name).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            return View();
        }
        // Xử lý thêm chủng loại mới
//        [HttpPost]
//        public IActionResult Add(Category category)
//        {
//            if (ModelState.IsValid) //kiem tra hop le
//            {
//                //thêm category vào table Categories
//                _db.Categories.Add(category);
//                _db.SaveChanges();
            
//TempData["success"] = "Category inserted success";
//                return RedirectToAction("Index");
//            }
//            return View();
//        }
        [HttpPost]
        public IActionResult Add(Category category, string selectedCompanyNamesJson)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();

            var lastCompanyId = _db.Companies.Max(c => c.Id);

            if (!string.IsNullOrEmpty(selectedCompanyNamesJson))
            {
                List<string> selectedCompanyNames = JsonConvert.DeserializeObject<List<string>>(selectedCompanyNamesJson);

                foreach (var companyName in selectedCompanyNames)
                {
                    var company = new Company
                    {
                        Name = companyName,
                        DisplayOrder = lastCompanyId,
                        CategoryId = category.Id
                    };
                    _db.Companies.Add(company);
                    lastCompanyId += 1;
                }

                _db.SaveChanges();
            }

            return View();
        }
        //Hiển thị form cập nhật chủng loại
        public IActionResult Update(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        // Xử lý cập nhật chủng loại
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid) //kiem tra hop le
            {
                //cập nhật category vào table Categories
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category updated success";
                return RedirectToAction("Index");
            }
            return View();
        }
        //Hiển thị form xác nhận xóa chủng loại
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        // Xử lý xóa chủng loại
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted success";
            return RedirectToAction("Index");
        }
    }
}
