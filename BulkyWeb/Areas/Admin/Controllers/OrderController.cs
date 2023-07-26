using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			return View();
		}

        public IActionResult Details(int orderId)
        {
			var model = new OrderVM()
			{
				OrderHeader = _unitOfWork.OrderHeader.Get(a => a.Id == orderId, includeProperties: "ApplicationUser"),
				OrderDetail = _unitOfWork.OrderDetail.GetAll(a => a.OrderHeaderId == orderId, includeProperties: "Product")
			};

            return View(model);
        }


        #region API Calls

        [HttpGet]
		public IActionResult GetAll(string status)
		{
			var obj = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");

			switch (status)
			{
				case "pending":
					obj = obj.Where(a => a.PaymentStatus == SD.PaymentStatusDelayedPayment);
					break;
                case "inprocess":
                    obj = obj.Where(a => a.PaymentStatus == SD.StatusProcess);
                    break;
                case "completed":
                    obj = obj.Where(a => a.PaymentStatus == SD.StatusShipped);
                    break;
                case "approved":
                    obj = obj.Where(a => a.PaymentStatus == SD.StatusApproved);
                    break;
                default:
					break;
			}


			return Json(new { data = obj });
		}
		

		#endregion
	}
}
