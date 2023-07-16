 using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Bulky.Models.Repository.IRepository;
using Bulky.Utility;
using BulkyWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
                return View(new Company());
            else
            {
                var model = this._unitOfWork.Company.Get(a => a.Id == id);
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company company, IFormFile? file)
        {
            if (ModelState.IsValid)
            {               

                if (company.Id == 0)
                    _unitOfWork.Company.Add(company);
                else
                    _unitOfWork.Company.Update(company);

                _unitOfWork.Save();
                TempData["success"] = "Company created sucessfully";
                return RedirectToAction(nameof(Index));
            }
            else
                return View(company);
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var objCompanyList = _unitOfWork.Company.GetAll();

            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToDeleted = _unitOfWork.Company.Get(a => a.Id == id);

            if (CompanyToDeleted == null)
                return Json(new { success = false, message = "Error while deleting" });
                        
            _unitOfWork.Company.Remove(CompanyToDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Company deleted sucessfully " });
        }

        #endregion
    }
}
