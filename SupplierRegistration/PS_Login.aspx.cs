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
    public partial class PS_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies.Get("RecCode") == null)
            {
                //Delete All Cookies
                string[] cookies = Request.Cookies.AllKeys;
                foreach (string cookie in cookies)
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-30);
                }
                
            }
            else
            {
                LinkURL.Value = Request.Cookies.Get("RecCode").Value;
                string[] cookies = Request.Cookies.AllKeys;
                foreach (string cookie in cookies)
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-30);
                }
            }
            

        }
        protected void Login_Click(object sender, EventArgs e)              
        {
            //Database Connect
            StringBuilder sql = new StringBuilder();
            SqlConnection conn = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            DataTable oDt = new DataTable();
            SqlDataAdapter oDa;
            //กำหนดตัวแปร 
            string Link = LinkURL.Value;
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

                    HttpCookie CookieName = new HttpCookie("Name");
                    CookieName.Value = oDt.Rows[0]["Name"].ToString();
                    Response.Cookies.Add(CookieName);

                    HttpCookie CookieFullName = new HttpCookie("FullName");
                    CookieFullName.Value = oDt.Rows[0]["FullName"].ToString();
                    Response.Cookies.Add(CookieFullName);

                    HttpCookie CookiePositionName = new HttpCookie("PositionName");
                    CookiePositionName.Value = oDt.Rows[0]["PositionName"].ToString();
                    Response.Cookies.Add(CookiePositionName);

                    HttpCookie CookieUnitCodeName = new HttpCookie("Department");
                    CookieUnitCodeName.Value = oDt.Rows[0]["UnitCodeName"].ToString();
                    Response.Cookies.Add(CookieUnitCodeName);

                    HttpCookie CookieEmail = new HttpCookie("Email");
                    CookieEmail.Value = oDt.Rows[0]["Email"].ToString();
                    Response.Cookies.Add(CookieEmail);

                    HttpCookie CookiePhone = new HttpCookie("Phone");
                    CookiePhone.Value = oDt.Rows[0]["Phone"].ToString();
                    Response.Cookies.Add(CookiePhone);

                    HttpCookie CookieAuth = new HttpCookie("Authorize");
                    CookieAuth.Value = oDt.Rows[0]["Authorize"].ToString();
                    Response.Cookies.Add(CookieAuth);

                    HttpCookie CookieUpdateDate = new HttpCookie("UpdateDate");
                    CookieUpdateDate.Value = oDt.Rows[0]["UpdateDate"].ToString();
                    Response.Cookies.Add(CookieUpdateDate);





                    if (Link != "")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oRedirect('success', 'Success', 'Login success.', '"+ Link + "');", true);

                    }
                    else
                    {
                        //SweetAlert Function ส่งค่าไปที่ function oLog
                        ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oLog('success', 'Success', 'Login success.');", true);

                    }

                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, typeof(System.Web.UI.Page), "Popup", "oAlert('error', 'Wrong', 'Account not found Or Password invalid.');", true);

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