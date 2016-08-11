using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using System.Data;
using System.Text;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        SiteUser s;
// Validates they entered something in textbox
        if (!string.IsNullOrEmpty(txtcustUser.Text) && !string.IsNullOrEmpty(txtcustPassword.Text))
        {
            s = new SiteUser(txtcustUser.Text.Trim(), txtcustPassword.Text.Trim());
            List<SiteUser> Users = new List<SiteUser>();
        }
        else
        {
            lblError.Text = "**Please enter a username and password!**";
            return;
        }

// Checks if returned a user
        if (!string.IsNullOrEmpty(s.UserID.ToString()))
        {
// Use .NET built in security system to set the UserID 
//within a client-side Cookie
            FormsAuthenticationTicket t =
                new FormsAuthenticationTicket(
                    1,
                    s.UserID.ToString(),
                    DateTime.Now,
                    DateTime.Now.AddDays(1),
                    true,
                    s.UserRole.ToString());

//For security reasons we may hash the cookies
            string encryptedTicked = FormsAuthentication.Encrypt(t);
            HttpCookie c = new HttpCookie(
                FormsAuthentication.FormsCookieName, encryptedTicked);
            Response.Cookies.Add(c);

//set the username to a client side cookie for future reference
            string strFullName = string.Concat(
                s.FName, " ",
                s.LName);

            StringBuilder sb = new StringBuilder();
            sb.Append(s.FName);
            sb.Append(" ");
            sb.Append(s.LName);
            Session["FullName"] = sb.ToString();
            HttpCookie ckGreeting = new HttpCookie("FullName");
            ckGreeting.Value = sb.ToString();// strFullName;
            ckGreeting.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(ckGreeting);
            // Redirect browser back to home page
            Response.Redirect("~/Home");
        }
        else
        {
//or else display the failed login message
            lblError.Text = "**Login Failed!**";
        }
    }



    protected void btnForgotPass_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForgotPassword.aspx");
    }




    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}