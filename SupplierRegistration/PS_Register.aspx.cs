using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupplierRegistration
{
    public partial class PS_Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Get_App_Detail();
        }
        protected void Get_App_Detail()
        {

            //Database Connect

            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;
            //ดึง AppID From Database.Form
            string EncodeID = Request.QueryString["id"];
            string EncodeToken = Request.QueryString["Token"];
            string AppID = null;
            string TokenID = null;
            string Pass = null;
            string Confirm_Pass = null;
            if (EncodeToken != null)
            {
                var base64Bytes = System.Convert.FromBase64String(EncodeToken);
                TokenID = System.Text.Encoding.UTF8.GetString(base64Bytes);

                if (EncodeID != null)
                {
                    base64Bytes = System.Convert.FromBase64String(EncodeID);
                    AppID = System.Text.Encoding.UTF8.GetString(base64Bytes);
                    try
                    {

                        hdfID.Value = AppID;
                        hdfToken.Value = TokenID;
                        if (conn.State == ConnectionState.Open)
                            conn.Close();

                        conn = new SqlConnection(Properties.Settings.Default.Conn);
                        conn.Open();
                        //Create IncidentNo
                        sql.Append("spRegisterCheck");
                        sqlcmd = new SqlCommand(sql.ToString(), conn);
                        sqlcmd.Parameters.AddWithValue("EmployeeID", AppID);
                        sqlcmd.Parameters.AddWithValue("Token", TokenID);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        oDa = new SqlDataAdapter(sqlcmd);
                        oDa.Fill(oDt);
                        if (oDt.Rows.Count > 0)
                        {

                         

                        }
                        else
                        {

                        }


                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        conn.Close();
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oRegis('error', 'Register Fail', 'Employee ID not found');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oRegis('error', 'Register Fail', 'Not found Token');", true);

            }

            //Context.Response.Write(base64Bytes);
            //string AppID = Request.QueryString["Id"];

        }

        protected void Register_Click(object sender, EventArgs e)
        {
            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;
            //กำหนดตัวแปร 
            string Pass = null;
            String Confirm_Pass = null;
            Pass = Password.Value;
            Confirm_Pass = Password_Confirm.Value;
            string AppID = Register_ID.Value;
            string TokenID = hdfToken.Value;
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                if (Pass == Confirm_Pass)
                {
                    sqlcmd = new SqlCommand(sql.ToString(), conn);
                    conn = new SqlConnection(Properties.Settings.Default.Conn);
                    conn.Open();
                    //Create IncidentNo
                    sql.Append("spRegister");
                    sqlcmd = new SqlCommand(sql.ToString(), conn);
                    sqlcmd.Parameters.AddWithValue("EmployeeID", AppID);
                    sqlcmd.Parameters.AddWithValue("Token", TokenID);
                    sqlcmd.Parameters.AddWithValue("Password", Pass);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    oDa = new SqlDataAdapter(sqlcmd);
                    oDa.Fill(oDt);
                    if (oDt.Rows.Count > 0)
                    {
                        if (oDt.Rows[0]["Result"].ToString() == "1")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oRegis('success', 'Success', 'Register Success');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Register Fail', 'Not found Account');", true);
                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Register Fail', 'Password invalid');", true);

                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
        }
    }




}
