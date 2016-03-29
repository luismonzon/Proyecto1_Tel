<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="ClientesDeuda.aspx.cs" Inherits="Proyecto1_Tel.Code.ClientesDeuda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
    <script type="text/javascript">


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
        <li><a href="ProductosInventario.aspx">Clientes Con Credito</a></li>    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/>
        <div id="modalpago" runat="server">
        
        </div>

        <div id="modalventa" runat="server">
        
        </div>


    <h4 class="widget-name"><i class="icon-columns"></i>Clientes Deudores</h4>
        <!-- Some controlы -->
        <div class="widget" id="tab_roles" runat="server">
        </div>
        <!-- /some controlы -->

        <!-- MODAL PARA PRODUCTOS EN BODEGA-->

 


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">

    <script type="text/javascript">

        function Deudas(identi) {

            $.ajax({
                type: 'POST',
                url: 'ClientesDeuda.aspx/Mostrar',
                data: JSON.stringify({ id: identi }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var data = JSON.parse(response.d);
                    var $select = $('#cDeuda');
                    var options = '';
                    for (var i = 0; i < data.length; i++) {
                        options += '<option value="' + data[i].key + '">' + data[i].value + '</option>';
                    }
                    $select.html(options);
                }
            });
        }

        function AbonarPago(id) {


            document.getElementById("<% = codigo.ClientID%>").value = id;
            var identi = document.getElementById("<% = codigo.ClientID%>").value;
            $.ajax({
                type: 'POST',
                url: 'ClientesDeuda.aspx/MostrarModal',
                data: JSON.stringify({ id: identi }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var $modal = $('#ContentPlaceHolder1_modalpago');
                    $modal.html(response.d);
                    Deudas(identi);
                    $('#modal-pago').modal({ //
                        show: true, //mostramos el modal registra producto
                        backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                    });

                    //$('#modal-pago').modal('hide').data('bs.modal', null);
                }
            });


        }
        

        function RealizarAbono() {
            var idDeuda = document.getElementById('cDeuda').value;
            var Deuda = $("#cDeuda  option:selected").text();
            var deuda = parseFloat(Deuda);
            var id = parseInt(idDeuda);
            var cantidad = document.getElementById('cantidad').value;
            if (deuda > 0 && cantidad > 0) {
                if (cantidad > deuda) {
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('La cantidad no debe ser mayor que la deuda').show(200).delay(2500).hide(200);
                } else {
                    alert(id + " " + cantidad);
                    $.ajax({
                        type: 'POST',
                        url: 'ClientesDeuda.aspx/Add',
                        data: JSON.stringify({ id: id, Cantidad: cantidad }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {

                            if (response.d == true) {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-success').html('Abono Realizado con exito').show(200).delay(2500).hide(200);
                                var identi = document.getElementById("<% = codigo.ClientID%>").value;
                                $('#formulario-pago')[0].reset();
                                document.getElementById("<% = codigo.ClientID%>").value = identi;
                                Deudas(identi);
                                 $("#ContentPlaceHolder1_cDeuda").text() = "";
                            } else {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-danger').html('No se pudo abonar a la deuda').show(200).delay(2500).hide(200);

                            }

                        }
                    });
                }
            } else
            {
                $('#mensaje').removeClass();
                $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(2500).hide(200);
            }
            

            return false;

        }


        function Ver_Venta(id) {
             $.ajax({
                 type: 'POST',
                 url: 'ClientesDeuda.aspx/MostrarVenta',
                 data: JSON.stringify({ id: id }),
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (response) {
                     var $modal = $('#ContentPlaceHolder1_modalventa');
                     $modal.html(response.d);
                     $('#modal-venta').modal({ //
                         show: true, //mostramos el modal registra producto
                         backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                     });

                     //$('#modal-pago').modal('hide').data('bs.modal', null);
                 }
             });
         }



    </script>

</asp:Content>
