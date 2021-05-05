using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.webServiceReference;

namespace WebApplication
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        private string path = @"C:\Users\DANI\source\repos\ProyectoIngles\ProyectoIngles\data.json";

        string id = "";
        int idInt = 0;
        string isDoctor = "";
        string doctorIDfixed = "";
        WebService1 ws;
        List<Historic> historicList;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ws = new WebService1();

                id = Request.QueryString["id"];
                idInt = Convert.ToInt32(id);
                isDoctor = Request.Cookies.Get("doctor").Value;
                string doctorID = Request.Cookies.Get("id").Value;
                doctorIDfixed = doctorID.Replace("id=", "");
                Pacient patient = ws.getPacientInfo(idInt);

                patientDniTb.Text = patient.dni;
                patientNameTb.Text = patient.name;
                editPatientBtn.Visible = false;
                deleteBtn.Visible = false;
                divAddHistoric.Visible = false;
                createJSON.Visible = false;
                sectionCardPatient.Visible = false;

                if (id != null && isDoctor == "doctor=true")
                {
                    divAddHistoric.Visible = true;
                    editPatientBtn.Visible = true;
                    deleteBtn.Visible = true;
                    createJSON.Visible = true;
                    sectionCardPatient.Visible = true;
                }
                historicList = ws.getAllPacientRecords(idInt).ToList();


                if (historicList.Count > 0)
                {
                    foreach (Historic informer in historicList)
                    {
                        tbody.InnerHtml += "<tr> <th scope='row'>" + informer.id + "</th> <td><a href=" + "./ datoUsuario.aspx>" +
                            informer.diagnose + "</a></td> <td>" + informer.date + "</td> <td>" + informer.treatment + "</td> </tr>";
                        //Console.WriteLine($"Element # {informer}");
                    }
                }
            }
            catch (Exception ex)
            {

            }



        }

        public void editPatient(Object sender, EventArgs e)
        {
            try
            {
                if (id != null && isDoctor == "doctor=true")
                {
                    string dni = patientDniTb.Text.ToString();
                    string name = patientNameTb.Text.ToString();
                    ws.updatePacient(id, dni, name);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void createJson(Object sender, EventArgs e)
        {
            Pacient patient = ws.getPacientInfo(idInt);
            string data = JsonConvert.SerializeObject(patient);
            string historic = JsonConvert.SerializeObject(historicList);
            File.WriteAllText(path, "[" + data.ToString() + historic.ToString() + "]");
            Response.Write("<script>alert('Json created succesfully');</script>");
        }
        public void deletePatientdoc(Object sender, EventArgs e)
        {
            try
            {
                if (id != null && isDoctor == "doctor=true")
                {
                    ws.deletePacient(id);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void addHistoric(Object sender, EventArgs e)
        {

            String diagnoseStr = diagnose.Text.ToString();
            String treatmentStr = treatment.Text.ToString();
            String dateStr = date.Text.ToString();
            ws.insertHistoric(Convert.ToInt32(doctorIDfixed), idInt, diagnoseStr, treatmentStr, dateStr);

        }

    }
}