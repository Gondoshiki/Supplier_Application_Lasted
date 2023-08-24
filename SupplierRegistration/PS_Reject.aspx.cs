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
    public partial class PS_Reject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Get_App_Detail();
            Get_Transaction();
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
            string EncodeID = Request.QueryString["ID"];
            string AppID = null;
            if (Request.Cookies.Get("EmployeeId") == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Employee ID is Null, Please Login First.');", true);
            }
            else
            {


                //string AppID = Request.QueryString["Id"];
                try
                {

                    if (EncodeID != null)
                    {
                        if (EncodeID.Length != 16)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oList('error', 'Error', 'ID is Complete, Please try again.');", true);

                        }
                        else
                        {
                            var base64Bytes = System.Convert.FromBase64String(EncodeID);
                            //Context.Response.Write(base64Bytes);
                            AppID = System.Text.Encoding.UTF8.GetString(base64Bytes);

                            if (conn.State == ConnectionState.Open)
                                conn.Close();

                            conn = new SqlConnection(Properties.Settings.Default.Conn);
                            conn.Open();
                            sql.Append("spGetAppDetail");
                            sqlcmd = new SqlCommand(sql.ToString(), conn);
                            sqlcmd.Parameters.AddWithValue("Id", AppID);
                            sqlcmd.CommandType = CommandType.StoredProcedure;
                            oDa = new SqlDataAdapter(sqlcmd);
                            oDa.Fill(oDt);
                            if (oDt.Rows.Count > 0)
                            {
                                string GetStat = oDt.Rows[0]["Status"].ToString();
                                if (GetStat == "P")
                                {
                                    Response.Redirect("Draft.aspx?id=" + EncodeID, false);
                                }
                                else if (GetStat == "R")
                                {
                                    Response.Redirect("PS_Revise.aspx?id=" + EncodeID, false);
                                }
                                else if (GetStat == "D")
                                {
                                    Response.Redirect("PS_Detail.aspx?id=" + EncodeID, false);
                                }


                            }
                            else
                            {
                                StringBuilder sql1 = new StringBuilder();
                                SqlConnection conn1 = new SqlConnection();
                                SqlCommand sqlcmd1 = new SqlCommand();
                                DataTable oDt1 = new DataTable();
                                SqlDataAdapter oDa1;
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();

                                conn1 = new SqlConnection(Properties.Settings.Default.Conn);
                                conn1.Open();
                                //Create IncidentNo
                                sql1.Append("spGetHistoryDetail");
                                sqlcmd1 = new SqlCommand(sql1.ToString(), conn1);
                                sqlcmd1.Parameters.AddWithValue("Id", AppID);
                                sqlcmd1.CommandType = CommandType.StoredProcedure;
                                oDa1 = new SqlDataAdapter(sqlcmd1);
                                oDa1.Fill(oDt1);
                                if (oDt1.Rows.Count > 0)
                                {
                                    // Alter Data แก้ไขข้อมูล Front = Back
                                    App_ID.Text = oDt1.Rows[0]["App_ID"].ToString();
                                    Vendor_Name.Value = oDt1.Rows[0]["Vendor_Name"].ToString();
                                    Vendor_PIC.Value = oDt1.Rows[0]["Vendor_PIC"].ToString();
                                    Email.Value = oDt1.Rows[0]["Email"].ToString();
                                    Comment.Text = oDt1.Rows[0]["Comment"].ToString();
                                    GmMail.Value = oDt1.Rows[0]["CC"].ToString();
                                    // Status Check And Redirect page.

                                    string GetStat1 = oDt1.Rows[0]["Status"].ToString();
                                    if (GetStat1 == "D")
                                    {
                                        Response.Redirect("Draft.aspx?id=" + EncodeID, false);
                                    }
                                    else if (GetStat1 == "R")
                                    {
                                        Response.Redirect("PS_Revise.aspx?id=" + EncodeID, false);
                                    }
                                    else if (GetStat1 == "P")
                                    {
                                        Response.Redirect("PS_Detail.aspx?id=" + EncodeID, false);
                                    }
                                    else if (GetStat1 == "F")
                                    {
                                        Response.Redirect("PS_Finish.aspx?id=" + EncodeID, false);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Employee ID is Null, Please Login First.');", true);
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
        protected void Get_Transaction()
        {

            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataReader oDr;
            string EncodeID = Request.QueryString["ID"];
            string AppID = null;
            if (EncodeID != null)
            {
                var base64Bytes = System.Convert.FromBase64String(EncodeID);
                //Context.Response.Write(base64Bytes);
                AppID = System.Text.Encoding.UTF8.GetString(base64Bytes);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Check fail, ID is null.');", true);
            }
            if (Request.Cookies.Get("EmployeeId") == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Check fail, EmployeeID is null.');", true);
            }
            else
            {


                //string AppID = Request.QueryString["Id"];
                try
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn = new SqlConnection(Properties.Settings.Default.Conn);
                    conn.Open();

                    sql.Append("spGetTransaction");
                    sqlcmd = new SqlCommand(sql.ToString(), conn);
                    //Add parameters ไปยัง*ตัวแปร*ใน Store
                    sqlcmd.Parameters.AddWithValue("Id", AppID);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    oDr = sqlcmd.ExecuteReader();


                    while (oDr.Read())
                    {
                        // Alter Data
                        DateTime Date1 = DateTime.Parse(oDr["Timestamp"].ToString());
                        string Date2 = Date1.ToString("dd/MM/yyyy HH:mm:ss");

                        //Transaction "<p> New Line"
                        Transaction.Text += "<p>" + Date2.ToString() + " " + oDr["Detail"].ToString() + "</p>";

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
}