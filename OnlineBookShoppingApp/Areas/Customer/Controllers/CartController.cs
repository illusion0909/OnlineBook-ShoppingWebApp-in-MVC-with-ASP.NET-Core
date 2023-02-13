using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.WebUtilities;
using OnlineBookShoppingApp.Data;
using OnlineBookShoppingApp.DataAccess.Repository.IRepository;
using OnlineBookShoppingApp.Models;
using OnlineBookShoppingApp.Models.ViewModels;
using OnlineBookShoppingApp.Utility;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineBookShoppingApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
      //  private static bool isEmailConfirm = false;
      //  private readonly IEmailSender _emailSender;
       private readonly UserManager<IdentityUser> _userManager;
        public CartController(IUnitOfWork unitOfWork, /*IEmailSender emailSender,*/ UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
          //  _emailSender = emailSender;
            _userManager = userManager;
        }
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        [Authorize]
        //[Authorize]
        public IActionResult Index()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var Claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (Claim == null)////ager loin nhi kiya hua
            {
                ShoppingCartVM = new ShoppingCartVM()
                {
                    ListCart = new List<ShoppingCart>()
                };
                return View(ShoppingCartVM);
            }
            //*****
            ShoppingCartVM = new ShoppingCartVM()
            {
                OrderHeader = new OrderHeader(),
                ListCart = _unitOfWork.ShoppingCart.GetAll(sp => sp.ApplicationUserId == Claim.Value, includeProperties: "Product")


            };
            ShoppingCartVM.OrderHeader.OrderTotal = 0;//default
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.FirstOrDefault(u => u.Id == Claim.Value, includeProperties: "Company");//jis user ne login kisa uski detail application user me aa gyi
            //jis user ne loin kia uski kitni items h cart me
            foreach (var list in ShoppingCartVM.ListCart)
            {
                list.Price = SD.GetPriceBasedQuantity(list.Count, list.Product.Price, list.Product.price50, list.Product.price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (list.Count * list.Price);//quantity or price
                if (list.Product.Description.Length > 5000)//product ki description ke liye
                {
                    list.Product.Description = list.Product.Description.Substring(0, 4999);

                }
            }
            ////****
            //if (!isEmailConfirm)
            //{
            //    ViewBag.EmailMessage = "Email has sent kindly verify your email !";
            //    ViewBag.EmailCSS = "text-success";
            //    isEmailConfirm = false;

            //}
            //else
            //{
            //    ViewBag.EmailMessage = "Email must be confirm for authorize customer!";
            //    ViewBag.EmailCSS = "text-danger";
            //}

            return View(ShoppingCartVM);
        }
        //[HttpPost]
        //[ActionName("Index")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> IndexPost()
        //{
        //    var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //    var user = _unitOfWork.ApplicationUser.FirstOrDefault(u => u.Id == claims.Value);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Email is Empty");
        //    }
        //    else
        //    {
        //        //Email register code
        //        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //        var callbackUrl = Url.Page(
        //            "/Account/ConfirmEmail",
        //            pageHandler: null,
        //            values: new { area = "Identity", userId = user.Id, code = code },
        //            protocol: Request.Scheme);

        //        await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
        //            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        //        isEmailConfirm = true;

        //    }
        //    return RedirectToAction(nameof(Index));
        //}
        public IActionResult plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.FirstOrDefault(sc => sc.Id == cartId);
            cart.Count += 1;
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.FirstOrDefault(sc => sc.Id == cartId);
            //cart.Count -= 1;
            if (cart.Count == 1)
            {
                cart.Count = 1;
            }
            else
            {
                cart.Count -= 1;
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult delete(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.FirstOrDefault(sc => sc.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            //session
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_CartSessionCount, count);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Summary()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                OrderHeader = new OrderHeader(),
                ListCart = _unitOfWork.ShoppingCart.GetAll(sc => sc.ApplicationUserId == claim.Value, includeProperties: "Product")

            };
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value, includeProperties: "Company");
            foreach (var list in ShoppingCartVM.ListCart)
            {
                list.Price = SD.GetPriceBasedQuantity(list.Count, list.Product.Price, list.Product.price50, list.Product.price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (list.Price * list.Count);
                list.Product.Description = SD.ConvertToRawHtml(list.Product.Description);
            }
            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.state = ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;
            return View(ShoppingCartVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPost(string stripeToken)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM.OrderHeader.ApplicationUser =
                _unitOfWork.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value, includeProperties: "Company");
            ShoppingCartVM.ListCart =
                _unitOfWork.ShoppingCart.GetAll(sc => sc.ApplicationUserId == claim.Value, includeProperties: "Product");//.value important

            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.OrderStatusPending;
            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;
            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();
            foreach (var list in ShoppingCartVM.ListCart)
            {
                list.Price = SD.GetPriceBasedQuantity(list.Count, list.Product.Price, list.Product.price50, list.Product.price100);
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = list.ProductId,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    Price = list.Price,
                    Count = list.Count
                };
                ShoppingCartVM.OrderHeader.OrderTotal += (list.Price * list.Count);
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }
            _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartVM.ListCart);
            _unitOfWork.Save();

            HttpContext.Session.SetInt32(SD.Ss_CartSessionCount, 0);

            #region Stripe Payment
            if (stripeToken == null)
            {
                ShoppingCartVM.OrderHeader.PaymentDueDate = DateTime.Now.AddDays(30);
                ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayPayment;
                ShoppingCartVM.OrderHeader.OrderStatus = SD.OrderStatusApproved;

            }
            else
            {
                //Payment Process
                var options = new ChargeCreateOptions()
                {
                    Amount = Convert.ToInt32(ShoppingCartVM.OrderHeader.OrderTotal),
                    Currency = "USD",
                    Description = "orderId:" + ShoppingCartVM.OrderHeader.Id,
                    Source = stripeToken
                };
                //payment
                var service = new ChargeService();
                Charge charge = service.Create(options);
                if (charge.BalanceTransactionId == null)
                    ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
                else
                    ShoppingCartVM.OrderHeader.TransactionId = charge.BalanceTransactionId;
                if (charge.Status.ToLower() == "succeeded")
                {
                    ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;
                    ShoppingCartVM.OrderHeader.PaymentDate = DateTime.Now;
                    ShoppingCartVM.OrderHeader.OrderStatus = SD.OrderStatusApproved;
                    ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
                }

            }
            _unitOfWork.Save();
            #endregion
            return RedirectToAction("OrderConfirmation", "Cart", new { id = ShoppingCartVM.OrderHeader.Id });

        }
        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
    }
}
