<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Rol.aspx.cs" Inherits="Proyecto1_Tel.Code.Rol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!-- Content wrapper -->
                 <!-- Breadcrumbs line -->
			    <div class="crumbs">
		            <ul id="breadcrumbs" class="breadcrumb"> 
		                <li><a href="../index.aspx">Inicio</a></li>
		                <li><a href="Rol.aspx">Roles</a></li>    
		            </ul>
			        </div>
		            
		
			
			    <!-- Page header -->
			    <div class="page-header">
			    	<div class="page-title">
				    	<h5>Roles</h5>
				    	<span>Agregar, Editar o Eliminar un rol</span>
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
	                           
                    <div style="align-content:center" class="span4"><a  id="nuevo-rol" class="btn btn-success btn-block">Agregar Rol</a></div>
                 </div>
            

    </section>

	    <h5 class="widget-name"><i class="icon-columns"></i>Roles</h5>
        <!-- Some controlы -->
        <div class="widget" id="tab_roles" runat="server">
        </div>
        <!-- /some controlы -->

    </div>

    <div>
        <h1 style="font-family: Calibri; font-size: 50px"></h1>
    </div>
    <div>
    </div>

    <!-- MODAL PARA EL REGISTRO DE PRODUCTOS-->
    <div class="modal fade" id="registra-rol" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
              <h4 class="modal-title" id="myModalLabel"><b>Administrar Rol</b></h4>
            </div>
            <form id="formulario" name="formulario" class="formulario">
            <div class="modal-body">
				<table border="0" width="100%">
               		<tr>
                           <td style="visibility:hidden; >
                               ID:
                           </td>
                           <td>
                               <input type="text" style="visibility:hidden; height:5px;" readonly="readonly" hidden="hidden" required="required" name="codigo" id="codigo" runat="server" maxlength="100"/>
                           </td>
               		</tr> 
                    <tr>
                    	<td>Nombre: </td>
                        <td><input type="text" name="nombre" id="nombre" runat="server" maxlength="100"/></td>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
        <script type="text/javascript">

            $('#reg').on('click', function () {

                var user = document.getElementById("<%=nombre.ClientID%>").value;
                if (user != "") {

                    $.ajax({

                        type: 'POST',
                        url: 'Rol.aspx/InsertRol',
                        data: JSON.stringify({ name: user }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            if (response.d == true) {
                                $('#formulario')[0].reset();
                                $('#mensaje').addClass('alert alert-success').html('Registro completado con exito').show(200).delay(2500).hide(200);
                                return false;
                            } else {
                                $('#formulario')[0].reset();
                                $('#mensaje').addClass('alert alert-danger').html('Rol ya existe').show(200).delay(2500).hide(200);
                            }
                            
                        }

                        

                    });

                }
                return false;
            });

           
            $('#edi').on('click', function () {
                
                var user = document.getElementById("<%=nombre.ClientID%>").value;
                var id = document.getElementById("<%=codigo.ClientID%>").value;
                if (user != "") {

                    $.ajax({

                        type: 'POST',
                        url: 'Rol.aspx/EditRol',
                        data: JSON.stringify({ name: user, id: id }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            if (response.d == true) {
                                
                                $('#mensaje').addClass('alert alert-success').html('Registro editado con exito').show(200).delay(2500).hide(200);
                                return false;
                            } else {
                                $('#formulario')[0].reset();
                                $('#mensaje').addClass('alert alert-danger').html('Nombre ya existe').show(200).delay(2500).hide(200);
                                $('#edi').html('Rol.aspx/Llenar_Roles');
                            }
                            
                        }
                        
                    });

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
            if (confirm("Esta seguro que desea eliminar al usuario?")) {
                var row = $(this).closest("tr");
                var RolId = id;
                $.ajax({
                    type: "POST",
                    url: "Rol.aspx/EliminarRol",
                    data: JSON.stringify({ id: RolId}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        row.remove();
                    }
                });
            }

        }
    </script>

</asp:Content>
