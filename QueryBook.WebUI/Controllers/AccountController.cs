using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using QueryBook.Business.Abstract;
using QueryBook.Entites.Concrete.Users;
using QueryBook.WebUI.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace QueryBook.WebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        public ActionResult Login()
        {
          
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            ViewBag.ErrMessage = "";
            var userResult = _userService.GetByUserNameAndOassword(model.User);
            if (userResult != null)
            {
                _userService.AutClear();
                AuthenticationHelper.CreateOutCookie(new Guid(), userResult.UserName, userResult.Email, DateTime.Now.AddDays(1), _userService.GetUserRoles(userResult).Select(u => u.RoleName).ToArray(), model.RememberMe, userResult.Name, userResult.LastName);
                ViewBag.UsrName = userResult.Name;
                ViewBag.UsrLastName = userResult.LastName;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrMessage = "Kullanıcı Adı Veya Şifre Hatalı";
            return View();
        }
    }
}