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
            var e_mail = User.Identity.GetUserName();
            var carPartsNames = db.Parts.Where(s => s.Email == e_mail).Select(s => s.Name).ToList();
            var repairNames = db.Repairs.Where(s => s.Email == e_mail).Select(s => s.Name).ToList();

            var carPartsCosts = db.Parts.Where(s => s.Email == e_mail).Select(s => s.Cost_Buy).ToList();
            var repairCosts = db.Repairs.Where(s => s.Email == e_mail).Select(s => s.Price).ToList();

            var sumParts = db.Parts.Where(s => s.Email == e_mail).Sum(s => s.Cost_Buy);
            var sumRepairs = db.Repairs.Where(s => s.Email == e_mail).Sum(s => s.Price);
            var total = sumParts + sumRepairs;
            ViewBag.Parts = carPartsCosts;  
            ViewBag.Repairs = repairCosts;
            ViewBag.PartsNames = carPartsNames;
            ViewBag.RepairsNames = repairNames;
            ViewBag.totalCosts = total;


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