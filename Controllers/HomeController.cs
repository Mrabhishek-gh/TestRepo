using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationTest.Filters;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class HomeController : Controller
    {
        [LoginFilter]
        public ActionResult Index()
        {
            
            return View();
        }

        [LoginFilter]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [LoginFilter]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Privacy()
        {
            List<int> list = new List<int>();
            try
            {
                int t = 5;
                int x = t / 0;
                
                //list.Add(x);

            }
            catch (Exception )
            {
                return RedirectToAction("Privacy2");
            }
            return View();
        }

        public ActionResult Privacy2() {
            return View("Error");
        }


    }
}