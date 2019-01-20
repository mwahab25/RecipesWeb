using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RepBatchs : System.Web.UI.Page
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
                    //
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
        table.Columns.Add("batchname", typeof(string));
        table.Columns.Add("batchunit", typeof(string));
        table.Columns.Add("batchcost", typeof(decimal));
        table.Columns.Add("batchprice", typeof(decimal));

        return table;
    }

    protected void Batcosts_query_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable ds = new DataTable();
            ds = null;
            GridView_items.DataSource = ds;
            GridView_items.DataBind();

            if (Batcosts_byname.Checked)
            {
                DataTable dtbycat = con.SelecthostProc(Com_username, "Batch_View_SelectByname", new string[] { "name" }, Batcosts_itemname.Text);
                if (dtbycat.Rows.Count > 0)
                {
                    DataTable dt = GetTable();
                    foreach (DataRow dr_ in dtbycat.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["batchname"] = dr_["Batch_NameEn"];
                        dr["batchunit"] = dr_["Unit_NameEn"];
                        dr["batchcost"] = dr_["Batch_Cost"];
                        dr["batchprice"] = dr_["Price"];

                        dt.Rows.Add(dr);

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }
            }
            else if (Batcosts_all.Checked)
            {
                DataTable dtbycat = con.SelecthostProc(Com_username, "Batch_View_Select", null, null);
                if (dtbycat.Rows.Count > 0)
                {
                    DataTable dt = GetTable();
                    foreach (DataRow dr_ in dtbycat.Rows)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["batchname"] = dr_["Batch_NameEn"];
                        dr["batchunit"] = dr_["Unit_NameEn"];
                        dr["batchcost"] = dr_["Batch_Cost"];
                        dr["batchprice"] = dr_["Price"];

                        dt.Rows.Add(dr);

                    }
                    GridView_items.DataSource = (DataView)dt.DefaultView;
                    GridView_items.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-RepBatchs-Batcosts_query_Click");
        }
    }
    protected void Batcosts_print_Click(object sender, EventArgs e)
    {
        try
        {
            string type = "";
            string name = "";

            if (Batcosts_byname.Checked)
            {
                type = "byname";
                name = Batcosts_itemname.Text;
                Response.Redirect("~/Reports/BatchsGenerate.aspx?name=" + name + "&" + "type=" + type);
            }
            else if (Batcosts_all.Checked)
            {
                type = "all";
                Response.Redirect("~/Reports/BatchsGenerate.aspx?type=" + type);
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-RepBatchs-Batcosts_print_Click");
        }
    }

    protected void Batcosts_byname_CheckedChanged(object sender, EventArgs e)
    {
        Batcosts_itemname.Enabled = true;
    }
    protected void Batcosts_all_CheckedChanged(object sender, EventArgs e)
    {
        Batcosts_itemname.Enabled = false;
    }
}