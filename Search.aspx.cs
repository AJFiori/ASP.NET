using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Search : System.Web.UI.Page
{
//Connects to the database
            SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings
            ["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {}

//Search button
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
//When clicked will perform these Parameters  
            string str = "Select * From Products Where(ProductDesc Like '%' + @Search + '%')";
            SqlCommand xp = new SqlCommand(str, cn);
            xp.Parameters.Add("@Search", SqlDbType.NChar).Value = txtSearch.Text;

//Opens connection
            cn.Open();
            xp.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = xp;
            DataSet ds = new DataSet();
            da.Fill(ds, "ProductDesc");
            GridSearch.DataSource = ds;
            GridSearch.DataBind();
//Closes connection
            cn.Close();
        }

        catch (FormatException)
        {
            //lblMessage.Text = "No Results Found";
        }
    }
}