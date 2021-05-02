using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Data.SQLite;

namespace ProyectoIngles
{
    public class Database
    {
        public SQLiteConnection myConnection;
        private string DBpath = "C:\\Users\\DANI\\source\\repos\\ProyectoIngles\\ProyectoIngles\\BaseDeDatos.db;";

        public Database()
        {
            myConnection = new SQLiteConnection("Data Source = " + DBpath + " Version = 3; ");
            if (!File.Exists("C:\\Users\\DANI\\source\\repos\\ProyectoIngles\\ProyectoIngles\\BaseDeDatos.db"))
            {
                SQLiteConnection.CreateFile("C:\\Users\\DANI\\source\\repos\\ProyectoIngles\\ProyectoIngles\\BaseDeDatosCreada.db");
                System.Console.WriteLine("Database file created");
            }
        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Clone();
            }
        }
    }
}