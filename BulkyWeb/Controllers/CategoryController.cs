using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {            
            var objCategoryList = _db.Categories.ToList();

            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exaclty match Name.");
            }

            if (ModelState.IsValid) 
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
            
        }
    }
}
