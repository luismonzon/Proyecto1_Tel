<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="Proyecto1_Tel.Code.Cliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<!--- URL DE LA PAGINA --->
    <li><a href="Cliente.aspx">Clientes</a></li>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!--- TODO EL CONTENIDO DE LA PAGINA--->    
    <h5 class="widget-name"><i class="icon-columns"></i>Clientes</h5>
    
    
    <div>
        <div><a title="Agregar Cliente" style="font-size: 13px" id="nuevo-cliente" class="btn btn-success"> Agregar Cliente <i class="icon-plus-sign" >&nbsp;</i></a></div>
    </div>
    
	    <!-- Some controlы -->
        <div class="widget" id="tab_clientes" runat="server">
        </div>
        <!-- /some controlы -->

    <!-- MODAL PARA CLIENTES-->
    <div class="modal fade" id="editar-cliente" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                
                <div class="step-title">
                            	<i>C</i>
					    		<h5>Administrar Cliente</h5>
					    		<span>Agregar o Editar un cliente</span>
				</div>
                        	
            </div>
            <form id="formulario-cliente" class="form-horizontal row-fluid well">
            <div class="modal-body">
				<table border="0" width="100%" >
                    <tr>
                         <td style="visibility:hidden; height:5px;" >ID </td>
                        <td colspan="2"><input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/></td>

                    </tr>
                    
                        <div>
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Nombre:</b></label>
	                                <div class="controls"><input required="required" placeholder="Nombres" style="font-size: 15px;" type="text" name="username" id="nombre" runat="server" class="span12" /></div>
	                            </div>
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>Apellido:</b></label>
	                                <div class="controls"><input style="font-size: 15px;" placeholder="Apellidos" type="text" class="span12" id="apellido" runat="server" /></div>
	                            </div>
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>Nit:</b></label>
	                                <div class="controls"><input style="font-size: 15px;" id="nit" placeholder="NIT" runat="server" type="text" class="span12" /></div>
	                            </div>
                                <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>Direccion:</b></label>
	                                <div class="controls"><input placeholder="Direccion" style="font-size: 15px;" id="direccion" runat="server" type="text" class="span12" /></div>
	                            </div>
                                <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;"><b>Telefono:</b></label>
	                                <div class="controls"><input style="font-size: 15px;" placeholder="Telefono" id="telefono" runat="server" type="text" class="span12" /></div>
	                            </div>
	                        </div>
                
                    <tr>
                    	<td colspan="2">
                            <div id="mensaje"></div>
                            <div class="alert margin">
                                <button type="button" class="close" data-dismiss="alert">×</button>
	                                Campos Obligatorios (*)

                            </div>
                            
                            
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
            	<input type="submit" value="Registrar" class="btn btn-success" id="reg"/>
                <input type="submit" value="Editar" class="btn btn-warning"  id="edi"/>
            </div>
            </form>
          </div>
        </div>
      </div>
   
    <div>
        <h1 style="font-family: Calibri; font-size: 50px"></h1>
    </div>
    <div>
    </div>

<!---FINALIZA HTML--->
</asp:Content>





