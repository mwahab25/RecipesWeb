<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>
<asp:Content ID="Content2" ContentPlaceHolderID="LangContent" runat="server">
     <li class="dropdown">
         <a href="About.aspx" class="dropdown-toggle" data-toggle="dropdown">English
           <b class="caret"></b></a>
           <ul class="dropdown-menu">
               <li><a href="ar/About.aspx">عربي</a></li>
                <li><a href="About.aspx">English</a></li>
           </ul>
     </li>
 </asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
</asp:Content>
