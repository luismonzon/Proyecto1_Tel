<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Depositos.aspx.cs" Inherits="Proyecto1_Tel.Code.Depositos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--- URL DE LA PAGINA --->
    <li><a href="Depositos.aspx">Depositos</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/>
    <div id="modaldetalle" runat="server" >
    </div>

	<h4 class="widget-name"><i class="icon-columns"></i>Depositos</h4>
    <div class="widget">
	    <div class="navbar">
	        <div class="navbar-inner">
	            <h6>Venta Diaria</h6>

	            <div class="pick-a-date no-append" id="navdatepicker">
		            <input type="text" class="datepicker" id="myDate" required="required"/>
                    <button type="button" class="btn btn-success" onclick="VerTabla();" id="verTabla">Ver</button>
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
                    url: 'Depositos.aspx/GenerarTabla',
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
                url: 'Depositos.aspx/ModalDetalle',
                data: JSON.stringify({ id: identi }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var $modal = $('#ContentPlaceHolder1_modaldetalle');
                    $modal.html(response.d);
                    $('#modal-detalle').on('show.bs.modal', function () {
                        $('.modal .modal-body').css('overflow-y', 'auto');
                        $('.modal .modal-body').css('max-height', $(window).height() * 0.7);
                        $('.modal .modal-body').css('height', $(window).height() * 0.7);
                        $('.modal .modal-content').css('height', '500px');
                        $('.modal .modal-content').css('overflow', 'auto');
                    });


                    $('#modal-detalle').modal({ //
                        show: true, //mostramos el modal registra producto
                        backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                    });
                    //$('#modal-pago').modal('hide').data('bs.modal', null);
                }
            });
        }

        function Delete(id) {
            if (confirm("Esta seguro que desea eliminar la Venta?")) {
                $.ajax({
                    type: "POST",
                    url: "VentaDiaria.aspx/Delete",
                    data: JSON.stringify({ id: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var str = $('#myDate').val();

                        if (response.d == true) {
                            alert("La Venta Ha Sido Eliminada Exitosamente");
                            var $modal = $('#ContentPlaceHolder1_modaldetalle');
                            $modal.html("");
                            VerTabla();
                            //reloadTable();
                        } else {
                            alert("La Venta No Pudo Ser Eliminada");
                        }

                    }
                });
            }
        }

        window.onload = function () {


            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }

            today = dd + '-' + mm + '-' + yyyy;
            $("#myDate").val(today);
        }

    </script>

</asp:Content>
