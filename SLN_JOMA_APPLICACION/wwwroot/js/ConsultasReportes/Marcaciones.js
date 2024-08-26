var URL_BASE_MARACAIONES = ""
var CONTROLERNAME_MARACAIONES = ""

var Marcaciones = {
    Init: function () {
        debugger;
        Marcaciones.GetMarcaciones();       
    },
    GetMarcaciones: function () {
        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE_MARACAIONES, CONTROLERNAME_MARACAIONES, "/GetMarcaciones"),
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            success: function (result) {
                var datos = result;
                $('#DataTableMarcaciones').DataTable({
                    data: result, // Pasar los datos directamente al DataTable
                    columns: [
                        { data: 'nombre', title: 'Nombre' },
                        { data: 'marcacionEntrada', title: 'ME' },
                        { data: 'marcacionInicioAlmuerzo', title: 'MIA' },
                        { data: 'marcacionFinAlmuerzo', title: 'MFA' },
                        { data: 'marcacionSalida', title: 'MS' }
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