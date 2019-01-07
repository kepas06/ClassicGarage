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
    public class CarPartsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: CarParts
        public ActionResult Index()
        {
            return View(db.Parts.ToList());
        }

        // GET: CarParts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarParts carParts = db.Parts.Find(id);
            if (carParts == null)
            {
                return HttpNotFound();
            }
            return View(carParts);
        }

        // GET: CarParts/Create
        public ActionResult Create(int? id)
        {
            //    ViewBag.num = id;

            //var owner = db.Owner.Where(u => u.Email == User.Identity.Name);
            //int z = 0;
            //foreach (OwnerModel s in owner)
            //{
            //    z = s.ID;
            //}
            //ViewBag.CarID = new SelectList(db.Car.Where(u => u.OwnerID == z), "ID", "Brand");
            ViewBag.CarID = new SelectList(db.Car, "ID", "Brand");
            ViewBag.RepairID = new SelectList(db.Repairs, "ID", "Name");


            return View();
        }

        // POST: CarParts/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarID,RepairID,Name,Cost_Buy,Cost_Sell,Buy_Date,Sell_Date,Email")] CarParts carParts)
        {
            var e_mail = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                carParts.Email = e_mail;
                db.Parts.Add(carParts);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(carParts);
        }

        // GET: CarParts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarParts carParts = db.Parts.Find(id);
            if (carParts == null)
            {
                return HttpNotFound();
            }
            return View(carParts);
        }

        // POST: CarParts/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Cost_Buy,Cost_Sell,Buy_Date,Sell_Date")] CarParts carParts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carParts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carParts);
        }

        // GET: CarParts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarParts carParts = db.Parts.Find(id);
            if (carParts == null)
            {
                return HttpNotFound();
            }
            return View(carParts);
        }

        // POST: CarParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarParts carParts = db.Parts.Find(id);
            db.Parts.Remove(carParts);
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
