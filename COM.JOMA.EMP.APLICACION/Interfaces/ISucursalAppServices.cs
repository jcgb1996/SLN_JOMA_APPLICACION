using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.SucursalDto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface ISucursalAppServices
    {
        public Task<JOMAResponse> RegistrarSucursal(SucursalReqDto sucursalReqDto);
        public Task<JOMAResponse> EditarSucursal(EditSucursalReqDto sucursalReqDto);
        public Task<(bool, bool)> ValidaSucursalXCedulaXRucEmpresaXCorreo(string Cedula, string RucEmpresa, string Correo);
    }
}
