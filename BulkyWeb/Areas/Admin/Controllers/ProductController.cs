 using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Bulky.Models.Repository.IRepository;
using BulkyWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var objProductList = _unitOfWork.Product.GetAll();

            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            var categoryList = _unitOfWork.Category.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString(),
            });

            var model = new ProductVM
            {
                CategoryList = categoryList
            };

            if (id == null || id == 0)
            {
                model = new ProductVM
                {
                    CategoryList = categoryList,
                    Product = new Product()
                };

                return View(model);
            }
            else
            {
                model.Product = this._unitOfWork.Product.Get(a=> a.Id== id);
                return View(model);
            }            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(model.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created sucessfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var categoryList = _unitOfWork.Category.GetAll().Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString(),
                });

                model = new ProductVM
                {
                    CategoryList = categoryList,
                    Product = new Product()
                };

                return View(model);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var Product = _unitOfWork.Product.Get(c => c.Id == id);

            if (Product == null)
                return NotFound();

            return View(Product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeltePost(int? id)
        {
            var Product = _unitOfWork.Product.Get(o => o.Id == id);

            if (Product == null)
                return NotFound();

            _unitOfWork.Product.Remove(Product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted sucessfully";
            return RedirectToAction(nameof(Index));

        }
    }
}
