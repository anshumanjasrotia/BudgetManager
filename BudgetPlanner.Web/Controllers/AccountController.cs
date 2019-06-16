using BudgetPlanner.Web.Helper;
using BudgetPlanner.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetPlanner.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: Account
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = APIHelper.Login(model);
                if (result != null)
                {
                    CreateAuthenticationCookie(result);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        private HttpCookie CreateAuthenticationCookie(AuthenticationResultModel data)
        {
            HttpCookie StudentCookies = new HttpCookie("StudentCookies");
            StudentCookies.Value = "hallo";
            StudentCookies.Expires = DateTime.Now.AddHours(1);
            return StudentCookies;
        }

        // GET: Account
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // GET: Account
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                APIHelper.Register(model);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}