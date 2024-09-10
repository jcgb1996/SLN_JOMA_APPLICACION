let URL_BASE = ""
let CONTROLERNAME = ""

var Login = {
    ACCION_FORZARCAMBIOCLAVE: "",
    ONCLICK_CERRAR_SESION: "Login.CerrarSesion();",
    passwordStrengthStatus: "Muy débil",
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
            $("#CerrarModal").attr('onclick', Login.ONCLICK_CERRAR_SESION);
            $("#CerrarForm").attr('onclick', Login.ONCLICK_CERRAR_SESION);
        }

        $('#forgotPasswordModal').modal('show');
    },
    RecuperarContrasenia: function myfunction(id, event) {
        if (!Site.ValidarForumarioById(id, event))
            return;

        var recuperacionReqAppDto = Site.GetObjetoFormularioById(id);

        Site.IniciarLoading();

        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE, CONTROLERNAME, "/RecuperarContrasena"),
            data: JSON.stringify(recuperacionReqAppDto),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result.success) {
                    if (result.message === Login.ACCION_FORZARCAMBIOCLAVE) {
                        Site.CerrarLoading();
                        Login.OpenModalRecuperarContrasenia(result.message, true);
                        return;
                    }

                    $("#ContentModalRecuperar").empty().html(result.view);
                } else {
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
    ValidarOtp: function (id, event) {
        if (!Site.ValidarForumarioById(id, event))
            return;

        var OtpReqAppDto = Site.GetObjetoFormularioById(id);

        Site.IniciarLoading();

        var Otp = OtpReqAppDto.digitInput1 + OtpReqAppDto.digitInput2 + OtpReqAppDto.digitInput3 + OtpReqAppDto.digitInput4;

        // Envía el string como un JSON que contiene solo un string
        var dataToSend = JSON.stringify(Otp);

        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE, CONTROLERNAME, "/ValidarOtp"),
            data: dataToSend, // Usando la variable serializada
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result.success) {
                    Site.mostrarNotificacion(result.message, 1);
                } else {
                    Site.mostrarNotificacion(result.message, 2);
                }
                $("#ContentModalRecuperar").empty().html(result.view);
            },
            error: function (result) {
                Site.AjaxError(result);
            },
            complete: function () {
                Site.CerrarLoading();
            }
        });
    },
    ReenviarOtp: function () {
        Site.IniciarLoading();
        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE, CONTROLERNAME, "/ReenviarOtp"),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                Site.CerrarLoading();
                if (result.success) {
                    Site.mostrarNotificacion(result.message, 1);
                } else {
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
    CerrarSesion: function () {
        $.ajax({
            type: 'Get',
            url: Site.createUrl(URL_BASE, CONTROLERNAME, "/CerrarSesion"),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result.success) {

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
    InitRecuperarContrasena: function () {

        // Toggle password visibility
        $('.toggle-password').on('click', function () {
            var $pwd = $(this).prev('input');
            if ($pwd.attr('type') === 'password') {
                $pwd.attr('type', 'text');
                $(this).find('i').removeClass('ti-eye').addClass('ti-eye-off');
            } else {
                $pwd.attr('type', 'password');
                $(this).find('i').removeClass('ti-eye-off').addClass('ti-eye');
            }
        });

        // Check password strength
        $('.password-field').on('input', function () {
            var strength = 0;
            var value = $(this).val();
            var strengthText = $('#passwordHelpBlock');
            if (value.length >= 12) strength += 1;
            if (value.match(/[A-Z]/)) strength += 1;
            if (value.match(/[a-z]/)) strength += 1;
            if (value.match(/[0-9]/)) strength += 1;
            if (value.match(/[\!\@\#\$\%\^\&\*\(\)\_\+\.\,\;\:]/)) strength += 1;

            switch (strength) {
                case 1:
                case 2:
                    strengthText.text("Débil").css('color', 'red');
                    passwordStrengthStatus = "Débil";
                    break;
                case 3:
                case 4:
                    strengthText.text("Media").css('color', 'orange');
                    passwordStrengthStatus = "Media";
                    break;
                case 5:
                    strengthText.text("Fuerte").css('color', 'green');
                    passwordStrengthStatus = "Fuerte";
                    break;
                default:
                    strengthText.text("Muy débil").css('color', 'grey');
                    passwordStrengthStatus = "Muy débil";
                    break;
            }
        });
    },
    togglePasswordVisibility: function (inputId, trigger) {
        var input = document.getElementById(inputId);
        if (input.type === "password") {
            input.type = "text";
            trigger.innerHTML = '<i class="ti ti-eye-off"></i>';
        } else {
            input.type = "password";
            trigger.innerHTML = '<i class="ti ti-eye"></i>';
        }
    },
    RealizarCambio: function (id, event) {

        if (!Site.ValidarForumarioById(id, event))
            return;

        var password = $('#Contrasena').val();
        var confirmPassword = $('#ConfirmarContrasena').val();

        if (password !== confirmPassword) {
            Site.mostrarNotificacion("Las contraseñas no coinciden.", 2);
            return false;
        }

        if (passwordStrengthStatus !== "Fuerte") {
            Site.mostrarNotificacion("Por favor, introduzca una contraseña más fuerte para continuar.", 2);
            return false;
        }

        Login.ActualziarContrasenas(id, event);
    },
    ActualziarContrasenas: function (id) {

        var Contrasenas = Site.GetObjetoFormularioById(id);

        Site.IniciarLoading();

        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE, CONTROLERNAME, "/ActualizarContrasena"),
            data: JSON.stringify(Contrasenas),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                Site.CerrarLoading();

                if (result.success) {
                    Site.mostrarNotificacion(result.message, 1);
                    Login.CerrarSesion();
                    $('#forgotPasswordModal').modal('hide');
                } else {
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