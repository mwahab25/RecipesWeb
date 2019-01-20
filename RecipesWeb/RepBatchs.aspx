﻿<%@ Page Title="RepBatchs" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RepBatchs.aspx.cs" Inherits="RepBatchs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="LangContent" runat="server">
     <li class="dropdown">
         <a href="RepBatchs.aspx" class="dropdown-toggle" data-toggle="dropdown">English
           <b class="caret"></b></a>
           <ul class="dropdown-menu">
               <li><a href="ar/RepBatchs.aspx">عربي</a></li>
                <li><a href="RepBatchs.aspx">English</a></li>
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
                  <li class="active"><a href="#Matcosts" data-toggle="tab">Batchs Costs</a></li>               
              </ul>

                <div class="profile-tab-content tab-content"> 
                    <div class="tab-pane fade active in" id="Batcosts">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          <ContentTemplate>   
                              <div class="form-horizontal row-fluid">
                                  <div class="control-group">
					                <div class="controls">
                                        <asp:LinkButton ID="Batcosts_query" runat="server" CssClass="btn btn-large" OnClick="Batcosts_query_Click">Refresh <i class="icon-refresh shaded"></i></asp:LinkButton> 
                                         <asp:LinkButton ID="Batcosts_print" runat="server" CssClass="btn btn-large" OnClick="Batcosts_print_Click">Print <i class="icon-print shaded"></i></asp:LinkButton>         
					                </div>
				                 </div> 
                                   <div class="control-group">
								<div class="controls">
                                    <label class="radio inline">
                                        <asp:RadioButton ID="Batcosts_all" runat="server" GroupName="battypes" Text="All" Checked="True" AutoPostBack="True" OnCheckedChanged="Batcosts_all_CheckedChanged" />
									</label> 
									<label class="radio inline">
                                        <asp:RadioButton ID="Batcosts_byname" runat="server" GroupName="battypes" Text="By Name" AutoPostBack="True" OnCheckedChanged="Batcosts_byname_CheckedChanged"/>					
									</label>                                 
								</div>
						</div>
                                    <div class="control-group">
					                <label class="control-label" for="Batcosts_itemname">Batch Name</label>
					                <div class="controls">
                                        <asp:TextBox ID="Batcosts_itemname" Width="160px" runat="server" CssClass="span8" Enabled="false"></asp:TextBox>
						                <span class="help-inline">
                                           
                                         </span>
					                </div>
				                </div>
                                   <div class="control-group">
                                <asp:GridView ID="GridView_items" AllowPaging="True" CssClass="table table-striped table-bordered table-condensed" runat="server" AutoGenerateColumns="False">
                                         <Columns>                                         
                                             <asp:BoundField DataField="batchname" HeaderText="Batch Name" ReadOnly="true" />
                                             <asp:BoundField DataField="batchunit" HeaderText="Unit" ReadOnly="true" />
                                             <asp:BoundField DataField="batchcost" HeaderText="Cost" ReadOnly="true" />
                                             <asp:BoundField DataField="batchprice" HeaderText="Price" ReadOnly="true" />                       
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
