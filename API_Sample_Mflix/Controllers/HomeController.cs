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

        public ActionResult MovieCatalog(string title, bool edit = false)
        {
            
            List<Movies> movies = db_mongo.Movies(db_mongo.mongoDatabase).ToList();
            List<Comments> comments = db_mongo.Comments(db_mongo.mongoDatabase, movies).ToList();

            var tuple = Tuple.Create(movies, comments);

            if(edit == false)
            {
            return View(tuple);
            }

            return View("MovieEdit", movies);

        }

        public ActionResult UpdateMovie(Movies updatedMovie, string langs)
        {
            string[] langsArr = langs.Split(',');
            updatedMovie.languages = langsArr;
            db_mongo.movieUpdate(updatedMovie);
            return RedirectToAction("Movies");
       
        }
    }
}
