
<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="VentaDiaria.aspx.cs" Inherits="Proyecto1_Tel.Code.VentaDiaria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script>
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#datepicker").datepicker({
                firstDay: 1
            });
        });
</script>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--- URL DE LA PAGINA --->
    <li><a href="VentaDiaria.aspx">Venta Diaria</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<h5 class="widget-name"><i class="icon-columns"></i>Venta Diaria</h5>
    


        <div class="widget">
	        <div class="navbar">
	            <div class="navbar-inner">
	                <h6>Venta Diaria</h6>
	                <div class="pick-a-date no-append">
	                    <input type="text"  placeholder="select date..." id="datepicker"/>
	                </div>
	            </div>
	        </div>
	    </div>
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
