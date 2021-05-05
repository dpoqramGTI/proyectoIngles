<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="patient.aspx.cs" Inherits="WebApplication.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <script>

</script>
    <div class="container-md">
        <p class="h1 text-center" id="patientHeader" runat="server">Patient...</p>

        <section class="align-self-center" id="sectionCardPatient" runat="server">
            <div class="card w-25">
                <div class="card-header">
                    <form id="form1" runat="server" name="form1">
                        <div>
                            <asp:TextBox ID="patientNameTb" runat="server" />
                            <asp:TextBox ID="patientDniTb" runat="server" />
                            <asp:Button ID="editPatientBtn" runat="server" Text="Editar paciente" OnClick="editPatient" />
                            <asp:Button ID="deleteBtn" runat="server" Text="Eliminar paciente" OnClick="deletePatientdoc" />
                            <asp:Button ID="createJSON" runat="server" Text="Download on JSON" OnClick="createJson"/>

                        </div>
                        <div class="form-group" id="divAddHistoric" runat="server">
                            <p class="center-text">New Historic1:</p>
                            <asp:TextBox ID="diagnose" runat="server" placeholder="diagnose" />
                            <p class="center-text">New Historic2</p>
                            <asp:TextBox ID="treatment" runat="server" placeholder="treatment" />
                            <p class="center-text">New Historic3</p>
                            <asp:TextBox ID="date" runat="server" placeholder="date" />
                            <asp:Button ID="addHistoricBtn" runat="server" Text="New Historic" OnClick="addHistoric" />
                        </div>
                    </form>

                </div>

                <div class="card-body">
                    <p class="card-text" id="dni" runat="server"></p>
                    <a href="#" class="btn btn-primary">VER PERFIL</a>
                </div>
            </div>
            <!--Mostrar aqui los datos de usuario en caso de ser medico poder editar su información-->
        <table class="table">
            <thead>
                <tr>
                    <th scope="col">Record num</th>
                    <th scope="col">Diagnose</th>
                    <th scope="col">Treatment</th>
                    <th scope="col">Date</th>
                </tr>
            </thead>
            <tbody runat="server" id="tbody">
            </tbody>
        </table>
        </section>
        <!---------------------->
       
    </div>
</asp:Content>
