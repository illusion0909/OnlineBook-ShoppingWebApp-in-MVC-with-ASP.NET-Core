using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShoppingApp.Data;
using OnlineBookShoppingApp.Models;
using OnlineBookShoppingApp.Utility;
using System.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OnlineBookShoppingApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _context.ApplicationUsers.Include(c => c.Company).ToList();//aspnetUser
            var roles = _context.Roles.ToList();//aspnetRoles
            var userRole = _context.UserRoles.ToList();//aspnetUserrole
            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;
                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };

                }
            }
            //if (!User.IsInRole(SD.Role_Admin))
            //{
            var adminUser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
            userList.Remove(adminUser);
            // }
            return Json(new { data = userList });
        }
    
      [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            bool isLocked = false;
            var userInDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (userInDb == null)
                return Json(new { success = false, message = "Something went wrong" });
            if (userInDb != null && userInDb.LockoutEnd > DateTime.Now)
            {
                userInDb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                userInDb.LockoutEnd = DateTime.Now.AddYears(100);
                isLocked = true;
            }
            _context.SaveChanges();
            return Json(new { success = true, message = isLocked == true ? "User Sucessfully Locked" : "User Successfully UnLocked" });
        }
        #endregion


    }
}
