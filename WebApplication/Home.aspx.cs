using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage master = (MasterPage)this.Master;
            //HtmlGenericControl hd = (HtmlGenericControl)master.FindControl("eee");
            //hd.InnerText = "Holas"
        }
    }
}