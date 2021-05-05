using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoIngles
{
    public class Userdata
    {
        public int id { get; set; }
        public string loguedAs { get; set; }

        public Userdata(int id, string loguedAs)
        {
            this.id = id;
            this.loguedAs = loguedAs;
        }
        public Userdata() { }

    }
}