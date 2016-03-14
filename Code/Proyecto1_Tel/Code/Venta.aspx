<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="Proyecto1_Tel.Code.Venta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
        <li><a href="Rol.aspx">Roles</a></li>    


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		            
		

	    <h5 class="widget-name"><i class="icon-columns"></i>Ventas</h5>
        <!-- Some controlы -->
        <div class="widget" id="tab_roles" runat="server">
        </div>
        <!-- /some controlы -->


    <div>
        <h1 style="font-family: Calibri; font-size: 50px"></h1>
    </div>
    <div class="form-horizontal">
	                        <div class="widget">
	                            <div class="navbar"><div class="navbar-inner"><h6>Nueva Venta</h6></div></div>

	                            <div class="well">
	                            
	                                <div class="control-group">
	                                    <label class="control-label">Codigo Cliente</label>
	                                    <div class="controls">
                                            <input type="text" name="regular" class="span12" id="idcliente" placeholder="Cod. Cliente" />
	                                        <button  id="busca" class="btn btn-primary">Buscar</button>

	                                    </div>
	                                </div>
	                                 <div class="control-group">
	                                    <label class="control-label">Codigo Cliente</label>
	                               
                                            <input type="text" name="regular" disabled="disabled" class="span12" id="codigo_cliente" placeholder="Cod. Cliente" />

	                                 </div>
	                                <div class="control-group">
	                                    <label class="control-label">Nombre Cliente</label>
	                               
                                            <input type="text" name="regular"  disabled="disabled" class="span12" id="nombre_cliente" placeholder="Nombre Cliente" />

	                                </div>
	                                
	                                <div class="control-group">
	                                    <label class="control-label">Nit Cliente</label>
	                                      <input type="text" name="regular"  disabled="disabled" class="span12" id="nit_cliente" placeholder="Nit Cliente" />

	                                </div>
	                                
	                              
	                               
	                                
	                                <div class="form-actions align-right">
	                                    <button type="submit" id="submit" class="btn btn-primary">Submit</button>
	                                    <button type="button" class="btn btn-danger">Cancel</button>
	                                    <button type="reset" class="btn">Reset</button>
	                                </div>

	                            </div>
	                            
	                        </div>
	                    </div>

    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
       
      <!-- Horizontal form -->
	<script type="text/javascript">
	    $('#busca').on('click', function () {
            
	        var id_cliente = document.getElementById('idcliente').value;

	        $.ajax({
	            type: 'POST',
	            url: 'Venta.aspx/Busca',
	            data: JSON.stringify({ idcliente:  id_cliente }),
	            contentType: 'application/json; charset=utf-8',
	            dataType: 'json',
	            success: function (msg) {
	                // Notice that msg.d is used to retrieve the result object
	                if (msg.d == "0") {

	                    alert("no existe");

	                }
	                else {
	                    var datos = msg.d.split(",");

	                    $("#codigo_cliente").val(datos[3]);
	                    $("#nit_cliente").val(datos[1]);
	                    $("#nombre_cliente").val(datos[0]+" "+datos[2]);

	                }
	            }
	        });
	    });


	</script>                    
	                    <!-- /horizontal form -->

</asp:Content>


