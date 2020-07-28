using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using DevFramework.Core.Entities;
using QueryBook.Business.Abstract;
using QueryBook.Business.Concrete;
using QueryBook.Business.DependencyResolved.Ninject;
using QueryBook.Entites.Concrete.Querys;
using QueryBook.Entites.Concrete.Users;
using QueryBook.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace QueryBook.WebUI.Controllers
{
    public class BaseController : Controller
    {
        
      
        public BaseUser _BaseUser { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // ... log stuff before execution     
            if (System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated == false)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    { "controller", "Account" },
                    { "action", "Login" } });
                return;
            }
            try
            {
                var authCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie == null)
                {
                    return;
                }

                var encTicket = authCookie.Value;
                if (string.IsNullOrEmpty(encTicket))
                {
                    return;
                }

                var ticket = FormsAuthentication.Decrypt(encTicket);

                var securityUtilies = new SecurityUtilities();
                var identity = securityUtilies.FormsAuthTiceketToIdentiyy(ticket);//önceden yazılıumış olan sınıf : utilitye çeviriyor
                var principal = new GenericPrincipal(identity, identity.Roles);
               
               if(!string.IsNullOrEmpty(identity.Name)&&!string.IsNullOrEmpty(identity.Email))
                {
                    IUserService userService = IstanceFactory.GetIstance<IUserService>();
                    var result = userService.Get(new Entites.Concrete.Users.User { InsCode = "HACYAZILIM", Email = identity.Email });
                    if(result!=null)
                    {
                        _BaseUser = new BaseUser
                        {
                            _FirstName = result.Name,
                            _LastName = result.LastName,
                            _InsCode = result.InsCode,
                            _UserName = identity.Name,
                            _UserId = result.Id
                        };

                        ViewBag.UserFirstName = _BaseUser._FirstName;
                        ViewBag.UserLastName = _BaseUser._LastName;
                        ViewBag.InsCode = _BaseUser._InsCode;
                    }
                    
                }
            }
            catch (Exception)
            {
            }

        }
    }
}