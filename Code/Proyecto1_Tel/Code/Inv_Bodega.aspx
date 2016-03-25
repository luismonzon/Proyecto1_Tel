<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Inv_Bodega.aspx.cs" Inherits="Proyecto1_Tel.Code.Inv_Bodega" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <li><a href="Inv_Bodega.aspx">Inventario Bodega</a></li>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <input runat="server" type="text" required="required" readonly="readonly" name="codigo" id="codigo"  style="visibility:hidden; height:5px;"/>
    <div id="divmodal" runat="server">
            <!-- MODAL PARA CLIENTES-->
    </div>
    
	    <h4 class="widget-name"><i class="icon-columns"></i>Inventario Bodega</h4>
    <div>
        <div><a style="font-size: 13px" id="nuevo-bodega" onclick="cambio();" class="btn btn-success"> Agregar Producto a Bodega <i class="icon-plus-sign" >&nbsp;</i></a></div>
    </div>
    
        <!-- Some controlы -->
        <div class="widget" id="tab_bodega" runat="server">
        </div>
        <!-- /some controlы -->


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">



    <script type="text/javascript">
        function FillCombo() {
            $.ajax({
                type: 'POST',
                url: 'Inv_Bodega.aspx/Fill',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var data = JSON.parse(response.d);
                    var $select = $('#producto');
                    var options = '';
                    for (var i = 0; i < data.length; i++) {
                        options += '<option value="' + data[i].key;
                        if (i == 0) { options += '" selected="selected"' } else { options += '"'; }
                        options += '>' + data[i].value + '</option>';
                    }
                    $select.html(options);
                    cambio();
                }
            });
        }


        $('#nuevo-bodega').on('click', function () {

            $.ajax({
                type: 'POST',
                url: 'Inv_Bodega.aspx/MostrarModal',
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
                    $('#divcantidad').hide();
                    $('#radio').hide();
                    $('#reg').show(); //mostramos el boton de registro
                    $('#Modal').modal({ //
                        show: true, //mostramos el modal registra producto
                        backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                    });
                }
            });
        });



        //cambia dependiendo de la opcion seleccionada
        function cambio() {

            var id = document.getElementById('producto').value;
            var selected = $("#producto  option:selected").text();
            

            $.ajax({
                type: 'POST',
                url: 'Inv_Bodega.aspx/Busca_Descripcion',
                data: JSON.stringify({ id: id}),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var produc = JSON.parse(response.d);
                    var Descripcion = produc[0];
                    document.getElementById("descripcion").value = Descripcion;

                   
                 }
            });

        }



         function AddProduct() {

            var idproducto = document.getElementById("producto").value;
            var cantidad = document.getElementById("cantidad").value;

            if (cantidad != "") {

                $.ajax({

                    type: 'POST',
                    url: 'Inv_Bodega.aspx/Add',
                    data: JSON.stringify({ producto: idproducto, cantidad: cantidad }),
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
                document.getElementById("cantidad").focus();
                $('#mensaje').removeClass();
                $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(2500).hide(200);

            }

            return false;
        }


        function Editar_Bodega(id) {
            $.ajax({
                type: 'POST',
                url: 'Inv_Bodega.aspx/MostrarModal',
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

                    $('#edi').show(); //escondemos el boton de edicion porque es un nuevo registro
                    $('#reg').hide(); //mostramos el boton de registro
                    $('#Modal').modal({ //
                        show: true, //mostramos el modal registra producto
                        backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                    });

                    $.ajax({
                        type: 'POST',
                        url: 'Inv_Bodega.aspx/Busca_Descripcion',
                        data: JSON.stringify({ id: id }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            var produc = JSON.parse(response.d);
                            var Descripcion = produc[0];
                            var Cantidad = produc[1];
                            document.getElementById("descripcion").value = Descripcion;
                            document.getElementById("cantdisponible").value = Cantidad;
                            document.getElementById("<% = codigo.ClientID%>").value = id;

                        }
                    });
                }
            });
        }

        function Edit() {
            var idproducto = document.getElementById("<% = codigo.ClientID%>").value;
            var cantidad = document.getElementById("cantidad").value;
            //var formulario = document.forms[0];
            var opcion = document.getElementsByName("opcion");
            
            for (var i = 0; i <opcion.length; i++) {
                if (opcion[i].checked) {
                    if (opcion[i].value == '1') {
                        $.ajax({

                            type: 'POST',
                            url: 'Inv_Bodega.aspx/Add',
                            data: JSON.stringify({ producto: idproducto, cantidad: cantidad }),
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
                            url: 'Inv_Bodega.aspx/Rest',
                            data: JSON.stringify({ producto: idproducto, cantidad: cantidad }),
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


        //--------- ELIMINAR PRODUCTO DE BODEGA -->
        function Eliminar_Bodega(id) {
            if (confirm("Esta seguro que desea eliminar el producto?")) {
                $.ajax({
                    type: "POST",
                    url: "Inv_Bodega.aspx/DeleteProd",
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
