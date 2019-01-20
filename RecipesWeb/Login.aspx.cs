using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    Connections con = new Connections();
    string Com_user;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            int cu = User_id.Value.IndexOf("/");
            string cu1 = "";
            string cu2 = "";

            if (cu != -1)
            {
                string[] h = User_id.Value.Split('/');
                cu1 = h[0];
                cu2 = h[1];

                Com_user = cu1;
                string UserName = cu2;
                string Password = User_pass.Value;

                string urlMain = "~/ar/Default.aspx";
                string urlMainen = "Default.aspx";

                if (chkkeepMe.Checked)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                }
                Response.Cookies["UserName"].Value = UserName.Trim();
                Response.Cookies["Password"].Value = Password.Trim();

                int results = 0;
                if (UserName != string.Empty && Password != string.Empty)
                {
                    results = con.ValidateLogin(Com_user, UserName.Trim(), Password.Trim(), "Login_SignIn");
                    if (results == 1)
                    {
                        DataTable dt = con.SelecthostProc(Com_user, "User_SelectByUsername", new string[] { "username" }, UserName.Trim());

                        Session.Add("LoginCom", Com_user);
                        Session.Add("LoginUser", UserName);

                        if (dt.Rows[0][4].ToString() == "ar")
                        {
                            Response.Redirect(urlMain);
                        }
                        else
                        {
                            Response.Redirect(urlMainen);
                        }

                        msg.Text = "";
                    }
                    else
                    {
                        msg.ForeColor = Color.Red;
                        msg.Text = "Login not successful. Please try again.";
                    }
                }
                else
                {
                    msg.ForeColor = Color.Red;
                    msg.Text = "Login not successful. Enter Password.";
                }
            }
            else
            {
                
                msg.ForeColor = Color.Red;
                msg.Text = "Login not successful. Please try again.";
            }
        }
        catch (Exception ex)
        {
            con.Ctrl_Log(Com_user, ex.Message);
        }
    }
}