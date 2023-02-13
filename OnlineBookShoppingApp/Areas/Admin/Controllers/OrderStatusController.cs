using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShoppingApp.DataAccess.Repository.IRepository;
using OnlineBookShoppingApp.Utility;
using System.Linq;

namespace OnlineBookShoppingApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderStatusController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderStatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            var Data = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            return View(Data);
        }
        public IActionResult Details(int id)
        {
            var data = _unitOfWork.OrderHeader.Get(id);
            if (data == null) return NotFound();
            return View(data);
        }
        public IActionResult PaymentStatusPending()
        {
            var Data = _unitOfWork.OrderHeader.GetAll(u => u.OrderStatus == SD.PaymentStatusPending, includeProperties: "ApplicationUser").ToList();
            return View(Data);
        }
        public IActionResult PaymentStatusApproved()
        {
            var Data = _unitOfWork.OrderHeader.GetAll(u => u.OrderStatus == SD.PaymentStatusApproved, includeProperties: "ApplicationUser").ToList();
            return View(Data);
        }
 
        public IActionResult PaymentStatusRejected()
        {
            var Data = _unitOfWork.OrderHeader.GetAll(u => u.OrderStatus == SD.PaymentStatusRejected, includeProperties: "ApplicationUser").ToList();
            return View(Data);

        }
        public IActionResult OrderStatusApproved()
        {
            var Data = _unitOfWork.OrderHeader.GetAll(u => u.OrderStatus == SD.OrderStatusApproved, includeProperties: "ApplicationUser").ToList();
            return View(Data);

        }
        public IActionResult OrderStatusCancelled()
        {
            var Data = _unitOfWork.OrderHeader.GetAll(u => u.OrderStatus == SD.OrderStatusCancelled, includeProperties: "ApplicationUser").ToList();
            return View(Data);

        }

    }
}
