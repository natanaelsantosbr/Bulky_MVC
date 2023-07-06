using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Bulky.Models.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product Product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created sucessfully";
                return RedirectToAction(nameof(Index));
            }

            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var Product = _unitOfWork.Product.Get(c => c.Id == id);

            if (Product == null)
                return NotFound();

            return View(Product);
        }

        [HttpPost]
        public IActionResult Edit(Product Product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(Product);
                _unitOfWork.Save();
                TempData["success"] = "Product updated sucessfully";
                return RedirectToAction(nameof(Index));
            }

            return View();

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
