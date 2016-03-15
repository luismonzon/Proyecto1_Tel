//MOSTRAR MODAL PARA AGREGAR ROL

$(function () {
    
    $('#nuevo-rol').on('click', function () {
        $('#formulario')[0].reset(); //formulario lo inicializa con datos vacios
        $('#edi').hide(); //escondemos el boton de edicion porque es un nuevo registro
        $('#reg').show(); //mostramos el boton de registro
        $('#registra-rol').modal({ //
            show: true, //mostramos el modal registra producto
            backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
        });
    });
    
    
    
});


//MUESTRA EL MODAL PARA AGREGAR CLIENTE
$(function () {

    $('#nuevo-cliente').on('click', function () {
        $('#formulario-cliente')[0].reset(); //formulario lo inicializa con datos vacios
        $('#pro').val('Registro'); //crea nuestra caja de procesos y se agrega el valor del registro
        $('#reg').show(); //mostramos el boton de registro
        $('#edi').hide();//se esconde el boton de editar
        $('#editar-cliente').modal({ //
            show: true, //mostramos el modal registra producto
            //backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
        });
    });
});

//MUESTRA EL MODAL PARA AGREGAR USUARIO

$(function () {

    $('#nuevo-usuario').on('click', function () {
        $('#formulario-usuario')[0].reset(); //formulario lo inicializa con datos vacios
        $('#reg').show(); //mostramos el boton de registro
        $('#edi').hide();//se esconde el boton de editar
        $('#editar-usuario').modal({ //
            show: true, //mostramos el modal registra producto
            backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
        });
    });
});

//MUESTRA EL MODAL PARA AGREGAR PRODUCTO

$(function () {

    $('#nuevo-producto').on('click', function () {
        $('#formulario-producto')[0].reset(); //formulario lo inicializa con datos vacios
        $('#reg').show(); //mostramos el boton de registro
        $('#edi').hide();//se esconde el boton de editar
        $('#modal-producto').modal({ //
            show: true, //mostramos el modal registra producto
            backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
        });
    });
});

function reloadTable() {
    $.get('Producto.aspx', function (data) {

        window.location.reload();
    });
}



$(function () {

    $('#nuevo-bodega').on('click', function () {
        $('#formulario-pro_bodega')[0].reset(); //formulario lo inicializa con datos vacios
        $('#edi').hide(); //escondemos el boton de edicion porque es un nuevo registro
        $('#radio').hide();
        $('#reg').show(); //mostramos el boton de registro
        $('#modal-pro_bodega').modal({ //
            show: true, //mostramos el modal registra producto
            backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
        });
    });

});


$(function () {

    $('#nuevo-tienda').on('click', function () {
        $('#formulario-pro_tienda')[0].reset(); //formulario lo inicializa con datos vacios
        $('#edi').hide(); //escondemos el boton de edicion porque es un nuevo registro
        $('#radio').hide();
        $('#reg').show(); //mostramos el boton de registro
        $('#modal-pro_tienda').modal({ //
            show: true, //mostramos el modal registra producto
            backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
        });
    });

});