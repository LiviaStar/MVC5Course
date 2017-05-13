using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult PartialAbout()
        {
            //ViewBag
            ViewBag.Message = "Your application description page.";

            if (Request.IsAjaxRequest())
            {
                return PartialView("About"); //沒有Layout
            }
            else
            {
                return View("About");
            }

            //F12 Console 下指令
            //$.get('/Home/PartialAbout'),function(data){alert(data)});
        }

        public ActionResult SomeAction()
        {
            return PartialView("SuccessRedirect","/");
            //不要用 return content("<script>alert('建立成功');loction.href='/';")
        }

        public ActionResult GetFile()
        {
            return File(Server.MapPath("~/Content/Snoopy.png)"), "image/png", "NewName.png");
        }

        public ActionResult GetJson()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return Json(db.Product.Take(5), JsonRequestBehavior.AllowGet);
        }

    }
}