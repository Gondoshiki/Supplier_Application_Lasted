using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SupplierRegistration
{
    public partial class Draft : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Use one time Accese
            if (IsPostBack == false)
            {
                Get_App_Detail();
                Get_Transaction();
            }
        }
        protected void Submit_Click(object sender, EventArgs e)
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
            if (Request.Cookies.Get("EmployeeId") == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Check fail, EmployeeID is null.');", true);
            }
            else
            {
                string CC = null;
                string VendorName = Vendor_Name.Value;
                string PIC = Vendor_PIC.Value;
                string Mail = Email.Value;
                string CreateBy = Request.Cookies.Get("EmployeeId").Value;
                string logonEmail = Request.Cookies.Get("Email").Value;
                string AppID = null;
                string EncodeID = Request.QueryString["ID"];

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
                            if (GetStat == "P")
                            {
                                Response.Redirect("PS_Detail.aspx?id=" + EncodeID, false);
                            }
                            else if (GetStat == "R")
                            {
                                Response.Redirect("PS_Revise.aspx?id=" + EncodeID, false);
                            }
                            else
                            {
                                // Check Name
                                string[] names = null;
                                string oName = null;
                                string[] checkNameFormat = null;
                                bool boolGetName = CheckNameFormat(PIC);
                                bool boolCheckVendor = CheckVendorName(VendorName);
                                bool boolName = false;
                                bool boolCheckName = true;
                                string name1 = null;
                                string[] name2 = null;
                                names = PIC.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                //Check vendor PIC format
                                for (int i = 0; i < names.Length; i++)
                                {
                                    int countname = 0;
                                    if (names[i].Trim() != "")
                                    {

                                        name2 = names[i].Trim().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                        //int countname = name2.Count();

                                        if (name2.Length == 1)
                                        {
                                            if (names[i] != "")
                                            {
                                                names[i].Trim();
                                                boolGetName = CheckNameFormat(names[i].Trim());

                                                if (boolGetName == true)
                                                {
                                                    if (names.Length != 1)
                                                    {
                                                        if (names[i].Trim().Contains(",") == false)
                                                        {
                                                            oName = oName + names[i].Trim() + ",";
                                                        }

                                                    }
                                                    else if (names.Length == 1)
                                                    {
                                                        oName = oName + names[i].Trim();
                                                    }
                                                    else //False Format
                                                    {
                                                        boolCheckName = false;
                                                    }

                                                }
                                                else //False Format
                                                {
                                                    boolCheckName = false;
                                                }
                                            }
                                            else
                                            {
                                                boolCheckName = false;
                                            }


                                        }
                                        else if (name2.Length == 2)
                                        {
                                            if (names[i] != "")
                                            {
                                                string fname = names[i].Trim();
                                                boolGetName = CheckNameFormatFullName(fname.Trim());
                                                if (boolGetName == true)
                                                {
                                                    if (names.Length != 1)
                                                    {
                                                        if (names[i].Trim().Contains(",") == false)
                                                        {
                                                            oName = oName + names[i].Trim() + ",";
                                                        }
                                                    }
                                                    else if (names.Length == 1)
                                                    {
                                                        oName = oName + names[i].Trim();
                                                    }
                                                    else //False Format
                                                    {
                                                        boolCheckName = false;
                                                    }

                                                }
                                                else //False Format
                                                {
                                                    boolCheckName = false;
                                                }
                                            }
                                            else
                                            {
                                                boolCheckName = false;
                                            }

                                        }
                                        else
                                        {
                                            boolCheckName = false;
                                        }

                                    }

                                }
                                string[] words = null;
                                string oMail = null;
                                string[] checkMailFormat = null;
                                string GM = GM_Email.Value;
                                bool boolGMCheck = CheckEmailFormat(GM);
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
                                                        string fwords = words[i].Trim();
                                                        if (fwords.Contains(";") == false)
                                                        {
                                                            oMail = oMail + words[i].Trim() + ";";
                                                        }
                                                        else
                                                        {
                                                            oMail = oMail + words[i].Trim();
                                                        }
                                                    }
                                                    else //False Format
                                                    {
                                                        boolCheckMail = false;
                                                    }
                                                }

                                            }
                                        }
                                    }

                                }


                                try
                                {
                                    //ดึง AppID From Database.Form
                                    //string EncodeID = Request.QueryString["ID"];
                                    ///var base64Bytes = System.Convert.FromBase64String(EncodeID);
                                    //Context.Response.Write(base64Bytes);
                                    //string AppID = System.Text.Encoding.UTF8.GetString(base64Bytes);
                                    if (boolCheckMail && boolCheckName && boolGMCheck == true && boolCheckVendor == true)
                                    {

                                        if (conn.State == ConnectionState.Open)
                                            conn.Close();
                                        string logonName = Request.Cookies.Get("FullName").Value;
                                        conn = new SqlConnection(Properties.Settings.Default.Conn);
                                        conn.Open();
                                        var Timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                                        string Detail2 = "Form has been sent email to " + VendorName;
                                        string Status = "P";
                                        CC = GM + "; " + logonEmail + ";";
                                        //Add ค่าใส่ colunm Attributes.Database <-- Front
                                        sqlcmd = new SqlCommand("spUpdateAppDetail", conn);
                                        sqlcmd.Parameters.AddWithValue("ID", AppID);
                                        sqlcmd.Parameters.AddWithValue("VendorName", VendorName);
                                        sqlcmd.Parameters.AddWithValue("VendorPIC", oName);
                                        sqlcmd.Parameters.AddWithValue("Email", oMail);
                                        sqlcmd.Parameters.AddWithValue("Status", Status);
                                        sqlcmd.Parameters.AddWithValue("Timestamp", Timestamp);
                                        sqlcmd.Parameters.AddWithValue("Detail2", Detail2);
                                        sqlcmd.Parameters.AddWithValue("CreateID", CreateBy);
                                        sqlcmd.Parameters.AddWithValue("GMail", GM);
                                        sqlcmd.CommandType = CommandType.StoredProcedure;
                                        oDr = sqlcmd.ExecuteReader();
                                        while (oDr.Read())
                                        {

                                        }
                                        sendEmail(EncodeID, oName, oMail, logonEmail, CC);

                                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oSubmit('success', 'Success', 'Request success, Value has inserted.', '" + EncodeID + "');", true);

                                    }
                                    else
                                    {
                                        if (boolCheckMail == false)
                                        {
                                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'Email is not correct, Please try again.');", true);
                                        }
                                        if (boolGMCheck == false)
                                        {
                                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'CC Email is not correct, Please try again.');", true);
                                        }
                                        if (boolCheckName == false)
                                        {
                                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'Vendor PIC format is not correct, Please try again.');", true);
                                        }
                                        if (boolCheckVendor == false)
                                        {
                                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Vendor Name format is not correct, Please try again.');", true);
                                        }
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
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Check fail, ID is null.');", true);
                }
            }
        }

        protected void Draft_Click(object sender, EventArgs e)
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
            string VendorName = Vendor_Name.Value;
            string PIC = Vendor_PIC.Value;
            string Mail = Email.Value;
            string CreateBy = Request.Cookies.Get("EmployeeId").Value;
            //Get AppID
            string AppID = null;
            string EncodeID = Request.QueryString["ID"];
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
                        if (GetStat == "P")
                        {
                            Response.Redirect("PS_Detail.aspx?id=" + EncodeID, false);
                        }
                        else if (GetStat == "R")
                        {
                            Response.Redirect("PS_Revise.aspx?id=" + EncodeID, false);
                        }
                        else
                        {

                            // Check Name
                            string[] names = null;
                            string oName = null;
                            string[] checkNameFormat = null;
                            bool boolGetName = CheckNameFormat(PIC);
                            bool boolCheckVendor = CheckVendorName(VendorName);
                            bool boolName = false;
                            bool boolCheckName = true;
                            string name1 = null;
                            string[] name2 = null;
                            names = PIC.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            //Check vendor PIC format
                            for (int i = 0; i < names.Length; i++)
                            {
                                int countname = 0;
                                if (names[i].Trim() != "")
                                {

                                    name2 = names[i].Trim().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                    //int countname = name2.Count();

                                    if (name2.Length == 1)
                                    {
                                        if (names[i] != "")
                                        {
                                            names[i].Trim();
                                            boolGetName = CheckNameFormat(names[i].Trim());

                                            if (boolGetName == true)
                                            {
                                                if (names.Length != 1)
                                                {
                                                    if (names[i].Trim().Contains(",") == false)
                                                    {
                                                        oName = oName + names[i].Trim() + ",";
                                                    }

                                                }
                                                else if (names.Length == 1)
                                                {
                                                    oName = oName + names[i].Trim();
                                                }
                                                else //False Format
                                                {
                                                    boolCheckName = false;
                                                }

                                            }
                                            else //False Format
                                            {
                                                boolCheckName = false;
                                            }
                                        }


                                    }
                                    else if (name2.Length == 2)
                                    {
                                        if (names[i] != "")
                                        {
                                            string fname = names[i].Trim();
                                            boolGetName = CheckNameFormatFullName(fname.Trim());
                                            if (boolGetName == true)
                                            {
                                                if (names.Length != 1)
                                                {
                                                    if (names[i].Trim().Contains(",") == false)
                                                    {
                                                        oName = oName + names[i].Trim() + ",";
                                                    }
                                                }
                                                else if (names.Length == 1)
                                                {
                                                    oName = oName + names[i].Trim();
                                                }
                                                else //False Format
                                                {
                                                    boolCheckName = false;
                                                }

                                            }
                                            else //False Format
                                            {
                                                boolCheckName = false;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        boolCheckName = false;
                                    }

                                }

                            }

                            string[] words = null;
                            string oMail = null;
                            string[] checkMailFormat = null;
                            string GM = GM_Email.Value;
                            bool boolGMCheck = CheckEmailFormat(GM);
                            bool boolMail = false;
                            bool boolCheckMail = true;
                            //CheckFormatEmail#1
                            words = Mail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < words.Length; i++)
                            {
                                if (words[i] != "")
                                {
                                    boolMail = CheckEmailFormat(words[i].Trim());
                                    if (boolMail == false)//Wrong Format
                                    {
                                        boolCheckMail = false;

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
                                                    string fwords = words[i].Trim();
                                                    if (fwords.Contains(";") == false)
                                                    {
                                                        oMail = oMail + words[i].Trim() + ";";
                                                    }
                                                    else
                                                    {
                                                        oMail = oMail + words[i].Trim();
                                                    }
                                                }
                                                else //False Format
                                                {
                                                    boolCheckMail = false;
                                                }
                                            }

                                        }
                                    }
                                }
                            }

                            if (VendorName == "")
                            {
                                VendorName = "";
                            }
                            if (PIC == "")
                            {
                                oName = "";
                            }
                            if (Mail == "")
                            {
                                oMail = "";
                            }
                            if (GM == "")
                            {
                                GM = "";
                            }
                            if (VendorName != "")
                            {
                                if (boolCheckVendor == false)
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Vendor Name format is not correct, Please try again.');", true);
                                }
                            }
                            if (PIC != "")
                            {
                                if (boolCheckName == false)
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'Vendor PIC format is not correct, Please try again.');", true);
                                }
                            }
                            if (Mail != "")
                            {
                                if (boolCheckMail == false)
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'Email is not correct, Please try again.');", true);
                                }
                            }
                            if (GM != "")
                            {
                                if (boolGMCheck == false)
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'CC Email is not correct, Please try again.');", true);

                                }
                            }
                            try
                            {
                                

                                    if (conn.State == ConnectionState.Open)
                                        conn.Close();

                                    conn = new SqlConnection(Properties.Settings.Default.Conn);
                                    conn.Open();
                                    //กำหนดตัวแปร
                                    string logonName = Request.Cookies.Get("FullName").Value;
                                    var Timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                                    string Detail2 = "Draft has been edited by " + logonName;
                                    string Status = "D";
                                    // Add Values
                                    if (App_ID != null)
                                    {
                                        sqlcmd = new SqlCommand("spUpdateAppDetail", conn);
                                        sqlcmd.Parameters.AddWithValue("ID", AppID);
                                        sqlcmd.Parameters.AddWithValue("VendorName", VendorName);
                                        sqlcmd.Parameters.AddWithValue("VendorPIC", oName);
                                        sqlcmd.Parameters.AddWithValue("Email", oMail);
                                        sqlcmd.Parameters.AddWithValue("Status", Status);
                                        sqlcmd.Parameters.AddWithValue("Timestamp", Timestamp);
                                        sqlcmd.Parameters.AddWithValue("Detail2", Detail2);
                                        sqlcmd.Parameters.AddWithValue("CreateID", CreateBy);
                                        sqlcmd.Parameters.AddWithValue("GMail", GM);
                                        sqlcmd.CommandType = CommandType.StoredProcedure;
                                        oDr = sqlcmd.ExecuteReader();
                                        while (oDr.Read())
                                        {

                                        }

                                        //SweetAlert Script
                                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oSaveDraft('success', 'Success', 'Request success, Value has inserted.','" + EncodeID + "');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'App Id is null, Please try again.');", true);
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
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Check fail, EmployeeID is null.');", true);
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
            string yearstamp = DateTime.Now.ToString("yyyy");
            string monthstamp = DateTime.Now.ToString("MM");
            //ดึง AppID From Database.Form
            string EncodeID = Request.QueryString["ID"];
            string AppID = null;

            //Context.Response.Write(base64Bytes);
            //string AppID = Request.QueryString["Id"];
            if (Request.Cookies.Get("EmployeeId") == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Employee ID is Null, Please Login First.');", true);
            }
            else
            {

                try
                {
                    if (EncodeID.Length != 16)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oList('error', 'Error', 'ID is Complete, Please try again.');", true);

                    }
                    else
                    {
                        if (EncodeID != null)
                        {
                            var base64Bytes = System.Convert.FromBase64String(EncodeID);
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

                                    // Alter Data แก้ไขข้อมูล Front = Back
                                    App_ID.Text = oDt1.Rows[0]["App_ID"].ToString();
                                    Vendor_Name.Value = oDt1.Rows[0]["Vendor_Name"].ToString();
                                    Vendor_PIC.Value = oDt1.Rows[0]["Vendor_PIC"].ToString();
                                    Email.Value = oDt1.Rows[0]["Email"].ToString();
                                    GM_Email.Value = oDt1.Rows[0]["CC"].ToString();

                                    // Status Check And Redirect page.
                                    string GetStat = oDt1.Rows[0]["Status"].ToString();
                                    if (GetStat == "P")
                                    {
                                        Response.Redirect("PS_Detail.aspx?id=" + EncodeID, false);
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
                                }
                            }
                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'ID is Null, Please Login First.');", true);
                        }
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
            string yearstamp = DateTime.Now.ToString("yyyy");
            string monthstamp = DateTime.Now.ToString("MM");
            string EncodeID = Request.QueryString["ID"];
            string AppID = null;
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

                }
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

        public static bool CheckNameFormat(string picName)
        {
            string pattern = @"^[a-zA-Zก-๏.]+$";
            if (Regex.IsMatch(picName, pattern))
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

            string pattern = @"^[a-zA-Zก-๏.\s\s]+$";
            if (Regex.IsMatch(FullName, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void sendEmail(string AppID, string vendor_PIC, string emailTo, string emailFrom, string GM)
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
            String Body = "<img src=\"https://career.hinothailand.com/Career/Images/companylogo.png\" style=\"width:125px; height:125px;\" /> ";
            //string emailTo = "Prompiriya_S@hinothailand.com";
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
                BodyMail = Client.DownloadString(Server.MapPath("Template/EmailTemplate.htm"));
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

        public static bool CheckVendorName(string picName)
        {

            string pattern = @"^[a-z0-9A-Zก-๏._\s\s]+$";
            if (Regex.IsMatch(picName, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}