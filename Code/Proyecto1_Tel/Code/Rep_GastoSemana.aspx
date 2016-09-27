﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Rep_GastoSemana.aspx.cs" Inherits="Proyecto1_Tel.Code.Rep_GastoSemana" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

     <!--- URL DE LA PAGINA --->
    <li><a href="Rep_GastoSeana.aspx">Gasto Semanal</a></li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
	<h4 class="widget-name"><i class="icon-columns"></i>Gasto Semana</h4>

        <div class="widget">
	        <div class="navbar">
                <div id="selectU" runat="server"></div>
                    
	            <div class="navbar-inner">
	                <h6>Gasto Diario</h6>

	                <div class="pick-a-date no-append" id="navdatepicker">
                        
		                <input type="text" class="datepicker" id="myDate" required="required"/>
                        <button type="button" class="btn btn-success" onclick="GenerarFecha();" id="Ver Productos">Ver</button>
	                </div>
                    
	            </div>
	        </div>
            <div id="tabla-gastos" class="table-overflow">
            </div>
 	    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">

    <script type="text/javascript">

        function GenerarFecha(){
            
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
                    url: 'Rep_GastoSemana.aspx/GenerarTabla',
                    data: JSON.stringify({ start: inicio, end: fin }),
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

        function dateFromDay(year, day) {
            var date = new Date(year, 0); // initialize a date in `year-01-01`
            return new Date(date.setDate(day)); // add the number of days
        }

        </script>
</asp:Content>