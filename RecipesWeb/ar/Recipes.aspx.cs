using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ar_Recipes : System.Web.UI.Page
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
    protected void NotifyMsg(string msgtext, string disp, string classclass)
    {
        msg.Text = msgtext;
        msgmsg.Attributes.Add("class", classclass);
        msgmsg.Style.Add("display", disp);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginCom"] != null && Session["LoginUser"] != null)
            {
                Com_username = Session["LoginCom"].ToString();
                User = Session["LoginUser"].ToString();

                DataTable dtpriv = con.SelecthostProc(Com_username, "User_Privilege_SelectByForm", new string[] { "formid", "name" }, 4, User);
                if (dtpriv.Rows.Count > 0)
                {

                    string s = NextRecipeID().ToString();
                    recipe_no.Text = s;

                    DataTable dtcat = con.SelecthostProc(Com_username, "IngredCat_Select", null, null);
                    recipe_ingredcat.DataSource = dtcat;
                    recipe_ingredcat.DataTextField = "IngredientCat_NameAr";
                    recipe_ingredcat.DataValueField = "IngredientCat_ID";
                    recipe_ingredcat.DataBind();
                    recipe_ingredcat.Items.Insert(0, new ListItem("", "0"));

                    DataTable dti = con.SelecthostProc(Com_username, "Batch_Select", null, null);
                    recipe_batchname.DataSource = dti;
                    recipe_batchname.DataTextField = "Batch_NameAr";
                    recipe_batchname.DataValueField = "Batch_ID";
                    recipe_batchname.DataBind();
                    recipe_batchname.Items.Insert(0, new ListItem("", "0"));

                    ViewState["Curtbl"] = GetTable();
                    ViewState["BatchCurtbl"] = BatchGetTable();
                    BindCatData();

                    BindRecipeData();

                    BindRecipeHowToDo();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }

    private void BindCatData()
    {
        try
        {
            DataTable dtcat = con.SelecthostProc(Com_username, "RecipeCat_Select", null, null);

            GridView_cats.DataSource = dtcat;
            GridView_cats.DataBind();

            recipe_cat.DataSource = dtcat;
            recipe_cat.DataTextField = "RecipeCat_NameAr";
            recipe_cat.DataValueField = "RecipeCat_ID";
            recipe_cat.DataBind();

            recipe_cat.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-BindCatData");
        }
    }
    protected void cat_save_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtcatname = con.SelecthostProc(Com_username, "RecipeCat_SelectCatname", new string[] { "nameAr", "nameEn" }, cat_nameAr.Text, cat_name.Text);
            if (dtcatname.Rows.Count > 0)
            {
                con.ExcutehostProc(Com_username, "RecipeCat_Save", new string[] { "nameAr", "nameEn" }, cat_nameAr.Text, cat_name.Text);
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
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-cat_save_Click");
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
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_cats_PageIndexChanging");
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
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_cats_RowEditing");
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

            con.ExcutehostProc(Com_username, "RecipeCat_Update", new string[] { "id", "nameAr", "nameEn" }, CatID, CatNameAr, CatNameEn);

            GridView_cats.EditIndex = -1;
            BindCatData();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_cats_RowUpdating");
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
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_cats_RowCancelingEdit");
        }

    }
    protected void DeleteCat(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkRemove = (LinkButton)sender;
            con.ExcutehostProc(Com_username, "RecipeCat_Delete", new string[] { "id" }, lnkRemove.CommandArgument);
            BindCatData();
            NotifyMsg("", "none", "");
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-DeleteCat");
        }
    }

    protected int NextRecipeID()
    {
        int RecipeIDNew = 0;
        int RecipeIDLast = 0;
        try
        {
            DataTable dt = con.SelecthostProc(Com_username, "Recipe_SelectTopID", null, null);
            if (dt.Rows.Count > 0)
            {
                RecipeIDLast = Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
                RecipeIDLast = 0;
            }

            RecipeIDNew = RecipeIDLast + 1;
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-NextRecipeID");
        }
        return RecipeIDNew;
    }
    protected void recipe_new_Click(object sender, EventArgs e)
    {
        try
        {
            recipe_save.Visible = true;
            recipe_update.Visible = false;
            recipe_delete.Visible = false;

            string s = NextRecipeID().ToString();
            recipe_no.Text = s;

            NotifyMsg("", "none", "");

            recipe_cat.SelectedIndex = 0;
            recipe_name.Text = "";
            recipe_nameAr.Text = "";

            recipe_ingredcat.SelectedIndex = 0;
            recipe_ingredname.SelectedIndex = 0;
            recipe_ingredqty.Text = "";

            recipe_batchname.SelectedIndex = 0;
            recipe_batchqty.Text = "";

            recipe_totalcost.Text = "";
            recipe_sellprice.Text = "";
            recipe_costmargin.Text = "";
            recipe_target.Text = "";
            recipe_variance.Text = "";


            ViewState["Curtbl"] = GetTable();
            DataTable ds = new DataTable();
            ds = null;
            GridView_items.DataSource = ds;
            GridView_items.DataBind();

            ViewState["BatchCurtbl"] = BatchGetTable();
            DataTable dsbatch = new DataTable();
            dsbatch = null;
            GridView_batchitems.DataSource = dsbatch;
            GridView_batchitems.DataBind();

            BindRecipeData();

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-recipe_new_Click");
        }
    }
    protected void recipe_save_Click(object sender, EventArgs e)
    {
        SqlConnection Cn = new SqlConnection(con.CoString(Com_username));

        if (Cn.State != ConnectionState.Open)
        {
            Cn.Open();
        }

        SqlTransaction tr;
        tr = Cn.BeginTransaction();

        try
        {
            if (GridView_items.Rows.Count > 0)
            {
                SqlCommand Run1 = new SqlCommand("Recipe_Save", Cn, tr);
                Run1.CommandType = CommandType.StoredProcedure;

                Run1.Parameters.AddWithValue("@id", recipe_no.Text);
                Run1.Parameters.AddWithValue("@nameAr", recipe_nameAr.Text);
                Run1.Parameters.AddWithValue("@nameEn", recipe_name.Text);
                Run1.Parameters.AddWithValue("@catid", recipe_cat.SelectedValue);
                Run1.Parameters.AddWithValue("@totalcost", recipe_totalcost.Text);
                Run1.Parameters.AddWithValue("@sellprice", recipe_sellprice.Text);
                Run1.Parameters.AddWithValue("@costmargin", recipe_costmargin.Text);
                Run1.Parameters.AddWithValue("@target", recipe_target.Text);
                Run1.Parameters.AddWithValue("@variance", recipe_variance.Text);


                Run1.ExecuteNonQuery();

                //decimal sum = 0;
                foreach (GridViewRow R in GridView_items.Rows)
                {
                    SqlCommand Run2 = new SqlCommand("RecipeIngredient_Save", Cn, tr);
                    Run2.CommandType = CommandType.StoredProcedure;

                    Run2.Parameters.AddWithValue("@recipeid", recipe_no.Text);
                    Run2.Parameters.AddWithValue("@ingredid", R.Cells[0].Text);
                    Run2.Parameters.AddWithValue("@qty", R.Cells[4].Text);

                    //sum += Convert.ToDecimal(R.Cells[5].Text);

                    Run2.ExecuteNonQuery();

                }

                if (GridView_batchitems.Rows.Count > 0)
                {
                    foreach (GridViewRow R in GridView_batchitems.Rows)
                    {
                        SqlCommand Run3 = new SqlCommand("RecipeBatch_Save", Cn, tr);
                        Run3.CommandType = CommandType.StoredProcedure;

                        Run3.Parameters.AddWithValue("@recipeid", recipe_no.Text);
                        Run3.Parameters.AddWithValue("@batchid", R.Cells[0].Text);
                        Run3.Parameters.AddWithValue("@qty", R.Cells[4].Text);

                        Run3.ExecuteNonQuery();

                    }
                }


                tr.Commit();
                BindRecipeData();
                NotifyMsg("Recipe Saved Successfully", "block", "alert alert-success");
                Cn.Close();

            }
            else
            {
                NotifyMsg("Recipe not have items!", "block", "alert");
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-recipe_save_Click");
            tr.Rollback();
        }
    }
    protected void recipe_update_Click(object sender, EventArgs e)
    {
        SqlConnection Cn = new SqlConnection(con.CoString(Com_username));

        if (Cn.State != ConnectionState.Open)
        {
            Cn.Open();
        }

        SqlTransaction tr;
        tr = Cn.BeginTransaction();

        try
        {
            if (GridView_items.Rows.Count > 0)
            {
                SqlCommand Run1 = new SqlCommand("Recipe_Update", Cn, tr);
                Run1.CommandType = CommandType.StoredProcedure;

                Run1.Parameters.AddWithValue("@id", recipe_no.Text);
                Run1.Parameters.AddWithValue("@nameAr", recipe_nameAr.Text);
                Run1.Parameters.AddWithValue("@nameEn", recipe_name.Text);
                Run1.Parameters.AddWithValue("@catid", recipe_cat.SelectedValue);
                Run1.Parameters.AddWithValue("@totalcost", recipe_totalcost.Text);
                Run1.Parameters.AddWithValue("@sellprice", recipe_sellprice.Text);
                Run1.Parameters.AddWithValue("@costmargin", recipe_costmargin.Text);
                Run1.Parameters.AddWithValue("@target", recipe_target.Text);
                Run1.Parameters.AddWithValue("@variance", recipe_variance.Text);
                Run1.ExecuteNonQuery();

                SqlCommand Run4 = new SqlCommand("RecipeIngredient_Delete", Cn, tr);
                Run4.CommandType = CommandType.StoredProcedure;
                Run4.Parameters.AddWithValue("@recipeid", recipe_no.Text);
                Run4.ExecuteNonQuery();

                foreach (GridViewRow R in GridView_items.Rows)
                {
                    SqlCommand Run2 = new SqlCommand("RecipeIngredient_Save", Cn, tr);
                    Run2.CommandType = CommandType.StoredProcedure;
                    Run2.Parameters.AddWithValue("@recipeid", recipe_no.Text);
                    Run2.Parameters.AddWithValue("@ingredid", R.Cells[0].Text);
                    Run2.Parameters.AddWithValue("@qty", R.Cells[4].Text);
                    Run2.ExecuteNonQuery();

                }

                if (GridView_batchitems.Rows.Count > 0)
                {
                    SqlCommand Run5 = new SqlCommand("RecipeBatch_Delete", Cn, tr);
                    Run5.CommandType = CommandType.StoredProcedure;
                    Run5.Parameters.AddWithValue("@recipeid", recipe_no.Text);
                    Run5.ExecuteNonQuery();

                    foreach (GridViewRow R in GridView_batchitems.Rows)
                    {
                        SqlCommand Run3 = new SqlCommand("RecipeBatch_Save", Cn, tr);
                        Run3.CommandType = CommandType.StoredProcedure;
                        Run3.Parameters.AddWithValue("@recipeid", recipe_no.Text);
                        Run3.Parameters.AddWithValue("@batchid", R.Cells[0].Text);
                        Run3.Parameters.AddWithValue("@qty", R.Cells[4].Text);
                        Run3.ExecuteNonQuery();
                    }
                }

                tr.Commit();
                BindRecipeData();
                NotifyMsg("Recipe Saved Successfully", "block", "alert alert-success");
                Cn.Close();

            }
            else
            {
                NotifyMsg("Recipe not have items!", "block", "alert");
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-recipe_update_Click");
            tr.Rollback();
        }
    }
    protected void recipe_delete_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsUpdate == true)
            {
                con.ExcutehostProc(Com_username, "Recipe_Delete", new string[] { "id" }, id);
                NotifyMsg("Deleted Successfully", "block", "alert alert-success");

                BindRecipeData();
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-recipe_delete_Click");
        }
    }
    protected void recipe_print_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/ar/Reports/RecipeGenerate.aspx?id=" + recipe_no.Text);
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-recipe_print_Click");
        }
    }

    static protected DataTable GetTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("itemid", typeof(int));
        table.Columns.Add("itemname", typeof(string));
        table.Columns.Add("itemunit", typeof(string));
        table.Columns.Add("itemprice", typeof(decimal));
        table.Columns.Add("itemqty", typeof(int));
        table.Columns.Add("total", typeof(decimal));

        return table;
    }
    protected void recipe_additem_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dsi = con.SelecthostProc(Com_username, "Ingredient_View_SelectByID", new string[] { "id" }, recipe_ingredname.SelectedValue);
            if (dsi.Rows.Count > 0)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];

                DataRow dr;
                dr = dt.NewRow();

                dr["itemid"] = Convert.ToInt32(recipe_ingredname.SelectedValue);
                dr["itemname"] = recipe_ingredname.SelectedItem.Text;
                dr["itemunit"] = dsi.Rows[0][6].ToString();
                dr["itemprice"] = dsi.Rows[0][4].ToString();
                dr["itemqty"] = Convert.ToInt32(recipe_ingredqty.Text);
                dr["total"] = Convert.ToInt32(recipe_ingredqty.Text) * Convert.ToDecimal(dsi.Rows[0][4].ToString());

                dt.Rows.Add(dr);
                ViewState["Curtbl"] = dt;

                //GridView_items.Columns[0].Visible = true;
                GridView_items.DataSource = (DataView)dt.DefaultView;
                GridView_items.DataBind();
                //GridView_items.Columns[0].Visible = false;

                decimal total = 0;
                decimal sum = 0;
                foreach (GridViewRow R in GridView_items.Rows)
                {
                    Label t = (Label)R.Cells[5].FindControl("Label5");
                    sum += Convert.ToDecimal(t.Text);
                }

                decimal sumbatch = 0;
                foreach (GridViewRow R in GridView_batchitems.Rows)
                {
                    Label t = (Label)R.Cells[5].FindControl("Label5");
                    sumbatch += Convert.ToDecimal(t.Text);
                }
                total = sum + sumbatch;
                recipe_totalcost.Text = total.ToString();
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-recipe_additem_Click");
        }
    }
    protected void recipe_batchcat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dti = con.SelecthostProc(Com_username, "Ingredient_SelectByCat", new string[] { "cat" }, recipe_ingredcat.SelectedValue);
            recipe_ingredname.DataSource = dti;
            recipe_ingredname.DataTextField = "Ingredient_NameAr";
            recipe_ingredname.DataValueField = "Ingredient_ID";
            recipe_ingredname.DataBind();
            recipe_ingredname.Items.Insert(0, new ListItem("", "0"));
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-recipe_batchcat_SelectedIndexChanged");
        }
    }

    protected void GridView_items_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Curtbl"] = dt;
                    GridView_items.DataSource = dt;
                    GridView_items.DataBind();

                    decimal total = 0;
                    decimal sum = 0;
                    foreach (GridViewRow R in GridView_items.Rows)
                    {
                        Label t = (Label)R.Cells[5].FindControl("Label5");
                        sum += Convert.ToDecimal(t.Text);
                    }

                    decimal sumbatch = 0;
                    foreach (GridViewRow R in GridView_batchitems.Rows)
                    {
                        Label t = (Label)R.Cells[5].FindControl("Label5");
                        sumbatch += Convert.ToDecimal(t.Text);
                    }

                    total = sum + sumbatch;
                    recipe_totalcost.Text = total.ToString();

                    recipe_sellprice.Text = "";
                    recipe_costmargin.Text = "";
                    recipe_target.Text = "";
                    recipe_variance.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_items_RowDeleting");
        }
    }
    protected void recipename_search_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-recipename_search_Click");
        }
    }
    protected void recipenameAr_search_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-recipenameAr_search_Click");
        }
    }

    static protected DataTable BatchGetTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("batchid", typeof(int));
        table.Columns.Add("batchname", typeof(string));
        table.Columns.Add("batchunit", typeof(string));
        table.Columns.Add("batchprice", typeof(decimal));
        table.Columns.Add("batchqty", typeof(int));
        table.Columns.Add("total", typeof(decimal));

        return table;
    }
    protected void recipe_batchadditem_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dsi = con.SelecthostProc(Com_username, "Batch_View_SelectByID", new string[] { "id" }, recipe_batchname.SelectedValue);
            if (dsi.Rows.Count > 0)
            {
                DataTable dt = (DataTable)ViewState["BatchCurtbl"];

                DataRow dr;
                dr = dt.NewRow();

                dr["batchid"] = Convert.ToInt32(recipe_batchname.SelectedValue);
                dr["batchname"] = recipe_batchname.SelectedItem.Text;
                dr["batchunit"] = dsi.Rows[0][4].ToString();
                dr["batchprice"] = dsi.Rows[0][2].ToString();
                dr["batchqty"] = Convert.ToInt32(recipe_batchqty.Text);
                dr["total"] = Convert.ToInt32(recipe_batchqty.Text) * Convert.ToDecimal(dsi.Rows[0][2].ToString());

                dt.Rows.Add(dr);
                ViewState["BatchCurtbl"] = dt;

                //GridView_batchitems.Columns[0].Visible = true;
                GridView_batchitems.DataSource = (DataView)dt.DefaultView;
                GridView_batchitems.DataBind();
                //GridView_batchitems.Columns[0].Visible = false;

                decimal total = 0;
                decimal sumbatch = 0;
                foreach (GridViewRow R in GridView_batchitems.Rows)
                {
                    Label t = (Label)R.Cells[5].FindControl("Label5");
                    sumbatch += Convert.ToDecimal(t.Text);
                }

                decimal sum = 0;
                foreach (GridViewRow R in GridView_items.Rows)
                {
                    Label t = (Label)R.Cells[5].FindControl("Label5");
                    sum += Convert.ToDecimal(t.Text);
                }

                total = sum + sumbatch;
                recipe_totalcost.Text = total.ToString();
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-recipe_batchadditem_Click");
        }
    }
    protected void GridView_batchitems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["BatchCurtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["BatchCurtbl"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["BatchCurtbl"] = dt;
                    GridView_batchitems.DataSource = dt;
                    GridView_batchitems.DataBind();

                    decimal total = 0;
                    decimal sumbatch = 0;
                    foreach (GridViewRow R in GridView_batchitems.Rows)
                    {
                        sumbatch += Convert.ToDecimal(R.Cells[5].Text);
                    }

                    decimal sum = 0;
                    foreach (GridViewRow R in GridView_items.Rows)
                    {
                        sum += Convert.ToDecimal(R.Cells[5].Text);
                    }

                    total = sum + sumbatch;
                    recipe_totalcost.Text = total.ToString();

                    recipe_sellprice.Text = "";
                    recipe_costmargin.Text = "";
                    recipe_target.Text = "";
                    recipe_variance.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_batchitems_RowDeleting");
        }
    }

    private void BindRecipeData()
    {
        try
        {
            DataTable dtu = con.SelecthostProc(Com_username, "Recipe_View_Select", null, null);
            GridView_Recipes.DataSource = dtu;
            GridView_Recipes.DataBind();

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-BindRecipeData");
        }
    }
    protected void GridView_Recipes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            BindRecipeData();
            GridView_Recipes.PageIndex = e.NewPageIndex;
            GridView_Recipes.DataBind();
            NotifyMsg("", "none", "");
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_Recipes_PageIndexChanging");
        }
    }
    protected void GridView_Recipes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ViewState["Curtbl"] = GetTable();
            DataTable ds = new DataTable();
            ds = null;
            GridView_items.DataSource = ds;
            GridView_items.DataBind();

            ViewState["BatchCurtbl"] = GetTable();
            DataTable dsbatch = new DataTable();
            dsbatch = null;
            GridView_batchitems.DataSource = dsbatch;
            GridView_batchitems.DataBind();

            Label lbId = (Label)GridView_Recipes.Rows[e.NewEditIndex].FindControl("LblRecipeID");
            id = int.Parse(lbId.Text);
            IsUpdate = true;

            DataTable dtrecipe = con.SelecthostProc(Com_username, "Recipe_SelectByID", new string[] { "id" }, id);
            if (dtrecipe.Rows.Count > 0)
            {
                recipe_no.Text = dtrecipe.Rows[0][0].ToString();
                recipe_cat.SelectedValue = dtrecipe.Rows[0][3].ToString();
                recipe_nameAr.Text = dtrecipe.Rows[0][1].ToString();
                recipe_name.Text = dtrecipe.Rows[0][2].ToString();
                recipe_sellprice.Text = dtrecipe.Rows[0][5].ToString();
                recipe_totalcost.Text = dtrecipe.Rows[0][4].ToString();
                recipe_costmargin.Text = dtrecipe.Rows[0][6].ToString();
                recipe_target.Text = dtrecipe.Rows[0][7].ToString();
                recipe_variance.Text = dtrecipe.Rows[0][8].ToString();


                DataTable dtrecipeingred = con.SelecthostProc(Com_username, "RecipeIngred_SelectByID", new string[] { "id" }, id);
                if (dtrecipeingred.Rows.Count > 0)
                {
                    DataTable dt = GetTable();

                    foreach (DataRow dr_ in dtrecipeingred.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["itemid"] = dr_["Ingredient_ID"];
                        dr["itemname"] = dr_["Ingredient_NameAr"];
                        dr["itemunit"] = dr_["Unit_NameAr"];
                        dr["itemprice"] = dr_["Price"];
                        dr["itemqty"] = dr_["Qty"];
                        dr["total"] = dr_["ExprPrice"];

                        dt.Rows.Add(dr);
                        ViewState["Curtbl"] = dt;

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }

                DataTable dtrecipebatch = con.SelecthostProc(Com_username, "RecipeBatch_SelectByID", new string[] { "id" }, id);
                if (dtrecipebatch.Rows.Count > 0)
                {
                    DataTable dt = BatchGetTable();

                    foreach (DataRow dr_ in dtrecipebatch.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["batchid"] = dr_["Batch_ID"];
                        dr["batchname"] = dr_["Batch_NameAr"];
                        dr["batchunit"] = dr_["Unit_NameAr"];
                        dr["batchprice"] = dr_["Price"];
                        dr["batchqty"] = dr_["Qty"];
                        dr["total"] = dr_["ExprPrice"];

                        dt.Rows.Add(dr);
                        ViewState["BatchCurtbl"] = dt;

                    }
                    GridView_batchitems.DataSource = (DataView)dt.DefaultView;
                    GridView_batchitems.DataBind();
                }

                recipe_save.Visible = false;
                recipe_update.Visible = true;
                recipe_delete.Visible = true;

            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_Recipes_RowEditing");
        }

    }
    protected void GridView_Recipes_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ViewState["Curtbl"] = GetTable();
            DataTable ds = new DataTable();
            ds = null;
            GridView_items.DataSource = ds;
            GridView_items.DataBind();

            ViewState["BatchCurtbl"] = GetTable();
            DataTable dsbatch = new DataTable();
            dsbatch = null;
            GridView_batchitems.DataSource = dsbatch;
            GridView_batchitems.DataBind();

            Label lbId = (Label)GridView_Recipes.Rows[e.RowIndex].FindControl("LblRecipeID");
            id = int.Parse(lbId.Text);
            IsUpdate = true;

            DataTable dtrecipe = con.SelecthostProc(Com_username, "Recipe_SelectByID", new string[] { "id" }, id);
            if (dtrecipe.Rows.Count > 0)
            {
                recipe_no.Text = dtrecipe.Rows[0][0].ToString();
                recipe_cat.SelectedValue = dtrecipe.Rows[0][3].ToString();
                recipe_nameAr.Text = dtrecipe.Rows[0][1].ToString();
                recipe_name.Text = dtrecipe.Rows[0][2].ToString();
                recipe_sellprice.Text = dtrecipe.Rows[0][5].ToString();
                recipe_totalcost.Text = dtrecipe.Rows[0][4].ToString();
                recipe_costmargin.Text = dtrecipe.Rows[0][6].ToString();
                recipe_target.Text = dtrecipe.Rows[0][7].ToString();
                recipe_variance.Text = dtrecipe.Rows[0][8].ToString();


                DataTable dtrecipeingred = con.SelecthostProc(Com_username, "RecipeIngred_SelectByID", new string[] { "id" }, id);
                if (dtrecipeingred.Rows.Count > 0)
                {
                    DataTable dt = GetTable();

                    foreach (DataRow dr_ in dtrecipeingred.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["itemid"] = dr_["Ingredient_ID"];
                        dr["itemname"] = dr_["Ingredient_NameAr"];
                        dr["itemunit"] = dr_["Unit_NameAr"];
                        dr["itemprice"] = dr_["Price"];
                        dr["itemqty"] = dr_["Qty"];
                        dr["total"] = dr_["ExprPrice"];

                        dt.Rows.Add(dr);
                        ViewState["Curtbl"] = dt;

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }

                DataTable dtrecipebatch = con.SelecthostProc(Com_username, "RecipeBatch_SelectByID", new string[] { "id" }, id);
                if (dtrecipebatch.Rows.Count > 0)
                {
                    DataTable dt = BatchGetTable();

                    foreach (DataRow dr_ in dtrecipebatch.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["batchid"] = dr_["Batch_ID"];
                        dr["batchname"] = dr_["Batch_NameAr"];
                        dr["batchunit"] = dr_["Unit_NameAr"];
                        dr["batchprice"] = dr_["Price"];
                        dr["batchqty"] = dr_["Qty"];
                        dr["total"] = dr_["ExprPrice"];

                        dt.Rows.Add(dr);
                        ViewState["BatchCurtbl"] = dt;

                    }
                    GridView_batchitems.DataSource = (DataView)dt.DefaultView;
                    GridView_batchitems.DataBind();
                }

                recipe_save.Visible = false;
                recipe_update.Visible = true;
                recipe_delete.Visible = true;

            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_Recipes_RowUpdating");
        }
    }
    protected void GridView_Recipes_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dtu = con.SelecthostProc(Com_username, "Recipe_View_Select", null, null);

            if (dtu != null)
            {
                DataView dataView = new DataView(dtu);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                GridView_Recipes.DataSource = dataView;
                GridView_Recipes.DataBind();
            }

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_Recipes_Sorting");
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

    protected void recipe_sellprice_TextChanged(object sender, EventArgs e)
    {

        if (recipe_sellprice.Text != String.Empty && recipe_totalcost.Text != String.Empty)
        {
            decimal sell = Convert.ToDecimal(recipe_sellprice.Text);
            decimal total = Convert.ToDecimal(recipe_totalcost.Text);

            decimal costmargin = (total / sell) * 100;
            recipe_costmargin.Text = costmargin.ToString();
        }
    }
    protected void recipe_target_TextChanged(object sender, EventArgs e)
    {
        if (recipe_target.Text != String.Empty && recipe_costmargin.Text != String.Empty)
        {
            decimal target = Convert.ToDecimal(recipe_target.Text);
            decimal costmargin = Convert.ToDecimal(recipe_costmargin.Text);
            decimal variance = target - costmargin;
            recipe_variance.Text = variance.ToString();
        }
    }

    protected void GridView_items_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_items.EditIndex = e.NewEditIndex;
            DataTable dt = (DataTable)ViewState["Curtbl"];
            GridView_items.DataSource = (DataView)dt.DefaultView;
            GridView_items.DataBind();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_items_RowEditing");
        }
    }
    protected void GridView_items_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                //DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count >= 1)
                {
                    TextBox box1 = (TextBox)GridView_items.Rows[rowIndex].Cells[4].FindControl("TextBox1");
                    Label label1 = (Label)GridView_items.Rows[rowIndex].Cells[4].FindControl("Label6");


                    dt.Rows[rowIndex]["itemqty"] = box1.Text;
                    dt.Rows[rowIndex]["total"] = Convert.ToInt32(dt.Rows[rowIndex]["itemqty"]) * Convert.ToDecimal(dt.Rows[rowIndex]["itemprice"]);


                    GridView_items.EditIndex = -1;
                    ViewState["Curtbl"] = dt;
                    GridView_items.DataSource = dt;
                    GridView_items.DataBind();

                    decimal total = 0;
                    decimal sum = 0;
                    foreach (GridViewRow R in GridView_items.Rows)
                    {
                        Label t = (Label)R.Cells[5].FindControl("Label5");
                        sum += Convert.ToDecimal(t.Text);
                    }

                    decimal sumbatch = 0;
                    foreach (GridViewRow R in GridView_batchitems.Rows)
                    {
                        Label t = (Label)R.Cells[5].FindControl("Label5");
                        sumbatch += Convert.ToDecimal(t.Text);
                    }

                    total = sum + sumbatch;
                    recipe_totalcost.Text = total.ToString();

                    recipe_sellprice.Text = "";
                    recipe_costmargin.Text = "";
                    recipe_target.Text = "";
                    recipe_variance.Text = "";
                }
            }


            //DataTable dt = (DataTable)ViewState["Curtbl"];
            //GridView_items.DataSource = (DataView)dt.DefaultView;
            //GridView_items.DataBind();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_items_RowUpdating");
        }
    }
    protected void GridView_items_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        try
        {
            GridView_items.EditIndex = -1;
            DataTable dt = (DataTable)ViewState["Curtbl"];
            GridView_items.DataSource = (DataView)dt.DefaultView;
            GridView_items.DataBind();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_items_RowCancelingEdit");
        }
    }

    protected void GridView_batchitems_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_batchitems.EditIndex = e.NewEditIndex;
            DataTable dt = (DataTable)ViewState["BatchCurtbl"];
            GridView_batchitems.DataSource = (DataView)dt.DefaultView;
            GridView_batchitems.DataBind();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_batchitems_RowEditing");
        }
    }
    protected void GridView_batchitems_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            if (ViewState["BatchCurtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["BatchCurtbl"];
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count >= 1)
                {
                    TextBox box1 = (TextBox)GridView_batchitems.Rows[rowIndex].Cells[4].FindControl("TextBox1");
                    Label label1 = (Label)GridView_batchitems.Rows[rowIndex].Cells[4].FindControl("Label6");


                    dt.Rows[rowIndex]["batchqty"] = box1.Text;
                    dt.Rows[rowIndex]["total"] = Convert.ToInt32(dt.Rows[rowIndex]["batchqty"]) * Convert.ToDecimal(dt.Rows[rowIndex]["batchprice"]);


                    GridView_batchitems.EditIndex = -1;
                    ViewState["BatchCurtbl"] = dt;
                    GridView_batchitems.DataSource = dt;
                    GridView_batchitems.DataBind();

                    decimal total = 0;
                    decimal sum = 0;
                    foreach (GridViewRow R in GridView_items.Rows)
                    {
                        Label t = (Label)R.Cells[5].FindControl("Label5");
                        sum += Convert.ToDecimal(t.Text);
                    }

                    decimal sumbatch = 0;
                    foreach (GridViewRow R in GridView_batchitems.Rows)
                    {
                        Label t = (Label)R.Cells[5].FindControl("Label5");
                        sumbatch += Convert.ToDecimal(t.Text);
                    }

                    total = sum + sumbatch;
                    recipe_totalcost.Text = total.ToString();

                    recipe_sellprice.Text = "";
                    recipe_costmargin.Text = "";
                    recipe_target.Text = "";
                    recipe_variance.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_batchitems_RowUpdating");
        }
    }
    protected void GridView_batchitems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_batchitems.EditIndex = -1;
            DataTable dt = (DataTable)ViewState["BatchCurtbl"];
            GridView_batchitems.DataSource = (DataView)dt.DefaultView;
            GridView_batchitems.DataBind();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-GridView_batchitems_RowCancelingEdit");
        }
    }

    protected void btnText_Click(object sender, EventArgs e)
    {
        //txtReText.Text = hdText.Value;
    }
    private void BindRecipeHowToDo()
    {
        try
        {
            DataTable dtu = con.SelecthostProc(Com_username, "RecipeCat_Select", null, null);
            howtodo_reccat.DataSource = dtu;
            howtodo_reccat.DataTextField = "RecipeCat_NameAr";
            howtodo_reccat.DataValueField = "RecipeCat_ID";
            howtodo_reccat.DataBind();
            howtodo_reccat.Items.Insert(0, new ListItem("", "0"));

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-BindRecipeHowToDo");
        }
    }
    protected void howtodo_new_Click(object sender, EventArgs e)
    {
        BindRecipeHowToDo();
        howtodo_rec.Items.Clear();
        NotifyMsg("", "none", "");
        txtEditor.Text = "";
    }
    protected void howtodo_save_Click(object sender, EventArgs e)
    {
        try
        {
            con.ExcutehostProc(Com_username, "Recipe_UpdateHowtodo", new string[] { "id", "howtodo" }, howtodo_rec.SelectedValue, txtEditor.Text.Trim());
            NotifyMsg("Saved Successfully", "block", "alert alert-success");
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-howtodo_save_Click");
        }
    }
    protected void howtodo_reccat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtu = con.SelecthostProc(Com_username, "Recipe_View_SelectByCat", new string[] { "cat" }, howtodo_reccat.SelectedValue);
            if (dtu.Rows.Count > 0)
            {
                howtodo_rec.DataSource = dtu;
                howtodo_rec.DataTextField = "Recipe_NameAr";
                howtodo_rec.DataValueField = "Recipe_ID";
                howtodo_rec.DataBind();
                howtodo_rec.Items.Insert(0, new ListItem("", "0"));
            }

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-howtodo_reccat_SelectedIndexChanged");
        }
    }
    protected void howtodo_rec_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = con.SelecthostProc(Com_username, "Recipe_SelectByID", new string[] { "id" }, howtodo_rec.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                txtEditor.Text = dt.Rows[0][9].ToString();
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Recipes-howtodo_rec_SelectedIndexChanged");
        }
    }
}