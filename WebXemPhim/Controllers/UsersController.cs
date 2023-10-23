using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebXemPhim.Models;

namespace WebXemPhim.Controllers
{
    public class UsersController : Controller
    {
        private readonly WebXemPhimContext _context;
        public UsersController(WebXemPhimContext context)
        {
            _context = context;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.UserName == model.UserName);

                if (user != null && user.UserPassword == model.UserPassword)
                {
                    HttpContext.Session.SetString("UserName", user.UserName);
                    if(user.UserRole == true)
                    {
                        return RedirectToAction("Index", "AdminMovies");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Movie");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
                }
            }

            return View(model);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.SingleOrDefault(u => u.UserName == model.UserName);

                if (existingUser == null)
                {
                    _context.Users.Add(model);
                    _context.SaveChanges();

                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Tên đăng nhập đã tồn tại.");
                }
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
