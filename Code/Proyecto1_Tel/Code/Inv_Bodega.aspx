<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Inv_Bodega.aspx.cs" Inherits="Proyecto1_Tel.Code.Inv_Bodega" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <li><a href="Inv_Bodega.aspx">Inventario Bodega</a></li>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
	    <h4 class="widget-name"><i class="icon-columns"></i>Bodega</h4>
    <div>
        <div><a style="font-size: 13px" id="nuevo-bodega" onclick="cambio();" class="btn btn-success"> Agregar Producto a Bodega <i class="icon-plus-sign" >&nbsp;</i></a></div>
    </div>
    
        <!-- Some controlы -->
        <div class="widget" id="tab_bodega" runat="server">
        </div>
        <!-- /some controlы -->



    
     <!-- MODAL PARA PRODUCTOS EN BODEGA-->
    <div class="row-fluid well body">
	                    <div class="span6">
	          <div class="modal fade" id="modal-pro_bodega" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" onclick="reloadTable();" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                
                <div class="step-title">
                            	<i>B</i>
					    		<h5>Producto en Bodega</h5>
					    		<span>Agregar o Editar Productos en Bodega</span>
				</div>
                        	
            </div>
            <form id="formulario-pro_bodega" class="form-horizontal row-fluid well">
            <div class="modal-body">
				<table border="0" width="100%" >
                    <tr>
                         <td style="visibility:hidden; height:5px;" >ID</td>
                        <td colspan="2"><input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/></td>

                    </tr>
                    
                        <div>
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Producto:</b></label>
                                                   <select onChange="cambio()"  class="select" runat="server" required="required" id="producto">
                                                </select>
	                             
	                            </div>
                                <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Descripcion:</b></label>
	                                <div class="controls"><input   readonly="readonly" style="font-size: 15px;" placeholder="Descripcion" type="text" class="span12" id="descripcion" runat="server" /></div>
	                            </div>
                                
                                <div id="divcantidad" class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Cantidad Disponible:</b></label>
	                                <div class="controls"><input readonly="readonly" required="required" style="font-size: 13px;" type="number"  id="cantdisponible" runat="server" /></div>
	                            </div>
                                    
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Cantidad:</b></label>
	                                <div class="controls"><input  required="required" style="font-size: 13px;" type="number"  id="cantidad" runat="server" /></div>
	                            </div>
                                <div id="radio" >
                                    <label class="radio">
                                    <input type="radio" name="opcion" id="agregar" class="styled" value="1" checked="checked">
									    Agregar
									</label>
									<label class="radio">
									    <input type="radio" name="opcion" id="quitar"  class="styled" value="2">
									    Quitar
									</label>
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
            	<input type="submit" value="Registrar" class="btn btn-success" id="reg"/>
                <input type="submit" value="Editar" class="btn btn-warning"  id="edi"/>
            </div>
            
          </div>
        </div>
    
                            
                                  </div>
        </div>
      
   

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">



    <script type="text/javascript">

        //cambia dependiendo de la opcion seleccionada
        function cambio() {
            
            var idproducto = document.getElementById("<%= producto.ClientID%>");
            
            var selected = idproducto.options[idproducto.selectedIndex].text;
            var id = idproducto.options[idproducto.selectedIndex].value;
            

            $.ajax({
                type: 'POST',
                url: 'Inv_Bodega.aspx/Busca_Descripcion',
                data: JSON.stringify({ id: id}),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var produc = JSON.parse(response.d);
                    var Descripcion = produc[0];
                    document.getElementById("<% = descripcion.ClientID %>").value = Descripcion;

                   
                 }
            });

        }



        $('#reg').on('click', function () {

            var idproducto = document.getElementById("<%=producto.ClientID%>").value;
            var cantidad = document.getElementById("<%=cantidad.ClientID%>").value;

            if (cantidad != "") {

                $.ajax({

                    type: 'POST',
                    url: 'Inv_Bodega.aspx/Add',
                    data: JSON.stringify({ producto: idproducto, cantidad: cantidad }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        alert(response.d);
                        if (response.d == true) {
                            $('#mensaje').removeClass();
                            $('#mensaje').addClass('alert alert-success').html('Producto agregado con exito').show(200).delay(2500).hide(200);

                        } else {
                            $('#mensaje').removeClass();
                            $('#mensaje').addClass('alert alert-danger').html('Producto no se pudo agregar').show(200).delay(2500).hide(200);

                        }

                    }
                });

            } else {
                document.getElementById("<%= cantidad.ClientID %>").focus();
                $('#mensaje').removeClass();
                $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(2500).hide(200);

            }

            return false;
        });


        function Editar_Bodega(id) {
            

            $('#edi').show(); //escondemos el boton de edicion porque es un nuevo registro
            $('#reg').hide(); //mostramos el boton de registro
            $('#modal-pro_bodega').modal({ //
                show: true, //mostramos el modal registra producto
                backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
            });

            $.ajax({
                type: 'POST',
                url: 'Inv_Bodega.aspx/Busca_Descripcion',
                data: JSON.stringify({ id: id }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var produc = JSON.parse(response.d);
                    var Descripcion = produc[0];
                    var Cantidad = produc[1];
                    document.getElementById("<% = descripcion.ClientID %>").value = Descripcion;
                    document.getElementById("<% = producto.ClientID %>").value = id;
                    document.getElementById("<% = cantdisponible.ClientID %>").value = Cantidad;
                    
                }
            });
        }

        $('#edi').on('click', function () {
            var idproducto = document.getElementById("<%=producto.ClientID%>").value;
            var cantidad = document.getElementById("<%=cantidad.ClientID%>").value;
            var formulario = document.forms[0];
            
            for (var i = 0; i < formulario.opcion.length; i++) {
                alert('for');
                if (formulario.opcion[i].checked) {

                    if (formulario.opcion[i].value == '1') {
                        $.ajax({

                            type: 'POST',
                            url: 'Inv_Bodega.aspx/Add',
                            data: JSON.stringify({ producto: idproducto, cantidad: cantidad }),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (response) {
                              
                                if (response.d == true) {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-success').html('Producto agregado con exito').show(200).delay(2500).hide(200);

                                } else {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-danger').html('Producto no se pudo agregar').show(200).delay(2500).hide(200);

                                }

                            }
                        });

                    } else {
                        $.ajax({

                            type: 'POST',
                            url: 'Inv_Bodega.aspx/Rest',
                            data: JSON.stringify({ producto: idproducto, cantidad: cantidad }),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (response) {

                                if (response.d == true) {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-success').html('Producto restado con exito').show(200).delay(2500).hide(200);

                                } else {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-danger').html('Producto no se pudo restar').show(200).delay(2500).hide(200);

                                }

                            }
                        });
                    }
                }
            }



        });


        //--------- ELIMINAR PRODUCTO DE BODEGA -->
        function Eliminar_Bodega(id) {
            if (confirm("Esta seguro que desea eliminar el producto?")) {
                $.ajax({
                    type: "POST",
                    url: "Inv_Bodega.aspx/DeleteProd",
                    data: JSON.stringify({ id: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d == true) {
                            reloadTable();
                            alert("Producto Eliminado Exitosamente");
                        } else {
                            alert("El Producto No Pudo Ser Eliminado");
                        }
                    }
                });
            }

        }



</script>


</asp:Content>
