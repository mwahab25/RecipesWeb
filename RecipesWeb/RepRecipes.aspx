<%@ Page Title="RepRecipes" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RepRecipes.aspx.cs" Inherits="RepRecipes" %>
<asp:Content ID="Content2" ContentPlaceHolderID="LangContent" runat="server">
     <li class="dropdown">
         <a href="RepRecipes.aspx" class="dropdown-toggle" data-toggle="dropdown">English
           <b class="caret"></b></a>
           <ul class="dropdown-menu">
               <li><a href="ar/RepRecipes.aspx">عربي</a></li>
                <li><a href="RepRecipes.aspx">English</a></li>
           </ul>
     </li>
 </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="content"> 
        <div class="module">
            <div class="module-body">   
                <div class="profile-head media">
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                      <ContentTemplate>                      
                   <div id="msgmsg" runat="server" class="alert" style="display:none">
							<button type="button" class="close" data-dismiss="alert">×</button>
                             <asp:Label ID="msg" runat="server" Text="mesage"></asp:Label>
                   </div>
                          </ContentTemplate>
                  </asp:UpdatePanel>
                </div>

                <ul class="profile-tab nav nav-tabs"> 
                  <li class="active"><a href="#Matcosts" data-toggle="tab">Recipes Costs</a></li>               
              </ul>

                <div class="profile-tab-content tab-content"> 
                    <div class="tab-pane fade active in" id="Matcosts">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          <ContentTemplate>   
                              <div class="form-horizontal row-fluid">
                                  <div class="control-group">
					                <div class="controls">
                                        <asp:LinkButton ID="Reccosts_query" runat="server" CssClass="btn btn-large" OnClick="Reccosts_query_Click">Refresh <i class="icon-refresh shaded"></i></asp:LinkButton> 
                                         <asp:LinkButton ID="Reccosts_print" runat="server" CssClass="btn btn-large" OnClick="Reccosts_print_Click">Print <i class="icon-print shaded"></i></asp:LinkButton>         
					                </div>
				                 </div> 
                                   <div class="control-group">
								<div class="controls">
                                    <label class="radio inline">
                                        <asp:RadioButton ID="Reccosts_all" runat="server" GroupName="mattypes" Text="All" Checked="True" AutoPostBack="True" OnCheckedChanged="Reccosts_all_CheckedChanged" />
									</label> 
									<label class="radio inline">
                                        <asp:RadioButton ID="Reccosts_bycat" runat="server" GroupName="mattypes" Text="By Category" AutoPostBack="True" OnCheckedChanged="Reccosts_bycat_CheckedChanged" />
									</label> 
									<label class="radio inline">
                                        <asp:RadioButton ID="Reccosts_byname" runat="server" GroupName="mattypes" Text="By Name" AutoPostBack="True" OnCheckedChanged="Reccosts_byname_CheckedChanged"/>					
									</label>                                 
								</div>
						</div>
                                 <div class="control-group">
					                <label class="control-label" for="Matcosts_cat">Category</label>
					                <div class="controls">
                                        <asp:DropDownList ID="Reccosts_cat" Width="160px" runat="server" CssClass="span8" Enabled="false"></asp:DropDownList>
						                <span class="help-inline">
                                           
                                         </span>
					                </div>
				                </div>
                                    <div class="control-group">
					                <label class="control-label" for="Reccosts_itemname">Recipe Name</label>
					                <div class="controls">
                                        <asp:TextBox ID="Reccosts_itemname" Width="160px" runat="server" CssClass="span8" Enabled="false"></asp:TextBox>
						                <span class="help-inline">
                                           
                                         </span>
					                </div>
				                </div>
                                   <div class="control-group">
                                <asp:GridView ID="GridView_items" AllowPaging="True" CssClass="table table-striped table-bordered table-condensed" runat="server" AutoGenerateColumns="False">
                                         <Columns>
                                             <asp:BoundField DataField="reccat" HeaderText="Category" ReadOnly="true" />
                                             <asp:BoundField DataField="recname" HeaderText="Recipe Name" ReadOnly="true" />
                                             <asp:BoundField DataField="reccost" HeaderText="Recipe Cost" ReadOnly="true" />
                                             <asp:BoundField DataField="recprice" HeaderText="Sell Price" ReadOnly="true" /> 
                                             <asp:BoundField DataField="recmargin" HeaderText="Cost Margin" ReadOnly="true" />   
                                             <asp:BoundField DataField="rectarget" HeaderText="Target" ReadOnly="true" />
                                              <asp:BoundField DataField="recvar" HeaderText="Variance" ReadOnly="true" />                          
                                         </Columns>
                                     </asp:GridView> 
                            </div>    

                                  <div class="control-group">
					                <div class="controls">    
					                </div>
				                 </div> 
                                   </div>
                          </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    
                </div>
                <br /><br />
            
             <div class="profile-head media">
                 
              </div>
           
            </div>
        </div>
     </div>
</asp:Content>

