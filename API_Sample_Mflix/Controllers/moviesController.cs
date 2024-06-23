using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API_Sample_Mflix.Models;
namespace API_Sample_Mflix.Controllers
{
    public class moviesController : Controller
    {
        // GET: movies
        public ActionResult MoviesTest()
        {
            mongo_db db_mongo = new mongo_db();
            List<Movies> movies = db_mongo.Movies(db_mongo.mongoDatabase).ToList();

            return View(movies);
        }
    }
}