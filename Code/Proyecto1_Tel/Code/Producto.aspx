<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="Proyecto1_Tel.Code.Producto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--- URL DE LA PAGINA --->
    <li><a href="Producto.aspx">Productos</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--- TODO EL CONTENIDO DE LA PAGINA--->    
			   
     <h5 class="widget-name"><i class="icon-columns"></i>Productos</h5>
       
    <div>
        <div><a style="font-size: 13px" id="nuevo-producto" class="btn btn-success"> Agregar Producto <i class="icon-plus-sign" >&nbsp;</i></a></div>
    </div>
    

    <!---TABLA QUE MUESTRA LOS USUARIOS--->
	    <!-- Some controlы -->
        <div class="widget" id="tab_producto" runat="server">
        </div>
        <!-- /some controlы -->


     <!-- MODAL PARA CLIENTES-->
    <div class="modal fade" id="modal-producto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" onclick="reloadTable();" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                
                <div class="step-title">
                            	<i>P</i>
					    		<h5>Administrar Producto</h5>
					    		<span>Agregar o Editar un producto</span>
				</div>
                        	
            </div>
            <form id="formulario-producto" class="form-horizontal row-fluid well">
            <div class="modal-body">
				<table border="0" width="100%" >
                    <tr>
                         <td style="visibility:hidden; height:5px;" >ID</td>
                        <td colspan="2"><input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/></td>

                    </tr>
                    
                        <div>
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Abreviatura:</b></label>
	                                <div class="controls"><input required="required" placeholder="Abreviatura de Producto" style="font-size: 15px;" type="text" name="abreviatura" id="abreviatura" runat="server" class="span12" /></div>
	                            </div>
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Descripcion:</b></label>
	                                <div class="controls"><input  required="required" style="font-size: 15px;" placeholder="Descripcion" type="text" class="span12" id="descripcion" runat="server" /></div>
	                            </div>
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;"><b>*Producto:</b></label>
                                     <div  class="controls">
	                                            <select  class="select" onChange="cambio()" runat="server" required="required" id="tipopro">
                                                </select>
	                                 </div>
	                        </div>
                                <div id="porcentaje" class="control-group">
	                                <label class="control-label" id="Lporcentaje"  style="font-size: 15px;" ><b>*Porcentaje:</b></label>
	                                <div class="controls">
                                        <input placeholder="Porcentaje" required="required" style="font-size: 12px;" id="porc"  type="number" />

	                                </div>
	                            </div>
                            </div>
                                <div  class="control-group">
	                                <label class="control-label"  id="Llargo" style="font-size: 15px;"><b>*Largo:</b></label>
	                                <div class="controls"><input required="required" type="number" id="largo" style="font-size: 12px;" value="" step="any" placeholder="Largo"/></div>
                                    <label class="control-label" id="Lancho"  style="font-size: 15px;"><b>*Ancho:</b></label>
	                                <div class="controls"><input required="required" type="number" id="ancho" style="font-size: 12px;" value="" step="any" placeholder="Ancho"/></div>
                                    <label class="control-label" style="font-size: 15px;"><b>*Marca:</b></label>
	                                <div class="controls"><input type="text" id="marca" runat="server" style="font-size: 12px;"  placeholder="Marca"/></div>
	                            </div>
                    <tr>
                    	<td colspan="2">
                            <div id="mensaje"></div>
                            <div class="alert margin">
                                <button type="button" onclick="actualizar()" class="close" data-dismiss="alert">×</button>
	                                Campos Obligatorios (*)

                            </div>
                            
                            
                        </td>
                    </tr>
                

                    </table>
                 </div>
                
                    
                </form>
            </div>
            
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="cerrar">Cerrar</button>
            	<input type="submit" value="Registrar" class="btn btn-success" id="reg"/>
                <input type="submit" value="Editar" class="btn btn-warning"  id="edi"/>
            </div>
            
          </div>
        </div>
      
   

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
    <script type="text/javascript">

        //cambia dependiendo de la opcion seleccionada
        function cambio() {
            var combo = document.getElementById("<%= tipopro.ClientID%>");
            
            var selected = combo.options[combo.selectedIndex].text;


            if (selected == "Articulo" || selected == "ARTICULO") {
                $('#porc').hide();
                $('#largo').hide();
                $('#ancho').hide();
                $('#Lancho').hide();
                $('#Llargo').hide();
                $('#Lporcentaje').hide();
                $('#porcentaje').hide();

            } else {
                $('#porc').show();
                $('#largo').show();
                $('#ancho').show();
                $('#Lancho').show();
                $('#Llargo').show();
                $('#Lporcentaje').show();
                $('#porcentaje').show();
            }
                

        }

        


        //Mostrar datos en edicion
        function Mostrar_Producto(id) {
            document.getElementById("<% = codigo.ClientID%>").value = id;
             var identi = document.getElementById("<% = codigo.ClientID%>").value;

             $('#reg').hide(); //escondemos el boton de registro
             $('#edi').show(); //escondemos el boton de edicion porque es un nuevo registro
             $('#reg').hide(); //mostramos el boton de registro
             $('#modal-producto').modal({ //
                 show: true, //mostramos el modal registra producto
                 backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
             });

             $.ajax({
                 type: 'POST',
                 url: 'Producto.aspx/BuscarProducto',
                 data: JSON.stringify({ id: identi }),
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (response) {
                     var produc = JSON.parse(response.d);
                     var Abreviatura = produc[0];
                     var Descripcion = produc[1];
                     var Tipo = produc[2];
                     var Porcentaje = produc[3];
                     var Largo = produc[4];
                     var Ancho = produc[5];
                     var Marca = produc[6];
                     var tipodesc = produc[7];

                     document.getElementById("<% = abreviatura.ClientID %>").value = Abreviatura;
                     document.getElementById("<% = descripcion.ClientID %>").value = Descripcion;
                     document.getElementById("<% = tipopro.ClientID %>").value = Tipo;
                     

                     

                     
                     if (tipodesc == "ARTICULO" || tipodesc == "Articulo")
                     {
                         $('#porc').hide();
                         $('#largo').hide();
                         $('#ancho').hide();
                         $('#Lancho').hide();
                         $('#Llargo').hide();
                         $('#Lporcentaje').hide();
                         $('#porcentaje').hide();
                     } else {
                         $('#porc').show();
                         $('#largo').show();
                         $('#ancho').show();
                         $('#Lancho').show();
                         $('#Llargo').show();
                         $('#Lporcentaje').show();
                         $('#porcentaje').show();
                         $('#porc').val(Porcentaje);
                         $('#largo').val(Largo);
                         $('#ancho').val(Ancho);


                     }
                     
                     document.getElementById("<% = marca.ClientID %>").value = Marca;

                 }
             });

        }

        $('#cerrar').on('click', function () {
            
            reloadTable();
        });

       

         //REGISTRAR UN PRODUCTO
        $('#reg').on('click', function () {
            

            var nAbre = document.getElementById("<%=abreviatura.ClientID%>").value;
             var nDescrip = document.getElementById("<%=descripcion.ClientID%>").value;
             var nTipopro = document.getElementById("<%=tipopro.ClientID%>").value;
             var nMarca = document.getElementById("<%=marca.ClientID%>").value;
            var nLargo = $('#largo').val();
            var nAncho = $('#largo').val();
            var nporcen = $('#porc').val();



            var combo = document.getElementById("<%= tipopro.ClientID%>");

            var selected = combo.options[combo.selectedIndex].text;
            
            if (selected == "ROLLO" || selected == "Rollo") {

                if (nLargo != "" && nAncho != "" && nporcen != "") {

                    if (nAbre != "" && nDescrip != "" && nTipopro != "" && nMarca != "") {


                        $.ajax({
                            type: 'POST',
                            url: 'Producto.aspx/Add',
                            data: JSON.stringify({ abrevia: nAbre, descripcion: nDescrip, tipo: nTipopro, marca: nMarca, largo: nLargo, ancho: nAncho, porc: nporcen }),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (response) {
                                if (response.d == true) {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-success').html('Producto agregado con exito').show(200).delay(5500).hide(200);
                                    $('#formulario-producto')[0].reset();
                                    $('#modal-producto')[0].reset();
                                } else {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-danger').html('Abreviatura ya existe').show(200).delay(5500).hide(200);

                                }

                            }
                        });

                    } else {
                        $('#mensaje').removeClass();
                        $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                    }
                }else{
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                }
                } else {
                if (nAbre != "" && nDescrip != "" && nTipopro != "" && nMarca != "") {
                    nAncho = "";
                    nLargo = "";
                    nporcen = "";
                    $.ajax({
                        type: 'POST',
                        url: 'Producto.aspx/Add',
                        data: JSON.stringify({ abrevia: nAbre, descripcion: nDescrip, tipo: nTipopro, marca: nMarca, largo: nLargo, ancho: nAncho, porc: nporcen }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            if (response.d == true) {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-success').html('Producto agregado con exito').show(200).delay(5500).hide(200);
                                $('#formulario-producto')[0].reset();
                                $('#modal-producto')[0].reset();
                            } else {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-danger').html('Abreviatura ya existe').show(200).delay(5500).hide(200);

                            }

                        }
                    });

                } else {
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                }                   
                }


                return false;

            }
         );


        //REGISTRAR UN PRODUCTO
        $('#edi').on('click', function () {


            var nAbre = document.getElementById("<%=abreviatura.ClientID%>").value;
            var nDescrip = document.getElementById("<%=descripcion.ClientID%>").value;
            var nTipopro = document.getElementById("<%=tipopro.ClientID%>").value;
            var nMarca = document.getElementById("<%=marca.ClientID%>").value;
            var nCodigo = document.getElementById("<%=codigo.ClientID%>").value;
            var nLargo = $('#largo').val();
            var nAncho = $('#largo').val();
            var nporcen = $('#porc').val();
            var combo = document.getElementById("<%= tipopro.ClientID%>");

            var selected = combo.options[combo.selectedIndex].text;

            if (selected == "ROLLO" || selected == "Rollo") {

                if (nLargo != "" && nAncho != "" && nporcen != "") {

                    if (nAbre != "" && nDescrip != "" && nTipopro != "" && nMarca != "") {


                        $.ajax({
                            type: 'POST',
                            url: 'Producto.aspx/EditPro',
                            data: JSON.stringify({id: nCodigo, abrevia: nAbre, descripcion: nDescrip, tipo: nTipopro, marca: nMarca, largo: nLargo, ancho: nAncho, porc: nporcen }),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (response) {
                                if (response.d == true) {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-success').html('Producto editado con exito').show(200).delay(5500).hide(200);
                                    
                                } else {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-danger').html('Abreviatura ya existe').show(200).delay(5500).hide(200);
                                    
                                }

                            }
                        });

                    } else {
                        $('#mensaje').removeClass();
                        $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                    }
                } else {
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                }
            } else {
                if (nAbre != "" && nDescrip != "" && nTipopro != "" && nMarca != "") {
                    nAncho = "";
                    nLargo = "";
                    nporcen = "";
                    $.ajax({
                        type: 'POST',
                        url: 'Producto.aspx/EditPro',
                        data: JSON.stringify({id: nCodigo, abrevia: nAbre, descripcion: nDescrip, tipo: nTipopro, marca: nMarca, largo: nLargo, ancho: nAncho, porc: "" }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            if (response.d == true) {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-success').html('Producto editado con exito').show(200).delay(5500).hide(200);
                                
                            } else {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-danger').html('Abreviatura ya existe').show(200).delay(5500).hide(200);
                                
                            }

                        }
                    });

                } else {
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                }
            }


            return false;

        }
         );

        //--------- ELIMINAR PRODUCTO -->
        function Eliminar_Producto(id) {
            if (confirm("Esta seguro que desea eliminar el producto?")) {
                $.ajax({
                    type: "POST",
                    url: "Producto.aspx/DeleteProd",
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
