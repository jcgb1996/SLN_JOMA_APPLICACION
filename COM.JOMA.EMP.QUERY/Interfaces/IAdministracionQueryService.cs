using COM.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Interfaces
{
    public interface IAdministracionQueryService
    {
        Task<List<MarcacionesQueryDto>> GetMarcacionesCompania(long IdCompania);
        Task<List<NotificacionesQueryDto>> GetNotificaciones(long IdCompania);
        Task<List<PacientesQueryDto>> GetPacientes(long IdCompania);
        Task<List<InteresadosQueryDto>> GetInteresados(long IdCompania);
    }
}
