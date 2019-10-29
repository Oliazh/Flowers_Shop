using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Flowers_web.Models;

namespace Flowers_web.Controllers
{
  
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }

    }
    public class CallCenterController : Controller
    {
        public CallCenter model;
        flowersEntities db;
        public CallCenterController()
        {
            model = new CallCenter();
            db = new flowersEntities();
        }

        // GET: CallCenter
        

      
            public ActionResult Index()
        {
            var data = db.Order_Del.ToList();
            ViewBag.userdetails = data;
            ViewBag.userdetails2 =new List<Order_Del>();

            var cookie = Request.Cookies["FlowersCookie"];
            if (cookie != null)
            {
                ViewBag.id = cookie.Value;
              
                model.GetBouqets();
                
                return View(model);
            }
            else
            {
                return View("Error");
            }
        }
        public JsonResult Index2(string CustIDx)
        {
            //var data = db.Order_Del.ToList();
             int id = Int32.Parse(CustIDx);
            // var data2 = db.Order_Del.Where(x => x.Cust_ID == id).ToList();
            //ViewBag.userdetails2 = data2;
            // return Json(new { Success = true, Genre = ViewBag.userdetails2 }, JsonRequestBehavior.AllowGet);

            var list2 = (from N in db.Order_Del.ToList()
                         where N.Cust_ID==id
                         select new
                         {
                            Name= N.Cust2.Fname + " " +N.Cust2.Lname,
                             N.Cust_ID,N.Order_ID, N.Order_Status
                           
                         });
            return Json(list2, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetOrder(int x)
        {
            IEnumerable<Order_Del> model = db.Order_Del.Where(c=>c.Cust_ID==x).ToList();
            return Json(model, JsonRequestBehavior.AllowGet); 
        }
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

        [HttpPost]
        public ActionResult Calculate(CallCenter model)
        {
            if (ModelState.IsValid)
            {
                model.SaveOrder(model);
                model.GetBouqets();
            }

            return View("Index",model);
        }
       
        [HttpPost]
        public JsonResult SearchUser(int search)
        {
            return Json(model.GetUsers(search.ToString()), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FindUsers(string Prefix)//work usersearch
        {
            //Console.WriteLine(Prefix);
            //var list = model.UsersFio(Prefix);
            var list2 = (from N in db.Cust2.ToList()
                         where N.Phone.StartsWith(Prefix)
                         select new  { N.Phone,N.Cust_ID,N.Lname,N.Fname,N.Email,
                             N.Gender,N.Address_ID,N.Address.Address_Name_Street,
                             N.Address.Address_Number_Street,N.Address.Address_Numder_Appartment,
                             N.Address.Address_State,N.Address.Address_ZIPCODE,N.Address.Address_City,
                             N.Address.Address_Country
                           });
            return Json(list2, JsonRequestBehavior.AllowGet);
        }


            public ActionResult Autocomplete(string term)
        {
            var items = new[] { "Apple", "Pear", "Banana", "Pineapple", "Peach" };

            var filteredItems = items.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                );
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
    }
}