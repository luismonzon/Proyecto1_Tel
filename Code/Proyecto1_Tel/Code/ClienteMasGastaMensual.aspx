<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="ClienteMasGastaMensual.aspx.cs" Inherits="Proyecto1_Tel.Code.ClienteMasGastaMensual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <li><a href="MasVendidoMes.aspx">Clientes Que Mas Compran - Mes</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/>
        <div id="modaldetalle" runat="server">
        
        </div>


	<h4 class="widget-name"><i class="icon-columns"></i>Mas Vendido - Mes</h4>

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
    
    <input runat="server" type="text" required="required" readonly="readonly" id="Text1" name="codigo"  style="visibility:hidden; height:5px;"/>
    <div id="modalprodcliente" runat="server">
        
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
                    url: 'ClienteMasGastaMensual.aspx/GenerarTabla',
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

        function VerProductos(id)
        {
            var str = $('#myDate').val();

            if (str != "") {
                var prueba = str.toString();
                var sp = prueba.split("-");
                var mes = parseInt(sp[1]);
                var anio = parseInt(sp[0]);

                document.getElementById("<% = codigo.ClientID%>").value = id;
                var identi = document.getElementById("<% = codigo.ClientID%>").value;
                $.ajax({
                    type: 'POST',
                    url: 'ClienteMasGastaMensual.aspx/Mostrar',
                    data: JSON.stringify({ id: id, mes: mes, anio: anio }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        var $modal = $('#ContentPlaceHolder1_modalprodcliente');
                        $modal.html(response.d);
                        $('#modal-pago').on('show.bs.modal', function () {
                            $('.modal .modal-body').css('overflow-y', 'auto');
                            $('.modal .modal-body').css('max-height', $(window).height() * 0.6);
                            $('.modal .modal-body').css('height', $(window).height() * 0.6);
                            $('.modal .modal-content').css('height', '450px');
                            $('.modal .modal-content').css('overflow', 'auto');
                        });
                        $('#modal-pago').modal({ //
                            show: true, //mostramos el modal registra producto
                            backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                        });

                        //$('#modal-pago').modal('hide').data('bs.modal', null);
                    }
                });
            }
        }
        

    </script>
</asp:Content>