<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
    <!--- COGIGO JAVASCRIPT --->


     <script type="text/javascript">

         function Mostrar_cliente(id) {
             document.getElementById("<% = codigo.ClientID%>").value = id;

             var identi = document.getElementById("<% = codigo.ClientID%>").value;

             $('#edi').show(); //escondemos el boton de edicion porque es un nuevo registro
             $('#reg').hide(); //mostramos el boton de registro
             $('#editar-cliente').modal({ //
                 show: true, //mostramos el modal registra producto
                 backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
             });

             $.ajax({
                 type: 'POST',
                 url: 'Cliente.aspx/BuscarCliente',
                 data: JSON.stringify({ id: identi }),
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (response) {
                     
                     var cliente = JSON.parse(response.d);
                     var NombreCliente = cliente[0];
                     var NitCliente = cliente[1];
                     var ApellCliente = cliente[2];
                     var DireCliente = cliente[3];
                     var TeleCliente = cliente[4];


                     document.getElementById("<% = nombre.ClientID%>").value = NombreCliente;
                     document.getElementById("<% = nit.ClientID %>").value = NitCliente;
                     document.getElementById("<% = apellido.ClientID %>").value = ApellCliente;
                     document.getElementById("<% = direccion.ClientID %>").value = DireCliente;
                     document.getElementById("<% = telefono.ClientID %>").value = TeleCliente;
                     
                 }



             });
         }

         $('#edi').on('click', function () {

             var Nombre = document.getElementById("<%=nombre.ClientID%>").value;
             var Id = document.getElementById("<%=codigo.ClientID%>").value;
             var Nit = document.getElementById("<%=nit.ClientID%>").value;
             var Apellido = document.getElementById("<% = apellido.ClientID %>").value;
             var Direccion = document.getElementById("<% = direccion.ClientID %>").value;
             var Telefono = document.getElementById("<% = telefono.ClientID %>").value;
                    
              if (Nombre != "") {

                  $.ajax({

                      type: 'POST',
                      url: 'Cliente.aspx/EditCliente',
                      data: JSON.stringify({ id: Id, nombre: Nombre, nit: Nit, apellido: Apellido, direccion: Direccion, telefono: Telefono }),
                      contentType: 'application/json; charset=utf-8',
                      dataType: 'json',
                      success: function (response) {
                          if (response.d == true) {
                              $('#mensaje').removeClass();
                              $('#mensaje').addClass('alert alert-success').html('Registro editado exitosamente').show(200).delay(2500).hide(200);
                              
                          } else {
                              $('#mensaje').removeClass();
                              $('#mensaje').addClass('alert alert-danger').html('Nit ya existe con otro cliente').show(200).delay(2500).hide(200);

                          }
                          
                      }
                        
                  });
              } else {
                  $('#mensaje').removeClass();
                  $('#mensaje').addClass('alert alert-danger').html('No debe dejar campos vacios').show(200).delay(2500).hide(200);
                  Mostrar_cliente(id);
              }
              
             return false;
         });

        
         

    </script>


    <!--- ELIMINAR CLIENTE -->
    <script type="text/javascript">
        function Eliminar_Cliente(id) {
            if (confirm("Esta seguro que desea eliminar al usuario?")) {
                var row = $(this).closest("tr");
                var ClienteId = id;
                $.ajax({
                    type: "POST",
                    url: "Cliente.aspx/EliminarCliente",
                    data: JSON.stringify({ id: ClienteId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d == true) {
                            alert("Cliente Eliminado Exitosamente");
                        } else {
                            alert("Cliente No Se Pudo Eliminar");
                        }
                    }
                });
            }

        }



        //AGREGAR CLIENTE

        $('#reg').on('click', function () {

            var nNombre = document.getElementById("<%=nombre.ClientID%>").value;
            var nNit = document.getElementById("<%=nit.ClientID%>").value;
            var nDireccion = document.getElementById("<%=direccion.ClientID%>").value;
            var nTelefono = document.getElementById("<%=telefono.ClientID%>").value;
            var nApellido = document.getElementById("<%=apellido.ClientID%>").value;
           


            if (nNombre != "") {


                $.ajax({
                    type: 'POST',
                    url: 'Cliente.aspx/Add',
                    data: JSON.stringify({ nombre: nNombre, nit: nNit, apellido: nApellido, direccion: nDireccion, telefono: nTelefono  }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        if (response.d == true) {
                            $('#mensaje').removeClass();
                            $('#mensaje').addClass('alert alert-success').html('Cliente agregado con exito').show(200).delay(2500).hide(200);
                            $('#formulario-cliente')[0].reset();
                             } else {
                                 $('#mensaje').removeClass();
                                 $('#mensaje').addClass('alert alert-danger').html('Cliente ya existe').show(200).delay(2500).hide(200);

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
