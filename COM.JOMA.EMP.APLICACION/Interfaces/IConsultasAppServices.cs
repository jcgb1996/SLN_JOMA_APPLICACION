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
        Task<List<SucursalQueryDto>> GetSucursalesPorIdCompania(long IdCompania);
        Task<SucursalQueryDto> GetSucursalesXId(long IdSucursal);

        #region  METODOS DE CATALAGO (Comobos)
        Task<List<SucursalQueryDto>> GetSucursalesPorIdEmpresa(long IdEmpresa);
        Task<List<RolQueryDto>> GetRolesXIdEmpresa(long IdEmpresa);
        Task<List<TipoTerapiaQueryDto>> GetTipoTerapiasXIdEmpresa(long IdEmpresa);
        Task<EmpresaQueryDtos> GetCompaniaXidXRuc(long IdCompania, string Ruc);
        #endregion
    }
}
