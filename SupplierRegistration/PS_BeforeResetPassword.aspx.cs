using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SupplierRegistration
{
    public partial class PS_BeforeResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] cookies = Request.Cookies.AllKeys;
            foreach (string cookie in cookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-30);
            }
        }

        protected void ResetCheck_Click(object sender, EventArgs e)
        {
            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;
            //กำหนดตัวแปร 

            string PS_ID = Login_ID.Value;
            string Pass = Login_Password.Value;
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn = new SqlConnection(Properties.Settings.Default.Conn);
                conn.Open();
                //Create IncidentNo
                sql.Append("spLogin");
                sqlcmd = new SqlCommand(sql.ToString(), conn);
                sqlcmd.Parameters.AddWithValue("ID", PS_ID);
                sqlcmd.Parameters.AddWithValue("Pass", Pass);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //Fill Data
                oDa = new SqlDataAdapter(sqlcmd);
                oDa.Fill(oDt);

                if (oDt.Rows.Count > 0)
                {
                    //อัพโหลดขึ้น Cookie ***Impotant***
                    HttpCookie CookieUserid = new HttpCookie("EmployeeId");
                    CookieUserid.Value = oDt.Rows[0]["EmpID"].ToString();
                    Response.Cookies.Add(CookieUserid);

                    HttpCookie CookieName = new HttpCookie("PasswordOld");
                    CookieName.Value = oDt.Rows[0]["Password"].ToString();
                    Response.Cookies.Add(CookieName);

                    //SweetAlert Function ส่งค่าไปที่ function oCheck
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oCheck('success', 'Success', 'เช็คเสร็จสมบูรณ์.');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Wrong', 'รหัสผ่าน / รหัสพนักงานไม่ถูกต้อง.');", true);
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