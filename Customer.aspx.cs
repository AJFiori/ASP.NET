using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Customer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

//Checks if user is authenticated
        if(!Request.IsAuthenticated)
        {
            Response.Redirect("~/Login.aspx");
        }
        int intID;

//user the request namespace to determine a query string value
        if(RouteData.Values["UserID"] !=null)
        {

//- request.QueryString gets items from the query string
//- convert the query string to the proper data type
            intID = Int32.Parse(RouteData.Values["UserID"].ToString());
        }
        else
        {
// - or set it to a number that will never be a valid value (good for conditional population of the data on the page,
            intID = -1;
        }

//- for example if id <> -1 fill customer record or if id = -1, prepare form for add record functionality

        if (!IsPostBack)
        {

//bind form data objects procedure call, made on first visit to page
            BindData(intID);
        }
    }

    private void BindData(int intID)
    {

//render page according to query string variable
//if in edit mode
        if(intID != -1)
        {
            btncustUpdate.Text = "Update";

            SiteCustomer sr = new SiteCustomer(intID);

            if(sr!=null)
            {
                txtcustFirst.Text = sr.UserFname;
                txtcustLast.Text = sr.UserLname;
                txtAddr1.Text = sr.UserAdd1;
                txtAddr2.Text = sr.UserAdd2;
                txtCity.Text = sr.UserCity;
                DDLState.SelectedValue = sr.StateID;
                txtcustZip.Text = sr.UserZip;
                txtcustUser.Text = sr.UserName;
                txtcustPassword.Text = sr.UserPassword;
                txtEmail.Text = sr.UserEmail;
                txtPhone.Text = sr.UserPhone;
                CBcustActive.Checked = sr.UserIsActive;
            }

        }
        else
        {
            btncustUpdate.Text = "Add";

        }
    
    }

// Update ot Adds Customer
    protected void btncustUpdate_Click(object sender, EventArgs e)
    {


        SiteCustomer sr;
        if (RouteData.Values["UserID"] != null)
        {
            sr = new SiteCustomer(Convert.ToInt32(
                RouteData.Values["UserID"].ToString()));
        }
        else
        {
            sr = new SiteCustomer(); 
        }     

       sr.UserFname = txtcustFirst.Text.Trim();
       sr.UserLname = txtcustLast.Text.Trim();
       sr.UserAdd1 = txtAddr1.Text.Trim();              
       sr.UserAdd2 = txtAddr2.Text.Trim();                
       sr.UserCity = txtCity.Text.Trim();                  
       sr.StateID = DDLState.SelectedValue.Trim();        
       sr.UserZip  = txtcustZip.Text.Trim();        
       sr.UserName = txtcustUser.Text.Trim();              
       sr.UserPassword  = txtcustPassword.Text.Trim();
       sr.UserEmail  = txtEmail.Text.Trim();                  
       sr.UserPhone  = txtPhone.Text.Trim();
       sr.UserIsActive = Convert.ToBoolean(CBcustActive.Checked.ToString());


       if (sr.UserID > 0)
       {
            if(SiteCustomer.UpdateCustomer(sr))
            {
                Response.Redirect("~/Store/Admin/Customers");
            }
       }
       else
       {

           if(SiteCustomer.InsertUserID(sr))
           {
               Response.Redirect("~/Store/Admin/Customers");

           }

           else
           {
               lblMessage.Text = "Customer Insert Failed!";
           }
       }
    }

    protected void btncustCancel_Click(object sender, EventArgs e)
    {

 //cancel button redirects to Customers page
        Response.Redirect("~/Customers.aspx");
    }

    protected void DDLState_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DDLState.Items.Insert(0, "");
        }
    }
}