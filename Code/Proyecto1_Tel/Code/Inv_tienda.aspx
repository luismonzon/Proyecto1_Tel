<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Inv_tienda.aspx.cs" Inherits="Proyecto1_Tel.Code.Inv_tienda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <li><a href="Inv_tienda.aspx">Inventario Tienda</a></li>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
	    <h4 class="widget-name"><i class="icon-columns"></i>Inventario Tienda</h4>
    <div>
        <div><a style="font-size: 13px" id="nuevo-tienda" onclick="cambio();" class="btn btn-success"> Agregar producto a inventario <i class="icon-plus-sign" >&nbsp;</i></a></div>
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
                            	<i>P</i>
					    		<h5>Producto en Bodega</h5>
					    		<span>Agregar o Editar Productos en Tienda</span>
				</div>
                        	
            </div>
            <form id="formulario-pro_tienda" class="form-horizontal row-fluid well">
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
                                
                                 <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Cantidad Disponible:</b></label>
	                                <div class="controls"><input  readonly="readonly" required="required" style="font-size: 13px;" type="number"  id="cantidad_bodega" runat="server" /></div>
	                            </div>
                               

	                            <div id="divcantidad" class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Cantidad:</b></label>
	                                <div class="controls"><input  required="required" style="font-size: 13px;" placeholder="Cantidad" type="number"  id="cantidad" runat="server" /></div>
	                            </div>

                                <div id="divmetros" class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Metros:</b></label>
	                                <div class="controls"><input  required="required" style="font-size: 13px;" placeholder="Metros" type="number" value=""  step="any"  id="metros" runat="server" /></div>
	                            </div>
                                <div id="divprecio"  class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Precio:</b></label>
	                                <div class="controls"><input  required="required" style="font-size: 13px;" placeholder="Precio" type="number" value=""  step="any"  id="precio" runat="server" /></div>
	                            </div>
                                
                                <div id="radio" class="control-group">
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


        //registro 
        $('#reg').on('click', function () {
            alert('aqui');
            var idproducto = document.getElementById("<%=producto.ClientID%>").value;
            alert(idproducto);
            var cantidad = document.getElementById("<%=cantidad.ClientID%>").value;
            alert(cantidad);
            var metros = document.getElementById("<%=metros.ClientID%>").value;
            alert(metros);
            var precio = document.getElementById("<%=precio.ClientID%>").value;
            alert(precio);
            var articulo = document.getElementById("<%=tipo2.ClientID%>").value;
            alert(articulo);
            
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
                  $('#mensaje').removeClass();
                  $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(2500).hide(200);

              }

              return false;
          });


    </script>


</asp:Content>

         