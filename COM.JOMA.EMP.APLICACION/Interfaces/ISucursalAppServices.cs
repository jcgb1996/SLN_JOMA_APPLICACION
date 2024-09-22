using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.SucursalDto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using COM.JOMA.EMP.QUERY.Dtos;
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
        public Task<SucursalQueryDto> GetSucursalesPorId(long IdSucursal, string Ruc);
        public Task<(bool, bool)> ValidaSucursalXCedulaXRucXCorreo(string Cedula, string Ruc, string Correo);
        public Task<List<SucursalGridQueryDto>> GetSucursalesXRuc(string Ruc);
    }
}
