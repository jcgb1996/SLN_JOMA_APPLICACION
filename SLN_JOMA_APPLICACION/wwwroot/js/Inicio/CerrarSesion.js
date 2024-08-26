let URL_BASE_CERRARSESION = ""
let CONTROLERNAME_CERRARSESION = ""
var CerrarSesion = {
    Cerrar: function () {

        $.ajax({
            type: 'GET',
            url: Site.createUrl(URL_BASE_CERRARSESION, CONTROLERNAME_CERRARSESION, "/CerrarSesion"),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                window.location.href = result;
            },
            error: function (result) {
                if (result.status == 500)
                    Site.mostrarNotificacion(result.responseText, 2);
            },
            complete: function () {
                Site.CerrarLoading();
            }
        });
    },

}