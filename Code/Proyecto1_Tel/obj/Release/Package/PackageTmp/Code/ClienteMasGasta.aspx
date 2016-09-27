<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="ClienteMasGasta.aspx.cs" Inherits="Proyecto1_Tel.Code.ClienteMasGasta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
            
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <li><a href="ClienteMasGasta.aspx">Clientes Que Mas Gastan</a></li>    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h4 class="widget-name"><i class="icon-columns"></i>Clientes Que Mas Compran</h4>
    <div class="widget">
	        <div class="navbar">
                <div id="selectU" runat="server"></div>
                    
	            <div class="navbar-inner">
	                <h6>Diario</h6>

	                <div class="pick-a-date no-append" id="navdatepicker">
                        
		                <input type="text" class="datepicker" id="myDate" required="required"/>
                        <button type="button" class="btn btn-success" onclick="VerTabla();" id="Ver Productos">Ver</button>
	                </div>
                    
	            </div>
	        </div>
            <div id="tabla-productos" class="table-overflow">
            </div>
 	    </div>
    <!-- Some controlы -->
    <div class="widget" id="tab_roles" runat="server">
    </div>
    <!-- /some controlы -->
    <input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/>
    <div id="modalprodcliente" runat="server">
        
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
                    url: 'ClienteMasGasta.aspx/LLenar_Tabla',
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

        function VerProductos(id)
        {
            var str = $('#myDate').val();
            if (str != "") {
                var prueba = str.toString();
                var sp = prueba.split("-");
                var dia = sp[0];
                var mes = sp[1];
                var anio = sp[2];

                var fecha = anio + mes + dia;
                document.getElementById("<% = codigo.ClientID%>").value = id;
                var identi = document.getElementById("<% = codigo.ClientID%>").value;
                $.ajax({
                    type: 'POST',
                    url: 'ClienteMasGasta.aspx/Mostrar',
                    data: JSON.stringify({ id: identi , fecha: fecha }),
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
