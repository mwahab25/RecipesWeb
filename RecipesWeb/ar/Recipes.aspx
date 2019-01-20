<%@ Page Title="الوصفات" Language="C#" MasterPageFile="~/ar/Site.master" AutoEventWireup="true" CodeFile="Recipes.aspx.cs" Inherits="ar_Recipes" %>
<asp:Content ID="Content2" ContentPlaceHolderID="LangContent" runat="server">
     <li class="dropdown"><a href="Recipes.aspx" class="dropdown-toggle" data-toggle="dropdown">عربي
            <b class="caret"></b></a>
            <ul class="dropdown-menu">
                <li><a href="../Recipes.aspx">English</a></li>
                <li><a href="Recipes.aspx">عربي</a></li>
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
                  <li><a href="#category" data-toggle="tab">التصنيفات</a></li>
                  <li class="active"><a href="#recipe" data-toggle="tab">الوصفات</a></li>   
                  <li><a href="#howtodo" data-toggle="tab">كيفية عمل الوصفة</a></li>            
              </ul>
              <div class="profile-tab-content tab-content">

                  <div class="tab-pane fade" id="category">   
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                          <ContentTemplate>                         
                       <div class="form-horizontal row-fluid" dir="rtl">
                           <div class="control-group">
					        <div class="controls">	        
                                  <asp:LinkButton ID="cat_save" runat="server" CssClass="btn btn-large" OnClick="cat_save_Click" ValidationGroup="c">حفظ <i class="icon-filter shaded"></i></asp:LinkButton>
                            </div>
				          </div>
                       <div class="control-group">
					         <label class="control-label" for="cat_nameAr">إسم التصنيف</label>
					        <div class="controls">	        
                                <asp:TextBox ID="cat_nameAr" runat="server" Width="160px" CssClass="span8" ValidationGroup="c"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Category name" ControlToValidate="cat_nameAr" Text="*" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                 </div>
				          </div>
                            <div class="control-group">
					         <label class="control-label" for="cat_name">إسم(English)</label>
					        <div class="controls">	        
                                <asp:TextBox ID="cat_name" runat="server" Width="160px" CssClass="span8" ValidationGroup="c"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Category name" ControlToValidate="cat_name" Text="*" ForeColor="Red" ValidationGroup="c"></asp:RequiredFieldValidator>
                                 </div>
				          </div>
                       </div>
                       <br />
                              <div dir="rtl">
                              <asp:GridView ID="GridView_cats" runat="server" 
                                  CssClass="table table-striped table-bordered table-condensed" 
                                  AllowPaging="True" AutoGenerateColumns="False" 
                                  OnPageIndexChanging="GridView_cats_PageIndexChanging" 
                                  OnRowCancelingEdit="GridView_cats_RowCancelingEdit" 
                                  OnRowEditing="GridView_cats_RowEditing" 
                                  OnRowUpdating="GridView_cats_RowUpdating" Width="100%">
                               <Columns>
                                   <asp:TemplateField HeaderText="م" Visible="False">
                                       <ItemTemplate>
                                           <asp:Label ID="lblCatID" runat="server" Text='<%# Eval("RecipeCat_ID")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="تصنيف">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="txtCatNameAr" runat="server" Text='<%# Eval("RecipeCat_NameAr")%>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblCatNameAr" runat="server" Text='<%# Eval("RecipeCat_NameAr")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="">
                                       <EditItemTemplate>
                                           <asp:TextBox ID="txtCatName" runat="server" Text='<%# Eval("RecipeCat_NameEn")%>'></asp:TextBox>
                                       </EditItemTemplate>
                                       <ItemTemplate>
                                           <asp:Label ID="lblCatName" runat="server" Text='<%# Eval("RecipeCat_NameEn")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:CommandField CancelText="الغاء" EditText="تحرير" ShowEditButton="True" UpdateText="تعديل" />
                                   <asp:TemplateField>
                                       <ItemTemplate>
                                           <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("RecipeCat_ID")%>' OnClientClick = "return confirm('لا يمكن الحذف في حالة الاستخدام')" OnClick = "DeleteCat">حذف</asp:LinkButton>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                               </Columns>
                           </asp:GridView>      
                                  </div>                                               
                         </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
                  <div class="tab-pane fade active in " id="recipe">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
                             <div class="form-horizontal row-fluid" dir="rtl">
                              <div class="control-group">
					            <div class="controls">
                                    <asp:LinkButton ID="recipe_new" runat="server" CssClass="btn btn-large" OnClick="recipe_new_Click">جديد <i class="icon-file shaded"></i></asp:LinkButton>
                                    <asp:LinkButton ID="recipe_save" runat="server" CssClass="btn btn-large" OnClick="recipe_save_Click">حفظ <i class="icon-save shaded"></i></asp:LinkButton>
                                    <asp:LinkButton ID="recipe_update" runat="server" CssClass="btn btn-large" Visible="false" OnClick="recipe_update_Click">تعديل <i class="icon-edit shaded"></i></asp:LinkButton>						
                                    <asp:LinkButton ID="recipe_delete" runat="server" CssClass="btn btn-large" Visible="false" OnClientClick = "return confirm('هل متأكد من الحذف؟')" OnClick="recipe_delete_Click">حذف <i class="icon-cut shaded"></i></asp:LinkButton>    
                                    <asp:LinkButton ID="recipe_print" runat="server" CssClass="btn btn-large" OnClick="recipe_print_Click">طباعة <i class="icon-print shaded"></i></asp:LinkButton>                                    
                                </div>
				            </div>
                                 <div class="control-group">
					        <label class="control-label" for="recipe_no">رقم الوصفة</label>
					        <div class="controls">						        
                                <asp:TextBox ID="recipe_no" runat="server" CssClass="span8" Width="160px" Enabled="false"></asp:TextBox>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                                  
                             <div class="control-group">
					        <label class="control-label" for="recipe_cat">تصنيف</label>
					        <div class="controls">
                                <asp:DropDownList ID="recipe_cat" runat="server" CssClass="span8"></asp:DropDownList>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                                  <div class="control-group">
					        <label class="control-label" for="recipe_nameAr">اسم الوصفة</label>
					        <div class="controls">						        
                                <asp:TextBox ID="recipe_nameAr" runat="server" CssClass="span8"></asp:TextBox>
                                <asp:LinkButton ID="recipenameAr_search" runat="server" CssClass="btn btn-small" OnClick="recipenameAr_search_Click"><i class="icon-search shaded"></i></asp:LinkButton>
						        <span class="help-inline"></span>
					        </div>
				           </div>   
                                 <div class="control-group">
					        <label class="control-label" for="recipe_name">اسم الوصفة(En)</label>
					        <div class="controls">						        
                                <asp:TextBox ID="recipe_name" runat="server" CssClass="span8"></asp:TextBox>
                                <asp:LinkButton ID="recipename_search" runat="server" CssClass="btn btn-small" OnClick="recipename_search_Click"><i class="icon-search shaded"></i></asp:LinkButton>
						        <span class="help-inline"></span>
					        </div>
				           </div>   
                               
                                 <div class="control-group" style="background-color:azure">
                                     <div class="controls" style="padding-top:5px">       
                                         <asp:Label ID="Label_items" runat="server" Text="الخامات" Font-Bold="true"></asp:Label>
                                     </div>
                                 <label class="control-label" for="recipe_ingredcat">تصنيف</label>
                                 <div class="controls" style="padding-top:5px">       
                                 <asp:DropDownList ID="recipe_ingredcat" runat="server" CssClass="span8" AutoPostBack="True" OnSelectedIndexChanged="recipe_batchcat_SelectedIndexChanged"></asp:DropDownList>
                                 </div>
                                 <label class="control-label" for="recipe_ingredname">الصنف</label>
                                 <div class="controls" style="padding-top:5px"> 
                                     <asp:DropDownList ID="recipe_ingredname" runat="server" CssClass="span8"></asp:DropDownList>
                                 </div>
                                 <label class="control-label" for="recipe_ingredqty">الكمية</label>
                                 <div class="controls" style="padding-top:5px">                                     
                                         <asp:TextBox ID="recipe_ingredqty" runat="server" Width="60px" TextMode="Number"></asp:TextBox>                
                                 </div>

                                 <label class="control-label" for="recipe_additem"></label>
                                 <div class="controls" style="padding-top:5px">                                                                           
                                     <asp:LinkButton ID="recipe_additem" runat="server" CssClass="btn btn-small" OnClick="recipe_additem_Click"><i class="icon-plus shaded"></i></asp:LinkButton>               
                              
                                 </div>
                                
                                 <div style="padding-top:10px" dir="rtl">
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
                                             <asp:TemplateField HeaderText="الصنف">
                                                 
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label2" runat="server" Text='<%# Bind("itemname") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="الوحدة">
                                   
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label3" runat="server" Text='<%# Bind("itemunit") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="السعر">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label4" runat="server" Text='<%# Bind("itemprice") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="الكمية">
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("itemqty") %>' Width="80px"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label6" runat="server" Text='<%# Bind("itemqty") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="الاجمالي">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label5" runat="server" Text='<%# Bind("total") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                               <asp:CommandField CancelText="إلغاء" EditText="تحرير" ShowEditButton="True" UpdateText="تعديل" />
                                             <asp:CommandField ShowDeleteButton="True" DeleteText="إلغاء"></asp:CommandField>
                                         </Columns>
                                     </asp:GridView> 
                                 </div>
                                 </div>

                                 <div class="control-group" style="background-color:beige">
                                  <div class="controls" style="padding-top:5px">       
                                         <asp:Label ID="Label_batchs" runat="server" Text="الباتشات" Font-Bold="true"></asp:Label>
                                     </div>
                                 <label class="control-label" for="recipe_batchname">اسم الباتش</label>
                                 <div class="controls" style="padding-top:5px"> 
                                     <asp:DropDownList ID="recipe_batchname" runat="server" CssClass="span8"></asp:DropDownList>
                                 </div>
                                 <label class="control-label" for="recipe_batchqty">الكمية</label>
                                 <div class="controls" style="padding-top:5px">                                     
                                         <asp:TextBox ID="recipe_batchqty" runat="server" Width="60px" TextMode="Number"></asp:TextBox>                
                                 </div>

                                 <label class="control-label" for="recipe_batchadditem"></label>
                                 <div class="controls" style="padding-top:5px">                                                                           
                                     <asp:LinkButton ID="recipe_batchadditem" runat="server" CssClass="btn btn-small" OnClick="recipe_batchadditem_Click"><i class="icon-plus shaded"></i></asp:LinkButton>               
                              
                                 </div>
                                
                                 <div style="padding-top:10px" dir="rtl">
                                     <asp:GridView ID="GridView_batchitems" CssClass="table table-striped table-bordered table-condensed" runat="server" AutoGenerateColumns="False" 
                                         OnRowDeleting="GridView_batchitems_RowDeleting" OnRowCancelingEdit="GridView_batchitems_RowCancelingEdit" OnRowEditing="GridView_batchitems_RowEditing" OnRowUpdating="GridView_batchitems_RowUpdating">
                                         <Columns>
                                             <asp:TemplateField HeaderText="Id">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label1" runat="server" Text='<%# Bind("batchid") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="الباتش">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label2" runat="server" Text='<%# Bind("batchname") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="الوحدة">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label3" runat="server" Text='<%# Bind("batchunit") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="السعر">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label4" runat="server" Text='<%# Bind("batchprice") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="الكمية">
                                                 <EditItemTemplate>
                                                     <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("batchqty") %>' Width="80px"></asp:TextBox>
                                                 </EditItemTemplate>
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label6" runat="server" Text='<%# Bind("batchqty") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="الاجمالي">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label5" runat="server" Text='<%# Bind("total") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:CommandField ShowEditButton="True" EditText="تحرير" UpdateText="تعديل"/>
                                             <asp:CommandField DeleteText="إلغاء" ShowDeleteButton="True" />
                                         </Columns>
                                     </asp:GridView> 
                                 </div>
                                 </div>

                                  <div class="control-group">
                                  <label class="control-label" for="recipe_totalcost">اجمالي التكلفة</label>
					            <div class="controls">
                                    <label class="radio inline">
                                        <asp:TextBox ID="recipe_totalcost" runat="server" Width="60px" Enabled="false"></asp:TextBox>
                                    </label>
                                     <label class="radio inline">
                                         سعر البيع
                                         &nbsp &nbsp &nbsp
                                        
                                         <asp:TextBox ID="recipe_sellprice" runat="server" Width="60px"  onkeypress= "return isNumberKey(event)" onblur="doPostBack(this);"  OnTextChanged="recipe_sellprice_TextChanged"></asp:TextBox>
                                         
                                    </label>
                                    <label class="radio inline">
                                         هامش التكلفة
                                         &nbsp &nbsp &nbsp
                                         <asp:TextBox ID="recipe_costmargin" runat="server" Width="60px" onkeypress= "return isNumberKey(event)"></asp:TextBox>
                                    </label>
                                     
                                </div>
                                  
                             </div>

                                  <div class="control-group">
                                  <label class="control-label" for="recipe_target">المستهدف</label>
					            <div class="controls">
                                    <label class="radio inline">
                                        <asp:TextBox ID="recipe_target" runat="server" Width="60px" onkeypress= "return isNumberKey(event)" onblur="doPostBack(this);"  OnTextChanged="recipe_target_TextChanged"></asp:TextBox>
                                    </label>
                                     <label class="radio inline">
                                         الفرق
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
                             <div style="padding-top:10px" dir="rtl">
                              <asp:GridView ID="GridView_Recipes" runat="server" AutoGenerateColumns="False" 
                                  CssClass="table table-striped table-bordered table-condensed" AllowPaging="True" OnPageIndexChanging="GridView_Recipes_PageIndexChanging" OnRowEditing="GridView_Recipes_RowEditing" AllowSorting="True" OnSorting="GridView_Recipes_Sorting" OnRowUpdating="GridView_Recipes_RowUpdating">

                                  <Columns>
                                      <asp:TemplateField HeaderText="ID" Visible="False" >
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipeID" runat="server" Text='<%# Bind("Recipe_ID") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="تصنيف">
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipecatname" runat="server" Text='<%# Bind("RecipeCat_NameEn") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                       <asp:TemplateField HeaderText="الاسم">
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipenameAr" runat="server" Text='<%# Bind("Recipe_NameAr") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="">
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipename" runat="server" Text='<%# Bind("Recipe_NameEn") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      
                                      <asp:TemplateField HeaderText="اجمالي التكلفة" SortExpression="Recipe_TotalCost">
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipecost" runat="server" Text='<%# Bind("Recipe_TotalCost") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="سعر البيع" SortExpression="Recipe_SellPrice">
                                          <ItemTemplate>
                                              <asp:Label ID="LblRecipeprice" runat="server" Text='<%# Bind("Recipe_SellPrice") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:CommandField ShowEditButton="True" ShowCancelButton="False" UpdateText="تعديل" EditText="تعديل"/>
                                  </Columns>
                              </asp:GridView> 
                              </div>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                  </div>     
                  <div class="tab-pane fade" id="howtodo">
                      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                          <ContentTemplate>
                        <div class="form-horizontal row-fluid" dir="rtl">
                             <div class="control-group">
					        <div class="controls">	     
                                <asp:LinkButton ID="howtodo_new" runat="server" CssClass="btn btn-large" OnClick="howtodo_new_Click">جديد <i class="icon-file shaded"></i></asp:LinkButton>   
                                  <asp:LinkButton ID="howtodo_save" runat="server" CssClass="btn btn-large" OnClick="howtodo_save_Click">حفظ <i class="icon-save shaded"></i></asp:LinkButton>
                            </div>
				          </div>
                             <div class="control-group">
					        <label class="control-label" for="howtodo_reccat">تصنيف</label>
					        <div class="controls">
                                <asp:DropDownList ID="howtodo_reccat" runat="server" CssClass="span8" AutoPostBack="true" OnSelectedIndexChanged="howtodo_reccat_SelectedIndexChanged"></asp:DropDownList>
						        <span class="help-inline"></span>
					        </div>
				           </div>

                             <div class="control-group">
					        <label class="control-label" for="howtodo_rec">الوصفة</label>
					        <div class="controls">
                                <asp:DropDownList ID="howtodo_rec" runat="server" CssClass="span8" AutoPostBack="true" OnSelectedIndexChanged="howtodo_rec_SelectedIndexChanged"></asp:DropDownList>
						        <span class="help-inline"></span>
					        </div>
				           </div>

                            <div class="control-group">
					         <label class="control-label" for="txtEditor">كيفية عمل الوصفة</label>
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

