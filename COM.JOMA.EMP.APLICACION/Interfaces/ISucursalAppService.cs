using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.SucursalDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface ISucursalAppService
    {
        public JOMAResponse RegistrarSucursal(SucursalReqDto sucursalReqtDto);
    }
}
