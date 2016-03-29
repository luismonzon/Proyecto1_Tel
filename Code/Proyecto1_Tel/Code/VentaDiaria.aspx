
<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="VentaDiaria.aspx.cs" Inherits="Proyecto1_Tel.Code.VentaDiaria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">

        $(document).ready(function () {

            $('.datepicker').datepicker({
                dateFormat: 'dd-mm-yy'
            });
        });
    </script>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--- URL DE LA PAGINA --->
    <li><a href="VentaDiaria.aspx">Venta Diaria</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/>
        <div id="modaldetalle" runat="server">
        
        </div>


	<h4 class="widget-name"><i class="icon-columns"></i>Venta Diaria</h4>

        <div class="widget">
	        <div class="navbar">
	            <div class="navbar-inner">
	                <h6>Venta Diaria</h6>

	                <div class="pick-a-date no-append" id="navdatepicker">
		                <input type="text" class="datepicker" id="myDate" required="required"/>
                        <button type="button" class="btn btn-success" onclick="VerTabla();" id="Ver Productos">Ver</button>
	                </div>
                    
	            </div>
	        </div>
            <div id="tabla-productos" class="table-overflow">
            </div>
 	    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
    
    <script type="text/javascript">

        function CerrarModal() {
            $('#modal-detalle').modal('toggle');
            var $modal = $('#ContentPlaceHolder1_modaldetalle');
            $modal.html("");
        }

        function VerTabla() {

            
            var str = $('#myDate').val();
            if (str != "") {
                var prueba = str.toString();
                var sp = prueba.split("-");
                var dia = sp[0];
                var mes = sp[1];
                var anio = sp[2];

                var fecha = anio + mes + dia;

                $.ajax({
                    type: 'POST',
                    url: 'VentaDiaria.aspx/GenerarTabla',
                    data: JSON.stringify({ fecha: fecha }),
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


        function VerDetalle(id) {
            document.getElementById("<% = codigo.ClientID%>").value = id;
            var identi = document.getElementById("<% = codigo.ClientID%>").value;
            $.ajax({
                type: 'POST',
                url: 'VentaDiaria.aspx/ModalDetalle',
                data: JSON.stringify({ id: identi }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var $modal = $('#ContentPlaceHolder1_modaldetalle');
                    $modal.html(response.d);
                    $('#modal-detalle').modal({ //
                        show: true, //mostramos el modal registra producto
                        backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                    });
                    //$('#modal-pago').modal('hide').data('bs.modal', null);
                }
            });
        }
    </script>

</asp:Content>
