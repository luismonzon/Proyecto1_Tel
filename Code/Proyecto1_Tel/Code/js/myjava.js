$(function () {
    
    $('#nuevo-rol').on('click', function () {
        $('#formulario')[0].reset(); //formulario lo inicializa con datos vacios
        $('#pro').val('Registro'); //crea nuestra caja de procesos y se agrega el valor del registro
        $('#edi').hide(); //escondemos el boton de edicion porque es un nuevo registro
        $('#reg').show(); //mostramos el boton de registro
        $('#registra-rol').modal({ //
            show: true, //mostramos el modal registra producto
            backdrop: 'static' //hace que no se cierre el modal si le dan clic afuera del mismo.
        });
    });
    
    
    
});


function agregaRegistro() {
    var url = '../Rol.aspx/InsertRol.php';
    $.ajax({
        type: 'POST',
        url: url,
        data: $('#formulario').serialize(),
        success: function (registro) {
            if ($('#pro').val() == 'Registro') {
                $('#formulario')[0].reset();
                $('#mensaje').addClass('bien').html('Registro completado con exito').show(200).delay(2500).hide(200);
                $('#agrega-registros').html(registro);
                return false;
            } else {
                $('#mensaje').addClass('bien').html('Edicion completada con exito').show(200).delay(2500).hide(200);
                $('#agrega-registros').html(registro);
                return false;
            }
        }
    });
    return false;
}


