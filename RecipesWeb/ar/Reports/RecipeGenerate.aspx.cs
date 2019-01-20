using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Reports_RecipeGenerate : System.Web.UI.Page
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

    string id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginCom"] != null && Session["LoginUser"] != null)
            {
                Com_username = Session["LoginCom"].ToString();
                User = Session["LoginUser"].ToString();

                if (Request.QueryString["id"] != null)
                {
                    id = Request.QueryString["id"];
                }
                try
                {
                    ReportDocument rptDoc = new ReportDocument();
                    RecipesDataSet ds = new RecipesDataSet();
                    DataTable dt = new DataTable();

                    dt.TableName = "Crystal Report Example";
                    dt = getAllRecords(id);

                    DataView dv = dt.DefaultView;

                    dt = dv.ToTable();
                    ds.Tables["RecipeSheet"].Merge(dt);

                    rptDoc.Load(Server.MapPath("~/ar/Reports/RecipeSheet.rpt"));
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
    public DataTable getAllRecords(string id)
    {
        DataTable dt = new DataTable();
        try
        {          
            dt = con.SelecthostProc(Com_username, "Recipe_Union_Select", new string[] { "id" }, id);            
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