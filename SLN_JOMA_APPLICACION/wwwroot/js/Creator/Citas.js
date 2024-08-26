var Agendamiento = {

    Init: function () {
        debugger;
        $('#CheckTodoElDia').on('change', function () {
            const isAllDay = $(this).is(':checked');
            if (isAllDay) {
                $('#HoraInicio, #HoraFin').val('').prop('disabled', true);
            } else {
                $('#HoraInicio, #HoraFin').prop('disabled', false);
            }
        });

        $('.dropdown-menu a').on('click', function (e) {
            e.preventDefault();

            // Obtener el texto y el color del elemento seleccionado
            const selectedText = $(this).text().trim();
            const selectedColor = $(this).find('.color-circle').css('background-color');
            const selectedValue = $(this).data('value');

            // Actualizar el botón del dropdown con el texto y el círculo de color
            $('#dropdownMenuButton').html(
                `<span class="color-circle" style="background-color:${selectedColor};"></span>${selectedText}`
            );

        });
    },

    GuardarCitas: function (event) {
        if (!Site.ValidarForumarioById("FrmCitas", event))
            return;
    }
}