<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Proyecto1_Tel.Code.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--- URL DE LA PAGINA --->
    <li><a href="User.aspx">Usuarios</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--- TODO EL CONTENIDO DE LA PAGINA--->    
			   
     <h5 class="widget-name"><i class="icon-columns"></i>Usuarios</h5>
       
    <div>
        <div><a style="font-size: 13px" id="nuevo-usuario" class="btn btn-success"> Agregar Usuario <i class="icon-plus-sign" >&nbsp;</i></a></div>
    </div>
    
                    <!---TABLA QUE MUESTRA LOS USUARIOS--->
	    <!-- Some controlы -->
        <div class="widget" id="tab_usuarios" runat="server">
        </div>
        <!-- /some controlы -->


     <!-- MODAL PARA EDITAR USUARIO-->
    <div class="modal fade" id="editar-usuario" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <div class="step-title">
                            	<i>R</i>
					    		<h5>Administrar Usuario</h5>
					    		<span>Agregar o Editar un Usuario</span>
				</div>


              
            </div>
            <form id="formulario-usuario" name="formulario-usuario" class="form-horizontal row-fluid well">
            <div class="modal-body">
				<table border="0" width="100%">
               		<tr>
                        <td style="visibility:hidden; height:5px;" >ID </td>
                        <td colspan="2"><input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/></td>

               		</tr> 

                        <div>
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Usuario:</b></label>
	                                <div class="controls"><input placeholder="NickName" required="required" style="font-size: 15px;" type="text" name="nickname" id="nickname" runat="server" class="span12" /></div>
	                            </div>
	                            <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>*Nombre:</b></label>
	                                <div class="controls"><input required="required" style="font-size: 15px;" placeholder="Nombre" type="text" class="span12" id="nombre" runat="server" /></div>
                                     <label class="control-label" style="font-size: 15px;" ><b>*Apellido:</b></label>
	                                <div class="controls"><input required="required" style="font-size: 15px;" id="apellido" placeholder="Apellido" runat="server" type="text" class="span12" /></div>
	                            </div>
                                <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;" ><b>DPI:</b></label>
	                                <div class="controls"><input placeholder="DPI" style="font-size: 15px;" id="dpi" runat="server" type="text" class="span12" /></div>
	                            </div>
                                <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;"><b>*Contraseña:</b></label>
	                                <div class="controls"><input required="required" style="font-size: 15px;" placeholder="Password" id="password" runat="server" type="password" class="span12" /></div>
                                     <label class="control-label" style="font-size: 15px;"><b>*Repetir Contraseña:</b></label>
	                                <div class="controls"><input required="required" style="font-size: 15px;" placeholder="Repetir Contraseña" id="password1" runat="server" type="password" class="span12" /></div>
	                            </div>
                                <div class="control-group">
	                                <label class="control-label" style="font-size: 15px;"><b>*Rol:</b></label>
                                     <div class="controls">
	                                            <select name="Rol" class="select" runat="server" id="Rol">

	                                            </select>
	                                 </div>
	                        </div>
                    <tr>
                    	<td colspan="2">
                        	<div  id="mensaje"></div>
                        </td>
                    </tr>
                    <tr>
                    	<td colspan="2">
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
            	<input type="submit" value="Registrar" class="btn btn-large btn-success " name="reg" id="reg"/>
                <input type="submit" value="Editar" class="btn btn-large btn-warning"  id="edi"/>
            </div>
            </form>
          </div>
        </div>
    

