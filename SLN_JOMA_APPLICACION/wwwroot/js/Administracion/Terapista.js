var URL_BASE_TERAPISTA = ""
var CONTROLERNAME_TERAPISTA = ""

var Terapista = {

    Init: function (UrlBase, Controller) {
        debugger;
        URL_BASE_TERAPISTA = UrlBase;
        CONTROLERNAME_TERAPISTA = Controller;
    },
    GuardarDatos: function (id, event) {
        debugger;
        if (!Site.ValidarForumarioById(id, event))
            return;

        var formDataObj = Site.GetObjetoFormularioById(id);
        Site.IniciarLoading();
        $.ajax({
            type: "POST",
            url: Site.createUrl(URL_BASE_TERAPISTA, CONTROLERNAME_TERAPISTA, "/GuardarTerapista"),
            data: JSON.stringify(formDataObj),
            contentType: "application/json; charset=utf-8",
            //dataType: "json",
            success: function (response) {
                Site.CerrarLoading();
                if (response && response.success) {
                    Site.mostrarNotificacion(response.message, 1);
                } else if (response && response.message) {
                    Site.mostrarNotificacion("Error al guardar los datos: " + response.message, 2);
                } else {
                    Site.mostrarNotificacion("Ocurrió un error inesperado." + response.message, 2);
                }
            },
            error: function (result) {
                Site.AjaxError(result);
            }
        });

    },
};