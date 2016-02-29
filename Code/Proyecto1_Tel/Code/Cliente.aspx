<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="Proyecto1_Tel.Code.Cliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<!--- URL DE LA PAGINA --->
    <li><a href="Cliente.aspx">Clientes</a></li>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!--- TODO EL CONTENIDO DE LA PAGINA--->    
			    <!-- Page header -->
			    <div class="page-header">
			    	<div class="page-title">
				    	<h5>Proteccion Solar</h5>
				    	<span>LA TORRE</span>
			    	</div>

			    	<ul class="page-stats">
			    		<li>
			    			<div class="showcase">
			    				<span>New visits</span>
			    				<h2>22.504</h2>
			    			</div>
			    			<div id="total-visits" class="chart">10,14,8,45,23,41,22,31,19,12, 28, 21, 24, 20</div>
			    		</li>
			    		<li>
			    			<div class="showcase">
			    				<span>My balance</span>
			    				<h2>$16.290</h2>
			    			</div>
			    			<div id="balance" class="chart">10,14,8,45,23,41,22,31,19,12, 28, 21, 24, 20</div>
			    		</li>
			    	</ul>
			    </div>
			    <!-- /page header -->
        <section>
            <form>

            </form>
               <div class="semi-widget row-fluid">
                    <div  class="span4"><a style="visibility:hidden; class="btn btn-danger btn-block bs-alert" title="Simple Alert">Alert dialog</a></div>
	                           
                    <div style="align-content:center" class="span4"><a href="AddCliente.aspx"  id="nuevo-cliente" class="btn btn-success btn-block">Agregar Cliente</a></div>
                 </div>
            

    </section>

	    <h5 class="widget-name"><i class="icon-columns"></i>Clientes</h5>
        <!-- Some controlы -->
        <div class="widget" id="tab_clientes" runat="server">
        </div>
        <!-- /some controlы -->

     <!-- MODAL PARA EDITAR CLIENTES-->
    <div class="modal fade" id="editar-cliente" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
              <h4 class="modal-title" id="myModalLabel"><b>Editar Cliente</b></h4>
            </div>
            <form id="formulario-cliente" name="formulario-cliente" class="formulario">
            <div class="modal-body">
				<table border="0" width="100%">
               		<tr>
                           <td style="visibility:hidden; height:5px;" >ID </td>
                        <td colspan="2"><input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/></td>

               		</tr> 
                    <tr>
                    	<td>*Nombre:</td>
                        <td><input type="text" required="required" name="nombre" id="nombre" runat="server" maxlength="100"/></td>
                    </tr>
                    <tr>
                    	<td>*Nit:</td>
                        <td><input type="text" name="nit" id="nit" required="required" runat="server" maxlength="100"/></td>
                    </tr>
                    <tr>
                    	<td colspan="2">
                        	<label>Campos Obligatorios (*)</label>
                        </td>
                    </tr>
                    <tr>
                    	<td colspan="2">
                        	<div  id="mensaje"></div>
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="modal-footer">
            	<input type="submit" value="Registrar" class="btn btn-large btn-success " name="reg" id="reg"/>
                <input type="submit" value="Editar" class="btn btn-large btn-warning"  id="edi"/>
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
                     document.getElementById("<% = nombre.ClientID%>").value = NombreCliente;
                     document.getElementById("<% = nit.ClientID %>").value = NitCliente;
                 }



             });
         }

         $('#edi').on('click', function () {

             var user = document.getElementById("<%=nombre.ClientID%>").value;
             var id = document.getElementById("<%=codigo.ClientID%>").value;
             var nit = document.getElementById("<%=nit.ClientID%>").value;
              if (user != "") {

                  $.ajax({

                      type: 'POST',
                      url: 'Cliente.aspx/EditCliente',
                      data: JSON.stringify({ id: id, nombre: user, nit: nit }),
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
    </script>

   
</asp:Content>
