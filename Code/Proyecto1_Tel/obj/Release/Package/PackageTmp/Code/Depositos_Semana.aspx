<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Depositos_Semana.aspx.cs" Inherits="Proyecto1_Tel.Code.Depositos_Semana" %>
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
                var d = new Date();
                d.setFullYear(anio, mes - 1, dia);

                var start = new Date(d.getFullYear(), 0);
                var diff = d - start;
                var oneDay = 1000 * 60 * 60 * 24;
                var day = Math.ceil(diff / oneDay);

                var startdate = dateFromDay(d.getFullYear(), day - d.getDay());
                var enddate = dateFromDay(d.getFullYear(), day + (6 - d.getDay()));


                var dia1 = startdate.getDate().toString();
                var m1 = startdate.getMonth() + 1;
                var mes1 = m1.toString();
                var anio1 = startdate.getFullYear().toString();

                if (mes1.length == 1) { mes1 = "0" + mes1; }
                if (dia1.length == 1) { dia1 = "0" + dia1; }
                var inicio = anio1 + mes1 + dia1;
                var dia2 = enddate.getDate().toString();
                var m2 = enddate.getMonth() + 1;
                var mes2 = m2.toString();
                if (mes2.length == 1) { mes2 = "0" + mes2; }
                if (dia2.length == 1) { dia2 = "0" + dia2; }
                var anio2 = enddate.getFullYear().toString();


                var fin = anio2 + mes2 + dia2;


                $.ajax({
                    type: 'POST',
                    url: 'Depositos_Semana.aspx/GenerarTabla',
                    data: JSON.stringify({ start:inicio, end:fin }),
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
            
            var str = $('#myDate').val();

            if (str != "") {
                var prueba = str.toString();
                var sp = prueba.split("-");
                var dia = sp[0];
                var mes = sp[1];
                var anio = sp[2];

                var fecha = anio + mes + dia;
                var d = new Date();
                d.setFullYear(anio, mes - 1, dia);

                var start = new Date(d.getFullYear(), 0);
                var diff = d - start;
                var oneDay = 1000 * 60 * 60 * 24;
                var day = Math.ceil(diff / oneDay);

                var startdate = dateFromDay(d.getFullYear(), day - d.getDay());
                var enddate = dateFromDay(d.getFullYear(), day + (6 - d.getDay()));


                var dia1 = startdate.getDate().toString();
                var m1 = startdate.getMonth() + 1;
                var mes1 = m1.toString();
                var anio1 = startdate.getFullYear().toString();

                if (mes1.length == 1) { mes1 = "0" + mes1; }
                if (dia1.length == 1) { dia1 = "0" + dia1; }
                var inicio = anio1 + mes1 + dia1;
                var dia2 = enddate.getDate().toString();
                var m2 = enddate.getMonth() + 1;
                var mes2 = m2.toString();
                if (mes2.length == 1) { mes2 = "0" + mes2; }
                if (dia2.length == 1) { dia2 = "0" + dia2; }
                var anio2 = enddate.getFullYear().toString();


                var fin = anio2 + mes2 + dia2;

                $.ajax({
                    type: 'POST',
                    url: 'Depositos_Semana.aspx/Mostrar',
                    data: JSON.stringify({ id: id, start: inicio, end: fin }),
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


        function dateFromDay(year, day) {
            var date = new Date(year, 0); // initialize a date in `year-01-01`
            return new Date(date.setDate(day)); // add the number of days
        }


    </script>

</asp:Content>
