var URL_BASE_ADMINISTRACION = ""
var CONTROLERNAME_ADMINISTRACION = ""

var Terapista = {

    Init: function (UrlBase, Controller) {
        debugger;
        URL_BASE_ADMINISTRACION = UrlBase;
        CONTROLERNAME_ADMINISTRACION = Controller;
    },
    GuardarDatos: function (event) {
        debugger;
        if (!Site.ValidarForumarioById("RegistrarTrabajador", event))
            return;
    },
};