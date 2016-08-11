using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
/// <summary>
/// Summary description for SiteCategories
/// </summary>
public class SiteCategories
{
    #region constructors
    //default constructor
    public SiteCategories()
    {

    }

//Creates a DataTable for Categories
    public SiteCategories(int id)
    {
        DataTable dt = new DataTable();
        dt = GetCategoryByID(id);
        if (dt.Rows.Count > 0)
        {
            this.CategoryID = Convert.ToInt32(dt.Rows[0]["CategoryID"].ToString());
            this.CategoryName = dt.Rows[0]["CategoryName"].ToString();
            this.CategoryDesc = dt.Rows[0]["CategoryDesc"].ToString();
            this.CategoryIsActive = Convert.ToBoolean(dt.Rows[0]["CategoryIsActive"].ToString());
        }
    }

    #endregion

    #region properties

//Gets and sets the Parameters 
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDesc { get; set; }
    public bool CategoryIsActive { get; set; }

    #endregion

    #region methods/functions

    private static DataTable GetCategoryByID(int id)
    {
        SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings
            ["ConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("spGetCategoryByID", cn);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
// Add Parameters to Stored Procedure
        cmd.Parameters.Add("@categoryid", SqlDbType.VarChar).Value = id;
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

    public static bool InsertCategory(SiteCategories sr)
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

        SqlCommand cmd = new SqlCommand("spInsertCategory", cn);

        // Mark the Command as a Stored Procedure
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Stored Procedure
        //Category Name & Description
        cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = sr.CategoryName;
        cmd.Parameters.Add("@CategoryDesc", SqlDbType.VarChar).Value = sr.CategoryDesc;

        //IF role is active
        cmd.Parameters.Add("@CategoryIsActive", SqlDbType.Bit).Value = sr.CategoryIsActive;

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
    public static bool UpdateCustomer(SiteCategories sr)
    {

        //declare return variable
        bool blnSuccess = false;

        //back to stored procedures :)
        //connection object - ConfigurationManager namespace allows for runtime 
        //access to web.config setting, specifically connection strings and key values

        SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("spUpdateCategory", cn);

        // Mark the Command as a Stored Procedure
        cmd.CommandType = CommandType.StoredProcedure;

        // Add Parameters to Stored Procedure
        cmd.Parameters.Add("@categoryid", SqlDbType.Int).Value = sr.CategoryID;

        //Category Name & Description
        cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = sr.CategoryName;
        cmd.Parameters.Add("@CategoryDesc", SqlDbType.VarChar).Value = sr.CategoryDesc;

        //IF role is active
        cmd.Parameters.Add("@CategoryIsActive", SqlDbType.Bit).Value = sr.CategoryIsActive;

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