<%@ Page Title="Setting" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Setting.aspx.cs" Inherits="Setting" %>
<asp:Content ID="Content2" ContentPlaceHolderID="LangContent" runat="server">
     <li class="dropdown">
         <a href="Setting.aspx" class="dropdown-toggle" data-toggle="dropdown">English
           <b class="caret"></b></a>
           <ul class="dropdown-menu">
               <li><a href="ar/Setting.aspx">عربي</a></li>
                <li><a href="Setting.aspx">English</a></li>
           </ul>
     </li>
 </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
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
                  <li class="active"><a href="#user" data-toggle="tab">Account setting</a></li>            
              </ul>
              <div class="profile-tab-content tab-content">

                  <div class="tab-pane fade active in" id="user">   
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                          <ContentTemplate>                         
                       <div class="form-horizontal row-fluid">
                        <div class="control-group">
					         <div class="controls">
                                <asp:LinkButton ID="user_new" runat="server" CssClass="btn btn-large" OnClick="user_new_Click">New <i class="icon-file shaded"></i></asp:LinkButton>
                                <asp:LinkButton ID="user_save" runat="server" CssClass="btn btn-large" OnClick="user_save_Click" ValidationGroup="u">Save <i class="icon-save shaded"></i></asp:LinkButton>		            
					         </div>
				        </div>
                       <div class="control-group">
  	
                          
					        <label class="control-label" for="user_name">User Name</label>
					        <div class="controls">	  
                               <asp:TextBox ID="user_name" runat="server" Width="160px" CssClass="span8" ValidationGroup="u"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter user name" ControlToValidate="user_name" Text="*" ForeColor="Red" ValidationGroup="u"></asp:RequiredFieldValidator>    
					        </div>
				        
                           </div>
                           <div class="control-group">
                               <label class="control-label" for="user_pass">Password</label>
                               <div class="controls">
                               <asp:TextBox ID="user_pass" runat="server" Width="160px" CssClass="span8" ValidationGroup="u" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="ادخل كلمة المرور" ControlToValidate="user_pass" Text="*" ForeColor="Red" ValidationGroup="u"></asp:RequiredFieldValidator>
                                   </div>
                           </div>
                           <div class="control-group">
                               <label class="control-label" for="user_repass">Re-Password</label>
                               <div class="controls">
                               <asp:TextBox ID="user_repass" runat="server" Width="160px" CssClass="span8" ValidationGroup="u" TextMode="Password"></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Password" ControlToValidate="user_repass" Text="*" ForeColor="Red" ValidationGroup="u"></asp:RequiredFieldValidator>
                                   <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password not match" ForeColor="Red" ValidationGroup="u" ControlToCompare="user_pass" ControlToValidate="user_repass"></asp:CompareValidator>
                                   </div>
                           </div>
                          <div class="control-group">
                               
								<div class="controls">
									<label class="radio inline">
                                        <asp:RadioButton ID="Radio_admin" runat="server" GroupName="roles" Text="Adminstrator" OnClick="checkAll(this)"/>
									</label> 
									<label class="radio inline">
                                        <asp:RadioButton ID="Radio_person" runat="server" GroupName="roles" Text="Employee" OnClick="checkperson(this)"/>					
									</label> 
								</div>
						</div>  
                           <div class="control-group">		
                              							
									<div class="controls">
                                        <asp:Repeater ID="Rpt_forms" runat="server">
                                            <ItemTemplate>                                                    
										<label class="checkbox">
                                            <asp:CheckBox ID="CheckBox1" runat="server" Text='<%#Eval("Form_name") %>' ToolTip='<%#Eval("Form_ID") %>'/>
										</label>	
                                            </ItemTemplate>
                                        </asp:Repeater>	
									</div>											
						   </div>

                           <div class="control-group">
                               <label class="control-label" for="Radio_ar">Language</label>
								<div class="controls">
                                <label class="radio inline">
                                        <asp:RadioButton ID="Radio_ar" runat="server" GroupName="lang" Text="Arabic" Checked="true"/>
									</label> 
									<label class="radio inline">
                                        <asp:RadioButton ID="Radio_en" runat="server" GroupName="lang" Text="English"/>					
									</label> 
                                </div>
                           </div>
                       </div>
                       <br />

                              <asp:GridView ID="GridView_users" runat="server" 
                                  CssClass="table table-striped table-bordered table-condensed" 
                                  AllowPaging="True" AutoGenerateColumns="False" 
                                  OnPageIndexChanging="GridView_users_PageIndexChanging" 
                                  Width="50%" OnSelectedIndexChanging="GridView_users_SelectedIndexChanging">
                               <Columns>
                                   <asp:TemplateField HeaderText="">
                                       <ItemTemplate>
                                           <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("User_ID")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="User name">
                                       <ItemTemplate>
                                           <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("User_name")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Password">
                                       <ItemTemplate>
                                           <asp:Label ID="lblPass" runat="server" Text='<%# Eval("User_pass")%>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Lang">
                                       <ItemTemplate>
                                           <asp:Label ID="lbllang" runat="server" Text='<%# Bind("lang") %>'></asp:Label>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField>
                                       <ItemTemplate>
                                           <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument='<%# Eval("User_ID")%>' OnClientClick="return confirm('Are you sure to delete?')" OnClick="DeleteUser">Delete</asp:LinkButton>

                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
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

     <script type="text/javascript" src="Scripts/external/jquery/jquery.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui.js"></script>
    <script type="text/javascript">
        
        function checkAll(cb) {
            var ctrls = document.getElementsByTagName('input');
            for (var i = 0; i < ctrls.length; i++) {
                var cbox = ctrls[i];
                if (cbox.type == "checkbox") {
                    cbox.checked = cb.checked;
                }
            }
        }
        function checkperson(cb) {
            var ctrls = document.getElementsByTagName('input');
            for (var i = 0; i < ctrls.length; i++) {
                var cbox = ctrls[i];
                if (cbox.type == "checkbox") {
                    cbox.checked = !cb.checked;
                }
            }
        }

        /*
        var clicked = false;
        $("#").on("click", function () {
            
            $("#Checkbox1").prop("checked", !clicked);
            clicked = !clicked;
        });
        */
    function GetRadioButtonSelectedValue() {
        var AspRadio = document.getElementById('<%= Radio_admin.ClientID %>');
 
        if (AspRadio.checked) {
            document.getElementById('<%= user_name.ClientID %>').value = 'go';
            }  
    }
  </script>
</asp:Content>

