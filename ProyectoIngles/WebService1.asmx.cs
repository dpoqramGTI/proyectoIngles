using Microsoft.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
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
        public void insertClinicalStory(int doctorId, int pacientId, string diagnose, string treatment)
        {
            Database databaseObject = new Database();
            // INSERT INTO DATABASE
            string query = "INSERT INTO Historic (`DoctorID`,`PacientID`,`Diagnose`,`Treatment`) VALUES (@DoctorID,@PacientID,@Diagnose,@Treatment)";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@DoctorID", doctorId);
            myCommand.Parameters.AddWithValue("@PacientID", pacientId);
            myCommand.Parameters.AddWithValue("@Diagnose", diagnose);
            myCommand.Parameters.AddWithValue("@Treatment", treatment);

            var result = myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();

            Console.WriteLine("Rows Added : {0}", result);
        }

        [WebMethod]
        public void InsertDoctor(string name)
        {
            Database databaseObject = new Database();
            // INSERT INTO DATABASE
            string query = "INSERT INTO Doctor (`Name`) VALUES (@Name)";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@Name", name);
            var result = myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();

            Console.WriteLine("Rows Added : {0}", result);
        }

        [WebMethod]
        public List<Historic> getAllDoctorPacients(int doctorId, int pacientId)
        {
            Database databaseObject = new Database();
            List<Historic> pacientList = new List<Historic>();

            //// SELECT FROM DATABASE
            string query = "SELECT * FROM Historic WHERE DoctorID=@DoctorID AND PacientID=@PacientID;";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@DoctorID", doctorId);
            myCommand.Parameters.AddWithValue("@PacientID", pacientId);

            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Historic pacient = new Historic(Convert.ToInt32(result["ID"]), Convert.ToInt32(result["DoctorID"]), Convert.ToInt32(result["PacientID"]), Convert.ToString(result["Diagnose"]), Convert.ToString(result["Treatment"]), Convert.ToString(result["Date"]));                  //Console.WriteLine("ID: {0} - Name: {1}", result["ID"], result["Name"], result["DNI"]);
                    pacientList.Add(pacient);
                }
            }
            databaseObject.CloseConnection();
            return pacientList;
        }

        [WebMethod]
        public string getPacientInfo(int pacientId)
        {
            Database databaseObject = new Database();
            string resultStr = "EMPTY";
            //// SELECT FROM DATABASE
            //string query = "SELECT ID FROM Pacient WHERE DNI = '@dni' AND Password = '@password';";
            string query = "SELECT * FROM Pacient WHERE ID = @id;";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@id", pacientId);
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    resultStr = Convert.ToString(result["ID"]);
                }
            }
            databaseObject.CloseConnection();
            return resultStr;
        }
        [WebMethod]
        public String loginPacient(string dni, string password)
        {
            Database databaseObject = new Database();
            string resultStr = "EMPTY";
            //// SELECT FROM DATABASE
            //string query = "SELECT ID FROM Pacient WHERE DNI = '@dni' AND Password = '@password';";
            string query = "SELECT ID FROM Pacient WHERE DNI = @DNI AND Password = @Password;";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@DNI", dni);
            myCommand.Parameters.AddWithValue("@Password", password);
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    resultStr = Convert.ToString(result["ID"]);
                }
            }
            databaseObject.CloseConnection();
            return resultStr;
        }

        public String loginDoctor(string dni, string password)
        {
            Database databaseObject = new Database();
            string resultStr = "EMPTY";
            //// SELECT FROM DATABASE
            //string query = "SELECT ID FROM Pacient WHERE DNI = '@dni' AND Password = '@password';";
            string query = "SELECT ID FROM Doctor WHERE DNI = @DNI AND Password = @Password;";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@DNI", dni);
            myCommand.Parameters.AddWithValue("@Password", password);
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    resultStr = Convert.ToString(result["ID"]);
                }
            }
            databaseObject.CloseConnection();
            return resultStr;
        }
        private static string ReadSingleRow(IDataRecord record)
        {
            return record[0].ToString();
        }

        [WebMethod]
        public void insertPacient(string dni, string name, string password)
        {
            Database databaseObject = new Database();
            // INSERT INTO DATABASE
            string query = "INSERT INTO Pacient (`DNI`, `Name`, `Password`) VALUES (@DNI,@Name,@Password)";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@DNI", dni);
            myCommand.Parameters.AddWithValue("@Name", name);
            String md5password = md5Encrypt(password);
            myCommand.Parameters.AddWithValue("@Password", md5password);

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

        public string md5Encrypt(string text)
        {
            Encoding unicode = Encoding.Unicode;
            // Convert unicode string into a byte array.  
            byte[] textInbyteArray = unicode.GetBytes(text);

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(textInbyteArray);
                string passwordMD5 = BitConverter.ToString(data).Replace("-", string.Empty);
                return passwordMD5;
            }
        }
    }
}
