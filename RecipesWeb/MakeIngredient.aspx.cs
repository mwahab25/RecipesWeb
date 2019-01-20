using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MakeIngredient : System.Web.UI.Page
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

                DataTable dtpriv = con.SelecthostProc(Com_username, "User_Privilege_SelectByForm", new string[] { "formid", "name" }, 3, User);
                if (dtpriv.Rows.Count > 0)
                {
                    string s = NextBatchID().ToString();
                    batch_no.Text = s;

                    DataTable dtu = con.SelecthostProc(Com_username, "Unit_Select", null, null);
                    batch_unit.DataSource = dtu;
                    batch_unit.DataTextField = "Unit_NameEn";
                    batch_unit.DataValueField = "Unit_ID";
                    batch_unit.DataBind();
                    batch_unit.Items.Insert(0, "");

                    DataTable dtcat = con.SelecthostProc(Com_username, "IngredCat_Select", null, null);
                    batch_batchcat.DataSource = dtcat;
                    batch_batchcat.DataTextField = "IngredientCat_NameEn";
                    batch_batchcat.DataValueField = "IngredientCat_ID";
                    batch_batchcat.DataBind();
                    batch_batchcat.Items.Insert(0, new ListItem("", "0"));

                    ViewState["Curtbl"] = GetTable();

                    BindBatchData();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }

    protected int NextBatchID()
    {
        int BatchIDNew = 0;
        int BatchIDLast = 0;
        try
        {
            DataTable dt = con.SelecthostProc(Com_username, "Batch_SelectTopID", null, null);
            if (dt.Rows.Count > 0)
            {
                BatchIDLast = Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
                BatchIDLast = 0;
            }

            BatchIDNew = BatchIDLast + 1;
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-NextBatchID");
        }
        return BatchIDNew;
    }
    protected void batch_new_Click(object sender, EventArgs e)
    {
        try
        {
            batch_save.Visible = true;
            batch_update.Visible = false;
            batch_delete.Visible = false;

            string s = NextBatchID().ToString();
            batch_no.Text = s;

            NotifyMsg("", "none", "");

            batch_batchcat.SelectedIndex = 0;
            batch_ingredname.SelectedIndex = 0;
            batch_ingredqty.Text = "";

            batch_unit.SelectedIndex = 0;
            batch_price.Text = "";
            batch_totalcost.Text = "";
            batch_name.Text = "";
            batch_nameAr.Text = "";

            ViewState["Curtbl"] = GetTable();
            DataTable ds = new DataTable();
            ds = null;
            GridView_items.DataSource = ds;
            GridView_items.DataBind();

            BindBatchData();

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-batch_new_Click");
        }
    }
    protected void batch_save_Click(object sender, EventArgs e)
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
                SqlCommand Run1 = new SqlCommand("Batch_Save", Cn, tr);
                Run1.CommandType = CommandType.StoredProcedure;

                Run1.Parameters.AddWithValue("@id", batch_no.Text);
                Run1.Parameters.AddWithValue("@nameEn", batch_name.Text);
                Run1.Parameters.AddWithValue("@nameAr", batch_nameAr.Text);
                Run1.Parameters.AddWithValue("@price", batch_price.Text);
                Run1.Parameters.AddWithValue("@cost", batch_totalcost.Text);
                Run1.Parameters.AddWithValue("@unitid", batch_unit.Text);


                Run1.ExecuteNonQuery();

                decimal sum = 0;
                foreach (GridViewRow R in GridView_items.Rows)
                {
                    SqlCommand Run2 = new SqlCommand("BatchIngredient_Save", Cn, tr);
                    Run2.CommandType = CommandType.StoredProcedure;

                    Run2.Parameters.AddWithValue("@id", batch_no.Text);
                    Run2.Parameters.AddWithValue("@ingredid", R.Cells[0].Text);
                    Run2.Parameters.AddWithValue("@qty", R.Cells[4].Text);

                    sum += Convert.ToDecimal(R.Cells[5].Text);

                    Run2.ExecuteNonQuery();

                }

               

                tr.Commit();
                BindBatchData();
                NotifyMsg("Batch Saved Successfully", "block", "alert alert-success");
                Cn.Close();

            }
            else
            {
                NotifyMsg("Batch not have items!", "block", "alert");
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-batch_save_Click");
            NotifyMsg("An Error occured!", "block", "alert");
            tr.Rollback();
        }
    }
    protected void batch_update_Click(object sender, EventArgs e)
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
                SqlCommand Run1 = new SqlCommand("Batch_Update", Cn, tr);
                Run1.CommandType = CommandType.StoredProcedure;
                Run1.Parameters.AddWithValue("@id", batch_no.Text);
                Run1.Parameters.AddWithValue("@nameEn", batch_name.Text);
                Run1.Parameters.AddWithValue("@nameAr", batch_nameAr.Text);
                Run1.Parameters.AddWithValue("@price", batch_price.Text);
                Run1.Parameters.AddWithValue("@cost", batch_totalcost.Text);
                Run1.Parameters.AddWithValue("@unitid", batch_unit.Text);
                Run1.ExecuteNonQuery();


                SqlCommand Run3 = new SqlCommand("BatchIngredient_Delete", Cn, tr);
                Run3.CommandType = CommandType.StoredProcedure;
                Run3.Parameters.AddWithValue("@id", batch_no.Text);
                Run3.ExecuteNonQuery();

                decimal sum = 0;
                foreach (GridViewRow R in GridView_items.Rows)
                {
                    SqlCommand Run2 = new SqlCommand("BatchIngredient_Save", Cn, tr);
                    Run2.CommandType = CommandType.StoredProcedure;

                    Run2.Parameters.AddWithValue("@id", batch_no.Text);
                    Run2.Parameters.AddWithValue("@ingredid", R.Cells[0].Text);
                    Run2.Parameters.AddWithValue("@qty", R.Cells[4].Text);

                    sum += Convert.ToDecimal(R.Cells[5].Text);

                    Run2.ExecuteNonQuery();

                }
                tr.Commit();
                BindBatchData();
                NotifyMsg("Batch Updated Successfully", "block", "alert alert-success");
                Cn.Close();
            }
            else
            {
                NotifyMsg("Batch not have items!", "block", "alert");
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-batch_update_Click");
            NotifyMsg("An Error occured!", "block", "alert");
            tr.Rollback();
        }
    }
    protected void batch_delete_Click(object sender, EventArgs e)
    {
        
        try
        {
            if (IsUpdate == true)
            {
                con.ExcutehostProc(Com_username, "Batch_Delete", new string[] { "id" }, id);
                //NotifyMsg("Deleted Successfully", "block", "alert alert-success");

                BindBatchData();
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-batch_delete_Click");
        }
    }
    protected void batch_print_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Reports/BatchGenerate.aspx?id=" + batch_no.Text);
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-batch_print_Click");
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
    protected void batch_additem_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dsi = con.SelecthostProc(Com_username, "Ingredient_View_SelectByID", new string[] { "id" }, batch_ingredname.SelectedValue);
            if (dsi.Rows.Count > 0)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];

                DataRow dr;
                dr = dt.NewRow();

                dr["itemid"] = Convert.ToInt32(batch_ingredname.SelectedValue);
                dr["itemname"] = batch_ingredname.SelectedItem.Text;
                dr["itemunit"] = dsi.Rows[0][6].ToString();
                dr["itemprice"] = dsi.Rows[0][4].ToString();
                dr["itemqty"] = Convert.ToInt32(batch_ingredqty.Text);
                dr["total"] = Convert.ToInt32(batch_ingredqty.Text) * Convert.ToDecimal(dsi.Rows[0][4].ToString());

                dt.Rows.Add(dr);
                ViewState["Curtbl"] = dt;

                // GridView_items.Columns[0].Visible = true;
                GridView_items.DataSource = (DataView)dt.DefaultView;
                GridView_items.DataBind();
                //GridView_items.Columns[0].Visible = false;

                decimal sum = 0;
                foreach (GridViewRow R in GridView_items.Rows)
                {
                    sum += Convert.ToDecimal(R.Cells[5].Text);
                }

                batch_totalcost.Text = sum.ToString();
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-batch_additem_Click");
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

                    decimal sum = 0;
                    foreach (GridViewRow R in GridView_items.Rows)
                    {
                        Label t = (Label)R.Cells[5].FindControl("Label5");
                        sum += Convert.ToDecimal(t.Text);
                    }

                    batch_totalcost.Text = sum.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-GridView_items_RowDeleting");
        }
    }
    protected void batch_batchcat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dti = con.SelecthostProc(Com_username, "Ingredient_SelectByCat", new string[] { "cat" }, batch_batchcat.SelectedValue);
            batch_ingredname.DataSource = dti;
            batch_ingredname.DataTextField = "Ingredient_NameEn";
            batch_ingredname.DataValueField = "Ingredient_ID";
            batch_ingredname.DataBind();
            batch_ingredname.Items.Insert(0, new ListItem("", "0"));
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-GridView_items_RowDeleting");
        }
    }
    protected void batchname_search_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Curtbl"] = GetTable();
            DataTable ds = new DataTable();
            ds = null;
            GridView_items.DataSource = ds;
            GridView_items.DataBind();

            if (batch_name.Text != "")
            {
                DataTable dtbatch = con.SelecthostProc(Com_username, "Batch_SelectByName", new string[] { "name" }, batch_name.Text);
                if (dtbatch.Rows.Count > 0)
                {
                    batch_no.Text = dtbatch.Rows[0][0].ToString();
                    batch_name.Text = dtbatch.Rows[0][2].ToString();
                    batch_price.Text = dtbatch.Rows[0][4].ToString();
                    batch_totalcost.Text = dtbatch.Rows[0][3].ToString();
                    batch_unit.SelectedValue = dtbatch.Rows[0][5].ToString();
                   

                    DataTable dtbatchingred = con.SelecthostProc(Com_username, "BatchIngred_SelectByName", new string[] { "name" }, batch_name.Text);
                    if (dtbatchingred.Rows.Count > 0)
                    {
                        DataTable dt = GetTable();

                        foreach (DataRow dr_ in dtbatchingred.Rows)
                        {
                            DataRow dr;
                            dr = dt.NewRow();
                            dr["itemid"] = dr_["Ingredient_ID"];
                            dr["itemname"] = dr_["Ingredient_NameEn"];
                            dr["itemunit"] = dr_["Unit_NameEn"];
                            dr["itemprice"] = dr_["Price"];
                            dr["itemqty"] = dr_["Qty"];
                            dr["total"] = dr_["Total"];

                            dt.Rows.Add(dr);
                            ViewState["Curtbl"] = dt;

                        }
                        GridView_items.DataSource = (DataView)dt.DefaultView;
                        GridView_items.DataBind();
                    }

                    batch_save.Visible = false;
                    batch_update.Visible = true;
                    batch_delete.Visible = true;
                   
                }
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-batchname_search_Click");
        }
    }
    protected void batchnameAr_search_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Curtbl"] = GetTable();
            DataTable ds = new DataTable();
            ds = null;
            GridView_items.DataSource = ds;
            GridView_items.DataBind();

            if (batch_nameAr.Text != "")
            {
                DataTable dtbatch = con.SelecthostProc(Com_username, "Batch_SelectByNameAr", new string[] { "nameAr" }, batch_nameAr.Text);
                if (dtbatch.Rows.Count > 0)
                {
                    batch_no.Text = dtbatch.Rows[0][0].ToString();
                    batch_nameAr.Text = dtbatch.Rows[0][1].ToString();
                    batch_name.Text = dtbatch.Rows[0][2].ToString();
                    batch_price.Text = dtbatch.Rows[0][4].ToString();
                    batch_totalcost.Text = dtbatch.Rows[0][3].ToString();
                    batch_unit.SelectedValue = dtbatch.Rows[0][5].ToString();


                    DataTable dtbatchingred = con.SelecthostProc(Com_username, "BatchIngred_SelectByNameAr", new string[] { "nameAr" }, batch_nameAr.Text);
                    if (dtbatchingred.Rows.Count > 0)
                    {
                        DataTable dt = GetTable();

                        foreach (DataRow dr_ in dtbatchingred.Rows)
                        {
                            DataRow dr;
                            dr = dt.NewRow();
                            dr["itemid"] = dr_["Ingredient_ID"];
                            dr["itemname"] = dr_["Ingredient_NameEn"];
                            dr["itemunit"] = dr_["Unit_NameEn"];
                            dr["itemprice"] = dr_["Price"];
                            dr["itemqty"] = dr_["Qty"];
                            dr["total"] = dr_["Total"];

                            dt.Rows.Add(dr);
                            ViewState["Curtbl"] = dt;

                        }
                        GridView_items.DataSource = (DataView)dt.DefaultView;
                        GridView_items.DataBind();
                    }

                    batch_save.Visible = false;
                    batch_update.Visible = true;
                    batch_delete.Visible = true;

                }
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-batchnameAr_search_Click");
        }
    }

    private void BindBatchData()
    {
        try
        {
            DataTable dtu = con.SelecthostProc(Com_username, "Batch_View_Select", null, null);
            GridView_Batchs.DataSource = dtu;
            GridView_Batchs.DataBind();

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-BindBatchData");
        }
    }
    protected void GridView_Batchs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            BindBatchData();
            GridView_Batchs.PageIndex = e.NewPageIndex;
            GridView_Batchs.DataBind();
            NotifyMsg("", "none", "");
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-GridView_Batchs_PageIndexChanging");
        }
    }
    protected void GridView_Batchs_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ViewState["Curtbl"] = GetTable();
            DataTable ds = new DataTable();
            ds = null;
            GridView_items.DataSource = ds;
            GridView_items.DataBind();

            Label lbId = (Label)GridView_Batchs.Rows[e.NewEditIndex].FindControl("LblBatchID");
            id = int.Parse(lbId.Text);
            IsUpdate = true;

            DataTable dtbatch = con.SelecthostProc(Com_username, "Batch_SelectByID", new string[] { "id" }, id);
            if (dtbatch.Rows.Count > 0)
            {
                batch_no.Text = dtbatch.Rows[0][0].ToString();
                batch_nameAr.Text = dtbatch.Rows[0][1].ToString();
                batch_name.Text = dtbatch.Rows[0][2].ToString();
                batch_price.Text = dtbatch.Rows[0][4].ToString();
                batch_totalcost.Text = dtbatch.Rows[0][3].ToString();
                batch_unit.SelectedValue = dtbatch.Rows[0][5].ToString();


                DataTable dtbatchingred = con.SelecthostProc(Com_username, "BatchIngred_SelectByID", new string[] { "id" }, id);
                if (dtbatchingred.Rows.Count > 0)
                {
                    DataTable dt = GetTable();

                    foreach (DataRow dr_ in dtbatchingred.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["itemid"] = dr_["Ingredient_ID"];
                        dr["itemname"] = dr_["Ingredient_NameEn"];
                        dr["itemunit"] = dr_["Unit_NameEn"];
                        dr["itemprice"] = dr_["Price"];
                        dr["itemqty"] = dr_["Qty"];
                        dr["total"] = dr_["Total"];

                        dt.Rows.Add(dr);
                        ViewState["Curtbl"] = dt;

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }

                batch_save.Visible = false;
                batch_update.Visible = true;
                batch_delete.Visible = true;

            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-GridView_Batchs_RowEditing");
        }

    }
    protected void GridView_Batchs_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ViewState["Curtbl"] = GetTable();
            DataTable ds = new DataTable();
            ds = null;
            GridView_items.DataSource = ds;
            GridView_items.DataBind();

            Label lbId = (Label)GridView_Batchs.Rows[e.RowIndex].FindControl("LblBatchID");
            id = int.Parse(lbId.Text);
            IsUpdate = true;

            DataTable dtbatch = con.SelecthostProc(Com_username, "Batch_SelectByID", new string[] { "id" }, id);
            if (dtbatch.Rows.Count > 0)
            {
                batch_no.Text = dtbatch.Rows[0][0].ToString();
                batch_nameAr.Text = dtbatch.Rows[0][1].ToString();
                batch_name.Text = dtbatch.Rows[0][2].ToString();
                batch_price.Text = dtbatch.Rows[0][4].ToString();
                batch_totalcost.Text = dtbatch.Rows[0][3].ToString();
                batch_unit.SelectedValue = dtbatch.Rows[0][5].ToString();


                DataTable dtbatchingred = con.SelecthostProc(Com_username, "BatchIngred_SelectByID", new string[] { "id" }, id);
                if (dtbatchingred.Rows.Count > 0)
                {
                    DataTable dt = GetTable();

                    foreach (DataRow dr_ in dtbatchingred.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["itemid"] = dr_["Ingredient_ID"];
                        dr["itemname"] = dr_["Ingredient_NameEn"];
                        dr["itemunit"] = dr_["Unit_NameEn"];
                        dr["itemprice"] = dr_["Price"];
                        dr["itemqty"] = dr_["Qty"];
                        dr["total"] = dr_["Total"];

                        dt.Rows.Add(dr);
                        ViewState["Curtbl"] = dt;

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }

                batch_save.Visible = false;
                batch_update.Visible = true;
                batch_delete.Visible = true;

            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-GridView_Batchs_RowUpdating");
        }
    }
    protected void GridView_Batchs_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dtu = con.SelecthostProc(Com_username, "Batch_View_Select", null, null);

            if (dtu != null)
            {
                DataView dataView = new DataView(dtu);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                GridView_Batchs.DataSource = dataView;
                GridView_Batchs.DataBind();
            }

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-GridView_Batchs_Sorting");
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
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-GridView_items_RowEditing");
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

                    total = sum;

                    batch_totalcost.Text = total.ToString();

                    
                }
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-GridView_items_RowUpdating");
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
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-MakeIngredient-GridView_items_RowCancelingEdit");
        }
    }
}