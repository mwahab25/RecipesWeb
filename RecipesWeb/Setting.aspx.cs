using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Setting : System.Web.UI.Page
{
    Connections con = new Connections();

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

                DataTable dtpriv = con.SelecthostProc(Com_username, "User_Privilege_SelectByForm", new string[] { "formid", "name" }, 1, User);
                if (dtpriv.Rows.Count > 0)
                {
                    BindUserData();

                    BindFormList();
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
    private void BindUserData()
    {
        try
        {
            GridView_users.DataSource = con.SelecthostProc(Com_username, "User_Select", null, null);
            GridView_users.DataBind();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Setting-BindUserData");
        }
    }
    private void BindFormList()
    {
        try
        {
            Rpt_forms.DataSource = con.SelecthostProc(Com_username, "Form_Select", null, null);
            Rpt_forms.DataBind();

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Setting-BindFormList");
        }
    }

    protected void user_new_Click(object sender, EventArgs e)
    {
        user_name.Enabled = true;
        user_name.Text = "";
        user_pass.Text = "";
        user_repass.Text = "";
        BindUserData();
        NotifyMsg("", "none", "");

        foreach (RepeaterItem r in Rpt_forms.Items)
        {
            CheckBox f = (CheckBox)r.FindControl("CheckBox1");
            f.Checked = false;
        }

        Radio_admin.Checked = false;
        Radio_person.Checked = false;

        Radio_ar.Checked = true;
        Radio_en.Checked = false;

        IsUpdate = false;

    }
    protected void user_save_Click(object sender, EventArgs e)
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
            //0 --> admin
            //1 -->sales person
            //2 -->purchase person
            int role = 0;
            if (Radio_admin.Checked)
            { role = 0; }
            else if (Radio_person.Checked)
            { role = 1; }


            string lang = "ar";
            if (Radio_ar.Checked)
            { lang = "ar"; }
            else if (Radio_en.Checked)
            { lang = "en"; }

            if (IsUpdate == false)
            {
                DataTable dtusername = con.SelecthostProc(Com_username, "User_SelectUsername", new string[] { "name" }, user_name.Text);
                if (dtusername.Rows[0][0].ToString() == "true")
                {
                    SqlCommand Run1 = new SqlCommand("User_Save", Cn, tr);
                    Run1.CommandType = CommandType.StoredProcedure;

                    Run1.Parameters.AddWithValue("@name", user_name.Text);
                    Run1.Parameters.AddWithValue("@pass", user_pass.Text);
                    Run1.Parameters.AddWithValue("@role", role);
                    Run1.Parameters.AddWithValue("@lang", lang);

                    Run1.ExecuteNonQuery();

                    //con.ExcutehostProc(Com_username, "User_Save", new string[] { "name", "pass", "role" }, user_name.Text, user_pass.Text, role);

                    foreach (RepeaterItem r in Rpt_forms.Items)
                    {
                        CheckBox f = (CheckBox)r.FindControl("CheckBox1");
                        if (f.Checked)
                        {
                            SqlCommand Run2 = new SqlCommand("User_Privlege_Save", Cn, tr);
                            Run2.CommandType = CommandType.StoredProcedure;

                            Run2.Parameters.AddWithValue("@username", user_name.Text);
                            Run2.Parameters.AddWithValue("@formid", Convert.ToInt32(f.ToolTip));

                            Run2.ExecuteNonQuery();
                            //con.ExcutehostProc(Com_username, "User_Privlege_Save", new string[] { "username", "formid" }, user_name.Text, Convert.ToInt32(f.ToolTip));
                        }
                    }

                    tr.Commit();
                    Cn.Close();
                    user_name.Text = "";
                    BindUserData();
                    NotifyMsg("", "none", "");

                }
                else
                {
                    NotifyMsg("إسم المستخدم مسجل من قبل", "block", "alert alert-error");
                }
            }
            else
            {
                SqlCommand Run1 = new SqlCommand("User_Update", Cn, tr);
                Run1.CommandType = CommandType.StoredProcedure;

                Run1.Parameters.AddWithValue("@id", id);

                Run1.Parameters.AddWithValue("@pass", user_pass.Text);
                Run1.Parameters.AddWithValue("@role", role);
                Run1.Parameters.AddWithValue("@lang", lang);

                Run1.ExecuteNonQuery();

                SqlCommand Run3 = new SqlCommand("User_Privlege_Delete", Cn, tr);
                Run3.CommandType = CommandType.StoredProcedure;

                Run3.Parameters.AddWithValue("@username", user_name.Text);
                Run3.ExecuteNonQuery();

                foreach (RepeaterItem r in Rpt_forms.Items)
                {
                    CheckBox f = (CheckBox)r.FindControl("CheckBox1");
                    if (f.Checked)
                    {
                        SqlCommand Run2 = new SqlCommand("User_Privlege_Save", Cn, tr);
                        Run2.CommandType = CommandType.StoredProcedure;

                        Run2.Parameters.AddWithValue("@username", user_name.Text);
                        Run2.Parameters.AddWithValue("@formid", Convert.ToInt32(f.ToolTip));

                        Run2.ExecuteNonQuery();
                    }
                }

                tr.Commit();
                Cn.Close();
                user_name.Text = "";
                BindUserData();
                NotifyMsg("", "none", "");
            }

        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Setting-user_save_Click");
            tr.Rollback();
        }
    }
    protected void GridView_users_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            BindUserData();
            GridView_users.PageIndex = e.NewPageIndex;
            GridView_users.DataBind();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Setting-GridView_users_PageIndexChanging");
        }
    }
    protected void DeleteUser(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkRemove = (LinkButton)sender;


            string username = "";
            DataTable dtsu = con.SelecthostProc(Com_username, "User_SelectByID", new string[] { "userid" }, lnkRemove.CommandArgument);
            if (dtsu.Rows.Count > 0)
            {
                username = dtsu.Rows[0][1].ToString();
            }

            con.ExcutehostProc(Com_username, "User_Delete", new string[] { "id" }, lnkRemove.CommandArgument);

            con.ExcutehostProc(Com_username, "User_Privlege_Delete", new string[] { "username" }, username);

            BindUserData();
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Setting-DeleteUser");
        }
    }
    protected void GridView_users_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        user_name.Enabled = false;
        try
        {
            Label lbId = (Label)GridView_users.Rows[e.NewSelectedIndex].FindControl("lblUserID");
            id = int.Parse(lbId.Text);

            Label lbName = (Label)GridView_users.Rows[e.NewSelectedIndex].FindControl("lblUserName");
            IsUpdate = true;

            DataTable dtsbu = con.SelecthostProc(Com_username, "User_SelectByID", new string[] { "userid" }, id);
            if (dtsbu.Rows.Count > 0)
            {
                user_name.Text = dtsbu.Rows[0][1].ToString();

                ////////////////////////////////////////
                if (dtsbu.Rows[0][3].ToString() == "0")
                {
                    Radio_admin.Checked = true;
                    Radio_person.Checked = false;
                }
                else if (dtsbu.Rows[0][3].ToString() == "1")
                {
                    Radio_person.Checked = true;
                    Radio_admin.Checked = false;
                }
                ///////////////////////////////////////
                if (dtsbu.Rows[0][4].ToString() == "ar")
                {
                    Radio_ar.Checked = true;
                    Radio_en.Checked = false;
                }
                else if (dtsbu.Rows[0][4].ToString() == "en")
                {
                    Radio_en.Checked = true;
                    Radio_ar.Checked = false;
                }
            }

            foreach (RepeaterItem r in Rpt_forms.Items)
            {
                CheckBox f = (CheckBox)r.FindControl("CheckBox1");
                f.Checked = false;
            }

            DataTable dtpris = con.SelecthostProc(Com_username, "User_Privlege_Select", new string[] { "username" }, lbName.Text);
            if (dtpris.Rows.Count > 0)
            {
                foreach (RepeaterItem r in Rpt_forms.Items)
                {
                    CheckBox f = (CheckBox)r.FindControl("CheckBox1");

                    for (int i = 0; i < dtpris.Rows.Count; i++)
                    {
                        if (dtpris.Rows[i][1].ToString() == f.ToolTip)
                        {
                            f.Checked = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            con.ExcutehostProc(Com_username, "Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-Setting-GridView_users_SelectedIndexChanging");
        }
    }
    protected void Radio_admin_CheckedChanged(object sender, EventArgs e)
    {
        foreach (RepeaterItem r in Rpt_forms.Items)
        {
            CheckBox f = (CheckBox)r.FindControl("CheckBox1");
            f.Checked = true;
        }
    }
}