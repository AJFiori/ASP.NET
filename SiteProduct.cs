using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for SiteProduct
/// </summary>
public class SiteProduct
{

    #region constructors
    //default constructor
    public SiteProduct() { }

//Creates a DataTable for Product
    public SiteProduct(int id)
    {
        DataTable dt = new DataTable();
        dt = CreateProductID(id);
        if (dt.Rows.Count > 0)
        {
            this.ProductID = Convert.ToInt32(dt.Rows[0]["ProductID"].ToString());
            this.CategoryID = Convert.ToInt32(dt.Rows[0]["CategoryID"].ToString());
            this.ProductName = dt.Rows[0]["ProductName"].ToString();
            this.ProductDesc = dt.Rows[0]["ProductDesc"].ToString();
            this.ProductSKU = dt.Rows[0]["ProductSKU"].ToString();
            this.ProductPrice = Convert.ToDecimal(dt.Rows[0]["ProductPrice"].ToString());
            this.ProductIsActive = Convert.ToBoolean(dt.Rows[0]["ProductIsActive"].ToString());
        }
    }

    #endregion

    #region properties

    public int ProductID { get; set; }
    public int CategoryID { get; set; }
    public string ProductName { get; set; }
    public string ProductDesc { get; set; }
    public string ProductSKU { get; set; }
    public Decimal ProductPrice { get; set; }
    public bool ProductIsActive { get; set; }

    #endregion

    #region methods/functions

    private static DataTable CreateProductID(int id)
    {
        SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings
            ["ConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("spCreateProductID", cn);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
        // Add Parameters to Stored Procedure
        cmd.Parameters.Add("@ProductID", SqlDbType.VarChar).Value = id;
        try
        {
            //opens connection to database, most failures happen here
            //check connection string for proper settings
            cn.Open();
            //data adapter object is trasport link between data source and 
            //data destination
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //fill method, for multiple tables use dataset
            da.Fill(dt);
        }
        catch (Exception exc)
        {
            //just put here to make debugging easier, can look at error directly
            exc.ToString();
        }
        finally
        {
            //must always close connections
            cn.Close();
        }

        // Return the datatable
        return dt;
    }

    public static bool InserProduct(SiteProduct sr)
    {
        //declare return variable
        bool blnSuccess = false;

        //back to stored procedures :)
        //connection object - ConfigurationManager namespace allows for runtime 
        //access to web.config setting, specifically connection strings and key values

        SqlConnection cn = new SqlConnection
         (
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
         );

        SqlCommand cmd = new SqlCommand("spInserProduct", cn);

        // Mark the Command as a Stored Procedure
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Stored Procedure
        //Category Name, Description, SKU, & Price
        cmd.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = sr.ProductName;
        cmd.Parameters.Add("@ProductDesc", SqlDbType.VarChar).Value = sr.ProductDesc;
        cmd.Parameters.Add("@ProductSKU", SqlDbType.VarChar).Value = sr.ProductSKU;
        cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = sr.CategoryID;
        cmd.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = sr.ProductPrice;

        //IF role is active
        cmd.Parameters.Add("@ProductIsActive", SqlDbType.Bit).Value = sr.ProductIsActive;

        // Open the database connection and execute the command
        try
        {
            cn.Open();

            //This is not a query so just issue the command to execute the stored procedure
            cmd.ExecuteNonQuery();
            blnSuccess = true;
        }
        catch (Exception exc)
        {

            //if error,notify user
            exc.ToString();
            blnSuccess = false;
        }
        finally
        {
            cn.Close();
        }
        return blnSuccess;
    }
    public static bool UpdateCustomer(SiteProduct sr)
    {

        //declare return variable
        bool blnSuccess = false;

        //back to stored procedures :)
        //connection object - ConfigurationManager namespace allows for runtime 
        //access to web.config setting, specifically connection strings and key values

        SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("spUpdateProduct", cn);

        // Mark the Command as a Stored Procedure
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Stored Procedure
        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = sr.ProductID;
        cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = sr.CategoryID;

        //Category Name, Description, SKU, & Price
        cmd.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = sr.ProductName;
        cmd.Parameters.Add("@ProductDesc", SqlDbType.VarChar).Value = sr.ProductDesc;
        cmd.Parameters.Add("@ProductSKU", SqlDbType.VarChar).Value = sr.ProductSKU;
        cmd.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = sr.ProductPrice;

        //IF role is active
        cmd.Parameters.Add("@ProductIsActive", SqlDbType.Bit).Value = sr.ProductIsActive;

        // Open the database connection and execute the command
        try
        {
            cn.Open();

            //This is not a query so just issue the command to execute the stored procedure
            cmd.ExecuteNonQuery();
            blnSuccess = true;
        }
        catch (Exception exc)
        {

            //if error,notify user
            exc.ToString();
            blnSuccess = false;
        }
        finally
        {
            cn.Close();
        }
        return blnSuccess;
    }

    #endregion
}