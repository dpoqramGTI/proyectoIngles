using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        //WebService1 webServiceReference = new WebService1();
        //WebService1 ws = new WebService1();
        private bool ValidateUser(string userName, string passWord)
        {
            // Check for invalid userName.
            // userName must not be null and must be between 1 and 15 characters.
            if ((null == userName) || (0 == userName.Length) || (userName.Length > 15))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.");
                return false;
            }

            // Check for invalid passWord.
            // passWord must not be null and must be between 1 and 25 characters.
            if ((null == passWord) || (0 == passWord.Length) || (passWord.Length > 25))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of passWord failed.");

                return false;
            }

           
            
            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void add(Object sender, EventArgs e)
        {
            
            String usernameTb = addUsername.Text.ToString();
            String passwordTb = addPassword.Text.ToString();
            if (ValidateUser(usernameTb, passwordTb))
            {
               
            }
            Response.Write(addUsername);
            //string passwordMD5 = md5Encrypt(passwordTb);

        }
    }
}