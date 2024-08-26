var Terapistas = {

    Configurar: function () {
        $("#ConfiguracionUsuario").modal("show");

        // Definir los datos del árbol, por ejemplo, el menú
        var menuData = [
            {
                "id": "1",
                "text": "Dashboard",
                "state": { "opened": true },
                "children": [
                    {
                        "id": "2",
                        "text": "Estadísticas",
                        "children": [
                            { "id": "3", "text": "Ventas" },
                            { "id": "4", "text": "Usuarios" }
                        ]
                    },
                    {
                        "id": "5",
                        "text": "Reportes",
                        "children": [
                            { "id": "6", "text": "Mensuales" },
                            { "id": "7", "text": "Anuales" }
                        ]
                    }
                ]
            },
            {
                "id": "8",
                "text": "Configuraciones",
                "children": [
                    { "id": "9", "text": "Usuarios" },
                    { "id": "10", "text": "Roles" }
                ]
            }
        ];

        // Inicializar jsTree
        $('#menuTree').jstree({
            "core": {
                "data": menuData
            },
            "plugins": [ "checkbox" ] // Activar la selección múltiple con checkboxes
        });

        // Obtener los permisos seleccionados
        $('#getSelected').click(function () {
            var selectedNodes = $('#menuTree').jstree("get_checked", true);
            var selectedIds = $.map(selectedNodes, function (node) {
                return node.id; // Devolver los IDs de los nodos seleccionados
            });
            $('#selectedPerms').html("Permisos seleccionados: " + selectedIds.join(", "));
        });
    }
}