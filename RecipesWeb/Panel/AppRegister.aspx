<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppRegister.aspx.cs" Inherits="Panel_AppRegister" %>

<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>Registration | Recipes App</title>
   <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
 <!-- Bootstrap 3.3.5 -->
  <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
  <!-- iCheck -->
  <link rel="stylesheet" href="plugins/iCheck/square/blue.css">

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

    <style>
        .reg-lang{text-align:center;}
        .lang-control {
            width: 100px;
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
        }
    </style>
</head>
<body class="hold-transition register-page">

    <div class="register-box">
            <div class="register-logo">
                <img src="../images/recipes.png" alt="" />
                <br />
                <a href="#"><b>Recipes </b>Management System</a>
            </div>
            <form id="form1" runat="server">
             <div class="register-box-body">
                     <p id="msg" runat="server" class="login-box-msg" style="color: #FF0000"></p>
                     <div class="form-group has-feedback">
                        <input id="text_email" runat="server" type="email" class="form-control" placeholder="Email" required="required">
                        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                     </div>
                     <div class="form-group has-feedback"> 
                       <asp:DropDownList ID="ddl_type" runat="server" CssClass="form-control">
                           <asp:ListItem Value="1">Small company</asp:ListItem>
                           <asp:ListItem Value="2">Medium company</asp:ListItem>
                           <asp:ListItem Value="3">Large company</asp:ListItem>
                       </asp:DropDownList>
                     </div>   
                     <div class="form-group has-feedback">
                        <input id="text_companyname" runat="server" type="text" class="form-control" placeholder="Company name" required="required">
                        <span class="glyphicon glyphicon-briefcase form-control-feedback"></span>
                     </div>       
                     <div class="row">
                     <div class="col-xs-8">
                      <div class="checkbox icheck">
                        <label>
                          <asp:CheckBox ID="chk_terms" runat="server" /> I agree to the <a href="#">terms</a>             
                        </label>
                      </div>
                    </div>
                    <!-- /.col -->
                    <div class="col-xs-4">
                        <asp:Button ID="btn_register" runat="server" Text="Register" CssClass="btn btn-primary btn-block btn-flat" OnClick="btn_register_Click"/>
                    </div>
                    <!-- /.col -->
                    </div>
                     <div class="social-auth-links text-center">
                    <p>- OR -</p>
                    <a href="#" class="btn btn-block btn-social btn-facebook btn-flat">
                    Take a quick tour in Application</a>
                </div>
            </div>
            </form>
    </div>
<script src="bower_components/jquery/dist/jquery.min.js"></script>
<script src="bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<script src="plugins/iCheck/icheck.min.js"></script>
<script>
  $(function () {
    $('input').iCheck({
      checkboxClass: 'icheckbox_square-blue',
      radioClass: 'iradio_square-blue',
      increaseArea: '20%' 
    });
  });
</script>
</body>
</html>
