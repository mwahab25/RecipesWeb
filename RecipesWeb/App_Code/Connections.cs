using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Connections
/// </summary>
public class Connections
{
    public SqlConnection PanelCn = new SqlConnection(ConfigurationManager.ConnectionStrings["PanelConnectionExpr"].ToString());
    SqlDataAdapter Panelda = new SqlDataAdapter();
    DataSet Panelds = new DataSet();

    SqlConnection Cn = new SqlConnection("");
    SqlDataAdapter da = new SqlDataAdapter();
    DataSet ds = new DataSet();

    string OldCompany_username;

    public DataSet PanelSelect(string sql, string Table)
    {
        if (PanelCn.State != ConnectionState.Open)
        {
            PanelCn.Open();
        }
        //ds.Clear() ; 
        Panelda = new SqlDataAdapter(sql, PanelCn);
        Panelda.Fill(Panelds, Table);
        PanelCn.Close();
        return Panelds;
    }
    public DataTable PanelSelectProc(string proname, string[] colums, params object[] values)
    {
        SqlCommand Com = new SqlCommand();


        if (PanelCn.State != ConnectionState.Open)
        {
            PanelCn.Open();
        }
        Com.Connection = PanelCn;
        Com.CommandType = CommandType.StoredProcedure;
        Com.CommandText = proname;
        if (values != null)
        {
            for (int i = 0; i < values.Count(); i++)
            {
                Com.Parameters.AddWithValue("@" + colums[i], values[i]);
            }
        }
        SqlDataAdapter da1 = new SqlDataAdapter(Com);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        PanelCn.Close();
        return dt1;
    }

    public void PanelExcute(string sql)
    {
        if (PanelCn.State != ConnectionState.Open)
        {
            PanelCn.Open();
        }
        SqlCommand Run = new SqlCommand(sql, PanelCn);
        Run.CommandTimeout = 0;
        try
        {
            Run.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            //int o = 8;
        }
        PanelCn.Close();
    }
    public void PanelExcuteProc(string procname, string[] colums, params object[] values)
    {
        SqlCommand Com = new SqlCommand();
        if (PanelCn.State != ConnectionState.Open)
        {
            PanelCn.Open();
        }
        Com.Connection = PanelCn;
        Com.CommandType = CommandType.StoredProcedure;
        Com.CommandText = procname;
        if (values != null)
        {
            for (int i = 0; i < values.Count(); i++)
            {
                Com.Parameters.AddWithValue("@" + colums[i], values[i]);
            }
        }
        Com.CommandTimeout = 0;
        Com.ExecuteNonQuery();
        PanelCn.InfoMessage += new SqlInfoMessageEventHandler(connection_InfoMessage);
        PanelCn.Close();
    }
    static void connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
    {
        if (e.Message.ToString() == "Sucess")
        {

        }
    }

    public string CoString(string Company_name)
    {
        DataSet dsConnS = PanelSelect("select ConnectionString from Companies where Company_username = '" + Company_name + "'", "ConnS");
        return dsConnS.Tables["ConnS"].Rows[0][0].ToString();
    }

    public DataSet Selecthost(string sql, string Table, string Company_username)
    {
        if (OldCompany_username != Company_username || Cn.ConnectionString == "")
        {
            Cn.ConnectionString = CoString(Company_username);
            if (Cn.State != ConnectionState.Open)
            {
                Cn.Open();
            }
            else
            {
                Cn.Close();
                Cn.Open();
            }

            OldCompany_username = Company_username;
        }

        da = new SqlDataAdapter(sql, Cn);
        da.Fill(ds, Table);
        //    Cn.Close();
        // ds.Clear();
        return ds;
    }
    public DataTable SelecthostProc(string Company_username,string proname, string[] colums, params object[] values)
    {
        SqlCommand Com = new SqlCommand();
        if (OldCompany_username != Company_username || Cn.ConnectionString == "")
        {
            Cn.ConnectionString = CoString(Company_username);

            if (Cn.State != ConnectionState.Open)
            {
                Cn.Open();
            }
            else
            {
                Cn.Close();
                Cn.Open();
            }
            OldCompany_username = Company_username;
        }
        Com.Connection = Cn;
        Com.CommandType = CommandType.StoredProcedure;
        Com.CommandText = proname;
        if (values != null)
        {
            for (int i = 0; i < values.Count(); i++)
            {
                Com.Parameters.AddWithValue("@" + colums[i], values[i]);
            }
        }
        SqlDataAdapter da1 = new SqlDataAdapter(Com);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        //Cn.Close();
        return dt1;
    }

    public void Excutehost(string sql, string Company_username)
    {
        if (OldCompany_username != Company_username || Cn.ConnectionString == "")
        {
            Cn.ConnectionString = CoString(Company_username);
            if (Cn.State != ConnectionState.Open)
            {
                Cn.Open();
            }
            else
            {
                Cn.Close();
                Cn.Open();
            }

            OldCompany_username = Company_username;
        }

        SqlCommand Run = new SqlCommand(sql, Cn);
        Run.CommandTimeout = 0;
        Run.ExecuteNonQuery();
        //Cn.Close();
        //  ds.Clear();
    }
    public void ExcutehostProc(string Company_username,string procname, string[] colums, params object[] values)
    {
        SqlCommand Com = new SqlCommand();
        if (OldCompany_username != Company_username || Cn.ConnectionString == "")
        {
            Cn.ConnectionString = CoString(Company_username);
            if (Cn.State != ConnectionState.Open)
            {
                Cn.Open();
            }
            else
            {
                Cn.Close();
                Cn.Open();
            }

            OldCompany_username = Company_username;
        }
        Com.Connection = Cn;
        Com.CommandType = CommandType.StoredProcedure;
        Com.CommandText = procname;
        if (values != null)
        {
            for (int i = 0; i < values.Count(); i++)
            {
                Com.Parameters.AddWithValue("@" + colums[i], values[i]);
            }
        }
        Com.CommandTimeout = 0;
        Com.ExecuteNonQuery();
        Cn.InfoMessage += new SqlInfoMessageEventHandler(connection_InfoMessage);
        //Cn.Close();
    }

    public int ValidateLogin(string Company_username,string username, string password, string proc)
    {
        SqlConnection conn = new SqlConnection(CoString(Company_username));
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = proc;
        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = username;
        cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = password;
        cmd.Parameters.Add("@outres", SqlDbType.Int);
        cmd.Parameters["@outres"].Direction = ParameterDirection.Output;
        cmd.Connection = conn;
        int Results = 0;
        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
            Results = (int)cmd.Parameters["@outres"].Value;
        }
        catch (SqlException ex)
        {
            //ex.Message;
        }
        finally
        {
            cmd.Dispose();
            if (conn != null)
            {
                conn.Close();
            }
        }
        return Results;
    }

    public void Ctrl_Log(string ErrMsg, string details)
    {
        string sql = "insert into Log(ErrMsg,ErrDetails) values('" + ErrMsg + "','" + details + "')";
        PanelExcute(sql);
    }
}