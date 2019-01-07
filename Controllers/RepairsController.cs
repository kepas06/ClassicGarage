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
    public class RepairsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Repairs
        public ActionResult Index()
        {
            var repairs = db.Repairs.Include(r => r.Car);
            return View(repairs.ToList());
        }

        // GET: Repairs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModel repairModel = db.Repairs.Find(id);
            if (repairModel == null)
            {
                return HttpNotFound();
            }
            return View(repairModel);
        }

        // GET: Repairs/Create
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(db.Car, "ID", "Brand");
            return View();
        }

        // POST: Repairs/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarID,Name,Description,Price,Email")] RepairModel repairModel)
        {
            var e_mail = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                repairModel.Email = e_mail;
                db.Repairs.Add(repairModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.Car, "ID", "Brand", repairModel.CarID);
            return View(repairModel);
        }

        // GET: Repairs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModel repairModel = db.Repairs.Find(id);
            if (repairModel == null)
            {
                return HttpNotFound();
            }
            var cars = db.Owner.Where(u => u.Email == User.Identity.Name);
            int i = 0;
            foreach (OwnerModel s in cars)
            {
                i = s.ID;
            }
            ViewBag.CarID = new SelectList(db.Car.Where(u => u.OwnerID == i), "ID", "Brand");
            return View(repairModel);
        }

        // POST: Repairs/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarID,Name,Description,Price")] RepairModel repairModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repairModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarID = new SelectList(db.Car, "ID", "Brand", repairModel.CarID);
            return View(repairModel);
        }

        // GET: Repairs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairModel repairModel = db.Repairs.Find(id);
            if (repairModel == null)
            {
                return HttpNotFound();
            }
            return View(repairModel);
        }

        // POST: Repairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepairModel repairModel = db.Repairs.Find(id);
            db.Repairs.Remove(repairModel);
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
