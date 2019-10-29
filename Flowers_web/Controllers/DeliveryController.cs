using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flowers_web.Models;


namespace Flowers_web.Controllers
{
    public class DeliveryController : Controller
    {
        Delivery model;
       // flowersEntities db = new flowersEntities();
        public DeliveryController()
        {
            model =  Delivery.Instance();
            string a1 = "195 Rue De l`Atmosphère Gatineau J9A 0A3";
            string a11 = "195 Rue de l'Atmosphère, Gatineau, QC J9A 0A3, Canada";
            string a2 = "69 Templeton St Ottawa K1N 0A6";
                string a22 = "69 Templeton St, Ottawa, ON K1N 7S8, Canada";
            string a3 = "149 Keltie Pvt Ottawa K2J 0A3";
            string a33 = "149 Keltie Private, Nepean, ON K2J 0A3, Canada";
            string a4 = "qwertyuiop";
            string a44 = "asdfghjkl";
            
            //  int b1=LevenshteinDistance.Compute(a1,a11 );
            //int c1 = LevenshteinDistance.Compute(a2, a22);
            //  int d1=LevenshteinDistance.Compute(a3, a33);
            //  int y1=LevenshteinDistance.Compute(a4, a44);
            //double b1 = CalculateSimilarity(a1, a11);
            //double c1 = CalculateSimilarity(a2, a22);
            //double d1 = CalculateSimilarity(a3, a33);
            //double y1 = CalculateSimilarity(a4, a44);

        }
       
        // GET: Delivery
        public ActionResult Index()
        {
           return View(model);
        }
        [HttpPost]
        public ActionResult GetOrders(string searching)
        {          
            
            if (!String.IsNullOrEmpty(searching))
            {
                model.GetOrderList(searching);
                return View("Index",model);               
            }
            return View();
        }
        [HttpPost]
        public ActionResult SetDrivers()
        {
            model.SetSimpleDrivers(); 
            return View("Index",model);
        }

    }
}