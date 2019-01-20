<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LangContent" runat="server">
     <li class="dropdown">
         <a href="Default.aspx" class="dropdown-toggle" data-toggle="dropdown">English
           <b class="caret"></b></a>
           <ul class="dropdown-menu">
               <li><a href="ar/Default.aspx">عربي</a></li>
                <li><a href="Default.aspx">English</a></li>
           </ul>
     </li>
 </asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
                            <div class="btn-controls">
                                <div class="btn-box-row row-fluid">                                    
                                    <a href="#" class="btn-box big span4"><i class="icon-list-ul"></i><b><asp:Label ID="Label_ingred" runat="server" Text=""></asp:Label></b>
                                        <p class="text-muted">Materials</p>
                                    </a>
                                    <a href="#" class="btn-box big span4"><i class="icon-beaker"></i><b><asp:Label ID="Label_batch" runat="server" Text=""></asp:Label></b>
                                        <p class="text-muted">Batchs</p>
                                    </a>
                                    <a href="#" class="btn-box big span4"><i class="icon-food"></i><b><asp:Label ID="Label_recipe" runat="server" Text=""></asp:Label></b>
                                        <p class="text-muted">Recipes</p>
                                    </a>
                                </div> 
                            </div>
                            
                           
                            <div class="module">
                                <div class="module-head">
                                    <h3>Recipes Materials</h3>
                                </div>
                                <div class="module-body table">
                                    <table cellpadding="0" cellspacing="0" border="0" class="datatable-1 table table-bordered table-striped	 display" width="100%">
                                        <thead>
                                            <tr>
                                                <th>
                                                    Category
                                                </th>
                                               
                                                <th>
                                                    Material
                                                </th>
                                                <th>
                                                    Price
                                                </th>
                                                <th>
                                                    Unit
                                                </th>
                                            </tr>
                                        </thead>

                                        <tbody>
                                            <asp:Repeater ID="Repeater_items" runat="server">
                                                <ItemTemplate>
                                               <tr class="gradeA">
                                                <td>
                                                    <%# Eval("IngredientCat_NameEn") %>
                                                </td>
                                              
                                                <td>
                                                    <%# Eval("Ingredient_NameEn") %> | <%# Eval("Ingredient_NameAr") %>
                                                </td>
                                                <td class="center">
                                                   <%# Eval("Price") %>
                                                </td>
                                                <td class="center">
                                                   <%# Eval("Unit_NameEn") %>
                                                </td>
                                            </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            
                                        </tbody>

                                        <tfoot>
                                           <tr>
                                                <th>
                                                    Category
                                                </th>
                                                <th>
                                                    Material
                                                </th>
                                                <th>
                                                    Price
                                                </th>
                                                <th>
                                                    Unit
                                                </th>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                            <!--/.module-->
                        </div>
</asp:Content>
