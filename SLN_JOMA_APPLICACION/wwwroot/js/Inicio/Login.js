let URL_BASE = ""
let CONTROLERNAME = ""

var Login = {
    Init: function () {

        $("#txtUser").on("keyup", (event) => {
            if (event.key === ENTER_KEY) {
                //$("#txtPassword").focus();
            }
        });

        $("#txtPassword").on("keyup", (event) => {
            if (event.key === ENTER_KEY) {
                $("#BtnLogin").trigger("click");
            }
        });

        $("#BtnLogin").on("click", (e) => {
            e.preventDefault();
            Login.RealizarLogin();
        });
    },

    RealizarLogin: function () {

        if (!Site.ValidarForumarioById("FrmLogin"))
            return;


        var txtCompania = $("#txtCompania").val();
        var txtUser = $("#txtUser").val();
        var txtPassword = $("#txtPassword").val();


        Site.IniciarLoading();


        var loginData = {
            Usuario: txtUser,
            Clave: txtPassword,
            Compania: txtCompania
        };

        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE, CONTROLERNAME, "/RealizarLogin"),
            data: JSON.stringify(loginData),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result == 'ForzarCambioClave') {
                    Site.CerrarLoading();
                    Login.OpenModalRecuperarContrasenia(result, true);
                    return;
                }
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

    OpenModalRecuperarContrasenia: function (ForzarCambioClave = false) {
        if (ForzarCambioClave) {
            $("#TitleOlvidasteContrasenia").addClass('d-none');
            $("#ForzarCambioContrasenia").removeClass('d-none');
            $("#ContenteReestablecerContrasenia").addClass('d-none');
            $("#ContenteForzarCambiosDeClave").removeClass('d-none');
        } else {

            $("#TitleOlvidasteContrasenia").removeClass('d-none');
            $("#ForzarCambioContrasenia").addClass('d-none');
            $("#ContenteReestablecerContrasenia").removeClass('d-none');
            $("#ContenteForzarCambiosDeClave").addClass('d-none');
        }

        $('#forgotPasswordModal').modal('show');
    },

    RecuperarContrasenia: function myfunction() {
        if (!Site.ValidarForumarioById("FrmRecuperacion"))
            return;

        $('#forgotPasswordModal').modal('hide');
        Site.mostrarNotificacion("Correo enviado exitosamente", 1, 5000);
    },


}