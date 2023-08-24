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
    public partial class PS_Form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static bool CheckEmailFormat(string email)
        { // Regular expression pattern to validate email format string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"; 
            // Use Regex.IsMatch() to check if the email matches the pattern bool isMatch = Regex.IsMatch(email, pattern);
            // return isMatch;
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (Regex.IsMatch(email, pattern))
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
        //public void sendEmail(string AppID, string vendor_PIC, string emailTo, string emailFrom, string GM)
        //{
        //    //Response.Write("Dear : " + vendor_PIC + "\n" + "Email : " + emailTo);
        //    StringBuilder sql = new StringBuilder();
        //    SqlConnection conn = new SqlConnection();
        //    SqlCommand sqlcmd = new SqlCommand();
        //    DataTable oDt = new DataTable();
        //    MailMessage myMail = new MailMessage();
        //    WebClient Client = new WebClient();
        //    string BodyMail;
        //    string Header;
        //    string Emp_Phone = Request.Cookies.Get("Phone").Value;
        //    string Emp_Name = Request.Cookies.Get("FullName").Value;

        //    //string emailTo = "Prompiriya_S@hinothailand.com";
        //    string link = "https://hinommt.com/SupplierApplication/Vendor_Login.aspx?id=" + AppID;
        //    string EncodeID = null;
        //    if (AppID != null)
        //    {
        //        var base64Bytes = System.Convert.FromBase64String(AppID);
        //        //Context.Response.Write(base64Bytes);
        //        EncodeID = System.Text.Encoding.UTF8.GetString(base64Bytes);
        //    }
        //    try
        //    {
        //        myMail.From = new MailAddress(emailFrom);
        //        if (GM != "")
        //        {
        //            if (GM.Contains(";"))
        //            {
        //                string GMTo = GM.Replace(";", ",");
        //                myMail.CC.Add(new MailAddress(GMTo));
        //            }
        //            else
        //            {
        //                myMail.CC.Add(new MailAddress(GM));
        //            }

        //        }

        //        if (emailTo.Contains(";"))
        //        {
        //            string EmailTo = emailTo.Replace(";", ",");
        //            myMail.To.Add(new MailAddress(EmailTo));
        //        }
        //        else
        //        {
        //            string MailTo = emailTo.Replace(" ", "");
        //            myMail.To.Add(new MailAddress(MailTo));
        //        }


        //        myMail.BodyEncoding = Encoding.UTF8;
        //        Header = "| Supplier Application system |";
        //        BodyMail = Client.DownloadString(Server.MapPath("Template/EmailTemplate.htm"));
        //        BodyMail = BodyMail.Replace("#NameTo", vendor_PIC);
        //        BodyMail = BodyMail.Replace("#AppID", EncodeID);
        //        BodyMail = BodyMail.Replace("#Phone", Emp_Phone);
        //        BodyMail = BodyMail.Replace("#EmployeeName", Emp_Name);
        //        BodyMail = BodyMail.Replace("#link", link);


        //        AlternateView HtmlView;
        //        HtmlView = AlternateView.CreateAlternateViewFromString(BodyMail, null, "text/html");
        //        myMail.AlternateViews.Add(HtmlView);
        //        myMail.Subject = Header;
        //        SmtpClient Clients = new SmtpClient();
        //        Clients.Host = Properties.Settings.Default.Ipmail;
        //        Clients.Send(myMail);


        //    }
        //    catch (Exception ex)
        //    {
        //        string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
        //    }

        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

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

        protected void Submit_Click(object sender, EventArgs e)
        {
            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataReader oDr;

            if (conn.State == ConnectionState.Open)
                conn.Close();

            string VendorName = Vendor_Name.Value;
            string PIC = Vendor_PIC.Value;
            string Mail = Email.Value;
            string GmMail = GM_Email.Value;
            string[] arrMail = Mail.Split(';');
            string[] checkMailFormat = null;
            string[] arrGmMail = Mail.Split(' ');
            string oMail = null;
            string logonName = Request.Cookies.Get("FullName").Value;
            string logonEmail = Request.Cookies.Get("Email").Value;
            string logonID = Request.Cookies.Get("EmployeeID").Value;
            string yearstamp = DateTime.Now.ToString("yyyy");
            string monthstamp = DateTime.Now.ToString("MM");
            string runningNo = null;
            string CC = null;
            bool boolGmMail = CheckEmailFormat(GmMail);
            bool boolMail = false; //check for set new email format
            bool boolCheckMail = true; //for check email format


            bool boolGetName = CheckNameFormat(PIC);
            bool boolCheckName = true;
            string[] names = null;
            bool boolCheckVendor = CheckVendorName(VendorName);


            string oName = null;
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

            //Check Email format

            for (int i = 0; i < arrMail.Length; i++)
            {
                if (arrMail[i] != "")
                {
                    boolMail = CheckEmailFormat(arrMail[i].Trim());
                    if (boolMail == false)
                    {
                        boolCheckMail = false;
                    }
                    else
                    {
                        //Check sub Email format
                        checkMailFormat = arrMail[i].Split(' ');
                        foreach (string mail in checkMailFormat)
                        {
                            if (mail != "")
                            {
                                boolMail = CheckEmailFormat(mail);
                                //Set Email format
                                if (boolMail == true)
                                {
                                    if (arrMail[i].Contains(";") == false)
                                    {
                                        oMail = oMail + arrMail[i].Trim() + ";";
                                    }
                                    else
                                    {
                                        oMail = oMail + arrMail[i].Trim();
                                    }
                                }
                                else
                                {
                                    boolCheckMail = false;
                                }
                            }
                        }
                    }

                }
            }
            conn = new SqlConnection(Properties.Settings.Default.Conn);
            conn.Open();
            try
            {
                if (boolCheckMail == true && boolGmMail == true && boolCheckName == true && boolCheckVendor == true)
                {
                    //Set GM_Email format
                    CC = GmMail + "; " + logonEmail + ";";
                    //Create IncidentNo
                    sql.Append("spCreateAPP_ID");
                    sqlcmd = new SqlCommand(sql.ToString(), conn);
                    sqlcmd.Parameters.AddWithValue("Year", yearstamp);
                    sqlcmd.Parameters.AddWithValue("Month", monthstamp);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    oDr = sqlcmd.ExecuteReader();
                    while (oDr.Read())
                    {
                        runningNo = oDr["Running"].ToString();
                    }
                    oDr.Close();
                    string Id = runningNo.PadLeft(4, '0'); // set format 0000
                    string AppID = "A" + yearstamp + monthstamp + "-" + Id;
                    var EncodeID = Convert.ToBase64String(Encoding.UTF8.GetBytes(AppID));
                    if (AppID != null)
                    {
                        var Timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
                        string Detail = "Form No. " + AppID + " has been created by " + logonName;
                        string Detail2 = "Form has been sent email to " + VendorName;
                        string Status = "P";

                        sqlcmd = new SqlCommand("spInsertSupplier", conn);
                        sqlcmd.Parameters.AddWithValue("Id", AppID);
                        sqlcmd.Parameters.AddWithValue("VendorName", VendorName);
                        sqlcmd.Parameters.AddWithValue("VendorPIC", oName);
                        sqlcmd.Parameters.AddWithValue("Email", oMail);
                        sqlcmd.Parameters.AddWithValue("Status", Status);
                        sqlcmd.Parameters.AddWithValue("Timestamp", Timestamp);
                        sqlcmd.Parameters.AddWithValue("Detail", Detail);
                        sqlcmd.Parameters.AddWithValue("Detail2", Detail2);
                        sqlcmd.Parameters.AddWithValue("CC", GmMail);
                        sqlcmd.Parameters.AddWithValue("CreateBy", logonID);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        oDr = sqlcmd.ExecuteReader();
                        oDr.Read();


                        sendEmail(EncodeID, oName, oMail, logonEmail, CC);
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oSubmit('success', 'Success', 'Request success, Value has inserted.', '" + EncodeID + "');", true);

                        //sendEmail(string AppID, string vendor_PIC, string emailTo, string emailFrom, string GM)

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Submit Fail', 'Request success, Value has inserted.', '" + EncodeID + "');", true);
                    }

                }
                else
                {
                    if (boolCheckMail == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'Email is not correct, Please try again.');", true);
                    }
                    if (boolGmMail == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'CC Email is not correct, Please try again.');", true);
                    }
                    if (boolCheckName == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'PIC format is not correct, Please try again.');", true);
                    }
                    if (boolCheckVendor == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Vendor Name format is not correct, Please try again.');", true);
                    }
                }
                //ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('success', 'Success', 'Email is correct.');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', '" + ex.Message + "');", true);
            }
            finally
            {
                conn.Close();
            }
        }
        protected void Draft_Click(object sender, EventArgs e)
        {
            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataReader oDr;

            if (conn.State == ConnectionState.Open)
                conn.Close();

            string VendorName = Vendor_Name.Value;
            string PIC = Vendor_PIC.Value;
            string Mail = Email.Value;
            string GmMail = GM_Email.Value;
            string[] arrMail = Mail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            string[] checkMailFormat = null;
            string[] arrGmMail = Mail.Split(' ');
            string oMail = null;
            string logonName = Request.Cookies.Get("FullName").Value;
            string logonEmail = Request.Cookies.Get("Email").Value;
            string logonID = Request.Cookies.Get("EmployeeID").Value;
            string yearstamp = DateTime.Now.ToString("yyyy");
            string monthstamp = DateTime.Now.ToString("MM");
            string runningNo = null;
            string CC = null;
            bool boolGmMail = CheckEmailFormat(GmMail);
            bool boolMail = false;
            bool boolCheckMail = true;

            bool boolGetName = CheckNameFormat(PIC);
            bool boolCheckName = true;
            bool boolCheckVendor = CheckVendorName(VendorName);
            string[] names = null;
            string oName = null;
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
            //Check Email format
            for (int i = 0; i < arrMail.Length; i++)
            {
                if (arrMail[i] != "")
                {
                    boolMail = CheckEmailFormat(arrMail[i].Replace(" ", ""));
                    if (boolMail == false)
                    {
                        boolCheckMail = false;
                    }
                    else
                    {
                        checkMailFormat = arrMail[i].Split(' ');
                        foreach (string mail in checkMailFormat)
                        {
                            if (mail != "")
                            {
                                boolMail = CheckEmailFormat(mail);
                                //Set Email format
                                if (boolMail == true)
                                {
                                    if (arrMail[i].Contains(";") == false)
                                    {
                                        oMail = oMail + arrMail[i].Trim() + ";";
                                    }
                                    else
                                    {
                                        oMail = oMail + arrMail[i].Trim();
                                    }
                                }
                                else
                                {
                                    boolCheckMail = false;
                                }
                            }
                        }
                    }
                }
            }

            conn = new SqlConnection(Properties.Settings.Default.Conn);
            conn.Open();

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
            if (GmMail == "")
            {
                GmMail = "";
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
            if (GmMail != "")
            {
                if (boolGmMail == false)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'CC Email is not correct, Please try again.');", true);
                    
                }
            }

            //if (boolCheckMail == true && boolGmMail == true && boolCheckName == true && boolCheckVendor == true)
            //{
                try
                {

                    //Set GM_Email format
                    CC = GmMail + "; " + logonEmail + ";";
                    //Create IncidentNo
                    sql.Append("spCreateAPP_ID");
                    sqlcmd = new SqlCommand(sql.ToString(), conn);
                    sqlcmd.Parameters.AddWithValue("Year", yearstamp);
                    sqlcmd.Parameters.AddWithValue("Month", monthstamp);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                
                    oDr = sqlcmd.ExecuteReader();
                    while (oDr.Read())
                    {
                        runningNo = oDr["Running"].ToString();
                    }
                    oDr.Close();
                    string Id = runningNo.PadLeft(4, '0'); // set format 0000
                    string AppID = "A" + yearstamp + monthstamp + "-" + Id;
                    if (AppID != null)
                    {
                        var Timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
                        string Detail = "Form No. " + AppID + " has created by " + logonName;
                        string Detail2 = "Draft has been saved by " + logonName;
                        string Status = "D";

                        sqlcmd = new SqlCommand("spInsertSupplier", conn);
                        sqlcmd.Parameters.AddWithValue("Id", AppID);
                        sqlcmd.Parameters.AddWithValue("VendorName", VendorName);
                        sqlcmd.Parameters.AddWithValue("VendorPIC", oName);
                        sqlcmd.Parameters.AddWithValue("Email", oMail);
                        sqlcmd.Parameters.AddWithValue("Status", Status);
                        sqlcmd.Parameters.AddWithValue("Timestamp", Timestamp);
                        sqlcmd.Parameters.AddWithValue("Detail", Detail);
                        sqlcmd.Parameters.AddWithValue("Detail2", Detail2);
                        sqlcmd.Parameters.AddWithValue("CC", GmMail);
                        sqlcmd.Parameters.AddWithValue("CreateBy", logonID);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        oDr = sqlcmd.ExecuteReader();
                        while (oDr.Read())
                        {

                        }
                        var EncodeID = Convert.ToBase64String(Encoding.UTF8.GetBytes(AppID));

                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oSaveDraft('success', 'Success', 'Request success, Value has inserted.', '" + EncodeID + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Save Draft Fail', 'App ID is null, Please try again.');", true);
                    }


                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', '" + ex.Message + "');", true);
                }
                finally
                {
                    conn.Close();
                }
            //}
            //else
            //{
            //    if (boolCheckMail == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'Email is not correct, Please try again.');", true);
            //    }
            //    if (boolGmMail == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'GM Email is not correct, Please try again.');", true);
            //    }
            //    if (boolCheckName == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'Vendor PIC format is not correct, Please try again.');", true);
            //    }
            //    if (boolCheckVendor == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Error', 'Vendor Name format is not correct, Please try again.');", true);
            //    }
            //    //ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('success', 'Success', 'Email is correct.');", true);
            //}

        }
    }
}