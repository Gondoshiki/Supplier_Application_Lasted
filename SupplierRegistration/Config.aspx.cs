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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupplierRegistration
{
    public partial class Config : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void UploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                int countfile = 0;
                if (Int32.TryParse(hdfFile.Value, out int Temp))
                    countfile = Temp;

                if (countfile >= 1)
                {
                    //var timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                    string folder = FolderName.Value;
                    string file = fileUpload.Value;
                    //string subfolder = id.Substring(1, 6);
                    string Path1 = Server.MapPath("Document\\fileAttach\\form\\" + folder);
                    string pattern = @"^[a-zA-Z0-9ก-๙!@#$%^&*<>""'']$";
                    //string pattern = @"^[a-zA-Z0-9ก-๙!@#$%^&*+=:;{}]";
                    if (Regex.IsMatch(file, pattern) == false)
                    {
                        if (Directory.Exists(Path1) == false)
                        {
                            Directory.CreateDirectory(Path1);
                            HttpPostedFile oFile = null;
                            if (Directory.Exists(Path1) == true)
                            {
                                for (int i = 0; i < countfile; i++)
                                {
                                    oFile = Context.Request.Files[i];
                                    //string filename = Uri.EscapeUriString(oFile.FileName);
                                    //string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                                    //Context.Response.Write(filename);                            
                                    if (i == 0)
                                    {
                                        //Context.Response.Write(Server.MapPath("Document\\fileAttach\\form\\" + folder + "\\" + filename));
                                        oFile.SaveAs(Server.MapPath("Document\\fileAttach\\form\\" + folder + "\\" + oFile.FileName));
                                    }
                                }
                                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oConfig('success', 'Success', 'Upload success, Value has inserted.');", true);


                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Upload Fail', 'Can not create folder, Please try again.');", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Upload fail', 'Have a same folder name');", true);
                            //string Path1 = Server.MapPath("Document\\fileAttach\\form\\");
                            //if (Directory.Exists(Path1) == false)
                            //{
                            //    Directory.CreateDirectory(Path1);
                            //}
                            //else
                            //{
                            //    string Path2 = Server.MapPath("Document\\fileAttach\\form\\" + folder);
                            //    if (Directory.Exists(Path2) == false)
                            //    {
                            //        Directory.CreateDirectory(Path2);
                            //    }
                            //    else
                            //    {
                            //        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Upload fail', 'Have a same folder name');", true);
                            //    }
                            //}

                            //Directory.Delete(oPath, true);
                            //Directory.CreateDirectory(oPath);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Upload Fail', 'File name is invalid, Please try again.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Upload fail', 'No have file upload');", true);
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('error', 'Somthing Wrong', 'Upload Failed, Please upload again.');", true);
            }
            finally
            {

            }
        }
        protected void Auth_Click(object sender, EventArgs e)
        {
            //Database Connect
            //oDT = DataTable
            //oDr = DataReader
            //oDa = DataAdapter
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;

            string Password = null;
            string vendor_PIC = null;
            string EmpID = EmployeeID.Value;
            string emailTo = empEmail.Value;
            string PhoneNo = Phone.Value;
            GetRandomPassword(20);
            string TokenNum = hdfToken.Value;
            var AppID = Convert.ToBase64String(Encoding.UTF8.GetBytes(EmpID));
            var TokenID = Convert.ToBase64String(Encoding.UTF8.GetBytes(TokenNum));
            hdfTokenEncode.Value = TokenID;
            hdfAppIDEncode.Value = AppID;

            //Get AppID
            //string CreateBy = Request.Cookies.Get("EmployeeId").Value;
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                //กำหนดตัวแปร
                string Auth = "PS";
                var Timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                // Add Values
                sqlcmd = new SqlCommand("spInsertAuth", conn);
                sqlcmd.Parameters.AddWithValue("ID", EmpID);
                //sqlcmd.Parameters.AddWithValue("Password", Password);
                sqlcmd.Parameters.AddWithValue("TokenID", TokenNum);
                sqlcmd.Parameters.AddWithValue("Phone", PhoneNo);
                sqlcmd.Parameters.AddWithValue("Timestamp", Timestamp);
                sqlcmd.Parameters.AddWithValue("Auth", Auth);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                oDa = new SqlDataAdapter(sqlcmd);
                oDa.Fill(oDt);

                if (oDt.Rows.Count > 0)
                {
                    //vendor_PIC = oDt.Rows[0]["FullName"].ToString();
                    //Password = oDt.Rows[0]["Password"].ToString();
                    if (oDt.Rows[0]["Result"].ToString() == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Add Authorize fail', 'Found Employee ID : " + EmpID + " in Database');", true);
                    }
                    else
                    {
                        if (oDt.Rows[0]["Result"].ToString() == "1")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Add Authorize fail', 'Can not find Employee in PummSoft, Please Try Again.');", true);

                        }
                        else if (oDt.Rows[0]["Result"].ToString() == "2")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Add Authorize fail', 'Can not Find Email in PummSoft, Please try again.');", true);
                        }
                        else
                        {
                            sendEmail(AppID);
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oConfig('success', 'Success', 'Add Authorize success, Value has inserted.');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'Something went worng, Please try again.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('warning', 'Something went wrong', '" + ex.Message + "');", true);
            }
            finally
            {
                conn.Close();
            }

        }

        protected void User_Check(object sender, EventArgs e)
        {
            //Database Connect
            //oDT = DataTable
            //oDr = DataReader
            //oDa = DataAdapter
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;


            string Password = null;
            string EmpID = EmployeeID.Value;
            string emailTo = empEmail.Value;
            string PhoneNo = Phone.Value;

            //Get AppID
            //string CreateBy = Request.Cookies.Get("EmployeeId").Value;
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                //กำหนดตัวแปร
                string Auth = "PS";
                var Timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                // Add Values
                sqlcmd = new SqlCommand("spInsertAuth", conn);
                sqlcmd.Parameters.AddWithValue("ID", EmpID);
                sqlcmd.Parameters.AddWithValue("Password", Password);
                sqlcmd.Parameters.AddWithValue("Phone", PhoneNo);
                sqlcmd.Parameters.AddWithValue("Timestamp", Timestamp);
                sqlcmd.Parameters.AddWithValue("Auth", Auth);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                oDa = new SqlDataAdapter(sqlcmd);
                oDa.Fill(oDt);

                if (oDt.Rows.Count > 0)
                {
                    //vendor_PIC = oDt.Rows[0]["FullName"].ToString();
                    //Password = oDt.Rows[0]["Password"].ToString();
                    if (oDt.Rows[0]["Result"].ToString() == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Add Authorize fail', 'Found Employee ID : " + EmpID + " in Database');", true);
                    }
                    else
                    {
                        if (oDt.Rows[0]["Result"].ToString() == "1")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Add Authorize fail', 'Can not find Employee in PummSoft, Please Try Again.');", true);

                        }
                        else if (oDt.Rows[0]["Result"].ToString() == "2")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Add Authorize fail', 'Can not Find Email in PummSoft, Please try again.');", true);
                        }
                        else
                        {
                            sendEmail(EmpID);
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oConfig('success', 'Success', 'Add Authorize success, Value has inserted.');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Fail', 'Something went worng, Please try again.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('warning', 'Something went wrong', '" + ex.Message + "');", true);
            }
            finally
            {
                conn.Close();
            }

        }
        //protected void ResetPassword_Click()
        //{
        //    string AppID = hdfEmpID.Value;
        //    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oResetPassword('success', 'Reset success', 'Reset Password Link has been sent to your email.');", true);
        //    //sendEmailResetPassword(AppID);
        //}
        public void sendEmailResetPassword_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;
            string TokenID = null;
            string vendor_PIC = null;
            string EmpID = null;
            string emailTo = null;
            EmpID = hdfEmpID.Value;


            //Get AppID
            //string CreateBy = Request.Cookies.Get("EmployeeId").Value;
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                sqlcmd = new SqlCommand("spResetPassword", conn);
                sqlcmd.Parameters.AddWithValue("ID", EmpID);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                oDa = new SqlDataAdapter(sqlcmd);
                oDa.Fill(oDt);
                //Response.Write("Dear : " + vendor_PIC + "\n" + "Email : " + emailTo);
                if (oDt.Rows.Count > 0)
                {
                    hdfEmailTo.Value = oDt.Rows[0]["Email"].ToString();
                    hdfNameTo.Value = oDt.Rows[0]["FullName"].ToString();
                    hdfToken.Value = oDt.Rows[0]["TokenID"].ToString();
                    EmpID = hdfEmpID.Value;
                    emailTo = hdfEmailTo.Value;
                    vendor_PIC = hdfNameTo.Value;
                    TokenID = hdfToken.Value;

                    string IDEncode = Convert.ToBase64String(Encoding.UTF8.GetBytes(EmpID));
                    string TokenEncode = Convert.ToBase64String(Encoding.UTF8.GetBytes(TokenID));
                    MailMessage myMail = new MailMessage();
                    WebClient Client = new WebClient();
                    string BodyMail;
                    string Header;





                    String Body = "<img src=\"https://career.hinothailand.com/Career/Images/companylogo.png\" style=\"width:125px; height:125px;\" /> ";

                    string logopath = Server.MapPath("Template\\image001.jpg");

                    string link = "https://hinommt.com/SupplierApplication/PS_ResetPassword.aspx?token=" + TokenEncode + "&id=" + IDEncode;
                    string Emp_Phone = Request.Cookies.Get("Phone").Value;
                    string Emp_Name = Request.Cookies.Get("FullName").Value;
                    string emailFrom = Request.Cookies.Get("Email").Value;
                    string[] MailTo = emailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < MailTo.Length; i++)
                    {
                        myMail.To.Add(new MailAddress(emailTo));
                    }
                    myMail.From = new MailAddress(emailFrom);
                    myMail.BodyEncoding = Encoding.UTF8;
                    Header = "| Supplier Application system |";
                    BodyMail = Client.DownloadString(Server.MapPath("Template/Reset_PasswordTemplate.htm"));
                    BodyMail = BodyMail.Replace("#NameTo", vendor_PIC);
                    BodyMail = BodyMail.Replace("#link", link);
                    BodyMail = BodyMail.Replace("#ID", EmpID);
                    BodyMail = BodyMail.Replace("#Token", TokenID);

                    // BodyMail = BodyMail.Replace("#image1", Body);
                    BodyMail = BodyMail.Replace("#image", Body);
                    BodyMail = BodyMail.Replace("#EmployeeName", Emp_Name);
                    BodyMail = BodyMail.Replace("#Phone", Emp_Phone);

                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('success', 'Reset Success', 'Reset Password Link has been sent to your email.');", true);

                    //string emailTo = "Prompiriya_S@hinothailand.com";






                    AlternateView HtmlView;
                    HtmlView = AlternateView.CreateAlternateViewFromString(BodyMail, null, "text/html");
                    myMail.AlternateViews.Add(HtmlView);
                    myMail.Subject = Header;
                    SmtpClient Clients = new SmtpClient();
                    Clients.Host = Properties.Settings.Default.Ipmail;
                    Clients.Send(myMail);

                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('warning', 'Reset Error', 'Can not find your ID.');", true);

                }
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

        public void sendEmail(string AppID)
        {
            //Response.Write("Dear : " + vendor_PIC + "\n" + "Email : " + emailTo);
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;


            MailMessage myMail = new MailMessage();
            WebClient Client = new WebClient();
            string BodyMail;
            string Header;
            string Emp_Phone = Request.Cookies.Get("Phone").Value;
            string Emp_Name = Request.Cookies.Get("FullName").Value;
            string emailFrom = Request.Cookies.Get("Email").Value;
            string emailTo = null;
            string Password = null;
            string TokenNum = null;
            string Vendor_Name = null;
            string Emp_ID = null;
            string TokenID = hdfTokenEncode.Value;
            //string emailTo = "Prompiriya_S@hinothailand.com";
            //string EncodeID = null;
            if (TokenID != null)
            {
                var base64Bytes = System.Convert.FromBase64String(TokenID);
                //Context.Response.Write(base64Bytes);
                TokenNum = System.Text.Encoding.UTF8.GetString(base64Bytes);
                if (AppID != null)
                {
                    base64Bytes = System.Convert.FromBase64String(AppID);
                    //Context.Response.Write(base64Bytes);
                    Emp_ID = System.Text.Encoding.UTF8.GetString(base64Bytes);

                    try
                    {
                        String Body = "<img src=\"https://career.hinothailand.com/Career/Images/companylogo.png\" style=\"width:125px; height:125px;\" /> ";
                        string link = "https://hinommt.com/SupplierApplication/PS_Register.aspx?token=" + TokenID + "&id=" + AppID;

                        if (conn.State == ConnectionState.Open)
                            conn.Close();

                        conn = new SqlConnection(Properties.Settings.Default.Conn);
                        conn.Open();
                        //Create IncidentNo
                        sql.Append("spCheckUser");
                        sqlcmd = new SqlCommand(sql.ToString(), conn);
                        sqlcmd.Parameters.AddWithValue("ID", Emp_ID);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        oDa = new SqlDataAdapter(sqlcmd);
                        oDa.Fill(oDt);
                        if (oDt.Rows.Count > 0)
                        {
                            hdfEmailTo.Value = oDt.Rows[0]["Email"].ToString();
                            hdfNameTo.Value = oDt.Rows[0]["FullName"].ToString();
                            Vendor_Name = hdfNameTo.Value;
                            emailTo = hdfEmailTo.Value;
                            string[] MailTo = emailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                            for (int i = 0; i < MailTo.Length; i++)
                            {
                                myMail.To.Add(new MailAddress(emailTo));
                            }


                            myMail.From = new MailAddress(emailFrom);

                            myMail.BodyEncoding = Encoding.UTF8;

                            Header = "| Supplier Application system |";
                            BodyMail = Client.DownloadString(Server.MapPath("Template/AddUserTemplate.htm"));
                            BodyMail = BodyMail.Replace("#NameTo", Vendor_Name);
                            BodyMail = BodyMail.Replace("#link", link);
                            BodyMail = BodyMail.Replace("#ID", Emp_ID);
                            BodyMail = BodyMail.Replace("#Token", TokenNum);
                            BodyMail = BodyMail.Replace("#image", Body);
                            BodyMail = BodyMail.Replace("#EmployeeName", Emp_Name);
                            BodyMail = BodyMail.Replace("#Phone", Emp_Phone);



                            AlternateView HtmlView;
                            HtmlView = AlternateView.CreateAlternateViewFromString(BodyMail, null, "text/html");
                            myMail.AlternateViews.Add(HtmlView);
                            myMail.Subject = Header;
                            SmtpClient Clients = new SmtpClient();
                            Clients.Host = Properties.Settings.Default.Ipmail;
                            Clients.Send(myMail);
                        }
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
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('error', 'Send Email Fail', 'Employee ID not found');", true);

                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oResoFileUploadedet('error', 'Send Email Fail', 'Not Found Token');", true);

            }

        }

        protected void EditAuth_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;

            if (conn.State == ConnectionState.Open)
                conn.Close();

            string empId = Request.Form[empID.UniqueID];
            string phone = Request.Form[empPhone.UniqueID];
            string email = Request.Form[empEmail.UniqueID];

            string[] words = null;
            string oMail = null;
            string[] checkMailFormat = null;
            bool boolMail = false;
            bool boolCheckMail = true;
            //CheckFormatEmail#1
            words = email.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
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
                                        oMail = fwords.Trim();
                                    }
                                    else
                                    {
                                        oMail = fwords.Trim();
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
                if (boolCheckMail == true)
                {


                    conn = new SqlConnection(Properties.Settings.Default.Conn);
                    conn.Open();
                    //กำหนดตัวแปร                
                    var Timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                    // Add Values
                    sqlcmd = new SqlCommand("spUpdateAuth", conn);
                    sqlcmd.Parameters.AddWithValue("ID", empId);
                    sqlcmd.Parameters.AddWithValue("Phone", phone);
                    sqlcmd.Parameters.AddWithValue("Timestamp", Timestamp);
                    sqlcmd.Parameters.AddWithValue("Email", oMail);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    oDa = new SqlDataAdapter(sqlcmd);
                    oDa.Fill(oDt);
                    if (oDt.Rows.Count > 0)
                    {
                        if (oDt.Rows[0]["Result"].ToString() == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Edit Fail', 'Insert the same value, Please try again.');", true);
                        }
                        else if (oDt.Rows[0]["Result"].ToString() == "1")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oConfig('success', 'Edit Success', 'Value has updated.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Edit Fail', 'Can not update value.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Edit Fail', 'E-mail Format invalid.');", true);
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

        protected void DeleteAuth_Click(object sender, EventArgs e)
        {
            //Database Connect
            //oDT = DataTable
            //oDr = DataReader
            //oDa = DataAdapter
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;
            SqlDataReader oDr;

            if (conn.State == ConnectionState.Open)
                conn.Close();

            string empId = hdfEmpID.Value;
            try
            {
                if (empId != null)
                {
                    conn = new SqlConnection(Properties.Settings.Default.Conn);
                    conn.Open();

                    // Add Values
                    sqlcmd = new SqlCommand("spDeleteSA_User", conn);
                    sqlcmd.Parameters.AddWithValue("ID", empId);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    oDa = new SqlDataAdapter(sqlcmd);
                    oDa.Fill(oDt);
                    if (oDt.Rows.Count > 0)
                    {
                        if (oDt.Rows[0]["Result"].ToString() == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Delete Fail', 'Not found Employee ID, Please try again.');", true);
                        }
                        else if (oDt.Rows[0]["Result"].ToString() == "1")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oConfig('success', 'Delete Success', 'Employee ID : " + empId + " has deleted');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Edit Fail', 'Can not update value.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Delete Fail', 'Employee ID is null, Please try again.');", true);
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

        protected void EditFile_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataReader oDr;

            int countfile = 0;
            if (Int32.TryParse(hdfFile.Value, out int Temp))
                countfile = Temp;

            string oldfilename = hdfFileName.Value;
            string oldfolder = hdfFolderName.Value;
            string newfilename = inpEditfile.Value;
            string newfolder = inpEditfolder.Value;
            string[] linkFile = null;
            string oldPath = Server.MapPath("Document\\fileAttach\\form\\" + oldfolder);

            HttpPostedFile oFile = null;

            //Get AppID
            try
            {
                if (countfile > 0)
                {
                    if (Directory.Exists(oldPath) == true)
                    {
                        //string oldPathfile = Server.MapPath("Document\\fileAttach\\form\\" + oldfolder + "\\" + oldfilename);
                        if (File.Exists(Path.Combine(oldPath, oldfilename)) == true)
                        {
                            Directory.Delete(oldPath, true);
                            string newPath = Server.MapPath("Document\\fileAttach\\form\\" + newfolder);
                            Directory.CreateDirectory(newPath);
                            if (Directory.Exists(newPath) == true)
                            {
                                for (int i = countfile; i < countfile + 1; i++)
                                {
                                    oFile = Context.Request.Files[i];
                                    oFile.SaveAs(Server.MapPath("Document\\fileAttach\\form\\" + newfolder + "\\" + oFile.FileName));
                                }
                                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oConfig('success', 'Edit Success', 'Value has updated.');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Edit Fail', 'Can not create new folder,Please try again');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Edit Fail', 'Not found old file, Please try again');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Edit Fail', 'Not found old folder');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Edit Fail', 'no have a file upload');", true);
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

        protected void DeleteFile_Click(object sender, EventArgs e)
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

            string filename = hdfFileName.Value;
            string folder = hdfFolderName.Value;
            string[] linkFile = null;
            string path = Server.MapPath("Document\\fileAttach\\form\\" + folder);
            //Get AppID
            try
            {
                if (Directory.Exists(path) == true)
                {
                    string pathfile = Server.MapPath("Document\\fileAttach\\form\\" + folder + "\\" + filename);
                    if (File.Exists(pathfile) == true)
                    {
                        Directory.Delete(path, true);
                    }

                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oConfig('success', 'Delete Success', 'Value has Removed.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Delete Fail', 'Not found file folder');", true);
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

        public void GetRandomPassword(int length)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }
            hdfToken.Value = sb.ToString();
            Console.WriteLine(sb);
        }





    }


}

