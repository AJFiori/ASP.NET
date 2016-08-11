using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for Customer
/// </summary>
public class SiteCustomer
{
	  #region constructors
    //default constructor
	public SiteCustomer()
	{

    }

//Creates a DataTable for Customer
    public SiteCustomer(int id)
    {
        DataTable dt = new DataTable();
        dt = GetUsersbyID(id);
        if(dt.Rows.Count>0)
        {
            this.UserID = Convert.ToInt32(dt.Rows[0]["UserID"].ToString());
            this.UserFname = dt.Rows[0]["UserFname"].ToString();
            this.UserLname = dt.Rows[0]["UserLname"].ToString();
            this.UserAdd1 = dt.Rows[0]["UserAdd1"].ToString();
            this.UserAdd2 = dt.Rows[0]["UserAdd2"].ToString();
            this.UserCity = dt.Rows[0]["UserCity"].ToString();
            this.StateID = dt.Rows[0]["StateID"].ToString();
            this.UserZip = dt.Rows[0]["UserZip"].ToString();
            this.UserName = dt.Rows[0]["UserName"].ToString();
            this.UserPassword = dt.Rows[0]["UserPassword"].ToString();
            this.UserEmail = dt.Rows[0]["UserEmail"].ToString();
            this.UserPhone = dt.Rows[0]["UserPhone"].ToString();
            this.UserRole = dt.Rows[0]["UserRole"].ToString();
            this.UserIsActive = Convert.ToBoolean(dt.Rows[0]["UserIsActive"].ToString());
        }
    }

    #endregion

    #region properties

//Gets and sets the Parameters 
    public int UserID { get; set; }
    public string UserFname { get; set; }
    public string UserLname { get; set; }
    public string UserAdd1 { get; set; }
    public string UserAdd2 { get; set; }
    public string UserCity { get; set; }
    public string StateID { get; set; }
    public string UserZip { get; set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public string UserEmail{ get; set; }
    public string UserPhone{ get; set; }
    public string UserRole { get; set; }
    public bool UserIsActive { get; set; }

    #endregion

    #region methods/functions

    private static DataTable GetUsersbyID(int id)
    {
        SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings
            ["ConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("spGetUsersbyID", cn);
        DataTable dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
        // Add Parameters to Stored Procedure
        cmd.Parameters.Add("@UserID", SqlDbType.VarChar).Value = id;
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

    public static bool InsertUserID(SiteCustomer sr)
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

        SqlCommand cmd = new SqlCommand("spInsertUser", cn);

        // Mark the Command as a Stored Procedure
        cmd.CommandType = CommandType.StoredProcedure;

// Add Parameters to Stored Procedure
//Name
        cmd.Parameters.Add("@UserFname", SqlDbType.VarChar).Value = sr.UserFname;
        cmd.Parameters.Add("@UserLname", SqlDbType.VarChar).Value = sr.UserLname;

//Adress
        cmd.Parameters.Add("@UserAdd1", SqlDbType.VarChar).Value = sr.UserAdd1;
        cmd.Parameters.Add("@UserAdd2", SqlDbType.VarChar).Value = sr.UserAdd2;
        cmd.Parameters.Add("@UserCity", SqlDbType.VarChar).Value = sr.UserCity;
        cmd.Parameters.Add("@StateID", SqlDbType.VarChar).Value = sr.StateID;
        cmd.Parameters.Add("@UserZip", SqlDbType.VarChar).Value = sr.UserZip;
        cmd.Parameters.Add("@UserEmail", SqlDbType.VarChar).Value = sr.UserEmail;

//User login info 
        cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = sr.UserName;
        cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = sr.UserPassword;
        cmd.Parameters.Add("@UserPhone", SqlDbType.VarChar).Value = sr.UserPhone;
        cmd.Parameters.Add("@UserRole", SqlDbType.VarChar).Value = "Customer";

//IF role is active
        cmd.Parameters.Add("@UserIsActive", SqlDbType.Bit).Value = sr.UserIsActive;

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
    public static bool UpdateCustomer(SiteCustomer sr)
    {
        
//declare return variable
        bool blnSuccess = false;

//back to stored procedures :)
//connection object - ConfigurationManager namespace allows for runtime 
//access to web.config setting, specifically connection strings and key values
        
        SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("spUpdateUser", cn);
       
// Mark the Command as a Stored Procedure
        cmd.CommandType = CommandType.StoredProcedure;

// Add Parameters to Stored Procedure
        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = sr.UserID;
   
//Name
        cmd.Parameters.Add("@UserFname", SqlDbType.VarChar).Value = sr.UserFname;
        cmd.Parameters.Add("@UserLname", SqlDbType.VarChar).Value = sr.UserLname;

//Adress
        cmd.Parameters.Add("@UserAdd1", SqlDbType.VarChar).Value = sr.UserAdd1;
        cmd.Parameters.Add("@UserAdd2", SqlDbType.VarChar).Value = sr.UserAdd2;
        cmd.Parameters.Add("@UserCity", SqlDbType.VarChar).Value = sr.UserCity;
        cmd.Parameters.Add("@StateID", SqlDbType.VarChar).Value = sr.StateID;
        cmd.Parameters.Add("@UserZip", SqlDbType.VarChar).Value = sr.UserZip;
        cmd.Parameters.Add("@UserEmail", SqlDbType.VarChar).Value = sr.UserEmail;

//User login info 
        cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = sr.UserName;
        cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = sr.UserPassword;
        cmd.Parameters.Add("@UserPhone", SqlDbType.VarChar).Value = sr.UserPhone;
        cmd.Parameters.Add("@UserRole", SqlDbType.VarChar).Value = sr.UserRole;

//IF role is active
        cmd.Parameters.Add("@UserIsActive", SqlDbType.Bit).Value = sr.UserIsActive;

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