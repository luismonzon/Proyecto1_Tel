<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Balance_Diario.aspx.cs" Inherits="Proyecto1_Tel.Code.Balance_Diario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <li><a href="Balance_Diario.aspx">Balance</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                    
			            
    <div class="row-fluid">
        <div class="span3">
            <div class="pick-a-date no-append" >
		                <input type="text" class="datepicker" id="myDate" required="required"/>
                        <button type="button" class="btn btn-success" onclick="GraficaPie();" id="vergrafica">Ver</button>
	                </div>
        
        </div>
       

        <!-- Pie chart -->
                        
                    <div class="span4">
			            	<div class="navbar">

			                	<div class="navbar-inner">

			                    	<h6>Balance Diario</h6>

			                    </div>

			                </div>
			                <div class="well body">
                        
			                	<div class="graph-standard" id="pie">


			                	</div>
			                </div>
			            </div>
			            <!-- /pie chart -->

        <div class="span4" id="tabla_balance">

            <!-- Row styling -->
                <!-- /row styling -->
        </div>
        
       

    
    
    </div>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">


    <script type="text/javascript">


        function GraficaPie() {

            var str = $('#myDate').val();
            if (str != "") {
                var prueba = str.toString();
                var sp = prueba.split("-");
                var dia = sp[0];
                var mes = sp[1];
                var anio = sp[2];

                var fecha = anio + mes + dia;

                var data = [];


                $.ajax({
                    type: 'POST',
                    url: 'Balance_Diario.aspx/GenerarTabla',
                    data: JSON.stringify({ fecha: fecha }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        var $modal = $('#tabla_balance');
                        $modal.html(response.d);
                    }
                });




            var series = 4;
            $.ajax({
                type: 'POST',
                url: 'Balance_Diario.aspx/Reporte_Diario',
                data: JSON.stringify({ fecha: fecha }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var Reporte_Diario = JSON.parse(response.d);
                    var Ventas = parseInt(Reporte_Diario[0]);
                    var Gastos = parseInt(Reporte_Diario[1]);
                    var Depositos = parseInt(Reporte_Diario[2]);
                    var Credito = parseInt(Reporte_Diario[3]);


                    data[0] = { label: "Gastos", data: Gastos }
                    data[1] = { label: "Ventas", data: Ventas }
                    data[2] = { label: "Depositos", data: Depositos }
                    data[3] = { label: "Creditos", data: Credito }

                    
                    $.plot($("#pie"), data,
                        
                        {
                            series: {
                                
                                pie: {
                                    show: true,
                                    radius: 1,
                                    
                                    label: {
                                        show: true,
                                        radius: 2 / 3,
                                        formatter: function (label, series) {
                                            
                                            return '<div style="font-size:8pt;text-align:center;padding:9px;color:white;">' + label + '<br/>' + Math.round(series.percent) + '%</div>';
                                        },
                                        threshold: 0.1
                                    }
                                }
                            },
                            legend: {
                                show: true,
                                noColumns: 1, // number of colums in legend table
                                labelFormatter: null, // fn: string -> string
                                labelBoxBorderColor: "#000", // border color for the little label boxes
                                container: null, // container (as jQuery object) to put legend in, null means default on top of graph
                                position: "ne", // position of default legend container within plot
                                margin: [5, 10], // distance from grid edge to default legend container within plot
                                backgroundColor: "#efefef", // null means auto-detect
                                backgroundOpacity: 1 // set to 0 to avoid background
                            },
                            grid: {
                                hoverable: true,
                                clickable: true
                            },
                        });

                    
                    
                }
            });
            }

        };



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
