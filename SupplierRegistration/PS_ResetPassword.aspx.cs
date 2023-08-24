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
    public partial class PS_ResetPassword : System.Web.UI.Page
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
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oReset('error', 'Register Fail', 'User not found');", true);

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
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oReset('error', 'Reset Fail', 'Employee ID not found');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oReset('error', 'Reset Fail', 'Token not found ');", true);
            }

            //Context.Response.Write(base64Bytes);
            //string AppID = Request.QueryString["Id"];

        }

        protected void ResetPassword_Click(object sender, EventArgs e)
        {
            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;
            //กำหนดตัวแปร 
            string Reset_ID = User_ID.Value;
            string ResetPassword = Register_Password.Value;
            string Confirm = ConfirmPassword.Value;
            string AppID = hdfID.Value;
            string TokenID = hdfToken.Value;

            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                //กำหนดตัวแปร
                if (ResetPassword == Confirm)
                {
                    if (Reset_ID != "")
                    {


                        sqlcmd = new SqlCommand("spRegister", conn);
                        sqlcmd.Parameters.AddWithValue("EmployeeID", Reset_ID);
                        sqlcmd.Parameters.AddWithValue("Password", ResetPassword);
                        sqlcmd.Parameters.AddWithValue("Token", TokenID);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        oDa = new SqlDataAdapter(sqlcmd);
                        oDa.Fill(oDt);
                        if (oDt.Rows.Count > 0 && Reset_ID != ResetPassword)
                        {   //ตรวจค่าที่ส่งมาว่าตรงตามเงื่อนไขหรือไม่
                            if (Reset_ID == AppID)
                            {
                                if (ResetPassword == Confirm)
                                {
                                    if (oDt.Rows[0]["Result"].ToString() == "1")
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oReset('success', 'Success', 'Reset Password success, Value has inserted.');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Wrong', 'Reset Password Fail, Value has insert fail.');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Wrong', 'Reset Password Fail, Password invalid.');", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Wrong', 'Reset Password Fail, Wrong Employee ID.');", true);

                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Wrong', 'Reset Password Fail, ID and Password not same.');", true);

                        }
                        //URL Script --> .Master
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Wrong', 'Password invalid');", true);
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