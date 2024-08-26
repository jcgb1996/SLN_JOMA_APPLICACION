const KEY_ENTER = 13

var Site = {

    IniciarLoading: function () {
        $('#loading-spinner').removeClass('d-none').addClass('d-flex');
    },

    CerrarLoading: function () {
        $('#loading-spinner').removeClass('d-flex').addClass('d-none');
    },

    limpiarFormularioById: function (IdFormulario) {
        // Selecciona el formulario por su ID y resetea todos los campos de texto
        $('#' + IdFormulario)[ 0 ].reset();
        // Remover clases de validación en todos los inputs, selects y textareas
        $('#'+ IdFormulario).find('.form-control').each(function () {
            $(this).removeClass('is-invalid is-valid'); // Remover las clases de Bootstrap 5
            $(this).closest('.form-group').find('.invalid-feedback').hide(); // Ocultar el mensaje de error
        });

        // Remover la clase `was-validated` del formulario si está presente
        $('#'+ IdFormulario).removeClass('was-validated');
    },
    ValidarForumarioById: function (IdFormulario, event) {
        var $form = $('#' + IdFormulario);

        // Validar el formulario
        if ($form[ 0 ].checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            $form.addClass('was-validated');
            return false;
        }

        return true;
    },
    mostrarNotificacion: function (mensaje, tipo = 1, Milisegundos) {
        $.jGrowl(mensaje, {
            //header: 'Notificación',
            theme: tipo == 1 ? 'growl-success' : 'growl-error',
            life: (Milisegundos === undefined || Milisegundos === null) ? 10000 : Milisegundos,
            position: 'top-right'
        });
    },

    createUrl: function (baseUrl, controller, action) {
        return baseUrl + controller + action;
    },

    RenderContent: function (Url) {
        Site.IniciarLoading();
        $.ajax({
            type: 'GET',
            url: Url,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                //window.location.href = result;
                $("#Content").empty().html(result);
            },
            error: function (result) {
                if (result.status == 500)
                    Site.mostrarNotificacion(result.responseText, 2);
            },
            complete: function () {
                Site.CerrarLoading();
            }
        });
    }

}