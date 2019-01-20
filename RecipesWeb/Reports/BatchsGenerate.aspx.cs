using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;

public partial class Reports_BatchsGenerate : System.Web.UI.Page
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
    string name = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginCom"] != null && Session["LoginUser"] != null)
            {
                Com_username = Session["LoginCom"].ToString();
                User = Session["LoginUser"].ToString();

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
                    dt = getAllRecords(name, type);

                    DataView dv = dt.DefaultView;

                    dt = dv.ToTable();
                    ds.Tables["Batchs"].Merge(dt);

                    rptDoc.Load(Server.MapPath("~/Reports/Batchs.rpt"));
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
    public DataTable getAllRecords(string name, string type)
    {
        DataTable dt = new DataTable();
        try
        {
            if (type == "byname")
            {
                dt = con.SelecthostProc(Com_username, "Batch_View_SelectByname", new string[] { "name" }, name);
            }
            else if (type == "all")
            {
                dt = con.SelecthostProc(Com_username, "Batch_View_Select", null, null);
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