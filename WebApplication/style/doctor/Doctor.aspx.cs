using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cookie = Request.Cookies.Get("doctor").Value;
            if (cookie == "doctor=false")
            {
                Response.Redirect(Page.ResolveClientUrl("patient/patient.aspx"));
            }
            if (cookie != "doctor=true")
            {
                Response.Redirect(Page.ResolveClientUrl("../Login.aspx"));
            }
        }
    }
}