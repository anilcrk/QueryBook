using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using QueryBook.Business.Abstract;
using QueryBook.Entites.Concrete.Institutions;
using QueryBook.Entites.Concrete.Querys;
using QueryBook.WebUI.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QueryBook.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        IInstutionService _instutionService;
        IQueryService _queryService;
        public HomeController(IInstutionService instutionService,IQueryService queryService)
        {
            _instutionService = instutionService;
            _queryService = queryService;
        }
        public ActionResult Index()
        {
            try
            {
                HomeViewModel model = new HomeViewModel();
                model.queries = _queryService.GetList(new Query { InsCode = _BaseUser._InsCode });
                string filePath = @"d:\testparameters\test.txt";
                var filetxt = System.IO.File.ReadAllText(filePath);
                ViewBag.fileTxt = filetxt;
                return View(model);
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message+"|error";
                return View();
            }
            
            
        }
        public ActionResult LogOut()
        {
            
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult QueryDetail(int id)
        {
            HomeViewModel model = new HomeViewModel();
            if(id>0)
            {
                model.query = _queryService.Get(x => x.Id == id);
                return PartialView("~/Views/Shared/Partials/Query/QueryPopUp.cshtml",model);
            }
            else
            {
                return PartialView("~/Views/Shared/Partials/Query/QueryPopUp.cshtml",model);
            }
        }
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult AddUpQuery(Query query)
        {
            query.InsCode = _BaseUser._InsCode;
            query.QueryTypeId = 1;
            query.ProjectId = 1;
            query.Active = 1;
            query.IsDel = 0;
            query.InsUserId = _BaseUser._UserId;
            query.InsDate = DateTime.Now;
            try
            {
                var result = _queryService.AddUp(query);
                if (result != null)
                {
                    TempData["Message"] = result.sError + "|success";
                }
                else
                {
                    TempData["Message"] = "HATA!|error";
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message+"|error";
                return RedirectToAction("Index");
            }
           
        }
    }
}