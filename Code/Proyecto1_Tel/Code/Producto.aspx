<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="Proyecto1_Tel.Code.Producto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <!--- URL DE LA PAGINA --->
    <li><a href="Producto.aspx">Productos</a></li>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <input runat="server" type="text" required="required" readonly="readonly" name="codigo" id="codigo"  style="visibility:hidden; height:5px;"/>
    <div id="divmodal" runat="server">
            <!-- MODAL PARA CLIENTES-->
    </div>


    <!--- TODO EL CONTENIDO DE LA PAGINA--->    
			   
     <h4 class="widget-name"><i class="icon-columns"></i>Productos</h4>
       
    <div>
        <div><a style="font-size: 13px" id="nuevo-producto" class="btn btn-success"> Agregar Producto <i class="icon-plus-sign" >&nbsp;</i></a></div>
    </div>
    

    <!---TABLA QUE MUESTRA LOS USUARIOS--->
	    <!-- Some controlы -->
        <div class="widget" id="tab_producto" runat="server">
        </div>
        <!-- /some controlы -->

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
    <script type="text/javascript">

        function FillCombo() {
            $.ajax({
                type: 'POST',
                url: 'Producto.aspx/Fill',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var data = JSON.parse(response.d);
                    var $select = $('#tipopro');
                    var options = '';
                    for (var i = 0; i < data.length; i++) {
                        options += '<option value="' + data[i].key + '">' + data[i].value + '</option>';
                    }
                    $select.html(options);
                }
            });
        }


        //cambia dependiendo de la opcion seleccionada
        function cambio() {
            var combo = document.getElementById("tipopro");
            
            var selected = $("#tipopro  option:selected").text();

            if (selected == "Articulo") {
                $('#divtam').hide();
                $('#divporc').hide();   
            } else {
                $('#divtam').show();
                $('#divporc').show();
            }
                

        }

        //MUESTRA EL MODAL PARA AGREGAR PRODUCTO


        $('#nuevo-producto').on('click', function () {

            $.ajax({
                type: 'POST',
                url: 'Producto.aspx/MostrarModal',
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
                    $('#reg').show(); //mostramos el boton de registro
                    $('#edi').hide();//se esconde el boton de editar
                    $('#Modal').modal({ //
                        show: true, //mostramos el modal registra producto
                        backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                    });
                }
            });
        });


        //Mostrar datos en edicion
        function Mostrar_Producto(id) {
            document.getElementById("<%=codigo.ClientID%>").value = id;
             var identi = document.getElementById("<% = codigo.ClientID%>").value;

            $.ajax({
                type: 'POST',
                url: 'Producto.aspx/MostrarModal',
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

                    FillCombo();
                    $('#reg').hide(); //escondemos el boton de registro
                    $('#edi').show(); //escondemos el boton de edicion porque es un nuevo registro
                    $('#reg').hide(); //mostramos el boton de registro
                    $('#Modal').modal({ //
                        show: true, //mostramos el modal registra producto
                        backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                    });

                    $.ajax({
                        type: 'POST',
                        url: 'Producto.aspx/BuscarProducto',
                        data: JSON.stringify({ id: identi }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            var produc = JSON.parse(response.d);
                            var Abreviatura = produc[0];
                            var Descripcion = produc[1];
                            var Tipo = produc[2];
                            var Porcentaje = produc[3];
                            var Ancho = produc[4];
                            var Marca = produc[5];
                            var tipodesc = produc[6];
                            var combo = document.getElementById("tamano");
                             var selectede = combo.options[combo.selectedIndex].text;



                             document.getElementById("abreviatura").value = Abreviatura;
                             document.getElementById("descripcion").value = Descripcion;
                             document.getElementById("tipopro").value = Tipo;


                             if (tipodesc == "ARTICULO" || tipodesc == "Articulo") {
                                 $('#porc').hide();
                                 $('#divporc').hide();
                                 $('#divtam').hide();

                             } else {
                                 $('#divporc').show();
                                 document.getElementById("porc").value = Porcentaje;
                                 document.getElementById("tipopro").value = Tipo;
                                 if (Ancho == "1.52") {

                                     document.getElementById("tamano").value = "grande";
                                 } else {
                                     document.getElementById("tamano").value = "pequeno";
                                 }
                                 $('#divtam').show();
                             }

                             document.getElementById("marca").value = Marca;

                         }
                     });

                }
            });


             
        }

       
       

         //REGISTRAR UN PRODUCTO
        function AddProduct() {
            

            var nAbre = document.getElementById("abreviatura").value;
            var nDescrip = document.getElementById("descripcion").value;
            var nTipopro = document.getElementById("tipopro").value;
            var nMarca = document.getElementById("marca").value;
            var nAncho = document.getElementById("tamano").value;            
            var nporcen = document.getElementById("porc").value; 
            var selected = $("#tipopro  option:selected").text();
            
            if (selected == "Polarizado") {

                if (nAncho != "" && nporcen != "") {

                    if (nAbre != "" && nDescrip != "" && nTipopro != "" && nMarca != "") {

                        if (nAncho == "pequeno") {
                            nAncho = "0.5"
                        } else {
                            nAncho = "1.52"
                        }

                        $.ajax({
                            type: 'POST',
                            url: 'Producto.aspx/Add',
                            data: JSON.stringify({ abrevia: nAbre, descripcion: nDescrip, tipo: nTipopro, marca: nMarca, ancho: nAncho, porc: nporcen }),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (response) {
                                if (response.d == true) {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-success').html('Producto agregado con exito').show(200).delay(5500).hide(200);
                                    $('#formulario')[0].reset();
                                } else {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-danger').html('Abreviatura ya existe').show(200).delay(5500).hide(200);
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {
                                alert('request failed');
                            }
                        });

                    } else {
                        $('#mensaje').removeClass();
                        $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                    }
                }else{
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                }
                } else {
                if (nAbre != "" && nDescrip != "" && nTipopro != "" && nMarca != "") {
                    nAncho = "";
                    nporcen = "";
                    $.ajax({
                        type: 'POST',
                        url: 'Producto.aspx/Add',
                        data: JSON.stringify({ abrevia: nAbre, descripcion: nDescrip, tipo: nTipopro, marca: nMarca, ancho: nAncho, porc: nporcen }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            alert(response.d);
                            if (response.d == true) {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-success').html('Producto agregado con exito').show(200).delay(5500).hide(200);
                                $('#formulario')[0].reset();
                            } else {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-danger').html('Abreviatura ya existe').show(200).delay(5500).hide(200);

                            }

                        },
                        error: function (xhr, textStatus, errorThrown) {
                            alert(errorThrown);
                        }
                    });

                } else {
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                }                   
                }


                return false;

            }


        //REGISTRAR UN PRODUCTO
        function Edit() {

            var nAbre = document.getElementById("abreviatura").value;
            var nDescrip = document.getElementById("descripcion").value;
            var nTipopro = document.getElementById("tipopro").value;
            var nMarca = document.getElementById("marca").value;
            var nCodigo = document.getElementById("<%=codigo.ClientID%>").value;
            var nporcen = document.getElementById("porc").value; 
            var nAncho = document.getElementById("tamano").value;
            var selected = $("#tipopro  option:selected").text();

            if (selected == "Polarizado") {

                if (nAncho != "" && nporcen != "") {

                    if (nAbre != "" && nDescrip != "" && nTipopro != "" && nMarca != "") {
                        if (nAncho == "pequeno") {
                            nAncho = "0.5"
                        } else {
                            nAncho = "1.52"
                        }


                        $.ajax({
                            type: 'POST',
                            url: 'Producto.aspx/EditPro',
                            data: JSON.stringify({id: nCodigo, abrevia: nAbre, descripcion: nDescrip, tipo: nTipopro, marca: nMarca, ancho: nAncho, porc: nporcen }),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            success: function (response) {
                                if (response.d == true) {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-success').html('Producto editado con exito').show(200).delay(5500).hide(200);
                                    
                                } else {
                                    $('#mensaje').removeClass();
                                    $('#mensaje').addClass('alert alert-danger').html('Abreviatura ya existe').show(200).delay(5500).hide(200);
                                    
                                }

                            }
                        });

                    } else {
                        $('#mensaje').removeClass();
                        $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                    }
                } else {
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                }
            } else {
                if (nAbre != "" && nDescrip != "" && nTipopro != "" && nMarca != "") {
                    nAncho = "";
                    nporcen = "";
                    $.ajax({
                        type: 'POST',
                        url: 'Producto.aspx/EditPro',
                        data: JSON.stringify({id: nCodigo, abrevia: nAbre, descripcion: nDescrip, tipo: nTipopro, marca: nMarca, ancho: nAncho, porc: "" }),
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (response) {
                            if (response.d == true) {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-success').html('Producto editado con exito').show(200).delay(5500).hide(200);
                                
                            } else {
                                $('#mensaje').removeClass();
                                $('#mensaje').addClass('alert alert-danger').html('Abreviatura ya existe').show(200).delay(5500).hide(200);
                                
                            }

                        }
                    });

                } else {
                    $('#mensaje').removeClass();
                    $('#mensaje').addClass('alert alert-danger').html('Revise los campos obligatorios marcados con (*)').show(200).delay(5500).hide(200);

                }
            }


            return false;

        }

        //--------- ELIMINAR PRODUCTO -->
        function Eliminar_Producto(id) {
            if (confirm("Esta seguro que desea eliminar el producto?")) {
                $.ajax({
                    type: "POST",
                    url: "Producto.aspx/DeleteProd",
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
