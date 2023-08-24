using SupplierRegistration.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupplierRegistration
{
    public partial class PS_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Get_App_Detail();
            Get_Transaction();

        }


        protected void Revise_Click(object sender, EventArgs e)
        {
            //Database Connect
            //oDT = DataTable
            //oDr = DataReader
            //oDa = DataAdapter
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataReader oDr;
            SqlDataAdapter oDa;
            string Mail = Request.Form[Email_Revise.UniqueID];
            string Comment = Comment_Revise.Value;


            //Get AppID
            //string AppID = Request.QueryString["ID"];

            string EncodeID = Request.QueryString["ID"];
            string AppID = null;

            string[] words = null;
            string oMail = null;

            string[] checkMailFormat = null;
            bool boolGmMail = CheckEmailFormat(Mail);
            bool boolMail = false;
            bool boolCheckMail = true;

            words = Mail.Split(';');

            //CheckFormatEmail#1
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != "")
                {
                    boolMail = CheckEmailFormat(words[i].Trim());
                    if (boolMail == false)//Wrong Format
                    {
                        boolCheckMail = false;
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Sent Failed, E-mail Wrong Format.');", true);
                    }
                    else //Correct Format
                    {
                        checkMailFormat = words[i].Split(' ');
                        foreach (string mail in checkMailFormat) //Loop CheckMailFormat
                        {
                            if (mail != "")
                            {
                                boolMail = CheckEmailFormat(mail); //CheckFormatEmail#2
                                //Set Email format
                                if (boolMail == true) //True Format
                                {
                                    if (words[i].Contains(";") == false)
                                    {
                                        oMail = oMail + words[i].Trim() + "; ";
                                    }
                                    else
                                    {
                                        oMail = oMail + words[i].Trim();
                                    }
                                }
                                else //False Format
                                {
                                    boolCheckMail = false;
                                    //Display Error
                                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Sent Failed, Email is invalid.');", true);
                                }
                            }

                        }
                    }


                }

            }
            if (EncodeID != null && EncodeID != "undefined")
            {
                var base64Bytes = System.Convert.FromBase64String(EncodeID);
                //Context.Response.Write(base64Bytes);
                AppID = System.Text.Encoding.UTF8.GetString(base64Bytes);
            }
            if (boolCheckMail != false && AppID != null)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                //Create IncidentNo
                sql.Append("spGetHistoryDetail");
                sqlcmd = new SqlCommand(sql.ToString(), conn);

                sqlcmd.Parameters.AddWithValue("Id", AppID);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                oDa = new SqlDataAdapter(sqlcmd);
                oDa.Fill(oDt);
                if (oDt.Rows.Count > 0)
                {
                    // Status Check And Redirect page.
                    string GetStat = oDt.Rows[0]["Status"].ToString();
                    if (GetStat == "F")
                    {
                        Response.Redirect("PS_Finish.aspx?id=" + EncodeID, false);
                    }
                    else if (GetStat == "J")
                    {
                        Response.Redirect("PS_Reject.aspx?id=" + EncodeID, false);
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
                    sql1.Append("spGetAppDetail");
                    sqlcmd1 = new SqlCommand(sql1.ToString(), conn1);
                    sqlcmd1.Parameters.AddWithValue("Id", AppID);
                    sqlcmd1.CommandType = CommandType.StoredProcedure;
                    oDa1 = new SqlDataAdapter(sqlcmd1);
                    oDa1.Fill(oDt1);
                    if (oDt1.Rows.Count > 0)
                    {
                        string GetStat = oDt1.Rows[0]["Status"].ToString();
                        if (GetStat == "D")
                        {
                            Response.Redirect("Draft.aspx?id=" + EncodeID, false);
                        }
                        else if (GetStat == "R")
                        {
                            Response.Redirect("PS_Revise.aspx?id=" + EncodeID, false);
                        }
                        else
                        {
                            try
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();

                                conn = new SqlConnection(Properties.Settings.Default.Conn);
                                conn.Open();
                                //กำหนดตัวแปร
                                string logonName = Request.Cookies.Get("FullName").Value;
                                var Timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                                string Detail2 = "Form No. " + AppID + " has been revised by " + logonName;
                                string Status = "R";
                                // Add Values
                                sqlcmd = new SqlCommand("spReviseClick", conn);
                                sqlcmd.Parameters.AddWithValue("Id", AppID);
                                sqlcmd.Parameters.AddWithValue("Email", Mail);
                                sqlcmd.Parameters.AddWithValue("Status", Status);
                                sqlcmd.Parameters.AddWithValue("Detail2", Detail2);
                                sqlcmd.Parameters.AddWithValue("Timestamp", Timestamp);
                                sqlcmd.Parameters.AddWithValue("Comment", Comment);


                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                oDr = sqlcmd.ExecuteReader();
                                while (oDr.Read())
                                {


                                }


                                //Sent To sendEmail()
                                string CC_Mail = GmMail.Value;
                                string PIC = Vendor_PIC.Value;
                                string logonEmail = Request.Cookies.Get("Email").Value;
                                string CC = CC_Mail + "; " + logonEmail + ";";
                                sendEmailRevise(EncodeID, PIC, oMail, logonEmail, CC);

                                //SweetAlert Script
                                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oSaveRevise('success', 'Success', 'Request success, Value has inserted.', '" + EncodeID + "');", true);




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
            else //False Format
            {
                //Display Error
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Sent Failed, Email is invalid.');", true);

            }

        }

        protected void Reject_Click(object sender, EventArgs e)
        {


            //Database Connect
            //oDT = DataTable
            //oDr = DataReader
            //oDa = DataAdapter
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataReader oDr;
            SqlDataAdapter oDa;
            string Mail = Request.Form[Email_Rej.UniqueID];
            string Comment = Comment_Reject.Value;
            //Get AppID
            string AppID = null;
            string EncodeID = Request.QueryString["ID"];

            string[] words = null;
            string oMail = null;

            string[] checkMailFormat = null;
            bool boolGmMail = CheckEmailFormat(Mail);
            bool boolMail = false;
            bool boolCheckMail = true;

            words = Mail.Split(';');

            //CheckFormatEmail#1
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != "")
                {
                    boolMail = CheckEmailFormat(words[i].Trim());
                    if (boolMail == false)//Wrong Format
                    {
                        boolCheckMail = false;
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Sent Failed, E-mail Wrong Format.');", true);
                    }
                    else //Correct Format
                    {
                        checkMailFormat = words[i].Split(' ');
                        foreach (string mail in checkMailFormat) //Loop CheckMailFormat
                        {
                            if (mail != "")
                            {
                                boolMail = CheckEmailFormat(mail); //CheckFormatEmail#2
                                //Set Email format
                                if (boolMail == true) //True Format
                                {
                                    if (words[i].Contains(";") == false)
                                    {
                                        oMail = oMail + words[i].Trim() + "; ";
                                    }
                                    else
                                    {
                                        oMail = oMail + words[i].Trim();
                                    }
                                }
                                else //False Format
                                {
                                    boolCheckMail = false;
                                    //Display Error
                                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Sent Failed, Email is invalid.');", true);
                                }
                            }

                        }
                    }


                }

            }



            if (EncodeID != null)
            {
                var base64Bytes = System.Convert.FromBase64String(EncodeID);
                //Context.Response.Write(base64Bytes);
                AppID = System.Text.Encoding.UTF8.GetString(base64Bytes);
            }

            if (boolCheckMail != false && AppID != null)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                //Create IncidentNo
                sql.Append("spGetHistoryDetail");
                sqlcmd = new SqlCommand(sql.ToString(), conn);

                sqlcmd.Parameters.AddWithValue("Id", AppID);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                oDa = new SqlDataAdapter(sqlcmd);
                oDa.Fill(oDt);
                if (oDt.Rows.Count > 0)
                {
                    // Status Check And Redirect page.
                    string GetStat = oDt.Rows[0]["Status"].ToString();
                    if (GetStat == "F")
                    {
                        Response.Redirect("PS_Finish.aspx?id=" + EncodeID, false);
                    }
                    else if (GetStat == "J")
                    {
                        Response.Redirect("PS_Reject.aspx?id=" + EncodeID, false);
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
                    sql1.Append("spGetAppDetail");
                    sqlcmd1 = new SqlCommand(sql1.ToString(), conn1);
                    sqlcmd1.Parameters.AddWithValue("Id", AppID);
                    sqlcmd1.CommandType = CommandType.StoredProcedure;
                    oDa1 = new SqlDataAdapter(sqlcmd1);
                    oDa1.Fill(oDt1);
                    if (oDt1.Rows.Count > 0)
                    {
                        string GetStat = oDt1.Rows[0]["Status"].ToString();
                        if (GetStat == "R")
                        {
                            Response.Redirect("PS_Revise.aspx?id=" + EncodeID, false);
                        }
                        else if (GetStat == "D")
                        {
                            Response.Redirect("Draft.aspx?id=" + EncodeID, false);
                        }
                        else
                        {
                            try
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();

                                conn = new SqlConnection(Properties.Settings.Default.Conn);
                                conn.Open();
                                //กำหนดตัวแปร
                                string logonName = Request.Cookies.Get("FullName").Value;
                                var Timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
                                string Detail2 = "Form No. " + AppID + " has been rejected by " + logonName;
                                string Status = "J";
                                // Add Values
                                sqlcmd = new SqlCommand("spReject_FinishClick", conn);
                                sqlcmd.Parameters.AddWithValue("ID", AppID);
                                sqlcmd.Parameters.AddWithValue("Status", Status);
                                sqlcmd.Parameters.AddWithValue("Email", oMail);
                                sqlcmd.Parameters.AddWithValue("Detail2", Detail2);
                                sqlcmd.Parameters.AddWithValue("Comment", Comment);
                                sqlcmd.Parameters.AddWithValue("Timestamp", Timestamp);


                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                oDr = sqlcmd.ExecuteReader();
                                while (oDr.Read())
                                {


                                }


                                //Sent To sendEmail()
                                string CC_Mail = GmMail.Value;
                                string PIC = Vendor_PIC.Value;
                                string logonEmail = Request.Cookies.Get("Email").Value;
                                string CC = CC_Mail + "; " + logonEmail + ";";
                                sendEmailReject(EncodeID, PIC, oMail, logonEmail, CC);

                                //SweetAlert Script
                                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oSaveReject('success', 'Success', 'Request success, Value has inserted.', '" + EncodeID + "');", true);




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
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Reject Failed, E-mail Wrong Format.');", true);
            }

        }

        protected void Finish_Click(object sender, EventArgs e)
        {
            //Database Connect
            //oDT = DataTable
            //oDr = DataReader
            //oDa = DataAdapter
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataReader oDr;
            SqlDataAdapter oDa;
            string Mail = Request.Form[Email_Finish.UniqueID];
            string Comment = Comment_Finish.Value;

            //Get AppID
            string AppID = null;
            string EncodeID = Request.QueryString["ID"];

            string[] words = null;
            string oMail = null;

            string[] checkMailFormat = null;
            bool boolGmMail = CheckEmailFormat(Mail);
            bool boolMail = false;
            bool boolCheckMail = true;

            words = Mail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            //CheckFormatEmail#1
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != "")
                {
                    boolMail = CheckEmailFormat(words[i].Trim());
                    if (boolMail == false)//Wrong Format
                    {
                        boolCheckMail = false;
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Sent Failed, E-mail Wrong Format.');", true);
                    }
                    else //Correct Format
                    {
                        checkMailFormat = words[i].Split(' ');
                        foreach (string mail in checkMailFormat) //Loop CheckMailFormat
                        {
                            if (mail != "")
                            {
                                boolMail = CheckEmailFormat(mail); //CheckFormatEmail#2
                                //Set Email format
                                if (boolMail == true) //True Format
                                {
                                    if (words[i].Contains(";") == false)
                                    {
                                        oMail = oMail + words[i].Trim() + "; ";
                                    }
                                    else
                                    {
                                        oMail = oMail + words[i].Trim();
                                    }
                                }
                                else //False Format
                                {
                                    boolCheckMail = false;
                                    //Display Error
                                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Sent Failed, Email Format is invalid.');", true);
                                }
                            }

                        }
                    }


                }
            }
            if (EncodeID != null && EncodeID != "undefined")
            {
                var base64Bytes = System.Convert.FromBase64String(EncodeID);
                //Context.Response.Write(base64Bytes);
                AppID = System.Text.Encoding.UTF8.GetString(base64Bytes);
            }

            if (AppID != null && boolCheckMail != false)
            {

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                //Create IncidentNo
                sql.Append("spGetHistoryDetail");
                sqlcmd = new SqlCommand(sql.ToString(), conn);

                sqlcmd.Parameters.AddWithValue("Id", AppID);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                oDa = new SqlDataAdapter(sqlcmd);
                oDa.Fill(oDt);
                if (oDt.Rows.Count > 0)
                {
                    // Status Check And Redirect page.
                    string GetStat = oDt.Rows[0]["Status"].ToString();
                    if (GetStat == "F")
                    {
                        Response.Redirect("PS_Finish.aspx?id=" + EncodeID, false);
                    }
                    else if (GetStat == "J")
                    {
                        Response.Redirect("PS_Reject.aspx?id=" + EncodeID, false);
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
                    sql1.Append("spGetAppDetail");
                    sqlcmd1 = new SqlCommand(sql1.ToString(), conn1);
                    sqlcmd1.Parameters.AddWithValue("Id", AppID);
                    sqlcmd1.CommandType = CommandType.StoredProcedure;
                    oDa1 = new SqlDataAdapter(sqlcmd1);
                    oDa1.Fill(oDt1);
                    if (oDt1.Rows.Count > 0)
                    {
                        string GetStat = oDt1.Rows[0]["Status"].ToString();
                        if (GetStat == "D")
                        {
                            Response.Redirect("Draft.aspx?id=" + EncodeID, false);
                        }
                        else if (GetStat == "R")
                        {
                            Response.Redirect("PS_Revise.aspx?id=" + EncodeID, false);
                        }
                        else
                        {
                            try
                            {
                                if (conn.State == ConnectionState.Open)
                                    conn.Close();

                                conn = new SqlConnection(Properties.Settings.Default.Conn);
                                conn.Open();
                                //กำหนดตัวแปร
                                string logonName = Request.Cookies.Get("FullName").Value;
                                var Timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
                                string Detail2 = "Form No. " + AppID + " has been finished by " + logonName;
                                string Status = "F";
                                // Add Values
                                sqlcmd = new SqlCommand("spReject_FinishClick", conn);
                                sqlcmd.Parameters.AddWithValue("ID", AppID);
                                sqlcmd.Parameters.AddWithValue("Status", Status);
                                sqlcmd.Parameters.AddWithValue("Email", oMail);
                                sqlcmd.Parameters.AddWithValue("Detail2", Detail2);
                                sqlcmd.Parameters.AddWithValue("Comment", Comment);
                                sqlcmd.Parameters.AddWithValue("Timestamp", Timestamp);


                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                oDr = sqlcmd.ExecuteReader();
                                while (oDr.Read())
                                {


                                }

                                //Sent To sendEmail()
                                string CC_Mail = GmMail.Value;
                                string PIC = Vendor_PIC.Value;
                                string logonEmail = Request.Cookies.Get("Email").Value;
                                string CC = CC_Mail + "; " + logonEmail + ";";
                                sendEmailFinish(EncodeID, PIC, oMail, logonEmail, CC);

                                //SweetAlert Script
                                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFinish('success', 'Success', 'Send success, Value has inserted.', '" + EncodeID + "');", true);




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
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Revise fail, ID null OR Email invalid.');", true);
            }

        }


        protected void Get_App_Detail()
        {

            //Database Connect

            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;

            string CreateB = null;
            //ดึง AppID From Database.Form
            //string AppID = Request.QueryString["Id"];
            string CreateBy = null;
            string AppID = null;
            string EncodeID = Request.QueryString["ID"];
            string CodeRec = Request.QueryString["RecCode"];
            string Encode_ID = EncodeID;
            if (CodeRec == "1")
            {
                HttpCookie CookieUserid = new HttpCookie("RecCode");
                CookieUserid.Value = HttpContext.Current.Request.Url.AbsoluteUri;
                Response.Cookies.Add(CookieUserid);
            }

            if (Request.Cookies.Get("EmployeeId") == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Employee ID is Null, Please Login First.');", true);
            }
            else
            {
                if (EncodeID.Length != 16)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oList('error', 'Error', 'ID is Complete, Please try again.');", true);

                }
                else
                {


                    CreateBy = Request.Cookies.Get("EmployeeId").Value;



                    try
                    {
                        if (EncodeID != "undefined")
                        {
                            if (EncodeID != null)
                            {
                                var base64Bytes = System.Convert.FromBase64String(EncodeID);
                                //Context.Response.Write(base64Bytes);
                                AppID = System.Text.Encoding.UTF8.GetString(base64Bytes);

                                if (conn.State == ConnectionState.Open)
                                    conn.Close();

                                conn = new SqlConnection(Properties.Settings.Default.Conn);
                                conn.Open();
                                //Create IncidentNo
                                sql.Append("spGetHistoryDetail");
                                sqlcmd = new SqlCommand(sql.ToString(), conn);

                                sqlcmd.Parameters.AddWithValue("Id", AppID);
                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                oDa = new SqlDataAdapter(sqlcmd);
                                oDa.Fill(oDt);
                                if (oDt.Rows.Count > 0)
                                {
                                    // Status Check And Redirect page.
                                    string GetStat = oDt.Rows[0]["Status"].ToString();
                                    if (GetStat == "F")
                                    {
                                        Response.Redirect("PS_Finish.aspx?id=" + EncodeID, false);
                                    }
                                    else if (GetStat == "J")
                                    {
                                        Response.Redirect("PS_Reject.aspx?id=" + EncodeID, false);
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
                                    sql1.Append("spGetAppDetail");
                                    sqlcmd1 = new SqlCommand(sql1.ToString(), conn1);
                                    sqlcmd1.Parameters.AddWithValue("Id", AppID);
                                    sqlcmd1.CommandType = CommandType.StoredProcedure;
                                    oDa1 = new SqlDataAdapter(sqlcmd1);
                                    oDa1.Fill(oDt1);
                                    if (oDt1.Rows.Count > 0)
                                    {
                                        // Status Check And Redirect page.
                                        string GetStat = oDt1.Rows[0]["Status"].ToString();
                                        if (GetStat == "D")
                                        {
                                            Response.Redirect("Draft.aspx?id=" + EncodeID, false);
                                        }
                                        else if (GetStat == "R")
                                        {
                                            Response.Redirect("PS_Revise.aspx?id=" + EncodeID, false);
                                        }
                                        else if (GetStat == "J")
                                        {
                                            Response.Redirect("PS_Reject.aspx?id=" + EncodeID, false);
                                        }
                                        else if (GetStat == "F")
                                        {
                                            Response.Redirect("Finish.aspx?id=" + EncodeID, false);
                                        }
                                        else
                                        {
                                            // Alter Data แก้ไขข้อมูล Front = Back
                                            App_ID.Text = oDt1.Rows[0]["App_ID"].ToString();
                                            Vendor_Name.Value = oDt1.Rows[0]["Vendor_Name"].ToString();
                                            Vendor_PIC.Value = oDt1.Rows[0]["Vendor_PIC"].ToString();
                                            Email.Value = oDt1.Rows[0]["Email"].ToString();
                                            Email_Revise.Value = oDt1.Rows[0]["Email"].ToString();
                                            Email_Rej.Value = oDt1.Rows[0]["Email"].ToString();
                                            Email_Finish.Value = oDt1.Rows[0]["Email"].ToString();
                                            UploadFile.Value = oDt1.Rows[0]["FileUpload"].ToString();
                                            CreateCom.Value = oDt1.Rows[0]["CreateByID"].ToString();
                                            CreateID.Value = CreateBy; //Cookies ID
                                            GmMail.Value = oDt1.Rows[0]["CC"].ToString();
                                        }



                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'ID is Null, Please Login First.');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Fail, ID undefined.');", true);
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
        protected void Get_Transaction()
        {

            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataReader oDr;
            string yearstamp = DateTime.Now.ToString("yyyy");
            string monthstamp = DateTime.Now.ToString("MM");
            string EncodeID = Request.QueryString["ID"];
            string AppID = null;
            //string AppID = Request.QueryString["Id"];
            if (EncodeID != null && EncodeID != "undefined")
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

                }
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



        protected void Upload_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                SqlConnection conn = new SqlConnection();
                SqlCommand sqlcmd = new SqlCommand();
                DataTable oDt = new DataTable();
                SqlDataReader oDr;
                string EncodeID = Request.QueryString["ID"];
                string id = null;

                if (EncodeID != null)
                {
                    var base64Bytes = System.Convert.FromBase64String(EncodeID);
                    //Context.Response.Write(base64Bytes);
                    id = System.Text.Encoding.UTF8.GetString(base64Bytes);
                }
                string filecode = hdfFile1.Value;
                if (id != null)
                {

                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn = new SqlConnection(Properties.Settings.Default.Conn);
                    conn.Open();

                    string logonName = Request.Cookies.Get("FullName").Value;
                    string status = "P";
                    var timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                    string detail = id + " Files was Edit by " + logonName;
                    sql.Append("spFileUpload");
                    sqlcmd = new SqlCommand(sql.ToString(), conn);
                    sqlcmd.Parameters.AddWithValue("Id", id);
                    sqlcmd.Parameters.AddWithValue("Status", status);
                    sqlcmd.Parameters.AddWithValue("Detail", detail);
                    sqlcmd.Parameters.AddWithValue("Update_Date", timestamp);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    oDr = sqlcmd.ExecuteReader();




                    string folder = id.Substring(1, 4);
                    string subfolder = id.Substring(1, 6);
                    string Path = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id);


                    if (Directory.Exists(Path) == false)
                    {
                        string pathAppForm = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form");
                        string pathSME = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research");
                        string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5");
                        string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank");
                        string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company");
                        string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20");
                        string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate");
                        string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10");

                        if (Directory.Exists(pathAppForm) == false)
                        {
                            Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form"));
                        }
                        if (Directory.Exists(pathSME) == false)
                        {
                            Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research"));
                        }
                        if (Directory.Exists(pathBOJ5) == false)
                        {
                            Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5"));
                        }
                        if (Directory.Exists(pathBookBank) == false)
                        {
                            Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank"));
                        }
                        if (Directory.Exists(pathOrgCompany) == false)
                        {
                            Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company"));
                        }
                        if (Directory.Exists(pathPP20) == false)
                        {
                            Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20"));
                        }
                        if (Directory.Exists(pathRegisCert) == false)
                        {
                            Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate"));
                        }
                        if (Directory.Exists(pathSPS10) == false)
                        {
                            Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10"));
                        }

                    }
                    else
                    {
                        string Path1 = Server.MapPath("Document\\fileAttach\\" + folder);
                        if (Directory.Exists(Path1) == false)
                        {
                            Directory.CreateDirectory(Path1);
                        }
                        else
                        {
                            string Path2 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder);
                            if (Directory.Exists(Path2) == false)
                            {
                                Directory.CreateDirectory(Path2);
                            }
                            else
                            {
                                string Path3 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id);
                                if (Directory.Exists(Path3) == false)
                                {
                                    Directory.CreateDirectory(Path3);

                                }
                                else
                                {
                                    string pathAppForm = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form");
                                    string pathSME = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research");
                                    string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5");
                                    string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank");
                                    string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company");
                                    string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20");
                                    string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate");
                                    string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10");

                                    if (Directory.Exists(pathAppForm) == false)
                                    {
                                        Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form"));
                                    }
                                    if (Directory.Exists(pathSME) == false)
                                    {
                                        Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research"));
                                    }
                                    if (Directory.Exists(pathBOJ5) == false)
                                    {
                                        Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5"));
                                    }
                                    if (Directory.Exists(pathBookBank) == false)
                                    {
                                        Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank"));
                                    }
                                    if (Directory.Exists(pathOrgCompany) == false)
                                    {
                                        Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company"));
                                    }
                                    if (Directory.Exists(pathPP20) == false)
                                    {
                                        Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20"));
                                    }
                                    if (Directory.Exists(pathRegisCert) == false)
                                    {
                                        Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate"));
                                    }
                                    if (Directory.Exists(pathSPS10) == false)
                                    {
                                        Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10"));
                                    }

                                    //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form"));
                                    //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate"));
                                    //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20"));
                                    //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank"));
                                    //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5"));
                                    //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company"));
                                    //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10"));
                                }
                            }
                        }
                        //Directory.Delete(oPath, true);
                        //Directory.CreateDirectory(oPath);
                    }
                    //string ReAppRegis = File1.Value;
                    //string ReBOJ5 = File2.Value;
                    //string ReBookBank = File3.Value;
                    //string ReOrgCompany = File4.Value;
                    //string RePP20 = File5.Value;
                    //string ReRegisCert = File6.Value;
                    //string ReSPS10 = File7.Value;

                    HttpPostedFile oFile = null;

                    if (filecode == "0")
                    {
                        oFile = Context.Request.Files[0];
                        string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("-", "_");


                        var folderPath = HttpContext.Current.Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\");
                        foreach (string file in Directory.GetFiles(folderPath))
                        {
                            File.Delete(file);
                        }
                        if (File.Exists(folderPath) == false)
                        {
                            oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\" + filename));
                        }

                        //File.Delete(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\"));
                        //System.IO.File.Delete(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\"));
                        //oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\" + filename));
                    }
                    else if (filecode == "1")
                    {
                        oFile = Context.Request.Files[1];
                        string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("-", "_");
                        var folderPath = HttpContext.Current.Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research\\");
                        foreach (string file in Directory.GetFiles(folderPath))
                        {
                            File.Delete(file);
                        }
                        if (File.Exists(folderPath) == false)
                        {
                            oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research\\" + filename));
                        }
                    }
                    else if (filecode == "2")
                    {
                        oFile = Context.Request.Files[5];
                        string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("-", "_");
                        var folderPath = HttpContext.Current.Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5\\");
                        foreach (string file in Directory.GetFiles(folderPath))
                        {
                            File.Delete(file);
                        }
                        if (File.Exists(folderPath) == false)
                        {
                            oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5\\" + filename));
                        }
                    }
                    else if (filecode == "3")
                    {
                        oFile = Context.Request.Files[4];
                        string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("-", "_");
                        var folderPath = HttpContext.Current.Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank\\");
                        foreach (string file in Directory.GetFiles(folderPath))

                        {
                            File.Delete(file);
                        }
                        if (File.Exists(folderPath) == false)
                        {
                            oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank\\" + filename));
                        }
                    }
                    else if (filecode == "4")
                    {
                        oFile = Context.Request.Files[6];
                        string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("-", "_");
                        var folderPath = HttpContext.Current.Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company\\");
                        foreach (string file in Directory.GetFiles(folderPath))

                        {
                            File.Delete(file);
                        }
                        if (File.Exists(folderPath) == false)
                        {
                            oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company\\" + filename));
                        }
                    }
                    else if (filecode == "5")
                    {
                        oFile = Context.Request.Files[3];
                        string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("-", "_");
                        var folderPath = HttpContext.Current.Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20\\");
                        foreach (string file in Directory.GetFiles(folderPath))

                        {
                            File.Delete(file);
                        }
                        if (File.Exists(folderPath) == false)
                        {
                            oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20\\" + filename));
                        }
                    }
                    else if (filecode == "6")
                    {
                        oFile = Context.Request.Files[2];
                        string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("-", "_");
                        var folderPath = HttpContext.Current.Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate\\");
                        foreach (string file in Directory.GetFiles(folderPath))
                        {
                            File.Delete(file);
                        }
                        if (File.Exists(folderPath) == false)
                        {
                            oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate\\" + filename));
                        }
                    }
                    else if (filecode == "7")
                    {
                        oFile = Context.Request.Files[7];
                        string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "").Replace("-", "_");
                        var folderPath = HttpContext.Current.Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10\\");
                        foreach (string file in Directory.GetFiles(folderPath))
                        {
                            File.Delete(file);
                        }
                        if (File.Exists(folderPath) == false)
                        {
                            oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10\\" + filename));
                        }
                    }

                    //HttpPostedFile oFile = null;
                    //for (int i = 0; i < countfile; i++)
                    //{
                    //    oFile = Context.Request.Files[i];
                    //    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                    //    oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\" + filename));

                    //}
                    string encodeApp_id = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(id));

                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('success', 'Success', 'Upload success, Value has inserted.', '" + encodeApp_id + "');", true);




                }

            }

            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        public static bool CheckEmailFormat(string arrMail)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (Regex.IsMatch(arrMail, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckNameFormatFullName(string FullName)
        {

            string pattern = @"^[a-zA-Z0-9ก-๙ ]+[a-zA-Z0-9ก-๙ ]";
            if (Regex.IsMatch(FullName, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void sendEmailRevise(string AppID, string vendor_PIC, string emailTo, string emailFrom, string GM)
        {
            //Response.Write("Dear : " + vendor_PIC + "\n" + "Email : " + emailTo);
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            MailMessage myMail = new MailMessage();
            WebClient Client = new WebClient();
            string BodyMail;
            string Header;
            string Emp_Phone = Request.Cookies.Get("Phone").Value;
            string Emp_Name = Request.Cookies.Get("FullName").Value;

            //string emailTo = "Prompiriya_S@hinothailand.com";
            String Body = "<img src=\"https://career.hinothailand.com/Career/Images/companylogo.png\" style=\"width:125px; height:125px;\" /> ";
            string link = "https://hinommt.com/SupplierApplication/Vendor_Login.aspx?id=" + AppID;
            string EncodeID = null;
            if (AppID != null)
            {
                var base64Bytes = System.Convert.FromBase64String(AppID);
                //Context.Response.Write(base64Bytes);
                EncodeID = System.Text.Encoding.UTF8.GetString(base64Bytes);
            }
            try
            {
                myMail.From = new MailAddress(emailFrom);
                if (GM != "")
                {
                    if (GM.Contains(";"))
                    {
                        foreach (var item in GM.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            myMail.CC.Add(new MailAddress(item));
                        }
                    }
                    else
                    {
                        myMail.CC.Add(new MailAddress(GM));
                    }

                }
                string MailTo = emailTo.Replace(" ", "");
                foreach (var email in MailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {


                    myMail.To.Add(new MailAddress(email));
                }

                //if (myMail.To.Count == emailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Count())
                //{

                myMail.BodyEncoding = Encoding.UTF8;

                Header = "| Supplier Application system |";
                BodyMail = Client.DownloadString(Server.MapPath("Template/ReviseTemplate.htm"));
                BodyMail = BodyMail.Replace("#NameTo", vendor_PIC);
                BodyMail = BodyMail.Replace("#AppID", EncodeID);
                BodyMail = BodyMail.Replace("#Phone", Emp_Phone);
                BodyMail = BodyMail.Replace("#image", Body);
                BodyMail = BodyMail.Replace("#EmployeeName", Emp_Name);
                BodyMail = BodyMail.Replace("#link", link);
                AlternateView HtmlView;
                HtmlView = AlternateView.CreateAlternateViewFromString(BodyMail, null, "text/html");
                myMail.AlternateViews.Add(HtmlView);
                myMail.Subject = Header;
                SmtpClient Clients = new SmtpClient();
                Clients.Host = Properties.Settings.Default.Ipmail;
                Clients.Send(myMail);
                //}


            }
            catch (Exception ex)
            {
                string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }

            finally
            {
                conn.Close();
            }
        }
        public void sendEmailFinish(string AppID, string vendor_PIC, string emailTo, string emailFrom, string GM)
        {
            //Response.Write("Dear : " + vendor_PIC + "\n" + "Email : " + emailTo);
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            MailMessage myMail = new MailMessage();
            WebClient Client = new WebClient();
            string BodyMail;
            string Header;
            string Emp_Phone = Request.Cookies.Get("Phone").Value;
            string Emp_Name = Request.Cookies.Get("FullName").Value;

            //string emailTo = "Prompiriya_S@hinothailand.com";
            String Body = "<img src=\"https://career.hinothailand.com/Career/Images/companylogo.png\" style=\"width:125px; height:125px;\" /> ";
            string link = "https://hinommt.com/SupplierApplication/Vendor_Login.aspx?id=" + AppID;
            string EncodeID = null;
            if (AppID != null)
            {
                var base64Bytes = System.Convert.FromBase64String(AppID);
                //Context.Response.Write(base64Bytes);
                EncodeID = System.Text.Encoding.UTF8.GetString(base64Bytes);
            }
            try
            {
                myMail.From = new MailAddress(emailFrom);
                if (GM != "")
                {
                    if (GM.Contains(";"))
                    {
                        string GMTo = GM.Replace(" ", "");
                        foreach (var item in GMTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            myMail.CC.Add(new MailAddress(item));
                        }
                    }
                    else
                    {
                        myMail.CC.Add(new MailAddress(GM));
                    }

                }
                string MailTo = emailTo.Replace(" ", "");

                foreach (var email in MailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {

                    myMail.To.Add(new MailAddress(email));
                }

                //if (myMail.To.Count == emailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Count())
                //{

                myMail.BodyEncoding = Encoding.UTF8;

                Header = "| Supplier Application system |";
                BodyMail = Client.DownloadString(Server.MapPath("Template/FinishTemplate.htm"));
                BodyMail = BodyMail.Replace("#NameTo", vendor_PIC);
                BodyMail = BodyMail.Replace("#AppID", EncodeID);
                BodyMail = BodyMail.Replace("#Phone", Emp_Phone);
                BodyMail = BodyMail.Replace("#image", Body);
                BodyMail = BodyMail.Replace("#EmployeeName", Emp_Name);
                BodyMail = BodyMail.Replace("#link", link);
                AlternateView HtmlView;
                HtmlView = AlternateView.CreateAlternateViewFromString(BodyMail, null, "text/html");
                myMail.AlternateViews.Add(HtmlView);
                myMail.Subject = Header;
                SmtpClient Clients = new SmtpClient();
                Clients.Host = Properties.Settings.Default.Ipmail;
                Clients.Send(myMail);
                //}

            }
            catch (Exception ex)
            {
                string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }

            finally
            {
                conn.Close();
            }
        }
        public void sendEmailReject(string AppID, string vendor_PIC, string emailTo, string emailFrom, string GM)
        {
            //Response.Write("Dear : " + vendor_PIC + "\n" + "Email : " + emailTo);
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            MailMessage myMail = new MailMessage();
            WebClient Client = new WebClient();
            string BodyMail;
            string Header;
            string Emp_Phone = Request.Cookies.Get("Phone").Value;
            string Emp_Name = Request.Cookies.Get("FullName").Value;

            //string emailTo = "Prompiriya_S@hinothailand.com";
            String Body = "<img src=\"https://career.hinothailand.com/Career/Images/companylogo.png\" style=\"width:125px; height:125px;\" /> ";
            string link = "https://hinommt.com/SupplierApplication/Vendor_Login.aspx?id=" + AppID;
            string EncodeID = null;
            if (AppID != null)
            {
                var base64Bytes = System.Convert.FromBase64String(AppID);
                //Context.Response.Write(base64Bytes);
                EncodeID = System.Text.Encoding.UTF8.GetString(base64Bytes);
            }
            try
            {
                myMail.From = new MailAddress(emailFrom);

                if (GM != "")
                {
                    if (GM.Contains(";"))
                    {
                        foreach (var item in GM.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            myMail.CC.Add(new MailAddress(item));
                        }
                    }
                    else
                    {
                        myMail.CC.Add(new MailAddress(GM));
                    }

                }
                string MailTo = emailTo.Replace(" ", "");

                foreach (var email in MailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    myMail.To.Add(new MailAddress(email));
                }
                //if (myMail.To.Count == emailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Count())
                //{

                myMail.BodyEncoding = Encoding.UTF8;

                Header = "| Supplier Application system |";
                BodyMail = Client.DownloadString(Server.MapPath("Template/RejectTemplate.htm"));
                BodyMail = BodyMail.Replace("#NameTo", vendor_PIC);
                BodyMail = BodyMail.Replace("#AppID", EncodeID);
                BodyMail = BodyMail.Replace("#Phone", Emp_Phone);
                BodyMail = BodyMail.Replace("#image", Body);
                BodyMail = BodyMail.Replace("#EmployeeName", Emp_Name);
                BodyMail = BodyMail.Replace("#link", link);
                AlternateView HtmlView;
                HtmlView = AlternateView.CreateAlternateViewFromString(BodyMail, null, "text/html");
                myMail.AlternateViews.Add(HtmlView);
                myMail.Subject = Header;
                SmtpClient Clients = new SmtpClient();
                Clients.Host = Properties.Settings.Default.Ipmail;
                Clients.Send(myMail);

                //}

            }
            catch (Exception ex)
            {
                string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }

            finally
            {
                conn.Close();
            }
        }

    }
}