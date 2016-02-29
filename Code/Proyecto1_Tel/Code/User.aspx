<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Proyecto1_Tel.Code.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--- URL DE LA PAGINA --->
    <li><a href="User.aspx">Usuarios</a></li>
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
                    <div  class="span4"><a style="visibility:hidden"; class="btn btn-danger btn-block bs-alert" title="Simple Alert">Alert dialog</a></div>
	                           
                    <div style="align-content:center" class="span4"><a href="AddUser.aspx"  id="nuevo-usuario" class="btn btn-success btn-block">Agregar Usuario</a></div>
                 </div>
            

    </section>

	    <h5 class="widget-name"><i class="icon-columns"></i>Usuarios</h5>
        <!-- Some controlы -->
        <div class="widget" id="tab_usuarios" runat="server">
        </div>
        <!-- /some controlы -->

     <!-- MODAL PARA EDITAR CLIENTES-->
    <div class="modal fade" id="editar-usuario" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
              <h4 class="modal-title" id="myModalLabel"><b>Editar Usuario</b></h4>
            </div>
            <form id="formulario-cliente" name="formulario-cliente" class="formulario">
            <div class="modal-body">
				<table border="0" width="100%">
               		<tr>
                        <td style="visibility:hidden; height:5px;" >ID </td>
                        <td colspan="2"><input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/></td>

               		</tr> 
                    <tr>
                    <td>*Usuario:</td>
                    <td><input type="text" required="required" name="nombre" id="nombre" runat="server" maxlength="100"/></td>
                    </tr>
                    <tr>
                    <td>*Contraseña:</td>
                    <td><input type="text" name="password" id="password" required="required" runat="server" maxlength="100"/></td>
                    </tr>
                    <tr>
                    <td>
                        <label class="control-label">*Rol:</label>
                    </td>
                    <td>
	                        <div class="controls">
	                            <select name="user_rol" class="styled" runat="server" id="user_rol" required="required">
	                            </select>
	                        </div>
                    </td>                        
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
    <!--
        $.ajax({
                 type: 'POST',
                 url: 'User.aspx/BuscarUsuario',
                 data: JSON.stringify({ id: identi }),
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (response) {
                     alert(response.d);
                     var cliente = JSON.parse(response.d);
                     var NombreCliente = cliente[0];
                     var NitCliente = cliente[1];
                     document.getElementById("<% = nombre.ClientID%>").value = NombreCliente;
                     document.getElementById("<% = password.ClientID %>").value = NitCliente;
                 }



             });

        -->

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">

    <!--- COGIGO JAVASCRIPT --->


     <script type="text/javascript">

         function Mostrar_cliente(id) {
             document.getElementById("<% = codigo.ClientID%>").value = id;

             var identi = document.getElementById("<% = codigo.ClientID%>").value;

             $('#editar-usuario').modal({ //
                 show: true, //mostramos el modal registra producto
                 backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
             });

             $.ajax({
                 type: 'POST',
                 url: 'User.aspx/BuscarUsuario',
                 data: JSON.stringify({ id: identi }),
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (response) {
                     var cliente = JSON.parse(response.d);
                     var NombreCliente = cliente[0];
                     var PasswordCliente = cliente[1];
                     var RolCliente = cliente[2];
                     document.getElementById("<% = nombre.ClientID%>").value = NombreCliente;
                     document.getElementById("<% = password.ClientID %>").value = PasswordCliente;
                     document.getElementById("<% = user_rol.ClientID %>").value = RolCliente;
                 }



             });
             
         }

         //Editar Usuario

         $('#edi').on('click', function () {

             var user = document.getElementById("<%=nombre.ClientID%>").value;
             var id = document.getElementById("<%=codigo.ClientID%>").value;
             var pass = document.getElementById("<%=password.ClientID%>").value;
             var rol = document.getElementById("<% = user_rol.ClientID %>").value;
             if (user != "" && pass != "" && rol != "") {
                 $.ajax({
                     
                     type: 'POST',
                     url: 'User.aspx/EditUser',
                     data: JSON.stringify({ id: id, nombre: user, rol: rol , pass: pass}),
                     contentType: 'application/json; charset=utf-8',
                     dataType: 'json',
                     success: function (response) {
                         if (response.d == true) {

                             $('#mensaje').addClass('alert alert-success').html('Registro editado con exito').show(200).delay(2500).hide(200);
                             return false;
                         }

                     }

                 });

             }
             $('#mensaje').addClass('alert alert-danger').html('No debe dejar campos vacios').show(200).delay(2500).hide(200);
             return false;
         });

    //--------- ELIMINAR CLIENTE -->
        function Eliminar_Usuario(id) {
            if (confirm("Esta seguro que desea eliminar al usuario?")) {
                $.ajax({
                    type: "POST",
                    url: "User.aspx/DeleteUser",
                    data: JSON.stringify({ id: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                    }
                });
            }

        }
    </script>


</asp:Content>
