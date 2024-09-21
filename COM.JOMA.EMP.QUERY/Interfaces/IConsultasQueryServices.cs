using COM.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Interfaces
{
    public interface IConsultasQueryServices
    {
        public Task<EmpresaQueryDtos> GetCompaniaXidXRuc(long IdCompania, string Ruc);
        /*Revisar con alan si este metodo queda en desuso GetSucursalesXIdCompañia*/
        public Task<SucursalQueryDto> GetSucursalesXIdCompañia(long IdSucursal);
        public Task<List<SucursalQueryDto>> GetSucursalesXIdEmpresa(long IdEmpresa);
        public Task<List<SucursalQueryDto>> GetSucursalesPorId(long IdCompania);
        public Task<List<TipoTerapiaQueryDto>> GetTipoTerapiasXIdEmpresa(long IdEmpresa);
        
    }
}
