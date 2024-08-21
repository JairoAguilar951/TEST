using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;

namespace API_Sample_Mflix.Models
{
    public class mongo_db
    {
        
        public mongo_db()
        {
            var connectionString = ConfigurationManager.AppSettings["MongoDBConnectionString"];
            var databaseName = ConfigurationManager.AppSettings["MongoDBDatabaseName"];
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            MongoClient = new MongoClient(settings);
            mongoDatabase = ClientDB(MongoClient, databaseName);
        }

        public mongo_db(string username, string password, string database_Name, string cluster)
        {
            //Constructor para conexión predeterminada usando [usuario], [contrasena] y [nombre de base de datos]
            //Propósito: Constructor para conexión predeterminada
            MongoClient = ConnectionClient(username, password, cluster);
            mongoDatabase = ClientDB(MongoClient, database_Name);
        }

        public mongo_db(string username, string password, string database_Name, string cluster, bool ping)
        {
            //Constructor para hacer ping con nuestras credenciales o visualizarlas para comprobarlas (en caso de que las envíes desde otro medio que no sea escribirlas directamente en este código, como tu propio formulario en frontend)
            //Propósito: Validar conexión
            if (ping)
            {
                //MongoClient = ConnectionClient(username, password, cluster);
                mongoDatabase = ClientDB(MongoClient, database_Name);
                ClientPing(MongoClient, database_Name);
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Usuario: {username}\nContrasena: {password}\nCluster: {cluster}\nNombre de la Base de Datos: {database_Name}");
            }
            Console.ResetColor();
        }

        public MongoClient ConnectionClient(string username, string password, string cluster)
        {
            //Conexión con el cliente/servidor
            //Propósito: Llegar al servidor
            string connectionUri = $"mongodb+srv://{username}:{password}@{cluster}";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            return new MongoClient(settings);
        }
        //Parámetros
        public MongoClient MongoClient { get; set; }

        public IMongoDatabase mongoDatabase { get; set; }

        //Métodos modulares para conexiones dinámicas
        //No muevas nada de aquí a menos que sepas qué hacen y puedas usarlas por separado, analíza y comprende
        //NO AUTORIZO
        //
        //public MongoClient ConnectionClient(string username, string password, string cluster)
        //{
        //    //Conexión con el cliente/servidor
        //    //Propósito: Llegar al servidor
        //    string connectionUri = $"mongodb+srv://{username}:{password}@{cluster}";

        //}

        public IMongoDatabase ClientDB(MongoClient client, string database_Name)
        {
            //Conexión con la base de datos
            //Propósito: Habiendo llegado al servidor, conectar con nuestra base de datos
            return client.GetDatabase(database_Name);
        }

        // Método para confirmar la conexión con la base de datos haciendo ping
        public string ClientPing(MongoClient client, string databaseName)
        {
            string text = "";
            try
            {
                var database = client.GetDatabase(databaseName);
                var command = new BsonDocument("ping", 1);
                var result = database.RunCommand<BsonDocument>(command);
                text = "El ping ha sido exitoso, estás conectado a MongoDB!";
            }
            catch (Exception ex)
            {
                text = $"Ha ocurrido un error: {ex.Message}";
            }
            return text;
        }

