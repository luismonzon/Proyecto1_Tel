<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="ClientesDeuda.aspx.cs" Inherits="Proyecto1_Tel.Code.ClientesDeuda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
    <script type="text/javascript">


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
        <li><a href="ProductosInventario.aspx">Clientes Con Credito</a></li>    

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <h5 class="widget-name"><i class="icon-columns"></i>Clientes</h5>
        <!-- Some controlы -->
        <div class="widget" id="tab_roles" runat="server">
        </div>
        <!-- /some controlы -->

        <!-- MODAL PARA PRODUCTOS EN BODEGA-->
    <div class="modal fade" id="modal-pago" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" onclick="reloadTable();" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                
                <div class="step-title">
                            	<i>P</i>
					    		<h5>Abonar Pago</h5>
					    		<span>Abonar pago a la deuda del cliente</span>
				</div>
                        	
            </div>
            <form id="formulario-pago" class="form-horizontal row-fluid well">
            <div class="modal-body">
				<table border="0" width="100%" >
                    <tr>
                         <td style="visibility:hidden; height:5px;" >ID</td>
                        <td colspan="2"><input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/></td>

                    </tr>
                    
                        <div>
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Deuda:</b></label>
                                    <select class="select2"  runat="server" required="required" id="cDeuda">
                                    </select>
	                             
	                            </div>

	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Cantidad:</b></label>
	                                <div class="controls"><input  required="required" style="font-size: 13px;" type="number"  id="cantidad" runat="server" /></div>
	                                </div>
                    <tr>
                    	<td colspan="2">
                            <div id="mensaje"></div>
                            <div class="alert margin">
                                <button type="button"  class="close" data-dismiss="alert">×</button>
	                                Campos Obligatorios (*)

                            </div>
                            
                            
                        </td>
                    </tr>
                </div>

                    </table>
                 </div>
                
                    
                </form>
            </div>
            
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="reloadTable();" id="cerrar">Cerrar</button>
            	<input type="submit" value="Abonar" class="btn btn-success" id="abonar"/>
            </div>
            
          </div>
        </div>
 


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
                    var $select = $('#ContentPlaceHolder1_cDeuda');
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

            $('#modal-pago').modal({ //
                show: true, //mostramos el modal registra producto
                backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
            });

            Deudas(identi);
        }
        

        $('#abonar').on('click', function () {
            var idDeuda = document.getElementById("<%=cDeuda.ClientID%>").value;
            var Deuda = $("#ContentPlaceHolder1_cDeuda  option:selected").text();
            var deuda = parseFloat(Deuda);
            var id = parseInt(idDeuda);
            var cantidad = document.getElementById("<%=cantidad.ClientID%>").value;
            if (deuda > 0 && cantidad > 0) {
                if (cantidad > deuda) {
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('La cantidad no debe ser mayor que la deuda').show(200).delay(2500).hide(200);
                } else {
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

        });

    </script>

</asp:Content>
