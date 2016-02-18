<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="addRol.aspx.cs" Inherits="Proyecto1_Tel.Code.addRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 50px;
        }
        .auto-style2 {
            height: 50px;
            width: 130px;
        }
        .auto-style3 {
            width: 130px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form_AddRol" runat="server">
    <h1>Agregar Rol</h1>
    
        <div allign="center" >
            
        <table  class="auto-style3" >
        <tr>
            <td style="text-align:center; font-family: Calibri; font-size: 20px; font-style: inherit; color: #000080;" class="auto-style2">Nombre Rol</td>
            <td class="auto-style1"><asp:TextBox class="form-control" placeholder="Nombre" aria-describedby="basic-addon1" ID="Nombre_Rol" runat="server" Font-Size="Medium"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align:right" class="auto-style3">
                <asp:Button ID="Agregar" class="btn btn-default navbar-btn" runat="server" Text="Agregar" Font-Bold="True" Font-Size="Medium" OnClick="Agregar_Click" />
            </td>
            <td style="text-align:right">
                <asp:Button ID="Cancelar" class="btn btn-default navbar-btn" runat="server" Text="Regresar" Font-Bold="True" Font-Size="Medium" OnClick="Cancelar_Click" />
            </td>
        
        </tr>
    </table>
    </div>
        
    </form>

</asp:Content>
