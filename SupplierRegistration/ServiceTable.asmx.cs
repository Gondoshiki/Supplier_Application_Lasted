
using SupplierRegistration.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

namespace SupplierRegistration
{
    /// <summary>
    /// Summary description for ServiceTable
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ServiceTable : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetListTableP()
        {
            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;

            List<List_Object> ObjectList = new List<List_Object>();
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                //Create IncidentNo
                //ดึงข้อมูลจาก Database ที่กำหนด
                sql.Append("spGetList");
                sqlcmd = new SqlCommand(sql.ToString(), conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                oDa = new SqlDataAdapter(sqlcmd);
                oDa.Fill(oDt);

                if (oDt.Rows.Count > 0)
                {
                    for (int i = 0; i < oDt.Rows.Count; i++)
                    {
                        List_Object Objects = new List_Object();

                        string appID = oDt.Rows[i]["App_ID"].ToString();
                        string encodeApp_id = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(appID));

                        if (oDt.Rows[i]["Status"].ToString() == "P")
                        {
                            Objects.App_ID = "<a href='PS_Detail.aspx?id=" + encodeApp_id + "'>" + oDt.Rows[i]["App_ID"].ToString() + "</a>"; Objects.V_Name = oDt.Rows[i]["Vendor_Name"].ToString();
                            Objects.Status = "<p style='color: blue'>" + "Process" + "</p>";
                        }
                        else if (oDt.Rows[i]["Status"].ToString() == "R")
                        {
                            Objects.App_ID = "<a href='PS_Revise.aspx?id=" + encodeApp_id + "'>" + oDt.Rows[i]["App_ID"].ToString() + "</a>"; Objects.V_Name = oDt.Rows[i]["Vendor_Name"].ToString();
                            Objects.Status = "<p style='color: orange'>" + "Revice" + "</p>";
                        }

                        // Alter Data
                        Objects.V_PIC = oDt.Rows[i]["Vendor_PIC"].ToString();
                        Objects.Email = oDt.Rows[i]["Email"].ToString();
                        DateTime Date1 = DateTime.Parse(oDt.Rows[i]["Update_Date"].ToString());
                        string Date2 = Date1.ToString("dd/MM/yyyy HH:mm:ss");
                        Objects.UpdateBy = Date2;

                        DateTime update1 = DateTime.Parse(oDt.Rows[i]["Update_Date"].ToString());
                        string Update2 = update1.ToString("yyyy MM dd HH:mm:ss");
                        Objects.UpdateHidden = Update2;

                        ObjectList.Add(Objects);

                    }
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Context.Response.Write(js.Serialize(ObjectList));

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
        [WebMethod]
        public void GetMyTask()
        {
            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;
            List<List_Object> ObjectList = new List<List_Object>();

            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
            if (Request.Cookies.Get("EmployeeId") == null)
            {
                Context.Response.Redirect("PS_Login.aspx");
            }
            string EmpID = null;
            //if (EmpID == null)
            //{
            //    HttpContext.Current.Response.Redirect("Vendor_Login.aspx");
            //}

            try
            {
                EmpID = Request.Cookies.Get("EmployeeId").Value;
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                //Create IncidentNo
                sql.Append("spGetMytaskList");
                sqlcmd = new SqlCommand(sql.ToString(), conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                oDa = new SqlDataAdapter(sqlcmd);
                oDa.Fill(oDt);

                if (oDt.Rows.Count > 0)
                {
                    EmpID = Request.Cookies.Get("EmployeeId").Value;
                    for (int i = 0; i < oDt.Rows.Count; i++)
                    {
                        if (oDt.Rows[i]["CreateByID"].ToString() == EmpID)
                        {
                            List_Object Objects = new List_Object();
                            string appID = oDt.Rows[i]["App_ID"].ToString();
                            string encodeApp_id = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(appID));

                            if (oDt.Rows[i]["Status"].ToString() == "R")
                            {
                                Objects.App_ID = "<a href='PS_Revise.aspx?id=" + encodeApp_id + "'>" + oDt.Rows[i]["App_ID"].ToString() + "</a>"; Objects.V_Name = oDt.Rows[i]["Vendor_Name"].ToString();
                                Objects.Status = "<p style='color: orange'>" + "Revise" + "</p>";
                            }
                            // Alter Data 
                            else if (oDt.Rows[i]["Status"].ToString() == "D")
                            {
                                Objects.App_ID = "<a href='Draft.aspx?id=" + encodeApp_id + "'>" + oDt.Rows[i]["App_ID"].ToString() + "</a>";
                                Objects.Status = "<p style='color: grey'>" + "Draft" + "</p>";
                            }
                            else if (oDt.Rows[i]["Status"].ToString() == "P")
                            {
                                Objects.App_ID = "<a href='PS_Detail.aspx?id=" + encodeApp_id + "'>" + oDt.Rows[i]["App_ID"].ToString() + "</a>";
                                Objects.Status = "<p style='color: blue'>" + "Process" + "</p>";
                            }
                            Objects.V_Name = oDt.Rows[i]["Vendor_Name"].ToString();
                            Objects.V_PIC = oDt.Rows[i]["Vendor_PIC"].ToString();
                            Objects.Email = oDt.Rows[i]["Email"].ToString();
                            DateTime Date1 = DateTime.Parse(oDt.Rows[i]["Update_Date"].ToString());
                            string Date2 = Date1.ToString("dd/MMM/yyyy HH:mm:ss");
                            Objects.UpdateBy = Date2;

                            DateTime update1 = DateTime.Parse(oDt.Rows[i]["Update_Date"].ToString());
                            string Update2 = update1.ToString("yyyy/MM/dd HH:mm:ss");
                            Objects.UpdateHidden = Update2;

                            ObjectList.Add(Objects);
                        }

                    }
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Context.Response.Write(js.Serialize(ObjectList));

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
        [WebMethod]
        public void GetHistoryTable()
        {
            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;

            List<History_Table> ObjectList = new List<History_Table>();
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                //Create IncidentNo
                //ดึงข้อมูลจาก Database ที่กำหนด
                sql.Append("spGetHistoryList");
                sqlcmd = new SqlCommand(sql.ToString(), conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                oDa = new SqlDataAdapter(sqlcmd);
                oDa.Fill(oDt);

                if (oDt.Rows.Count > 0)
                {
                    for (int i = 0; i < oDt.Rows.Count; i++)
                    {

                        History_Table Objects = new History_Table();

                        string appID = oDt.Rows[i]["App_ID"].ToString();
                        string encodeApp_id = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(appID));

                        if (oDt.Rows[i]["Status"].ToString() == "F")
                        {
                            Objects.App_ID = "<a href='PS_Finish.aspx?id=" + encodeApp_id + "'>" + oDt.Rows[i]["App_ID"].ToString() + "</a>";

                        }
                        else if (oDt.Rows[i]["Status"].ToString() == "J")
                        {
                            Objects.App_ID = "<a href='PS_Reject.aspx?id=" + encodeApp_id + "'>" + oDt.Rows[i]["App_ID"].ToString() + "</a>";

                        }
                        // Alter Data   "<a href='PS_Detail.aspx?id=" + '></a>
                        Objects.V_Name = oDt.Rows[i]["Vendor_Name"].ToString();
                        Objects.V_PIC = oDt.Rows[i]["Vendor_PIC"].ToString();
                        Objects.Email = oDt.Rows[i]["Email"].ToString();

                        if (oDt.Rows[i]["Status"].ToString() == "F")
                        {
                            Objects.Status = "<p style='color: green'>" + "Finish" + "</p>";
                        }
                        else if (oDt.Rows[i]["Status"].ToString() == "J")
                        {
                            Objects.Status = "<p style='color: red'>" + "Reject" + "</p>";
                        }

                        Objects.Comment = oDt.Rows[i]["Comment"].ToString();

                        DateTime Date1 = DateTime.Parse(oDt.Rows[i]["TimeStamp"].ToString());
                        string Date2 = Date1.ToString("dd/MMM/yyyy HH:mm:ss");
                        Objects.Update_Date = Date2;

                        DateTime update1 = DateTime.Parse(oDt.Rows[i]["TimeStamp"].ToString());
                        string Update2 = update1.ToString("yyyy MM dd HH:mm:ss");
                        Objects.Hidden_Date = Update2;

                        ObjectList.Add(Objects);
                    }
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Context.Response.Write(js.Serialize(ObjectList));

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


        [WebMethod]
        public void SA_User()
        {
            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;
            List<SA_User> ObjectList = new List<SA_User>();

            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;

            //string AppID = Request.QueryString["EmployeeId"];
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                //Create IncidentNo
                sql.Append("spGetSA_User");
                sqlcmd = new SqlCommand(sql.ToString(), conn);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                oDa = new SqlDataAdapter(sqlcmd);
                var Timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                oDa.Fill(oDt);

                if (oDt.Rows.Count > 0)
                {
                    for (int i = 0; i < oDt.Rows.Count; i++)
                    {

                        SA_User Objects = new SA_User();
                        Objects.EmpID = oDt.Rows[i]["EmpID"].ToString();
                        Objects.FullName = oDt.Rows[i]["FullName"].ToString();
                        Objects.Department = oDt.Rows[i]["UnitCodeName"].ToString();
                        Objects.PositionName = oDt.Rows[i]["PositionName"].ToString();
                        Objects.Email = oDt.Rows[i]["Email"].ToString();
                        Objects.Phone = oDt.Rows[i]["Phone"].ToString();

                        DateTime Date1 = DateTime.Parse(oDt.Rows[i]["UpdateDate"].ToString());
                        string Date2 = Date1.ToString("dd/MMM/yyyy HH:mm:ss");
                        Objects.Update_Date = Date2;

                        DateTime update1 = DateTime.Parse(oDt.Rows[i]["UpdateDate"].ToString());
                        string Update2 = update1.ToString("yyyy/MM/dd/ HH:mm:ss");
                        Objects.Hidden_Date = Update2;
                        //Objects.Update_Date = oDt.Rows[i]["UpdateDate"].ToString();
                        Objects.Source0 = "<button type='button' class='btn btn-warning' onclick=javascript:Reset_Password('" + oDt.Rows[i]["EmpID"].ToString() + "','"+ oDt.Rows[0]["TokenID"].ToString()+"')>Reset Password</button>";
                        Objects.Source2 = "<button class='btn btn-dark' id='btnResetAuth' type='submit' runat='server' onserverclick='sendEmailResetPassword_Click('"+ oDt.Rows[i]["EmpID"].ToString() + "')' style='display: none'>Reset</button>";
                        Objects.Source = "<button type='button' class='btn btn-secondary' onclick=javascript:editUser_Modal('" + oDt.Rows[i]["EmpID"].ToString() + "','" + oDt.Rows[i]["Email"].ToString() + "','" + oDt.Rows[i]["Phone"].ToString() + "') >Edit</button>";
                        Objects.Source1 = "<button type='button' class='btn btn-danger' onclick=javascript:deleteUser('" + oDt.Rows[i]["EmpID"].ToString() + "') >Delete</button>";
                        ObjectList.Add(Objects);
                    }
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    Context.Response.Write(js.Serialize(ObjectList));

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

        [WebMethod]
        public void ReviseUploadList(string id)
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
                    string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\BOJ5");
                    string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Book-Bank");
                    string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Organization_Company");
                    string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\PP20");
                    string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Registration_Certificate");
                    string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SPS1-10");
                    string pathSME = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SMEs_Research");

                    if (Directory.Exists(pathAppForm) == true)
                    {
                        FileList ofilesAppForm = new FileList();
                        fileAppForm = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\Application_Form"));
                        ofilesAppForm.Title = "<p>Application for Registration :</p>";
                        if (fileAppForm.Length > 0)
                        {
                            ofilesAppForm.FileName = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/Application_Form/" + Path.GetFileName(fileAppForm[0]) + "' target='_blank'>" + Path.GetFileName(fileAppForm[0]) + "</a>";
                            //ofilesAppForm.Source = "<button class='btn btn-warning' type='button' id='AppRegis' data-toggle='modal' data-target='#ReplaceAppRegis'>Edit File</button>";

                        }
                        else
                        {
                            ofilesAppForm.FileName = "Not Found";
                            //ofilesAppForm.Source = "<button class='btn btn-primary' type='button' id='AppRegis' data-toggle='modal' data-target='#ReplaceAppRegis' >Add File</button>";
                        }
                        oFileList.Add(ofilesAppForm);
                    }
                    if (Directory.Exists(pathSME) == true)
                    {
                        FileList ofilesSME = new FileList();
                        fileSME = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SMEs_Research"));
                        ofilesSME.Title = "<p>SMEs Research : </p>";
                        if (fileSME.Length > 0)
                        {
                            ofilesSME.FileName = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/SMEs_Research/" + Path.GetFileName(fileSME[0]) + "' target='_blank'>" + Path.GetFileName(fileSME[0]) + "</a>";
                            //ofilesSPS10.Source = "<button class='btn btn-warning' type='button' id='SPS10' data-toggle='modal' data-target='#ReplaceSPS10' >Edit File</button>";

                        }
                        else
                        {
                            ofilesSME.FileName = "Not Found";
                            //ofilesSPS10.Source = "<button class='btn btn-primary' type='button' id='SPS10' data-toggle='modal' data-target='#ReplaceSPS10' >Add File</button>";

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
                            //ofilesRegisCert.Source = "<button class='btn btn-warning' type='button' id='RegisCert' data-toggle='modal' data-target='#ReplaceRegisCert'>Edit File</button>";

                        }
                        else
                        {
                            ofilesRegisCert.FileName = "Not Found";
                            //ofilesRegisCert.Source = "<button class='btn btn-primary' type='button' id='RegisCert' data-toggle='modal' data-target='#ReplaceRegisCert'>Add File</button>";

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
                            //ofilesPP20.Source = "<button class='btn btn-warning' type='button' id='PP20' data-toggle='modal' data-target='#ReplacePP20'>Edit File</button>";

                        }
                        else
                        {
                            ofilesPP20.FileName = "Not Found";
                            //ofilesPP20.Source = "<button class='btn btn-primary' type='button' id='PP20' data-toggle='modal' data-target='#ReplacePP20' >Add File</button>";

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
                            //ofilesBookBank.Source = "<button class='btn btn-warning' type='button'id='BookBank' data-toggle='modal' data-target='#ReplaceBookBank'>Edit File</button>";

                        }
                        else
                        {
                            ofilesBookBank.FileName = "Not Found";
                            //ofilesBookBank.Source = "<button class='btn btn-primary' type='button'  id='BookBank' data-toggle='modal' data-target='#ReplaceBookBank' >Add File</button>";
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
                            //ofilesBOJ5.Source = "<button class='btn btn-warning' type='button' id='BOJ5' data-toggle='modal' data-target='#ReplaceBOJ5'>Edit File</button>";

                        }
                        else
                        {
                            ofilesBOJ5.FileName = "Not Found";
                            //ofilesBOJ5.Source = "<button class='btn btn-primary' type='button' id='BOJ5' data-toggle='modal' data-target='#ReplaceBOJ5' >Add File</button>";

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
                            //ofilesOrgCompany.Source = "<button class='btn btn-warning' type='button' id='OrgCompany' data-toggle='modal' data-target='#ReplaceOrgCompany' >Edit File</button>";

                        }
                        else
                        {
                            ofilesOrgCompany.FileName = "Not Found";
                            //ofilesOrgCompany.Source = "<button class='btn btn-primary' type='button' id='OrgCompany' data-toggle='modal' data-target='#ReplaceOrgCompany' >Add File</button>";

                        }
                        oFileList.Add(ofilesOrgCompany);
                    }
                                       
                    if (Directory.Exists(pathSPS10) == true)
                    {
                        FileList ofilesSPS10 = new FileList();
                        fileSPS10 = System.IO.Directory.GetFiles(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + AppID + "\\SPS1-10"));
                        ofilesSPS10.Title = "สปส1-10";
                        if (fileSPS10.Length > 0)
                        {
                            ofilesSPS10.FileName = "<a href='Document/fileAttach/" + folder + "/" + subfolder + "/" + AppID + "/SPS1-10/" + Path.GetFileName(fileSPS10[0]) + "' target='_blank'>" + Path.GetFileName(fileSPS10[0]) + "</a>";
                            //ofilesSPS10.Source = "<button class='btn btn-warning' type='button' id='SPS10' data-toggle='modal' data-target='#ReplaceSPS10' >Edit File</button>";

                        }
                        else
                        {
                            ofilesSPS10.FileName = "Not Found";
                            //ofilesSPS10.Source = "<button class='btn btn-primary' type='button' id='SPS10' data-toggle='modal' data-target='#ReplaceSPS10' >Add File</button>";

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


    }
}
