<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="ValesMensual.aspx.cs" Inherits="Proyecto1_Tel.Code.ValesMensual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <li><a href="ValesMensual.aspx">Vales</a></li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    
    <input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/>
        <div id="modaldetalle" runat="server">
        
        </div>


	<h4 class="widget-name"><i class="icon-columns"></i>Vales</h4>

        <div class="widget">
	        <div class="navbar">
                
                <div id="selectU" runat="server"></div>
	            <div class="navbar-inner">
	                <h6>Vales Del Mes De </h6>

		                <input type="month" id="myDate" required="required"/>
                        <button type="button" class="btn btn-success" onclick="GenerarFecha();" id="Ver Vales">Ver</button>
                    
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

        function GenerarFecha() {

            var str = $('#myDate').val();

            
            if (str != "") {
                

                var prueba = str.toString();
                var sp = prueba.split("-");
                var mes = parseInt(sp[1]);
                var anio = parseInt(sp[0]);

                var id = document.getElementById("usuarios").value;
                $.ajax({
                    type: 'POST',
                    url: 'ValesMensual.aspx/GenerarTabla',
                    data: JSON.stringify({ mes: mes, anio: anio, tipo: id}),
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
                url: 'ValesMensual.aspx/ModalDetalle',
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
            if (confirm("Esta seguro que desea eliminar este vale?")) {
                $.ajax({
                    type: "POST",
                    url: "VentaDiaria.aspx/Delete",
                    data: JSON.stringify({ id: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var str = $('#myDate').val();

                        if (response.d == true) {
                            alert("El Vale Ha Sido Eliminado Exitosamente");
                            var $modal = $('#ContentPlaceHolder1_modaldetalle');
                            $modal.html("");
                            VerTabla();
                            reloadTable();
                        } else {
                            alert("El Vale No Pudo Ser Eliminado");
                        }

                    }
                });
            }
        }

    </script>

</asp:Content>
