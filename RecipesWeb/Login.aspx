<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
  <title>Log in | Recipes App</title>
  <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
  <!-- Bootstrap 3.3.5 -->
  <link rel="stylesheet" href="Panel/bower_components/bootstrap/dist/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="Panel/dist/css/AdminLTE.min.css"/>
  <!-- iCheck -->
  <link rel="stylesheet" href="Panel/plugins/iCheck/square/blue.css"/>

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <style>
        .login-lang{text-align:center;}
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
<body class="hold-transition login-page"> 
<div class="login-box">
  <div class="login-logo">
      <img src="images/recipes-01.png" alt="" />
      <br />
      <a href="#"><b>Recipes </b>Management System</a>
      
  </div>
    
    <%--<div class="login-lang">
      <select id="Select1" class="lang-control">
          <option>English</option>
          <option>عربي</option>
      </select>
    </div><br />--%>
  
  <div class="login-box-body">
    <p class="login-box-msg">
        
        <asp:Label ID="msg" runat="server" Text="Sign in to start your session"></asp:Label>
    </p>

    <form id="form1" runat="server">
       
      <div class="form-group has-feedback">
        <input id="User_id" type="text" class="form-control" placeholder="company/user" runat="server"/>
          <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
        <input id="User_pass" type="password" class="form-control" placeholder="Password" runat="server"/>
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
      <div class="row">
        <div class="col-xs-8">
          <div class="checkbox icheck">
            <label>
                 <asp:CheckBox ID="chkkeepMe" runat="server" Text="Keep me signed in"/>
             
            </label>
          </div>
        </div>
        <!-- /.col -->
        <div class="col-xs-4">
            <asp:Button ID="Btn_submit" runat="server" Text="Sign In" CssClass="btn btn-primary btn-block btn-flat" OnClick="Btn_submit_Click"/>
        </div>
        <!-- /.col -->
      </div>
    </form>

   
    <!--

    <a href="#">I forgot my password</a><br>
    <a href="register.html" class="text-center">Register a new membership</a>

  </div>
   -->
</div>

    </div>
<!-- jQuery 3 -->
<script src="Panel/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src="Panel/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- iCheck -->
<script src="Panel/plugins/iCheck/icheck.min.js"></script>
<script>
  $(function () {
    $('input').iCheck({
      checkboxClass: 'icheckbox_square-blue',
      radioClass: 'iradio_square-blue',
      increaseArea: '20%' // optional
    });
  });
</script>
</body>
</html>