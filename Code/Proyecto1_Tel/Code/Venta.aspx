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
	                                <button  type="submit" id="nuevo-cliente" class="btn btn-success pull-right"><b>Agregar Cliente</b></button></div></div>

	                            <div class="well">
	                             <div class="control-group">
	                                    
                                        <div class="span1">
                                            <label class="control-label" style="font-size: 15px;"><b>Buscar:</b></label>
                                        </div>
                                        <div class="span2">
                                            
                                            <div class="align-left">
                                                <div id="sclientes" class="span6"  runat="server">
                                                </div>
                                             </div>
                                        </div>
                                      <input type="text" name="regular" style="font-size: 14px; visibility:hidden;" disabled="disabled" id="codigo_cliente" placeholder="Cod. Cliente" />
                                       
                                     <!--
	                                    <div class="span6">
                                                <label style="font-size: 15px;" class="control-label"><b>Buscar</b> </label>
                                                
                                                <input type="text" name="regular" class="span3" style="font-size: 14px;" id="idcliente" placeholder="Cod. Cliente" />
	                                         
                                            <button  id="busca" class="btn btn-primary">Buscar</button>
                                             
	                                    </div>
                                        <div class="span6">
	                                        <label class="control-label" style="font-size: 15px;">Codigo</label>
	                                          <div class=" align-left"> 
                                                <input type="text" name="regular" style="font-size: 14px;" disabled="disabled" class="span4" id="codigo_cliente" placeholder="Cod. Cliente" />
                                                </div>
	                                    </div>
	                                
                                     -->
	                                </div>
	                                
	                                 <div class="control-group">    
                                         
                                      
	                                    <div class="span6">
	                                        <label class="control-label" style="font-size: 15px;"> <b>Nombre:</b></label>
	                                          <div class=" align-left"> 
                                            <input type="text" name="regular" style="font-size: 14px;"  disabled="disabled" class="span8" id="nombre_cliente" placeholder="Nombre del Cliente" />
                                            </div>
	                                    </div>
                                         <div class="span6">
	                                        <label class="control-label" style="font-size: 15px;"> <b>Nit:</b></label>
	                                          <div class=" align-left"> 
                                                <input type="text" name="regular" style="font-size: 14px;" disabled="disabled" class="span8" id="nit_cliente" placeholder="Nit del Cliente" />
                                            </div>
	                                    </div>
	                                                    
	                                 </div>
	                                <div class="control-group">
                                         <div class="span6">
	                                        <label class="control-label" style="font-size: 15px;"><b>Telefono:</b></label>
	                                          <div class=" align-left"> 
                                                <input type="text" name="regular" style="font-size: 14px;" disabled="disabled" class="span8" id="tel_cliente" placeholder="Telefono del Cliente" />

                                            </div>
	                                    </div>
                                        <div class="span6">
	                                        <label class="control-label" style="font-size: 15px;"><b>Comercio:</b></label>
	                                          <div class=" align-left"> 
                                                <input type="text" name="regular" style="font-size: 14px;" disabled="disabled" class="span8" id="comercio" placeholder="Nombre Comercio" />

                                            </div>
	                                    </div>

	                                </div>
	                                
                                    <div class="control-group">
	                                        <label class="control-label" style="font-size: 15px;"><b>Direccion del Cliente</b></label>
	                                          <div class=" align-left"> 
                                                <input type="text" name="regular" style="font-size: 15px;" disabled="disabled" class="span8" id="dir_cliente" placeholder="Direccion del Cliente" />
                                            </div>
	                                    </div>

	                            </div>
	                            
	                        </div>
	                    </div>
	                    <!-- /horizontal form -->
	                	
	                </div>
	                <!-- /column --> 
                     

	            </div>
    <div class="row-fluid">
          	<!-- Column -->
	                <div class="span12">
                        
	                    <div class="form-horizontal">
	                                <div class="widget">
	                            <div class="navbar"><div class="navbar-inner"><h6>Nueva Venta</h6></div></div>

	                            <div class="well">
	                            
                                         
                                    <div  class="control-group">

                                        
                                        <div class="span6">
                                            <label class="control-label" style="font-size: 15px;"><b>Unidades</b></label>
                                            <div class="align-left">
                                                <input type="number" style="font-size: 11px;" name="regular"  class="span3" id="txtcantidad" placeholder="Cantidad" />
                                            </div>                                                   
                                         </div>

                                        <div class="span1">
                                            <label class="control-label" style="font-size: 15px;"><b>Producto</b></label>
                                        </div>
                                        <div class="span2">
                                            
                                            <div class="align-left">
                                                <div id="productos" class="span6"  runat="server">
                                                </div>
                                             </div>
                                        </div>
                                        
                                    </div>
                                       
                                     <div class="control-group">
                                         
                                        
                                        <div id="metros" class="span5">
                                            <label class="control-label" style="font-size: 15px;"><b>Metros</b></label>
                                            <div class=" align-left"> 
                                                <input type="text" style="font-size: 13px;" name="regular"  class="span5" id="txtmetros" placeholder="Metros" />
                                                       
                                            </div>
                                         </div>
                                         <div class="span2">
                                             <label class="checkbox">
													<input type="checkbox" value="" class="styled" id="pulgadas">
													Pulgadas
												</label>
                                         </div>
                                         <div class="span5">
                                                    <label class="control-label" style="font-size: 15px;"><b>Cantidad Disponible</b></label>
                                                     <div class="align-left">
                                                         <input type="text" style="font-size: 13px;" name="regular" readonly="readonly" class="span3" id="disponible"  />
                                                     </div>                                                   
                                         </div>
                                         
                                         <button  id="agregar" style="font-size: 15px;" class="btn btn-success align-right">Agregar Producto</button>
                                           
                                    </div>

                                      <input  type="text" style="visibility:hidden; height:5px;" name="regular" readonly="readonly" class="span1" id="idventa" hidden="hidden"/>
                                   
                                    
                                               
                                            
                                                
                                    <div class="control-group" id="detalleproductos">


                                    </div>
	                              
	                                <div class="form-actions align-right">
	                                    <button type="submit" id="comprar" class="btn btn-primary">Aceptar</button>
	                                    <button type="reset" id="cleancarrito" class="btn">Limpiar</button>
	                                </div>

	                            </div>
	                            
	                        </div>
	                   </div>

	                    
	                </div>
        </div>
   



    	         
	                <!-- /column -->

            <!-- MODAL PARA AGREGAR CLIENTES CLIENTES-->
            <div id="divmodal" runat="server">
    
            </div>
            
            <div id="divpago" runat="server">
    
            </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">





    <script type="text/javascript">

        function cambioscliente()
        {
            var id = document.getElementById('cmbclientes').value;
            var selected = $("#cmbclientes  option:selected").text();

            $.ajax({
                type: 'POST',
                url: 'Venta.aspx/Busca',
                data: JSON.stringify({ idcliente: id }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    // Notice that msg.d is used to retrieve the result object
                    if (msg.d == "0") {
                        alert("Cliente no existe");
                    }
                    else {
                        var datos = JSON.parse(msg.d);
                        $("#codigo_cliente").val(datos[3]);
                        $("#nit_cliente").val(datos[1]);
                        $("#nombre_cliente").val(datos[0]);
                        $("#comercio").val(datos[2]);
                        $("#tel_cliente").val(datos[5]);
                        $("#dir_cliente").val(datos[4]);
                    }
                }
            });

        }



        function cambios() {

            var id = document.getElementById('cmbproductos').value;
            var selected = $("#cmbproductos  option:selected").text();

            $.ajax({
                type: 'POST',
                url: 'Venta.aspx/Busca_Datos',
                data: JSON.stringify({ id: id }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {

                    var produc = JSON.parse(response.d);
                    var Descripcion = produc[0];
                    var Cantidad_Dispo = produc[1];
                    var Tipo = produc[2];
                    var Precio = produc[3];
                    var Metros = produc[4];



                    if (Tipo == "ARTICULO" || Tipo == "Articulo") {

                        $('#metros').hide();
                        document.getElementById("disponible").value = Cantidad_Dispo;
                    } else {

                        $('#metros').show();
                        document.getElementById("disponible").value = Metros;

                    }


                }
            });

        }



        $('#comprar').on('click', function () { // modal pago

            var client = $("#codigo_cliente").val();
            if (client != "") {

                $.ajax({
                    type: 'POST',
                    url: 'Venta.aspx/MostrarModalPago',
                    data: JSON.stringify({ cliente: client }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        if (response.d == '0') {
                            alert('Carrito Vacio');
                        } else {


                            //se escribe el modal
                            var $modal = $('#ContentPlaceHolder1_divpago');
                            $modal.html(response.d);
                            $('#ModalPago').on('show.bs.modal', function () {
                                $('.modal .modal-body').css('overflow-y', 'auto');
                                $('.modal .modal-body').css('max-height', $(window).height() * 0.7);
                                $('.modal .modal-body').css('height', $(window).height() * 0.7);
                            });

                            //Modal
                            $('#formulario_modal')[0].reset(); //formulario lo inicializa con datos vacios
                            $('#pro_modal').val('Registro'); //crea nuestra caja de procesos y se agrega el valor del registro
                            $('#reg_modal').show(); //mostramos el boton de registro
                            $('#edi_modal').hide();//se esconde el boton de editar
                            $('#ModalPago').modal({ //
                                show: true, //mostramos el modal registra producto
                                //backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                            });
                        }
                    }
                });
            } else {
                alert("Debe elegir un cliente primero!!");
            }


        });



        function removecarrito(val) {
            $.ajax({
                type: 'POST',
                url: 'Venta.aspx/quitarcantidad',
                data: JSON.stringify({ codigo: val }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {

                    var eliminado = parseFloat(data.d);
                    var anterior = parseFloat($("#disponible").val());

                    var dispon = anterior + eliminado;

                    document.getElementById("disponible").value = dispon;

                    $.ajax({
                        type: 'POST',
                        url: 'Venta.aspx/removecarrito',
                        data: JSON.stringify({ codigo: val }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (msg) {

                            $("#detalleproductos").html(msg.d);
                        }
                    });


                }
            });

        }

    </script>
   
    <script type="text/javascript">
        $('#agrega').on('click', function () {
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
                        var str = msg.d + "";
                        var datos = str.split(';');
                        $("#codigo_cliente").val(datos[3]);
                        $("#nit_cliente").val(datos[1]);
                        $("#nombre_cliente").val(datos[0]);
                        $("#comercio").val(datos[2]);
                        $("#tel_cliente").val(datos[5]);
                        $("#dir_cliente").val(datos[4]);
                    }
                }
            });
        });




        $('#agregar').on('click', function () {

            var cod = $("#cmbproductos").val();
            var nombre = $("#cmbproductos  option:selected").text();
            var cant = $("#txtcantidad").val();
            var largo = $("#txtmetros").val();
            if (largo == "") {
                largo = 1;
            }
            var disp = $("#disponible").val();

            var idventa = parseFloat(1);
            var venta = $("#idventa").val();

            if (venta == "") {
                document.getElementById("idventa").value = idventa;

            } else {
                document.getElementById("idventa").value = parseFloat(venta) + parseFloat(1);
            }

            if ($('#pulgadas').is(":checked")) {
                largo = largo * 0.0254;
                largo = Math.round(largo * Math.pow(10, 2)) / Math.pow(10, 2);
            }

            var cantidad = cant * largo;
            if (cantidad <= disp) {
                var disponible = $("#disponible").val() - cantidad;
                document.getElementById("disponible").value = disponible;

                var iventa = $("#idventa").val();
                $.ajax({
                    type: 'POST',
                    url: 'Venta.aspx/AddProducto',
                    data: JSON.stringify({ idventa: iventa, producto: nombre, cantidad: cant, codigo: cod, largo: largo }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (msg) {
                        $("#txtcantidad").val("");
                        $("#txtmetros").val("");
                        $("#txtcantidad").focus();
                        $("#detalleproductos").html(msg.d);
                    }
                });

            } else {
                alert("La cantidad que desea vender excede a la disponible");
            }

        });


        $('#cleancarrito').on('click', function () {


            $.ajax({
                type: 'POST',
                url: 'Venta.aspx/CleanCarrito',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {

                    $("#detalleproductos").html(msg.d);
                }
            });
        });

    </script>
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
	                    var datos = JSON.parse(msg.d);
	                    $("#codigo_cliente").val(datos[3]);
	                    $("#nit_cliente").val(datos[1]);
	                    $("#nombre_cliente").val(datos[0]);
	                    $("#comercio").val(datos[2]);
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


	    function AddPago() {


	        var tot = document.getElementById("totalpago").value;
	        var client = document.getElementById("codclientepago").value;

	        var client = document.getElementById('cmbclientes').value;
	        var tipo = $("#cmbpago").val();
	        var pago = document.getElementById("totalabonado").value;


	        var totalventa = parseFloat(tot.replace(",", "."));

	        var pagototal = parseFloat(pago.replace(",", "."));

	        if (pagototal > totalventa || pagototal == totalventa) {
	            if (client != "" && tot != "0") {


	                $.ajax({
	                    type: 'POST',
	                    url: 'Venta.aspx/AddPago',
	                    data: JSON.stringify({ total: tot, cliente: client, tipopago: tipo }),
	                    contentType: 'application/json; charset=utf-8',
	                    dataType: 'json',
	                    success: function (response) {
	                        if (response.d == true) {
	                            $.ajax({
	                                type: 'POST',
	                                url: 'Venta.aspx/GetVenta',
	                                data: JSON.stringify({}),
	                                contentType: 'application/json; charset=utf-8',
	                                dataType: 'json',
	                                success: function (response) {
	                                    var abono = document.getElementById("totalabonado").value;
	                                    var total = document.getElementById("totalpago").value;

	                                    var CodVenta = response.d;
	                                    $('#venta').removeClass();
	                                    $('#venta').addClass('alert alert-success').html('Codigo de Venta: ' + CodVenta.substr(-2)).show(200).delay(2500);

	                                    var c = 0;
	                                    if (abono != "") {
	                                        var aaa = total.replace(",", ".");
	                                        var a = parseFloat(abono);
	                                        var b = parseFloat(aaa);
	                                        if (a > 0 && a > b || a > 0 && a == b) {
	                                            c = parseFloat(a - b);
	                                            $('#vuelto').removeClass();
	                                            $('#vuelto').addClass('label label-success').html('Vuelto:  Q.' + c).show(200).delay(2500);
	                                            alert('Su Vuelto es: ' + parseFloat(c));
	                                        }
	                                    }
	                                    reloadTable();

	                                }
	                            });

	                        } else {
	                            $('#mensaje').removeClass();
	                            $('#mensaje').addClass('alert alert-danger').html('Error al insertar').show(200).delay(2500).hide(200);

	                        }

	                    }
	                });

	            } else {
	                $('#mensaje').removeClass();
	                $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(2500).hide(200);

	            }


	        } else {

	            $('#mensaje').removeClass();
	            $('#mensaje').addClass('alert alert-danger').html('El pago es menor que el total').show(200).delay(2500).hide(200);

	        }




	        return false;


	    }

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
	    function closeModalPago() {


	        $('#ModalPago').modal('toggle');
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
