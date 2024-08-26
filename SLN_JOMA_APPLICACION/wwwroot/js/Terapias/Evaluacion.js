var Evaluacion = {
    wizardInitialized: false,
    SelectInitialized: false, 
    Init: function (UrlBase, Controller) {
        debugger;

        var form = $(".validation-wizard").show();
        var stepCount = 5;

        $(".validation-wizard").steps({
            headerTag: "h6",
            bodyTag: "section",
            transitionEffect: "fade",
            titleTemplate: '<span class="step">#index#</span> #title#',
            labels: {
                finish: "Enviar",            // Cambiado a "Enviar"
                next: "Siguiente",           // Cambiado a "Siguiente"
                previous: "Anterior",        // Cambiado a "Anterior"
                loading: "Cargando..."       // Cambiado a "Cargando..."
            },
            onStepChanging: function (event, currentIndex, newIndex) {
                // Aseguramos que los campos de todos los pasos se validen correctamente
                form.validate().settings.ignore = ":disabled,:hidden";
                return form.valid(); // Valida todos los campos visibles
            },
            onFinishing: function (event, currentIndex) {
                form.validate().settings.ignore = ":disabled";
                return form.valid(); // Valida todos los campos antes de finalizar
            },
            onFinished: function (event, currentIndex) {
                swal(
                    "Form Submitted!",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat eleifend ex semper, lobortis purus sed."
                );
            },
            onStepChanged: function (event, currentIndex, priorIndex) {
                debugger;
                if (currentIndex === stepCount - 1) {
                    if (Evaluacion.wizardInitialized) {
                        $(".validation-wizard2").steps('destroy');
                    }

                    Evaluacion.InitWizardVertical();
                }
            }
        });

        // Configuración de validación de jQuery Validate
        $(".validation-wizard").validate({
            ignore: "input[type=hidden]", // Ignorar campos ocultos
            errorClass: "is-invalid", // Clase CSS para los campos con error
            successClass: "is-valid", // Clase CSS para los campos válidos
            highlight: function (element, errorClass, validClass) {
                $(element).addClass(errorClass).removeClass(validClass);
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).removeClass(errorClass).addClass(validClass);
            },
            errorPlacement: function (error, element) {
                var feedbackElement = element.siblings('.invalid-feedback');
                if (feedbackElement.length > 0) {
                    feedbackElement.show();
                } else {
                    error.insertAfter(element);
                }
            },
            success: function (label, element) {
                $(element).siblings('.invalid-feedback').hide();
            },
            rules: {
                email: {
                    email: true,
                },
            },
        });
    },

    InitWizardVertical: function () {
        var stepCount = 4;
        $(".validation-wizard2").steps({
            headerTag: "h6",
            bodyTag: "section",
            transitionEffect: "fade",
            titleTemplate: '<span class="step">#index#</span> #title#',
            labels: {
                finish: "Enviar",            // Cambiado a "Enviar"
                next: "Siguiente",           // Cambiado a "Siguiente"
                previous: "Anterior",        // Cambiado a "Anterior"
                loading: "Cargando..."       // Cambiado a "Cargando..."
            },
            onStepChanged: function (event, currentIndex, priorIndex) {
                debugger;
                if (currentIndex === stepCount - 1) {
                    if (!Evaluacion.SelectInitialized) {
                        $('#reconoceNociones').select2({
                            placeholder: "Seleccione una o más opciones",
                            allowClear: true
                        });
                    }
                    Evaluacion.SelectInitialized = true;
                }
            }
        });

        Evaluacion.wizardInitialized = true;
    }
}
