var URL_BASE_INTERESADOS = ""
var CONTROLERNAME_INTERESADOS = ""

var Interesados = {
    Init: function (UrlBase, Controller) {
        debugger;
        URL_BASE_INTERESADOS = UrlBase;
        CONTROLERNAME_INTERESADOS = Controller;
        Interesados.GetInteresados();
    },
    GetInteresados: function () {
        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE_INTERESADOS, CONTROLERNAME_INTERESADOS, "/GetInteresados"),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                $('#DataTableInteresados').DataTable({
                    data: result, // Pasar los datos directamente al DataTable
                    columns: [
                        { data: 'nombre', title: 'Nombre' },
                        { data: 'mail', title: 'Mail' },
                        { data: 'asunto', title: 'Asunto' },
                        { data: 'mensaje', title: 'Mensaje' },
                        { data: 'telefono', title: 'Contacto' }
                    ],
                    dom: 'Bfrtip',
                    select: {
                        style: 'os',
                        selector: 'td:first-child'
                    },
                    colReorder: true,
                    processing: false,
                    serverSide: false,
                    paging: true,
                    scrollX: true,
                    ordering: true,
                    info: true,
                    pageLength: 10,
                    searching: false,
                    responsive: true,
                    destroy: true,
                    select: false,
                    language: {
                        processing: "Procesando...",
                        search: "Buscar: _INPUT_",
                        lengthMenu: '   Mostrar _MENU_',
                        info: 'Mostrando desde _START_ al _END_ de _TOTAL_ registros',
                        infoEmpty: 'Mostrando ningún elemento.',
                        infoFiltered: '(filtrado _MAX_ elementos total)',
                        infoPostFix: '',
                        loadingRecords: 'Cargando registros...',
                        zeroRecords: 'No se encontraron registros',
                        emptyTable: 'No hay datos disponibles en la tabla',
                        paginate: {
                            first: 'Primero',
                            previous: 'Anterior',
                            next: 'Siguiente',
                            last: 'Último'
                        }
                    },
                    lengthChange: false,
                });
            },
            error: function (result) {
                if (result.status == 500)
                    Site.mostrarNotificacion(result.responseText, 2);
            },
            complete: function () {
                Site.CerrarLoading();
            }
        });
    }
}