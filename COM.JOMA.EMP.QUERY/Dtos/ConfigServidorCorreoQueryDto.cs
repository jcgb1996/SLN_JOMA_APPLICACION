using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class ConfigServidorCorreoQueryDto
    {
        public ServidorCorreoQueryDto ServidorCorreoEmision { set; get; }
        public string? ServerProduccion { set; get; }
        public string? FormaCopiaMail { set; get; }
        public string? FormatoRideEnviarCorreo { set; get; }
    }
}
