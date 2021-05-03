using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.webServiceReference;
namespace WebApplication
{
    public partial class Home : System.Web.UI.Page
    {     
        protected void Page_Load(object sender, EventArgs e)
        {
            WebService1 ws = new WebService1();
        }
    }
}