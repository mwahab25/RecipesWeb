using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;

public partial class Reports_MaterialGenerate : System.Web.UI.Page
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

    string type = "";
    string cat = "";
    string name = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginCom"] != null && Session["LoginUser"] != null)
            {
                Com_username = Session["LoginCom"].ToString();
                User = Session["LoginUser"].ToString();

                if (Request.QueryString["cat"] != null)
                {
                    cat = Request.QueryString["cat"];
                }
                if (Request.QueryString["name"] != null)
                {
                    name = Request.QueryString["name"];
                }

                if (Request.QueryString["type"] != null)
                {
                    type = Request.QueryString["type"];
                }
                try
                {
                    ReportDocument rptDoc = new ReportDocument();
                    RecipesDataSet ds = new RecipesDataSet();
                    DataTable dt = new DataTable();

                    dt.TableName = "Crystal Report Example";
                    dt = getAllRecords(cat, name, type);

                    DataView dv = dt.DefaultView;

                    dt = dv.ToTable();
                    ds.Tables["Items"].Merge(dt);

                    rptDoc.Load(Server.MapPath("~/ar/Reports/Materials.rpt"));
                    rptDoc.SetDataSource(ds);
                    CrystalReportViewer1.ReportSource = rptDoc;
                    CrystalReportViewer1.DataBind();

                    CrystalReportViewer1.DataBind();
                    rptDoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ExportedReport");

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString());
                }
            }
        }
    }
    public DataTable getAllRecords(string cat, string name,string type)
    {
        DataTable dt = new DataTable();
        try
        {
            if (type == "bycat")
            {
                dt = con.SelecthostProc(Com_username, "Ingredient_SelectByCat", new string[] { "cat"}, cat);
            }
            else if (type == "byname")
            {
                dt = con.SelecthostProc(Com_username, "Ingredient_SelectBynameAr", new string[] { "nameAr"}, name);
            }
            else if (type == "all")
            {
                dt = con.SelecthostProc(Com_username, "Ingredient_View_Select", null,null);
            }


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
        }
        return dt;
    }
}