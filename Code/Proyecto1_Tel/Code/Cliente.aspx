<%@ Page Title="" Language="C#" MasterPageFile="~/Code/Site1.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="Proyecto1_Tel.Code.Cliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<!--- URL DE LA PAGINA --->
    <li><a href="Cliente.aspx">Clientes</a></li>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <input runat="server" type="text" required="required" readonly="readonly" name="codigo" id="codigo"  style="visibility:hidden; height:5px;"/>
    <div id="divmodal" runat="server">
            <!-- MODAL PARA CLIENTES-->

    </div>


<!--- TODO EL CONTENIDO DE LA PAGINA--->    
    <h5 class="widget-name"><i class="icon-columns"></i>Clientes</h5>
    
    <input runat="server" type="text" required="required" readonly="readonly" name="codigo" id="Text1"  style="visibility:hidden; height:5px;"/>
    <div>
        <div><a title="Agregar Cliente" style="font-size: 13px" id="nuevo-cliente" class="btn btn-success"> Agregar Cliente <i class="icon-plus-sign" >&nbsp;</i></a></div>
    </div>
    
	    <!-- Some controlы -->
        <div class="widget" id="tab_clientes" runat="server">
        </div>
        <!-- /some controlы -->


<!---FINALIZA HTML--->
</asp:Content>





<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
    <!--- COGIGO JAVASCRIPT --->


     <script type="text/javascript">


         //MUESTRA EL MODAL PARA AGREGAR CLIENTE

         $('#nuevo-cliente').on('click', function () { // nuevo-cliente es el id del boton para agregar al cliente
             $.ajax({
                 type: 'POST',
                 url: 'Cliente.aspx/MostrarModal',
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
                         backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
                     });
                 }
             });


        });





         function Mostrar_cliente(id) {
             document.getElementById("<% = codigo.ClientID%>").value = id;

             var identi = document.getElementById("<% = codigo.ClientID%>").value;

             $.ajax({
                 type: 'POST',
                 url: 'Cliente.aspx/MostrarModal',
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
                         url: 'Cliente.aspx/BuscarCliente',
                         data: JSON.stringify({ id: identi }),
                         contentType: 'application/json; charset=utf-8',
                         dataType: 'json',
                         success: function (response) {

                             var cliente = JSON.parse(response.d);
                             var NombreCliente = cliente[0];
                             var NitCliente = cliente[1];
                             var ApellCliente = cliente[2];
                             var DireCliente = cliente[3];
                             var TeleCliente = cliente[4];


                             document.getElementById("nombre").value = NombreCliente;
                             document.getElementById("nit").value = NitCliente;
                             document.getElementById("apellido").value = ApellCliente;
                             document.getElementById("direccion").value = DireCliente;
                             document.getElementById("telefono").value = TeleCliente;
                             document.getElementById("<% = codigo.ClientID%>").value = id;

                         }

                     });
                 }
             });
         }

         function Edit() {

             var Nombre = document.getElementById("nombre").value;
             var Id = document.getElementById("<% = codigo.ClientID%>").value;
             var Nit = document.getElementById("nit").value;
             var Apellido = document.getElementById("apellido").value;
             var Direccion = document.getElementById("direccion").value;
             var Telefono = document.getElementById("telefono").value;
                    
              if (Nombre != "") {

                  $.ajax({

                      type: 'POST',
                      url: 'Cliente.aspx/EditCliente',
                      data: JSON.stringify({ id: Id, nombre: Nombre, nit: Nit, apellido: Apellido, direccion: Direccion, telefono: Telefono }),
                      contentType: 'application/json; charset=utf-8',
                      dataType: 'json',
                      success: function (response) {
                          if (response.d == true) {
                              $('#mensaje').removeClass();
                              $('#mensaje').addClass('alert alert-success').html('Registro editado exitosamente').show(200).delay(2500).hide(200);
                              
                          } else {
                              $('#mensaje').removeClass();
                              $('#mensaje').addClass('alert alert-danger').html('Nit ya existe con otro cliente').show(200).delay(2500).hide(200);

                          }
                          
                      }
                        
                  });
              } else {
                  $('#mensaje').removeClass();
                  $('#mensaje').addClass('alert alert-danger').html('No debe dejar campos vacios').show(200).delay(2500).hide(200);
                  Mostrar_cliente(id);
              }
              
             return false;
         }

        
         

    </script>


    <!--- ELIMINAR CLIENTE -->
    <script type="text/javascript">
        function Eliminar_Cliente(id) {
            if (confirm("Esta seguro que desea eliminar al Cliente?")) {
                var row = $(this).closest("tr");
                var ClienteId = id;
                $.ajax({
                    type: "POST",
                    url: "Cliente.aspx/EliminarCliente",
                    data: JSON.stringify({ id: ClienteId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d == true) {
                            reloadTable();
                            alert("Cliente Eliminado Exitosamente");
                        } else {
                            alert("Cliente No Se Pudo Eliminar");
                        }
                    }
                });
            }

        }



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
                    url: 'Cliente.aspx/Add',
                    data: JSON.stringify({ nombre: nNombre, nit: nNit, apellido: nApellido, direccion: nDireccion, telefono: nTelefono  }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        if (response.d == true) {
                            $('#mensaje').removeClass();
                            $('#mensaje').addClass('alert alert-success').html('Cliente agregado con exito').show(200).delay(2500).hide(200);
                            $('#formulario')[0].reset();
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


        
        
        </script>

   
</asp:Content>
