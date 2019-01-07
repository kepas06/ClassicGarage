using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassicGarage.Controllers
{
    public class HomeController : Controller
    {
        private ClassicGarage.DAL.GarageContext db = new DAL.GarageContext();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Panel");
            }
            else
            {
                return View();
            }
           
        }

        public ActionResult Panel()
        {
            var e_mail = User.Identity.GetUserName();
            var query = db.Owner.Where(s => s.Email == e_mail).Select(s => s.ID).FirstOrDefault();
            ViewBag.OwnerID = query;
            Session["UserID"] = query;
            return View();
        }

        public ActionResult Costs()
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
    }
}