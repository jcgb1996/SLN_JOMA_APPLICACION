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
        $('#' + IdFormulario).find('.form-control').each(function () {
            $(this).removeClass('is-invalid is-valid'); // Remover las clases de Bootstrap 5
            $(this).closest('.form-group').find('.invalid-feedback').hide(); // Ocultar el mensaje de error 
            $(this).val('').trigger('change');
        });

        // Remover la clase `was-validated` del formulario si está presente
        $('#' + IdFormulario).removeClass('was-validated');
    },
    GetObjetoFormularioById: function (IdFormulario) {
        var formDataObj = $('#' + IdFormulario).serializeArray().reduce(function (obj, item) {
            obj[ item.name ] = item.value;
            return obj;
        }, {});
        return formDataObj;
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
    mostrarNotificacion: function (Mensaje, tipo = 1, Milisegundos) {
        switch (tipo) {
            case 1: {
                toastr.success(Mensaje, "", {
                    closeButton: true,
                    timeOut: (Milisegundos === undefined || Milisegundos === null) ? 10000 : Milisegundos,
                });
            } break;
            case 2: {
                toastr.error(Mensaje, "", {
                    closeButton: true,
                    timeOut: (Milisegundos === undefined || Milisegundos === null) ? 10000 : Milisegundos,
                });

            } break;
            default:
        }
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
                Site.AjaxError(result);
            },
            complete: function () {
                Site.CerrarLoading();
            }
        });
    },
    AjaxError: function (result) {
        var errorResponse;
        try {
            errorResponse = JSON.parse(result.responseText);
        } catch (e) {
            errorResponse = { message: "An unexpected error occurred." };
        }

        switch (result.status) {
            case 400:
                // Bad Request
                Site.mostrarNotificacion(Site.getErrorMessage(errorResponse) || "Bad request. Please check the data and try again.", 2);
                break;
            case 401:
                // Unauthorized
                Site.mostrarNotificacion(Site.getErrorMessage(errorResponse) || "You are not authorized to perform this action. Please log in.", 2);
                break;
            case 403:
                // Forbidden
                Site.mostrarNotificacion(Site.getErrorMessage(errorResponse) || "You do not have permission to access this resource.", 2);
                break;
            case 404:
                // Not Found
                Site.mostrarNotificacion(Site.getErrorMessage(errorResponse) || "The requested resource was not found.", 2);
                break;
            case 500:
                // Internal Server Error
                Site.mostrarNotificacion(Site.getErrorMessage(errorResponse) || "An internal server error occurred. Please try again later.", 2);
                break;
            default:
                // Other status codes
                Site.mostrarNotificacion(Site.getErrorMessage(errorResponse) || "An unexpected error occurred.", 2);
                break;
        }
    },
    getErrorMessage: function (errorResponse) {
        if (errorResponse.message && errorResponse.message.trim() !== "") {
            return errorResponse.message;
        }

        if (errorResponse.Message && errorResponse.Message.trim() !== "") {
            return errorResponse.Message;
        }

        return "Bad request. Please check the input.";
    }

}