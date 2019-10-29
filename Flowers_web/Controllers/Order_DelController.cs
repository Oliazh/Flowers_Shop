using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Flowers_web.Models;

namespace Flowers_web.Controllers
{
    public class Order_DelController : Controller
    {
        private flowersEntities1 db = new flowersEntities1();

        // GET: Order_Del
        public ActionResult Index()
        {
            var order_Del = db.Order_Del.Include(o => o.Bouquet).Include(o => o.Cust2).Include(o => o.Driver).Include(o => o.Florist).Where(o=>o.Cust_ID==82);
            return View(order_Del.ToList());
        }

        // GET: Order_Del/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Del order_Del = db.Order_Del.Find(id);
            if (order_Del == null)
            {
                return HttpNotFound();
            }
            return View(order_Del);
        }

        // GET: Order_Del/Create
        public ActionResult Create()
        {
            ViewBag.Bouquet_ID = new SelectList(db.Bouquets, "Bouquet_ID", "Bouquet_Name");
            ViewBag.Cust_ID = new SelectList(db.Cust2, "Cust_ID", "Fname");
            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_DLicence");
            ViewBag.Florist_ID = new SelectList(db.Florists, "Florist_ID", "Frorist_Fname");
            return View();
        }

        // POST: Order_Del/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_ID,Bouquet_ID,Florist_ID,Cust_ID,Driver_ID,Order_Date,Bouquet_Quontity,Date_Delived,Order_Status,Total_Price,Size_Bouquets")] Order_Del order_Del)
        {
            if (ModelState.IsValid)
            {
                db.Order_Del.Add(order_Del);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Bouquet_ID = new SelectList(db.Bouquets, "Bouquet_ID", "Bouquet_Name", order_Del.Bouquet_ID);
            ViewBag.Cust_ID = new SelectList(db.Cust2, "Cust_ID", "Fname", order_Del.Cust_ID);
            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_DLicence", order_Del.Driver_ID);
            ViewBag.Florist_ID = new SelectList(db.Florists, "Florist_ID", "Frorist_Fname", order_Del.Florist_ID);
            return View(order_Del);
        }

        // GET: Order_Del/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Del order_Del = db.Order_Del.Find(id);
            if (order_Del == null)
            {
                return HttpNotFound();
            }
            ViewBag.Bouquet_ID = new SelectList(db.Bouquets, "Bouquet_ID", "Bouquet_Name", order_Del.Bouquet_ID);
            ViewBag.Cust_ID = new SelectList(db.Cust2, "Cust_ID", "Fname", order_Del.Cust_ID);
            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_DLicence", order_Del.Driver_ID);
            ViewBag.Florist_ID = new SelectList(db.Florists, "Florist_ID", "Frorist_Fname", order_Del.Florist_ID);
            return View(order_Del);
        }

        // POST: Order_Del/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_ID,Bouquet_ID,Florist_ID,Cust_ID,Driver_ID,Order_Date,Bouquet_Quontity,Date_Delived,Order_Status,Total_Price,Size_Bouquets")] Order_Del order_Del)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Del).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bouquet_ID = new SelectList(db.Bouquets, "Bouquet_ID", "Bouquet_Name", order_Del.Bouquet_ID);
            ViewBag.Cust_ID = new SelectList(db.Cust2, "Cust_ID", "Fname", order_Del.Cust_ID);
            ViewBag.Driver_ID = new SelectList(db.Drivers, "Driver_ID", "Driver_DLicence", order_Del.Driver_ID);
            ViewBag.Florist_ID = new SelectList(db.Florists, "Florist_ID", "Frorist_Fname", order_Del.Florist_ID);
            return View(order_Del);
        }

        // GET: Order_Del/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Del order_Del = db.Order_Del.Find(id);
            if (order_Del == null)
            {
                return HttpNotFound();
            }
            return View(order_Del);
        }

        // POST: Order_Del/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Del order_Del = db.Order_Del.Find(id);
            db.Order_Del.Remove(order_Del);
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