        //Método para probar inserción, actualización y eliminación
        public void TestCUD()
        {
            // Generar un objeto de ejemplo con la información necesaria acorde a la estructura del documento (hay otras formas también)
            Theater newTheater = new Theater
            {
                TheaterId = 2024,
                Location = new Location
                {
                    Address = new Address
                    {
                        Street1 = "1234 Main St",
                        City = "REYNOSA",
                        State = "ST",
                        Zipcode = "12345"
                    },
                    Geo = new Geo
                    {
                        Type = "Point",
                        Coordinates = new double[] { -74.0059, 40.7128 } // Coordenadas de ejemplo para Nueva York
                    }
                }
            };

            //ESTRUCTURA JSON PARA POST

            Users newUser = new Users
            {

                name = "exName",
                email = "emailexample@gmail.com",
                password = "testtesttes",


            };

            /*
            // Ejemplo de crear una lista de objetos Theater para insertar (hay otras formas también)
            List<Theater> newTheaters = new List<Theater>
            {
                new Theater
                {
                    TheaterId = 1004,
                    Location = new Location
                    {
                        Address = new Address
                        {
                            Street1 = "1234 Main St",
                            City = "Anytown",
                            State = "ST",
                            Zipcode = "12345"
                        },
                        Geo = new Geo
                        {
                            Type = "Point",
                            Coordinates = new double[] { -74.0059, 40.7128 } // Coordenadas de ejemplo para Nueva York
                        }
                    }
                },
                new Theater
                {
                    TheaterId = 1005,
                    Location = new Location
                    {
                        Address = new Address
                        {
                            Street1 = "5678 Elm St",
                            City = "Othertown",
                            State = "OT",
                            Zipcode = "54321"
                        },
                        Geo = new Geo
                        {
                            Type = "Point",
                            Coordinates = new double[] { -77.0369, 38.9072 } // Coordenadas de ejemplo para Washington, D.C.
                        }
                    }
                }
            };
             */

            //El objeto generado se guarda
            this.Theater_Save(newTheater);
            this.User_Save(newUser);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nDocumento insertado\nId: {newTheater.Id}, City: {newTheater.Location.Address.City}");
            Console.WriteLine($"\nDocumento insertado\nId: {newUser.Id}, email: {newUser.email}");


            //El objeto generado se actualiza
            //newTheater.Location.Address.City = "Reynosa";
            //this.Theater_Update(newTheater);
            //Console.ForegroundColor = ConsoleColor.DarkCyan;
            //Console.WriteLine($"\nDocumento actualizado\nId: {newTheater.Id}, City: {newTheater.Location.Address.City}");

            //this.Theater_Delete(newTheater.Id);
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"\nDocumento eliminado\n");

            Console.ResetColor();
        }

        public List<Movies> Movies(IMongoDatabase database)
        {
            //Obtener una lista de la clase Theater
            //Propósito: Habiendo llegado a la base de datos, recuperaremos todos los registros de la tabla theaters, estos se generarán en forma de lista a partir de nuestra clase Theaters

            //Creamos una variable "var" llamada con un texto que haga alusión a la colección que vamos a buscar, para intentar aplicar una normalización
            //En este caso queremos una colección de Theaters (es un NOMBRE de clase), entonces usaremos <NOMBRE>Collection
            //Theater = Es una clase que hemos creado para facilitar nuestras consultas, crearás una por cada colección que tengas junto a sus atributos

            var moviesCollection = database.GetCollection<Movies>("movies"); //database.GetCollection<CLASE>("COLECCIÓN")

            //Crearemos una lista de nuestra clase, para que los documentos tengan el formato deseado de la clase y facilitar su manipulación
            var filter = Builders<Movies>.Filter.Gte(x => x.num_mflix_comments, 1);

            // Obtener los primeros 200 documentos que cumplen con el filtro
            List<Movies> allMovies = moviesCollection.Find(filter).Limit(400).ToList();
            //List<Movies> filterList = allMovies.Where(x => x.released.Year >= 1800 && x.released.Year <= 1950).ToList();t;

            //Retornamos nuestra lista, de esa manera al usar este método podrán recuperar la lista completa de registros cuando los necesitemos
            return allMovies;
            //Se repetirá este procedimiento/método para todas las tablas que desees convertir a listas a partir de sus respectivas clases, asegurate de crear su clase con sus parámetros antes
        }

        public List<Comments> Comments(IMongoDatabase database, List<Movies> movies)
        {
            //Obtener una lista de la clase Theater
            //Propósito: Habiendo llegado a la base de datos, recuperaremos todos los registros de la tabla theaters, estos se generarán en forma de lista a partir de nuestra clase Theaters

            //Creamos una variable "var" llamada con un texto que haga alusión a la colección que vamos a buscar, para intentar aplicar una normalización
            //En este caso queremos una colección de Theaters (es un NOMBRE de clase), entonces usaremos <NOMBRE>Collection
            //Theater = Es una clase que hemos creado para facilitar nuestras consultas, crearás una por cada colección que tengas junto a sus atributos

            var CommentsCollection = database.GetCollection<Comments>("comments"); //database.GetCollection<CLASE>("COLECCIÓN")

            //Crearemos una lista de nuestra clase, para que los documentos tengan el formato deseado de la clase y facilitar su manipulación

            // Crear un filtro para los IDs de películas
            var movieIds = movies.Select(movie => movie.Id);
            var filter = Builders<Comments>.Filter.In(comment => comment.movie_id, movieIds);

            // Obtener los comentarios que cumplen con el filtro
            List<Comments> filteredComments = CommentsCollection.Find(filter).Limit(500).ToList();
            //Retornamos nuestra lista, de esa manera al usar este método podrán recuperar la lista completa de registros cuando los necesitemos
            return filteredComments;
            //Se repetirá este procedimiento/método para todas las tablas que desees convertir a listas a partir de sus respectivas clases, asegurate de crear su clase con sus parámetros antes
        }
        public List<Product> products(IMongoDatabase database)
        {
            var productsCollection = database.GetCollection<Product>("Products");
            List<Product> allProducts = productsCollection.Find(new BsonDocument()).ToList();
            return allProducts;
        }

