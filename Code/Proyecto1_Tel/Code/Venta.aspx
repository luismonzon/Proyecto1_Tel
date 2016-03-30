<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="Proyecto1_Tel.Code.Venta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
        <li><a href="Venta.aspx">Ventas</a></li>    


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


	    <h4 class="widget-name"><i class="icon-columns"></i>Ventas</h4>
    
    <div class="row-fluid">
	            

	            	
	                <!-- Column -->
	                <div class="span12">
	                	
	                    <!-- Horizontal form -->
	                    <div class="form-horizontal">
	                        <div class="widget">
	                            <div class="navbar"><div class="navbar-inner"><h6>Cliente</h6>
	                                <button  type="submit" id="nuevo-cliente" class="btn btn-success pull-right">Agregar Cliente</button></div></div>

	                            <div class="well">
	                             <div class="control-group">
	                                    <label class="control-label">Buscar</label>
	                                    <div class="controls">
                                            <div class="align-left">
                                                <input type="text" name="regular" class="span2" id="idcliente" placeholder="Cod. Cliente" />
	                                         
                                            <button  id="busca" class="btn btn-primary">Buscar</button>
                                             </div>
	                                    </div>
	                                </div>
	                                
	                                 <div class="control-group">    
                                         
                                         <div class="span6">
	                                        <label class="control-label">Codigo</label>
	                                          <div class=" align-left"> 
                                                <input type="text" name="regular" disabled="disabled" class="span2" id="codigo_cliente" placeholder="Cod. Cliente" />
                                                </div>
	                                    </div>
	                                
	                                    <div class="span6">
	                                        <label class="control-label">Nombre</label>
	                                          <div class=" align-left"> 
                                            <input type="text" name="regular"  disabled="disabled" class="span6" id="nombre_cliente" placeholder="Nombre del Cliente" />
                                            </div>
	                                    </div>
                                                               
	                                 </div>
	                                <div class="control-group">
	                                    <div class="span6">
	                                        <label class="control-label">Nit Cliente</label>
	                                          <div class=" align-left"> 
                                                <input type="text" name="regular"  disabled="disabled" class="span6" id="nit_cliente" placeholder="Nit del Cliente" />
                                            </div>
	                                    </div>
	                                
	                                    <div class="span6">
	                                        <label class="control-label">Telefono Cliente</label>
	                                          <div class=" align-left"> 
                                                <input type="text" name="regular"  disabled="disabled" class="span6" id="tel_cliente" placeholder="Telefono del Cliente" />

                                            </div>
	                                    </div>

	                                </div>
	                                
                                    <div class="control-group">
	                                        <label class="control-label">Direccion del Cliente</label>
	                                          <div class=" align-left"> 
                                                <input type="text" name="regular"  disabled="disabled" class="span8" id="dir_cliente" placeholder="Direccion del Cliente" />
                                            </div>
	                                    </div>

	                            </div>
	                            
	                        </div>
	                    </div>
	                    <!-- /horizontal form -->
	                	
	                </div>
	                <!-- /column --> 
	            </div>

    	            	<!-- Column -->
	                <div class="span6">
                        
	                    <div class="form-horizontal">
	                                <div class="widget">
	                            <div class="navbar"><div class="navbar-inner"><h6>Nueva Venta</h6></div></div>

	                            <div class="well">
	                            
                                    <div  class="control-group">
                                        <label class="control-label">Agregar Producto</label>
                                        
                                            <div class="align-left">
                                                <div id="productos" class="span6"  runat="server">
                                                </div>
                                                <button  id="agregar" class="btn btn-primary ">Agregar</button>
                                             </div>
                                    </div>
	                              
	                                <div class="form-actions align-right">
	                                    <button type="submit" id="submit" class="btn btn-primary">Aceptar</button>
	                                    <button type="button" class="btn btn-danger">Cancelar</button>
	                                    <button type="reset" class="btn">Limpiar</button>
	                                </div>

	                            </div>
	                            
	                        </div>
	                   </div>

	                    
	                </div>
	                <!-- /column -->

            <!-- MODAL PARA AGREGAR CLIENTES CLIENTES-->
            <div id="divmodal" runat="server">
    
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
	            data: JSON.stringify({ idcliente: id_cliente }),
	            contentType: 'application/json; charset=utf-8',
	            dataType: 'json',
	            success: function (msg) {
	                // Notice that msg.d is used to retrieve the result object
	                if (msg.d == "0") {
	                    alert("Cliente no existe");
	                }
	                else {
	                    var datos = msg.d.split(",");
	                    $("#codigo_cliente").val(datos[3]);
	                    $("#nit_cliente").val(datos[1]);
	                    $("#nombre_cliente").val(datos[0] + " " + datos[2]);
	                    $("#tel_cliente").val(datos[5]);
	                    $("#dir_cliente").val(datos[4]);
	                }
	            }
	        });
	    });

	    $('#nuevo-cliente').on('click', function () { // nuevo-cliente es el id del boton para agregar al cliente
	        $.ajax({
	            type: 'POST',
	            url: 'Venta.aspx/MostrarModal',
	            data: JSON.stringify({ id: "" }),
	            contentType: 'application/json; charset=utf-8',
	            dataType: 'json',
	            success: function (response) {
	                //se escribe el modal
	                var $modal = $('#ContentPlaceHolder1_divmodal');
	                $modal.html(response.d);
	                $('#Modal').on('show.bs.modal', function () {
	                    $('.modal .modal-body').css('overflow-y', 'auto');
	                    $('.modal .modal-body').css('max-height', $(window).height() * 0.7);
	                    $('.modal .modal-body').css('height', $(window).height() * 0.7);
	                });

	                //Modal
	                $('#formulario')[0].reset(); //formulario lo inicializa con datos vacios
	                $('#pro').val('Registro'); //crea nuestra caja de procesos y se agrega el valor del registro
	                $('#reg').show(); //mostramos el boton de registro
	                $('#edi').hide();//se esconde el boton de editar
	                $('#Modal').modal({ //
	                    show: true, //mostramos el modal registra producto
	                    //backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
	                });
	            }
	        });


	    });


	    //AGREGAR CLIENTE

	    function AddClient() {

	        var nNombre = document.getElementById("nombre").value;
	        var nNit = document.getElementById("nit").value;
	        var nDireccion = document.getElementById("direccion").value;
	        var nTelefono = document.getElementById("telefono").value;
	        var nApellido = document.getElementById("apellido").value;



	        if (nNombre != "") {


	            $.ajax({
	                type: 'POST',
	                url: 'Venta.aspx/Add',
	                data: JSON.stringify({ nombre: nNombre, nit: nNit, apellido: nApellido, direccion: nDireccion, telefono: nTelefono }),
	                contentType: 'application/json; charset=utf-8',
	                dataType: 'json',
	                success: function (response) {
	                    if (response.d == true) {
	                        alert("aqui");
	                        $.ajax({
	                            type: 'POST',
	                            url: 'Venta.aspx/BuscarCliente',
	                            data: JSON.stringify({ nombre: nNombre, nit: nNit, apellido: nApellido, direccion: nDireccion, telefono: nTelefono }),
	                            contentType: 'application/json; charset=utf-8',
	                            dataType: 'json',
	                            success: function (response) {

	                                var cliente = JSON.parse(response.d);
	                                var idCliente = cliente[0];

	                                document.getElementById("codigo").value = idCliente;
	                                $('#mensaje').removeClass();
	                                $('#mensaje').addClass('alert alert-success').html('Cliente agregado con exito').show(200).delay(2500).hide(200);


	                            }

	                        });




	                    } else {
	                        $('#mensaje').removeClass();
	                        $('#mensaje').addClass('alert alert-danger').html('Nit ya existe').show(200).delay(2500).hide(200);

	                    }

	                }
	            });

	        } else {
	            $('#mensaje').removeClass();
	            $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(2500).hide(200);

	        }
	        return false;


	    }

	    function closeModal() {


	        $('#Modal').modal('toggle');
	        var $modal = $('#ContentPlaceHolder1_modaldetalle');
	        $modal.html("");

	    }

	    function cambio() {
	        var combo = document.getElementById("cmbproductos");

	        var selected = $("#cmbproductos  option:selected").text();

	        if (selected == "Articulo") {
	            $('#divtam').hide();
	            $('#divporc').hide();
	        } else {
	            $('#divtam').show();
	            $('#divporc').show();
	        }


	    }

	</script>                    
	                    <!-- /horizontal form -->




</asp:Content>
