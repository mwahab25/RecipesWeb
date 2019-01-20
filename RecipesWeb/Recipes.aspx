<%@ Page Title="Recipes" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Recipes.aspx.cs" Inherits="Recipes" %>
<asp:Content ID="Content2" ContentPlaceHolderID="LangContent" runat="server">
     <li class="dropdown">
         <a href="Recipes.aspx" class="dropdown-toggle" data-toggle="dropdown">English
           <b class="caret"></b></a>
           <ul class="dropdown-menu">
               <li><a href="ar/Recipes.aspx">عربي</a></li>
                <li><a href="Recipes.aspx">English</a></li>
           </ul>
     </li>
 </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <link href="css/jquery-te-1.4.0.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    function doPostBack(o) {
        __doPostBack(o.id, '');
    }
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
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                  <li><a href="#category" data-toggle="tab">Categories</a></li>
                  <li class="active"><a href="#recipe" data-toggle="tab">Recipes</a></li>  
                   <li><a href="#howtodo" data-toggle="tab">How to do</a></li>           
              </ul>
              <div class="profile-tab-content tab-content">

                  <div class="tab-pane fade" id="category">   
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                          <ContentTemplate>                         
                       <div class="form-horizontal row-fluid">
                           <div class="control-group">
					        <div class="controls">	        
                                  <asp:LinkButton ID="cat_save" runat="server" CssClass="btn btn-large" OnClick="cat_save_Click" ValidationGroup="c">Save<i class="icon-save shaded"></i></asp:LinkButton>
                            </div>
				          </div>
                       <div class="control-group">
					         <label class="control-label" for="cat_name">Cat Name</label>
					        <div class="controls">	        
                                <asp:TextBox ID="cat_name" runat="server" Width="160px" CssClass="span8" ValidationGroup="c"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Category name" ControlToValidate="cat_name" Text="*" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                 </div>
				          </div>
                            <div class="control-group">
					         <label class="control-label" for="cat_nameAr">Cat Name(Arabic)</label>
					        <div class="controls">	        
                                <asp:TextBox ID="cat_nameAr" runat="server" Width="160px" CssClass="span8" ValidationGroup="c"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Category name" ControlToValidate="cat_nameAr" Text="*" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
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
                                           <asp:Label ID="lblCatID" runat="server" Text='<%# Eval("RecipeCat_ID")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Category">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="txtCatName" runat="server" Text='<%# Eval("RecipeCat_NameEn")%>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblCatName" runat="server" Text='<%# Eval("RecipeCat_NameEn")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Category">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="txtCatNameAr" runat="server" Text='<%# Eval("RecipeCat_NameAr")%>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblCatNameAr" runat="server" Text='<%# Eval("RecipeCat_NameAr")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:CommandField CancelText="Cancel" EditText="Update" ShowEditButton="True" UpdateText="Update" />
                                   <asp:TemplateField>
                                       <ItemTemplate>
                                           <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("RecipeCat_ID")%>' OnClientClick = "return confirm('No Delete if Use')" OnClick = "DeleteCat">Delete</asp:LinkButton>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                               </Columns>
                           </asp:GridView>                                                     
                         </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
                  <div class="tab-pane fade active in " id="recipe">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
                             <div class="form-horizontal row-fluid">
                              <div class="control-group">
					            <div class="controls">
                                    <asp:LinkButton ID="recipe_new" runat="server" CssClass="btn btn-large" OnClick="recipe_new_Click">New <i class="icon-file shaded"></i></asp:LinkButton>
                                    <asp:LinkButton ID="recipe_save" runat="server" CssClass="btn btn-large" OnClick="recipe_save_Click">Save <i class="icon-save shaded"></i></asp:LinkButton>
                                    <asp:LinkButton ID="recipe_update" runat="server" CssClass="btn btn-large" Visible="false" OnClick="recipe_update_Click">Update <i class="icon-edit shaded"></i></asp:LinkButton>						
                                    <asp:LinkButton ID="recipe_delete" runat="server" CssClass="btn btn-large" Visible="false" OnClientClick = "return confirm('Are you sure to delete?')" OnClick="recipe_delete_Click">Delete <i class="icon-cut shaded"></i></asp:LinkButton>    
                                    <asp:LinkButton ID="recipe_print" runat="server" CssClass="btn btn-large" OnClick="recipe_print_Click">Print <i class="icon-print shaded"></i></asp:LinkButton>                                    
                                </div>
				            </div>
                                 <div class="control-group">
					        <label class="control-label" for="recipe_no">Recipe No</label>
					        <div class="controls">						        
                                <asp:TextBox ID="recipe_no" runat="server" CssClass="span8" Width="160px" Enabled="false"></asp:TextBox>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                                  
                             <div class="control-group">
					        <label class="control-label" for="recipe_cat">Recipe Category</label>
					        <div class="controls">
                                <asp:DropDownList ID="recipe_cat" runat="server" CssClass="span8"></asp:DropDownList>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                                 <div class="control-group">
					        <label class="control-label" for="recipe_name">Recipe Name</label>
					        <div class="controls">						        
                                <asp:TextBox ID="recipe_name" runat="server" CssClass="span8"></asp:TextBox>
                                <asp:LinkButton ID="recipename_search" runat="server" CssClass="btn btn-small" OnClick="recipename_search_Click"><i class="icon-search shaded"></i></asp:LinkButton>
						        <span class="help-inline"></span>
					        </div>
				           </div>   
                                <div class="control-group">
					        <label class="control-label" for="recipe_nameAr">Recipe Name(Arabic)</label>
					        <div class="controls">						        
                                <asp:TextBox ID="recipe_nameAr" runat="server" CssClass="span8"></asp:TextBox>
                                <asp:LinkButton ID="recipenameAr_search" runat="server" CssClass="btn btn-small" OnClick="recipenameAr_search_Click"><i class="icon-search shaded"></i></asp:LinkButton>
						        <span class="help-inline"></span>
					        </div>
				           </div>   
                                 <div class="control-group" style="background-color:azure">
                                     <div class="controls" style="padding-top:5px">       
                                         <asp:Label ID="Label_items" runat="server" Text="Materials" Font-Bold="true"></asp:Label>
                                     </div>
                                 <label class="control-label" for="recipe_ingredcat">Category</label>
                                 <div class="controls" style="padding-top:5px">       
                                 <asp:DropDownList ID="recipe_ingredcat" runat="server" CssClass="span8" AutoPostBack="True" OnSelectedIndexChanged="recipe_batchcat_SelectedIndexChanged"></asp:DropDownList>
                                 </div>
                                 <label class="control-label" for="recipe_ingredname">Item name</label>
                                 <div class="controls" style="padding-top:5px"> 
                                     <asp:DropDownList ID="recipe_ingredname" runat="server" CssClass="span8"></asp:DropDownList>
                                 </div>
                                 <label class="control-label" for="recipe_ingredqty">Qty</label>
                                 <div class="controls" style="padding-top:5px">                                     
                                         <asp:TextBox ID="recipe_ingredqty" runat="server" Width="60px" TextMode="Number"></asp:TextBox>                
                                 </div>

                                 <label class="control-label" for="recipe_additem"></label>
                                 <div class="controls" style="padding-top:5px">                                                                           
                                     <asp:LinkButton ID="recipe_additem" runat="server" CssClass="btn btn-small" OnClick="recipe_additem_Click"><i class="icon-plus shaded"></i></asp:LinkButton>               
                              
                                 </div>
                                
                                 <div style="padding-top:10px">
                                     <asp:GridView ID="GridView_items" CssClass="table table-striped table-bordered table-condensed" runat="server" 
                                         AutoGenerateColumns="False" 
                                         OnRowDeleting="GridView_items_RowDeleting" OnRowEditing="GridView_items_RowEditing" 
                                         OnRowCancelingEdit="GridView_items_RowCancelingEdit" OnRowUpdating="GridView_items_RowUpdating">
                                         <Columns>
                                             <asp:TemplateField HeaderText="Id">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label1" runat="server" Text='<%# Bind("itemid") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Item">
                                                 
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label2" runat="server" Text='<%# Bind("itemname") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Unit">
                                   
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label3" runat="server" Text='<%# Bind("itemunit") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Price">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label4" runat="server" Text='<%# Bind("itemprice") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Qty">
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("itemqty") %>' Width="80px"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label6" runat="server" Text='<%# Bind("itemqty") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Total">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label5" runat="server" Text='<%# Bind("total") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                               <asp:CommandField CancelText="Cancel" EditText="Edit" ShowEditButton="True" UpdateText="Update" />
                                             <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                         </Columns>
                                     </asp:GridView> 
                                 </div>
                                 </div>

                                 <div class="control-group" style="background-color:beige">
                                  <div class="controls" style="padding-top:5px">       
                                         <asp:Label ID="Label_batchs" runat="server" Text="Batchs" Font-Bold="true"></asp:Label>
                                     </div>
                                 <label class="control-label" for="recipe_batchname">Batch name</label>
                                 <div class="controls" style="padding-top:5px"> 
                                     <asp:DropDownList ID="recipe_batchname" runat="server" CssClass="span8"></asp:DropDownList>
                                 </div>
                                 <label class="control-label" for="recipe_batchqty">Qty</label>
                                 <div class="controls" style="padding-top:5px">                                     
                                         <asp:TextBox ID="recipe_batchqty" runat="server" Width="60px" TextMode="Number"></asp:TextBox>                
                                 </div>

                                 <label class="control-label" for="recipe_batchadditem"></label>
                                 <div class="controls" style="padding-top:5px">                                                                           
                                     <asp:LinkButton ID="recipe_batchadditem" runat="server" CssClass="btn btn-small" OnClick="recipe_batchadditem_Click"><i class="icon-plus shaded"></i></asp:LinkButton>               
                              
                                 </div>
                                
                                 <div style="padding-top:10px">
                                     <asp:GridView ID="GridView_batchitems" CssClass="table table-striped table-bordered table-condensed" runat="server" AutoGenerateColumns="False" 
                                         OnRowDeleting="GridView_batchitems_RowDeleting" OnRowCancelingEdit="GridView_batchitems_RowCancelingEdit" OnRowEditing="GridView_batchitems_RowEditing" OnRowUpdating="GridView_batchitems_RowUpdating">
                                         <Columns>
                                             <asp:TemplateField HeaderText="Id">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label1" runat="server" Text='<%# Bind("batchid") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Batch">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label2" runat="server" Text='<%# Bind("batchname") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Unit">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label3" runat="server" Text='<%# Bind("batchunit") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Price">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label4" runat="server" Text='<%# Bind("batchprice") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Qty">
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("batchqty") %>' Width="80px"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label6" runat="server" Text='<%# Bind("batchqty") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Total">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label5" runat="server" Text='<%# Bind("total") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:CommandField ShowEditButton="True" />
                                             <asp:CommandField DeleteText="Cancel" ShowDeleteButton="True" />
                                         </Columns>
                                     </asp:GridView> 
                                 </div>
                                 </div>

                                  <div class="control-group">
                                  <label class="control-label" for="recipe_totalcost">Total Cost</label>
					            <div class="controls">
                                    <label class="radio inline">
                                        <asp:TextBox ID="recipe_totalcost" runat="server" Width="60px" Enabled="false"></asp:TextBox>
                                    </label>
                                     <label class="radio inline">
                                         Sell Price
                                         &nbsp &nbsp &nbsp
                                        
                                         <asp:TextBox ID="recipe_sellprice" runat="server" Width="60px"  onkeypress= "return isNumberKey(event)" onblur="doPostBack(this);"  OnTextChanged="recipe_sellprice_TextChanged"></asp:TextBox>
                                         
                                    </label>
                                    <label class="radio inline">
                                         Cost Margin
                                         &nbsp &nbsp &nbsp
                                         <asp:TextBox ID="recipe_costmargin" runat="server" Width="60px" onkeypress= "return isNumberKey(event)"></asp:TextBox>
                                    </label>
                                     
                                </div>
                                  
                             </div>

                                  <div class="control-group">
                                  <label class="control-label" for="recipe_target">Target</label>
					            <div class="controls">
                                    <label class="radio inline">
                                        <asp:TextBox ID="recipe_target" runat="server" Width="60px" onkeypress= "return isNumberKey(event)" onblur="doPostBack(this);"  OnTextChanged="recipe_target_TextChanged"></asp:TextBox>
                                    </label>
                                     <label class="radio inline">
                                         Variance
                                         &nbsp &nbsp &nbsp
                                        
                                         <asp:TextBox ID="recipe_variance" runat="server" Width="60px"  onkeypress= "return isNumberKey(event)"></asp:TextBox>
                                         
                                    </label>
                                    
                                </div>
                                  
                             </div>

                                 <div class="control-group">
					            <div class="controls">
                                    
                                </div>
                                  
                             </div>
                             </div>
                              <br />
                              <asp:GridView ID="GridView_Recipes" runat="server" AutoGenerateColumns="False" 
                                  CssClass="table table-striped table-bordered table-condensed" AllowPaging="True" OnPageIndexChanging="GridView_Recipes_PageIndexChanging" OnRowEditing="GridView_Recipes_RowEditing" AllowSorting="True" OnSorting="GridView_Recipes_Sorting" OnRowUpdating="GridView_Recipes_RowUpdating">

                                  <Columns>
                                      <asp:TemplateField HeaderText="ID" Visible="False" >
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipeID" runat="server" Text='<%# Bind("Recipe_ID") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Category">
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipecatname" runat="server" Text='<%# Bind("RecipeCat_NameEn") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Name">
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipename" runat="server" Text='<%# Bind("Recipe_NameEn") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                       <asp:TemplateField HeaderText="">
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipenameAr" runat="server" Text='<%# Bind("Recipe_NameAr") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Total Cost" SortExpression="Recipe_TotalCost">
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipecost" runat="server" Text='<%# Bind("Recipe_TotalCost") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Sell Price" SortExpression="Recipe_SellPrice">
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipeprice" runat="server" Text='<%# Bind("Recipe_SellPrice") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:CommandField ShowEditButton="True" ShowCancelButton="False" UpdateText="Edit" />
                                  </Columns>
                              </asp:GridView> 
                         </ContentTemplate>
                     </asp:UpdatePanel>
                  </div>  
                  <div class="tab-pane fade" id="howtodo">
                      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                          <ContentTemplate>
                        <div class="form-horizontal row-fluid">
                             <div class="control-group">
					        <div class="controls">	     
                                <asp:LinkButton ID="howtodo_new" runat="server" CssClass="btn btn-large" OnClick="howtodo_new_Click">New<i class="icon-file shaded"></i></asp:LinkButton>   
                                  <asp:LinkButton ID="howtodo_save" runat="server" CssClass="btn btn-large" OnClick="howtodo_save_Click">Save<i class="icon-save shaded"></i></asp:LinkButton>
                            </div>
				          </div>
                             <div class="control-group">
					        <label class="control-label" for="howtodo_reccat">Recipe Category</label>
					        <div class="controls">
                                <asp:DropDownList ID="howtodo_reccat" runat="server" CssClass="span8" AutoPostBack="true" OnSelectedIndexChanged="howtodo_reccat_SelectedIndexChanged"></asp:DropDownList>
						        <span class="help-inline"></span>
					        </div>
				           </div>

                             <div class="control-group">
					        <label class="control-label" for="howtodo_rec">Recipe</label>
					        <div class="controls">
                                <asp:DropDownList ID="howtodo_rec" runat="server" CssClass="span8" AutoPostBack="true" OnSelectedIndexChanged="howtodo_rec_SelectedIndexChanged"></asp:DropDownList>
						        <span class="help-inline"></span>
					        </div>
				           </div>

                            <div class="control-group">
					         <label class="control-label" for="txtEditor">How to do</label>
					        <div class="controls">	        
                             <asp:TextBox ID="txtEditor" TextMode="MultiLine" runat="server" CssClass="textEditor span8" onblur="Test()"></asp:TextBox>
                            <%--<asp:Button ID="btnText" runat="server" Text="Show Text" OnClick="btnText_Click" />
                            <asp:HiddenField ID="hdText" runat="server" />
                            <asp:TextBox ID="txtReText" TextMode="MultiLine" runat="server" CssClass="textEditor1"></asp:TextBox>--%>
                             </div>
				          </div>
                           
                        </div>
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

    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-te-1.4.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            bindEvents();
        });
        // attach the event binding function to every partial update
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            bindEvents();
        });
        function bindEvents(sender, args) {
            $('.textEditor').jqte();
            <%--$(".textEditor").jqte({
                blur: function () {
                    document.getElementById('<%=hdText.ClientID %>').value = document.getElementById('<%=txtEditor.ClientID %>').value;
                }
            });--%>
        }
</script>
</asp:Content>




