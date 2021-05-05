<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="addPatient.aspx.cs" Inherits="WebApplication.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">

    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<!------ Include the above in your HEAD tag ---------->
    
<div class="wrapper fadeInDown">
  <div id="formContent">
    <!-- Tabs Titles -->
           <link href="../style/login.css" rel="stylesheet" />
    <!-- Icon -->
    <div class="fadeIn first">

      <img src="../assets/img/Gato.jpg" id="icon" style="margin-top:50px;margin-bottom:50px;"alt="User Icon" />
                <h1>ADD NEW USER</h1>
    </div>

    <!-- Login Form -->
   
   <form id="addPatient" runat="server">
       <p class="center-text">New user:</p>
      <asp:TextBox id="addUsername" runat="server" placeholde="user"  />
       <p class="center-text">New password:</p>
            <asp:TextBox id="addPassword" runat="server" placeholde="password" />
            <asp:Button id="addButton" runat="server" Text="Button" OnClick="add"/>
    </form>
  </div>
</div>
</asp:Content>

