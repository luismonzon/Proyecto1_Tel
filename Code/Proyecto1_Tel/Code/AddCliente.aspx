<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="AddCliente.aspx.cs" Inherits="Proyecto1_Tel.Code.AddCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!---URL CLIENTE--->
    <li><a href="AddCliente.aspx">Agregar Cliente</a></li>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
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

	    <h5 class="widget-name"><i class="icon-columns"></i>Cliente</h5>
        <!-- Some controlы -->
        <div class="widget" id="tab_roles" runat="server">
        </div>
        <!-- /some controlы -->


        <form class="form-horizontal" action="#">
        <fieldset>   
            <!--<div class="navbar">
				<div class="navbar-inner">
					<h6>Formulario para Agregar Clientes</h6>
				</div>
			</div>
        -->
			<div class="widget row-fluid">
                <div class="span8 offset2">
                        <div class="widget">
                            <div class="navbar">
                                <div class="navbar-inner">
                                    <h6>Agregar Cliente</h6>
                                </div>
                            </div>
                            <div class="well body">
                                    <div class="well">
                                        <div class="control-group">
		                                    <label class="control-label">*Nombre y Apellido:</label>
		                                    <div class="controls"><input type="text" required="required" name="user" class="span12" placeholder="Nombre Completo" id="nombre" runat="server" /></div>

		                                    <label class="control-label">*Nit:</label>
		                                    <div class="controls"><input class="span12" required="required" type="text" name="pass" placeholder="Nit" id="nit" runat="server" /></div>
                                            <br />
                                            <label>Los campos marcados con (*) son obligatorios</label>
                                            <div id="dvResult"></div>
                                             <div id="mensaje"> </div> 

                                            <div class="form-actions align-right">
	                                            <input type="submit" id="agregar-cliente" class="btn btn-primary" value="Guardar" />
	                                            <input onclick="location.href = 'Cliente.aspx';" type="submit"  class="btn btn-danger" value="Cancelar" />
                                                
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


<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">

     <script type="text/javascript">

         $('#agregar-cliente').on('click', function () {
             $("#dvResult").text("");
             var nNombre = document.getElementById("<%=nombre.ClientID%>").value;
             var nNit = document.getElementById("<%=nit.ClientID%>").value;
             
             
             if (nNombre != "" && nNit != "") {
                 

                     $.ajax({
                         type: 'POST',
                         url: 'AddCliente.aspx/Add',
                         data: JSON.stringify({ nombre: nNombre, nit: nNit }),
                         contentType: 'application/json; charset=utf-8',
                         dataType: 'json',
                         success: function (response) {
                             if (response.d == true) {
                                 $('#mensaje').removeClass();
                                 $('#mensaje').addClass('alert alert-success').html('Cliente agregado con exito').show(200).delay(2500).hide(200);
                                 document.getElementById("<%=nombre.ClientID%>").value = "";
                                 document.getElementById("<%=nit.ClientID%>").value = "";
             
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
