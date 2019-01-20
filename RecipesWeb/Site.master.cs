using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    Connections con = new Connections();
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    string Com_username="";

    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
            if (Session["LoginCom"] != null && Session["LoginUser"] != null)
            {
                Label_com.Text = Session["LoginCom"].ToString();
                Label_user.Text = Session["LoginUser"].ToString();

                Com_username = Session["LoginCom"].ToString();

                DataTable dtcountingred = con.SelecthostProc(Label_com.Text, "Count_Ingred", null, null);
                if (dtcountingred.Rows.Count > 0)
                {
                    Label_ingred.Text = dtcountingred.Rows[0][0].ToString();
                }

                DataTable dtcountbatch = con.SelecthostProc(Label_com.Text, "Count_Batch", null, null);
                if (dtcountingred.Rows.Count > 0)
                {
                    Label_batch.Text = dtcountbatch.Rows[0][0].ToString();
                }

                DataTable dtcountrecipe = con.SelecthostProc(Label_com.Text, "Count_Recipe", null, null);
                if (dtcountingred.Rows.Count > 0)
                {
                    Label_recipe.Text = dtcountrecipe.Rows[0][0].ToString();
                }

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }
    protected void LinkButton_Exit_Click(object sender, EventArgs e)
    {
        Session["LoginCom"] = null;
        Session["LoginUser"] = null;
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }
}