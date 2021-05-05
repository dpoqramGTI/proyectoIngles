using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using WebApplication.webServiceReference;
using System.Web.Security;

namespace WebApplication
{
    public partial class WebForm2 : System.Web.UI.Page
    {
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

            WebService1 ws = new WebService1();

            return true;
        }
        public void createLogin(Object sender, EventArgs e)
        {
            try
            {
                String usernameTb = username.Text.ToString();
                String passwordTb = password.Text.ToString();

                if (ValidateUser(usernameTb, passwordTb))
                {
                    WebService1 ws = new WebService1();
                    Userdata userdataRes = ws.login(usernameTb, passwordTb);

                    if (userdataRes.id != 0 && userdataRes.loguedAs.Equals("DOCTOR"))
                    {

                        createCoockie("id", userdataRes.id.ToString());
                        HttpCookie userInfo = new HttpCookie("doctor");
                        userInfo["doctor"] = "true";
                        userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                        Response.Cookies.Add(userInfo);
                        FormsAuthentication.RedirectFromLoginPage(usernameTb, false);
                        Response.Redirect(Page.ResolveClientUrl("./doctor/Doctor.aspx"));
                    }
                    else if (userdataRes.id != 0 && userdataRes.loguedAs.Equals("PACIENT"))
                    {

                        createCoockie("id", userdataRes.id.ToString());
                        HttpCookie userInfo = new HttpCookie("doctor");
                        userInfo["doctor"] = "false";
                        userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                        Response.Cookies.Add(userInfo);
                        FormsAuthentication.RedirectFromLoginPage(usernameTb, false);
                        Response.Redirect(Page.ResolveClientUrl("./patient/patient.aspx?id=" + userdataRes.id.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        void createCoockie(string name, string value)
        {
            HttpCookie userInfo = new HttpCookie(name);
            userInfo[name] = value;
            userInfo.Expires.Add(new TimeSpan(0, 10, 0));
            Response.Cookies.Add(userInfo);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }
}