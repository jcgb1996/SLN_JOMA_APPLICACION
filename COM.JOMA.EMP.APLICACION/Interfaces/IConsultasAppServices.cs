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
        public Task<SucursalQueryDto> GetSucursalesXIdCompañia(long IdSucursal);
        public Task<List<SucursalQueryDto>> GetSucursalesPorId(long IdCompania);

    }
}
