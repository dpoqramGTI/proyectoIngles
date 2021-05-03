using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoIngles
{

    public class Historic
    {
        public int id { get; set; }
        public int DoctorId { get; set; }
        public int PacientId { get; set; }

        public string diagnose { get; set; }
        public string treatment { get; set; }
        public string date { get; set; }

        public Historic(int id, int doctorId, int pacientId, string diagnose, string treatment, string date)
        {
            this.id = id;
            DoctorId = doctorId;
            PacientId = pacientId;
            this.diagnose = diagnose;
            this.treatment = treatment;
            this.date = date;
        }

        public Historic() { }
    }
}