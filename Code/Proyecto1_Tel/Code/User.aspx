<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="Proyecto1_Tel.Code.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--- URL DE LA PAGINA --->
    <li><a href="User.aspx">Usuarios</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <!-- MODAL -->
    <input runat="server" type="text" required="required" readonly="readonly" name="codigo" id="codigo"  style="visibility:hidden; height:5px;"/>
        <div id="modalusuario" runat="server">
        
        </div>
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



</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">

    <!--- COGIGO JAVASCRIPT --->


     <script type="text/javascript">

         function FillCombo() {
             $.ajax({
                 type: 'POST',
                 url: 'User.aspx/Fill',
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (response) {
                     var data = JSON.parse(response.d);
                     var $select = $('#Rol');
                     var options = '';
                     for (var i = 0; i < data.length; i++) {
                         options += '<option value="' + data[i].key + '">' + data[i].value + '</option>';
                     }
                     $select.html(options);
                 }
             });
         }

        $('#nuevo-usuario').on('click', function () {
            $.ajax({
                type: 'POST',
                url: 'User.aspx/MostrarModal',
                data: JSON.stringify({ id: "" }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    //se escribe el modal
                    var $modal = $('#ContentPlaceHolder1_modalusuario');
                    $modal.html(response.d);
                    $('#editar-usuario').on('show.bs.modal', function () {
                        $('.modal .modal-body').css('overflow-y', 'auto');
                        $('.modal .modal-body').css('max-height', $(window).height() * 0.7);
                        $('.modal .modal-body').css('height', $(window).height() * 0.7);
                    });

                    //MUESTRA EL MODAL PARA AGREGAR USUARIO
                    FillCombo();
                    $('#formulario-usuario')[0].reset(); //formulario lo inicializa con datos vacios
                    $('#edi').hide(); //escondemos el boton de edicion porque es un nuevo registro
                    $('#reg').show(); //mostramos el boton de registro
                    $('#editar-usuario').modal({ //
                        show: true, //mostramos el modal registra producto
                        backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                    });
                }
            });

        });
         function Mostrar_usuario(id) {
             document.getElementById("<% = codigo.ClientID%>").value = id;
             var identi = document.getElementById("<% = codigo.ClientID%>").value;
             $.ajax({
                 type: 'POST',
                 url: 'User.aspx/MostrarModal',
                 data: JSON.stringify({ id: identi }),
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (response) {
                     //se escribe el modal
                     var $modal = $('#ContentPlaceHolder1_modalusuario');
                     $modal.html(response.d);
                     $('#editar-usuario').on('show.bs.modal', function () {
                         $('.modal .modal-body').css('overflow-y', 'auto');
                         $('.modal .modal-body').css('max-height', $(window).height() * 0.7);
                         $('.modal .modal-body').css('height', $(window).height() * 0.7);
                     });
                     FillCombo();
                     //Acciones Anteriores
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

                             document.getElementById("nombre").value = NombreUsuario;
                             document.getElementById("nickname").value = NickUsuario;
                             document.getElementById("apellido").value = ApellidoUsuario;
                             document.getElementById("dpi").value = DpiUsuario;
                             document.getElementById("Rol").value = RolUsuario;
                             document.getElementById("password").value = PassUsuario;
                             document.getElementById("password1").value = PassUsuario;

                         }
                    });
                 }
             });


             
             
         }

         //Editar Usuario
         function Edit(){

             var nick = document.getElementById("nickname").value;
             var id = document.getElementById("<%=codigo.ClientID%>").value;
             var pass = document.getElementById("password").value;
             var rol = document.getElementById("Rol").value;
             var nombre = document.getElementById("nombre").value;;
             var apellido = document.getElementById("apellido").value;;
             var dpi = document.getElementById("dpi").value;
             var nContrasena1 = document.getElementById("password1").value;

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

         }

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
                                 reloadTable();
                             } else {
                                 alert("El Usuario No Pudo Ser Eliminado");
                             }
                         }
                     });
                 }

             }

             //AGREGAR USUARIO
             function AddUser() {

                 var nNick = document.getElementById("nickname").value;
                 var nNombre = document.getElementById("nombre").value;
                 var nApellido = document.getElementById("apellido").value;
                 var nDPI = document.getElementById("dpi").value;
                 var nContrasena = document.getElementById("password").value;
                 var nContrasena1 = document.getElementById("password1").value;
                 var nRol = document.getElementById("Rol").value;
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


             }


       


    </script>


</asp:Content>
