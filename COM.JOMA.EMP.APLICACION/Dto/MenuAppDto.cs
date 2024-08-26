using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto
{
    public class MenuAppDto
    {// Propiedades del ítem del menú
        public string? Title { get; set; } // Título del menú (nombre)
        public string? Icon { get; set; } // Icono para el ítem del menú
        public string? Action { get; set; } // Acción (URL o nombre de acción)
        public string? Controller { get; set; } // Controlador asociado (si es MVC)
        public string? Area { get; set; } // Área asociada (si aplica)
        public List<MenuAppDto> Children { get; set; } // Lista de submenús

        // Constructor para inicializar la lista de hijos
        public MenuAppDto()
        {
            Children = new List<MenuAppDto>();
        }
    }
}
