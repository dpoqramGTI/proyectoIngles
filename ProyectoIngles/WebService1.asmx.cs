using Microsoft.Data.Sqlite;
using System;
using System.Data.SQLite;
using System.Web;
using System.Web.Services;

namespace ProyectoIngles
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public String DBpath;
        public SQLiteConnection conn;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }
        [WebMethod]
        /*public void insertDoctor()
        {
            try
            {
                // Creating db connection
                //DBpath = HttpContext.Current.Request.PhysicalApplicationPath + "BaseDeDatos.db";
                DBpath = "Data Source = C:\\Users\\DANI\\source\\repos\\ProyectoIngles\\ProyectoIngles\\BaseDeDatos.db; Version = 3; ";
                conn = new SQLiteConnection("Data Source=" + DBpath + "Version=3;");

                // Query
                string query = "INSERT INTO Doctor(Name) VALUES('" + "Angela" + "';";
                conn.Open();
                SQLiteCommand comm = new SQLiteCommand(query, conn);
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }*/

        public void insertDoctor(string name)
        {
            Database databaseObject = new Database();
            // INSERT INTO DATABASE
            string query = "INSERT INTO Doctor (`Name`) VALUES (@Name)";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@Name", "Anita");
            var result = myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();

            Console.WriteLine("Rows Added : {0}", result);
        }
        public void selectAllDoctors()
        {
            Database databaseObject = new Database();

            //// SELECT FROM DATABASE
            string query = "SELECT * FROM Doctor";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Console.WriteLine("ID: {0} - Name: {1}", result["ID"], result["Name"]);
                }
            }
            databaseObject.CloseConnection();

            //Console.ReadKey();
        }
    }
}
