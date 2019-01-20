using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class RepRecipes : System.Web.UI.Page
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

                DataTable dtpriv = con.SelecthostProc(Com_username, "User_Privilege_SelectByForm", new string[] { "formid", "name" }, 5, User);
                if (dtpriv.Rows.Count > 0)
                {
                    DataTable dtcat = con.SelecthostProc(Com_username, "RecipeCat_Select", null, null);
                    Reccosts_cat.DataSource = dtcat;
                    Reccosts_cat.DataTextField = "RecipeCat_NameEn";
                    Reccosts_cat.DataValueField = "RecipeCat_ID";
                    Reccosts_cat.DataBind();
                    Reccosts_cat.Items.Insert(0, "");
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            Page.ClientTarget = "uplevel";
    }

    static protected DataTable GetTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("reccat", typeof(string));
        table.Columns.Add("recname", typeof(string));
        table.Columns.Add("reccost", typeof(decimal));
        table.Columns.Add("recprice", typeof(decimal));
        table.Columns.Add("recmargin", typeof(decimal));
        table.Columns.Add("rectarget", typeof(decimal));
        table.Columns.Add("recvar", typeof(decimal));

        return table;
    }

    protected void Reccosts_query_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable ds = new DataTable();
            ds = null;
            GridView_items.DataSource = ds;
            GridView_items.DataBind();

            if (Reccosts_bycat.Checked)
            {
                DataTable dtbycat = con.SelecthostProc(Com_username, "Recipe_View_SelectByCat", new string[] { "cat" }, Reccosts_cat.SelectedValue);
                if (dtbycat.Rows.Count > 0)
                {
                    DataTable dt = GetTable();
                    foreach (DataRow dr_ in dtbycat.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["reccat"] = dr_["RecipeCat_NameEn"];
                        dr["recname"] = dr_["Recipe_NameEn"];
                        dr["reccost"] = dr_["Recipe_TotalCost"];
                        dr["recprice"] = dr_["Recipe_SellPrice"];
                        dr["recmargin"] = dr_["Recipe_CostMargin"];
                        dr["rectarget"] = dr_["Recipe_Target"];
                        dr["recvar"] = dr_["Recipe_Variance"];

                        dt.Rows.Add(dr);

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }
            }
            else if (Reccosts_byname.Checked)
            {
                DataTable dtbycat = con.SelecthostProc(Com_username, "Recipe_View_SelectByname", new string[] { "name" }, Reccosts_itemname.Text);
                if (dtbycat.Rows.Count > 0)
                {
                    DataTable dt = GetTable();
                    foreach (DataRow dr_ in dtbycat.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["reccat"] = dr_["RecipeCat_NameEn"];
                        dr["recname"] = dr_["Recipe_NameEn"];
                        dr["reccost"] = dr_["Recipe_TotalCost"];
                        dr["recprice"] = dr_["Recipe_SellPrice"];
                        dr["recmargin"] = dr_["Recipe_CostMargin"];
                        dr["rectarget"] = dr_["Recipe_Target"];
                        dr["recvar"] = dr_["Recipe_Variance"];

                        dt.Rows.Add(dr);

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }
            }
            else if (Reccosts_all.Checked)
            {
                DataTable dtbycat = con.SelecthostProc(Com_username, "Recipe_View_Select", null, null);
                if (dtbycat.Rows.Count > 0)
                {
                    DataTable dt = GetTable();
                    foreach (DataRow dr_ in dtbycat.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["reccat"] = dr_["RecipeCat_NameEn"];
                        dr["recname"] = dr_["Recipe_NameEn"];
                        dr["reccost"] = dr_["Recipe_TotalCost"];
                        dr["recprice"] = dr_["Recipe_SellPrice"];
                        dr["recmargin"] = dr_["Recipe_CostMargin"];
                        dr["rectarget"] = dr_["Recipe_Target"];
                        dr["recvar"] = dr_["Recipe_Variance"];

                        dt.Rows.Add(dr);

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-RepRecipes-Reccosts_query_Click");
        }
    }
    protected void Reccosts_print_Click(object sender, EventArgs e)
    {
        try
        {
            string type = "";
            string cat = "";
            string name = "";
            if (Reccosts_bycat.Checked)
            {
                type = "bycat";
                cat = Reccosts_cat.SelectedValue;
                Response.Redirect("~/Reports/RecipesGenerate.aspx?cat=" + cat + "&" + "type=" + type);
            }
            else if (Reccosts_byname.Checked)
            {
                type = "byname";
                name = Reccosts_itemname.Text;
                Response.Redirect("~/Reports/RecipesGenerate.aspx?name=" + name + "&" + "type=" + type);
            }
            else if (Reccosts_all.Checked)
            {
                type = "all";
                Response.Redirect("~/Reports/RecipesGenerate.aspx?type=" + type);
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-RepRecipes-Reccosts_print_Click");
        }
    }
    protected void Reccosts_bycat_CheckedChanged(object sender, EventArgs e)
    {
        Reccosts_cat.Enabled = true;
        Reccosts_itemname.Enabled = false;
    }
    protected void Reccosts_byname_CheckedChanged(object sender, EventArgs e)
    {
        Reccosts_cat.Enabled = false;
        Reccosts_itemname.Enabled = true;
    }
    protected void Reccosts_all_CheckedChanged(object sender, EventArgs e)
    {
        Reccosts_cat.Enabled = false;
        Reccosts_itemname.Enabled = false;
    }
}