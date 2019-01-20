<%@ Page Title="Ingredients" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Ingredients.aspx.cs" Inherits="Ingredients" %>
<asp:Content ID="Content2" ContentPlaceHolderID="LangContent" runat="server">
     <li class="dropdown">
         <a href="Ingredients.aspx" class="dropdown-toggle" data-toggle="dropdown">English
           <b class="caret"></b></a>
           <ul class="dropdown-menu">
               <li><a href="ar/Ingredients.aspx">عربي</a></li>
                <li><a href="Ingredients.aspx">English</a></li>
           </ul>
     </li>
 </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <script type="text/javascript">
         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                 return false;

             return true;
         }
    </script>
 <div class="content"> 
      <div class="module">
        <div class="module-body">   
            <!-- head -->
              <div class="profile-head media">
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                      <ContentTemplate>
                        <div id="msgmsg" runat="server" class="alert" style="display:none">
							<button type="button" class="close" data-dismiss="alert">×</button>
                             <asp:Label ID="msg" runat="server" Text=""></asp:Label>
                        </div>
                         
                      </ContentTemplate>
                  </asp:UpdatePanel>                 
              </div>
            <!-- head -->

              <ul class="profile-tab nav nav-tabs">
                  <li ><a href="#unit" data-toggle="tab">Units</a></li>
                  <li><a href="#category" data-toggle="tab">Categories</a></li>      
                  <li class="active"><a href="#ingred" data-toggle="tab">Materials</a></li>           
              </ul>
              <div class="profile-tab-content tab-content">

                  <div class="tab-pane fade" id="unit">   
                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          <ContentTemplate>                         
                       <div class="form-horizontal row-fluid">
                           <div class="control-group">
					        <div class="controls">	        
                                 <asp:LinkButton ID="unit_save" runat="server" CssClass="btn btn-large" OnClick="unit_save_Click" ValidationGroup="u">Save<i class="icon-filter shaded"></i></asp:LinkButton>
                            </div>
				          </div>
                       <div class="control-group">
					        <label class="control-label" for="unit_name">Unit name</label>
					        <div class="controls">	        
                                <asp:TextBox ID="unit_name" runat="server" Width="160px" CssClass="span8" ValidationGroup="u"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Unit name" ControlToValidate="unit_name" Text="*" ForeColor="Red" ValidationGroup="u"></asp:RequiredFieldValidator>
                                 </div>
				          </div>
                           <div class="control-group">
					        <label class="control-label" for="unit_nameAr">Unit name(Arabic)</label>
					        <div class="controls">	        
                                <asp:TextBox ID="unit_nameAr" runat="server" Width="160px" CssClass="span8" ValidationGroup="u"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Unit name" ControlToValidate="unit_nameAr" Text="*" ForeColor="Red" ValidationGroup="u"></asp:RequiredFieldValidator>
                                 </div>
				          </div>
                       </div>
                       <br />
                              <asp:GridView ID="GridView_units" runat="server" 
                                  CssClass="table table-striped table-bordered table-condensed" 
                                  AllowPaging="True" AutoGenerateColumns="False" 
                                  OnPageIndexChanging="GridView_units_PageIndexChanging" 
                                  OnRowCancelingEdit="GridView_units_RowCancelingEdit" 
                                  OnRowEditing="GridView_units_RowEditing" 
                                  OnRowUpdating="GridView_units_RowUpdating" Width="100%">
                               <Columns>
                                   <asp:TemplateField HeaderText="S" Visible="False">
                                       <ItemTemplate>
                                           <asp:Label ID="lblUnitID" runat="server" Text='<%# Eval("Unit_ID")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Unit">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="txtUnitName" runat="server" Text='<%# Eval("Unit_NameEn")%>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("Unit_NameEn")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="txtUnitNameAr" runat="server" Text='<%# Eval("Unit_NameAr")%>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblUnitNameAr" runat="server" Text='<%# Eval("Unit_NameAr")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:CommandField CancelText="Cancel" EditText="Update" ShowEditButton="True" UpdateText="Update" />
                                   <asp:TemplateField>
                                       <ItemTemplate>
                                           <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("Unit_ID")%>' OnClientClick = "return confirm('No Delete if Use')" OnClick = "DeleteUnit">Delete</asp:LinkButton>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                               </Columns>
                           </asp:GridView>                                                     
                         </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
                  <div class="tab-pane fade" id="category">
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                          <ContentTemplate>                         
                       <div class="form-horizontal row-fluid">
                            <div class="control-group">
					        <div class="controls">	        
                                 <asp:LinkButton ID="cat_save" runat="server" CssClass="btn btn-large" OnClick="cat_save_Click" ValidationGroup="c">Save<i class="icon-filter shaded"></i></asp:LinkButton>
                            </div>
				          </div>
                       <div class="control-group">
					        <label class="control-label" for="cat_name">Cat name</label>
					        <div class="controls">	        
                                <asp:TextBox ID="cat_name" runat="server" Width="160px" CssClass="span8" ValidationGroup="c"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Category name" ControlToValidate="cat_name" Text="*" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                 </div>
				          </div>
                            <div class="control-group">
					        <label class="control-label" for="cat_nameAr">Cat name(Arabic)</label>
					        <div class="controls">	        
                                <asp:TextBox ID="cat_nameAr" runat="server" Width="160px" CssClass="span8" ValidationGroup="c"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Category name" ControlToValidate="cat_nameAr" Text="*" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                 </div>
				          </div>
                       </div>
                       <br />
                              <asp:GridView ID="GridView_cats" runat="server" 
                                  CssClass="table table-striped table-bordered table-condensed" 
                                  AllowPaging="True" AutoGenerateColumns="False" 
                                  OnPageIndexChanging="GridView_cats_PageIndexChanging" 
                                  OnRowCancelingEdit="GridView_cats_RowCancelingEdit" 
                                  OnRowEditing="GridView_cats_RowEditing" 
                                  OnRowUpdating="GridView_cats_RowUpdating" Width="100%">
                               <Columns>
                                   <asp:TemplateField HeaderText="S" Visible="False">
                                       <ItemTemplate>
                                           <asp:Label ID="lblCatID" runat="server" Text='<%# Eval("IngredientCat_ID")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Category">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="txtCatName" runat="server" Text='<%# Eval("IngredientCat_NameEn")%>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblCatName" runat="server" Text='<%# Eval("IngredientCat_NameEn")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="txtCatNameAr" runat="server" Text='<%# Eval("IngredientCat_NameAr")%>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblCatNameAr" runat="server" Text='<%# Eval("IngredientCat_NameAr")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:CommandField CancelText="Cancel" EditText="Update" ShowEditButton="True" UpdateText="Update" />
                                   <asp:TemplateField>
                                       <ItemTemplate>
                                           <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("IngredientCat_ID")%>' OnClientClick = "return confirm('No Delete if Use')" OnClick = "DeleteCat">Delete</asp:LinkButton>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                               </Columns>
                           </asp:GridView>                                                     
                         </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
                   <div class="tab-pane fade active in" id="ingred">
                      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                          <ContentTemplate>
                        <div class="form-horizontal row-fluid">
                            <div class="control-group">
					            <div class="controls">
                                    <asp:LinkButton ID="item_new_" runat="server" CssClass="btn btn-large" OnClick="item_new__Click">New <i class="icon-file shaded"></i></asp:LinkButton>
                                    <asp:LinkButton ID="item_save_" runat="server" CssClass="btn btn-large" OnClick="item_save__Click">Save <i class="icon-save shaded"></i></asp:LinkButton>
                                    <asp:LinkButton ID="item_update_" runat="server" CssClass="btn btn-large" Visible="false" OnClick="item_update__Click">Update <i class="icon-edit shaded"></i></asp:LinkButton>						
                                    <asp:LinkButton ID="item_delete_" runat="server" CssClass="btn btn-large" Visible="false" OnClientClick = "return confirm('Not Delete if used?')" OnClick="item_delete__Click">Delete <i class="icon-cut shaded"></i></asp:LinkButton>                                     
                                </div>
				            </div>
                           <div class="control-group">
					        <label class="control-label" for="item_cat">Category</label>
					        <div class="controls">
                                <asp:DropDownList ID="item_cat" runat="server" CssClass="span8" Width="200px"></asp:DropDownList>
                                <asp:LinkButton ID="itemcat_search" runat="server" CssClass="btn btn-small" Height="16px" OnClick="itemcat_search_Click" ><i class="icon-search shaded"></i></asp:LinkButton>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                           <div class="control-group">
					        <label class="control-label" for="item_name">Material Name</label>
					        <div class="controls">						        
                                <asp:TextBox ID="item_name" runat="server" CssClass="span8"></asp:TextBox>
                                <asp:LinkButton ID="itemname_search" runat="server" CssClass="btn btn-small" OnClick="itemname_search_Click" ><i class="icon-search shaded"></i></asp:LinkButton>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                            <div class="control-group">
					        <label class="control-label" for="item_nameAr">Name(Arabic)</label>
					        <div class="controls">						        
                                <asp:TextBox ID="item_nameAr" runat="server" CssClass="span8"></asp:TextBox>
                                <asp:LinkButton ID="itemnameAr_search" runat="server" CssClass="btn btn-small" OnClick="itemnameAr_search_Click" ><i class="icon-search shaded"></i></asp:LinkButton>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                           
                            <div class="control-group">
					        <label class="control-label" for="item_expiredate">Price</label>
					        <div class="controls">                                
                                <asp:TextBox ID="item_price" runat="server" CssClass="span8" Width="160px" onkeypress= "return isNumberKey(event)"></asp:TextBox>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                             <div class="control-group">
					        <label class="control-label" for="item_unit">Unit</label>
					        <div class="controls">
                                <asp:DropDownList ID="item_unit" runat="server" CssClass="span8" Width="160px"></asp:DropDownList>
						        <span class="help-inline"></span>
					        </div>
				           </div>

                             <%-- <div class="control-group">
					            <div class="controls">
                                    <asp:LinkButton ID="item_new" runat="server" CssClass="btn btn-small" OnClick="item_new__Click">New <i class="icon-file shaded"></i></asp:LinkButton>
                                    <asp:LinkButton ID="item_save" runat="server" CssClass="btn btn-small" OnClick="item_save__Click">Save <i class="icon-save shaded"></i></asp:LinkButton>
                                    <asp:LinkButton ID="item_update" runat="server" CssClass="btn btn-small" Visible="false" OnClick="item_update__Click">Update <i class="icon-edit shaded"></i></asp:LinkButton>						
                                    <asp:LinkButton ID="item_delete" runat="server" CssClass="btn btn-small" Visible="false" OnClick="item_delete__Click" OnClientClick = "return confirm('Are you sure to delete?')">Delete <i class="icon-cut shaded"></i></asp:LinkButton>
					            </div>
				            </div>--%>
                        </div> 
                        <br />
                              <asp:GridView ID="GridView_items" runat="server" AutoGenerateColumns="False" 
                                  CssClass="table table-striped table-bordered table-condensed" AllowPaging="True" OnPageIndexChanging="GridView_items_PageIndexChanging" OnRowEditing="GridView_items_RowEditing" AllowSorting="True" OnSorting="GridView_items_Sorting" OnRowUpdating="GridView_items_RowUpdating">

                                  <Columns>
                                      <asp:TemplateField HeaderText="ID" Visible="False" >
                                          <ItemTemplate>
                                              <asp:Label ID="LblIngredID" runat="server" Text='<%# Bind("Ingredient_ID") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Category">
                                          <ItemTemplate>
                                              <asp:Label ID="LblIngredcat" runat="server" Text='<%# Bind("IngredientCat_NameEn") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Name">
                                          <ItemTemplate>
                                              <asp:Label ID="LblIngredname" runat="server" Text='<%# Bind("Ingredient_NameEn") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                          <ItemTemplate>
                                              <asp:Label ID="LblIngrednameAr" runat="server" Text='<%# Bind("Ingredient_NameAr") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Price" SortExpression="Price">
                                          <ItemTemplate>
                                              <asp:Label ID="LblIngredprice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Unit">
                                          <ItemTemplate>
                                              <asp:Label ID="LblIngredunit" runat="server" Text='<%# Bind("Unit_NameEn") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:CommandField ShowEditButton="True" ShowCancelButton="False" UpdateText="Edit" />
                                  </Columns>
                              </asp:GridView>                                          
                     </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>                    
             </div> 
            <br /><br />
            <!-- foot -->
             <div class="profile-head media">
                 
              </div>
            <!-- foot -->
       </div>  
    </div>
    </div>    
</asp:Content>


