using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Sample_Mflix.Models;

namespace API_Sample_Mflix.Controllers
{
    public class ValuesController : ApiController
    {
        const string usuario = "userTest";
        const string contrasena = "1234";
        const string JAAL = "JAAL";
        const string EYPR = "EYPR";
        const string CEGE = "CEGE";
        const string cluster = "qualitycontrol.s7wyt0n.mongodb.net/?retryWrites=true&w=majority&appName=QualityControl";

        //Ingresa tus credenciales y haz una prueba de ping
        //Puedes ingresar las credenciales desde variables y datos escritos
        //mongo_db test = new mongo_db(usuario, contrasena, base_de_datos, cluster, true);

        //Ingresa tus credenciales y visualiza si los datos están correctamente escritos
        //Puedes ingresar las credenciales directamente
        //mongo_db credentials = new mongo_db("", "", "", "", false);

        //Ingresa tus credenciales y genera un objeto con la conexión a base de datos
        //Puedes ingresar las credenciales completamente con variables
        //mongo_db db_mongo = new mongo_db(usuario, contrasena, base_de_datos, cluster);
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("api/eypr")]
        public IHttpActionResult PostProductEYPR(API record)
        {
            mongo_db db_mongo = new mongo_db(usuario, contrasena, EYPR, cluster);

            db_mongo.TimeSeries_Save(record);

            return Ok(new { success = true, message = "Se inserto el producto correctamente" + record });

        }

        [HttpPost]
        [Route("api/cege")]
        public IHttpActionResult PostProductCEGE(API record)
        {
            mongo_db db_mongo = new mongo_db(usuario, contrasena, CEGE, cluster);

            db_mongo.TimeSeries_Save(record);

            return Ok(new { success = true, message = "Se inserto el producto correctamente" + record });

        }

        [HttpPost]
        [Route("api/jaal")]
        public IHttpActionResult PostProductJAAL(API record)
        {
            mongo_db db_mongo = new mongo_db(usuario, contrasena, JAAL, cluster);

            db_mongo.TimeSeries_Save(record);

            return Ok(new { success = true, message = "Se inserto el producto correctamente" + record });

        }

        [HttpGet]
        [Route("api/ping")]
        public IHttpActionResult ping()
        {
            mongo_db db_mongo = new mongo_db();
            return Ok(db_mongo.ClientPing(db_mongo.MongoClient, "sample_mflix"));
        }


        [HttpGet]
        [Route("api/theaters")]
        public IHttpActionResult getTheaters()
        {
            mongo_db db_mongo = new mongo_db();
            List<Theater> theaters = db_mongo.Theaters(db_mongo.mongoDatabase).ToList();
            return Ok(theaters);
        }



        [HttpGet]
        [Route("api/movies/rating")]
        public IHttpActionResult getMovies()
        {
            mongo_db db_mongo = new mongo_db();
            List<Movies> movies = db_mongo.Movies(db_mongo.mongoDatabase).ToList();
            List<Movies> filtered = movies.Where(x => x.imdb.rating > 8).ToList();
            return Ok(filtered);
        }

        [HttpGet]
        [Route("api/movies/awards")]
        public IHttpActionResult getMoviesAwards()
        {
            mongo_db db_mongo = new mongo_db();
            List<Movies> movies = db_mongo.Movies(db_mongo.mongoDatabase).ToList();
            List<Movies> filtered = movies.Where(x => x.awards.wins >= 1).ToList();
            return Ok(filtered);
        }




        [HttpGet]
        [Route("api/products")]
        public IHttpActionResult getProducts()
        {
            mongo_db db_mongo = new mongo_db();
            List<Product> products = db_mongo.products(db_mongo.mongoDatabase).ToList();
            return Ok(products);
        }

        [HttpGet]
        [Route("api/currentproduct")]
        public IHttpActionResult getCurrentProduct()
        {
            mongo_db db_mongo = new mongo_db();
            List<CurrentProduct> product = db_mongo.CurrentProduct(db_mongo.mongoDatabase).ToList();
            return Ok(product);
        }

        [HttpPost]
        [Route("api/products")]
        public IHttpActionResult PostProduct(CurrentProduct product)
        {
            mongo_db db_mongo = new mongo_db();

            db_mongo.Product_Save(product);

            return Ok(new { success = true, message = "Se inserto el producto correctamente" + product });

        }


