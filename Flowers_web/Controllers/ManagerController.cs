using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flowers_web.Controllers
{
    public class ManagerController : Controller
    {

        // GET: Manager
        public ActionResult Index()
        {
            var cookie = Request.Cookies["FlowersCookie"];
            if (cookie != null)
            {
                ViewBag.id = cookie.Value;
                
                return View();
            }
            else
            {
                return View("Error");
            }
        }
    }
}