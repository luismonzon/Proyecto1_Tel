<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="ClienteMasGasta.aspx.cs" Inherits="Proyecto1_Tel.Code.ClienteMasGasta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <li><a href="ClienteMasGasta.aspx">Clientes Que Mas Gastan</a></li>    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h5 class="widget-name"><i class="icon-columns"></i>Clientes</h5>
    <!-- Some controlы -->
    <div class="widget" id="tab_roles" runat="server">
    </div>
    <!-- /some controlы -->

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
