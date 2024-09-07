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
        public Task<List<TerapistaQueryDto>> GetTerapistasPorIdCompania(long IdCompania);
        public Task<TerapistaQueryDto> GetTerapistasPorId(long IdTerapista);
        public Task<List<SucursalQueryDto>> GetSucursalesPorIdCompania(long IdCompania);
        public Task<SucursalQueryDto> GetSucursalesXId(long IdSucursal);
    }
}
