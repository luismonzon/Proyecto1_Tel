<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="VentaMensual.aspx.cs" Inherits="Proyecto1_Tel.Code.VentaMensual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <li><a href="VentaMensual.aspx">Venta Mensual</a></li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/>
        <div id="modaldetalle" runat="server">
        
        </div>


	<h4 class="widget-name"><i class="icon-columns"></i>Venta Mensual</h4>

        <div class="widget">
	        <div class="navbar">
	            <div class="navbar-inner">
	                <h6>Venta Mensual</h6>

		                <input type="month" id="myDate" required="required"/>
                        <button type="button" class="btn btn-success" onclick="GenerarFecha();" id="Ver Productos">Ver</button>
                    
	            </div>
	        </div>
            <div id="tabla-productos" class="table-overflow">
            </div>
 	    </div>



</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">

    <script type="text/javascript">

        function GenerarFecha() {

            var str = $('#myDate').val();

            
            if (str != "") {
                

                var prueba = str.toString();
                var sp = prueba.split("-");
                var mes = parseInt(sp[1]);
                var anio = parseInt(sp[0]);

                $.ajax({
                    type: 'POST',
                    url: 'VentaMensual.aspx/GenerarTabla',
                    data: JSON.stringify({ mes: mes, anio: anio }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        var $modal = $('#tabla-productos');
                        $modal.html(response.d);
                    }
                });

            }
            return false;
        }

    </script>

</asp:Content>
