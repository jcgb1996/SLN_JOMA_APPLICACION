var URL_BASE_PACIENTE = ""
var CONTROLERNAME_PACIENTE = ""

var Paciente = {

    Init: function (UrlBase, Controller) {
        debugger;
        URL_BASE_PACIENTE = UrlBase;
        CONTROLERNAME_PACIENTE = Controller;
    },
    GuardarDatos: function (event) {
        debugger;
        if (!Site.ValidarForumarioById("RegistrarTrabajador", event))
            return;
    },
};