using Newtonsoft.Json;
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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupplierRegistration
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies.Get("EmployeeId") == null && Request.Cookies.Get("Email") == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Employee ID is Null, Please Login First.'); ", true);
            }
            else if (Request.Cookies.Get("Email") == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'E-mail is Null, Please Login First.'); ", true);
            }
            else
            {
                //Database Connect
                StringBuilder sql = new StringBuilder();
                SqlConnection conn = new SqlConnection();
                SqlCommand sqlcmd = new SqlCommand();
                DataTable oDt = new DataTable();
                SqlDataAdapter oDa;
                //กำหนดตัวแปร 

                try
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    conn = new SqlConnection(Properties.Settings.Default.Conn);
                    conn.Open();
                    //Create IncidentNo
                    string ID = Request.Cookies.Get("EmployeeId").Value;

                    sql.Append("spCheckUser");
                    sqlcmd = new SqlCommand(sql.ToString(), conn);
                    sqlcmd.Parameters.AddWithValue("ID", ID);

                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    oDa = new SqlDataAdapter(sqlcmd);
                    oDa.Fill(oDt);
                    if (oDt.Rows.Count > 0)
                    {
                        //userSpan.Text = oDt.Rows[0]["FullName"].ToString();
                        //userDepartment.Text = oDt.Rows[0]["UnitCodeName"].ToString();
                        //userPosition.Text = oDt.Rows[0]["PositionName"].ToString();
                        // Alter Data แก้ไขข้อมูล Front = Back
                        userSpan.Text = Request.Cookies.Get("FullName").Value;
                        userDepartment.Text = Request.Cookies.Get("Department").Value;
                        userPosition.Text = Request.Cookies.Get("PositionName").Value;
                        nameUser.Text = Request.Cookies.Get("FullName").Value;
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
        public void Logout_Click(object sender, EventArgs e)
        {
            string[] cookies = Request.Cookies.AllKeys;
            foreach (string cookie in cookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddMonths(-1);

            }
            Response.Redirect("Vendor_Login.aspx");
        }

        //public void Manual_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string Atag = null;
        //        string[] FolderName = null;
        //        string Path1 = Server.MapPath("Document\\fileAttach\\Manual\\");
        //        string[] fileForm = null;

        //        List<FileList> oFileList = new List<FileList>();



        //        if (Directory.Exists(Path1) == true)
        //        {
        //            FolderName = System.IO.Directory.GetFiles(Path1);

        //            for (int i = 0; i < FolderName.Length; i++)
        //            {
        //                if (File.Exists(FolderName[i]) == true)
        //                {
        //                    string FileName1 = Path.GetFileName(FolderName[i]);
        //                    string ManualName = Uri.EscapeUriString(FileName1);
        //                    string URL_Manual = "Document\\fileAttach\\Manual\\" + ManualName;
        //                    Atag += "<a class='collapse-item' href='Document\\fileAttach\\Manual\\" + ManualName + "' target='_blank'>> Manual</a>";

        //                    //Encode Url Specials Char
        //                    //Response.Write("Document\\fileAttach\\Manual\\" + ManualName);
        //                    //HttpContext.Current.Response.Redirect(URL_Manual);


        //                }
        //            }
        //            Context.Response.Write(JsonConvert.SerializeObject(Atag).ToString());
        //            Context.Response.End();
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //string script = "<script type=\"text/javascript\">alert('" + ex.Message + "');</script>";
        //        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
        //    }
        //    finally
        //    {

        //    }
        //}



    }
}