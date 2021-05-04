using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoIngles
{
    public class Doctor
    {
        public int id { get; set; }
        public string dni { get; set; }
        public string name { get; set; }
        public string password { get; set; }

        public Doctor(int id, string dni, string name, string password)
        {
            this.id = id;
            this.dni = dni;
            this.name = name;
            this.password = password;
        }

        public Doctor(int id, string dni, string name)
        {
            this.id = id;
            this.dni = dni;
            this.name = name;
            this.password = "*****";
        }

        public Doctor(){}
    }
}