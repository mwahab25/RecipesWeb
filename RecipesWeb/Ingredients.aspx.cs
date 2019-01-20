using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Ingredients : System.Web.UI.Page
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

    public bool IsUpdate
    {
        get
        {
            return (ViewState["IsUpdate"] == null) ? false : (bool)ViewState["IsUpdate"];
        }

        set
        {
            ViewState["IsUpdate"] = value;
        }
    }
    public int id
    {
        get
        {
            return (ViewState["id"] == null) ? 0 : (int)ViewState["id"];
        }

        set
        {
            ViewState["id"] = value;
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

                DataTable dtpriv = con.SelecthostProc(Com_username, "User_Privilege_SelectByForm", new string[] { "formid", "name" }, 2, User);
                if(dtpriv.Rows.Count >0)
                {
                    BindData();
                    BindCatData();
                    BindItemData();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }

                
            }
        }
    }

    protected void NotifyMsg(string msgtext, string disp, string classclass)
    {
        msg.Text = msgtext;
        msgmsg.Attributes.Add("class", classclass);
        msgmsg.Style.Add("display", disp);
    }

    private void BindData()
    {
        try
        {
            DataTable dtu = con.SelecthostProc(Com_username, "Unit_Select", null, null);
            GridView_units.DataSource = dtu;
            GridView_units.DataBind();


            item_unit.DataSource = dtu;
            item_unit.DataTextField = "Unit_NameEn";
            item_unit.DataValueField = "Unit_ID";
            item_unit.DataBind();

            item_unit.Items.Insert(0, "");

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-BindData");
        }
    }
    protected void unit_save_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtunitname = con.SelecthostProc(Com_username, "Unit_SelectUnitname", new string[] { "nameAr", "nameEn" }, unit_nameAr.Text, unit_name.Text);
            if (dtunitname.Rows[0][0].ToString() == "true")
            {
                con.ExcutehostProc(Com_username, "Unit_Save", new string[] { "nameAr", "nameEn" }, unit_nameAr.Text, unit_name.Text);
                unit_name.Text = "";
                unit_nameAr.Text = "";
                BindData();
                NotifyMsg("", "none", "");
            }
            else
            {
                NotifyMsg("Unit saved before", "block", "alert");
            }
        }
        catch (Exception ex)
        {
            NotifyMsg("Something error", "block", "alert alert-error");
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-unit_save_Click");
        }
    }
    protected void GridView_units_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            BindData();
            GridView_units.PageIndex = e.NewPageIndex;
            GridView_units.DataBind();
            NotifyMsg("", "none", "");
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_units_PageIndexChanging");
        }
    }
    protected void GridView_units_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            NotifyMsg("The unit name change in all Ingredients", "block", "alert");
            GridView_units.EditIndex = e.NewEditIndex;
            BindData();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_units_RowEditing");
        }
    }
    protected void GridView_units_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            NotifyMsg("", "none", "");
            string UnitID = ((Label)GridView_units.Rows[e.RowIndex].FindControl("lblUnitID")).Text;
            string UnitNameAr = ((TextBox)GridView_units.Rows[e.RowIndex].FindControl("txtUnitNameAr")).Text;
            string UnitNameEn = ((TextBox)GridView_units.Rows[e.RowIndex].FindControl("txtUnitName")).Text;

            con.ExcutehostProc(Com_username, "Unit_Update", new string[] { "id", "nameAr", "nameEn" }, UnitID, UnitNameAr, UnitNameEn);

            GridView_units.EditIndex = -1;
            BindData();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_units_RowUpdating");
        }
    }
    protected void GridView_units_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            NotifyMsg("", "none", "");
            GridView_units.EditIndex = -1;
            BindData();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_units_RowCancelingEdit");
        }

    }
    protected void DeleteUnit(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkRemove = (LinkButton)sender;
            con.ExcutehostProc(Com_username, "Unit_Delete", new string[] { "id" }, lnkRemove.CommandArgument);

            BindData();
            NotifyMsg("", "none", "");
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-DeleteUnit");
        }
    }

    private void BindCatData()
    {
        try
        {
            DataTable dtcat = con.SelecthostProc(Com_username, "IngredCat_Select", null, null);

            GridView_cats.DataSource = dtcat;
            GridView_cats.DataBind();

            item_cat.DataSource = dtcat;
            item_cat.DataTextField = "IngredientCat_NameEn";
            item_cat.DataValueField = "IngredientCat_ID";
            item_cat.DataBind();

            item_cat.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-BindCatData");
        }
    }
    protected void cat_save_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtcatname = con.SelecthostProc(Com_username, "IngredCat_SelectCatname", new string[] { "nameAr", "nameEn" }, cat_nameAr.Text, cat_name.Text);
            if (dtcatname.Rows[0][0].ToString() == "true")
            {
                con.ExcutehostProc(Com_username, "IngredCat_Save", new string[] { "nameAr", "nameEn" }, cat_nameAr.Text,cat_name.Text);
                cat_name.Text = "";
                cat_nameAr.Text = "";
                BindCatData();
                NotifyMsg("", "none", "");
            }
            else
            {
                NotifyMsg("Category saved before", "block", "alert");
            }
        }
        catch (Exception ex)
        {
            NotifyMsg("Something error", "block", "alert alert-error");
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-cat_save_Click");
        }
    }
    protected void GridView_cats_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            BindCatData();
            GridView_cats.PageIndex = e.NewPageIndex;
            GridView_cats.DataBind();
            NotifyMsg("", "none", "");
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_cats_PageIndexChanging");
        }
    }
    protected void GridView_cats_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            NotifyMsg("Category name change in all Ingredients", "block", "alert");
            GridView_cats.EditIndex = e.NewEditIndex;
            BindCatData();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_cats_RowEditing");
        }
    }
    protected void GridView_cats_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            NotifyMsg("", "none", "");
            string CatID = ((Label)GridView_cats.Rows[e.RowIndex].FindControl("lblCatID")).Text;
            string CatNameAr = ((TextBox)GridView_cats.Rows[e.RowIndex].FindControl("txtCatNameAr")).Text;
            string CatNameEn = ((TextBox)GridView_cats.Rows[e.RowIndex].FindControl("txtCatName")).Text;

            con.ExcutehostProc(Com_username, "IngredCat_Update", new string[] { "id", "nameAr", "nameEn" }, CatID, CatNameAr, CatNameEn);

            GridView_cats.EditIndex = -1;
            BindCatData();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_cats_RowUpdating");
        }
    }
    protected void GridView_cats_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            NotifyMsg("", "none", "");
            GridView_cats.EditIndex = -1;
            BindCatData();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_cats_RowCancelingEdit");
        }

    }
    protected void DeleteCat(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkRemove = (LinkButton)sender;
            con.ExcutehostProc(Com_username, "IngredCat_Delete", new string[] { "id" }, lnkRemove.CommandArgument);
            BindCatData();
            NotifyMsg("", "none", "");
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-DeleteCat");
        }
    }

    private void BindItemData()
    {
        try
        {
            DataTable dtu = con.SelecthostProc(Com_username, "Ingredient_View_Select", null, null);
            GridView_items.DataSource = dtu;
            GridView_items.DataBind();

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-BindItemData");
        }
    }
    protected void item_new__Click(object sender, EventArgs e)
    {
        //item_save.Visible = true;
        item_save_.Visible = true;
        //item_update.Visible = false;
        item_update_.Visible = false;
        //item_delete.Visible = false;
        item_delete_.Visible = false;

        item_unit.SelectedIndex = 0;
        item_cat.SelectedIndex = 0;

        item_name.Text = "";
        item_nameAr.Text = "";
        item_price.Text = "";
               
        NotifyMsg("", "none", "");
        GridView_items.EditIndex = -1;
        BindItemData();
    }
    protected void item_save__Click(object sender, EventArgs e)
    {
        try
        {
            con.ExcutehostProc(Com_username, "Ingredient_Save", new string[] { "nameAr", "nameEn", "catid", "price", "unitid"}, item_nameAr.Text, item_name.Text, Convert.ToInt32(item_cat.SelectedValue), item_price.Text, Convert.ToInt32(item_unit.SelectedValue));
            
            NotifyMsg("Saved Successfully", "block", "alert alert-success");
            BindItemData();
            item_name.Text = "";
            item_nameAr.Text = "";
            item_price.Text = "";

            item_unit.SelectedIndex = 0;
            item_cat.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            NotifyMsg("Something error", "block", "alert alert-error");
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-item_save__Click");
        }
    }
    protected void item_update__Click(object sender, EventArgs e)
    {
        try
        {
            if (IsUpdate == true)
            {
                con.ExcutehostProc(Com_username, "Ingredient_Update", new string[] { "code","nameAr", "nameEn", "catid", "price", "unitid" },id, item_nameAr.Text,item_name.Text, Convert.ToInt32(item_cat.SelectedValue), item_price.Text, Convert.ToInt32(item_unit.SelectedValue));
               

                con.ExcutehostProc(Com_username, "IngredientPrices_Insert", new string[] { "code", "price", "user" }, id,item_price.Text, User);

                NotifyMsg("Updated Successfully/Prices Updated Successfully", "block", "alert alert-success");
                BindItemData();
            }
        }
        catch (Exception ex)
        {
            NotifyMsg("Something error", "block", "alert alert-error");
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-item_update__Click");
        }
    }
    protected void item_delete__Click(object sender, EventArgs e)
    {
        try
        {
            if (IsUpdate == true)
            {
                con.ExcutehostProc(Com_username, "Ingredient_Delete", new string[] { "code" }, id);
                //NotifyMsg("Deleted Successfully", "block", "alert alert-success");

                BindItemData();
            }

        }
        catch (Exception ex)
        {
            NotifyMsg("Something error", "block", "alert alert-error");
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-item_delete__Click");
        }
    }
    protected void GridView_items_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            BindItemData();
            GridView_items.PageIndex = e.NewPageIndex;
            GridView_items.DataBind();
            NotifyMsg("", "none", "");
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_items_PageIndexChanging");
        }
    }
    protected void GridView_items_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            item_save_.Visible = false;
            item_update_.Visible = true;
            item_delete_.Visible = true;

            Label lbId = (Label)GridView_items.Rows[e.NewEditIndex].FindControl("LblIngredID");
            id = int.Parse(lbId.Text);
            IsUpdate = true;

            DataTable dtu = con.SelecthostProc(Com_username, "Ingredient_SelectByID", new string[] { "code" }, id);

            item_nameAr.Text = dtu.Rows[0][1].ToString();
            item_name.Text = dtu.Rows[0][2].ToString();
            item_price.Text = dtu.Rows[0][3].ToString();
            item_cat.SelectedValue= dtu.Rows[0][5].ToString();
            item_unit.SelectedValue = dtu.Rows[0][4].ToString();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_items_RowEditing");
        }

    }
    protected void GridView_items_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            item_save_.Visible = false;
            item_update_.Visible = true;
            item_delete_.Visible = true;

            Label lbId = (Label)GridView_items.Rows[e.RowIndex].FindControl("LblIngredID");
            id = int.Parse(lbId.Text);
            IsUpdate = true;

            DataTable dtu = con.SelecthostProc(Com_username, "Ingredient_SelectByID", new string[] { "code" }, id);

            item_nameAr.Text = dtu.Rows[0][1].ToString();
            item_name.Text = dtu.Rows[0][2].ToString();
            item_price.Text = dtu.Rows[0][3].ToString();
            item_cat.SelectedValue = dtu.Rows[0][5].ToString();
            item_unit.SelectedValue = dtu.Rows[0][4].ToString();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_items_RowUpdating");
        }
    }
    protected void GridView_items_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dtu = con.SelecthostProc(Com_username, "Ingredient_View_Select", null, null);

            if (dtu != null)
            {
                DataView dataView = new DataView(dtu);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                GridView_items.DataSource = dataView;
                GridView_items.DataBind();
            }

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-GridView_items_Sorting");
        }
    }
    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "DESC"; }
        set { ViewState["SortDirection"] = value; }
    }
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;

            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }

        return GridViewSortDirection;
    }

    protected void itemcat_search_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = con.SelecthostProc(Com_username, "Ingredient_SelectByCat", new string[] { "cat" }, item_cat.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                NotifyMsg("", "none", "");

                GridView_items.DataSource = dt;
                GridView_items.DataBind();

                item_unit.SelectedIndex = 0;
                //item_cat.SelectedIndex = 0;

                item_name.Text = "";
                item_price.Text = "";

            }
            else
            {
                NotifyMsg("No Data found", "block", "alert alert-error");
            }

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-itemcat_search_Click");
        }
    }

    protected void itemname_search_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = con.SelecthostProc(Com_username, "Ingredient_SelectByname", new string[] { "name " }, item_name.Text);
            if (dt.Rows.Count > 0)
            {
                NotifyMsg("", "none", "");

                GridView_items.DataSource = dt;
                GridView_items.DataBind();

                item_unit.SelectedIndex = 0;
                item_cat.SelectedIndex = 0;

                item_nameAr.Text = "";
                item_price.Text = "";

            }
            else
            {
                NotifyMsg("No Data found", "block", "alert alert-error");
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-itemname_search_Click");
        }
    }

    protected void itemnameAr_search_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = con.SelecthostProc(Com_username, "Ingredient_SelectBynameAr", new string[] { "nameAr " }, item_nameAr.Text);
            if (dt.Rows.Count > 0)
            {
                NotifyMsg("", "none", "");

                GridView_items.DataSource = dt;
                GridView_items.DataBind();

                item_unit.SelectedIndex = 0;
                item_cat.SelectedIndex = 0;

                item_name.Text = "";
                item_price.Text = "";

            }
            else
            {
                NotifyMsg("No Data found", "block", "alert alert-error");
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Item-itemnameAr_search_Click");
        }
    }


}