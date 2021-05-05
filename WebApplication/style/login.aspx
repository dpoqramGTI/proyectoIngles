<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebApplication.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<!------ Include the above in your HEAD tag ---------->
    
<div class="wrapper fadeInDown">
  <div id="formContent">
    <!-- Tabs Titles -->
           <link href="style/login.css" rel="stylesheet" />
    <!-- Icon -->
    <div class="fadeIn first">

      <img src="assets/img/Gato.jpg" id="icon" alt="User Icon" />
                <h1>MewMew Hospital</h1>
    </div>

    <!-- Login Form -->
   <form id="form1" runat="server" name="form1">
        <div>
            <asp:TextBox id="username" runat="server" />
            <asp:TextBox id="password" runat="server" />
            <asp:Button id="loginButton" runat="server" Text="Button" OnClick="createLogin"/>
        </div>
    </form>

    <!-- Remind Passowrd -->
    <div id="formFooter">
      <a class="underlineHover" href="#">Forgot Password?</a>
    </div>

  </div>
</div>
</asp:Content>
