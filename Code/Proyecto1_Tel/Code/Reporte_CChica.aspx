<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Reporte_CChica.aspx.cs" Inherits="Proyecto1_Tel.Code.Reporte_CChica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
      <!--- URL DE LA PAGINA --->
    <li><a href="VentaDiaria.aspx">Venta Diaria</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/>
        <div id="modaldetalle" runat="server" >
        
        </div>


	<h4 class="widget-name"><i class="icon-columns"></i>Caja Chica</h4>

        <div class="widget">
	        <div class="navbar">
                <div id="selectU" runat="server"></div>
                    
	            <div class="navbar-inner">
	                <h6>Caja Chica</h6>

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
                var id = document.getElementById("usuarios").value;
                $.ajax({
                    type: 'POST',
                    url: 'Reporte_CChica.aspx/GenerarTabla',
                    data: JSON.stringify({ fecha: fecha, tipo: id }),
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


        

        function Delete(id) {
            if (confirm("Esta seguro que desea eliminar esta transaccion?")) {
                $.ajax({
                    type: "POST",
                    url: "Reporte_CChica.aspx/Delete",
                    data: JSON.stringify({ id: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var str = $('#myDate').val();

                        if (response.d == true) {
                            alert("La Transaccion Ha Sido Eliminada Exitosamente");
                            var $modal = $('#ContentPlaceHolder1_modaldetalle');
                            $modal.html("");
                            VerTabla();
                            //reloadTable();
                        } else {
                            alert("La Transaccion No Pudo Ser Eliminada");
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

            VerTabla();
        }

    </script>
</asp:Content>
