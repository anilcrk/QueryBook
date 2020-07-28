using QueryBook.Business.Abstract;
using QueryBook.Entites.Concrete.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QueryBook.WebUI.Controllers
{
    public class InstutionController : Controller
    {
        private IInstutionService _instutionService;
        public InstutionController(IInstutionService instutionService)
        {
            _instutionService = instutionService;
        }
        [HttpPost]
        public ActionResult AddInstution(Institution data)
         {
            try
            {
                data.UserId = 3;
                var result=_instutionService.Add(data);
                TempData["Message"]=result.sError+"|success";
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message + "|error";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}