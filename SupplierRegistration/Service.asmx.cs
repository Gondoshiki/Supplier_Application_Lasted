using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using SupplierRegistration.Model;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace SupplierRegistration
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetAppDetail(string Id)
        {
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;
            string AppID = null;
            string appid = null;
            if (Id != "")
            {
                var base64Bytes = System.Convert.FromBase64String(Id);
                AppID = System.Text.Encoding.UTF8.GetString(base64Bytes);
            }
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                string id = Id;

                if (AppID != null)
                {
                    sql.Append("spGetAppDetail");
                    sqlcmd = new SqlCommand(sql.ToString(), conn);
                    sqlcmd.Parameters.AddWithValue("Id", AppID);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    oDa = new SqlDataAdapter(sqlcmd);
                    oDa.Fill(oDt);

                    if (oDt.Rows.Count > 0)
                    {
                        Context.Response.Write(JsonConvert.SerializeObject(oDt).ToString());
                        Context.Response.End();
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
                            Context.Response.Write(JsonConvert.SerializeObject(oDt1).ToString());
                            Context.Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {
                conn.Close();
            }
        }
        [WebMethod]
        public void GetFilePreview(string id)
        {
            try
            {
                string encodeID = id;
                string AppID = null;
                string Path1 = null;
                if (encodeID != "")
                {
                    AppID = Encoding.UTF8.GetString(Convert.FromBase64String(id));
                    Path1 = Server.MapPath("Document\\fileAttach\\temp\\" + AppID);
                }

                string[] linkFile;
                string[] linkFolder;
                string[] fileAppForm = null;
                string[] fileSME = null;
                string[] fileBOJ5 = null;
                string[] fileBookBank = null;
                string[] fileOrgCompany = null;
                string[] filePP20 = null;
                string[] fileRegisCert = null;
                string[] fileSPS10 = null;
                string[] Atag = null;
                string folderName = null;

                List<FileList> oFileList = new List<FileList>();
                if (AppID != null)
                {
                    if (Directory.Exists(Path1) == true)
                    {
                        string pathAppForm = Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\Application_Form");
                        string pathSME = Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\SMEs_Research");
                        string pathBOJ5 = Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\BOJ5");
                        string pathBookBank = Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\Book-Bank");
                        string pathOrgCompany = Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\Organization_Company");
                        string pathPP20 = Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\PP20");
                        string pathRegisCert = Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\Registration_Certificate");
                        string pathSPS10 = Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\SPS1-10");

                        if (Directory.Exists(pathAppForm) == true)
                        {
                            fileAppForm = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\Application_Form"));
                            if (fileAppForm.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileAppForm[0]);
                                FileList ofilesAppForm = new FileList();
                                ofilesAppForm.Title = "<p>Application for Registration :</p>";
                                ofilesAppForm.FileName = PathName;
                                ofilesAppForm.Source = "<a id='taAppForm' href='Document/fileAttach/temp/" + AppID + "/Application_Form/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileAppForm[0]) + "</a>";
                                ofilesAppForm.Source1 = "<asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='inpAppForm_temp' ErrorMessage=' * ' ForeColor='#ff0000' ValidationGroup='editUpload' SetFocusOnError='true'></asp:RequiredFieldValidator>";
                                ofilesAppForm.Source1 += "<input type='file' id='inpAppForm_temp' onchange='checkFile(inpAppForm_temp)' class='form-control' runat='server' />";
                                //ofilesAppForm.Source += "<asp:HiddenField ID='hdfAppForm_temp' runat='server' style='display: none' />";
                                //ofilesAppForm.Source2 = @"<button class=""btn btn-warning"" type=""button"" onclick=""editFileTemp('1')"" >Edit</button>";
                                //ofilesAppForm.Source2 = "<label id='lbAppForm_temp' runat='server' text='"+ fileAppForm[0] + "' style='display: none' />";
                                oFileList.Add(ofilesAppForm);
                            }
                        }
                        if (Directory.Exists(pathSME) == true)
                        {
                            fileSME = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\SMEs_Research"));
                            if (fileSME.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileSME[0]);
                                FileList ofilesSME = new FileList();
                                ofilesSME.Title = "<p>SMEs Research :</p>";
                                ofilesSME.FileName = PathName;
                                ofilesSME.Source = "<a id='taSME' href='Document/fileAttach/temp/" + AppID + "/SMEs_Research/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileSME[0]) + "</a>";
                                ofilesSME.Source1 = "<asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='inpSME_temp' ErrorMessage=' * ' ForeColor='#ff0000' ValidationGroup='editUpload' SetFocusOnError='true'></asp:RequiredFieldValidator>";
                                ofilesSME.Source1 += "<input type='file' id='inpSME_temp' onchange='checkFile(inpSME_temp)' class='form-control' runat='server' />";
                                //ofilesRegisCert.Source2 = @"<button class=""btn btn-warning"" type=""button"" onclick=""editFileTemp('2')"" >Edit</button>";
                                oFileList.Add(ofilesSME);
                            }
                        }
                        if (Directory.Exists(pathRegisCert) == true)
                        {
                            fileRegisCert = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\Registration_Certificate"));
                            if (fileRegisCert.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileRegisCert[0]);
                                FileList ofilesRegisCert = new FileList();
                                ofilesRegisCert.Title = "<p>หนังสือรับรอง :</p>";
                                ofilesRegisCert.FileName = PathName;
                                ofilesRegisCert.Source = "<a id='taRegisCert' href='Document/fileAttach/temp/" + AppID + "/Registration_Certificate/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileRegisCert[0]) + "</a>";
                                ofilesRegisCert.Source1 = "<asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='inpRegisCert_temp' ErrorMessage=' * ' ForeColor='#ff0000' ValidationGroup='editUpload' SetFocusOnError='true'></asp:RequiredFieldValidator>";
                                ofilesRegisCert.Source1 += "<input type='file' id='inpRegisCert_temp' onchange='checkFile(inpRegisCert_temp)' class='form-control' runat='server' />";
                                //ofilesRegisCert.Source2 = @"<button class=""btn btn-warning"" type=""button"" onclick=""editFileTemp('2')"" >Edit</button>";
                                oFileList.Add(ofilesRegisCert);
                            }
                        }
                        if (Directory.Exists(pathPP20) == true)
                        {
                            filePP20 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\PP20"));
                            if (filePP20.Length > 0)
                            {
                                string PathName = Path.GetFileName(filePP20[0]);
                                FileList ofilesPP20 = new FileList();
                                ofilesPP20.Title = "<p>ภพ.20 :</p>";
                                ofilesPP20.FileName = PathName;
                                ofilesPP20.Source = "<a id='taFilePP20' href='Document/fileAttach/temp/" + AppID + "/PP20/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(filePP20[0]) + "</a>";
                                ofilesPP20.Source1 = "<asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='inpPP20_temp' ErrorMessage=' * ' ForeColor='#ff0000' ValidationGroup='editUpload' SetFocusOnError='true'></asp:RequiredFieldValidator>";
                                ofilesPP20.Source1 += "<input type='file' id='inpPP20_temp' onchange='checkFile(inpPP20_temp)' class='form-control' runat='server' />";
                                //ofilesPP20.Source2 = @"<button class=""btn btn-warning"" type=""button"" onclick=""editFileTemp('3')"" >Edit</button>";
                                oFileList.Add(ofilesPP20);
                            }
                        }
                        if (Directory.Exists(pathBookBank) == true)
                        {
                            fileBookBank = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\Book-Bank"));
                            if (fileBookBank.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileBookBank[0]);
                                FileList ofilesBookBank = new FileList();
                                ofilesBookBank.Title = "<p>Book Bank :</p>";
                                ofilesBookBank.FileName = PathName;
                                ofilesBookBank.Source = "<a id='taBookBank' href='Document/fileAttach/temp/" + AppID + "/Book-Bank/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileBookBank[0]) + "</a>";
                                ofilesBookBank.Source1 = "<asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='inpBookBank_temp' ErrorMessage=' * ' ForeColor='#ff0000' ValidationGroup='editUpload' SetFocusOnError='true'></asp:RequiredFieldValidator>";
                                ofilesBookBank.Source1 += "<input type='file' id='inpBookBank_temp' onchange='checkFile(inpBookBank_temp)' class='form-control' runat='server' />";
                                //ofilesBookBank.Source2 = @"<button class=""btn btn-warning"" type=""button"" onclick=""editFileTemp('4')"" >Edit</button>";
                                oFileList.Add(ofilesBookBank);
                            }
                        }
                        if (Directory.Exists(pathBOJ5) == true)
                        {
                            fileBOJ5 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\BOJ5"));
                            if (fileBOJ5.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileBOJ5[0]);
                                FileList ofilesBOJ5 = new FileList();
                                ofilesBOJ5.Title = "<p>บอจ.5 :</p>";
                                ofilesBOJ5.FileName = PathName;
                                ofilesBOJ5.Source = "<a id='taFileBOJ5' href='Document/fileAttach/temp/" + AppID + "/BOJ5/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileBOJ5[0]) + "</a>";
                                ofilesBOJ5.Source1 = "<asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='inpBOJ5_temp' ErrorMessage=' * ' ForeColor='#ff0000' ValidationGroup='editUpload' SetFocusOnError='true'></asp:RequiredFieldValidator>";
                                ofilesBOJ5.Source1 += "<input type='file' id='inpBOJ5_temp' onchange='checkFile(inpBOJ5_temp)' class='form-control' runat='server' />";
                                //ofilesBOJ5.Source2 = @"<button class=""btn btn-warning"" type=""button"" onclick=""editFileTemp('5')"" >Edit</button>";
                                oFileList.Add(ofilesBOJ5);
                            }
                        }
                        if (Directory.Exists(pathOrgCompany) == true)
                        {
                            fileOrgCompany = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\Organization_Company"));
                            if (fileOrgCompany.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileOrgCompany[0]);
                                FileList ofilesOrgCompany = new FileList();
                                ofilesOrgCompany.Title = "<p>Organization Company :</p>";
                                ofilesOrgCompany.FileName = PathName;
                                ofilesOrgCompany.Source = "<a id='taFileOrgCompany' href='Document/fileAttach/temp/" + AppID + "/Organization_Company/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileOrgCompany[0]) + "</a>";
                                ofilesOrgCompany.Source1 = "<asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='inpOrgCompany_temp' ErrorMessage=' * ' ForeColor='#ff0000' ValidationGroup='editUpload' SetFocusOnError='true'></asp:RequiredFieldValidator>";
                                ofilesOrgCompany.Source1 += "<input type='file' id='inpOrgCompany_temp' onchange='checkFile(inpOrgCompany_temp)' class='form-control' runat='server' />";
                                //ofilesOrgCompany.Source2 = @"<button class=""btn btn-warning"" type=""button"" onclick=""editFileTemp('6')"" >Edit</button>";
                                oFileList.Add(ofilesOrgCompany);
                            }
                        }
                        if (Directory.Exists(pathSPS10) == true)
                        {
                            FileList ofilesSPS10 = new FileList();
                            fileSPS10 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\temp\\" + AppID + "\\SPS1-10"));
                            if (fileSPS10.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileSPS10[0]);
                                ofilesSPS10.Title = "<p>สปส1-10 :</p>";
                                ofilesSPS10.FileName = PathName;
                                ofilesSPS10.Source = "<a id='taFileSPS10' href='Document/fileAttach/temp/" + AppID + "/SPS1-10/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileSPS10[0]) + "</a>";
                                ofilesSPS10.Source1 = "<asp:RequiredFieldValidator ID='RequiredFieldValidator1' runat='server' ControlToValidate='inpSPS10_temp' ErrorMessage=' * ' ForeColor='#ff0000' ValidationGroup='editUpload' SetFocusOnError='true'></asp:RequiredFieldValidator>";
                                ofilesSPS10.Source1 += "<input type='file' id='inpSPS10_temp' onchange='checkFile(inpSPS10_temp)' class='form-control' runat='server' />";
                                //ofilesSPS10.Source2 = @"<button class=""btn btn-warning"" type=""button"" onclick=""editFileTemp('7')"" >Edit</button>";
                                oFileList.Add(ofilesSPS10);
                            }
                        }
                        //LinkFile = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\form"));                                                                                                                                                            
                        //int countfile = fileAppForm.Length + fileBOJ5.Length + fileBookBank.Length + fileOrgCompany.Length + filePP20.Length + fileRegisCert.Length + fileSPS10.Length;                    
                        //JavaScriptSerializer js = new JavaScriptSerializer();
                        //Context.Response.Write(js.Serialize(oFileList));
                    }
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Context.Response.Write(js.Serialize(oFileList));
                }

            }
            catch (Exception ex)
            {
                //string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {

            }
        }
        [WebMethod]
        public void GetFileRevise(string id)
        {
            try
            {
                string encodeID = id;
                string AppID = null;
                string folder = null;
                string subfolder = null;
                string Path1 = null;
                if (encodeID != "")
                {
                    AppID = Encoding.UTF8.GetString(Convert.FromBase64String(id));
                    folder = AppID.Substring(1, 4);
                    subfolder = AppID.Substring(1, 6);
                    Path1 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID);
                }

                string[] linkFile;
                string[] linkFolder;
                string[] fileAppForm = null;
                string[] fileSME = null;
                string[] fileBOJ5 = null;
                string[] fileBookBank = null;
                string[] fileOrgCompany = null;
                string[] filePP20 = null;
                string[] fileRegisCert = null;
                string[] fileSPS10 = null;
                string[] Atag = null;
                string folderName = null;

                List<FileList> oFileList = new List<FileList>();
                if (AppID != null)
                {

                    if (Directory.Exists(Path1) == true)
                    {
                        string pathAppForm = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Application_Form");
                        string pathSME = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SMEs_Research");
                        string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\BOJ5");
                        string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Book-Bank");
                        string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Organization_Company");
                        string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\PP20");
                        string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Registration_Certificate");
                        string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SPS1-10");

                        if (Directory.Exists(pathAppForm) == true)
                        {
                            fileAppForm = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Application_Form"));
                            if (fileAppForm.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileAppForm[0]);
                                FileList ofilesAppForm = new FileList();
                                ofilesAppForm.Title = "<p>Application for Registration :</p>";
                                ofilesAppForm.FileName = PathName;
                                ofilesAppForm.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Application_Form/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileAppForm[0]) + "</a>";
                                ofilesAppForm.Source1 += "<input type='file' id='inpAppForm_temp' onchange='checkFile(inpAppForm_temp)' class='form-control' runat='server' />";
                                oFileList.Add(ofilesAppForm);
                            }
                        }
                        if (Directory.Exists(pathSME) == true)
                        {
                            fileSME = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SMEs_Research"));
                            if (fileSME.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileSME[0]);
                                FileList ofilesSME = new FileList();
                                ofilesSME.Title = "<p>SMEs Research :</p>";
                                ofilesSME.FileName = PathName;
                                ofilesSME.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/SMEs_Research/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileSME[0]) + "</a>";
                                ofilesSME.Source1 += "<input type='file' id='inpSME_temp' onchange='checkFile(inpAppForm_temp)' class='form-control' runat='server' />";
                                oFileList.Add(ofilesSME);
                            }
                        }
                        if (Directory.Exists(pathRegisCert) == true)
                        {
                            fileRegisCert = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Registration_Certificate"));
                            if (fileRegisCert.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileRegisCert[0]);
                                FileList ofilesRegisCert = new FileList();
                                ofilesRegisCert.Title = "<p>หนังสือรับรอง :</p>";
                                ofilesRegisCert.FileName = PathName;
                                ofilesRegisCert.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Registration_Certificate/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileRegisCert[0]) + "</a>";
                                ofilesRegisCert.Source1 += "<input type='file' id='inpRegisCert_temp' onchange='checkFile(inpAppForm_temp)' class='form-control' runat='server' />";
                                oFileList.Add(ofilesRegisCert);
                            }
                        }
                        if (Directory.Exists(pathPP20) == true)
                        {
                            filePP20 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\PP20"));
                            if (filePP20.Length > 0)
                            {
                                string PathName = Path.GetFileName(filePP20[0]);
                                FileList ofilesPP20 = new FileList();
                                ofilesPP20.Title = "<p>ภพ.20 :</p>";
                                ofilesPP20.FileName = PathName;
                                ofilesPP20.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/PP20/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(filePP20[0]) + "</a>";
                                ofilesPP20.Source1 += "<input type='file' id='inpPP20_temp' onchange='checkFile(inpAppForm_temp)' class='form-control' runat='server' />";
                                oFileList.Add(ofilesPP20);
                            }
                        }
                        if (Directory.Exists(pathBookBank) == true)
                        {
                            fileBookBank = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Book-Bank"));
                            if (fileBookBank.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileBookBank[0]);
                                FileList ofilesBookBank = new FileList();
                                ofilesBookBank.Title = "<p>Book Bank :</p>";
                                ofilesBookBank.FileName = PathName;
                                ofilesBookBank.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Book-Bank/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileBookBank[0]) + "</a>";
                                ofilesBookBank.Source1 += "<input type='file' id='inpBookBank_temp' onchange='checkFile(inpAppForm_temp)' class='form-control' runat='server' />";
                                oFileList.Add(ofilesBookBank);
                            }
                        }
                        if (Directory.Exists(pathBOJ5) == true)
                        {
                            fileBOJ5 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\BOJ5"));
                            if (fileBOJ5.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileBOJ5[0]);
                                FileList ofilesBOJ5 = new FileList();
                                ofilesBOJ5.Title = "<p>บอจ.5 :</p>";
                                ofilesBOJ5.FileName = PathName;
                                ofilesBOJ5.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/BOJ5/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileBOJ5[0]) + "</a>";
                                ofilesBOJ5.Source1 += "<input type='file' id='inpBOJ5_temp' onchange='checkFile(inpAppForm_temp)' class='form-control' runat='server' />";
                                oFileList.Add(ofilesBOJ5);
                            }
                        }
                        if (Directory.Exists(pathOrgCompany) == true)
                        {
                            fileOrgCompany = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Organization_Company"));
                            if (fileOrgCompany.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileOrgCompany[0]);
                                FileList ofilesOrgCompany = new FileList();
                                ofilesOrgCompany.Title = "<p>Organization Company :</p>";
                                ofilesOrgCompany.FileName = PathName;
                                ofilesOrgCompany.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Organization_Company/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileOrgCompany[0]) + "</a>";
                                ofilesOrgCompany.Source1 += "<input type='file' id='inpOrgCompany_temp' onchange='checkFile(inpAppForm_temp)' class='form-control' runat='server' />";
                                oFileList.Add(ofilesOrgCompany);
                            }
                        }
                        if (Directory.Exists(pathSPS10) == true)
                        {
                            FileList ofilesSPS10 = new FileList();
                            fileSPS10 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SPS1-10"));
                            if (fileSPS10.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileSPS10[0]);
                                ofilesSPS10.Title = "<p>สปส1-10 :</p>";
                                ofilesSPS10.FileName = PathName;
                                ofilesSPS10.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/SPS1-10/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileSPS10[0]) + "</a>";
                                ofilesSPS10.Source1 += "<input type='file' id='inpSPS10_temp' onchange='checkFile(inpAppForm_temp)' class='form-control' runat='server' />";
                                oFileList.Add(ofilesSPS10);
                            }
                        }
                        //LinkFile = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\form"));                                                                                                                                                            
                        //int countfile = fileAppForm.Length + fileBOJ5.Length + fileBookBank.Length + fileOrgCompany.Length + filePP20.Length + fileRegisCert.Length + fileSPS10.Length;                    
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        Context.Response.Write(js.Serialize(oFileList));
                    }
                }

            }
            catch (Exception ex)
            {
                //string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {

            }
        }
        [WebMethod]
        public void GetFileList(string id)
        {
            try
            {
                string encodeID = id;
                string AppID = null;
                string folder = null;
                string subfolder = null;
                string Path1 = null;
                if (encodeID != "")
                {
                    AppID = Encoding.UTF8.GetString(Convert.FromBase64String(id));
                    folder = AppID.Substring(1, 4);
                    subfolder = AppID.Substring(1, 6);
                    Path1 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID);
                }

                string[] linkFile;
                string[] linkFolder;
                string[] fileAppForm = null;
                string[] fileBOJ5 = null;
                string[] fileBookBank = null;
                string[] fileOrgCompany = null;
                string[] filePP20 = null;
                string[] fileRegisCert = null;
                string[] fileSPS10 = null;
                string[] fileSME = null;
                string[] Atag = null;
                string folderName = null;

                List<FileList> oFileList = new List<FileList>();
                if (AppID != null)
                {
                    if (Directory.Exists(Path1) == true)
                    {
                        string pathAppForm = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Application_Form");
                        string pathSME = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SMEs_Research");
                        string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\BOJ5");
                        string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Book-Bank");
                        string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Organization_Company");
                        string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\PP20");
                        string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Registration_Certificate");
                        string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SPS1-10");

                        if (Directory.Exists(pathAppForm) == true)
                        {
                            fileAppForm = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Application_Form"));
                            if (fileAppForm.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileAppForm[0]);
                                FileList ofilesAppForm = new FileList();
                                ofilesAppForm.Title = "<p>Application for Registration :</p>";
                                ofilesAppForm.FileName = PathName;
                                ofilesAppForm.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Application_Form/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileAppForm[0]) + "</a>";
                                oFileList.Add(ofilesAppForm);
                            }
                        }
                        if (Directory.Exists(pathSME) == true)
                        {
                            FileList ofilesSME = new FileList();
                            fileSME = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SMEs_Research"));
                            if (fileSME.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileSME[0]);
                                ofilesSME.Title = "<p>SMEs Research :</p>";
                                ofilesSME.FileName = PathName;
                                ofilesSME.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/SMEs_Research/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileSME[0]) + "</a>";
                                oFileList.Add(ofilesSME);
                            }
                        }
                        if (Directory.Exists(pathRegisCert) == true)
                        {
                            fileRegisCert = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Registration_Certificate"));
                            if (fileRegisCert.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileRegisCert[0]);
                                FileList ofilesRegisCert = new FileList();
                                ofilesRegisCert.Title = "<p>หนังสือรับรอง :</p>";
                                ofilesRegisCert.FileName = PathName;
                                ofilesRegisCert.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Registration_Certificate/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileRegisCert[0]) + "</a>";
                                oFileList.Add(ofilesRegisCert);
                            }
                        }
                        if (Directory.Exists(pathPP20) == true)
                        {
                            filePP20 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\PP20"));
                            if (filePP20.Length > 0)
                            {
                                string PathName = Path.GetFileName(filePP20[0]);
                                FileList ofilesPP20 = new FileList();
                                ofilesPP20.Title = "<p>ภพ.20 :</p>";
                                ofilesPP20.FileName = PathName;
                                ofilesPP20.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/PP20/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(filePP20[0]) + "</a>";

                                oFileList.Add(ofilesPP20);
                            }
                        }
                        if (Directory.Exists(pathBookBank) == true)
                        {
                            fileBookBank = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Book-Bank"));
                            if (fileBookBank.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileBookBank[0]);
                                FileList ofilesBookBank = new FileList();
                                ofilesBookBank.Title = "<p>Book Bank :</p>";
                                ofilesBookBank.FileName = PathName;
                                ofilesBookBank.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Book-Bank/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileBookBank[0]) + "</a>";

                                oFileList.Add(ofilesBookBank);
                            }
                        }
                        if (Directory.Exists(pathBOJ5) == true)
                        {
                            fileBOJ5 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\BOJ5"));
                            if (fileBOJ5.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileBOJ5[0]);
                                FileList ofilesBOJ5 = new FileList();
                                ofilesBOJ5.Title = "<p>บอจ.5 :</p>";
                                ofilesBOJ5.FileName = PathName;
                                ofilesBOJ5.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/BOJ5/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileBOJ5[0]) + "</a>";

                                oFileList.Add(ofilesBOJ5);
                            }
                        }

                        if (Directory.Exists(pathOrgCompany) == true)
                        {
                            fileOrgCompany = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Organization_Company"));
                            if (fileOrgCompany.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileOrgCompany[0]);
                                FileList ofilesOrgCompany = new FileList();
                                ofilesOrgCompany.Title = "<p>Organization Company :</p>";
                                ofilesOrgCompany.FileName = PathName;
                                ofilesOrgCompany.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Organization_Company/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileOrgCompany[0]) + "</a>";

                                oFileList.Add(ofilesOrgCompany);
                            }
                        }
                        if (Directory.Exists(pathSPS10) == true)
                        {
                            FileList ofilesSPS10 = new FileList();
                            fileSPS10 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SPS1-10"));
                            if (fileSPS10.Length > 0)
                            {
                                string PathName = Path.GetFileName(fileSPS10[0]);
                                ofilesSPS10.Title = "<p>สปส1-10 :</p>";
                                ofilesSPS10.FileName = PathName;
                                ofilesSPS10.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/SPS1-10/" + Uri.EscapeUriString(PathName) + "' target='_blank'>" + Path.GetFileName(fileSPS10[0]) + "</a>";
                                oFileList.Add(ofilesSPS10);
                            }
                        }
                        //LinkFile = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\form"));                                                                                                                                                            
                        //int countfile = fileAppForm.Length + fileBOJ5.Length + fileBookBank.Length + fileOrgCompany.Length + filePP20.Length + fileRegisCert.Length + fileSPS10.Length;                    
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        Context.Response.Write(js.Serialize(oFileList));
                    }
                }

            }
            catch (Exception ex)
            {
                //string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {

            }
        }
        [WebMethod]
        public void DeletePreview(string id)
        {
            try
            {
                if(id != "")
                {
                    string tempPath = Server.MapPath("Document\\fileAttach\\temp\\" + id);
                    if (Directory.Exists(tempPath))
                    {
                        Directory.Delete(tempPath, true);
                        Context.Response.Write(JsonConvert.SerializeObject("success").ToString());
                        Context.Response.End();
                    }
                    else
                    {
                        Context.Response.Write(JsonConvert.SerializeObject("fail").ToString());
                        Context.Response.End();
                    }
                }
                else
                {
                    Context.Response.Write(JsonConvert.SerializeObject("fail").ToString());
                    Context.Response.End();
                }
            }
            catch(Exception ex)
            {
                //Context.Response.Write(JsonConvert.SerializeObject(ex.Message).ToString());
                //Context.Response.End();
            }
        }
        [WebMethod]
        public void EditPreview()
        {
            string id = HttpContext.Current.Request.Form["id"];
            int countfile = Context.Request.Files.Count;
            //HttpPostedFile otest = HttpContext.Current.Request.Files["fileApp_temp"];
            HttpPostedFile ocheck = null;

            HttpPostedFile oFileApp_temp = null;
            HttpPostedFile oFileSME_temp = null;
            HttpPostedFile oFileRegisCert_temp = null;
            HttpPostedFile oFilePP20_temp = null;
            HttpPostedFile oFileBookBank_temp = null;
            HttpPostedFile oFileBOJ5_temp = null;
            HttpPostedFile oFileOrgCompany_temp = null;
            HttpPostedFile oFileSPS10_temp = null;


            if (File.Equals(HttpContext.Current.Request.Files["fileApp_temp"], null) == false)
            {
                oFileApp_temp = HttpContext.Current.Request.Files["fileApp_temp"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileSME_temp"], null) == false)
            {
                oFileSME_temp = HttpContext.Current.Request.Files["fileSME_temp"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileRegisCert_temp"], null) == false)
            {
                oFileRegisCert_temp = HttpContext.Current.Request.Files["fileRegisCert_temp"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["filePP20_temp"], null) == false)
            {
                oFilePP20_temp = HttpContext.Current.Request.Files["filePP20_temp"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileBookBank_temp"], null) == false)
            {
                oFileBookBank_temp = HttpContext.Current.Request.Files["fileBookBank_temp"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileBOJ5_temp"], null) == false)
            {
                oFileBOJ5_temp = HttpContext.Current.Request.Files["fileBOJ5_temp"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileOrgCompany_temp"], null) == false)
            {
                oFileOrgCompany_temp = HttpContext.Current.Request.Files["fileOrgCompany_temp"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileSPS10_temp"], null) == false)
            {
                oFileSPS10_temp = HttpContext.Current.Request.Files["fileSPS10_temp"];
            }

            //oFileRegisCert_temp = Context.Request.Files[1]; 
            //oFilePP20_temp = Context.Request.Files[2]; 
            //oFileBookBank_temp = Context.Request.Files[3]; 
            //oFileBOJ5_temp = Context.Request.Files[4]; 
            //oFileOrgCompany_temp = Context.Request.Files[5]; 
            //oFileSPS10_temp = Context.Request.Files[6];            

            try
            {
                if (countfile > 0 && id != "")
                {
                    string Path = Server.MapPath("Document\\fileAttach\\temp\\" + id);
                    if (Directory.Exists(Path) == true)
                    {

                        if (oFileApp_temp != null)
                        {
                            string pathAppForm = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Application_Form");
                            string filename = oFileApp_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileApp_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Application_Form\\" + filename));
                        }
                        if (oFileSME_temp != null)
                        {
                            string pathSME = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\SMEs_Research");
                            string filename = oFileSME_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathSME) == true)
                            {
                                foreach (string file in Directory.GetFiles(pathSME))
                                {
                                    File.Delete(file);
                                }
                            }
                            else
                            {
                                Directory.CreateDirectory(pathSME);
                            }
                            oFileSME_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\SMEs_Research\\" + filename));
                        }
                        if (oFileRegisCert_temp != null)
                        {
                            string pathRegisCert = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Registration_Certificate");
                            string filename = oFileRegisCert_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileRegisCert_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Registration_Certificate\\" + filename));
                        }
                        if (oFilePP20_temp != null)
                        {
                            string pathPP20 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\PP20");
                            string filename = oFilePP20_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFilePP20_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\PP20\\" + filename));
                        }
                        if (oFileBookBank_temp != null)
                        {
                            string pathBookBank = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Book-Bank");
                            string filename = oFileBookBank_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileBookBank_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Book-Bank\\" + filename));
                        }
                        if (oFileBOJ5_temp != null)
                        {
                            string pathBOJ5 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\BOJ5");
                            string filename = oFileBOJ5_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileBOJ5_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\BOJ5\\" + filename));
                        }
                        if (oFileOrgCompany_temp != null)
                        {
                            string pathOrgCompany = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Organization_Company");
                            string filename = oFileOrgCompany_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileOrgCompany_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Organization_Company\\" + filename));
                        }
                        if (oFileSPS10_temp != null)
                        {
                            string pathSPS10 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\SPS1-10");
                            string filename = oFileSPS10_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileSPS10_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\SPS1-10\\" + filename));
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(Path);
                        if (oFileApp_temp != null)
                        {
                            string pathAppForm = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Application_Form");
                            Directory.CreateDirectory(pathAppForm);
                            string filename = oFileApp_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathAppForm) == true)
                            {
                                oFileApp_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Application_Form\\" + filename));
                            }
                        }
                        if (oFileSME_temp != null)
                        {
                            string pathSME = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\SMEs_Research");
                            Directory.CreateDirectory(pathSME);
                            string filename = oFileSME_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathSME) == true)
                            {
                                oFileSME_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\SMEs_Research\\" + filename));
                            }

                        }
                        if (oFileRegisCert_temp != null)
                        {
                            string pathRegisCert = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Registration_Certificate");
                            Directory.CreateDirectory(pathRegisCert);
                            string filename = oFileRegisCert_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathRegisCert) == true)
                            {
                                oFileRegisCert_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Registration_Certificate\\" + filename));
                            }

                        }
                        if (oFilePP20_temp != null)
                        {
                            string pathPP20 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\PP20");
                            Directory.CreateDirectory(pathPP20);
                            string filename = oFilePP20_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathPP20) == true)
                            {
                                oFilePP20_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\PP20\\" + filename));
                            }

                        }
                        if (oFileBookBank_temp != null)
                        {
                            string pathBookBank = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Book-Bank");
                            Directory.CreateDirectory(pathBookBank);
                            string filename = oFileBookBank_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathBookBank) == true)
                            {
                                oFileBookBank_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Book-Bank\\" + filename));

                            }

                        }
                        if (oFileBOJ5_temp != null)
                        {
                            string pathBOJ5 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\BOJ5");
                            Directory.CreateDirectory(pathBOJ5);
                            string filename = oFileBOJ5_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathBOJ5) == true)
                            {
                                oFileBOJ5_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\BOJ5\\" + filename));
                            }

                        }

                        if (oFileOrgCompany_temp != null)
                        {
                            string pathOrgCompany = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Organization_Company");
                            Directory.CreateDirectory(pathOrgCompany);
                            string filename = oFileOrgCompany_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathOrgCompany) == true)
                            {
                                oFileOrgCompany_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Organization_Company\\" + filename));
                            }

                        }
                        if (oFileSPS10_temp != null)
                        {
                            string pathSPS10 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\SPS1-10");
                            Directory.CreateDirectory(pathSPS10);
                            string filename = oFileSPS10_temp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathSPS10) == true)
                            {
                                oFileSPS10_temp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\SPS1-10\\" + filename));
                            }
                        }
                    }
                    Context.Response.Write(JsonConvert.SerializeObject("success").ToString());
                    Context.Response.End();
                }
                else
                {
                    Context.Response.Write(JsonConvert.SerializeObject("fail").ToString());
                    Context.Response.End();
                }
            }
            catch (Exception ex)
            {

            }

        }
        [WebMethod]
        public void SaveToPreview()
        {
            string id = HttpContext.Current.Request.Form["id"];
            int countfile = Context.Request.Files.Count;
            //HttpPostedFile otest = HttpContext.Current.Request.Files["fileApp_temp"];
            HttpPostedFile ocheck = null;

            HttpPostedFile oFileApp = null;
            HttpPostedFile oFileRegisCert = null;
            HttpPostedFile oFilePP20 = null;
            HttpPostedFile oFileBookBank = null;
            HttpPostedFile oFileBOJ5 = null;
            HttpPostedFile oFileOrgCompany = null;
            HttpPostedFile oFileSPS10 = null;
            HttpPostedFile oFileSME = null;


            if (File.Equals(HttpContext.Current.Request.Files["fileApp"], null) == false)
            {
                oFileApp = HttpContext.Current.Request.Files["fileApp"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileSME"], null) == false)
            {
                oFileSME = HttpContext.Current.Request.Files["fileSME"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileRegisCert"], null) == false)
            {
                oFileRegisCert = HttpContext.Current.Request.Files["fileRegisCert"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["filePP20"], null) == false)
            {
                oFilePP20 = HttpContext.Current.Request.Files["filePP20"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileBookBank"], null) == false)
            {
                oFileBookBank = HttpContext.Current.Request.Files["fileBookBank"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileBOJ5"], null) == false)
            {
                oFileBOJ5 = HttpContext.Current.Request.Files["fileBOJ5"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileOrgCompany"], null) == false)
            {
                oFileOrgCompany = HttpContext.Current.Request.Files["fileOrgCompany"];
            }
            if (File.Equals(HttpContext.Current.Request.Files["fileSPS10"], null) == false)
            {
                oFileSPS10 = HttpContext.Current.Request.Files["fileSPS10"];
            }

            //oFileRegisCert_temp = Context.Request.Files[1]; 
            //oFilePP20_temp = Context.Request.Files[2]; 
            //oFileBookBank_temp = Context.Request.Files[3]; 
            //oFileBOJ5_temp = Context.Request.Files[4]; 
            //oFileOrgCompany_temp = Context.Request.Files[5]; 
            //oFileSPS10_temp = Context.Request.Files[6];            

            try
            {
                if (countfile > 0 && id != "")
                {
                    string Path = Server.MapPath("Document\\fileAttach\\temp\\" + id);
                    if (Directory.Exists(Path) == true)
                    {

                        if (oFileApp != null)
                        {
                            string pathAppForm = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Application_Form");
                            string filename = oFileApp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileApp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Application_Form\\" + filename));
                        }
                        if (oFileSME != null)
                        {
                            string pathSME = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\SMEs_Research");
                            string filename = oFileSME.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathSME) == true)
                            {
                                foreach (string file in Directory.GetFiles(pathSME))
                                {
                                    File.Delete(file);
                                }
                            }
                            else
                            {
                                Directory.CreateDirectory(pathSME);
                            }
                            oFileSME.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\SMEs_Research\\" + filename));
                        }
                        if (oFileRegisCert != null)
                        {
                            string pathRegisCert = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Registration_Certificate");
                            string filename = oFileRegisCert.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileRegisCert.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Registration_Certificate\\" + filename));
                        }
                        if (oFilePP20 != null)
                        {
                            string pathPP20 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\PP20");
                            string filename = oFilePP20.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFilePP20.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\PP20\\" + filename));
                        }
                        if (oFileBookBank != null)
                        {
                            string pathBookBank = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Book-Bank");
                            string filename = oFileBookBank.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileBookBank.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Book-Bank\\" + filename));
                        }
                        if (oFileBOJ5 != null)
                        {
                            string pathBOJ5 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\BOJ5");
                            string filename = oFileBOJ5.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileBOJ5.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\BOJ5\\" + filename));
                        }
                        if (oFileOrgCompany != null)
                        {
                            string pathOrgCompany = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Organization_Company");
                            string filename = oFileOrgCompany.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileOrgCompany.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Organization_Company\\" + filename));
                        }
                        if (oFileSPS10 != null)
                        {
                            string pathSPS10 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\SPS1-10");
                            string filename = oFileSPS10.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileSPS10.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\SPS1-10\\" + filename));
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(Path);
                        if (oFileApp != null)
                        {
                            string pathAppForm = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Application_Form");
                            Directory.CreateDirectory(pathAppForm);
                            string filename = oFileApp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathAppForm) == true)
                            {
                                oFileApp.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Application_Form\\" + filename));
                            }
                        }
                        if (oFileSME != null)
                        {
                            string pathSME = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\SMEs_Research");
                            Directory.CreateDirectory(pathSME);
                            string filename = oFileSME.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathSME) == true)
                            {
                                oFileSME.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\SMEs_Research\\" + filename));
                            }

                        }
                        if (oFileRegisCert != null)
                        {
                            string pathRegisCert = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Registration_Certificate");
                            Directory.CreateDirectory(pathRegisCert);
                            string filename = oFileRegisCert.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathRegisCert) == true)
                            {
                                oFileRegisCert.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Registration_Certificate\\" + filename));
                            }

                        }
                        if (oFilePP20 != null)
                        {
                            string pathPP20 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\PP20");
                            Directory.CreateDirectory(pathPP20);
                            string filename = oFilePP20.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathPP20) == true)
                            {
                                oFilePP20.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\PP20\\" + filename));
                            }

                        }
                        if (oFileBookBank != null)
                        {
                            string pathBookBank = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Book-Bank");
                            Directory.CreateDirectory(pathBookBank);
                            string filename = oFileBookBank.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathBookBank) == true)
                            {
                                oFileBookBank.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Book-Bank\\" + filename));

                            }

                        }
                        if (oFileBOJ5 != null)
                        {
                            string pathBOJ5 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\BOJ5");
                            Directory.CreateDirectory(pathBOJ5);
                            string filename = oFileBOJ5.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathBOJ5) == true)
                            {
                                oFileBOJ5.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\BOJ5\\" + filename));
                            }

                        }
                        if (oFileOrgCompany != null)
                        {
                            string pathOrgCompany = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\Organization_Company");
                            Directory.CreateDirectory(pathOrgCompany);
                            string filename = oFileOrgCompany.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathOrgCompany) == true)
                            {
                                oFileOrgCompany.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\Organization_Company\\" + filename));
                            }

                        }
                        if (oFileSPS10 != null)
                        {
                            string pathSPS10 = Server.MapPath("Document\\fileAttach\\temp\\" + id + "\\SPS1-10");
                            Directory.CreateDirectory(pathSPS10);
                            string filename = oFileSPS10.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathSPS10) == true)
                            {
                                oFileSPS10.SaveAs(Server.MapPath("Document\\FileAttach\\temp\\" + id + "\\SPS1-10\\" + filename));
                            }
                        }
                    }
                    Context.Response.Write(JsonConvert.SerializeObject("success").ToString());
                    Context.Response.End();
                }
                else
                {
                    Context.Response.Write(JsonConvert.SerializeObject("fail").ToString());
                    Context.Response.End();
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void UploadRevise()
        {

            try
            {
                StringBuilder sql = new StringBuilder();
                SqlConnection conn = new SqlConnection();
                SqlCommand sqlcmd = new SqlCommand();
                DataTable oDt = new DataTable();
                SqlDataReader oDr;

                string[] arrtempPath = null;
                string id = HttpContext.Current.Request.Form["id"];
                int countfile = Context.Request.Files.Count;
                //HttpPostedFile otest = HttpContext.Current.Request.Files["fileApp_temp"];
                HttpPostedFile ocheck = null;

                HttpPostedFile oFileApp = null;
                HttpPostedFile oFileSME = null;
                HttpPostedFile oFileRegisCert = null;
                HttpPostedFile oFilePP20 = null;
                HttpPostedFile oFileBookBank = null;
                HttpPostedFile oFileBOJ5 = null;
                HttpPostedFile oFileOrgCompany = null;
                HttpPostedFile oFileSPS10 = null;


                if (File.Equals(HttpContext.Current.Request.Files["fileApp_temp"], null) == false)
                {
                    oFileApp = HttpContext.Current.Request.Files["fileApp_temp"];
                }
                if (File.Equals(HttpContext.Current.Request.Files["fileSME_temp"], null) == false)
                {
                    oFileSME = HttpContext.Current.Request.Files["fileSME_temp"];
                }
                if (File.Equals(HttpContext.Current.Request.Files["fileRegisCert_temp"], null) == false)
                {
                    oFileRegisCert = HttpContext.Current.Request.Files["fileRegisCert_temp"];
                }
                if (File.Equals(HttpContext.Current.Request.Files["filePP20_temp"], null) == false)
                {
                    oFilePP20 = HttpContext.Current.Request.Files["filePP20_temp"];
                }
                if (File.Equals(HttpContext.Current.Request.Files["fileBookBank_temp"], null) == false)
                {
                    oFileBookBank = HttpContext.Current.Request.Files["fileBookBank_temp"];
                }
                if (File.Equals(HttpContext.Current.Request.Files["fileBOJ5_temp"], null) == false)
                {
                    oFileBOJ5 = HttpContext.Current.Request.Files["fileBOJ5_temp"];
                }
                if (File.Equals(HttpContext.Current.Request.Files["fileOrgCompany_temp"], null) == false)
                {
                    oFileOrgCompany = HttpContext.Current.Request.Files["fileOrgCompany_temp"];
                }
                if (File.Equals(HttpContext.Current.Request.Files["fileSPS10_temp"], null) == false)
                {
                    oFileSPS10 = HttpContext.Current.Request.Files["fileSPS10_temp"];
                }

                if (countfile >= 1)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn = new SqlConnection(Properties.Settings.Default.Conn);
                    conn.Open();

                    string status = "P";
                    var timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                    string detail = id + "was uploaded by Vendor";
                    //Create IncidentNo
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

                    string folder = id.Substring(1, 4);
                    string subfolder = id.Substring(1, 6);
                    string localPath = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id);
                    string tempPath = Server.MapPath("Document\\fileAttach\\temp\\" + id);
                    if (Directory.Exists(localPath) == true)
                    {
                        if (oFileApp != null)
                        {
                            string pathAppForm = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form");
                            string filename = oFileApp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            //oFileApp.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\" + filename));
                            //Get File From Temporary folder
                            if (Directory.Exists(tempPath) == true)
                            {
                                arrtempPath = Directory.GetDirectories(tempPath);
                                
                                foreach(string otempPath in arrtempPath)
                                {
                                    string tempFolderName = Path.GetDirectoryName(otempPath);
                                    if(Directory.Exists(localPath + "\\" + tempFolderName))
                                    {
                                        foreach (string file in Directory.GetFiles(localPath + "\\" + tempFolderName))
                                        {
                                            File.Delete(file);
                                        }
                                        Directory.Move(otempPath, localPath);
                                    }
                                }
                                
                            }
                        }
                        if (oFileSME != null)
                        {

                            string pathSME = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research");
                            string filename = oFileSME.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                            if (Directory.Exists(pathSME) == true)
                            {
                                foreach (string file in Directory.GetFiles(pathSME))
                                {
                                    File.Delete(file);
                                }
                            }
                            else
                            {
                                Directory.CreateDirectory(pathSME);
                            }
                            oFileSME.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research\\" + filename));
                        }
                        if (oFileRegisCert != null)
                        {

                            string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate");
                            string filename = oFileRegisCert.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileRegisCert.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate\\" + filename));
                        }
                        if (oFilePP20 != null)
                        {
                            string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20");
                            string filename = oFilePP20.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFilePP20.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20\\" + filename));
                        }
                        if (oFileBookBank != null)
                        {
                            string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank");
                            string filename = oFileBookBank.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileBookBank.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank\\" + filename));
                        }
                        if (oFileBOJ5 != null)
                        {
                            string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5");
                            string filename = oFileBOJ5.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileBOJ5.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5\\" + filename));
                        }
                        if (oFileOrgCompany != null)
                        {
                            string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company");
                            string filename = oFileOrgCompany.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileOrgCompany.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company\\" + filename));
                        }
                        if (oFileSPS10 != null)
                        {

                            string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10");
                            string filename = oFileSPS10.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                            oFileSPS10.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10\\" + filename));
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
                                string pathAppForm = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form");
                                string pathSME = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research");
                                string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5");
                                string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank");
                                string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company");
                                string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20");
                                string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate");
                                string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10");
                                if (oFileApp != null)
                                {

                                    string filename = oFileApp.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                                    oFileApp.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form\\" + filename));
                                }
                                if (oFileSME != null)
                                {

                                    string filename = oFileSME.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                                    if (Directory.Exists(pathSME) == true)
                                    {
                                        foreach (string file in Directory.GetFiles(pathSME))
                                        {
                                            File.Delete(file);
                                        }
                                    }
                                    else
                                    {
                                        Directory.CreateDirectory(pathSME);
                                    }
                                    oFileSME.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research\\" + filename));
                                }
                                if (oFileRegisCert != null)
                                {

                                    string filename = oFileRegisCert.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                                    oFileRegisCert.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate\\" + filename));
                                }
                                if (oFilePP20 != null)
                                {

                                    string filename = oFilePP20.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                                    oFilePP20.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20\\" + filename));
                                }
                                if (oFileBookBank != null)
                                {

                                    string filename = oFileBookBank.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                                    oFileBookBank.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank\\" + filename));
                                }
                                if (oFileBOJ5 != null)
                                {


                                    string filename = oFileBOJ5.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                                    oFileBOJ5.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5\\" + filename));
                                }
                                if (oFileOrgCompany != null)
                                {

                                    string filename = oFileOrgCompany.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                                    oFileOrgCompany.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company\\" + filename));
                                }
                                if (oFileSPS10 != null)
                                {

                                    string filename = oFileSPS10.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
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
                                    oFileSPS10.SaveAs(Server.MapPath("Document\\FileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10\\" + filename));
                                }

                            }
                        }
                    }
                    Context.Response.Write(JsonConvert.SerializeObject("success").ToString());
                    Context.Response.End();
                }
                else
                {
                    Context.Response.Write(JsonConvert.SerializeObject("fail").ToString());
                    Context.Response.End();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        [WebMethod]
        public void FileProcess(string id)
        {
            try
            {
                var base64Bytes = System.Convert.FromBase64String(id);
                //Context.Response.Write(base64Bytes);
                string AppID = System.Text.Encoding.UTF8.GetString(base64Bytes);

                //string AppID = id;
                string folder = AppID.Substring(1, 4);
                string subfolder = AppID.Substring(1, 6);
                string Path1 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID);

                string[] fileAppForm = null;
                string[] fileBOJ5 = null;
                string[] fileBookBank = null;
                string[] fileOrgCompany = null;
                string[] filePP20 = null;
                string[] fileRegisCert = null;
                string[] fileSPS10 = null;
                string[] fileSME = null;
                string[] Atag = null;
                string btn = null;

                List<FileList> oFileList = new List<FileList>();

                if (Directory.Exists(Path1) == true)
                {
                    //linkFolder = System.IO.Directory.GetDirectories(Path1);

                    //for (int i=0; i<linkFolder.Length; i++)
                    //{
                    //    folderName = Path.GetFileName(linkFolder[i]);
                    //    linkFile = System.IO.Directory.GetFiles(linkFolder[i]);
                    //    for(int j=0; j<linkFile.Length; j++)
                    //    {
                    //        FileList ofiles = new FileList();
                    //        ofiles.FileName = "";
                    //        ofiles.Source = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/" + folderName + "/" + Path.GetFileName(linkFile[j]) + "' target='_blank'>" + Path.GetFileName(linkFile[j]) + "</a>";
                    //        oFileList.Add(ofiles);
                    //    }
                    //}
                    string pathAppForm = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Application_Form");
                    string pathSME = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SMEs_Research");
                    string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\BOJ5");
                    string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Book-Bank");
                    string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Organization_Company");
                    string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\PP20");
                    string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Registration_Certificate");
                    string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SPS1-10");

                    if (Directory.Exists(pathAppForm) == true)
                    {
                        FileList ofilesAppForm = new FileList();
                        fileAppForm = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Application_Form"));
                        ofilesAppForm.Title = "<p>Application for Registration :</p>";
                        if (fileAppForm.Length > 0)
                        {
                            ofilesAppForm.FileName = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Application_Form/" + Path.GetFileName(fileAppForm[0]) + "' target='_blank'>" + Path.GetFileName(fileAppForm[0]) + "</a>";
                            ofilesAppForm.Source = "<button class='btn btn-warning' type='button' id='AppRegis' data-toggle='modal' data-target='#ReplaceAppRegis'>Edit File</button>";

                        }
                        else
                        {
                            ofilesAppForm.FileName = "Not Found";
                            ofilesAppForm.Source = "<button class='btn btn-primary' type='button' id='AppRegis' data-toggle='modal' data-target='#ReplaceAppRegis' >Add File</button>";
                        }
                        oFileList.Add(ofilesAppForm);
                    }
                    if (Directory.Exists(pathSME) == true)
                    {
                        FileList ofilesSME = new FileList();
                        fileSME = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SMEs_Research"));
                        ofilesSME.Title = " <p> SMEs Research : </p> ";
                        if (fileSME.Length > 0)
                        {
                            ofilesSME.FileName = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/SMEs_Research/" + Path.GetFileName(fileSME[0]) + "' target='_blank'>" + Path.GetFileName(fileSME[0]) + "</a>";
                            ofilesSME.Source = "<button class='btn btn-warning' type='button' id='SME' data-toggle='modal' data-target='#ReplaceSME' >Edit File</button>";
                        }
                        else
                        {
                            ofilesSME.FileName = "<p>Not Found</p>";
                            ofilesSME.Source = "<button class='btn btn-primary' type='button' id='SME' data-toggle='modal' data-target='#ReplaceSME' >Add File</button>";

                        }
                        oFileList.Add(ofilesSME);
                    }
                    if (Directory.Exists(pathRegisCert) == true)
                    {
                        FileList ofilesRegisCert = new FileList();
                        fileRegisCert = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Registration_Certificate"));
                        ofilesRegisCert.Title = "<p>หนังสือรับรอง :</p>";
                        if (fileRegisCert.Length > 0)
                        {
                            ofilesRegisCert.FileName = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Registration_Certificate/" + Path.GetFileName(fileRegisCert[0]) + "' target='_blank'>" + Path.GetFileName(fileRegisCert[0]) + "</a>";
                            ofilesRegisCert.Source = "<button class='btn btn-warning' type='button' id='RegisCert' data-toggle='modal' data-target='#ReplaceRegisCert'>Edit File</button>";

                        }
                        else
                        {
                            ofilesRegisCert.FileName = "Not Found";
                            ofilesRegisCert.Source = "<button class='btn btn-primary' type='button' id='RegisCert' data-toggle='modal' data-target='#ReplaceRegisCert'>Add File</button>";

                        }
                        oFileList.Add(ofilesRegisCert);
                    }
                    if (Directory.Exists(pathPP20) == true)
                    {
                        FileList ofilesPP20 = new FileList();
                        filePP20 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\PP20"));


                        ofilesPP20.Title = "<p>ภพ.20 :</p>";
                        if (filePP20.Length > 0)
                        {
                            ofilesPP20.FileName = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/PP20/" + Path.GetFileName(filePP20[0]) + "' target='_blank'>" + Path.GetFileName(filePP20[0]) + "</a>";
                            ofilesPP20.Source = "<button class='btn btn-warning' type='button' id='PP20' data-toggle='modal' data-target='#ReplacePP20'>Edit File</button>";

                        }
                        else
                        {
                            ofilesPP20.FileName = "Not Found";
                            ofilesPP20.Source = "<button class='btn btn-primary' type='button' id='PP20' data-toggle='modal' data-target='#ReplacePP20' >Add File</button>";

                        }
                        oFileList.Add(ofilesPP20);
                    }
                    if (Directory.Exists(pathBookBank) == true)
                    {
                        FileList ofilesBookBank = new FileList();

                        fileBookBank = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Book-Bank"));
                        ofilesBookBank.Title = "<p>Book Bank :</p>";
                        if (fileBookBank.Length > 0)
                        {
                            ofilesBookBank.FileName = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Book-Bank/" + Path.GetFileName(fileBookBank[0]) + "' target='_blank'>" + Path.GetFileName(fileBookBank[0]) + "</a>";
                            ofilesBookBank.Source = "<button class='btn btn-warning' type='button'id='BookBank' data-toggle='modal' data-target='#ReplaceBookBank'>Edit File</button>";

                        }
                        else
                        {
                            ofilesBookBank.FileName = "Not Found";
                            ofilesBookBank.Source = "<button class='btn btn-primary' type='button'  id='BookBank' data-toggle='modal' data-target='#ReplaceBookBank' >Add File</button>";
                        }
                        oFileList.Add(ofilesBookBank);
                    }
                    if (Directory.Exists(pathBOJ5) == true)
                    {
                        FileList ofilesBOJ5 = new FileList();
                        fileBOJ5 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\BOJ5"));
                        ofilesBOJ5.Title = "<p>บอจ.5 :</p>";
                        if (fileBOJ5.Length > 0)
                        {
                            ofilesBOJ5.FileName = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/BOJ5/" + Path.GetFileName(fileBOJ5[0]) + "' target='_blank'>" + Path.GetFileName(fileBOJ5[0]) + "</a>";
                            ofilesBOJ5.Source = "<button class='btn btn-warning' type='button' id='BOJ5' data-toggle='modal' data-target='#ReplaceBOJ5'>Edit File</button>";

                        }
                        else
                        {
                            ofilesBOJ5.FileName = "Not Found";
                            ofilesBOJ5.Source = "<button class='btn btn-primary' type='button' id='BOJ5' data-toggle='modal' data-target='#ReplaceBOJ5' >Add File</button>";

                        }
                        oFileList.Add(ofilesBOJ5);
                    }
                    if (Directory.Exists(pathOrgCompany) == true)
                    {
                        FileList ofilesOrgCompany = new FileList();
                        fileOrgCompany = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Organization_Company"));

                        ofilesOrgCompany.Title = "<p>Organization Company :</p>";
                        if (fileOrgCompany.Length > 0)
                        {
                            ofilesOrgCompany.FileName = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Organization_Company/" + Path.GetFileName(fileOrgCompany[0]) + "' target='_blank'>" + Path.GetFileName(fileOrgCompany[0]) + "</a>";
                            ofilesOrgCompany.Source = "<button class='btn btn-warning' type='button' id='OrgCompany' data-toggle='modal' data-target='#ReplaceOrgCompany' >Edit File</button>";

                        }
                        else
                        {
                            ofilesOrgCompany.FileName = "Not Found";
                            ofilesOrgCompany.Source = "<button class='btn btn-primary' type='button' id='OrgCompany' data-toggle='modal' data-target='#ReplaceOrgCompany' >Add File</button>";

                        }
                        oFileList.Add(ofilesOrgCompany);
                    }
                    if (Directory.Exists(pathSPS10) == true)
                    {
                        FileList ofilesSPS10 = new FileList();
                        fileSPS10 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SPS1-10"));
                        ofilesSPS10.Title = "<p>สปส1-10</p>";
                        if (fileSPS10.Length > 0)
                        {
                            ofilesSPS10.FileName = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/SPS1-10/" + Path.GetFileName(fileSPS10[0]) + "' target='_blank'>" + Path.GetFileName(fileSPS10[0]) + "</a>";
                            ofilesSPS10.Source = "<button class='btn btn-warning' type='button' id='SPS10' data-toggle='modal' data-target='#ReplaceSPS10' >Edit File</button>";

                        }
                        else
                        {
                            ofilesSPS10.FileName = "Not Found";
                            ofilesSPS10.Source = "<button class='btn btn-primary' type='button' id='SPS10' data-toggle='modal' data-target='#ReplaceSPS10' >Add File</button>";

                        }
                        oFileList.Add(ofilesSPS10);
                    }

                    //LinkFile = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\form"));                                                                                                                                                            
                    //int countfile = fileAppForm.Length + fileBOJ5.Length + fileBookBank.Length + fileOrgCompany.Length + filePP20.Length + fileRegisCert.Length + fileSPS10.Length;                    
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Context.Response.Write(js.Serialize(oFileList));
                }
            }
            catch (Exception ex)
            {
                //string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {

            }
        }
        [WebMethod]
        public void GetApplicationForm()
        {
            try
            {
                string Path1 = Server.MapPath("Document\\fileAttach\\form");
                string Atag = null;
                string[] linkFolder;
                string[] linkFile;
                string fileType = null;
                string folderName = null;

                List<FileList> oFileList = new List<FileList>();

                if (Directory.Exists(Path1) == true)
                {
                    linkFolder = System.IO.Directory.GetDirectories(Path1);

                    for (int i = 0; i < linkFolder.Length; i++)
                    {
                        linkFile = System.IO.Directory.GetFiles(linkFolder[i]);
                        folderName = Path.GetFileName(linkFolder[i]);
                        string newFolderName = folderName.Replace("_", " ");
                        for (int j = 0; j < linkFile.Length; j++)
                        {
                            fileType = Path.GetExtension(linkFile[j]);
                            string PathName = Path.GetFileName(linkFile[j]);
                            if (Path.GetExtension(linkFile[j]) == ".xlsx")
                            {
                                Atag += "<h3 class='text-white'><i class='fa-regular fa-file-excel fa-2xl'></i></h3>";
                                Atag += "<p><h5 class='text-white'>" + newFolderName + ".xlsx</h5></p>";
                                Atag += "<a class='btn btn-success btn-xl' href='Document/fileAttach/form/" + folderName + "/" + Uri.EscapeUriString(PathName) + "' target='_blank'><h5><i class='fa-solid fa-cloud-arrow-down fa-bounce fa-xl'></i> Download Here </h5></a>";
                            }
                            else if (Path.GetExtension(linkFile[j]) == ".pdf")
                            {

                                Atag += "<h3 class='text-white'><i class='fa-solid fa-file-pdf fa-2xl'></i></h3>";
                                Atag += "<p><h5 class='text-white'>" + newFolderName + ".pdf</h5></p>";
                                Atag += "<a class='btn btn-danger btn-xl' href='Document/fileAttach/form/" + folderName + "/" + Uri.EscapeUriString(PathName) + "' target='_blank'><h5><i class='fa-solid fa-cloud-arrow-down fa-bounce fa-xl'></i> Download Here </h5></a>";
                            }
                            else
                            {
                                Atag += "<h3 class='text-white'><i class='fa-solid fa-file fa-2xl'></i></h3>";
                                Atag += "<h5 class='text-white'>" + newFolderName + "</h5>";
                                Atag += "<a class='btn btn-primary btn-xl' href='Document/fileAttach/form/" + folderName + "/" + Uri.EscapeUriString(PathName) + "' target='_blank'><h5><i class='fa-solid fa-file-arrow-down fa-bounce fa-xl'></i> Download Here </h5></a>";
                            }

                            Atag += "<hr/>";
                        }
                    }
                    //LinkFile = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\form"));

                    //for(int i =0; i<LinkFile.Length; i++)
                    //{
                    //    FileList ofiles = new FileList();
                    //    if (Path.GetFileName(LinkFile[i]) != null)
                    //    {
                    //        Atag += "<hr class='divider divider-light' />";
                    //        Atag += "<i class='fa-solid fa-file-pdf'></i>";
                    //        Atag += "<h5 class='text-white mb-4'>Application Form</h5>";
                    //        Atag += "<a class='btn btn-light btn-xl' href='Document/fileAttach/form/Application_Form" + Path.GetFileName(LinkFile[i]) + "' target='_blank'>Download Here!</a>";

                    //    }
                    //}
                    //JavaScriptSerializer js = new JavaScriptSerializer();
                    //Context.Response.Write(js.Serialize(Atag));
                    Context.Response.Write(JsonConvert.SerializeObject(Atag).ToString());
                    Context.Response.End();
                }

            }
            catch (Exception ex)
            {
                //string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {

            }
        }

        [WebMethod]
        public void GetFormList()
        {
            try
            {
                string[] FolderName = null;
                string Path1 = Server.MapPath("Document\\fileAttach\\form\\");
                string[] fileForm = null;

                List<FileList> oFileList = new List<FileList>();

                if (Directory.Exists(Path1) == true)
                {
                    FolderName = System.IO.Directory.GetDirectories(Path1);

                    //string[] Atag = null;
                    //string folderName = null;


                    for (int i = 0; i < FolderName.Length; i++)
                    {
                        if (Directory.Exists(FolderName[i]) == true)
                        {
                            string FolderName1 = Path.GetFileName(FolderName[i]);

                            string pathfileForm = Server.MapPath("Document\\fileAttach\\form\\" + FolderName1);

                            if (Directory.Exists(pathfileForm) == true)
                            {
                                FileList ofilesAppForm = new FileList();
                                fileForm = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\form\\" + FolderName1));
                                ofilesAppForm.Title = FolderName1;

                                for (int j = 0; j < fileForm.Length; j++)
                                {
                                    //Encode Url Specials Char
                                    string PathName = Path.GetFileName(fileForm[j]);
                                    ofilesAppForm.FileName = "<a href=Document/fileAttach/form/" + FolderName1 + "/" + Uri.EscapeUriString(PathName) + " target='_blank'>" + Path.GetFileName(fileForm[j]) + "</a>";
                                    ofilesAppForm.Source = "<button type='button' class='btn btn-secondary' onclick=javascript:editFile_Modal('" + Uri.EscapeUriString(PathName) + "','" + FolderName1 + "')>Edit</button>";
                                    ofilesAppForm.Source1 = "<button type='button' class='btn btn-danger' onclick=javascript:deleteFile('" + Uri.EscapeUriString(PathName) + "','" + FolderName1 + "')>Delete</button>";
                                    //ofilesAppForm.Source = "<a href='" + fileForm[j] + "' target='_blank'>" + Path.GetFileName(fileForm[j]) + "</a>";

                                }
                                oFileList.Add(ofilesAppForm);
                                //LinkFile = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\form"));                                                                                                                                                            
                                //int countfile = fileAppForm.Length + fileBOJ5.Length + fileBookBank.Length + fileOrgCompany.Length + filePP20.Length + fileRegisCert.Length + fileSPS10.Length;                    

                            }

                        }
                    }
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Context.Response.Write(js.Serialize(oFileList));
                }
            }
            catch (Exception ex)
            {
                //string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {

            }
        }

        [WebMethod]
        public void ManualLink()
        {
            try
            {
                string Atag = null;
                string[] FolderName = null;
                string Path1 = Server.MapPath("Document\\fileAttach\\Manual\\");
                string[] fileForm = null;

                List<FileList> oFileList = new List<FileList>();



                if (Directory.Exists(Path1) == true)
                {
                    FolderName = System.IO.Directory.GetFiles(Path1);

                    for (int i = 0; i < FolderName.Length; i++)
                    {
                        if (File.Exists(FolderName[i]) == true)
                        {
                            if (Path.GetExtension(FolderName[i]) == ".pdf")
                            {
                                string FileName1 = Path.GetFileName(FolderName[i]);
                                string ManualName = Uri.EscapeUriString(FileName1);
                                string URL_Manual = "Document\\fileAttach\\Manual\\" + ManualName;
                                Atag += "<a class='collapse-item' href='Document\\fileAttach\\Manual\\" + ManualName + "' target='_blank'> Manual</a>";
                            }


                            //Encode Url Specials Char
                            //Response.Write("Document\\fileAttach\\Manual\\" + ManualName);
                            //HttpContext.Current.Response.Redirect(URL_Manual);


                        }
                    }
                    Context.Response.Write(JsonConvert.SerializeObject(Atag).ToString());
                    Context.Response.End();
                }


            }
            catch (Exception ex)
            {
                //string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {

            }
        }

    }
}
