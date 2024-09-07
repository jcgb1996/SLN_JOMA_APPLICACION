using COM.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IAdministracionAppServices
    {
        Task<List<MarcacionesQueryDto>> GetMarcacionesCompania(long IdCompania);
        Task<List<NotificacionesQueryDto>> GetNotificaciones(long IdCompania);
        Task<List<PacientesQueryDto>> GetPacientes(long IdCompania);
        Task<List<InteresadosQueryDto>> GetInteresados(long IdCompania);
        Task<Tuple<bool, EmpresaQueryDtos>> ExisteCompania(long IdCompania, string? Ruc);
    }
}
