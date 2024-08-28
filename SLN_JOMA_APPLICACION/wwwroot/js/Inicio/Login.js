let URL_BASE = ""
let CONTROLERNAME = ""

var Login = {
    ACCION_FORZARCAMBIOCLAVE: "",
    ONCLICK_CERRAR_SESION: "Login.CerrarSesion();",
    Init: function () {

        $("#txtUser").on("keyup", (event) => {
            if (event.key === KEY_ENTER) {
                //$("#txtPassword").focus();
            }
        });

        $("#txtPassword").on("keyup", (event) => {
            if (event.key === KEY_ENTER) {
                $("#BtnLogin").trigger("click");
            }
        });

        $("#BtnLogin").on("click", (e) => {
            e.preventDefault();
            Login.RealizarLogin();
        });
    },

    RealizarLogin: function (id, event) {
        debugger;
        if (!Site.ValidarForumarioById(id, event))
            return;


        // Serializar los datos del formulario y convertirlos a un objeto clave-valor

        var loginData = Site.GetObjetoFormularioById(id);

        Site.IniciarLoading();

        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE, CONTROLERNAME, "/RealizarLogin"),
            data: JSON.stringify(loginData),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result.success) {
                    if (result.message === Login.ACCION_FORZARCAMBIOCLAVE) {
                        Site.CerrarLoading();
                        Login.OpenModalRecuperarContrasenia(result.message, true);
                        return;
                    }
                    window.location.href = result.message;
                } else {
                    // Manejar casos en los que success es false
                    Site.mostrarNotificacion(result.message, 2);
                }
            },
            error: function (result) {
                Site.AjaxError(result);
            },
            complete: function () {
                Site.CerrarLoading();
            }
        });
    },

    OpenModalRecuperarContrasenia: function (ForzarCambioClave = false) {
        if (ForzarCambioClave) {
            $("#CerrarModal").attr('onclick', Login.ONCLICK_CERRAR_SESION);
            $("#CerrarForm").attr('onclick', Login.ONCLICK_CERRAR_SESION);
            $("#TitleOlvidasteContrasenia").addClass('d-none');
            $("#ForzarCambioContrasenia").removeClass('d-none');
            $("#ContenteReestablecerContrasenia").addClass('d-none');
            $("#ContenteForzarCambiosDeClave").removeClass('d-none');
        } else {

            $("#TitleOlvidasteContrasenia").removeClass('d-none');
            $("#ForzarCambioContrasenia").addClass('d-none');
            $("#ContenteReestablecerContrasenia").removeClass('d-none');
            $("#ContenteForzarCambiosDeClave").addClass('d-none');
            $("#CerrarModal").attr('onclick', '');
            $("#CerrarForm").attr('onclick', '');
        }

        $('#forgotPasswordModal').modal('show');
    },

    RecuperarContrasenia: function myfunction() {
        if (!Site.ValidarForumarioById("FrmRecuperacion"))
            return;

        $('#forgotPasswordModal').modal('hide');
        Site.mostrarNotificacion("Correo enviado exitosamente", 1, 5000);
    },

    CerrarSesion: function () {
        $.ajax({
            type: 'Get',
            url: Site.createUrl(URL_BASE, CONTROLERNAME, "/CerrarSesion"),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result.success) {
                    if (result.message === Login.ACCION_FORZARCAMBIOCLAVE) {
                        Site.CerrarLoading();
                        Login.OpenModalRecuperarContrasenia(result.message, true);
                        return;
                    }
                    window.location.href = result.message;
                } else {
                    // Manejar casos en los que success es false
                    Site.mostrarNotificacion(result.message, 2);
                }
            },
            error: function (result) {
                Site.AjaxError(result);
            },
            complete: function () {
                Site.CerrarLoading();
            }
        });
    }


}