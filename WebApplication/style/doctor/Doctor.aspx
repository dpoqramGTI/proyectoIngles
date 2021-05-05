<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Doctor.aspx.cs" Inherits="WebApplication.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <div class="container-md">
            <p class="h1 text-center">Pacientes:</p>
        <a href="./addPatient.aspx">Add Patient</a>
    <!--Mostrar aqui los usuarios al seleccionar uno de ellos te envia a su ubicación con sus datos-->
    <table class="table">
  <thead>
    <tr>
      <th scope="col">#</th>
      <th scope="col">First</th>
      <th scope="col">Last</th>
      <th scope="col">Handle</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th scope="row">1</th>
      <td><a href="./datoUsuario.aspx">Pepe</a></td>
      <td>Otto</td>
      <td>@mdo</td>
    </tr>
    <tr>
      <th scope="row">2</th>
      <td>Jacob</td>
      <td>Thornton</td>
      <td>@fat</td>
    </tr>
    <tr>
      <th scope="row">3</th>
      <td>Larry</td>
      <td>the Bird</td>
      <td>@twitter</td>
    </tr>
  </tbody>
</table>
        </div>
</asp:Content>
