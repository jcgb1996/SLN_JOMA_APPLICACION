var URL_BASE_SUCURSAL= ""
var CONTROLERNAME_SUCURSAL= ""

var Sucursal = {

    Init: function (UrlBase, Controller) {
        debugger;
        URL_BASE_SUCURSAL = UrlBase;
        CONTROLERNAME_SUCURSAL = Controller;
        Sucursal.GetSucursal();
    },

    GetSucursal: function () {
        var table = $("#DataTableSucursales").DataTable({
            ajax: {
                url: Site.createUrl(URL_BASE_TERAPISTA, CONTROLERNAME_TERAPISTA, "/GetSucursales"),
                type: "POST",
                dataType: "json",
                dataSrc: "data",
                data: function (d) {
                    Site.IniciarLoading();
                    //Eliminar parámetros de solicitud adicionales
                    for (var key in d) {
                        if (key.indexOf("columns") == 0 || key.indexOf("order") == 0) { // Los parámetros que comienzan con columnas se eliminan
                            delete d[key];
                        }
                    }

                    d.search = d.search.value;
                    return d;
                },
                dataFilter: function (response) {// json son los datos devueltos por el servidor
                    response = JSON.parse(response);
                    if (!response.success) {
                        Site.mostrarNotificacion(response.message);
                    }

                    response = response.response;
                    var returnData = {};
                    returnData.draw = response.draw;
                    returnData.recordsTotal = response.recordsTotal;
                    returnData.recordsFiltered = response.recordsFiltered;
                    returnData.data = response.data; //JSON.stringify(response.data);
                    return JSON.stringify(returnData);
                },
                complete: function () {
                    Site.CerrarLoading();
                },
                error: function (result) {
                    Site.CerrarLoading();
                    Site.AjaxError(result);
                }
            },

            buttons: [
                {
                    text: '<i title="Nuevo Sucursal" class="fa fa-plus-circle"></i> Nuevo Sucursal',
                    className: 'btn waves-effect waves-light btn-primary',
                    action: function (e, dt, node, config) {
                        AdministracionRol.AgregarNuevo();
                    },
                    attr: {
                        title: 'Nuevo Sucursal',
                        'aria-label': 'Nuevo Sucursal'
                    },

                }
            ],
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
                                    <a class="dropdown-item d-flex align-items-center gap-3" onclick="Terapista.InactivarTerapista('${row.id}');" href="javascript:void(0)">
                                        <i class="fs-4 ti ti-trash"></i> Inactivar
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
                        return '<img src="../assets/images/profile/user-5.jpg" width="45" class="rounded-circle">  ' + data;
                    }
                },
                {
                    data: 'areaDesignada',
                    className: 'dt-center',
                    title: 'Área Designada'
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
            dom: 'Bfrtip',
            select: {
                style: 'os',
                selector: 'td:first-child'
            },
            colReorder: true,
            processing: false,
            serverSide: true,
            paging: true,
            scrollX: true,
            ordering: false,
            info: true,
            pageLength: 10,
            searching: true,
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
    GuardarDatos: function (id, event) {
        debugger;
        if (!Site.ValidarForumarioById(id, event))
            return;

        var formDataObj = Site.GetObjetoFormularioById(id);
        Site.IniciarLoading();
        $.ajax({
            type: "POST",
            url: Site.createUrl(URL_BASE_TERAPISTA, CONTROLERNAME_TERAPISTA, "/GuardarTerapista"),
            data: JSON.stringify(formDataObj),
            contentType: "application/json; charset=utf-8",
            //dataType: "json",
            success: function (response) {
                Site.CerrarLoading();
                $("#ModalTerapista").modal('hide');
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

    EditarTerapista: function (Id) {
        Site.IniciarLoading();
        $.ajax({
            type: "GET",
            url: Site.createUrl(URL_BASE_TERAPISTA, CONTROLERNAME_TERAPISTA, "/GetDatosTerapista"),
            data: { Id: Id },
            contentType: "application/json; charset=utf-8",
            //dataType: "json",
            success: function (response) {
                Site.CerrarLoading();
                $("#ContenteModal").empty().html(response);
                $("#ModalTerapista").modal('show');
            },
            error: function (result) {
                Site.AjaxError(result);
            }
        });
    },

    InactivarTerapista: function (Id) {
        Site.IniciarLoading();
        $.ajax({
            type: "POST",
            url: Site.createUrl(URL_BASE_TERAPISTA, CONTROLERNAME_TERAPISTA, "/InactivarTerapista"),
            data: { Id: Id },
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
    }


};