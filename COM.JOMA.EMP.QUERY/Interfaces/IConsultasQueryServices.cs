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
        /*Revisar con alan si este metodo queda en desuso GetSucursalesXIdCompañia*/
        Task<SucursalQueryDto> GetSucursalesXIdCompañia(long IdSucursal);
        Task<List<SucursalQueryDto>> GetSucursalesPorId(long IdCompania);

        #region  METODOS DE CATALAGO (Combos)
        Task<List<RolQueryDto>> GetRolesXIdEmpresa(long IdEmpresa);
        Task<List<TipoTerapiaQueryDto>> GetTipoTerapiasXIdEmpresa(long IdEmpresa);
        Task<List<SucursalQueryDto>> GetSucursalesXIdEmpresa(long IdEmpresa);
        Task<EmpresaQueryDtos> GetCompaniaXidXRuc(long IdCompania, string Ruc);

        #endregion
    }
}
