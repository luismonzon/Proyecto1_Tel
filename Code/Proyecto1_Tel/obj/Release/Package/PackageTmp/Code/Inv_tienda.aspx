<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Inv_tienda.aspx.cs" Inherits="Proyecto1_Tel.Code.Inv_tienda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <li><a href="Inv_tienda.aspx">Inventario Tienda</a></li>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input runat="server" type="text" required="required" readonly="readonly" name="codigo" id="codigo"  style="visibility:hidden; height:5px;"/>
    <div id="divmodal" runat="server">
            <!-- MODAL PARA CLIENTES-->
    </div>
    
	    <h4 class="widget-name"><i class="icon-columns"></i>Inventario Tienda</h4>

    <input runat="server" type="text" required="required" readonly="readonly" name="codigo" id="Text1"  style="visibility:hidden; height:5px;"/>
    <div>
        <div><a style="font-size: 13px" id="nuevo-tienda" onclick="NuevoTienda();" class="btn btn-success"> Agregar Producto a Tienda <i class="icon-plus-sign" >&nbsp;</i></a></div>
    </div>
    
        <!-- Some controlы  -->
        <div class="widget" id="tab_tienda" runat="server">
        </div>
        <!-- /some controlы -->



     
     <!-- MODAL PARA PRODUCTOS EN BODEGA-->

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">

    <script type="text/javascript">

        function FillCombo() {
            $.ajax({
                type: 'POST',
                url: 'Inv_tienda.aspx/Fill',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var data = JSON.parse(response.d);
                    var $select = $('#producto');
                    var options = '';
                    for (var i = 0; i < data.length; i++) {
                        options += '<option value="' + data[i].key;
                        if (i == 0) { options += '" selected="selected"' } else { options += '"'; }
                        options += '>'+data[i].value + '</option>';
                    }
                    $select.html(options);
                    cambio();
                }
            });
        }

        function NuevoTienda()
        {
            $.ajax({
                type: 'POST',
                url: 'Inv_tienda.aspx/MostrarModal',
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
                    FillCombo();

                    $('#formulario')[0].reset(); //formulario lo inicializa con datos vacios
                    $('#edi').hide(); //escondemos el boton de edicion porque es un nuevo registro
                    
                    $('#in_producto').hide();

                    $('#divnmetros').hide();
                    $('#div_disponible').hide();
                    $('#radio').hide();
                    $('#reg').show(); //mostramos el boton de registro


                    $('#Modal').modal({ //
                        show: true, //mostramos el modal registra producto
                        backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                    });

                }
            });
            
        }

        $('#nuevo-tienda').on('click', function () {
            
        });

        function cambio() {

            var id = document.getElementById('producto').value;
            var selected = $("#producto  option:selected").text();
             $.ajax({
                 type: 'POST',
                 url: 'Inv_tienda.aspx/Busca_Datos',
                 data: JSON.stringify({ id: id }),
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (response) {

                     var produc = JSON.parse(response.d);
                     var Descripcion = produc[0];
                     var Cantidad_Dispo = produc[1];
                     var Tipo = produc[2];
                     var Precio = produc[3];

                     document.getElementById("descripcion").value = Descripcion;

                     document.getElementById("cantidad_bodega").value = Cantidad_Dispo;

                     document.getElementById("tipo2").value = Tipo;

                     if (Tipo == "ARTICULO" || Tipo == "Articulo") {
                         $('#divmetros').hide();
                         $('#divcantidad').show();
                         

                     } else {
                         $('#divmetros').show();
                         $('#divcantidad').hide();
                     }
                     if (Precio == "1") {
                         $('#divprecio').hide();
                     } else {
                         $('#divprecio').show();
                     }

                }
            });

        }


        //Registro 
        function AddProduct() {
            var idproducto = document.getElementById("producto").value;
            var cantidad = document.getElementById("cantidad").value;
            var metros = document.getElementById("metros").value;
            var precio = document.getElementById("precio").value;
            var articulo = document.getElementById("tipo2").value;
            
            if (articulo == "ARTICULO" || articulo == "Articulo") {
                metros = "0";

            } else {
                cantidad = "1";
            }

              if (metros != "" || cantidad != "") {

                  $.ajax({

                      type: 'POST',
                      url: 'Inv_tienda.aspx/Add',
                      data: JSON.stringify({ producto: idproducto, cantidad: cantidad , precio: precio, metros: metros}),
                      contentType: 'application/json; charset=utf-8',
                      dataType: 'json',
                      success: function (response) {
                          if (response.d == true) {
                              $('#mensaje').removeClass();
                              $('#mensaje').addClass('alert alert-success').html('Producto agregado con exito').show(200).delay(2500).hide(200);

                          } else {
                              $('#mensaje').removeClass();
                              $('#mensaje').addClass('alert alert-danger').html('Producto no se pudo agregar').show(200).delay(2500).hide(200);

                          }

                      }
                  });

              } else {

                  document.getElementById("metros").focus();
                  document.getElementById("cantidad").focus();
                  $('#mensaje').removeClass();
                  $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(2500).hide(200);

              }

              return false;
          }



        //EDITAR 

        function Editar_Tienda(id) {
            $.ajax({
                type: 'POST',
                url: 'Inv_tienda.aspx/MostrarModal',
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
                    $('#edi').show(); //escondemos el boton de edicion porque es un nuevo registro
                    $('#reg').hide(); //mostramos el boton de registro
                    $('#Modal').modal({ //
                        show: true, //mostramos el modal registra producto
                        backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                    });

                    $.ajax({
                        type: 'POST',
                        url: 'Inv_tienda.aspx/Busca_Descripcion',
                        data: JSON.stringify({ id: id }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            var produc = JSON.parse(response.d);
                            var Descripcion = produc[0];
                            var Cantidad = produc[1];
                            var Tipo_Descrip = produc[2];
                            var Precio = produc[3];
                            var Metros = produc[4];
                            var Abreviatura = produc[5];



                            document.getElementById("descripcion").value = Descripcion;
                            document.getElementById("producto").value = id;
                            document.getElementById("cantdisponible").value = Cantidad;
                            document.getElementById("precio").value = Precio;
                            document.getElementById("metrosdisponibles").value = Metros;
                            document.getElementById("abre_producto").value = Abreviatura;
                            document.getElementById("<% = codigo.ClientID%>").value = id;

                            $('#cmbproducto').hide();


                            document.getElementById("tipo2").value = Tipo_Descrip;
                            if (Tipo_Descrip == "Polarizado") {
                                $('#divcantidad').hide();
                                $('#divcantdisp').hide();
                                $('#divnmetros').show();
                                $('#div_disponible').hide();
                            } else {
                                $('#divcantidad').show();
                                $('#divcantdisp').hide();
                                $('#divmetros').hide();
                                $('#divnmetros').hide();
                                $('#radio').show();
                                $('#div_disponible').show();
                            }


                        }
                    });
                }
            });
        }



        //Editar Productos en tienda

        function Edit() {
            var idproducto = document.getElementById("<% = codigo.ClientID%>").value;
            var cantidad = document.getElementById("cantidad").value;
            var precio = document.getElementById("precio").value;
            var metros = document.getElementById("metros").value;
            var formulario = document.forms[0];
            var opcion = document.getElementsByName("opcion");
            
            for (var i = 0; i <opcion.length; i++) {
                if (opcion[i].checked) {
                    
                    if (opcion[i].value == '1') {
                        $.ajax({
                            type: 'POST',
                            url: 'Inv_tienda.aspx/Agregar',
                            data: JSON.stringify({ producto: idproducto, cantidad: cantidad, precio: precio, metros: metros }),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (response) {
                                if (response.d == true) {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-success').html('Producto agregado con exito').show(200).delay(2500).hide(200);

                                } else {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-danger').html('Producto no se pudo agregar').show(200).delay(2500).hide(200);

                                }

                            }
                        });

                    } else {
                        $.ajax({

                            type: 'POST',
                            url: 'Inv_tienda.aspx/Rest',
                            data: JSON.stringify({ producto: idproducto, cantidad: cantidad, precio: precio, metros: metros }),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (response) {

                                if (response.d == true) {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-success').html('Producto restado con exito').show(200).delay(2500).hide(200);

                                } else {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-danger').html('Producto no se pudo restar').show(200).delay(2500).hide(200);

                                }

                            }
                        });
                    }
                }
            }

        }



        //--------- ELIMINAR PRODUCTO DE TIENDA -->
        function Eliminar_Tienda(id) {
            if (confirm("Esta seguro que desea eliminar el producto?")) {
                $.ajax({
                    type: "POST",
                    url: "Inv_tienda.aspx/DeleteProd",
                    data: JSON.stringify({ id: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d == true) {
                            reloadTable();
                            alert("Producto Eliminado Exitosamente");
                        } else {
                            alert("El Producto No Pudo Ser Eliminado");
                        }
                    }
                });
            }

        }

       
    </script>


</asp:Content>

         