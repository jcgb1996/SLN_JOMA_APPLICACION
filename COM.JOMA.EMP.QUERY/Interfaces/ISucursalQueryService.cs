using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.Dtos;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Interfaces
{
    public interface ISucursalQueryService
    {
        public bool RegistrarSucursal(Sucursal sucursal);
        Task<bool> EditarSucursal(Sucursal sucursal);
        Task<SucursalQueryDto> GetSucursalesXIdXIdEmpresa(long IdSucursal, long IdEmpresa); 
        Task<List<SucursalGridQueryDto>> GetSucursalesXRuc(string Ruc);
        Task<ValidaTerapistaQueryDto> ValidaTerapistaXCedulaXCorreo(string Cedula, string RucEmpresa, string Correo);
    }
}
