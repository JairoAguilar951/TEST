using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Sample_Mflix.Models;
namespace API_Sample_Mflix.Controllers
{
    public class UsersController : ApiController
    {
        mongo_db db_mongo = new mongo_db();
        List<User> Allusers;

        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult login(User user)
        {
            Allusers = db_mongo.ApiUsers(db_mongo.mongoDatabase).ToList();
            if (Allusers.Any(x => x.username == user.username && x.password == user.password))
            {
                user = Allusers.Where(x => x.username == user.username).FirstOrDefault();
                return Ok(new { Success = true, value = user, Message = "Login success.", Message_data = "", Message_Classes = "alert-success", Message_concat = false });
            }
            else
            {
                return Ok(new { Success = false, Message = "Incorrect E-mail or password", Message_data = "", Message_Classes = "alert-danger", Message_concat = false });
            }
        }

        [HttpPost]
        [Route("api/newUser")]
        public IHttpActionResult PostUser(User NewUser)
        {
            Allusers = db_mongo.ApiUsers(db_mongo.mongoDatabase).ToList();

            if (Allusers.Any(x => x.username == NewUser.username))
            {
                return Ok(new { Success = false, Message = "This E-mail already exists", Message_data = "", Message_Classes = "alert-danger", Message_concat = false });
            }

            db_mongo.Api_User_Save(NewUser);

            return Ok(new { success = true, message = "Se inserto el usuario correctamente, Id: " + NewUser.Id });

        }


        [HttpPut]
        [Route("api/updateUser")]
        public IHttpActionResult UpdateUser(User upatedUser)
        {
            db_mongo.Api_User_Update(upatedUser);

            return Ok(new { success = true, message = "Se actualizo el usuario correctamente, Id: " + upatedUser.Id });

        }

        [HttpGet]
        [Route("api/allUsers")]
        public IHttpActionResult getUsers()
        {
            Allusers = db_mongo.ApiUsers(db_mongo.mongoDatabase).ToList();
            return Ok(Allusers);

        }

        [HttpGet]
        [Route("api/userById/{id}")]
        public IHttpActionResult getUserById(string id)
        {
            Allusers = db_mongo.ApiUsers(db_mongo.mongoDatabase).ToList();
            List<User> filter = Allusers.Where(x => x.Id == id).ToList();
            return Ok(filter);

        }

        [HttpDelete]
        [Route("api/deleteUser/{id}")]
        public IHttpActionResult deleteUser(string id)
        {
            db_mongo.Api_User_Delete(id);
            return Ok($"Usuario eliminado");

        }
    }
}
