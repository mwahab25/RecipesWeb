using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Panel_AppRegister : System.Web.UI.Page
{
    Connections con = new Connections();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_register_Click(object sender, EventArgs e)
    {
        try
        {
            if (chk_terms.Checked)
            {
                con.PanelExcuteProc("Rigister_Save", new string[] { "name", "type", "email" }, text_companyname.Value, ddl_type.Text, text_email.Value);
                text_companyname.Value = "";
                ddl_type.SelectedIndex = 0;
                text_email.Value = "";
                msg.InnerText = "Success! We will send your ";
            }
        }
        catch (Exception ex)
        {
            con.PanelExcuteProc("Log_Issues", new string[] { "msg", "details" }, ex.Message, "Err-register-save");
        }
    }
}