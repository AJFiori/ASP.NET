using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Order : System.Web.UI.Page
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
        if (RouteData.Values["OrderID"] != null)
        {

//- request.QueryString gets items from the query string
//- convert the query string to the proper data type
            intID = Int32.Parse(RouteData.Values["OrderID"].ToString());
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
            btnorderUpdate.Text = "Update";

            SiteOrder sr = new SiteOrder(intID);

            if (sr != null)
            {
                txtuserID.Text = (sr.UserID).ToString();
                txtorderAddr1.Text = sr.OrderAdd1;
                txtorderAddr2.Text = sr.OrderAdd2;
                txtorderCity.Text = sr.OrderCity;
                DDLStates.SelectedValue = (sr.StateID).ToString();
                txtorderZip.Text = sr.OrderZip;
                
            }

        }
        else
        {
            btnorderUpdate.Text = "Add";

        }
    }
//Updates and Adds Orders
    protected void btnorderUpdate_Click(object sender, EventArgs e)
    {

        SiteOrder sr;
        if (RouteData.Values["OrderID"] != null)
        {
            sr = new SiteOrder(Convert.ToInt32(
                RouteData.Values["OrderID"].ToString()));
        }
        else
        {
            sr = new SiteOrder();
        }

        sr.UserID = txtuserID.Text.Trim();
        sr.OrderAdd1 = txtorderAddr1.Text.Trim();
        sr.OrderAdd2 = txtorderAddr2.Text.Trim();
        sr.OrderCity = txtorderCity.Text.Trim();
        sr.StateID = DDLStates.SelectedValue.Trim(); 
        sr.OrderZip = txtorderZip.Text.Trim();


        if (sr.OrderID > 0)
        {
            if (SiteOrder.UpdateCustomer(sr))
            {
                Response.Redirect("~/Store/Admin/Orders");
            }
        }
        else
        {

            if (SiteOrder.InsertOrder(sr))
            {
                Response.Redirect("~/Store/Admin/Orders");

            }

            else
            {
                lblMessage.Text = "**Order Insert Failed!**";
            }
        }
    }

//Cancels and redirects you to the Orders page
    protected void btnorderCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Orders.aspx");
    }

//DropDown List for States
    protected void DDLStates_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DDLStates.Items.Insert(0, "");
        }
    }
}