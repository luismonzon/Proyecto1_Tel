<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Inv_tienda.aspx.cs" Inherits="Proyecto1_Tel.Code.Inv_tienda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <li><a href="Inv_tienda.aspx">Inventario Tienda</a></li>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
	    <h4 class="widget-name"><i class="icon-columns"></i>Inventario Tienda</h4>
    <div>
        <div><a style="font-size: 13px" id="nuevo-tienda" onclick="cambio();" class="btn btn-success"> Agregar producto a tienda <i class="icon-plus-sign" >&nbsp;</i></a></div>
    </div>
    
        <!-- Some controlы -->
        <div class="widget" id="tab_tienda" runat="server">
        </div>
        <!-- /some controlы -->



     
     <!-- MODAL PARA PRODUCTOS EN BODEGA-->
    <div class="modal fade" id="modal-pro_tienda" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" onclick="reloadTable();" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                
                <div class="step-title">
                            	<i>T</i>
					    		<h5>Administrar Producto en Tienda</h5>
					    		<span>Agregar o Editar Productos en Tienda</span>
				</div>
                        	
            </div>
            <form id="formulario-pro_tienda" method="POST" class="form-horizontal row-fluid well">
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
	                                <div class="controls"><input   readonly="readonly" style="font-size: 15px;"  type="text" class="span12" id="descripcion" runat="server" /></div>
	                            </div>
                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Tipo:</b></label>
	                                <div class="controls"><input   readonly="readonly" style="font-size: 15px;"  type="text" class="span12" id="tipo2" runat="server" /></div>
	                            </div>
                                
                                 <div id="divcantdisp" class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Cantidad en Bodega:</b></label>
	                                <div class="controls"><input  readonly="readonly" required="required" style="font-size: 13px;" type="number"  id="cantidad_bodega" runat="server" /></div>
	                            </div>

                               <div id="div_disponible" class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Cantidad Disponible:</b></label>
	                                <div class="controls"><input readonly="readonly" required="required" style="font-size: 13px;" type="number"  id="cantdisponible" runat="server" /></div>
	                            </div>
                                

	                            <div id="divcantidad" class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Cantidad:</b></label>
	                                <div class="controls"><input  required="required" style="font-size: 13px;" placeholder="Cantidad" type="number"  id="cantidad" runat="server" /></div>
	                            </div>
                                <div id="divnmetros" class="control-group">
	                                <label class="control-label" style="font-size: 13px;" ><b>*Metros Disponibles:</b></label>
	                                <div class="controls"><input  readonly="readonly" style="font-size: 13px;" placeholder="Metros" type="number" value=""  step="any"  id="metrosdisponibles" runat="server" /></div>
	                            </div>
    
                                <div id="divmetros" class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Metros:</b></label>
	                                <div class="controls"><input  required="required" style="font-size: 13px;" placeholder="Metros" type="number" value=""  step="any"  id="metros" runat="server" /></div>
	                            
                                </div>
                                <div id="radio">
                                    <label class="radio">
                                    <input type="radio" name="opcion" id="agregar" class="styled" value="1" checked="checked">
									    Agregar
									</label>
									<label class="radio">
									    <input type="radio" name="opcion" id="quitar"  class="styled" value="2">
									    Quitar
									</label>
                                </div>

                            

                                
                                <div id="divprecio"  class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Precio:</b></label>
	                                <div class="controls"><input  required="required" style="font-size: 13px;" placeholder="Precio" type="number" value=""  step="any"  id="precio" runat="server" /></div>
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
   

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">

    <script type="text/javascript">


        function cambio() {

            var idproducto = document.getElementById("<%= producto.ClientID%>");

             var selected = idproducto.options[idproducto.selectedIndex].text;
             var id = idproducto.options[idproducto.selectedIndex].value;

             $.ajax({
                 type: 'POST',
                 url: 'Inv_tienda.aspx/Busca_Datos',
                 data: JSON.stringify({ id: id }),
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (response) {
                     var produc = JSON.parse(response.d);
                     var Descripcion = produc[0];
                     var Cantidad_Dispo = produc[1];
                     var Tipo = produc[2];
                     var Precio = produc[3];

                     document.getElementById("<% = descripcion.ClientID %>").value = Descripcion;

                     document.getElementById("<% = cantidad_bodega.ClientID %>").value = Cantidad_Dispo;

                     document.getElementById("<% = tipo2.ClientID %>").value = Tipo;

                     if (Tipo == "ARTICULO" || Tipo == "Articulo") {
                         $('#divmetros').hide();
                         $('#divcantidad').show();
                         

                     } else {
                         $('#divmetros').show();
                         $('#divcantidad').hide();
                     }
                     if (Precio == "1") {
                         $('#divprecio').hide();
                     } else {
                         $('#divprecio').show();
                     }

                }
            });

        }


        //Registro 
        $('#reg').on('click', function () {
            var idproducto = document.getElementById("<%=producto.ClientID%>").value;
            
            var cantidad = document.getElementById("<%=cantidad.ClientID%>").value;
            var metros = document.getElementById("<%=metros.ClientID%>").value;
            var precio = document.getElementById("<%=precio.ClientID%>").value;
            var articulo = document.getElementById("<%=tipo2.ClientID%>").value;
            
            if (articulo == "ARTICULO" || articulo == "Articulo") {
                metros = "0";

            } else {
                cantidad = "1";
            }

              if (metros != "" || cantidad != "") {

                  $.ajax({

                      type: 'POST',
                      url: 'Inv_tienda.aspx/Add',
                      data: JSON.stringify({ producto: idproducto, cantidad: cantidad , precio: precio, metros: metros}),
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

                  document.getElementById("<%=metros.ClientID%>").focus();
                  document.getElementById("<%=cantidad.ClientID%>").focus();
                  $('#mensaje').removeClass();
                  $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(2500).hide(200);

              }

              return false;
          });



        //EDITAR 

        function Editar_Tienda(id) {


            $('#edi').show(); //escondemos el boton de edicion porque es un nuevo registro
            $('#reg').hide(); //mostramos el boton de registro
            $('#modal-pro_tienda').modal({ //
                show: true, //mostramos el modal registra producto
                backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
            });

            $.ajax({
                type: 'POST',
                url: 'Inv_tienda.aspx/Busca_Descripcion',
                data: JSON.stringify({ id: id }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var produc = JSON.parse(response.d);
                    var Descripcion = produc[0];
                    var Cantidad = produc[1];
                    var Tipo_Descrip = produc[2];
                    var Precio = produc[3];
                    var Metros = produc[4];
                    


                    document.getElementById("<% = descripcion.ClientID %>").value = Descripcion;
                    document.getElementById("<% = producto.ClientID %>").value = id;
                    document.getElementById("<% = cantdisponible.ClientID %>").value = Cantidad;
                    document.getElementById("<% = precio.ClientID %>").value = Precio;
                    document.getElementById("<% = metrosdisponibles.ClientID %>").value = Metros;
                    

                    document.getElementById("<% = tipo2.ClientID %>").value = Tipo_Descrip;
                    if (Tipo_Descrip == "Polarizado") {
                        $('#divcantidad').hide();
                        $('#divcantdisp').hide();
                        $('#divnmetros').show();
                        $('#div_disponible').hide();
                    } else {
                        $('#divcantidad').show();
                        $('#divcantdisp').hide();
                        $('#divmetros').hide();
                        $('#divnmetros').hide();
                        $('#radio').show();
                        $('#div_disponible').show();
                    }


                }
            });
        }



        //Editar Productos en tienda

        $('#edi').on('click', function () {
            var idproducto = document.getElementById("<%=producto.ClientID%>").value;
            var cantidad = document.getElementById("<%=cantidad.ClientID%>").value;
            var precio = document.getElementById("<%=precio.ClientID%>").value;
            var metros = document.getElementById("<%=metros.ClientID%>").value;

            var formulario = document.forms[0];
            
            for (var i = 0; i < formulario.opcion.length; i++) {
                if (formulario.opcion[i].checked) {
                    
                    if (formulario.opcion[i].value == '1') {
                       
                        $.ajax({
                            type: 'POST',
                            url: 'Inv_tienda.aspx/Agregar',
                            data: JSON.stringify({ producto: idproducto, cantidad: cantidad, precio: precio, metros: metros }),
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
                            url: 'Inv_tienda.aspx/Rest',
                            data: JSON.stringify({ producto: idproducto, cantidad: cantidad, precio: precio, metros: metros }),
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



        //--------- ELIMINAR PRODUCTO DE TIENDA -->
        function Eliminar_Tienda(id) {
            if (confirm("Esta seguro que desea eliminar el producto?")) {
                $.ajax({
                    type: "POST",
                    url: "Inv_tienda.aspx/DeleteProd",
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

         