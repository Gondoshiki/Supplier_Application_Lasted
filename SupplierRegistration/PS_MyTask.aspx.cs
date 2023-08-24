using SupplierRegistration.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SupplierRegistration
{
    public partial class PS_MyTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Get_App_Detail();
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
            if (Request.Cookies.Get("EmployeeId") == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oFail('warning', 'Error', 'Employee ID is Null, Please Login First.');", true);
            }
            else
            {
                try
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    string EmpID = Request.Cookies.Get("EmployeeId").Value;
                    conn = new SqlConnection(Properties.Settings.Default.Conn);
                    conn.Open();
                    //Create IncidentNo
                    sql.Append("spCountMyTask");
                    sqlcmd = new SqlCommand(sql.ToString(), conn);

                    sqlcmd.Parameters.AddWithValue("EK", EmpID);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    oDa = new SqlDataAdapter(sqlcmd);
                    oDa.Fill(oDt);
                    if (oDt.Rows.Count > 0)
                    {
                        // Alter Data แก้ไขข้อมูล Front = Back
                        ProcessC.Text = oDt.Rows[0]["Process"].ToString();
                        ReviceC.Text = oDt.Rows[0]["Revice"].ToString();
                        DraftC.Text = oDt.Rows[0]["Draft"].ToString();
                        //FinishC.Text = oDt.Rows[0]["Finish"].ToString();




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