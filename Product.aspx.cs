using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Product : System.Web.UI.Page
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
        if (RouteData.Values["ProductID"] != null)
        {

            //- request.QueryString gets items from the query string
            //- convert the query string to the proper data type
            intID = Int32.Parse(RouteData.Values["ProductID"].ToString());
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
            btnprodUpdate.Text = "Update";
            btnaddToCart.Visible = true;

            SiteProduct sr = new SiteProduct(intID);

            if (sr != null)
            {
                txtprodName.Text = sr.ProductName;
                txtprodDescription.Text = sr.ProductDesc;
                txtprodSKU.Text = sr.ProductSKU;
                DDLCategory.SelectedValue = (sr.CategoryID).ToString();
                txtProdPrice.Text = (sr.ProductPrice).ToString();
                CBprodActive.Checked = sr.ProductIsActive;
            }

        }
        else
        {
            btnprodUpdate.Text = "Add";
            btnaddToCart.Visible = false;

        }
    }

    // Update ot Adds Customer
    protected void btnprodUpdate_Click(object sender, EventArgs e)
    {
        SiteProduct sr;
        if (RouteData.Values["ProductID"] != null)
        {
            sr = new SiteProduct(Convert.ToInt32(
                RouteData.Values["ProductID"].ToString()));
        }
        else
        {
            sr = new SiteProduct();
        }

        sr.ProductName = txtprodName.Text.Trim();
        sr.ProductDesc = txtprodDescription.Text.Trim();
        sr.ProductSKU = txtprodSKU.Text.Trim();
        sr.CategoryID = Convert.ToInt32(DDLCategory.SelectedValue);
        sr.ProductPrice = Convert.ToDecimal(txtProdPrice.Text);
        sr.ProductIsActive = Convert.ToBoolean(CBprodActive.Checked.ToString());


        if (sr.ProductID > 0)
        {
            if (SiteProduct.UpdateCustomer(sr))
            {
                Response.Redirect("~/Store/Admin/Products");
            }
        }
        else
        {

            if (SiteProduct.InserProduct(sr))
            {
                Response.Redirect("~/Store/Admin/Products");

            }

            else
            {
                lblMessage.Text = "**Product Insert Failed!**";
            }
        }
    }


//Cancels and redirects to the Products page
    protected void btnprodCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Products.aspx");
    }

//Displays dropdown with no value selected
    protected void DDLCategory_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DDLCategory.Items.Insert(0, "");
        }
    }
    protected void btnaddToCart_Click(object sender, EventArgs e)
    {
        ArrayList AL  = new ArrayList();

       if (Session["Cart"] != null)
       {
           AL = (ArrayList)Session["Cart"];
       }

       AL.Add(RouteData.Values["ProductID"]);
       Session["Cart"] = AL;
       Response.Redirect("~/Products.aspx");
       
    }
}