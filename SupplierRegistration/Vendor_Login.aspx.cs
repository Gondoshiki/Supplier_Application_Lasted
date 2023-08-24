using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.IO;
using System.Web.Script.Services;
using System.Net.Mail;
using System.Net;

namespace SupplierRegistration
{
    public partial class Vendor_Login : System.Web.UI.Page
    {

        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[ScriptMethod(UseHttpGet = true)]
        protected void Page_Load(object sender, EventArgs e)
        {
            //string Controller = Request.Form["Controller"];
            //switch (Controller)
            //{
            //    case "SavePreview":
            //        //SavePreview_Click(); break;
            //        SavePreview();
            //        break;
            //    default:
            //        break;

            //}

        }
        protected void UploadLocal_Click(object sender, EventArgs e)
        {
            HttpPostedFile oFile = null;

            string id = hdfApp_ID.Value;
            string folder = id.Substring(1, 4);
            string subfolder = id.Substring(1, 6);
            int countfile = 0;
            if (Int32.TryParse(hdfFile.Value, out int Temp))
                countfile = Temp;
            var EncodeID = Convert.ToBase64String(Encoding.UTF8.GetBytes(id));

            
            string[] arrtempPath = null;
            string[] arrtempName = null;
            string[] arrtemp = null;

            string[] arrlocalfiles = null;
            string[] arrtempfiles = null;

            string otempName = null;
            string localPath = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id);
            string tempPath = Server.MapPath("Document\\fileAttach\\temp\\" + id);
            try
            {
                if (countfile > 0)
                {

                }
                else
                {
                    StringBuilder sql = new StringBuilder();
                    SqlConnection conn = new SqlConnection();
                    SqlCommand sqlcmd = new SqlCommand();
                    DataTable oDt = new DataTable();
                    SqlDataReader oDr;

                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn = new SqlConnection(Properties.Settings.Default.Conn);
                    conn.Open();

                    string status = "P";
                    string appStatus = hdfStatus.Value;
                    string detail = null;
                    var timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
                    if (appStatus == "P")
                    {
                        detail = id + " Files has been uploaded by Vendor";
                    }
                    else if (appStatus == "R")
                    {
                        detail = id + " The file has been edited and uploaded by Vendor";
                    }

                    sql.Append("spFileUpload");
                    sqlcmd = new SqlCommand(sql.ToString(), conn);
                    sqlcmd.Parameters.AddWithValue("Id", id);
                    sqlcmd.Parameters.AddWithValue("Status", status);
                    sqlcmd.Parameters.AddWithValue("Detail", detail);
                    sqlcmd.Parameters.AddWithValue("Update_Date", timestamp);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    oDr = sqlcmd.ExecuteReader();
                    while (oDr.Read())
                    {

                    }
                    oDr.Close();

                    StringBuilder sql1 = new StringBuilder();
                    SqlConnection conn1 = new SqlConnection();
                    SqlCommand sqlcmd1 = new SqlCommand();
                    DataTable oDt1 = new DataTable();
                    SqlDataAdapter oDa1;
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    string CreateBy = hdfCreateID.Value;
                    string FullName = null;
                    string Emp_Email = null;
                    string Emp_Phone = null;
                    string Venddor_Email = null;
                    conn1 = new SqlConnection(Properties.Settings.Default.Conn);
                    conn1.Open();
                    //Create IncidentNo
                    sql1.Append("spGetEmail");
                    sqlcmd1 = new SqlCommand(sql1.ToString(), conn1);
                    sqlcmd1.Parameters.AddWithValue("CreateID", CreateBy);
                    sqlcmd1.CommandType = CommandType.StoredProcedure;
                    oDa1 = new SqlDataAdapter(sqlcmd1);
                    oDa1.Fill(oDt1);
                    if (oDt1.Rows.Count > 0)
                    {
                        hdfName.Value = oDt1.Rows[0]["FullName"].ToString();
                        hdfCC.Value = oDt1.Rows[0]["Email"].ToString();
                        hdfPhone.Value = oDt1.Rows[0]["Phone"].ToString();
                    }

                    string Path1 = Server.MapPath("Document\\fileAttach\\" + folder);
                    string Path2 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder);
                    string Path3 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id);
                    if (Directory.Exists(localPath) == false)
                    {
                        Directory.CreateDirectory(localPath);
                    }
                    else
                    {
                        if (Directory.Exists(Path1) == false)
                        {
                            Directory.CreateDirectory(Path1);
                        }
                        else
                        {                           
                            if (Directory.Exists(Path2) == false)
                            {
                                Directory.CreateDirectory(Path2);
                            }
                            else
                            {                               
                                if (Directory.Exists(Path3) == false)
                                {
                                    Directory.CreateDirectory(Path2);
                                }
                            }
                        }                        
                    }

