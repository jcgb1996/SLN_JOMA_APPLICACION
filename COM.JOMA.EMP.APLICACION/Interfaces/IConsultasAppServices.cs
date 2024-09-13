using COM.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IConsultasAppServices
    {
        public void GetPacientes(long IdCompania);
        public Task<List<SucursalQueryDto>> GetSucursalesPorIdCompania(long IdCompania);
        public Task<SucursalQueryDto> GetSucursalesXId(long IdSucursal);
        public Task<List<TerapistaQueryDto>> GetTerapistasXRucEmpresa(string RucEmpresa);
        public Task<TerapistaQueryDto> GetTerapistasPorId(long IdTerapista, string RucCompania);
        public Task<EmpresaQueryDtos> GetCompaniaXidXRuc(long IdCompania, string Ruc);
        public Task<TerapistaQueryDto> GetTerapistasXCedulaXRucEmpresa(string Cedula, string RucCompania);
        public Task<List<TipoTerapiaQueryDto>> GetTipoTerapiasXIdEmpresa(long IdEmpresa);
        public Task<List<SucursalQueryDto>> GetSucursalesPorIdEmpresa(long IdEmpresa);
    }
}
