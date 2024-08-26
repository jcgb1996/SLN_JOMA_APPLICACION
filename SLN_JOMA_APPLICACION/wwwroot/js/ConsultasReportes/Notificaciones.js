var URL_BASE_NOTIFICACIONES = ""
var CONTROLERNAME_NOTIFICACIONES = ""

var Notificaciones = {
    Init: function (UrlBase, Controller) {
        debugger;
        URL_BASE_NOTIFICACIONES = UrlBase;
        CONTROLERNAME_NOTIFICACIONES = Controller;
        Notificaciones.GetNotificaciones();
    },
    GetNotificaciones: function () {
        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE_NOTIFICACIONES, CONTROLERNAME_NOTIFICACIONES, "/GetNotificaciones"),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                $('#DataTableNotificaicones').DataTable({
                    data: result, // Pasar los datos directamente al DataTable
                    columns: [
                        { data: 'nombre', title: 'Nombre de la Persona' },
                        { data: 'tipo', title: 'Tipo' },
                        { data: 'fechaHora', title: 'Fecha y Hora' },
                        {
                            data: 'estado',
                            title: 'Estado',
                            render: function (data, type, row) {
                                // Si el estado es 'no enviado', pinta la celda de rojo suave
                                var color = data === 'no enviado' ? '#f8d7da' : '';
                                return '<span style="background-color:' + color + '">' + data + '</span>';
                            }
                        }
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