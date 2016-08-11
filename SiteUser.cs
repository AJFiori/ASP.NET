using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for SiteUser
/// </summary>
public class SiteUser
{
    #region Constructors
    public SiteUser() { }

    //Creates a DataTable for User
    public SiteUser(string UserName, string Password)
    {
        DataTable dt = GetUserByCredentials(UserName, Password);
        if (dt.Rows.Count > 0)
        {
            this.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
            this.FName = dt.Rows[0]["UserFname"].ToString();
            this.LName = dt.Rows[0]["UserLname"].ToString();
            this.UserRole = dt.Rows[0]["UserRole"].ToString();
        }
    }
    #endregion

    #region Properties

//Gets and sets the Parameters 
    public int UserID { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public string UserRole { get; set; }
    #endregion

    #region Methods

//grabs the user credentials and tables 
    private static DataTable GetUserByCredentials(string UserName, string Password)
    {
        DataTable dt = new DataTable();
        
        SqlConnection cn = new SqlConnection
            (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        
        SqlCommand cmd = new SqlCommand("spUserLogin", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = UserName;
        cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = Password;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            cn.Open();
            da.Fill(dt);
        }
        catch (Exception ex)
        {

        }
        finally
        {
            cn.Close();
        }
        
        return dt;
    }
    #endregion
}