using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API_Sample_Mflix.Models;

namespace API_Sample_Mflix.Controllers
{
    public class HomeController : Controller
    {
        mongo_db db_mongo = new mongo_db();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Movies()
        {
          
            List<Movies> movies = db_mongo.Movies(db_mongo.mongoDatabase).ToList();

            return View(movies);
        }

        public ActionResult Movie(string title, bool edit = false)
        {
            
            List<Movies> movies = db_mongo.Movies(db_mongo.mongoDatabase).ToList();
            List<Movies> filter = movies.Where(x => x.title == title).ToList();

            if(edit == false)
            {
            return View(filter);
            }

            return View("MovieEdit", filter);

        }
    }
}
