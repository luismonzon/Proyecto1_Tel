<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="ProductosInventario.aspx.cs" Inherits="Proyecto1_Tel.Code.ProductosInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <li><a href="ProductosInventario.aspx">Productos en el Inventario</a></li>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h4 class="widget-name"><i class="icon-columns"></i>Inventario</h4>
        <!-- Some controlы -->
        <div class="widget" id="tab_roles" runat="server">
        </div>
        <!-- /some controlы -->


    <div>
        <h1 style="font-family: Calibri; font-size: 50px"></h1>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