                    //Get File From Temporary folder
                    if (Directory.Exists(tempPath) == true)
                    {
                        arrtempPath = Directory.GetDirectories(tempPath);
                        //arrtempName = Path.GetDirectoryName(tempPath)

                        foreach (string otempPath in arrtempPath)
                        {
                            string tempFolderName = new DirectoryInfo(otempPath).Name;
                            if (Directory.Exists(localPath + "\\" + tempFolderName) == false)
                            {
                                Directory.CreateDirectory(localPath + "\\" + tempFolderName);
                                //arrlocalfiles = Directory.GetFiles(localPath + "\\" + tempFolderName);
                                //foreach(string ofile in arrlocalfiles)
                                //{
                                //    File.Delete(ofile);
                                //}
                                //arrtempfiles = Directory.GetFiles(otempPath);
                                //foreach(string otempfile in arrtempfiles)
                                //{
                                //    string filename = Path.GetFileName(otempfile);
                                //    File.Copy(otempfile, localPath + "\\" + tempFolderName + "\\" + filename, true);
                                //}
                                
                                //Directory.Delete(localPath + "\\" + tempFolderName, true);
                                //foreach (string DirectoryPath in Directory.GetDirectories(localPath + "\\" + tempFolderName))
                                //{
                                //    File.Delete(DirectoryPath);
                                //}                                
                            }
                            arrlocalfiles = Directory.GetFiles(localPath + "\\" + tempFolderName);
                            foreach (string ofile in arrlocalfiles)
                            {
                                File.Delete(ofile);
                            }
                            arrtempfiles = Directory.GetFiles(otempPath);
                            foreach (string otempfile in arrtempfiles)
                            {
                                string filename = Path.GetFileName(otempfile);
                                File.Copy(otempfile, localPath + "\\" + tempFolderName + "\\" + filename, true);
                            }
                            //Directory.Move(otempPath, localPath, true);
                            //File.Copy
                        }
                        Directory.Delete(tempPath, true);
                    }
                    FullName = hdfName.Value;
                    Emp_Email = hdfCC.Value;
                    Emp_Phone = hdfPhone.Value;
                    Venddor_Email = hdfMail.Value;

