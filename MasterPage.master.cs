using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["FullName"] != null)
        {
            lblWelcome.Text = String.Concat("Welcome, ",
                Request.Cookies["FullName"].Value.ToString());

            lbtnLogIn.Visible = false;
            lbtnLogOut.Visible = true;
        }
        else
        {
            lblWelcome.Text = "Welcome, Guest";

            lbtnLogIn.Visible = true;
            lbtnLogOut.Visible = false;
        }
    }
    protected void lbtnLogIn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Logout");
    }
    protected void lbtnLogOut_Click(object sender, EventArgs e)
    {
        Request.Cookies["FullName"].Expires = DateTime.Now.AddDays(-1d);
        Response.Cookies.Add(Request.Cookies["FullName"]);
        Response.Redirect("~/Home");
        FormsAuthentication.SignOut();

       
    }
}
