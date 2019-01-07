using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassicGarage.DAL;
using ClassicGarage.Models;
using Microsoft.AspNet.Identity;

namespace ClassicGarage.Controllers
{
    [Authorize]
    public class OwnersController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Owners
        public ActionResult Index()
        {
            return View(db.Owner.ToList());
        }

        // GET: Owners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Create", "Owners");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            if (User.Identity.IsAuthenticated && id==0)
            {
                return RedirectToAction("Create", "Owners");
            }
            OwnerModel ownerModel = db.Owner.Find(id);
            if (ownerModel == null)
            {
                return HttpNotFound();
            }
            return View(ownerModel);
        }

        // GET: Owners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Phone,Email")] OwnerModel ownerModel)
        {
            var e_mail = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                ownerModel.Email = e_mail;
                db.Owner.Add(ownerModel);
                db.SaveChanges();
                var query = db.Owner.Where(s => s.Email == e_mail).Select(s => s.ID).First();
                ViewBag.OwnerId = query;
                Session["UserID"] = query;
                return RedirectToAction("Panel", "Home");
            }

            
            //var cars = db.Car.Where(s => s.OwnerID == query);
            return View(ownerModel);
        }

        // GET: Owners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerModel ownerModel = db.Owner.Find(id);
            if (ownerModel == null)
            {
                return HttpNotFound();
            }
            return View(ownerModel);
        }

        // POST: Owners/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Phone,Email")] OwnerModel ownerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ownerModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ownerModel);
        }

        // GET: Owners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerModel ownerModel = db.Owner.Find(id);
            if (ownerModel == null)
            {
                return HttpNotFound();
            }
            return View(ownerModel);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OwnerModel ownerModel = db.Owner.Find(id);
            db.Owner.Remove(ownerModel);
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
