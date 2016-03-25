<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="VentaAnual.aspx.cs" Inherits="Proyecto1_Tel.Code.VentaAnual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <li><a href="VentaAnual.aspx">Venta Anual</a></li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/>
        <div id="modaldetalle" runat="server">
        
        </div>


	<h5 class="widget-name"><i class="icon-columns"></i>Venta Anual</h5>

        <div class="widget">
	        <div class="navbar">
	            <div class="navbar-inner">
	                <h6>Venta Anual</h6>

		                <input type="text" id="myDate" required="required"/>
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
                    var tam = prueba.length;
                    if (prueba.length >= 4)
                    {
                        var anio = parseInt(prueba);
                        if (anio > 2000)
                        {
                            $.ajax({
                                type: 'POST',
                                url: 'VentaAnual.aspx/GenerarTabla',
                                data: JSON.stringify({ anio: anio }),
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                success: function (response) {
                                    var $modal = $('#tabla-productos');
                                    $modal.html(response.d);
                                }
                            });

                        }
                    }
                }
                return false;
            }

    </script>


</asp:Content>