</div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">

    <!--- COGIGO JAVASCRIPT --->


     <script type="text/javascript">

         function Mostrar_usuario(id) {
             document.getElementById("<% = codigo.ClientID%>").value = id;
             var identi = document.getElementById("<% = codigo.ClientID%>").value;

             $('#reg').hide(); //escondemos el boton de registro
             $('#edi').show(); //escondemos el boton de edicion porque es un nuevo registro
             $('#reg').hide(); //mostramos el boton de registro
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
                     var usuario = JSON.parse(response.d);
                     var NickUsuario = usuario[0];
                     var NombreUsuario = usuario[1];
                     var RolUsuario = usuario[2];
                     var ApellidoUsuario = usuario[3];
                     var DpiUsuario = usuario[4];
                     var PassUsuario = usuario[5];

                     document.getElementById("<% = nombre.ClientID %>").value = NombreUsuario;
                     document.getElementById("<% = nickname.ClientID %>").value = NickUsuario;
                     document.getElementById("<% = apellido.ClientID %>").value = ApellidoUsuario;
                     document.getElementById("<% = dpi.ClientID %>").value = DpiUsuario;
                     document.getElementById("<% = Rol.ClientID %>").value = RolUsuario;
                     document.getElementById("<% = password.ClientID %>").value = PassUsuario;
                     document.getElementById("<% = password1.ClientID %>").value = PassUsuario;

                 }
             });
             
         }

         //Editar Usuario

         $('#edi').on('click', function () {

             var nick = document.getElementById("<%=nickname.ClientID%>").value;
             var id = document.getElementById("<%=codigo.ClientID%>").value;
             var pass = document.getElementById("<%=password.ClientID%>").value;
             var rol = document.getElementById("<% = Rol.ClientID %>").value;
             var nombre = document.getElementById("<% = nombre.ClientID %>").value;;
             var apellido = document.getElementById("<% = apellido.ClientID %>").value;;
             var dpi = document.getElementById("<% = dpi.ClientID %>").value;
             var nContrasena1 = document.getElementById("<% = password1.ClientID %>").value;

             if (pass == nContrasena1) {
                 if (nick != "" && pass != "" && rol != "" && nombre != "" && apellido != "") {


                     $.ajax({

                         type: 'POST',
                         url: 'User.aspx/EditUser',
                         data: JSON.stringify({ id: id, nickname: nick, nombre: nombre, apellido: apellido, rol: rol, pass: pass, dpi: dpi }),
                         contentType: 'application/json; charset=utf-8',
                         dataType: 'json',
                         success: function (response) {
                             if (response.d == true) {
                                 $('#mensaje').removeClass();
                                 $('#mensaje').addClass('alert alert-success').html('Usuario editado con exito').show(200).delay(5500).hide(200);
                             } else {
                                 $('#mensaje').removeClass();
                                 $('#mensaje').addClass('alert alert-danger').html('Nick ya existe con otro usuario').show(200).delay(5500).hide(200);

                             }



                         }

                     });

                 } else {
                     $('#mensaje').removeClass();
                     $('#mensaje').addClass('alert alert-danger').html('No deje campos obligatorios (*) vacios').show(200).delay(5500).hide(200);
                 }
             } else {
                 $('#mensaje').removeClass();
                 $('#mensaje').addClass('alert alert-danger').html('Contraseña no coincide').show(200).delay(5500).hide(200);
             }
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
                             if (response.d == true) {
                                 alert("Usuario Eliminado Exitosamente");
                             } else {
                                 alert("El Usuario No Pudo Ser Eliminado");
                             }
                         }
                     });
                 }

             }

             //AGREGAR USUARIO

             $('#reg').on('click', function () {

                 var nNick = document.getElementById("<%=nickname.ClientID%>").value;
                 var nNombre = document.getElementById("<%=nombre.ClientID%>").value;
                 var nApellido = document.getElementById("<%=apellido.ClientID%>").value;
                 var nDPI = document.getElementById("<%=dpi.ClientID%>").value;
                 var nContrasena = document.getElementById("<%=password.ClientID%>").value;
                 var nContrasena1 = document.getElementById("<%=password1.ClientID%>").value;
                 var nRol = document.getElementById("<%=Rol.ClientID%>").value;

                 if (nContrasena == nContrasena1) {
                     if (nNick != "" && nNombre != "" && nApellido != "" && nRol != "") {


                         $.ajax({
                             type: 'POST',
                             url: 'User.aspx/Add',
                             data: JSON.stringify({ nickname: nNick, nombre: nNombre, apellido: nApellido, dpi: nDPI, password: nContrasena, rol: nRol}),
                             contentType: 'application/json; charset=utf-8',
                             dataType: 'json',
                             success: function (response) {
                                 if (response.d == true) {
                                     $('#mensaje').removeClass();
                                     $('#mensaje').addClass('alert alert-success').html('Usuario agregado con exito').show(200).delay(5500).hide(200);
                                     $('#formulario-usuario')[0].reset();
                                 } else {
                                     $('#mensaje').removeClass();
                                     $('#mensaje').addClass('alert alert-danger').html('Nick ya existe').show(200).delay(5500).hide(200);

                                 }

                             }
                         });

                     } else {
                         $('#mensaje').removeClass();
                         $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                     }


                 }else{
                     $('#mensaje').removeClass();
                     $('#mensaje').addClass('alert alert-danger').html('Contrasena no coincide').show(200).delay(5500).hide(200);

                 }
                 return false;


             });


       


    </script>


</asp:Content>
