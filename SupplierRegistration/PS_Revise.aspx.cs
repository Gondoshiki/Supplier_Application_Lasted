using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupplierRegistration
{
    public partial class PS_Revise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Get_App_Detail();
                Get_Transaction();
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
            //ดึง AppID From Database.Form
            string EncodeID = Request.QueryString["ID"];
            string AppID = null;

            if (Request.Cookies.Get("EmployeeId") == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Employee ID is Null, Please Login First.');", true);
            }
            else
            {


                try
                {
                    if (EncodeID.Length < 14 || EncodeID.Length > 20)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oList('error', 'Error', 'ID is Complete, Please try again.');", true);

                    }
                    else
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
                                sqlcmd1.Parameters.AddWithValue("ID", AppID);
                                sqlcmd1.CommandType = CommandType.StoredProcedure;
                                oDa1 = new SqlDataAdapter(sqlcmd1);
                                oDa1.Fill(oDt1);
                                if (oDt1.Rows.Count > 0)
                                {
                                    // Alter Data แก้ไขข้อมูล Front = Back
                                    App_ID.Text = oDt1.Rows[0]["App_ID"].ToString();
                                    Vendor_Name_R.Value = oDt1.Rows[0]["Vendor_Name"].ToString();
                                    Vendor_PIC_R.Value = oDt1.Rows[0]["Vendor_PIC"].ToString();
                                    Email_R.Value = oDt1.Rows[0]["Email"].ToString();
                                    Comment.Text = oDt1.Rows[0]["Comment"].ToString();
                                    GmMail.Value = oDt1.Rows[0]["CC"].ToString();
                                    UploadFile.Value = oDt1.Rows[0]["FileUpload"].ToString();

                                    // Status Check And Redirect page.
                                    string GetStat = oDt1.Rows[0]["Status"].ToString();
                                    if (GetStat == "D")
                                    {
                                        Response.Redirect("Draft.aspx?id=" + EncodeID, false);
                                    }
                                    else if (GetStat == "P")
                                    {
                                        Response.Redirect("PS_Detail.aspx?id=" + EncodeID, false);
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
                            ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Employee ID is Null, Please Login First.');", true);

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
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'ID is Null, Please Login First.');", true);
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

        protected void Upload_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                SqlConnection conn = new SqlConnection();
                SqlCommand sqlcmd = new SqlCommand();
                DataTable oDt = new DataTable();
                SqlDataReader oDr;
                string id = Request.QueryString["ID"];
                string filecode = hdfFile1.Value;
                if (id != null)
                {

                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn = new SqlConnection(Properties.Settings.Default.Conn);
                    conn.Open();


                    string status = "P";
                    var timestamp = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                    string detail = id + " Files was Edit by Admin";
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
                        string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5");
                        string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank");
                        string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company");
                        string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20");
                        string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate");
                        string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10");
                        string pathSME = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research");

                        if (Directory.Exists(pathAppForm) == false)
                        {
                            Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form"));
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
                        if (Directory.Exists(pathSME) == false)
                        {
                            Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research"));
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
                                    string pathBOJ5 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\BOJ5");
                                    string pathBookBank = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Book-Bank");
                                    string pathOrgCompany = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Organization_Company");
                                    string pathPP20 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\PP20");
                                    string pathRegisCert = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Registration_Certificate");
                                    string pathSPS10 = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SPS1-10");
                                    string pathSME = Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research");

                                    if (Directory.Exists(pathAppForm) == false)
                                    {
                                        Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\Application_Form"));
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
                                    if (Directory.Exists(pathSME) == false)
                                    {
                                        Directory.CreateDirectory(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\SMEs_Research"));
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
                    else if (filecode == "2")
                    {
                        oFile = Context.Request.Files[2];
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
                    else if (filecode == "3")
                    {
                        oFile = Context.Request.Files[3];
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
                    else if (filecode == "4")
                    {
                        oFile = Context.Request.Files[4];
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
                    else if (filecode == "5")
                    {
                        oFile = Context.Request.Files[5];
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
                    else if (filecode == "6")
                    {
                        oFile = Context.Request.Files[6];
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
                    else if (filecode == "7")
                    {
                        oFile = Context.Request.Files[7];
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


                    //HttpPostedFile oFile = null;
                    //for (int i = 0; i < countfile; i++)
                    //{
                    //    oFile = Context.Request.Files[i];
                    //    string filename = oFile.FileName.ToString().Replace("'", "").Replace("%", "").Replace("$", "").Replace("#", "");
                    //    oFile.SaveAs(Server.MapPath("Document\\fileAttach\\" + folder + "\\" + subfolder + "\\" + id + "\\" + filename));

                    //}

                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFileUploaded('success', 'Success', 'Upload success, Value has inserted.', '" + id + "');", true);




                }

            }

            catch (Exception ex)
            {

            }
            finally
            {

            }
        }


    }
}