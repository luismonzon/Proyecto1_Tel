<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="Proyecto1_Tel.Code.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">

        $('#submit').on('click', function () {
            $("#dvResult").text("");
            var user = document.getElementById("<%=user.ClientID%>").value;
            var pass = document.getElementById("<%=password.ClientID%>").value;
            var rol = document.getElementById("<%=Rol.ClientID%>").value;
            var msg = "";

            if (user == "") {
                msg += "<li>Por Favor ingrese el usuario</li>";
            }
            if (pass == "") {
                msg += "<li>Por Favor ingrese la contraseña</li>";
            }
            if (msg.length == 0) {
                $.ajax({

                    type: 'POST',
                    url: 'AddUser.aspx/Add',
                    data: JSON.stringify({ usuario: user, password: pass, rol: rol }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        if (response.d == true) {
                            $('#dvResult').text("Usuario agregado Correctamente");
                        } else {
                            $('#dvResult').text("Usuario no pudo ser agregado Correctamente");
                        }
                        $('#dvResult').fadeOut(6000);
                    },
                    error: function (xhr, textStatus, error) {
                        $('#dvResult').text("Error: " + error);
                    }


                });
            } else {
                //Validation failure message
                $('#dvResult').html('');
                $('#dvResult').html(msg);
            }
            $('#dvResult').fadeIn();


        });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        

             <!-- Breadcrumbs line -->
			    <div class="crumbs">
		            <ul id="breadcrumbs" class="breadcrumb"> 
		                <li><a href="index.html">Dashboard</a></li>
		                <li class="active"><a href="AddUser.aspx" title="">Usuario</a></li>
		            </ul>
			        
		            
			    </div>
			    <!-- /breadcrumbs line -->

			    <!-- Page header -->
			    <div class="page-header">
			    	<div class="page-title">
				    	<h5>Tables</h5>
				    	<span>Static and dynamic tables</span>
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

	    <h5 class="widget-name"><i class="icon-columns"></i>Agregar Usuario</h5>
        <!-- Some controlы -->
        <div class="widget" id="tab_roles" runat="server">
        </div>
        <!-- /some controlы -->


        <form class="form-horizontal" action="#">
        <fieldset>   
            <!--<div class="navbar">
				<div class="navbar-inner">
					<h6>Formulario para Agregar Usuarios</h6>
				</div>
			</div>
        -->
			<div class="widget row-fluid">
                <div class="span8 offset2">
                        <div class="widget">
                            <div class="navbar">
                                <div class="navbar-inner">
                                    <h6>Formulario para Agregar Usuario</h6>
                                </div>
                            </div>
                            <div class="well body">
                                    <div class="well">
                                        <div class="alert margin">
	                                		<button type="button" class="close" data-dismiss="alert">×</button>
	                                		Debe llenar todos los Campos
	                                	</div>
	                                    <div class="control-group">
		                                    <label class="control-label">Usuario:</label>
		                                    <div class="controls"><input type="text" name="user" class="span12" placeholder="Usuario" id="user" runat="server" /></div>

		                                    <label class="control-label">Contraseña:</label>
		                                    <div class="controls"><input class="span12" type="password" name="pass" placeholder="Contraseña" id="password" runat="server" /></div>

	                                        <label class="control-label">Rol:</label>
	                                        <div class="controls">
	                                            <select name="Rol" class="styled" runat="server" id="Rol">

	                                            </select>
	                                        </div>
                                            <br />
                                            <div id="dvResult"></div>
                                            <div class="form-actions align-right">
	                                            <button type="submit" id="submit" class="btn btn-primary" onclick="Save() return false;">Ingresar</button>
	                                            <button type="button" class="btn btn-danger">Cancelar</button>
	                                        </div>
	                                    </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                            
			
            </div>
	    </fieldset>
        
        </form>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">

    



</asp:Content>