        public List<CurrentProduct> CurrentProduct(IMongoDatabase database)
        {
            var productsCollection = database.GetCollection<CurrentProduct>("CurrentProduct");
            List<CurrentProduct> allProducts = productsCollection.Find(new BsonDocument()).ToList();
            return allProducts;
        }

        public void Product_Save(CurrentProduct product)
        {
            //Propósito: Modificar un solo documento
            var collection = this.mongoDatabase.GetCollection<CurrentProduct>("CurrentProduct");

            var filter = Builders<CurrentProduct>.Filter.Eq("_id", new ObjectId(product.Id)); // Filtrar por el identificador (Id), para actualizar a ese documento en específico que queremos modificar

            var update = Builders<CurrentProduct>.Update
                .Set("Sensor1", product.Sensor1)
                .Set("Sensor2", product.Sensor2)
                .Set("Sensor3", product.Sensor3)
                .Set("Sensor4", product.Sensor4)
                .Set("Color.Red", product.Color.Red)
                .Set("Color.Blue", product.Color.Blue)
                .Set("Color.Green", product.Color.Green)
                .Set("Active", product.Active);

            collection.UpdateOne(filter, update); //Se reemplaza el viejo objeto por el nuevo con los campos modificados
        }

        

        public void productDelete(string id)
        {
            // Propósito: Eliminar un solo documento basado en su identificador único
            var commentColection = this.mongoDatabase.GetCollection<Product>("Products");

            var filter = Builders<Product>.Filter.Eq("_id", new ObjectId(id)); // Filtrar por el identificador único (Id) del documento a eliminar

            commentColection.DeleteOne(filter);
        }

        public void TimeSeries_Save(API newRecord)
        {
            var collection = this.mongoDatabase.GetCollection<API>("TIME_SERIES");
            collection.InsertOne(newRecord);
        }

        //A esto si le puedes mover, es más, debes hacerlo
        //
        public List<Theater> Theaters(IMongoDatabase database)
        {
            //Obtener una lista de la clase Theater
            //Propósito: Habiendo llegado a la base de datos, recuperaremos todos los registros de la tabla theaters, estos se generarán en forma de lista a partir de nuestra clase Theaters

            //Creamos una variable "var" llamada con un texto que haga alusión a la colección que vamos a buscar, para intentar aplicar una normalización
            //En este caso queremos una colección de Theaters (es un NOMBRE de clase), entonces usaremos <NOMBRE>Collection
            //Theater = Es una clase que hemos creado para facilitar nuestras consultas, crearás una por cada colección que tengas junto a sus atributos

            var theatersCollection = database.GetCollection<Theater>("theaters"); //database.GetCollection<CLASE>("COLECCIÓN")

            //Crearemos una lista de nuestra clase, para que los documentos tengan el formato deseado de la clase y facilitar su manipulación
            List<Theater> allTheaters = theatersCollection.Find(new BsonDocument()).ToList();

            //Retornamos nuestra lista, de esa manera al usar este método podrán recuperar la lista completa de registros cuando los necesitemos
            return allTheaters;
            //Se repetirá este procedimiento/método para todas las tablas que desees convertir a listas a partir de sus respectivas clases, asegurate de crear su clase con sus parámetros antes
        }


        public void movieUpdate(Movies updatedMovie)
        {
            //Propósito: Modificar un solo documento
            var moviessCollection = this.mongoDatabase.GetCollection<Movies>("movies");

            var filter = Builders<Movies>.Filter.Eq("_id", new ObjectId(updatedMovie.Id)); // Filtrar por el identificador (Id), para actualizar a ese documento en específico que queremos modificar

            var update = Builders<Movies>.Update
                //.Set("CAMPO", <ATRIBUTO>); // Actualizar el campo del documento a reemplazar, a partir del atributo de tu objeto entrante que es el nuevo
                .Set("plot", updatedMovie.plot)
                .Set("fullplot", updatedMovie.fullplot) // Actualizar el campo City
                .Set("languages", updatedMovie.languages);

            moviessCollection.UpdateOne(filter, update); //Se reemplaza el viejo objeto por el nuevo con los campos modificados
        }

