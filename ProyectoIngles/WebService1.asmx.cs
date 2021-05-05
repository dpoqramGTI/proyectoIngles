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

        /*********************************************************************************************
         * 
         * INSERTS
         * 
        *********************************************************************************************/

        [WebMethod]
        public void insertHistoric(int doctorId, int pacientId, string diagnose, string treatment, string date)
        {
            Database databaseObject = new Database();
            string query = "INSERT INTO Historic (`DoctorID`,`PacientID`,`Diagnose`,`Treatment`, `Date`) VALUES (@DoctorID,@PacientID,@Diagnose,@Treatment,@Date)";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@DoctorID", doctorId);
            myCommand.Parameters.AddWithValue("@PacientID", pacientId);
            myCommand.Parameters.AddWithValue("@Diagnose", diagnose);
            myCommand.Parameters.AddWithValue("@Treatment", treatment);
            myCommand.Parameters.AddWithValue("@Date", date);


            var result = myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();

            Console.WriteLine("Rows Added : {0}", result);
        }

        [WebMethod]
        public void InsertDoctor(string dni, string name, string password)
        {
            Database databaseObject = new Database();
            string query = "INSERT INTO Doctor (`DNI`, `Name`, `Password`) VALUES (@DNI,@Name,@Password)";
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
        private Pacient getLatestPacient()
        {
            Database databaseObject = new Database();
            Pacient pacient = null;

            string query = "SELECT * FROM Pacient ORDER BY ID DESC LIMIT 1";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    pacient = new Pacient(Convert.ToInt32(result["ID"]), Convert.ToString(result["DNI"]), Convert.ToString(result["Name"]));
                }
            }
            databaseObject.CloseConnection();
            return pacient;
        }
        [WebMethod]
        public int insertPacient(string doctorId, string dni, string name, string password)
        {
            int id = 0;
            Database databaseObject = new Database();
            string query = "INSERT INTO Pacient (`DNI`, `Name`, `Password`) VALUES (@DNI,@Name,@Password)";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@DNI", dni);
            myCommand.Parameters.AddWithValue("@Name", name);
            String md5password = md5Encrypt(password);
            myCommand.Parameters.AddWithValue("@Password", md5password);
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    id = Convert.ToInt32(result["ID"]);
                }
                databaseObject.CloseConnection();
                Console.WriteLine("Rows Added : {0}", result);
            }
            Pacient pacient = getLatestPacient();
            insertHistoric(Convert.ToInt32(doctorId), pacient.id, "initial diagnose", "initial treatment", DateTime.Now.ToString());
            return pacient.id;
        }


        /*********************************************************************************************
         * 
         * SELECTS
         * 
        *********************************************************************************************/


        [WebMethod]
        public List<Pacient> getAllDoctorPacients(int doctorId)
        {
            Database databaseObject = new Database();
            List<Pacient> pacientList = new List<Pacient>();

            string query = "SELECT * FROM Historic WHERE DoctorID = @DoctorID GROUP By PacientID; ";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@DoctorID", doctorId);

            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Pacient pacient = getPacientInfo(Convert.ToInt32(result["PacientID"]));
                    pacientList.Add(pacient);
                }
            }
            databaseObject.CloseConnection();
            return pacientList;
        }

        [WebMethod]
        public List<Historic> getAllPacientRecords(int pacientId)
        {
            Database databaseObject = new Database();
            List<Historic> pacientList = new List<Historic>();

            string query = "SELECT * FROM Historic WHERE PacientID=@PacientID;";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
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
        public Pacient getPacientInfo(int pacientId)
        {
            Database databaseObject = new Database();
            Pacient pacient = null;
            string query = "SELECT * FROM Pacient WHERE ID = @id;";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@id", pacientId);
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    pacient = new Pacient(Convert.ToInt32(result["ID"]), Convert.ToString(result["DNI"]), Convert.ToString(result["Name"]));
                }
            }
            databaseObject.CloseConnection();
            return pacient;
        }



        [WebMethod]
        public Doctor getDoctorInfo(int doctorId)
        {
            Database databaseObject = new Database();
            Doctor doctor = null;
            string query = "SELECT * FROM Doctor WHERE ID = @id;";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@id", doctorId);
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    doctor = new Doctor(Convert.ToInt32(result["ID"]), Convert.ToString(result["DNI"]), Convert.ToString(result["Name"]), Convert.ToString(result["Password"]));
                }
            }
            databaseObject.CloseConnection();
            return doctor;
        }

        [WebMethod]
        public List<Doctor> getAllDoctors()
        {
            Database databaseObject = new Database();
            List<Doctor> doctorList = new List<Doctor>();

            string query = "SELECT * FROM Doctor";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            SQLiteDataReader result = myCommand.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Doctor doctor = new Doctor(Convert.ToInt32(result["ID"]), Convert.ToString(result["DNI"]), Convert.ToString(result["Name"]), Convert.ToString(result["Password"]));
                    doctorList.Add(doctor);
                }
            }
            databaseObject.CloseConnection();

            return doctorList;
        }

        /*********************************************************************************************
         * 
         * LOGIN STUFF
         * 
        *********************************************************************************************/

        [WebMethod]
        public String loginPacient(string dni, string password)
        {
            Database databaseObject = new Database();
            string resultStr = "EMPTY";
            string query = "SELECT ID FROM Pacient WHERE DNI = @DNI AND Password = @Password;";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@DNI", dni);
            String md5password = md5Encrypt(password);
            myCommand.Parameters.AddWithValue("@Password", md5password);
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
            string query = "SELECT ID FROM Doctor WHERE DNI = @DNI AND Password = @Password;";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@DNI", dni);
            String md5password = md5Encrypt(password);
            myCommand.Parameters.AddWithValue("@Password", md5password);
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
        public Userdata login(string dni, string password)
        {

            string doctorCheck = loginDoctor(dni, password);
            string pacientCheck = loginPacient(dni, password);

            if (!doctorCheck.Equals("EMPTY"))
            {
                return new Userdata(Convert.ToInt32(doctorCheck), "DOCTOR");
            }
            else if (!pacientCheck.Equals("EMPTY"))
            {
                return new Userdata(Convert.ToInt32(pacientCheck), "PACIENT");
            }
            else
            {
                return new Userdata(0, "EMPTY");
            }
        }

        public string md5Encrypt(string text)
        {
            Encoding unicode = Encoding.Unicode;
            byte[] textInbyteArray = unicode.GetBytes(text);

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(textInbyteArray);
                string passwordMD5 = BitConverter.ToString(data).Replace("-", string.Empty);
                return passwordMD5;
            }
        }

        /*********************************************************************************************
         * 
         * UPDATES
         * 
        *********************************************************************************************/

        [WebMethod]
        public void updateDoctor(string doctorId, string dni, string name, string password)
        {
            Database databaseObject = new Database();
            string query = "UPDATE Doctor SET DNI = @dni, Name = @name, Password = @password WHERE ID = @doctorId; ";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@name", name);
            myCommand.Parameters.AddWithValue("@dni", dni);
            String md5password = md5Encrypt(password);
            myCommand.Parameters.AddWithValue("@password", md5password);
            myCommand.Parameters.AddWithValue("@doctorId", doctorId);
            myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();
        }

        [WebMethod]
        public void updatePacient(string pacientId, string dni, string name)
        {
            Database databaseObject = new Database();
            string query = "UPDATE Pacient SET DNI = @dni, Name = @name WHERE ID = @pacientId; ";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@name", name);
            myCommand.Parameters.AddWithValue("@dni", dni);
            myCommand.Parameters.AddWithValue("@pacientId", pacientId);
            myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();
        }

        [WebMethod]
        public void updateClinicalRecords(string id, string diagnose, string treatment, string date)
        {
            Database databaseObject = new Database();
            string query = "UPDATE Historic SET Diagnose = @diagnose, Treatment = @treatment, Date = @date WHERE ID = @id; ";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@id", id);
            myCommand.Parameters.AddWithValue("@diagnose", diagnose);
            myCommand.Parameters.AddWithValue("@treatment", treatment);
            myCommand.Parameters.AddWithValue("@date", date);
            myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();
        }

        /*********************************************************************************************
         * 
         * UPDATES
         * 
        *********************************************************************************************/

        [WebMethod]
        public void deleteDoctor(string doctorId)
        {
            Database databaseObject = new Database();
            string query = "DELETE FROM Doctor WHERE ID = @doctorId; ";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@doctorId", doctorId);
            myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();
        }

        [WebMethod]
        public void deletePacient(string pacientId)
        {
            Database databaseObject = new Database();
            string query = "DELETE FROM Pacient WHERE ID = @pacientId; ";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@pacientId", pacientId);
            myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();
            deleteClinicalRecordsWithPacientId(pacientId);
        }

        [WebMethod]
        public void deleteClinicalRecord(string recordId)
        {
            Database databaseObject = new Database();
            string query = "DELETE FROM Historic WHERE ID = @recordId; ";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@recordId", recordId);
            myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();
        }

        [WebMethod]
        public void deleteClinicalRecordsWithPacientId(string pacientId)
        {
            Database databaseObject = new Database();
            string query = "DELETE FROM Historic WHERE PacientID = @pacientId; ";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            databaseObject.OpenConnection();
            myCommand.Parameters.AddWithValue("@pacientId", pacientId);
            myCommand.ExecuteNonQuery();
            databaseObject.CloseConnection();
        }
    }
}
