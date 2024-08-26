var Evaluaciones = {

    Init: function () {
        debugger;
        Survey.surveyLocalization.supportedLocales = [ "en", "de", "es", "fr" ];
        SurveyCreator.localization.currentLocale = "es";

        const questionTypes = [
            "radiogroup",    // Grupo de opciones de selección única (radio buttons).
            "rating",        // Pregunta de calificación, permite seleccionar un valor numérico en una escala.
            "checkbox",      // Grupo de casillas de verificación (checkboxes) para selección múltiple.
            "dropdown",      // Menú desplegable de opciones de selección única.
            "tagbox",        // Dropdown con múltiples selecciones usando "tags" en lugar de checkboxes.
            "boolean",       // Pregunta de tipo sí/no (booleano).
            "comment",       // Campo de texto grande para comentarios o respuestas largas.
            "multipletext",  // Grupo de campos de texto (permite múltiples entradas de texto en una pregunta).
            "matrix",        // Matriz de selección única por filas y columnas.
            "matrixdropdown",// Matriz con celdas de tipo dropdown, text, checkbox, etc.

            //"text",          // Campo de texto simple para respuestas cortas.
            //"file",          // Campo para cargar archivos.
            //"panel",         // Contenedor para agrupar preguntas relacionadas.
            //"paneldynamic",  // Panel dinámico que permite añadir y remover conjuntos de preguntas.
            //"html",          // Campo para mostrar contenido HTML estático.
            //"matrixdynamic", // Matriz dinámica que permite añadir filas y cada fila es editable.
            "imagepicker",   // Selector de imágenes donde se elige una imagen de una lista.
            //"expression",    // Pregunta para mostrar valores calculados sin permitir la entrada del usuario.
            //"signaturepad",  // Campo para capturar firmas mediante un pad de firmas.
            "buttongroup",   // Grupo de botones donde se puede seleccionar una opción.
            //"image",         // Muestra una imagen estática dentro del formulario.
            "ranking",       // Permite al usuario ordenar opciones según una jerarquía o preferencia.

            "nouislider",    // Control deslizante interactivo para seleccionar valores numéricos.
            "datepicker",    // Selector de fecha que permite al usuario elegir una fecha.
            "sortablelist",  // Lista ordenable que permite arrastrar y soltar elementos en el orden preferido.
            "colorpicker"    // Selector de color para elegir un color dentro del formulario.
        ];

        const options = {
            questionTypes: questionTypes,
            showJSONEditorTab: false,
            showLogicTab: false,
            showTranslationTab: false,
            showEmbeddedSurveyTab: false,
            showTestSurveyTab: true,
            allowModifyPages: true,
            isAutoSave: false,

            showPropertyGrid: true,
            showToolbox: true,
            toolboxLocation: "left",
            showElementEditorAsPropertyGrid: true,

            allowEditSurveyTitle: true,
            allowEditSurveyDescription: true,
            allowEditSurveyLogo: true,
            allowEditPageTitle: true,
            allowEditPageDescription: true,

            showPageSelector: true,
            showUndoRedo: true,
            showOptions: true,
            showState: true,

            allowAddPages: true,
            allowDeletePages: true,
            allowMovePages: true,

            showSurveyTitle: "ifentered",
            showSurveyDescription: "ifentered",
            showSurveyLogo: "ifentered",

            questionDescriptionLocation: "underTitle",
            questionTitleLocation: "top",

            allowChangeTheme: false,
            allowChangeSurveyWidth: false,
            allowChangeSurveyPadding: false,

            allowShowInvisibleElements: false,
            allowShowEmptyTitles: false,

            allowModifySurveyCss: false,
            allowModifySurveyJS: false,
            allowModifySurveyLogic: false,

            showSaveButton: true,
            showPreviewButton: true,
            showEditButton: true,

            showSurveyThemeSelector: false,
            allowModifySurveyTheme: false,
        };

        // Crear la instancia del Survey Creator con las opciones configuradas
        const creator = new SurveyCreator.SurveyCreator("surveyContainer", options);

        // Eventos personalizados
        creator.survey.onComplete.add((sender, options) => {
            console.log(JSON.stringify(sender.data, null, 3));
        });



        // Eliminar elementos no deseados de la barra de herramientas
        let expandSettingsAction = creator.toolbar.actions.indexOf(creator.toolbar.actions.find(action => action.id === 'svd-grid-expand'))
        creator.toolbar.actions.splice(expandSettingsAction, 1);

        let settingsAction = creator.toolbar.actions.indexOf(creator.toolbar.actions.find(action => action.id === 'svd-settings'));
        creator.toolbar.actions.splice(settingsAction, 1);

        creator.onDefineElementMenuItems.add((sender, options) => {
            $('.svc-creator__banner').removeClass();
            let settingsItemIndex = options.items.indexOf(options.items.find(item => item.id === 'settings'));
            if (settingsItemIndex !== -1) {
                //options.items.splice(settingsItemIndex, 1);
            }
        });

        // Renderizar el Survey Creator en el contenedor especificado
        creator.render("surveyContainer");

        // Establecer la localización a español
        creator.locale = "es";
    },
}