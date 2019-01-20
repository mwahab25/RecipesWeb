using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RepMaterials : System.Web.UI.Page
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
                    DataTable dtcat = con.SelecthostProc(Com_username, "IngredCat_Select", null, null);
                    Matcosts_cat.DataSource = dtcat;
                    Matcosts_cat.DataTextField = "IngredientCat_NameEn";
                    Matcosts_cat.DataValueField = "IngredientCat_ID";
                    Matcosts_cat.DataBind();
                    Matcosts_cat.Items.Insert(0, "");

                    Matpriceshis_cat.DataSource = dtcat;
                    Matpriceshis_cat.DataTextField = "IngredientCat_NameEn";
                    Matpriceshis_cat.DataValueField = "IngredientCat_ID";
                    Matpriceshis_cat.DataBind();
                    Matpriceshis_cat.Items.Insert(0, "");


                    BindData();
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
        table.Columns.Add("itemcat", typeof(string));
        table.Columns.Add("itemname", typeof(string));
        table.Columns.Add("itemunit", typeof(string));
        table.Columns.Add("itemprice", typeof(decimal));

        return table;
    }

    private void BindData()
    {
        try
        {
            DataTable dtbycat = con.SelecthostProc(Com_username, "Ingredient_View_Select", null, null);
            if (dtbycat.Rows.Count > 0)
            {
                DataTable dt = GetTable();
                foreach (DataRow dr_ in dtbycat.Rows)
                {
                    DataRow dr;
                    dr = dt.NewRow();
                    dr["itemcat"] = dr_["IngredientCat_NameEn"];
                    dr["itemname"] = dr_["Ingredient_NameEn"];
                    dr["itemunit"] = dr_["Unit_NameEn"];
                    dr["itemprice"] = dr_["Price"];

                    dt.Rows.Add(dr);

                }
                GridView_items.DataSource = (DataView)dt.DefaultView;
                GridView_items.DataBind();
            }

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-RepMaterials-BindData");
        }
    }
    protected void Matcosts_query_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable ds = new DataTable();
            ds = null;
            GridView_items.DataSource = ds;
            GridView_items.DataBind();

            if (Matcosts_bycat.Checked)
            {
                DataTable dtbycat = con.SelecthostProc(Com_username, "Ingredient_SelectByCat", new string[] { "cat" }, Matcosts_cat.SelectedValue);
                if (dtbycat.Rows.Count > 0)
                {
                    DataTable dt = GetTable();
                    foreach (DataRow dr_ in dtbycat.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["itemcat"] = dr_["IngredientCat_NameEn"];
                        dr["itemname"] = dr_["Ingredient_NameEn"];
                        dr["itemunit"] = dr_["Unit_NameEn"];
                        dr["itemprice"] = dr_["Price"];

                        dt.Rows.Add(dr);

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }
            }
            else if (Matcosts_byname.Checked)
            {
                DataTable dtbycat = con.SelecthostProc(Com_username, "Ingredient_SelectByname", new string[] { "name" }, Matcosts_itemname.Text);
                if (dtbycat.Rows.Count > 0)
                {
                    DataTable dt = GetTable();
                    foreach (DataRow dr_ in dtbycat.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["itemcat"] = dr_["IngredientCat_NameEn"];
                        dr["itemname"] = dr_["Ingredient_NameEn"];
                        dr["itemunit"] = dr_["Unit_NameEn"];
                        dr["itemprice"] = dr_["Price"];

                        dt.Rows.Add(dr);

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }
            }
            else if (Matcosts_all.Checked)
            {
                DataTable dtbycat = con.SelecthostProc(Com_username, "Ingredient_View_Select", null, null);
                if (dtbycat.Rows.Count > 0)
                {
                    DataTable dt = GetTable();
                    foreach (DataRow dr_ in dtbycat.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["itemcat"] = dr_["IngredientCat_NameEn"];
                        dr["itemname"] = dr_["Ingredient_NameEn"];
                        dr["itemunit"] = dr_["Unit_NameEn"];
                        dr["itemprice"] = dr_["Price"];

                        dt.Rows.Add(dr);

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-RepMaterials-Matcosts_query_Click");
        }
    }
    protected void Matcosts_print_Click(object sender, EventArgs e)
    {
        try
        {
            string type = "";
            string cat = "";
            string name = "";
            if (Matcosts_bycat.Checked)
            {
                type = "bycat";
                cat = Matcosts_cat.SelectedValue;
                Response.Redirect("~/Reports/MaterialGenerate.aspx?cat=" + cat + "&" + "type=" + type);
            }
            else if (Matcosts_byname.Checked)
            {
                type = "byname";
                name = Matcosts_itemname.Text;
                Response.Redirect("~/Reports/MaterialGenerate.aspx?name=" + name + "&" + "type=" + type);
            }
            else if (Matcosts_all.Checked)
            {
                type = "all";
                Response.Redirect("~/Reports/MaterialGenerate.aspx?type=" + type);
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-RepMaterials-Matcosts_print_Click");
        }
    }
    protected void Matcosts_bycat_CheckedChanged(object sender, EventArgs e)
    {
        Matcosts_cat.Enabled = true;
        Matcosts_itemname.Enabled = false;
    }
    protected void Matcosts_byname_CheckedChanged(object sender, EventArgs e)
    {
        Matcosts_cat.Enabled = false;
        Matcosts_itemname.Enabled = true;
    }
    protected void Matcosts_all_CheckedChanged(object sender, EventArgs e)
    {
        Matcosts_cat.Enabled = false;
        Matcosts_itemname.Enabled = false;
    }
    protected void GridView_items_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (Matcosts_bycat.Checked)
            {
                DataTable dtbycat = con.SelecthostProc(Com_username, "Ingredient_SelectByCat", new string[] { "cat" }, Matcosts_cat.SelectedValue);
                if (dtbycat.Rows.Count > 0)
                {
                    DataTable dt = GetTable();
                    foreach (DataRow dr_ in dtbycat.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["itemcat"] = dr_["IngredientCat_NameEn"];
                        dr["itemname"] = dr_["Ingredient_NameEn"];
                        dr["itemunit"] = dr_["Unit_NameEn"];
                        dr["itemprice"] = dr_["Price"];

                        dt.Rows.Add(dr);

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();

                    GridView_items.PageIndex = e.NewPageIndex;
                    GridView_items.DataBind();
                }
            }
            else if (Matcosts_byname.Checked)
            {
                DataTable dtbycat = con.SelecthostProc(Com_username, "Ingredient_SelectByname", new string[] { "name" }, Matcosts_itemname.Text);
                if (dtbycat.Rows.Count > 0)
                {
                    DataTable dt = GetTable();
                    foreach (DataRow dr_ in dtbycat.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["itemcat"] = dr_["IngredientCat_NameEn"];
                        dr["itemname"] = dr_["Ingredient_NameEn"];
                        dr["itemunit"] = dr_["Unit_NameEn"];
                        dr["itemprice"] = dr_["Price"];

                        dt.Rows.Add(dr);

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();

                    GridView_items.PageIndex = e.NewPageIndex;
                    GridView_items.DataBind();
                }
            }
            else if (Matcosts_all.Checked)
            {
                BindData();
                GridView_items.PageIndex = e.NewPageIndex;
                GridView_items.DataBind();
            }

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-RepMaterials-GridView_items_PageIndexChanging");
        }
    }

    static protected DataTable PricesGetTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("itemadddate", typeof(DateTime));
        table.Columns.Add("itemprice", typeof(decimal));

        return table;
    }
    protected void Matpriceshis_query_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable ds = new DataTable();
            ds = null;
            GridView_Matpriceshis_prices.DataSource = ds;
            GridView_Matpriceshis_prices.DataBind();


            DataTable dtbyid = con.SelecthostProc(Com_username, "IngredPrices_View_SelectByID", new string[] { "id" }, Matpriceshis_name.SelectedValue);
            if (dtbyid.Rows.Count > 0)
            {
                DataTable dt = PricesGetTable();
                foreach (DataRow dr_ in dtbyid.Rows)
                {
                    DataRow dr;
                    dr = dt.NewRow();
                    dr["itemadddate"] = dr_["Add_date"];
                    dr["itemprice"] = dr_["Price"];

                    dt.Rows.Add(dr);

                }
                GridView_Matpriceshis_prices.DataSource = (DataView)dt.DefaultView;
                GridView_Matpriceshis_prices.DataBind();
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-RepMaterials-Matpriceshis_query_Click");
        }
    }
    protected void Matpriceshis_print_Click(object sender, EventArgs e)
    {

    }
    protected void Matpriceshis_cat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dti = con.SelecthostProc(Com_username, "Ingredient_SelectByCat", new string[] { "cat" }, Matpriceshis_cat.SelectedValue);
            Matpriceshis_name.DataSource = dti;
            Matpriceshis_name.DataTextField = "Ingredient_NameEn";
            Matpriceshis_name.DataValueField = "Ingredient_ID";
            Matpriceshis_name.DataBind();
            Matpriceshis_name.Items.Insert(0, new ListItem("", "0"));
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-RepMaterials-Matpriceshis_cat_SelectedIndexChanged");
        }

    }
}