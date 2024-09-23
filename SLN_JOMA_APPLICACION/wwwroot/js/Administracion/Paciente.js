var URL_BASE_PACIENTE = ""
var CONTROLERNAME_PACIENTE = ""

var Paciente = {

    Init: function (UrlBase, Controller) {
        debugger;
        URL_BASE_PACIENTE = UrlBase;
        CONTROLERNAME_PACIENTE = Controller;
        Paciente.GetPacientes();
    },
    GetPacientes: function () {
        Site.IniciarLoading();
        debugger;
        $.ajax({
            url: Site.createUrl(URL_BASE_PACIENTE, CONTROLERNAME_PACIENTE, "/GetPacientes"),
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            success: function (response) {
                Site.CerrarLoading();
                $("#ModalPaciente").modal('hide');
                if (response && response.success) {
                } else if (response && response.message) {
                    Site.mostrarNotificacion(response.message, 2);
                } else {
                    Site.mostrarNotificacion(response.message, 2);
                }

                $('#DataTablePacientes').DataTable({
                    data: response.response.data,
                    destroy: true, // Utiliza 'destroy' para reinicializar si ya existe
                    columns: [
                        {
                            data: null,
                            title: 'Acciones',
                            className: 'dt-center',
                            width: '5px',
                            render: function (data, type, row) {
                                return `
                        <div class="dropdown dropstart">
                            <a href="javascript:void(0)" class="text-muted" id="dropdownMenuButton_${row.id}" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="ti ti-dots fs-5"></i>
                            </a>
                            <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton_${row.id}">
                                
                                <li>
                                    <a class="dropdown-item d-flex align-items-center gap-3" onclick="Terapista.EditarTerapista('${row.id}');" href="javascript:void(0)">
                                        <i class="fs-4 ti ti-edit"></i> Editar
                                    </a>
                                </li>
                                <li>
                                    <a class="dropdown-item d-flex align-items-center gap-3" onclick="Terapista.ReenvioMailBienvenida('${row.cedula}', '${row.nombreUsuario}');" href="javascript:void(0)">
                                        <i class="ti ti-mail"></i> Reenviar correo bienvenida
                                    </a>
                                </li>
                                <li>
                                    <a class="dropdown-item d-flex align-items-center gap-3" onclick="Terapista.InactivarTerapista('${row.id}');" href="javascript:void(0)">
                                        <i class="ti ti-user-plus"></i> Asignar pacientes
                                    </a>
                                </li>
                            </ul>
                        </div>
                    `;
                            }
                        },
                        {
                            data: 'id',
                            title: 'ID',
                            className: 'dt-center',
                            visible: false,
                        },
                        {
                            data: 'nombresApellidosPaciente',
                            title: 'Paciente', render: function (data, type, row) {
                                return '<img src="../assets/images/profile/user-5.jpg" width="30" class="rounded-circle">  ' + row.nombresApellidosPaciente;
                            }
                        },
                        {
                            data: 'cedulaPaciente',
                            title: 'Cedula',
                        },
                        {
                            data: 'representanteLegal',
                            title: 'Repre. legal'
                        },
                        {
                            data: 'cedulaRepresentante',
                            title: 'Cedula'
                        },
                        {
                            data: 'nombreMadre',
                            title: 'Madre'
                        },
                        {
                            data: 'nombrePadre',
                            title: 'Padre'
                        },
                        {
                            data: 'correoNotificacion',
                            title: 'Notificación a:'
                        },
                        {
                            data: 'estado',
                            className: 'dt-center',
                            title: 'Estado', render: function (data, type, row) {


                                if (data) {
                                    var Estado = "Activo";
                                    return `
                        <span class="badge bg-success-subtle text-success fw-semibold fs-2 gap-1 d-inline-flex align-items-center">
                          <i class="ti ti-circle fs-3"></i>${Estado}
                        </span>
                    `;
                                }

                                if (data) {
                                    var Estado = "Inactivo";
                                    return `
                        <span class="badge text-bg-light text-dark fw-semibold fs-2 gap-1 d-inline-flex align-items-center">
                          <i class="ti ti-circle fs-3"></i>${Estado}
                        </span>
                    `;
                                }


                            }
                        }
                    ],
                    buttons: [
                        {
                            text: '<i title="Nuevo Terapsita" class="fa fa-plus-circle"></i> Nuevo Terapista',
                            className: 'btn waves-effect waves-light btn-primary',
                            action: function (e, dt, node, config) {
                                Paciente.NuevoPaciente();
                            },
                            attr: {
                                title: 'Nuevo paciente',
                                'aria-label': 'Nuevo paciente'
                            },

                        }
                    ],
                    dom: 'Bfrtip',
                    colReorder: true,
                    processing: false,
                    serverSide: false,
                    paging: false,
                    scrollX: true,
                    ordering: false,
                    info: true,
                    pageLength: 10,
                    searching: true,
                    responsive: true,
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
            error: function (xhr, status, error) {
                Site.CerrarLoading();
                Site.AjaxError(result);
            }
        });
    },
    NuevoPaciente: function () {
        //Terapista.LimpiarComponentes();
        //Terapista.InitSelect2('IdSucursal', 'Seleccione una Sucursal', Terapista.CmbSucursales);
        //Terapista.InitSelect2('Genero', 'Seleccione un género', Terapista.CmbGenero);
        //Terapista.InitSelect2('IdTipoTerapia', 'Seleccione un tipo', Terapista.CmbTipoTerapista);
        //Terapista.InitSelect2('Estado', 'Seleccione un Estado', Terapista.CmbEstado, 1);
        //Terapista.InitSelect2('IdRol', 'Seleccione un Rol', Terapista.CmbRol, 1);
        //Terapista.InitDataPicker();
        $("#ModalPaciente").modal('show');
    },
    GuardarDatos: function (event, id) {
        debugger;
        if (!Site.ValidarForumarioById(id, event))
            return;

        var formDataObj = Site.GetObjetoFormularioById(id);
        Site.IniciarLoading();
        $.ajax({
            type: "POST",
            url: Site.createUrl(URL_BASE_PACIENTE, CONTROLERNAME_PACIENTE, "/GuardarPaciente"),
            data: JSON.stringify(formDataObj),
            contentType: "application/json; charset=utf-8",
            //dataType: "json",
            success: function (response) {
                Site.CerrarLoading();
                if (response && response.success) {
                    Site.mostrarNotificacion(response.message, 1);
                } else if (response && response.message) {
                    Site.mostrarNotificacion("Error al guardar los datos: " + response.message, 2);
                } else {
                    Site.mostrarNotificacion("Ocurrió un error inesperado." + response.message, 2);
                }
            },
            error: function (result) {
                Site.AjaxError(result);
            }
        });

    },
};