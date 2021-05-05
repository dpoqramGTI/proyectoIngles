using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.webServiceReference;

namespace WebApplication.doctor
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebService1 ws = new WebService1();
            string doctorID = Request.Cookies.Get("id").Value;
            string id = doctorID.Replace("id=", "");
            List<Pacient> historicList = ws.getAllDoctorPacients(Convert.ToInt32(id)).ToList();

            /*tbody.InnerHtml = "<tr> <th scope='row'>1</th> <td><a href=" + "./ datoUsuario.aspx>" +
                    historicList[0].diagnose + "</a></td> <td>" + historicList[0].date + "</td> <td>" + historicList[0].treatment + "</td> </tr>";
            */
            if (historicList.Count > 0)
            {
                foreach (Pacient patient in historicList)
                {
                    tbodydoctor.InnerHtml += "<tr> <th scope='row'>" + patient.id + "</th> <td><a href=" + "../patient/patient.aspx?id=" + patient.id + ">" +
                        patient.name + "</a></td> <td>" + patient.dni + "</td> </tr>";
                    //Console.WriteLine($"Element # {informer}");
                }
            }
        }
    }
}