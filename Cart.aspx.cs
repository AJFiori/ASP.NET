using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
//Array List to add multiple to the cart
        ArrayList AL = (ArrayList)Session["Cart"];
        StringBuilder sb = new StringBuilder();

        if (AL != null)
        { 
            for(int i = 0; i < AL.Count; i++)
            {
                sb.Append(AL[i]);
                sb.Append(",");
            }

            sb.Length--;
            lbloutPut.Text = sb.ToString();
            lbloutPut.Visible = false;
        } 
    }

//Check out button
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
//Grabs all info from the session 
        if (Session["Cart"]!= null )
        {
            SiteCustomer customer = new SiteCustomer(Convert.ToInt32(User.Identity.Name));
            SiteOrder Order = new SiteOrder();

            Order.UserID = Convert.ToInt32(customer.UserID).ToString();
            Order.OrderAdd1 = customer.UserAdd1;
            Order.OrderAdd2 = customer.UserAdd2;
            Order.OrderCity = customer.UserCity;
            Order.StateID = customer.StateID;
            Order.OrderZip = customer.UserZip;

//If the order is true it will insert into the database and output a message
        if (SiteOrder.InsertOrder(Order))
            {
                lblMessage.Text = " Your Order Was Sucessful!";
                gvCart.Visible = false;
                Session.Contents.Remove("Cart");
                Session.Contents.Remove("Cart");
            }
        }
    }

//Empty button
    protected void btnEmpty_Click(object sender, EventArgs e)
    {
        ArrayList AL = (ArrayList)Session["Cart"];

//If there is items in the cart it will push out a message and clear the cart
        if (Session["Cart"] != null)
        {
            lblMessage.Text = "Your Cart Is Empty!";
            gvCart.Visible = false;
            Session.Contents.Remove("Cart");
            Session.Contents.Remove("Cart");
        }
    }
}