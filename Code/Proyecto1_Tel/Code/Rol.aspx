<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Rol.aspx.cs" Inherits="Proyecto1_Tel.Code.Rol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
        <li><a href="Rol.aspx">Roles</a></li>    


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		            
		

	    <h5 class="widget-name"><i class="icon-columns"></i>Roles</h5>
    <div>
        <div><a style="font-size: 13px" id="nuevo-rol" class="btn btn-success"> Agregar Rol <i class="icon-plus-sign" >&nbsp;</i></a></div>
    </div>
    
        <!-- Some controlы -->
        <div class="widget" id="tab_roles" runat="server">
        </div>
        <!-- /some controlы -->

    <div>
        <h1 style="font-family: Calibri; font-size: 50px"></h1>
    </div>
    <div>
    </div>

    <!-- MODAL PARA EL REGISTRO DE ROLES-->
    <div class="modal fade" id="registra-rol" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>

                <div class="step-title">
                            	<i>R</i>
					    		<h5>Administrar Rol</h5>
					    		<span>Agregar o Editar un Rol</span>
				</div>

            </div>
            <form id="formulario" name="formulario" class="formulario">
            <div class="modal-body">
				<table border="0" width="100%">
               		<tr>
                           <td style="visibility:hidden; height:5px;" >ID </td>
                        <td colspan="2"><input runat="server" type="text" required="required" readonly="readonly" id="codigo" name="codigo"  style="visibility:hidden; height:5px;"/></td>

               		</tr> 
                    <tr>
                    	<td style="font-size: 15px;">*Nombre Rol: </td>
                        <td><input type="text" style="font-size: 15px;" required="required" name="nombre" id="nombre" runat="server" maxlength="100"/></td>
                    </tr>
                    <tr>
                    	<td colspan="2">
                        	<div class="alert margin">
	                            <button type="button" class="close" data-dismiss="alert">×</button>
	                                Campos Obligatorios (*)
	                        </div>
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


        <script type="text/javascript">

            $('#reg').on('click', function () {

                var nombre = document.getElementById("<%=nombre.ClientID%>").value;
                if (nombre != "") {

                    $.ajax({

                        type: 'POST',
                        url: 'Rol.aspx/InsertRol',
                        data: JSON.stringify({ name: nombre }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            if (response.d == true) {
                                $('#formulario')[0].reset();
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-success').html('Rol completado con exito').show(200).delay(2500).hide(200);
                                
                            } else {
                                $('#formulario')[0].reset();
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-danger').html('Rol ya existe').show(200).delay(2500).hide(200);
                                
                            }
                            
                        }
                    });

                } else {
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(2500).hide(200);

                }
                
                return false;
            });

           
            $('#edi').on('click', function () {
                
                var nombre = document.getElementById("<%=nombre.ClientID%>").value;
                var id = document.getElementById("<%=codigo.ClientID%>").value;

                if (nombre != "") {

                    $.ajax({

                        type: 'POST',
                        url: 'Rol.aspx/EditRol',
                        data: JSON.stringify({ name: nombre, id: id }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            if (response.d == true) {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-success').html('Rol editado con exito').show(200).delay(2500).hide(200);
                                return false;
                            } else {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-danger').html('Rol ya existe').show(200).delay(2500).hide(200);
                                $('#edi').html('Rol.aspx/Llenar_Roles');
                            }
                            
                        }
                        
                    });

                } else {
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('No debe dejar campos vacios').show(200).delay(2500).hide(200);
                    Mostrar(id);
                    
                }
                return false;
            });

    </script>

    <script type="text/javascript">
        
        function Mostrar( id ) {
            document.getElementById("<% = codigo.ClientID%>").value = id;
            var identi = document.getElementById("<% = codigo.ClientID%>").value;  
            
            $('#edi').show(); //escondemos el boton de edicion porque es un nuevo registro
            $('#reg').hide(); //mostramos el boton de registro
            $('#registra-rol').modal({ //
                show: true, //mostramos el modal registra producto
                backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
            });

            $.ajax({
                type: 'POST',
                url: 'Rol.aspx/BuscarRol',
                data: JSON.stringify({ id: identi }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    
                    var RolNombre = response.d;
                    document.getElementById("<% = nombre.ClientID%>").value = RolNombre;
                }
            });
        }

    </script>
    <script type="text/javascript">
        function Eliminar(id) {
            if (confirm("Esta seguro que desea eliminar el Rol?")) {
                var row = $(this).closest("tr");
                var RolId = id;
                $.ajax({
                    type: "POST",
                    url: "Rol.aspx/EliminarRol",
                    data: JSON.stringify({ id: RolId}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d == true) {
                            alert("Rol eliminado con exito");
                        } else {
                            alert("Rol no se pudo eliminar");
                        }
                       
                    }
                });
            }

        }


        


    </script>

</asp:Content>
