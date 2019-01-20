<%@ Page Title="تقرير الخامات" Language="C#" MasterPageFile="~/ar/Site.master" AutoEventWireup="true" CodeFile="RepMaterials.aspx.cs" Inherits="ar_RepMaterials" %>
<asp:Content ID="Content2" ContentPlaceHolderID="LangContent" runat="server">
     <li class="dropdown"><a href="RepMaterials.aspx" class="dropdown-toggle" data-toggle="dropdown">عربي
            <b class="caret"></b></a>
            <ul class="dropdown-menu">
                <li><a href="../RepMaterials.aspx">English</a></li>
                <li><a href="RepMaterials.aspx">عربي</a></li>
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
                  <li class="active"><a href="#Matcosts" data-toggle="tab">اسعار الخامات</a></li>   
                    <li><a href="#Matpriceshis" data-toggle="tab">خط الاسعار</a></li>             
              </ul>

                <div class="profile-tab-content tab-content"> 
                    <div class="tab-pane fade active in" id="Matcosts">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          <ContentTemplate>   
                              <div class="form-horizontal row-fluid" dir="rtl">
                                  <div class="control-group">
					                <div class="controls">
                                        <asp:LinkButton ID="Matcosts_query" runat="server" CssClass="btn btn-large" OnClick="Matcosts_query_Click">تحديث <i class="icon-refresh shaded"></i></asp:LinkButton> 
                                         <asp:LinkButton ID="Matcosts_print" runat="server" CssClass="btn btn-large" OnClick="Matcosts_print_Click">طباعة <i class="icon-print shaded"></i></asp:LinkButton>         
					                </div>
				                 </div> 
                                   <div class="control-group">
								<div class="controls">
                                    <label class="radio inline">
                                        <asp:RadioButton ID="Matcosts_all" runat="server" GroupName="mattypes" Text="الكل" Checked="True" AutoPostBack="True" OnCheckedChanged="Matcosts_all_CheckedChanged" />
									</label> 
									<label class="radio inline">
                                        <asp:RadioButton ID="Matcosts_bycat" runat="server" GroupName="mattypes" Text="بالتصنيف" AutoPostBack="True" OnCheckedChanged="Matcosts_bycat_CheckedChanged" />
									</label> 
									<label class="radio inline">
                                        <asp:RadioButton ID="Matcosts_byname" runat="server" GroupName="mattypes" Text="بالاسم" AutoPostBack="True" OnCheckedChanged="Matcosts_byname_CheckedChanged"/>					
									</label>                                 
								</div>
						</div>
                                 <div class="control-group">
					                <label class="control-label" for="Matcosts_cat">التصنيف</label>
					                <div class="controls">
                                        <asp:DropDownList ID="Matcosts_cat" Width="160px" runat="server" CssClass="span8" Enabled="false"></asp:DropDownList>
						                <span class="help-inline">
                                           
                                         </span>
					                </div>
				                </div>
                                    <div class="control-group">
					                <label class="control-label" for="Matcosts_itemname">اسم الصنف</label>
					                <div class="controls">
                                        <asp:TextBox ID="Matcosts_itemname" Width="160px" runat="server" CssClass="span8" Enabled="false"></asp:TextBox>
						                <span class="help-inline">
                                           
                                         </span>
					                </div>
				                </div>
                                   <div class="control-group" dir="rtl">
                                <asp:GridView ID="GridView_items" AllowPaging="True" CssClass="table table-striped table-bordered table-condensed" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GridView_items_PageIndexChanging" PageSize="20">
                                         <Columns>
                                             <asp:BoundField DataField="itemcat" HeaderText="تصنيف" ReadOnly="true" />
                                             <asp:BoundField DataField="itemname" HeaderText="الصنف" ReadOnly="true" />
                                             <asp:BoundField DataField="itemunit" HeaderText="الوحدة" ReadOnly="true" />
                                             <asp:BoundField DataField="itemprice" HeaderText="السعر" ReadOnly="true" />                       
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

                    <div class="tab-pane fade" id="Matpriceshis">
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                          <ContentTemplate>
                              <div class="form-horizontal row-fluid" dir="rtl">
                                  <div class="control-group">
					                <div class="controls">
                                        <asp:LinkButton ID="Matpriceshis_query" runat="server" CssClass="btn btn-large" OnClick="Matpriceshis_query_Click">تحديث <i class="icon-refresh shaded"></i></asp:LinkButton> 
                                         <asp:LinkButton ID="Matpriceshis_print" runat="server" CssClass="btn btn-large" OnClick="Matpriceshis_print_Click">طباعة <i class="icon-print shaded"></i></asp:LinkButton>         
					                </div>
				                 </div> 
                                  <div class="control-group">
					                <label class="control-label" for="Matpriceshis_cat">التصنيف</label>
					                <div class="controls">
                                        <asp:DropDownList ID="Matpriceshis_cat" Width="160px" runat="server" CssClass="span8" AutoPostBack="True" OnSelectedIndexChanged="Matpriceshis_cat_SelectedIndexChanged"></asp:DropDownList>
						                <span class="help-inline">
                                           
                                         </span>
					                </div>
				                </div>
                                  <div class="control-group">
					                <label class="control-label" for="Matpriceshis_name">اسم الصنف</label>
					                <div class="controls">
                                       <asp:DropDownList ID="Matpriceshis_name" Width="160px" runat="server" CssClass="span8"></asp:DropDownList>
						                <span class="help-inline">
                                           
                                         </span>
					                </div>
				                </div>
                                  <div class="control-group">
                                <asp:GridView ID="GridView_Matpriceshis_prices" AllowPaging="false" CssClass="table table-striped table-bordered table-condensed" runat="server" AutoGenerateColumns="False">
                                         <Columns>
                                             <asp:BoundField DataField="itemadddate" HeaderText="تاريخ" ReadOnly="true" />                                            
                                             <asp:BoundField DataField="itemprice" HeaderText="السعر" ReadOnly="true" />                    
                                         </Columns>
                                     </asp:GridView> 
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