        public void Comment_Update(Comments updatedComment)
        {
            //Propósito: Modificar un solo documento
            var commentsCollection = this.mongoDatabase.GetCollection<Comments>("comments");

            var filter = Builders<Comments>.Filter.Eq("_id", new ObjectId(updatedComment.Id)); // Filtrar por el identificador (Id), para actualizar a ese documento en específico que queremos modificar

            var update = Builders<Comments>.Update
                //.Set("CAMPO", <ATRIBUTO>); // Actualizar el campo del documento a reemplazar, a partir del atributo de tu objeto entrante que es el nuevo
                .Set("name", updatedComment.name)
                .Set("email", updatedComment.email) // Actualizar el campo City
                .Set("movie_id", updatedComment.movie_id)
            .Set("text", updatedComment.text)
            .Set("date", updatedComment.date);

            commentsCollection.UpdateOne(filter, update); //Se reemplaza el viejo objeto por el nuevo con los campos modificados
        }

        public void Comment_Delete(string commentId)
        {
            // Propósito: Eliminar un solo documento basado en su identificador único
            var commentColection = this.mongoDatabase.GetCollection<Comments>("comments");

            var filter = Builders<Comments>.Filter.Eq("_id", new ObjectId(commentId)); // Filtrar por el identificador único (Id) del documento a eliminar

            commentColection.DeleteOne(filter);
        }

        public List<Users> Users(IMongoDatabase database)
        {
            var UsersCollection = database.GetCollection<Users>("users"); 
            List<Users> allUsers = UsersCollection.Find(new BsonDocument()).ToList();
            return allUsers;
        }

        public List<User> ApiUsers(IMongoDatabase database)
        {
            var UsersCollection = database.GetCollection<User>("ApiUsers");
            List<User> allUsers = UsersCollection.Find(new BsonDocument()).ToList();
            return allUsers;
        }

        public void Api_User_Save(User newUser)
        {
            var UsersCollection = this.mongoDatabase.GetCollection<User>("ApiUsers");
            UsersCollection.InsertOne(newUser);
        }

        public void Api_User_Delete(string userId)
        {
            var usersCollection = this.mongoDatabase.GetCollection<User>("ApiUsers");
            var filter = Builders<User>.Filter.Eq("_id", new ObjectId(userId));
            usersCollection.DeleteOne(filter);
        }

        public void Api_User_Update(User updatedUser)
        {
            //Propósito: Modificar un solo documento
            var usersCollection = this.mongoDatabase.GetCollection<User>("ApiUsers");

            var filter = Builders<User>.Filter.Eq("_id", new ObjectId(updatedUser.Id)); // Filtrar por el identificador (Id), para actualizar a ese documento en específico que queremos modificar

            var update = Builders<User>.Update
                //.Set("CAMPO", <ATRIBUTO>); // Actualizar el campo del documento a reemplazar, a partir del atributo de tu objeto entrante que es el nuevo
                .Set("username", updatedUser.username)
                .Set("status", updatedUser.status)
                .Set("role", updatedUser.role); // Actualizar el campo City

            usersCollection.UpdateOne(filter, update); //Se reemplaza el viejo objeto por el nuevo con los campos modificados
        }

        public void Theater_Save(Theater newTheater)
        {
            //Propósito: Guardar un solo documento
            //Recuerda cambiar la clase y la colección por las de tu preferencia, para poder guardar tus registros en esa colección a partir de un objeto
            var theatersCollection = this.mongoDatabase.GetCollection<Theater>("theaters"); //database.GetCollection<CLASE>("COLECCIÓN")

            //Insertar el nuevo documento en la colección
            theatersCollection.InsertOne(newTheater);
        }

        //GUARDAR UN USUARIO

        public void User_Save(Users newUser)
        {
            var UsersCollection = this.mongoDatabase.GetCollection<Users>("users"); 
            UsersCollection.InsertOne(newUser);
        }

        public void User_Delete(string userId)
        {
            var theatersCollection = this.mongoDatabase.GetCollection<Users>("users");
            var filter = Builders<Users>.Filter.Eq("_id", new ObjectId(userId));
            theatersCollection.DeleteOne(filter);
        }

