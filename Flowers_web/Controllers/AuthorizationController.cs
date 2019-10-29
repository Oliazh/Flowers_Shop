using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Flowers_web.Models;

namespace Flowers_web.Controllers
{
    public class AuthorizationController : Controller
    {
        private flowersEntities db = new flowersEntities();

        // GET: Authorization
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Group);
            return View(users.ToList());
        }

        // GET: Authorization/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Authorization/Create
        public ActionResult Create()
        {
            ViewBag.User_of_Group = new SelectList(db.Groups, "ID", "Name_Group");
            return View();
        }

        // POST: Authorization/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_ID,User_Name,User_Pass,User_Status,User_of_Group")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_of_Group = new SelectList(db.Groups, "ID", "Name_Group", user.User_of_Group);
            return View(user);
        }

        // GET: Authorization/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_of_Group = new SelectList(db.Groups, "ID", "Name_Group", user.User_of_Group);
            return View(user);
        }

        // POST: Authorization/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_ID,User_Name,User_Pass,User_Status,User_of_Group")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_of_Group = new SelectList(db.Groups, "ID", "Name_Group", user.User_of_Group);
            return View(user);
        }

        // GET: Authorization/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Authorization/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Pass))
            {
                return View();
            }
            var user = db.Users.FirstOrDefault(d => d.User_Name.Equals(User) && d.User_Pass.Equals(Pass));
            if (user == null)
            {
                return View();
            }

            if (user.Group.Name_Group.Equals(Role.Manager.ToString()))
            {
                string userName = user.User_Name;
               ViewBag.UserGroup = user.Group.Name_Group.ToString();
                Response.SetCookie(CreateCookie(userName));
                return this.RedirectToAction("Index", "Delivery");
            }
           else if (user.Group.Name_Group.Equals(Role.CallCenter.ToString()))
            {
                string userName = user.User_Name;
                ViewBag.UserGroup = user.Group.Name_Group.ToString(); 
                Response.SetCookie(CreateCookie(userName));
                return this.RedirectToAction("Index", "CallCenter");
            }
            return View();
        }
        private HttpCookie CreateCookie(string value)
        {
            HttpCookie CookiesFlowers = new HttpCookie("FlowersCookie")
            {
                Value = value,
                Expires = DateTime.Now.AddDays(3)
            };
            return CookiesFlowers;
        }
    }

    enum Role
    {
        Manager = 1,
        CallCenter = 2,
        User = 3,
        Admin = 4
    }
}
