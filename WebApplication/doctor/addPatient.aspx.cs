using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.webServiceReference;
namespace WebApplication.doctor
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        WebService1 ws;
        private bool ValidateUser(string userName, string passWord)
        {
            if ((null == userName) || (0 == userName.Length) || (userName.Length > 15))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.");
                return false;
            }

            if ((null == passWord) || (0 == passWord.Length) || (passWord.Length > 25))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of passWord failed.");

                return false;
            }

            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ws = new WebService1();
        }
        public void add(Object sender, EventArgs e)
        {
            try
            {
                String usernameTb = addUsername.Text.ToString();
                String passwordTb = addPassword.Text.ToString();
                String dniTb = dniTextbox.Text.ToString();

                if (ValidateUser(usernameTb, passwordTb))
                {
                    string doctorID = Request.Cookies.Get("id").Value;
                    string doctorIdFixed = doctorID.Replace("id=", "");
                    int id = ws.insertPacient(doctorIdFixed, dniTb, usernameTb, passwordTb);
                    Response.Redirect(Page.ResolveClientUrl("../patient/patient.aspx?id=" + id));
                }
            }
            catch (Exception ex)
            {

            }
           
        }

    }
}
