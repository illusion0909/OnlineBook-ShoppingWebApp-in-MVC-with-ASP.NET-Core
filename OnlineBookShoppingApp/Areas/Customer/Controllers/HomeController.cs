using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineBookShoppingApp.DataAccess.Repository;
using OnlineBookShoppingApp.DataAccess.Repository.IRepository;
using OnlineBookShoppingApp.Models;
using OnlineBookShoppingApp.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineBookShoppingApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string SearchString, string searchValue)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_CartSessionCount, count);
            }


            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");

            



            if (_unitOfWork.Product == null)
            {
                return Problem("Entity Set 'OnlineShoppingSite.Product' is null.");

            }
            if (string.IsNullOrEmpty(searchValue))
            {
                TempData["InfoMessage"] = "Please provide search value.";
                return View(productList);
            }
            // var query = from m in productlist select m;
            else
            {
                if (SearchString.ToLower() == "title")
                {
                    var searchbyname = productList.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()));
                    return View(searchbyname);

                }

                else if (SearchString.ToLower() == "isbn")
                {
                    var searchbyIsbn = productList.Where(p => p.ISBN.ToLower().Contains(searchValue.ToLower()));
                    return View(searchbyIsbn);
                }


            }

            return View(productList);
        }
        public IActionResult Details(int id)
        {
            var productInDb = _unitOfWork.Product.FirstOrDefault(p => p.Id == id, includeProperties: "Category,CoverType");
            if (productInDb == null) return NotFound();
            var ShoppingCart = new ShoppingCart()
            {
                Product = productInDb,
                ProductId = productInDb.Id
            };
            //session
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_CartSessionCount, count);
            }
            return View(ShoppingCart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            shoppingCart.Id = 0;
            if (ModelState.IsValid)
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                shoppingCart.ApplicationUserId = claim.Value;

                var shoppingCartFromDb = _unitOfWork.ShoppingCart.FirstOrDefault
                    (u => u.ApplicationUserId == claim.Value &&
                    u.ProductId == shoppingCart.ProductId);
                if (shoppingCartFromDb == null)
                {
                    //add
                    _unitOfWork.ShoppingCart.Add(shoppingCart);
                }
                else
                {
                    //update
                    shoppingCartFromDb.Count += shoppingCart.Count;
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var productInDb = _unitOfWork.Product.FirstOrDefault(p => p.Id == shoppingCart.ProductId, includeProperties: "Category,CoverType");
                if (productInDb == null) return NotFound();
                var shoppingCartEdit = new ShoppingCart()
                {
                    Product = productInDb,
                    ProductId = productInDb.Id,
                };

                return View(shoppingCartEdit);
            }
        }


        public IActionResult Privacy()          
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
