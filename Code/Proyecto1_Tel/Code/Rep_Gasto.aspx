<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Rep_Gasto.aspx.cs" Inherits="Proyecto1_Tel.Code.Rep_Gasto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--- URL DE LA PAGINA --->
    <li><a href="Rep_Gasto.aspx">Gasto Diario</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
	<h4 class="widget-name"><i class="icon-columns"></i>Gasto Diario</h4>

        <div class="widget">
	        <div class="navbar">
                <div id="selectU" runat="server"></div>
                    
	            <div class="navbar-inner">
	                <h6>Gasto Diario</h6>

	                <div class="pick-a-date no-append" id="navdatepicker">
                        
		                <input type="text" class="datepicker" id="myDate" required="required"/>
                        <button type="button" class="btn btn-success" onclick="VerTabla();" id="Ver Productos">Ver</button>
	                </div>
                    
	            </div>
	        </div>
            <div id="tabla-gastos" class="table-overflow">
            </div>
 	    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">

    <script type="text/javascript">

        
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
                    url: 'Rep_Gasto.aspx/GenerarTabla',
                    data: JSON.stringify({ fecha: fecha}),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        var $modal = $('#tabla-gastos');
                        $modal.html(response.d);
                    }
                });

            }
            return false;
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

            VerTabla();
        }

    </script>


</asp:Content>
