<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication.doctor.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <a href="./addPatient.aspx">Add Patient</a>

     <table class="table">
            <thead>
                <tr>
                    <th scope="col">Pacient code</th>
                    <th scope="col">Name</th>
                    <th scope="col">Dni</th>
                </tr>
            </thead>
            <tbody runat="server" id="tbodydoctor">
            </tbody>
        </table>
</asp:Content>