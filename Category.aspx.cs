using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

//Checks if user is authenticated
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("~/Login.aspx");
        }
        int intID;

//user the request namespace to determine a query string value
        if (RouteData.Values["CategoryID"] != null)
        {

//- request.QueryString gets items from the query string
//- convert the query string to the proper data type
            intID = Int32.Parse(RouteData.Values["CategoryID"].ToString());
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
        if (intID != -1)
        {
            btncatUpdate.Text = "Update";

            SiteCategories sr = new SiteCategories(intID);

            if (sr != null)
            {
                txtcatName.Text = sr.CategoryName;
                txtcatDesc.Text = sr.CategoryDesc;
                CBcatActive.Checked = sr.CategoryIsActive;
            }

        }
        else
        {
            btncatUpdate.Text = "Add";

        }

    }

// Update or Adds Customer
    protected void btncatUpdate_Click(object sender, EventArgs e)
    {
        SiteCategories sr;
        if (RouteData.Values["CategoryID"] != null)
        {
            sr = new SiteCategories(Convert.ToInt32(
                RouteData.Values["CategoryID"].ToString()));
        }
        else
        {
            sr = new SiteCategories();
        }

        sr.CategoryName = txtcatName.Text.Trim();
        sr.CategoryDesc = txtcatDesc.Text.Trim();
        sr.CategoryIsActive = Convert.ToBoolean(CBcatActive.Checked.ToString());


        if (sr.CategoryID > 0)
        {
            if (SiteCategories.UpdateCustomer(sr))
            {
                Response.Redirect("~/Products/Categories");
            }
        }
        else
        {

            if (SiteCategories.InsertCategory(sr))
            {
                Response.Redirect("~/Products/Categories");

            }

            else
            {
                lblMessage.Text = "**Category Insert Failed!**";
            }
        }
    }

// Cancels and redirects to the Categories page
    protected void btncatCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Categories.aspx");
    }
}