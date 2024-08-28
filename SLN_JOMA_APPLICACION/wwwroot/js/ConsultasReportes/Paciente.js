var URL_BASE_PACIENTES = ""
var CONTROLERNAME_PACIENTES = ""

var Pacientes = {

    Init: function (UrlBase, Controller) {
        debugger;
        URL_BASE_PACIENTES = UrlBase;
        CONTROLERNAME_PACIENTES = Controller;
        Pacientes.GetPacientes();
    },
    GetPacientes: function () {
        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE_PACIENTES, CONTROLERNAME_PACIENTES, "/GetPacientes"),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                $('#DataTablePacientes').DataTable({
                    data: result, // Pasar los datos directamente al DataTable
                    columns: [

                        {
                            data: 'nombres', title: 'Nombre', className: '', render: function (data, type, row) {
                                return '<img src="../assets/images/profile/user-5.jpg" width="45" class="rounded-circle">  ' + data;
                            }
                        },
                        {
                            data: 'contacto',
                            title: 'Contacto'
                        },
                        {
                            data: 'cantidadFaltas',
                            title: 'Faltas',
                        },
                        {
                            data: 'cantidadAsistentencias',
                            title: 'Asistencias',
                        },
                        {
                            data: 'terapiasTomadas',
                            title: 'Terapias Tomadas',
                        },
                        {
                            data: 'terapiasDisponibles',
                            title: 'Terapias Disponibles',
                        },
                        {
                            data: 'tipoPlan',
                            title: 'Plan',
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