        public void User_Update(Users updatedUser)
        {
            //Propósito: Modificar un solo documento
            var usersCollection = this.mongoDatabase.GetCollection<Users>("users");
            var filter = Builders<Users>.Filter.Eq("_id", new ObjectId(updatedUser.Id));
            var update = Builders<Users>.Update
                .Set("name", updatedUser.name)

                .Set("email", updatedUser.email); 
            usersCollection.UpdateOne(filter, update); 
        }

        public void Users_DeleteAll(List<string> userIds)
        {
            // Propósito: Eliminar una lista de documentos basados en sus identificadores únicos
            var usersCollection = this.mongoDatabase.GetCollection<Users>("users");

            foreach (var userId in userIds)
            {
                var filter = Builders<Users>.Filter.Eq("_id", new ObjectId(userId)); // Filtrar por el identificador único (Id) del documento a eliminar

                usersCollection.DeleteMany(filter);
            }
        }

        public void Theater_SaveAll(List<Theater> newTheaters)
        {
            //Propósito: Guardar una lista de documentos
            //Recuerda cambiar la clase y la colección por las de tu preferencia, para poder guardar tus registros en esa colección a partir de una lista de objetos
            var theatersCollection = this.mongoDatabase.GetCollection<Theater>("theaters"); //database.GetCollection<CLASE>("COLECCIÓN")

            //Insertar los nuevos documentos en la colección
            theatersCollection.InsertMany(newTheaters);
        }

        public void Theater_Update(Theater updatedTheater)
        {
            //Propósito: Modificar un solo documento
            var theatersCollection = this.mongoDatabase.GetCollection<Theater>("theaters");

            var filter = Builders<Theater>.Filter.Eq("_id", new ObjectId(updatedTheater.Id)); // Filtrar por el identificador (Id), para actualizar a ese documento en específico que queremos modificar

            var update = Builders<Theater>.Update
                //.Set("CAMPO", <ATRIBUTO>); // Actualizar el campo del documento a reemplazar, a partir del atributo de tu objeto entrante que es el nuevo
                .Set("Location.Address.City", updatedTheater.Location.Address.City); // Actualizar el campo City

            theatersCollection.UpdateOne(filter, update); //Se reemplaza el viejo objeto por el nuevo con los campos modificados
        }
        public void Theater_UpdateAll(List<Theater> updatedTheaters)
        {
            //Propósito: Modificar una lista de documentos
            var theatersCollection = this.mongoDatabase.GetCollection<Theater>("theaters");

            foreach (var updatedTheater in updatedTheaters)
            {
                //Iremos pasando por cada elemento de nuestra lista para buscar esos documentos coincidentes a partir del Id y hacer las modificaciones correspondientes por cada elemento de la lista, si son 5 objetos en la lista, se actualizan 5 documentos
                var filter = Builders<Theater>.Filter.Eq("_id", new ObjectId(updatedTheater.Id)); // Filtrar por el identificador (Id), para actualizar a ese documento en específico que queremos modificar

                var update = Builders<Theater>.Update
                    .Set("TheaterId", updatedTheater.TheaterId) // Actualizar el campo TheaterId
                                                                //.Set("CAMPO", <ATRIBUTO>); // Actualizar el campo del documento a reemplazar, a partir del atributo de tu objeto entrante que es el nuevo
                    .Set("Location", updatedTheater.Location); // Actualizar el campo Location

                theatersCollection.UpdateOne(filter, update); //Se reemplaza el viejo objeto por el nuevo con los campos modificados
            }
        }
        public void Theater_Delete(string theaterId)
        {
            // Propósito: Eliminar un solo documento basado en su identificador único
            var theatersCollection = this.mongoDatabase.GetCollection<Theater>("theaters");

            var filter = Builders<Theater>.Filter.Eq("_id", new ObjectId(theaterId)); // Filtrar por el identificador único (Id) del documento a eliminar

            theatersCollection.DeleteOne(filter);
        }
        public void Theater_DeleteAll(List<string> theaterIds)
        {
            // Propósito: Eliminar una lista de documentos basados en sus identificadores únicos
            var theatersCollection = this.mongoDatabase.GetCollection<Theater>("theaters");

            foreach (var theaterId in theaterIds)
            {
                var filter = Builders<Theater>.Filter.Eq("_id", new ObjectId(theaterId)); // Filtrar por el identificador único (Id) del documento a eliminar

                theatersCollection.DeleteMany(filter);
            }
        }

        //Demás listas y métodos de guardado, actualización o eliminación de aquí en adelante
        //SI AUTORIZO
        //...
    }
}