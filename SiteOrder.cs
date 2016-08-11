using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for SiteOrder
/// </summary>
public class SiteOrder
{
    #region constructors
//default constructor
	public SiteOrder(){}

//Creates a DataTable for Order
    public SiteOrder(int id)
    {
        DataTable dt = new DataTable();
        dt = GetOrderByID(id);
        if (dt.Rows.Count > 0)
        {
            
            this.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"].ToString());
            this.UserID = dt.Rows[0]["UserID"].ToString();
            this.StatusID = dt.Rows[0]["StatusID"].ToString();
            this.OrderDate = dt.Rows[0]["OrderDate"].ToString();
            this.OrderAdd1 = dt.Rows[0]["OrderAdd1"].ToString();
            this.OrderAdd2 = dt.Rows[0]["OrderAdd2"].ToString();
            this.OrderCity = dt.Rows[0]["OrderCity"].ToString();
            this.StateID = dt.Rows[0]["StateID"].ToString();
            this.OrderZip = dt.Rows[0]["OrderZip"].ToString();
            this.OrderLastUpdate = dt.Rows[0]["OrderLastUpdate"].ToString();
            
        }
    }

    #endregion

    #region properties

//Gets and sets the Parameters 
    public int OrderID { get; set; }
    public string UserID { get; set; }
    public string StatusID { get; set; }
    public string OrderDate { get; set; }
    public string OrderAdd1 { get; set; }
    public string OrderAdd2 { get; set; }
    public string OrderCity { get; set; }
    public string StateID { get; set; }
    public string OrderZip { get; set; }
    public string OrderLastUpdate { get; set; }

    #endregion
    #region methods/functions

    private static DataTable GetOrderByID(int id)
    {
        SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings
            ["ConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("spGetOrderByID", cn);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
// Add Parameters to Stored Procedure
        cmd.Parameters.Add("@OrderID", SqlDbType.VarChar).Value = id;
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

    public static bool InsertOrder(SiteOrder sr)
    {
        //declare return variable
        bool Success = true;

//back to stored procedures :)
//connection object - ConfigurationManager namespace allows for runtime 
//access to web.config setting, specifically connection strings and key values

        SqlConnection cn = new SqlConnection
         (
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString
         );

        SqlCommand cmd = new SqlCommand("spInsertOrder", cn);

        // Mark the Command as a Stored Procedure
        cmd.CommandType = CommandType.StoredProcedure;

// Add Parameters to Stored Procedure
         cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = sr.UserID;

        cmd.Parameters.Add("@OrderDate", SqlDbType.Date).Value = DateTime.Today;
        cmd.Parameters.Add("@OrderLastUpdate", SqlDbType.Date).Value = DateTime.Today;

//Order Status & Address
        cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = 1;
        cmd.Parameters.Add("@OrderAdd1", SqlDbType.VarChar).Value = sr.OrderAdd1;
        cmd.Parameters.Add("@OrderAdd2", SqlDbType.VarChar).Value = sr.OrderAdd2;
        cmd.Parameters.Add("@OrderCity", SqlDbType.VarChar).Value = sr.OrderCity;
        cmd.Parameters.Add("@StateID", SqlDbType.VarChar).Value = sr.StateID;
        cmd.Parameters.Add("@OrderZip", SqlDbType.VarChar).Value = sr.OrderZip;

//OutPut
        SqlParameter outPutParameter = new SqlParameter();
        outPutParameter.ParameterName = "@NewOrderID";
        outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
        outPutParameter.Direction = System.Data.ParameterDirection.Output;
        cmd.Parameters.Add(outPutParameter);

// Open the database connection and execute the command
        try
        {
            cn.Open();

//This is not a query so just issue the command to execute the stored procedure
            cmd.ExecuteNonQuery();
            Success = true;
            sr.OrderID = (int)outPutParameter.Value;
            
            
        }
        catch (Exception exc)
        {

            //if error,notify user
            exc.ToString();
            Success = false;
        }
        finally
        {
            cn.Close();
        }
        return Success;
    }
    public static bool UpdateCustomer(SiteOrder sr)
    {

//declare return variable
        bool blnSuccess = false;

//back to stored procedures :)
//connection object - ConfigurationManager namespace allows for runtime 
//access to web.config setting, specifically connection strings and key values

        SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("spUpdateOrder", cn);

// Mark the Command as a Stored Procedure
        cmd.CommandType = CommandType.StoredProcedure;

// Add Parameters to Stored Procedure
        
        cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = sr.OrderID;
        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = sr.UserID;

        cmd.Parameters.Add("@OrderDate", SqlDbType.Date).Value = sr.OrderDate;
        cmd.Parameters.Add("@OrderLastUpdate", SqlDbType.Date).Value = DateTime.Today;

//Order Status & Address
        cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = sr.StatusID;
        cmd.Parameters.Add("@OrderAdd1", SqlDbType.VarChar).Value = sr.OrderAdd1;
        cmd.Parameters.Add("@OrderAdd2", SqlDbType.VarChar).Value = sr.OrderAdd2;
        cmd.Parameters.Add("@OrderCity", SqlDbType.VarChar).Value = sr.OrderCity;
        cmd.Parameters.Add("@StateID", SqlDbType.VarChar).Value = sr.StateID;
        cmd.Parameters.Add("@OrderZip", SqlDbType.VarChar).Value = sr.OrderZip;

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