<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecipeGenerate.aspx.cs" Inherits="Reports_RecipeGenerate" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width: 100%;text-align:center;">
            <tr>
                <td style="text-align:center;">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" 
                        EnableDatabaseLogonPrompt="false" ReuseParameterValuesOnRefresh="true" 
                        ToolPanelView="None" ToolPanelWidth="150px" Width="350px"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
