var URL_BASE_TERAPISTA = ""
var CONTROLERNAME_TERAPISTA = ""

var Terapista = {
    CmbGenero: "",
    CmbTipoTerapista: "",
    CmbSucursales: "",
    CmbEstado: "",
    CmbRol: "",

    Init: function (UrlBase, Controller) {
        debugger;
        URL_BASE_TERAPISTA = UrlBase;
        CONTROLERNAME_TERAPISTA = Controller;
        Terapista.GetTerapistas();
    },

    GetTerapistas: function () {
        debugger;
        $.ajax({
            url: Site.createUrl(URL_BASE_TERAPISTA, CONTROLERNAME_TERAPISTA, "/GetTerapistas"),
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            success: function (response) {
                Site.CerrarLoading();
                $("#ModalTerapista").modal('hide');
                if (response && response.success) {
                } else if (response && response.message) {
                    Site.mostrarNotificacion(response.message, 2);
                } else {
                    Site.mostrarNotificacion(response.message, 2);
                }

                $('#DataTableTerapistas').DataTable({
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
                            data: 'nombre',
                            title: 'Nombre', render: function (data, type, row) {
                                return '<img src="../assets/images/profile/user-5.jpg" width="30" class="rounded-circle">  ' + row.nombre + " " + row.apellido;
                            }
                        },
                        {
                            data: 'cedula',
                            title: 'Cedula',
                        },
                        {
                            data: 'email',
                            title: 'Correo'
                        },
                        {
                            data: 'telefonoContacto',
                            title: 'Teléfono'
                        },
                        {
                            data: 'nombreTerapia',
                            title: 'Tipo Terapia'
                        },
                        {
                            data: 'nombreRol',
                            title: 'Rol'
                        },
                        {
                            data: 'estado',
                            className: 'dt-center',
                            title: 'Estado', render: function (data, type, row) {


                                if (data === 1) {
                                    var Estado = "Activo";
                                    return `
                        <span class="badge bg-success-subtle text-success fw-semibold fs-2 gap-1 d-inline-flex align-items-center">
                          <i class="ti ti-circle fs-3"></i>${Estado}
                        </span>
                    `;
                                }

                                if (data === 0) {
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
                                Terapista.NuevoTerapista();
                            },
                            attr: {
                                title: 'Nuevo Terapsita',
                                'aria-label': 'Nuevo Terapsita'
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

    procesarDatos: function (datos) {
        // Aquí puedes agregar cualquier lógica de procesamiento de datos necesaria
        // Por ejemplo, podrías querer modificar, agregar o eliminar ciertos campos
        return datos.map(function (item) {
            // Supongamos que queremos agregar un nuevo campo
            item.nombreCompleto = item.nombre + " " + item.apellido;
            return item;
        });
    },

    InitSelect2: function (id, placeholder, Data, values) {
        if (!Data || Data.length === 0) {
            Data = [{
                id: '',
                text: 'No hay datos que mostrar'
            }];
        }

        $("#" + id).select2({
            placeholder: placeholder,
            dropdownParent: $("#ModalTerapista"),
            width: '100%',
            language: {
                noResults: function () {
                    return "No se encontraron resultados";
                }
            },
            data: Data.map(function (terapia) {
                return {
                    id: terapia.id || '',
                    text: terapia.Nombre || terapia.text
                };
            })
        });

        if (values) {
            $("#" + id).val(values).trigger('change');
        }
    },

    InitDataPicker: function () {
        $('.datepicker').datepicker({
            language: 'es',
            format: 'yyyy-mm-dd',
            clearBtn: false,
            todayHighlight: false,
            daysOfWeekHighlighted: "0,6",
            autoclose: true,
            todayBtn: "linked",
            title: "Selecciona una fecha",
            uiLibrary: 'bootstrap5'
        }).on('show', function (e) {
            if ($(this).val() === '0001-01-01' || $(this).val() === '') {
                $(this).datepicker('update', ''); // Limpia el valor si es inválido
            }
        });
    },

    GuardarDatos: function (id, event, idTerapista) {
        debugger;
        if (!Site.ValidarForumarioById(id, event))
            return;

        var formDataObj = Site.GetObjetoFormularioById(id);
        formDataObj.NombreTerapia = $('#IdTipoTerapia option:selected').text();
        formDataObj.NombreRol = $('#IdRol option:selected').text();
        formDataObj.Estado = formDataObj.Estado == "99" ? 0 : formDataObj.Estado;


        Site.IniciarLoading();

        var con = "/GuardarTerapista";
        if (idTerapista > 0) {
            con = "/EditarTerapista";
        }

        $.ajax({
            type: "POST",
            url: Site.createUrl(URL_BASE_TERAPISTA, CONTROLERNAME_TERAPISTA, con),
            data: JSON.stringify(formDataObj),
            contentType: "application/json; charset=utf-8",
            //dataType: "json",
            success: function (response) {
                Site.CerrarLoading();
                $("#ModalTerapista").modal('hide');
                if (response && response.success) {
                    Site.mostrarNotificacion(response.message, 1);
                    Terapista.GetTerapistas();
                } else if (response && response.message) {
                    Site.mostrarNotificacion("Error al guardar los datos: " + response.message, 2);
                } else {
                    Site.mostrarNotificacion("Ocurrió un error inesperado." + response.message, 2);
                }
            },
            error: function (result) {
                Site.CerrarLoading();
                Site.AjaxError(result);
            }
        });

    },
    EditarTerapista: function (Id) {
        Site.IniciarLoading();
        var Id = JSON.stringify(Id);
        $.ajax({
            type: "POST",
            url: Site.createUrl(URL_BASE_TERAPISTA, CONTROLERNAME_TERAPISTA, "/GetDatosTerapista"),
            data: Id,
            contentType: "application/json; charset=utf-8",
            //dataType: "json",
            success: function (response) {
                Site.CerrarLoading();
                Terapista.LimpiarComponentes();
                $("#ContenteModal").empty().html(response);
                $("#ModalTerapista").modal('show');
            },
            error: function (result) {
                Site.CerrarLoading();
                Site.AjaxError(result);
            }
        });
    },
    ReenvioMailBienvenida: function (cedula, nombreUsuario) {
        debugger;
        Site.IniciarLoading();
        var url = Site.createUrl(URL_BASE_TERAPISTA, CONTROLERNAME_TERAPISTA, "/ReenvioMailBienvenida") + "?cedula=" + encodeURIComponent(cedula) + "&nombreUsuario=" + encodeURIComponent(nombreUsuario);
        console.log("URL de la solicitud:", url); // Imprime la URL para verificar
        $.ajax({
            type: "POST",
            url: Site.createUrl(URL_BASE_TERAPISTA, CONTROLERNAME_TERAPISTA, "/ReenvioMailBienvenida") + "?cedula=" + encodeURIComponent(cedula) + "&nombreUsuario=" + encodeURIComponent(nombreUsuario),
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
                Site.CerrarLoading();
                Site.AjaxError(result);
            }
        });
    },

    LimpiarComponentes: function () {
        Terapista.Limpiar("IdSucursal");
        Terapista.Limpiar("Genero");
        Terapista.Limpiar("IdTipoTerapia");
        Terapista.Limpiar("Estado");
        Terapista.Limpiar("IdRol");
    },

    Limpiar: function (id) {
        var $select = $('#' + id).select2();
        $select.empty();
    },
    NuevoTerapista: function () {
        Terapista.LimpiarComponentes();
        Terapista.InitSelect2('IdSucursal', 'Seleccione una Sucursal', Terapista.CmbSucursales);
        Terapista.InitSelect2('Genero', 'Seleccione un género', Terapista.CmbGenero);
        Terapista.InitSelect2('IdTipoTerapia', 'Seleccione un tipo', Terapista.CmbTipoTerapista);
        Terapista.InitSelect2('Estado', 'Seleccione un Estado', Terapista.CmbEstado, 1);
        Terapista.InitSelect2('IdRol', 'Seleccione un Rol', Terapista.CmbRol, 1);
        Terapista.InitDataPicker();
        $("#ModalTerapista").modal('show');
    },
    AsiganarValorDireccion: function (Direccion) {
        $("#Direccion").val(Direccion).trigger('change');
    }
};