        [HttpDelete]
        [Route("api/products/{id}")]
        public IHttpActionResult DeleteProduct(string id)
        {
            mongo_db db_mongo = new mongo_db();

            db_mongo.productDelete(id);

            return Ok(new { success = true, message = "Se elimino el comentario correctamente" });

        }
        //[HttpGet]
        //[Route("api/comments")]
        //public IHttpActionResult getComments()
        //{
        //    string[] words = { "test", "Jaqen H'ghar", "Jaqen" }; //Array de palabras a encontrar
        //    mongo_db db_mongo = new mongo_db();
        //    List<Comments> Comments = db_mongo.Comments(db_mongo.mongoDatabase).ToList();
        //    /* list<users> filtered = users.where(x => x.email == "testemil").tolist();*/ //donde coincida una cadena
        //    //List<Users> filtered = users.Where(x => x.user_id > 1 && x.user_id < 500).ToList(); Filtrar por rango
        //    //List<Users> filtered = users.Where(x => words.Contains(x.name)).ToList(); Evaluar en un campo si contiene una o mas palabras del array
        //    //List<Users> filtered = users.Where(x => x.name.Split(' ').Contains(words[2])).ToList(); Separar las palabras de un campo y evaluar si alguna coincide con una palabra en especifico del array
        //    List<Comments> filtered = Comments.Where(x => x.date.Year > 2000 ).OrderBy(x=> x.date.Year).ToList(); //Verificar terminacion de palabras, ej, correo
        //    int count = Comments.Count(); //contar registros
        //    return Ok(filtered);
        //}

        [HttpPut]
        [Route("api/comments")]
        public IHttpActionResult UpdatetUser(Comments comments)
        {
            mongo_db db_mongo = new mongo_db();

            db_mongo.Comment_Update(comments);

            return Ok(new { success = true, message = "Se actualizo el comentario correctamente" });

        }


        [HttpGet]
        [Route("api/users")]
        public IHttpActionResult getUsers()
        {
            mongo_db db_mongo = new mongo_db();
            List<Users> users = db_mongo.Users(db_mongo.mongoDatabase).ToList();
            return Ok(users);
        }

        [HttpGet]
        [Route("api/user/{id}")]
        public IHttpActionResult getUserById(string Id)
        {

            mongo_db db_mongo = new mongo_db();
            List<Users> users = db_mongo.Users(db_mongo.mongoDatabase).ToList();
            List<Users> filter = users.Where(x => x.Id == Id).ToList();
            return Ok(filter);
        }


        [HttpPost]
        [Route("api/users")]
        public IHttpActionResult PostUser(Users user)
        {
            mongo_db db_mongo = new mongo_db();
            List<Users>Allusers = db_mongo.Users(db_mongo.mongoDatabase).ToList();

            if (Allusers.Any(x => x.email == user.email))
            {
                return Ok(new { Success = false, Message = "This E-mail already exists", Message_data = "", Message_Classes = "alert-danger", Message_concat = false });
            }
            db_mongo.User_Save(user);

            return Ok(new { success = true, message = $"Se inserto el usuario correctamente, Id: {user.Id}" });
      
        }

        [HttpPut]
        [Route("api/users")]
        public IHttpActionResult UpdatetUser(Users users)
        {
            mongo_db db_mongo = new mongo_db();

            db_mongo.User_Update(users);

            return Ok(new { success = true, message = "Se actualizo el usuario correctamente" + users });

        }

        [HttpDelete]
        [Route("api/comments/{id}")]
        public IHttpActionResult DeleteComment(string id)
        {
            mongo_db db_mongo = new mongo_db();

            db_mongo.Comment_Delete(id);

            return Ok(new { success = true, message = "Se elimino el comentario correctamente" });

        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public IHttpActionResult DeleteUser(string id)
        {
            mongo_db db_mongo = new mongo_db();

            db_mongo.User_Delete(id);

            return Ok(new { success = true, message = "Se elimino el usuario correctamente"});

        }
        [HttpDelete]
        [Route("api/users/deleteall/{name}")]  //Borrar muchos con base a nombre
        public IHttpActionResult DeleteManyUsers(string name)
        {
            mongo_db db_mongo = new mongo_db();
            List<Users> AllUsers = db_mongo.Users(db_mongo.mongoDatabase);
            List<Users> filtered = AllUsers.Where(x => x.name.Split(' ').Contains(name)).ToList(); //Borrar solo los que tengan ese nombre (para no borrar todos)
            List<String> userIdsToDelete = filtered.Select(u => u.Id).ToList();

            db_mongo.Users_DeleteAll(userIdsToDelete);

            return Ok(new { success = true, message = "Se eliminaron los usuarios correctamente" }); 

        }


        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

}
