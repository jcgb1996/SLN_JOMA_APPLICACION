var URL_BASE_TERAPIA = "";
var CONTROLERNAME_TERAPIA = "";

var Asistencia = {
    Init: function (UrlBase, Controller) {
        URL_BASE_TERAPIA = UrlBase;
        CONTROLERNAME_TERAPIA = Controller;
        Asistencia.GetPacientes();
    },
    GetPacientes: function () {
        $.ajax({
            type: 'POST',
            url: Site.createUrl(URL_BASE_TERAPIA, CONTROLERNAME_TERAPIA, "/GetPacientes"),
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                console.log(result); // Verifica qué datos están llegando
                var table = $('#DataTableAsistencia').DataTable({
                    data: result, // Pasar los datos directamente al DataTable
                    columns: [
                        {
                            data: 'nombre',
                            title: 'Nombre',
                            render: function (data, type, row) {
                                return '<img src="../assets/images/profile/user-5.jpg" width="45" class="rounded-circle" style="cursor: pointer;" onclick="Asistencia.ShowModal(\'' + row.cedula + '\', \'' + row.nombre + '\')"> ' + data;
                            }
                        },
                        {
                            title: 'Presente',
                            width: '10px',
                            className: 'dt-center', // Centra el contenido de la columna
                            render: function (data, type, row) {
                                return '<div class="form-check form-switch d-flex justify-content-center">' +
                                    '<input class="form-check-input secondary chk-presente" type="checkbox" id="presente-' + row.cedula + '" data-id="' + row.cedula + '">' +
                                    '<label class="form-check-label" for="presente-' + row.cedula + '"></label>' +
                                    '</div>';
                            }
                        },
                        {
                            title: 'Tarde',
                            width: '10px',
                            render: function (data, type, row) {
                                return '<div class="form-check form-switch d-flex justify-content-center">' +
                                    '<input class="form-check-input secondary chk-tarde" type="checkbox" id="tarde-' + row.cedula + '" data-id="' + row.cedula + '">' +
                                    '<label class="form-check-label" for="tarde-' + row.cedula + '"></label>' +
                                    '</div>';
                            },
                        },
                        {
                            title: 'Faltó',
                            width: '10px',
                            render: function (data, type, row) {
                                return '<div class="form-check form-switch d-flex justify-content-center">' +
                                    '<input class="form-check-input secondary chk-falto" type="checkbox" id="falto-' + row.cedula + '" data-id="' + row.cedula + '">' +
                                    '<label class="form-check-label" for="falto-' + row.cedula + '"></label>' +
                                    '</div>';
                            },
                        }
                    ],
                    dom: 'Bfrtip',
                    colReorder: true,
                    paging: true,
                    scrollX: true,
                    ordering: true,
                    info: true,
                    pageLength: 10,
                    searching: false,
                    responsive: true,
                    destroy: true, // Permite reinicializar la tabla
                    language: {
                        processing: "Procesando...",
                        search: "Buscar: _INPUT_",
                        lengthMenu: 'Mostrar _MENU_',
                        info: 'Mostrando desde _START_ al _END_ de _TOTAL_ registros',
                        infoEmpty: 'Mostrando ningún elemento.',
                        infoFiltered: '(filtrado _MAX_ elementos total)',
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

                // Evento para asegurarse de que solo un checkbox sea seleccionado por fila
                $('#DataTableAsistencia').on('change', 'input[type="checkbox"]', function () {
                    var $row = $(this).closest('tr');
                    $row.find('input[type="checkbox"]').not(this).prop('checked', false);
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
    },
    ShowModal: function (cedula, nombre) {
        // Llenar los campos del modal con la información de la fila seleccionada
        $('#modalTitle').text('Registrar Actividad para ' + nombre);
        $('#modalCedula').val(cedula);
        $('#modalNombre').val(nombre);

        // Mostrar el modal
        $('#modalRegistroActividad').modal('show');
    }
}
