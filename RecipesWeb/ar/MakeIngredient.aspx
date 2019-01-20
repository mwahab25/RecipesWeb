<%@ Page Title="الباتشات" Language="C#" MasterPageFile="~/ar/Site.master" AutoEventWireup="true" CodeFile="MakeIngredient.aspx.cs" Inherits="ar_MakeIngredient" %>
<asp:Content ID="Content2" ContentPlaceHolderID="LangContent" runat="server">
     <li class="dropdown"><a href="MakeIngredient.aspx" class="dropdown-toggle" data-toggle="dropdown">عربي
            <b class="caret"></b></a>
            <ul class="dropdown-menu">
                <li><a href="../MakeIngredient.aspx">English</a></li>
                <li><a href="MakeIngredient.aspx">عربي</a></li>
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
             <ul class="profile-tab nav nav-tabs">
                  <li class="active"><a href="#batch" data-toggle="tab">تصنيع الخامات</a></li>        
              </ul>
             <div class="profile-tab-content tab-content">
                 <div class="tab-pane fade active in" id="batch">
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                         <ContentTemplate>
                             <div class="form-horizontal row-fluid" dir="rtl">
                              <div class="control-group">
					            <div class="controls">
                                    <asp:LinkButton ID="batch_new" runat="server" CssClass="btn btn-large" OnClick="batch_new_Click">جديد <i class="icon-file shaded"></i></asp:LinkButton>
                                    <asp:LinkButton ID="batch_save" runat="server" CssClass="btn btn-large" OnClick="batch_save_Click">حفظ <i class="icon-save shaded"></i></asp:LinkButton>
                                    <asp:LinkButton ID="batch_update" runat="server" CssClass="btn btn-large" Visible="false" OnClick="batch_update_Click">تعديل <i class="icon-edit shaded"></i></asp:LinkButton>						
                                    <asp:LinkButton ID="batch_delete" runat="server" CssClass="btn btn-large" Visible="false" OnClientClick = "return confirm('لا يمكن الحذف في حالة الاستخدام')" OnClick="batch_delete_Click">حذف <i class="icon-cut shaded"></i></asp:LinkButton>   
                                    <asp:LinkButton ID="batch_print" runat="server" CssClass="btn btn-large" OnClick="batch_print_Click">طباعة <i class="icon-print shaded"></i></asp:LinkButton>                                  
                                </div>
				            </div>
                                 <div class="control-group">
					        <label class="control-label" for="batch_no">رقم الباتش</label>
					        <div class="controls">						        
                                <asp:TextBox ID="batch_no" runat="server" CssClass="span8" Width="160px" Enabled="false"></asp:TextBox>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                                 <div class="control-group">
					        <label class="control-label" for="batch_nameAr">اسم الباتش</label>
					        <div class="controls">						        
                                <asp:TextBox ID="batch_nameAr" runat="server" CssClass="span8"></asp:TextBox>
                                <asp:LinkButton ID="batchnameAr_search" runat="server" CssClass="btn btn-small" OnClick="batchnameAr_search_Click" ><i class="icon-search shaded"></i></asp:LinkButton>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                                   <div class="control-group">
					        <label class="control-label" for="batch_name">اسم(English)</label>
					        <div class="controls">						        
                                <asp:TextBox ID="batch_name" runat="server" CssClass="span8"></asp:TextBox>
                                <asp:LinkButton ID="batchname_search" runat="server" CssClass="btn btn-small" OnClick="batchname_search_Click" ><i class="icon-search shaded"></i></asp:LinkButton>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                                 <div class="control-group">
					        <label class="control-label" for="batch_price">السعر</label>
					        <div class="controls">                                
                                <asp:TextBox ID="batch_price" runat="server" CssClass="span8" Width="160px" onkeypress= "return isNumberKey(event)"></asp:TextBox>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                             <div class="control-group">
					        <label class="control-label" for="batch_unit">الوحدة</label>
					        <div class="controls">
                                <asp:DropDownList ID="batch_unit" runat="server" CssClass="span8" Width="160px"></asp:DropDownList>
						        <span class="help-inline"></span>
					        </div>
				           </div>
                                 <div class="control-group" style="background-color:azure">
                                 <label class="control-label" for="batch_batchcat">تصنيف</label>
                                 <div class="controls" style="padding-top:5px">       
                                 <asp:DropDownList ID="batch_batchcat" runat="server" CssClass="span8" AutoPostBack="True" OnSelectedIndexChanged="batch_batchcat_SelectedIndexChanged"></asp:DropDownList>
                                 </div>
                                 <label class="control-label" for="batch_ingredname">الصنف</label>
                                 <div class="controls" style="padding-top:5px"> 
                                     <asp:DropDownList ID="batch_ingredname" runat="server" CssClass="span8"></asp:DropDownList>
                                 </div>
                                 <label class="control-label" for="batch_ingredqty">الكمية</label>
                                 <div class="controls" style="padding-top:5px">                                     
                                         <asp:TextBox ID="batch_ingredqty" runat="server" Width="60px" TextMode="Number"></asp:TextBox>                
                                 </div>

                                 <label class="control-label" for="batch_additem"></label>
                                 <div class="controls" style="padding-top:5px">                                                                           
                                     <asp:LinkButton ID="batch_additem" runat="server" CssClass="btn btn-small" OnClick="batch_additem_Click"><i class="icon-plus shaded"></i></asp:LinkButton>               
                              
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
                                             <asp:CommandField DeleteText="حذف" ShowDeleteButton="True" />
                                         </Columns>
                                     </asp:GridView> 
                                 </div>
                                 </div>

                                  <div class="control-group">
                                  <label class="control-label" for="recipe_totalcost">اجمالي التكلفة</label>
					            <div class="controls">
                                    <label class="radio inline">
                                        <asp:TextBox ID="batch_totalcost" runat="server" Width="60px" Enabled="false"></asp:TextBox>
                                    </label>     
                                </div>
                                  
                             </div>
                                 
                             </div>
                              <br />
                             <div dir="rtl">
                              <asp:GridView ID="GridView_Batchs" runat="server" AutoGenerateColumns="False" 
                                  CssClass="table table-striped table-bordered table-condensed" AllowPaging="True" OnPageIndexChanging="GridView_Batchs_PageIndexChanging" OnRowEditing="GridView_Batchs_RowEditing" AllowSorting="True" OnSorting="GridView_Batchs_Sorting" OnRowUpdating="GridView_Batchs_RowUpdating">

                                  <Columns>
                                      <asp:TemplateField HeaderText="ID" Visible="False" >
                                          <ItemTemplate>
                                              <asp:Label ID="LblBatchID" runat="server" Text='<%# Bind("Batch_ID") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="الاســم">
                                          <ItemTemplate>
                                              <asp:Label ID="LblBatchnameAr" runat="server" Text='<%# Bind("Batch_NameAr") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="">
                                          <ItemTemplate>
                                              <asp:Label ID="LblBatchname" runat="server" Text='<%# Bind("Batch_NameEn") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="السعر" SortExpression="Price">
                                          <ItemTemplate>
                                              <asp:Label ID="LblIngredprice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="الوحدة">
                                          <ItemTemplate>
                                              <asp:Label ID="LblBatchunit" runat="server" Text='<%# Bind("Unit_NameEn") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:CommandField ShowEditButton="True" ShowCancelButton="False" UpdateText="تحرير" EditText="تحرير"/>
                                  </Columns>
                              </asp:GridView> 
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
</asp:Content>