                    sendEmail(EncodeID, FullName, Emp_Email, Venddor_Email, Emp_Phone);

                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('success', 'Upload Sucess', 'file has inserted', '" + EncodeID + "');", true);
                }

            }
            catch (Exception ex)
            {

            }
        }
        [WebMethod]
        protected void SavePreview_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataReader oDr;

            HttpPostedFile oFile = null;

            string[] linkFile;

            string id = hdfApp_ID.Value;
            string folder = id.Substring(1, 4);
            string subfolder = id.Substring(1, 6);
            int countfile = 0;
            if (Int32.TryParse(hdfFile.Value, out int Temp))
                countfile = Temp;
            try
            {
                if (countfile > 0 && id != "")
                {
                    string Path = Server.MapPath("Document\\fileAttach\\temp\\" + id);
                    if (Directory.Exists(Path) == true)
                    {
                        if (inpFileApp.Value == "")
                        {
                            string pathAppForm = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Application_Form");
                            oFile = Context.Request.Files[0];
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathAppForm) == true)
                            {
                                foreach (string file in Directory.GetFiles(pathAppForm))
                                {
                                    File.Delete(file);
                                }
                            }
                            else
                            {
                                Directory.CreateDirectory(pathAppForm);
                            }
                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Application_Form\\" + filename));
                        }
                        if (inpFileRegisCert.Value != "")
                        {
                            oFile = Context.Request.Files[1];
                            string pathRegisCert = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Registration_Certificate");
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathRegisCert) == true)
                            {
                                foreach (string file in Directory.GetFiles(pathRegisCert))
                                {
                                    File.Delete(file);
                                }
                            }
                            else
                            {
                                Directory.CreateDirectory(pathRegisCert);
                            }
                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Registration_Certificate\\" + filename));
                        }
                        if (inpFilePP20.Value != "")
                        {
                            string pathPP20 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\PP20");
                            oFile = Context.Request.Files[2];
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathPP20) == true)
                            {
                                foreach (string file in Directory.GetFiles(pathPP20))
                                {
                                    File.Delete(file);
                                }
                            }
                            else
                            {
                                Directory.CreateDirectory(pathPP20);
                            }
                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\PP20\\" + filename));
                        }
                        if (inpFileBookBank.Value != "")
                        {
                            string pathBookBank = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Book-Bank");
                            oFile = Context.Request.Files[3];
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathBookBank) == true)
                            {
                                foreach (string file in Directory.GetFiles(pathBookBank))
                                {
                                    File.Delete(file);
                                }

                            }
                            else
                            {
                                Directory.CreateDirectory(pathBookBank);
                            }
                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Book-Bank\\" + filename));
                        }
                        if (inpFileBOJ5.Value != "")
                        {
                            string pathBOJ5 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\BOJ5");
                            oFile = Context.Request.Files[4];
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathBOJ5) == true)
                            {
                                foreach (string file in Directory.GetFiles(pathBOJ5))
                                {
                                    File.Delete(file);
                                }
                            }
                            else
                            {
                                Directory.CreateDirectory(pathBOJ5);
                            }
                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\BOJ5\\" + filename));
                        }
                        if (inpFileOrgCompany.Value != "")
                        {
                            string pathOrgCompany = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Organization_Company");
                            oFile = Context.Request.Files[5];
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathOrgCompany) == true)
                            {
                                foreach (string file in Directory.GetFiles(pathOrgCompany))
                                {
                                    File.Delete(file);
                                }
                            }
                            else
                            {
                                Directory.CreateDirectory(pathOrgCompany);
                            }
                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Organization_Company\\" + filename));
                        }
                        if (inpFileSPS10.Value != "")
                        {
                            oFile = Context.Request.Files[6];
                            string pathSPS10 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\SPS1-10");
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathSPS10) == true)
                            {
                                foreach (string file in Directory.GetFiles(pathSPS10))
                                {
                                    File.Delete(file);
                                }
                            }
                            else
                            {
                                Directory.CreateDirectory(pathSPS10);
                            }
                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\SPS1-10\\" + filename));
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(Path);
                        if (inpFileApp.Value != "")
                        {
                            string pathAppForm = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Application_Form");
                            Directory.CreateDirectory(pathAppForm);
                            oFile = Context.Request.Files[0];
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathAppForm) == true)
                            {
                                oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Application_Form\\" + filename));
                            }
                        }
                        if (inpFileRegisCert.Value != "")
                        {
                            oFile = Context.Request.Files[1];
                            string pathRegisCert = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Registration_Certificate");
                            Directory.CreateDirectory(pathRegisCert);
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathRegisCert) == true)
                            {
                                oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Registration_Certificate\\" + filename));
                            }

                        }
                        if (inpFilePP20.Value != "")
                        {
                            string pathPP20 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\PP20");
                            Directory.CreateDirectory(pathPP20);
                            oFile = Context.Request.Files[2];
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathPP20) == true)
                            {
                                oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\PP20\\" + filename));
                            }

                        }
                        if (inpFileBookBank.Value != "")
                        {
                            string pathBookBank = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Book-Bank");
                            Directory.CreateDirectory(pathBookBank);
                            oFile = Context.Request.Files[3];
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathBookBank) == true)
                            {
                                oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Book-Bank\\" + filename));

                            }

                        }
                        if (inpFileBOJ5.Value != "")
                        {
                            string pathBOJ5 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\BOJ5");
                            Directory.CreateDirectory(pathBOJ5);
                            oFile = Context.Request.Files[4];
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathBOJ5) == true)
                            {
                                oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\BOJ5\\" + filename));
                            }

                        }
                        if (inpFileOrgCompany.Value != "")
                        {
                            string pathOrgCompany = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Organization_Company");
                            Directory.CreateDirectory(pathOrgCompany);
                            oFile = Context.Request.Files[5];
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathOrgCompany) == true)
                            {
                                oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Organization_Company\\" + filename));
                            }

                        }
                        if (inpFileSPS10.Value != "")
                        {
                            oFile = Context.Request.Files[6];
                            string pathSPS10 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\SPS1-10");
                            Directory.CreateDirectory(pathSPS10);
                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathSPS10) == true)
                            {
                                oFile.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\SPS1-10\\" + filename));
                            }
                        }
                    }
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('success', 'Preview Success', 'Please continue.', '');", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('error', 'Upload Fail', 'no file chosen', '');", true);
                }

            }
            catch (Exception ex)
            {

            }
        }
        //protected void Upload_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string[] linkFile;
        //        string id = hdfApp_ID.Value;
        //        string folder = id.Substring(1, 4);
        //        string subfolder = id.Substring(1, 6);
        //        int countfile = 0;
        //        if (Int32.TryParse(hdfFile.Value, out int Temp))
        //            countfile = Temp;
        //        var EncodeID = Convert.ToBase64String(Encoding.UTF8.GetBytes(id));
        //        if (countfile > 0 && id != null)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            SqlConnection conn = new SqlConnection();
        //            SqlCommand sqlcmd = new SqlCommand();
        //            DataTable oDt = new DataTable();
        //            SqlDataReader oDr;

        //            HttpPostedFile oFile = null;

        //            if (conn.State == ConnectionState.Open)
        //                conn.Close();

        //            conn = new SqlConnection(Properties.Settings.Default.Conn);
        //            conn.Open();

        //            string status = "P";
        //            string appStatus = hdfStatus.Value;
        //            string detail = null;
        //            var timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
        //            if (appStatus == "P")
        //            {
        //                detail = id + " Files has been uploaded by Vendor";
        //            }
        //            else if (appStatus == "R")
        //            {
        //                detail = id + " The file has been edited and uploaded by Vendor";
        //            }

        //            sql.Append("spFileUpload");
        //            sqlcmd = new SqlCommand(sql.ToString(), conn);
        //            sqlcmd.Parameters.AddWithValue("Id", id);
        //            sqlcmd.Parameters.AddWithValue("Status", status);
        //            sqlcmd.Parameters.AddWithValue("Detail", detail);
        //            sqlcmd.Parameters.AddWithValue("Update_Date", timestamp);
        //            sqlcmd.CommandType = CommandType.StoredProcedure;
        //            oDr = sqlcmd.ExecuteReader();
        //            while (oDr.Read())
        //            {

        //            }
        //            oDr.Close();

        //            StringBuilder sql1 = new StringBuilder();
        //            SqlConnection conn1 = new SqlConnection();
        //            SqlCommand sqlcmd1 = new SqlCommand();
        //            DataTable oDt1 = new DataTable();
        //            SqlDataAdapter oDa1;
        //            if (conn.State == ConnectionState.Open)
        //                conn.Close();
        //            string CreateBy = hdfCreateID.Value;
        //            string FullName = null;
        //            string Emp_Email = null;
        //            string Emp_Phone = null;
        //            string Venddor_Email = null;
        //            conn1 = new SqlConnection(Properties.Settings.Default.Conn);
        //            conn1.Open();
        //            //Create IncidentNo
        //            sql1.Append("spGetEmail");
        //            sqlcmd1 = new SqlCommand(sql1.ToString(), conn1);
        //            sqlcmd1.Parameters.AddWithValue("CreateID", CreateBy);
        //            sqlcmd1.CommandType = CommandType.StoredProcedure;
        //            oDa1 = new SqlDataAdapter(sqlcmd1);
        //            oDa1.Fill(oDt1);
        //            if (oDt1.Rows.Count > 0)
        //            {
        //                hdfName.Value = oDt1.Rows[0]["FullName"].ToString();
        //                hdfCC.Value = oDt1.Rows[0]["Email"].ToString();
        //                hdfPhone.Value = oDt1.Rows[0]["Phone"].ToString();
        //            }

        //            string Path = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id);
        //            if (Directory.Exists(Path) == true)
        //            {
        //                if (inpFileApp.Value != "")
        //                {
        //                    string pathAppForm = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form");
        //                    oFile = Context.Request.Files[0];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    if (Directory.Exists(pathAppForm) == true)
        //                    {
        //                        foreach (string file in Directory.GetFiles(pathAppForm))
        //                        {
        //                            File.Delete(file);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(pathAppForm);
        //                    }
        //                    oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\" + filename));
        //                }
        //                if (inpFileRegisCert.Value != "")
        //                {
        //                    oFile = Context.Request.Files[1];
        //                    string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate");
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    if (Directory.Exists(pathRegisCert) == true)
        //                    {
        //                        foreach (string file in Directory.GetFiles(pathRegisCert))
        //                        {
        //                            File.Delete(file);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(pathRegisCert);
        //                    }
        //                    oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate\\" + filename));
        //                }
        //                if (inpFilePP20.Value != "")
        //                {
        //                    string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20");
        //                    oFile = Context.Request.Files[2];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    if (Directory.Exists(pathPP20) == true)
        //                    {
        //                        foreach (string file in Directory.GetFiles(pathPP20))
        //                        {
        //                            File.Delete(file);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(pathPP20);
        //                    }
        //                    oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20\\" + filename));
        //                }
        //                if (inpFileBookBank.Value != "")
        //                {
        //                    string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank");
        //                    oFile = Context.Request.Files[3];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    if (Directory.Exists(pathBookBank) == true)
        //                    {
        //                        foreach (string file in Directory.GetFiles(pathBookBank))
        //                        {
        //                            File.Delete(file);
        //                        }

        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(pathBookBank);
        //                    }
        //                    oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank\\" + filename));
        //                }
        //                if (inpFileBOJ5.Value != "")
        //                {
        //                    string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5");
        //                    oFile = Context.Request.Files[4];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    if (Directory.Exists(pathBOJ5) == true)
        //                    {
        //                        foreach (string file in Directory.GetFiles(pathBOJ5))
        //                        {
        //                            File.Delete(file);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(pathBOJ5);
        //                    }
        //                    oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5\\" + filename));
        //                }

        //                if (inpFileOrgCompany.Value != "")
        //                {
        //                    string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company");
        //                    oFile = Context.Request.Files[5];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    if (Directory.Exists(pathOrgCompany) == true)
        //                    {
        //                        foreach (string file in Directory.GetFiles(pathOrgCompany))
        //                        {
        //                            File.Delete(file);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(pathOrgCompany);
        //                    }
        //                    oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company\\" + filename));
        //                }
        //                if (inpFileSPS10.Value != "")
        //                {
        //                    oFile = Context.Request.Files[6];
        //                    string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10");
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    if (Directory.Exists(pathSPS10) == true)
        //                    {
        //                        foreach (string file in Directory.GetFiles(pathSPS10))
        //                        {
        //                            File.Delete(file);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Directory.CreateDirectory(pathSPS10);
        //                    }
        //                    oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10\\" + filename));
        //                }


        //                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('success', 'Upload Success', 'The value had inserted', '" + EncodeID + "');", true);
        //            }
        //            else
        //            {
        //                string Path1 = Server.MapPath("Document\\fileAttach\\" + folder);
        //                if (Directory.Exists(Path1) == false)
        //                {
        //                    Directory.CreateDirectory(Path1);
        //                }
        //                else
        //                {
        //                    string Path2 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder);
        //                    if (Directory.Exists(Path2) == false)
        //                    {
        //                        Directory.CreateDirectory(Path2);
        //                    }
        //                    else
        //                    {
        //                        string Path3 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id);
        //                        if (Directory.Exists(Path3) == false)
        //                        {
        //                            Directory.CreateDirectory(Path3);
        //                        }
        //                        string pathAppForm = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form");
        //                        string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5");
        //                        string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank");
        //                        string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company");
        //                        string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20");
        //                        string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate");
        //                        string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10");
        //                        if (inpFileApp.Value != "")
        //                        {
        //                            oFile = Context.Request.Files[0];
        //                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                            if (Directory.Exists(pathAppForm) == true)
        //                            {
        //                                foreach (string file in Directory.GetFiles(pathAppForm))
        //                                {
        //                                    File.Delete(file);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Directory.CreateDirectory(pathAppForm);
        //                            }
        //                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\" + filename));
        //                        }
        //                        if (inpFileRegisCert.Value != "")
        //                        {
        //                            oFile = Context.Request.Files[1];
        //                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                            if (Directory.Exists(pathRegisCert) == true)
        //                            {
        //                                foreach (string file in Directory.GetFiles(pathRegisCert))
        //                                {
        //                                    File.Delete(file);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Directory.CreateDirectory(pathRegisCert);
        //                            }
        //                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate\\" + filename));
        //                        }
        //                        if (inpFilePP20.Value != "")
        //                        {
        //                            oFile = Context.Request.Files[2];
        //                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                            if (Directory.Exists(pathPP20) == true)
        //                            {
        //                                foreach (string file in Directory.GetFiles(pathPP20))
        //                                {
        //                                    File.Delete(file);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Directory.CreateDirectory(pathPP20);
        //                            }
        //                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20\\" + filename));
        //                        }
        //                        if (inpFileBookBank.Value != "")
        //                        {
        //                            oFile = Context.Request.Files[3];
        //                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                            if (Directory.Exists(pathBookBank) == true)
        //                            {
        //                                foreach (string file in Directory.GetFiles(pathBookBank))
        //                                {
        //                                    File.Delete(file);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Directory.CreateDirectory(pathBookBank);
        //                            }
        //                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank\\" + filename));
        //                        }
        //                        if (inpFileBOJ5.Value != "")
        //                        {

        //                            oFile = Context.Request.Files[4];
        //                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                            if (Directory.Exists(pathBOJ5) == true)
        //                            {
        //                                foreach (string file in Directory.GetFiles(pathBOJ5))
        //                                {
        //                                    File.Delete(file);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Directory.CreateDirectory(pathBOJ5);
        //                            }
        //                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5\\" + filename));
        //                        }
        //                        if (inpFileOrgCompany.Value != "")
        //                        {
        //                            oFile = Context.Request.Files[5];
        //                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                            if (Directory.Exists(pathOrgCompany) == true)
        //                            {
        //                                foreach (string file in Directory.GetFiles(pathOrgCompany))
        //                                {
        //                                    File.Delete(file);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Directory.CreateDirectory(pathOrgCompany);
        //                            }
        //                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company\\" + filename));
        //                        }
        //                        if (inpFileSPS10.Value != "")
        //                        {
        //                            oFile = Context.Request.Files[6];
        //                            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                            if (Directory.Exists(pathSPS10) == true)
        //                            {
        //                                foreach (string file in Directory.GetFiles(pathSPS10))
        //                                {
        //                                    File.Delete(file);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Directory.CreateDirectory(pathSPS10);
        //                            }
        //                            oFile.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10\\" + filename));
        //                        }

        //                    }
        //                }
        //            }
        //            //string PIC = lbVendorPIC.Text;
        //            //string oMail = lbEmail.Text;
        //            //string logonEmail = hdflogonMail.Value;
        //            //string CC = hdfCC.Value + ";" + logonEmail + ";";
        //            //string FullName = hdfName.Value;
        //            //string FPhone = hdfPhone.Value;
        //            //Set GM_Email format

        //            FullName = hdfName.Value;
        //            Emp_Email = hdfCC.Value;
        //            Emp_Phone = hdfPhone.Value;
        //            Venddor_Email = hdfMail.Value;

        //            sendEmail(EncodeID, FullName, Emp_Email, Venddor_Email, Emp_Phone);
        //            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('success', 'Upload Sucess', 'file has inserted', '" + EncodeID + "');", true);
        //        }
        //        else
        //        {
        //            if (countfile <= 0)
        //            {
        //                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('error', 'Upload Fail', 'no files', '" + EncodeID + "');", true);
        //            }
        //            if (id == null)
        //            {
        //                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('error', 'Upload Fail', 'not found ID', '" + EncodeID + "');", true);
        //            }

        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
        //    }
        //}
        //protected void Upload_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        StringBuilder sql = new StringBuilder();
        //        SqlConnection conn = new SqlConnection();
        //        SqlCommand sqlcmd = new SqlCommand();
        //        DataTable oDt = new DataTable();
        //        SqlDataReader oDr;

        //        int countfile = 0;
        //        if (Int32.TryParse(hdfFile.Value, out int Temp))
        //            countfile = Temp;

        //        if (countfile >= 1)
        //        {

        //            if (conn.State == ConnectionState.Open)
        //                conn.Close();

        //            conn = new SqlConnection(Properties.Settings.Default.Conn);
        //            conn.Open();

        //            string id = hdfApp_ID.Value;
        //            string status = "P";
        //            var timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
        //            string detail = id + "was uploaded by Vendor";

        //            HttpPostedFile oFile = null;
        //            //Create IncidentNo
        //            sql.Append("spFileUpload");
        //            sqlcmd = new SqlCommand(sql.ToString(), conn);
        //            sqlcmd.Parameters.AddWithValue("Id", id);
        //            sqlcmd.Parameters.AddWithValue("Status", status);
        //            sqlcmd.Parameters.AddWithValue("Detail", detail);
        //            sqlcmd.Parameters.AddWithValue("Update_Date", timestamp);
        //            sqlcmd.CommandType = CommandType.StoredProcedure;
        //            oDr = sqlcmd.ExecuteReader();
        //            while (oDr.Read())
        //            {

        //            }
        //            oDr.Close();

        //            string folder = id.Substring(1, 4);
        //            string subfolder = id.Substring(1, 6);
        //            string Path = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id);

        //            if (Directory.Exists(Path) == false)
        //            {
        //                string pathAppForm = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form");
        //                string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5");
        //                string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank");
        //                string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company");
        //                string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20");
        //                string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate");
        //                string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10");

        //                if (Directory.Exists(pathAppForm) == false)
        //                {
        //                    oFile = Context.Request.Files[0];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form"));
        //                    oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\" + filename));
        //                }
        //                if (Directory.Exists(pathBOJ5) == false)
        //                {
        //                    oFile = Context.Request.Files[1];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5"));
        //                    oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5\\" + filename));
        //                }
        //                if (Directory.Exists(pathBookBank) == false)
        //                {
        //                    oFile = Context.Request.Files[2];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank"));
        //                    oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank\\" + filename));
        //                }
        //                if (Directory.Exists(pathOrgCompany) == false)
        //                {
        //                    oFile = Context.Request.Files[3];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company"));
        //                    oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company\\" + filename));
        //                }
        //                if (Directory.Exists(pathPP20) == false)
        //                {
        //                    oFile = Context.Request.Files[4];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20"));
        //                    oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20\\" + filename));
        //                }
        //                if (Directory.Exists(pathRegisCert) == false)
        //                {
        //                    oFile = Context.Request.Files[5];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate"));
        //                    oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate\\" + filename));
        //                }
        //                if (Directory.Exists(pathSPS10) == false)
        //                {
        //                    oFile = Context.Request.Files[6];
        //                    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                    Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10"));
        //                    oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10\\" + filename));
        //                }

        //            }
        //            else
        //            {
        //                string Path1 = Server.MapPath("Document\\fileAttach\\" + folder);
        //                if (Directory.Exists(Path1) == false)
        //                {
        //                    Directory.CreateDirectory(Path1);
        //                }
        //                else
        //                {
        //                    string Path2 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder);
        //                    if (Directory.Exists(Path2) == false)
        //                    {
        //                        Directory.CreateDirectory(Path2);
        //                    }
        //                    else
        //                    {
        //                        string Path3 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id);
        //                        if (Directory.Exists(Path3) == false)
        //                        {
        //                            Directory.CreateDirectory(Path3);                                    
        //                        }
        //                        else
        //                        {
        //                            string pathAppForm = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form");
        //                            string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5");
        //                            string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank");
        //                            string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company");
        //                            string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20");
        //                            string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate");
        //                            string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10");

        //                            if(Directory.Exists(pathAppForm) == false)
        //                            {
        //                                oFile = Context.Request.Files[0];
        //                                string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                                Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form"));
        //                                oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\" + filename));
        //                            }
        //                            if (Directory.Exists(pathBOJ5) == false)
        //                            {
        //                                oFile = Context.Request.Files[0];
        //                                string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                                Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5"));
        //                                oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5\\" + filename));
        //                            }
        //                            if (Directory.Exists(pathBookBank) == false)
        //                            {
        //                                oFile = Context.Request.Files[0];
        //                                string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                                Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank"));
        //                                oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank\\" + filename));
        //                            }
        //                            if (Directory.Exists(pathOrgCompany) == false)
        //                            {
        //                                oFile = Context.Request.Files[0];
        //                                string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                                Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company"));
        //                                oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company\\" + filename));
        //                            }
        //                            if (Directory.Exists(pathPP20) == false)
        //                            {
        //                                oFile = Context.Request.Files[0];
        //                                string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                                Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20"));
        //                                oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20\\" + filename));
        //                            }
        //                            if (Directory.Exists(pathRegisCert) == false)
        //                            {
        //                                oFile = Context.Request.Files[0];
        //                                string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                                Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate"));
        //                                oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate\\" + filename));
        //                            }
        //                            if (Directory.Exists(pathSPS10) == false && countfile == 7)
        //                            {
        //                                oFile = Context.Request.Files[0];
        //                                string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //                                Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10"));
        //                                oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10\\" + filename));
        //                            }

        //                            //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form"));
        //                            //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate"));
        //                            //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20"));
        //                            //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank"));
        //                            //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5"));
        //                            //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company"));
        //                            //Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10"));
        //                        }
        //                    }
        //                }
        //                //Directory.Delete(oPath, true);
        //                //Directory.CreateDirectory(oPath);
        //            }
        //            //HttpPostedFile oFile = null;
        //            //for (int i = 0; i < countfile; i++)
        //            //{
        //            //    oFile = Context.Request.Files[i];
        //            //    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //            //    if (i == 0)
        //            //    {
        //            //        oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\" + filename));
        //            //    }
        //            //    else if (i == 1)
        //            //    {
        //            //        oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate\\" + filename));
        //            //    }
        //            //    else if (i == 2)
        //            //    {
        //            //        oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20\\" + filename));
        //            //    }
        //            //    else if (i == 3)
        //            //    {
        //            //        oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank\\" + filename));
        //            //    }
        //            //    else if (i == 4)
        //            //    {
        //            //        oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5\\" + filename));
        //            //    }
        //            //    else if (i == 5)
        //            //    {
        //            //        oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company\\" + filename));
        //            //    }
        //            //    else if (i == 6)
        //            //    {
        //            //        oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10\\" + filename));
        //            //    }
        //            //}

        //            //HttpPostedFile oFile = null;
        //            //for (int i = 0; i < countfile; i++)
        //            //{
        //            //    oFile = Context.Request.Files[i];
        //            //    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //            //    oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\" + filename));

        //            //}

        //            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('success', 'Success', 'Upload success, Value has inserted.', '" + id + "');", true);

        //        }



        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //    }
        //}

        //protected void UploadRevise_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //StringBuilder sql = new StringBuilder();
        //        //SqlConnection conn = new SqlConnection();
        //        //SqlCommand sqlcmd = new SqlCommand();
        //        //DataTable oDt = new DataTable();
        //        //SqlDataReader oDr;

        //        //int countfile = 0;
        //        //if (Int32.TryParse(hdfFile1.Value, out int Temp))
        //        //    countfile = Temp;


        //        //if (countfile >= 1)
        //        //{
        //        //    if (conn.State == ConnectionState.Open)
        //        //        conn.Close();

        //        //    conn = new SqlConnection(Properties.Settings.Default.Conn);
        //        //    conn.Open();

        //        //    string id = hdfApp_ID.Value;
        //        //    string status = "P";
        //        //    var timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
        //        //    string detail = id + "was uploaded by Vendor";
        //        //    //Create IncidentNo
        //        //    sql.Append("spFileUpload");
        //        //    sqlcmd = new SqlCommand(sql.ToString(), conn);
        //        //    sqlcmd.Parameters.AddWithValue("Id", id);
        //        //    sqlcmd.Parameters.AddWithValue("Status", status);
        //        //    sqlcmd.Parameters.AddWithValue("Detail", detail);
        //        //    sqlcmd.Parameters.AddWithValue("Update_Date", timestamp);
        //        //    sqlcmd.CommandType = CommandType.StoredProcedure;
        //        //    oDr = sqlcmd.ExecuteReader();
        //        //    while (oDr.Read())
        //        //    {

        //        //    }
        //        //    oDr.Close();

        //        //    string folder = id.Substring(1, 4);
        //        //    string subfolder = id.Substring(1, 6);
        //        //    string Path = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id);

        //        //    if (Directory.Exists(Path) == true)
        //        //    {
        //        //        DirectoryInfo dir = new DirectoryInfo(Path);
        //        //        foreach (FileInfo fi in dir.GetFiles())
        //        //        {
        //        //            fi.Delete();
        //        //        }

        //        //        HttpPostedFile oFile = null;
        //        //        for (int i = 0; i < countfile; i++)
        //        //        {
        //        //            oFile = Context.Request.Files[1];
        //        //            string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
        //        //            oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\" + filename));
        //        //        }

        //        //        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('success', 'Success', 'Upload success, Value has inserted.', '" + id + "');", true);
        //        //    }
        //        //    else
        //        //    {
        //        //        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('error', 'Fail', 'Upload fail, Can not insert value.', '" + id + "');", true);
        //        //    }


        //        //}



        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //    }
        //}
        public void sendEmail(string AppID, string Emp_Name, string emailTo, string emailFrom, string Emp_Phone)
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

            //string emailTo = "Prompiriya_S@hinothailand.com";
            string link = "https://hinommt.com/SupplierApplication/PS_Detail.aspx?id=" + AppID + "&RecCode=1";
            String Body = "<img src=\"https://career.hinothailand.com/Career/Images/companylogo.png\" style=\"width:125px; height:125px;\" /> ";
            string EncodeID = null;
            if (AppID != null)
            {
                var base64Bytes = System.Convert.FromBase64String(AppID);
                //Context.Response.Write(base64Bytes);
                EncodeID = System.Text.Encoding.UTF8.GetString(base64Bytes);
            }
            try
            {


                if (emailTo != "")
                {
                    if (emailTo.Contains(";"))
                    {
                        string MailTo = emailTo.Replace(" ", "");
                        foreach (var email in MailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            myMail.To.Add(new MailAddress(email));
                        }
                    }
                    else
                    {
                        myMail.To.Add(new MailAddress(emailTo));
                    }
                }



                //if (myMail.To.Count == emailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Count())
                //{
                if (emailFrom != "")
                {
                    if (emailFrom.Contains(";"))
                    {
                        string MailFrom = emailFrom.Replace(" ", "");
                        foreach (var item in MailFrom.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            myMail.From = new MailAddress(item);


                            myMail.BodyEncoding = Encoding.UTF8;

                            Header = "| Supplier Application system |";
                            BodyMail = Client.DownloadString(Server.MapPath("Template/UploadTemplate.htm"));
                            BodyMail = BodyMail.Replace("#NameTo", Emp_Name);
                            BodyMail = BodyMail.Replace("#AppID", EncodeID);
                            BodyMail = BodyMail.Replace("#Phone", Emp_Phone);
                            BodyMail = BodyMail.Replace("#EmployeeName", Emp_Name);
                            BodyMail = BodyMail.Replace("#image", Body);
                            BodyMail = BodyMail.Replace("#link", link);
                            AlternateView HtmlView;
                            HtmlView = AlternateView.CreateAlternateViewFromString(BodyMail, null, "text/html");
                            myMail.AlternateViews.Add(HtmlView);
                            myMail.Subject = Header;
                            SmtpClient Clients = new SmtpClient();
                            Clients.Host = Properties.Settings.Default.Ipmail;
                            Clients.Send(myMail);
                        }
                    }
                }
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