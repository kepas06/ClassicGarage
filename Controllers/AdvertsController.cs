using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassicGarage.DAL;
using ClassicGarage.Models;
using Microsoft.AspNet.Identity;

namespace ClassicGarage.Controllers
{
    public class AdvertsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Adverts
        public ActionResult Index()
        {
            var notice = db.Notice.Include(a => a.Car);
            var e_mail = User.Identity.GetUserName();
            
            ViewBag.User = e_mail;
            return View(notice.ToList());
        }

        // GET: Adverts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adverts adverts = db.Notice.Find(id);
            if (adverts == null)
            {
                return HttpNotFound();
            }
            return View(adverts);
        }

        // GET: Adverts/Create
        public ActionResult Create()
        {
            var owner = db.Owner.Where(u => u.Email == User.Identity.Name);
            int z = 0;
            foreach (OwnerModel s in owner)
            {
                z = s.ID;
            }
            ViewBag.CarID = new SelectList(db.Car.Where(u => u.OwnerID == z), "ID", "Brand");
            return View();
        }

        // POST: Adverts/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarID,Description,Active,Price,Photo,Email,phone")] Adverts adverts)
        {
            string main = Server.MapPath("~/Content/Photo/AdvertsPhoto");
            
            if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = Request.Files["Photo"];
                if (postedFile.ContentLength > 0)
                {
                    
                    var fileName = Path.GetFileName(postedFile.FileName);
                    var path = Path.Combine(main, fileName);
                    adverts.Photo = fileName;
                    postedFile.SaveAs(path);

                }
                db.Notice.Add(adverts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            //var email = User.Identity.GetUserName();

            //ViewBag.RepairID = new SelectList(db.Repairs.Where(u => u.Email == email), "ID", "Name");

            //ViewBag.CarID = new SelectList(db.Car, "ID", "Brand", adverts.CarID);
            return View(adverts);

            }

        // GET: Adverts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adverts adverts = db.Notice.Find(id);
            if (adverts == null)
            {
                return HttpNotFound();
            }

            var owner = db.Owner.Where(u => u.Email == User.Identity.Name);
            int z = 0;
            foreach (OwnerModel s in owner)
            {
                z = s.ID;
            }
            ViewBag.CarID = new SelectList(db.Car.Where(u => u.OwnerID == z), "ID", "Brand");

            return View(adverts);
        }

        // POST: Adverts/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarID,Description,Active,Price,Photo,Email,phone")] Adverts adverts)
        {
            string main = Server.MapPath("~/Content/Photo/AdvertsPhoto");

            if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = Request.Files["Photo"];
                if (postedFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(postedFile.FileName);
                    var path = Path.Combine(main, fileName);
                    adverts.Photo = fileName;
                    postedFile.SaveAs(path);

                }
      
                db.Entry(adverts).State = EntityState.Modified;
                 
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.Car, "ID", "Brand", adverts.CarID);
            return View(adverts);
        }

        // GET: Adverts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adverts adverts = db.Notice.Find(id);
            if (adverts == null)
            {
                return HttpNotFound();
            }
            return View(adverts);
        }

        // POST: Adverts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adverts adverts = db.Notice.Find(id);
            db.Notice.Remove(adverts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
