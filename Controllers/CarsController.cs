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
    [Authorize] 
    public class CarsController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Cars
        public ActionResult Index()
        {
            var e_mail = User.Identity.GetUserName();
            var query = db.Owner.Where(s => s.Email == e_mail).Select(s => s.ID).First();
            var car = db.Car.Where(s=> s.OwnerID ==query);
            return View(car.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = db.Car.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Brand,Model,Year,VIN,Series,Photo,Buy_Date,Sell_Date,Buy_Cost,Sell_Cost,OwnerID")] Cars cars)
        {

            string main = Server.MapPath("~/Content/Photo/CarsPhotos");
            var e_mail = User.Identity.GetUserName();

         

            //var TargetLocation = Path.Combine(main, src);
            if (ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = Request.Files["Photo"];
                
                if (postedFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(postedFile.FileName);
                    var path = Path.Combine(main, fileName);
                    cars.Photo = fileName;
                    postedFile.SaveAs(path);
                    
                }

                db.Car.Add(cars);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(cars);
            }

            
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = db.Car.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // POST: Cars/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Brand,Model,Year,VIN,Series,Photo,Buy_Date,Sell_Date,Buy_Cost,Sell_Cost,OwnerID")] Cars cars)
        {
            var e_mail = User.Identity.GetUserName();

            var id = db.Owner.Where(s => s.Email == e_mail).Select(s => s.ID).FirstOrDefault();
            if (ModelState.IsValid)
            {
                cars.OwnerID = id;
                db.Entry(cars).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cars);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = db.Car.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cars cars = db.Car.Find(id);
            db.Car.Remove(cars);
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
