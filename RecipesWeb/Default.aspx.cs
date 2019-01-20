using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{

    Connections con = new Connections();
    public string Com_username
    {
        get
        {
            return (ViewState["Com_username"] == null) ? "" : (string)ViewState["Com_username"];
        }

        set
        {
            ViewState["Com_username"] = value;
        }
    }

    public string User
    {
        get
        {
            return (ViewState["User"] == null) ? "" : (string)ViewState["User"];
        }

        set
        {
            ViewState["User"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginCom"] != null && Session["LoginUser"] != null)
            {
                Com_username = Session["LoginCom"].ToString();
                User = Session["LoginUser"].ToString();

                DataTable dt = con.SelecthostProc(Com_username, "Ingredient_View_Select", null, null);
                Repeater_items.DataSource = dt;
                Repeater_items.DataBind();

                DataTable dtcountingred = con.SelecthostProc(Com_username, "Count_Ingred", null, null);
                if (dtcountingred.Rows.Count > 0)
                {
                    Label_ingred.Text = dtcountingred.Rows[0][0].ToString();
                }

                DataTable dtcountbatch = con.SelecthostProc(Com_username, "Count_Batch", null, null);
                if (dtcountingred.Rows.Count > 0)
                {
                    Label_batch.Text = dtcountbatch.Rows[0][0].ToString();
                }

                DataTable dtcountrecipe = con.SelecthostProc(Com_username, "Count_Recipe", null, null);
                if (dtcountingred.Rows.Count > 0)
                {
                    Label_recipe.Text = dtcountrecipe.Rows[0][0].ToString();
                }
            } 
        }
       
    }
}