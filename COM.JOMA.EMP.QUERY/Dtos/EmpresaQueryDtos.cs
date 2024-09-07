using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class EmpresaQueryDtos
    {
        public int Id { get; set; }
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
