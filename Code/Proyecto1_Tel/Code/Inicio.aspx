<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Proyecto1_Tel.Code.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
	    <h4 class="widget-name"><i class="icon-columns"></i>Bodega</h4>
    <div>
        <div><a style="font-size: 13px" id="nuevo-bodega" onclick="cambio();" class="btn btn-success"> Agregar Producto a Bodega <i class="icon-plus-sign" >&nbsp;</i></a></div>
    </div>
    
        <!-- Some controlы -->
        <div class="widget" id="tab_bodega" runat="server">
        </div>
        <!-- /some controlы -->



</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
